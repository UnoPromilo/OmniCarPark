using System.Net;
using System.Net.Http.Json;
using OmniCarPark.Tests.DTOs;

namespace OmniCarPark.Tests;

public class FixedTime
{
    [Test]
    public async Task GetTime_ShouldReturnUtcNow()
    {
        var utcNow = DateTime.UtcNow.AddSeconds(-1);
        var client = ApiTestClient.Create();
        var response = await client.GetAsync("internal/time/get");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var content = await response.Content.ReadFromJsonAsync<GetTimeResponse>();
        Assert.That(content, Is.Not.Null);
        Assert.That(content.UtcNow, Is.GreaterThanOrEqualTo(utcNow));
    }

    [Test]
    public async Task SetTime_Then_GetTime_ShouldReturnFixedTime()
    {
        var fixedTime = DateTime.UtcNow + TimeSpan.FromHours(1);
        var client = ApiTestClient.Create();
        await client.SetFixedTime(fixedTime);
        var response = await client.GetAsync("internal/time/get");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var content = await response.Content.ReadFromJsonAsync<GetTimeResponse>();
        Assert.That(content, Is.Not.Null);
        Assert.That(content.UtcNow, Is.EqualTo(fixedTime));
    }

    [Test]
    public async Task SetTime_Then_ResetTime_ShouldReturnUtcNow()
    {
        var utcNow = DateTime.UtcNow;
        var fixedTime = utcNow + TimeSpan.FromHours(1);
        var client = ApiTestClient.Create();
        await client.SetFixedTime(fixedTime);
        await client.ResetFixedTime();
        var response = await client.GetAsync("internal/time/get");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var content = await response.Content.ReadFromJsonAsync<GetTimeResponse>();
        Assert.That(content, Is.Not.Null);
        Assert.That(content.UtcNow, Is.GreaterThanOrEqualTo(utcNow));
        Assert.That(content.UtcNow, Is.Not.EqualTo(fixedTime));
    }

    [TearDown]
    public async Task Clean()
    {
        var client = ApiTestClient.Create();
        await client.ResetFixedTime();
    }
}