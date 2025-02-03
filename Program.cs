using Payments_API.Interfaces;
using Payments_API.Services;
using Payments_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI support
builder.Services.AddOpenApi();

// Register custom services
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IOpenPayService, OpenPayService>();
builder.Services.AddScoped<IPhoenixService, PhoenixService>();

// Configure HTTP client for PhoenixService
builder.Services.AddHttpClient<IPhoenixService, PhoenixService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServicesUrl:Phoenix"] 
        ?? throw new ArgumentNullException("ServicesUrl:Phoenix"));
});

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


// Use DI for the open pay service
app.MapGet("/openpay", (IOpenPayService openPayService) =>
{
    return openPayService;
}).WithName("GetOpenPay");

//Use DI for the phoenix service
app.MapGet("/phoenix/createinvoice", (IPhoenixService phoenixService) =>
{
    // This is a test request
    var invoiceRequest = new InvoiceRequest
    {
        description = "Test",
        amountSat = 100000,
        externalId = null,
        webhookUrl = null  
    };
    // TODO: Add error handling AND return an object not a string
    return phoenixService.CreateInvoiceAsync(invoiceRequest);
}).WithName("GetPhoenix");

// Use DI for the phoenix service


app.Run();