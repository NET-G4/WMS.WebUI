using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.SaleViewModels;

namespace WMS.WebUI.Mappings;

public static class SalesMappings
{
    public static TransactionView ToTransaction(this SaleViewModel sale)
    {
        return new TransactionView
        {
            Id = sale.Id,
            Amount = sale.TotalPaid,
            Date = sale.Date,
            Partner = sale.Customer,
            PartnerId = sale.CustomerId,
            Type = TransactionType.Sale
        };  
    }
}
