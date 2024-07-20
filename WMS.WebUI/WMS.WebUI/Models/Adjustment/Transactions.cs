using WMS.WebUI.ViewModels.SaleViewModels;
using WMS.WebUI.ViewModels.SupplyViewModels;

namespace WMS.WebUI.Models.Adjustment;

public class Transactions
{
    public IEnumerable<SaleViewModel> Sales { get; set; }
    public IEnumerable<SupplyViewModel> Supplies { get; set; }
}
