namespace Payments_API.Models;

public class InvoiceRequest
{
    public string description { get; set; } = string.Empty;
    public int amountSat { get; set; }
    public string? externalId { get; set; } = null;
    public string? webhookUrl { get; set; } = null;
}
