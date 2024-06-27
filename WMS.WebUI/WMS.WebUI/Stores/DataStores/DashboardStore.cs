using Newtonsoft.Json;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.DataStores;

public class DashboardStore : IDashboardStore
{
    private readonly ApiClient _client;

    public DashboardStore(ApiClient client)
    {
        _client = client;
    }

    public async Task<DashboardViewModel> Get()
    {
        var dashboard = await _client.GetAsync<DashboardViewModel>("dashboard");

        return dashboard;
    }
}
