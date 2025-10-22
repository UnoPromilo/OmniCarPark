using System.Net;
using System.Net.Http.Json;
using OmniCarPark.Tests.DTOs;

namespace OmniCarPark.Tests;

public class GetParkingInfo
{
    private ApiTestClient _client;

    [SetUp]
    public void SetUp()
    {
        _client = ApiTestClient.Create();
    }

    [TestCase(1, 0)]
    [TestCase(20, 5)]
    public async Task Test(int availableParkingSpaces, int takenParkingSpaces)
    {
        await _client.RestoreDatabase(availableParkingSpaces);

        for (var i = 0; i < takenParkingSpaces; i++)
        {
            await TakeSpace($"REG{i}");
        }

        var response = await _client.GetAsync("parking");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<ParkingGetResponse>();
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.AvailableSpaces, Is.EqualTo(availableParkingSpaces - takenParkingSpaces));
            Assert.That(result.OccupiedSpaces, Is.EqualTo(takenParkingSpaces));
        });
    }

    private async Task TakeSpace(string vehicleReg)
    {
        var request = new ParkingPostRequest
        {
            VehicleReg = vehicleReg,
            VehicleType = 1
        };

        await _client.PostAsync("parking", request);
    }
}