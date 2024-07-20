using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.SaleViewModels;
using WMS.WebUI.ViewModels.SupplyViewModels;

namespace WMS.WebUI.Mappings;

public static class SupplyMappings
{
    public static TransactionView ToTransaction(this SupplyViewModel supply)
    {
        return new TransactionView
        {
            Id = supply.Id,
            Amount = supply.TotalPaid,
            Date = supply.Date,
            Partner = supply.Supplier,
            PartnerId = supply.SupplierId,
            Type = TransactionType.Supply,
        };
    }
}
