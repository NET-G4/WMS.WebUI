using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Mappings;

public static class SupplyMappings
{
    public static TransactionView ToTransaction(this SupplyViewModel supply)
    {
        return new TransactionView
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
