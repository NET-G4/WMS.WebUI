using Newtonsoft.Json;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores;

public class DashboardStore : IDashboardStore
{
    private readonly HttpClient _client;

    public DashboardStore()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:7097/api/");
    }

    public async Task<DashboardViewModel> Get()
    {
        var response = await _client.GetAsync("dashboard");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var dashboard = JsonConvert.DeserializeObject<DashboardViewModel>(json);

        if (dashboard is null)
        {
            throw new InvalidCastException("Could not convert json data to Dashboard format");
        }

        return dashboard;
    }
}
