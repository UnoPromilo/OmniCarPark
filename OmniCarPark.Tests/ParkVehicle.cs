using System.Net;
using System.Net.Http.Json;
using OmniCarPark.Tests.DTOs;

namespace OmniCarPark.Tests;

public class ParkVehicleReturnsSpace
{
    private ApiTestClient _client;
    private const int AvailableParkingSpaces = 4;

    [SetUp]
    public async Task SetUp()
    {
        _client = ApiTestClient.Create();
        await _client.RestoreDatabase(AvailableParkingSpaces);
    }

    [Test]
    public async Task ShouldReturnSpace()
    {
        var earliestAllowedTime = DateTime.UtcNow.AddSeconds(-1);

        var response = await TakeSpace("REG123");
        Assert.That(response.IsSuccessStatusCode);

        var result = await response.Content.ReadFromJsonAsync<ParkingPostResponse>();

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.VehicleReg, Is.EqualTo("REG123"));
            Assert.That(result.SpaceNumber, Is.EqualTo(1));
            Assert.That(result.TimeIn, Is.GreaterThanOrEqualTo(earliestAllowedTime));
        });
    }

    [Test]
    public async Task DuplicatedEntry()
    {
        await TakeSpace("TEST1");
        var responseMessage = await TakeSpace("test1");

        Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.UnprocessableEntity));
    }

    [Test]
    public async Task NoEmptySpaces()
    {
        await _client.RestoreDatabase(AvailableParkingSpaces);
        for (var i = 0; i < AvailableParkingSpaces; i++)
        {
            await TakeSpace($"Valid{i}");
        }

        var responseMessage = await TakeSpace("Invalid");

        Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
    }

    [Test]
    public async Task ShouldAllocateFirstAvailableSpace()
    {
        for (var i = 0; i < AvailableParkingSpaces; i++)
        {
            var validResponse = await TakeSpace($"Test{i}");
            Assert.That(validResponse.IsSuccessStatusCode);
            var validResult = await validResponse.Content.ReadFromJsonAsync<ParkingPostResponse>();
            Assert.That(validResult, Is.Not.Null);
            Assert.That(validResult.SpaceNumber, Is.EqualTo(i + 1));
        }
    }

    private async Task<HttpResponseMessage> TakeSpace(string vehicleReg, int vehicleType = 1)
    {
        var request = new ParkingPostRequest
        {
            VehicleReg = vehicleReg,
            VehicleType = vehicleType
        };

        return await _client.PostAsync("parking", request);
    }
}