using System.Net.Http.Json;
using OmniCarPark.Tests.DTOs;
[assembly: LevelOfParallelism(1)]
namespace OmniCarPark.Tests;

public class ApiTestClient(string baseUrl)
{
    private readonly HttpClient _client = new() { BaseAddress = new Uri(baseUrl) };
    public string BaseUrl { get; } = baseUrl;

    public Task<HttpResponseMessage> GetAsync(string endpoint) => _client.GetAsync(endpoint);

    public Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data) =>
        _client.PostAsJsonAsync(endpoint, data);

    public static ApiTestClient Create()
    {
        return new("http://localhost:8080");
    }

    public async Task RestoreDatabase(int availableParkingSpaces)
    {
        var request = new RestoreDatabaseRequest
        {
            AvailableParkingSpaces
                = availableParkingSpaces
        };
        var result = await PostAsync("internal/restore", request);
        if (!result.IsSuccessStatusCode)
        {
            throw new("Something went wrong while restoring database");
        }
    }

    public async Task SetFixedTime(DateTime fixedTime)
    {
        var request = new SetFixedTimeRequest
        {
            FixedTime = fixedTime
        };

        var result = await PostAsync("internal/time/set", request);
        if (!result.IsSuccessStatusCode)
        {
            throw new("Something went wrong while setting time");
        }
    }

    public async Task ResetFixedTime()
    {
        var result = await PostAsync("internal/time/reset", new object());
        if (!result.IsSuccessStatusCode)
        {
            throw new("Something went wrong while resetting time");
        }
    }
}