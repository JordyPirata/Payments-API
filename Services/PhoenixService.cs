using Payments_API.Interfaces;
using Payments_API.Models;
using System.Net.Http.Headers;
using System.Text;

namespace Payments_API.Services;

public class PhoenixService : IPhoenixService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiPassword;

    public PhoenixService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // GET API PASSWORD FROM ENVIRONMENT VARIABLE
        _apiPassword = Environment.GetEnvironmentVariable("PHOENIX_API_PASSWORD") 
            ?? throw new ArgumentNullException("PHOENIX_API_PASSWORD");

        // Set up basic authentication
        var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($":{_apiPassword}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

    }

    public async Task<object> CreateInvoiceAsync(InvoiceRequest request)
    {
        var requestData = new Dictionary<string, string>
        {
            { "description", request.description },
            { "amountSat", request.amountSat.ToString() }
        };

        if (!string.IsNullOrEmpty(request.externalId))
        {
            requestData.Add("externalId", request.externalId);
        }

        if (!string.IsNullOrEmpty(request.webhookUrl))
        {
            requestData.Add("webhookUrl", request.webhookUrl);
        }

        var content = new FormUrlEncodedContent(requestData);
        var response = await _httpClient.PostAsync("/createinvoice", content); 

        var responseContent = await response.Content.ReadFromJsonAsync<object>() 
            ?? throw new InvalidOperationException("Response content is null");
            
        return responseContent;
    }

}
