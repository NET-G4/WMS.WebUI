using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Mappings;

public static class SaleMappings
{
    public static TransactionView ToTransaction(this SaleViewModel sale)
    {
        return new TransactionView
        {
            Id = sale.Id,
            TotalDue = sale.TotalPaid,
            Date = sale.Date,
            Partner = sale.Customer,
            PartnerId = sale.CustomerId,
            Type = TransactionType.Sale
        };
    }
}
