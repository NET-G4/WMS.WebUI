using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface IDashboardStore
{
    Task<DashboardViewModel> Get();
}
