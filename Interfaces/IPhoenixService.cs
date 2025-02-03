namespace Payments_API.Interfaces;
using Payments_API.Models;

public interface IPhoenixService 
{
    Task<object> CreateInvoiceAsync(InvoiceRequest request);
}