using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.Transaction;

namespace WMS.WebUI.Mappings;

public static class SalesMappings
{
    public static TransactionViewModel ToTransaction(this SaleViewModel sale)
    {
        return new TransactionViewModel
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
