using Microsoft.AspNetCore.Mvc.Testing;

public class WeatherForecastControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WeatherForecastControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        //await using var application = new WebApplicationFactory<Api.Startup>();
        //using var client = application.CreateClient();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/weatherforecast");
        //response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }
}