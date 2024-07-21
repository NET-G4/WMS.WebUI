using WMS.WebUI.Mappings;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.SaleViewModels;
using WMS.WebUI.ViewModels.SupplyViewModels;

namespace WMS.WebUI.Stores;

public class TransactionsStore : ITransactionsStore
{
    private readonly HttpClient _client;

    public TransactionsStore()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:7108/api/");
    }

    public async Task<List<TransactionView>> GetTransactionsAsync(string? search, string? type)
    {
        var salesTask = _client.GetFromJsonAsync<PaginatedApiResponse<SaleViewModel>>($"sales?search={search}");
        var suppliesTask = _client.GetFromJsonAsync<PaginatedApiResponse<SupplyViewModel>>($"supplies?search={search}");

        await Task.WhenAll(salesTask, suppliesTask);

        var sales = salesTask.Result;
        var supplies = suppliesTask.Result;
        List<TransactionView> transactions = [];

        sales?.Data.ForEach(sale => transactions.Add(sale.ToTransaction()));
        supplies?.Data.ForEach(supply => transactions.Add(supply.ToTransaction()));

        return transactions;
    }

    public async Task<List<PartnerViewModel>> GetPartnersAsync()
    {
        var customersTask = _client.GetFromJsonAsync<PaginatedApiResponse<PartnerViewModel>>("customers");
        var suppliersTask = _client.GetFromJsonAsync<PaginatedApiResponse<PartnerViewModel>>("suppliers");

        await Task.WhenAll(customersTask, suppliersTask);

        customersTask.Result!.Data.ForEach(el => el.Type = PartnerType.Customer);
        suppliersTask.Result!.Data.ForEach(el => el.Type = PartnerType.Supplier);

        return [.. customersTask.Result.Data, .. suppliersTask.Result.Data];
    }

    public async Task<TransactionView> Create(CreateTransactionViewModel transaction)
    {
        var transactionNew = new TransactionView();

        return transactionNew;
    }
}