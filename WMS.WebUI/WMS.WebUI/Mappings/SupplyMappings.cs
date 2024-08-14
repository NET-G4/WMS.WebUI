using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.Transaction;

namespace WMS.WebUI.Mappings;

public static class SupplyMappings
{
    public static TransactionViewModel ToTransaction(this SupplyViewModel supply)
    {
        return new TransactionViewModel
        {
            Id = supply.Id,
            TotalDue = supply.TotalPaid,
            Date = supply.Date,
            Partner = supply.Supplier,
            PartnerId = supply.SupplierId,
            Type = TransactionType.Supply
        };
    }
}
