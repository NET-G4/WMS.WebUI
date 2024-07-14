using WMS.WebUI.Mappings;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores;

public class TransactionsStore : ITransactionsStore
{
    private readonly HttpClient _client;

    public TransactionsStore()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:44389/api/");
    }

    public async Task<List<TransactionView>> GetTransactionsAsync(string? search, string? type)
    {
        var salesTask = _client.GetFromJsonAsync<List<SaleViewModel>>($"sales?search={search}");
        var suppliesTask = _client.GetFromJsonAsync<List<SupplyViewModel>>($"supply?search={search}");
        
        await Task.WhenAll(salesTask, suppliesTask);

        var sales = salesTask.Result;
        var supplies = suppliesTask.Result;
        List<TransactionView> transactions = [];

        sales?.ForEach(sale => transactions.Add(sale.ToTransaction()));
        supplies?.ForEach(supply => transactions.Add(supply.ToTransaction()));

        return transactions;
    }

    public async Task<List<PartnerViewModel>> GetPartnersAsync()
    {
        var customersTask = _client.GetFromJsonAsync<List<PartnerViewModel>>("customers");
        var suppliersTask = _client.GetFromJsonAsync<List<PartnerViewModel>>("suppliers");

        await Task.WhenAll(customersTask, suppliersTask);

        customersTask.Result!.ForEach(el => el.Type = PartnerType.Customer);
        suppliersTask.Result!.ForEach(el => el.Type = PartnerType.Supplier);

        return [.. customersTask.Result, .. suppliersTask.Result];
    }

    public async Task<TransactionView> Create(CreateTransactionViewModel transaction)
    {
        var transactionNew = new TransactionView();

        return transactionNew;
    }
}
