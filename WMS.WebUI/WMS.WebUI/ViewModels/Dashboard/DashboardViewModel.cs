using WMS.WebUI.ViewModels.Transaction;

namespace WMS.WebUI.ViewModels.Dashboard;

public class DashboardViewModel
{
    public SummaryViewModel Summary { get; set; }
    public List<SalesByCategoryViewModel> SalesByCategories { get; set; }
    public List<SplineChart> SplineCharts { get; set; }
    public List<TransactionViewModel> Transactions { get; set; }
}
