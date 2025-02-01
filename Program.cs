using Payments_API.Interfaces;
using Payments_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI support
builder.Services.AddOpenApi();

// Register custom services
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IOpenPayService, OpenPayService>();
builder.Services.AddScoped<IPhoenixService, PhoenixService>();

var app = builder.Build();

// Enable OpenAPI in development mode
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Use DI for the weather forecast
app.MapGet("/weatherforecast", (IWeatherService weatherService) =>
{
    return weatherService.GetForecast();
}).WithName("GetWeatherForecast");

/*
// Use DI for the open pay service
app.MapGet("/openpay", (IOpenPayService openPayService) =>
{
    return openPayService.GetOpenPay();
}).WithName("GetOpenPay");

// Use DI for the phoenix service
app.MapGet("/phoenix", (IPhoenixService phoenixService) =>
{
    return phoenixService.GetPhoenix();
}).WithName("GetPhoenix");
*/

app.Run();