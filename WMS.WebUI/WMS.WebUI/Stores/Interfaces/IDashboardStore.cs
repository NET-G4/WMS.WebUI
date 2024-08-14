using WMS.WebUI.ViewModels.Dashboard;

namespace WMS.WebUI.Stores.Interfaces;

public interface IDashboardStore
{
    Task<DashboardViewModel> Get();
}
