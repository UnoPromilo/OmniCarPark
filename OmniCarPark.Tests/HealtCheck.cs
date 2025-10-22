using System.Net;

namespace OmniCarPark.Tests;

public class Tests
{
    [Test]
    public async Task HealthCheckShouldReturnOk()
    {
        var client =  ApiTestClient.Create();
        var response = await client.GetAsync("health");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}