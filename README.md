## How to Run

Run the following command in the root directory:

```bash
docker compose up
```

The API should be available at http://localhost:8080
.
---

### Tests

`OmniCarPark.Tests` contains a set of E2E tests.
`OmniCarPark.UnitTests` contains a set of unit tests.

---

### Testing endpoints

Because of limited time, I decided to skip the integration tests and go with E2E tests. Because of that I had to create
a set of endpoints to test the API. Of course, this is not a production-ready solution, but for this project it was
enough.

| Method | Endpoint               | Description                                                                 |
|--------|------------------------|-----------------------------------------------------------------------------|
| `POST` | `/internal/time/set`   | Sets a static time for testing.                                             |
| `POST` | `/internal/time/reset` | Resets the static time.                                                     |
| `GET`  | `/internal/time/get`   | Returns the current static time.                                            |
| `POST` | `/internal/restore`    | Clears and restores the database to the specified number of parking spaces. |

Check `OmniCarPark.Tests/Controllers/FixedTime.cs` for more details.

You can use them to test the API by your own.

---

### Assumptions

- All the DateTimes are in UTC
- Vehicle registration is case insensitive
- Valid vehicle registrations are 3-20 characters long, containing only letters and numbers
- Api should be able to handle concurrent requests and not allow parking the same vehicle twice at any time, but it may
  throw an exception if the database handles the concurrency
- Exit parking request is not that safe, it should handle most of the cases well, but in close timing it may allow to
  exit the parking space twice
- Fee may have rounding errors, but it is because of the requested contract, check `FeeCalculator.cs` to see the details
- Time rounding rules:
    - Total minutes are **rounded up** (e.g. 4:56 → 5 minutes).
    - The **5-minute fee increment** is **rounded down** (e.g. 4:55 → no fee, 9:56 → +£1).

### End notes

- Naming can be a bit chaotic and not consistent
- I wanted to convert repositories into specifications, but there was no time for it
- Command and Query handling is primitive, but even that is overkill for this project
- Type safe values like RegistrationNumber and SpaceNumber should be used in data models too
- Command handlers and Repositories should be divided, and some logic should be moved to services or pipelines