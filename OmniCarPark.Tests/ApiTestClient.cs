using System.Net.Http.Json;

namespace OmniCarPark.Tests;

public class ApiTestClient(string baseUrl)
{
    private readonly HttpClient _client = new() { BaseAddress = new Uri(baseUrl) };
    public string BaseUrl { get; } = baseUrl;

    public Task<HttpResponseMessage> GetAsync(string endpoint) => _client.GetAsync(endpoint);
    public Task<HttpResponseMessage> PostJsonAsync<T>(string endpoint, T data) =>
        _client.PostAsJsonAsync(endpoint, data);

    public Task<T?> ReadJsonAsync<T>(HttpResponseMessage response) =>
        response.Content.ReadFromJsonAsync<T>();

    public static ApiTestClient Create()
    {
        return new("http://localhost:8080");
    }
}