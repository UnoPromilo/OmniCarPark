using System.Net;
using System.Net.Http.Json;
using OmniCarPark.Tests.DTOs;

namespace OmniCarPark.Tests;

public class ExitParking
{
    [TestCase(1, 1, 0.1)]
    [TestCase(2, 2, 0.4)]
    [TestCase(3, 3, 1.2)]
    [TestCase(10, 3, 6)]
    [TestCase(11, 3, 6.4)]
    public async Task Valid(int minutes, int vehicleType, double expectedFee)
    {
        const string vehicleReg = "XDDD";
        var client = ApiTestClient.Create();
        await client.RestoreDatabase(10);
        var timeIn = DateTime.UtcNow;
        await client.SetFixedTime(timeIn);
        var allocateRequest = new ParkingPostRequest
        {
            VehicleReg = vehicleReg,
            VehicleType = vehicleType
        };
        await client.PostAsync("parking", allocateRequest);

        var timeOut = timeIn.AddMinutes(minutes);
        await client.SetFixedTime(timeOut);
        var exitRequest = new ParkingExitPostRequest
        {
            VehicleReg = vehicleReg
        };
        var exitResponse = await client.PostAsync("parking/exit", exitRequest);

        Assert.That(exitResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var exitContent = await exitResponse.Content.ReadFromJsonAsync<ParkingExitPostResponse>();
        Assert.That(exitContent, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(exitContent.VehicleReg, Is.EqualTo(vehicleReg));
            Assert.That(exitContent.TimeIn, Is.EqualTo(timeIn));
            Assert.That(exitContent.TimeOut, Is.EqualTo(timeOut));
            Assert.That(exitContent.VehicleCharge, Is.EqualTo(expectedFee));
        });
    }

    [Test]
    public async Task NotParked()
    {
        var client = ApiTestClient.Create();
        await client.RestoreDatabase(10);
        var exitRequest = new ParkingExitPostRequest
        {
            VehicleReg = "XDDD"
        };
        var exitResponse = await client.PostAsync("parking/exit", exitRequest);

        Assert.That(exitResponse.StatusCode, Is.EqualTo(HttpStatusCode.UnprocessableEntity));
    }

    [Test]
    public async Task CantExitTwice()
    {
        const string vehicleReg = "XDDD";
        var client = ApiTestClient.Create();
        await client.RestoreDatabase(10);
        var allocateRequest = new ParkingPostRequest
        {
            VehicleReg = vehicleReg,
            VehicleType = 1
        };
        await client.PostAsync("parking", allocateRequest);
        var exitRequest = new ParkingExitPostRequest
        {
            VehicleReg = vehicleReg
        };
        await client.PostAsync("parking/exit", exitRequest);
        var exitResponse = await client.PostAsync("parking/exit", exitRequest);

        Assert.That(exitResponse.StatusCode, Is.EqualTo(HttpStatusCode.UnprocessableEntity));
    }


    [TearDown]
    public async Task Clean()
    {
        var client = ApiTestClient.Create();
        await client.ResetFixedTime();
    }
}