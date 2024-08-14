using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.Services;
using WMS.WebUI.ViewModels.Dashboard;

namespace WMS.WebUI.Stores;

public class DashboardStore : IDashboardStore
{
    private readonly ApiClient _client;

    public DashboardStore(ApiClient client)
    {
        _client = client;
    }

    public async Task<DashboardViewModel> Get()
    {
        var response = await _client.GetAsync<DashboardViewModel>("dashboard");

        return response;
    }
}
