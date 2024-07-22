using Newtonsoft.Json;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.PartnerViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WMS.WebUI.Stores;

public class PartnerStore : IPartnerStore
{
    private readonly HttpClient _client;
    public PartnerStore(IConfiguration configuration)
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri(configuration["WMSApiUrl"]);
    }
    public async Task<List<PartnerViewModel>> GetPartnersAsync(string? search,string? type)
    {
        var customersTask = _client.GetFromJsonAsync<List<PartnerViewModel>>($"customers?search={search}");
        var suppliersTask = _client.GetFromJsonAsync<List<PartnerViewModel>>($"suppliers?search={search}");
         
        await Task.WhenAll(customersTask, suppliersTask);

        customersTask.Result!.ForEach(el => el.Type = PartnerType.Customer);
        suppliersTask.Result!.ForEach(el => el.Type = PartnerType.Supplier);

        return [.. customersTask.Result, .. suppliersTask.Result];
    }
    public async Task<PartnerViewModel> GetByIdAndTypeAsync(int id, PartnerType type)
    {
        PartnerViewModel partner;

        if (type == PartnerType.Customer)
        {
            var customer = await _client.GetFromJsonAsync<CustomerViewModel>($"customers/{id}");
            partner = new PartnerViewModel
            {
                Id = id,
                FullName = customer.FullName,
                Balance  = customer.Balance,
                PhoneNumber = customer.PhoneNumber,
                Type = PartnerType.Customer,
            };
        }
        else
        {
            var supplier = await _client.GetFromJsonAsync<SupplierViewModel>($"suppliers/{id}");
            partner = new PartnerViewModel
            {
                Id = id,
                FullName = supplier.FullName,
                Balance  = supplier.Balance,
                PhoneNumber = supplier.PhoneNumber,
                Type = PartnerType.Supplier,
            };
        }

        return partner;
    }
    public async Task<PartnerViewModel> Create(CreatePartnerViewModel partner)
    {
        HttpResponseMessage result;
        var endpoint = partner.Type == PartnerType.Customer
            ? "customers"
            : "suppliers";
        var data = new
        {
            FirstName = partner.FirstName,
            LastName = partner.LastName,
            Balance = partner.Balance,
            PhoneNumber = partner.PhoneNumber,
        };

        result = await _client.PostAsJsonAsync(endpoint, data);
        result.EnsureSuccessStatusCode();

        var json = await result.Content.ReadAsStringAsync();
        var createdPartner = JsonConvert.DeserializeObject<PartnerViewModel>(json);

        if (createdPartner is null)
        {
            throw new InvalidCastException();
        }

        createdPartner.Type = partner.Type;
        return createdPartner;
    }
    public async Task UpdateAsync(EditPartnerViewModel partner)
    {
        HttpResponseMessage result;
        var endpoint = partner.Type == PartnerType.Customer
            ? "customers"
            : "suppliers";

        var data = new
        {
            Id = partner.Id,
            FirstName = partner.FirstName,
            LastName = partner.LastName,
            Balance = partner.Balance,
            PhoneNumber = partner.PhoneNumber,
        };

        result = await _client.PostAsJsonAsync(endpoint + $"/{partner.Id}", data);
        result.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id, PartnerType type)
    {
        HttpResponseMessage result;
        var endpoint = type == PartnerType.Customer
            ? "customers"
            : "suppliers";

        result = await _client.DeleteAsync(endpoint + $"/{id}");
        result.EnsureSuccessStatusCode();
    }
}
