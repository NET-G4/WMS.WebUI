using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using WMS.WebUI.Constants;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;
using WMS.WebUI.Services;

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
