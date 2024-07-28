using WMS.WebUI.Mappings;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores;

public class TransactionsStore : ITransactionsStore
{
    private readonly ApiClient _client;

    public TransactionsStore(ApiClient client)
    {
        _client = client;
    }

    public async Task<List<TransactionView>> GetTransactionsAsync(string? search, string? type)
    {
        var salesTask = _client.GetAsync<List<SaleViewModel>>($"sales?search={search}");
        var suppliesTask = _client.GetAsync<List<SupplyViewModel>>($"supplies?search={search}");

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
        var customersTask = _client.GetAsync<List<PartnerViewModel>>("customers");
        var suppliersTask = _client.GetAsync<List<PartnerViewModel>>("suppliers");

        await Task.WhenAll(customersTask, suppliersTask);

        customersTask.Result!.ForEach(el => el.Type = PartnerType.Customer);
        suppliersTask.Result!.ForEach(el => el.Type = PartnerType.Supplier);

        return [.. customersTask.Result, .. suppliersTask.Result];
    }

    public async Task<TransactionView> GetByIdAndTypeAsync(int id, TransactionType type)
    {
        TransactionView transaction;

        if (type == TransactionType.Sale)
        {
            var sale = await _client.GetAsync<SaleViewModel>($"sales/{id}");
            transaction = new TransactionView
            {
                Id = sale.Id,
                Date = sale.Date,
                PartnerId = sale.CustomerId,
                Partner = sale.Customer,
                Items = sale.SaleItems,
                TotalDue = sale.TotalDue,
            };
        }
        else
        {
            var supply = await _client.GetAsync<SupplyViewModel>($"supplies/{id}");
            transaction = new TransactionView
            {
                Id = supply.Id,
                Date = supply.Date,
                PartnerId = supply.SupplierId,
                Partner = supply.Supplier,
                Items = supply.SupplyItems,
                TotalDue = supply.TotalDue,
            };
        }

        return transaction;
    }
                
    public async Task<TransactionView> Create(CreateTransactionViewModel transaction)
    {
        var endpoint = transaction.Type == TransactionType.Sale
            ? "sales"
            : "supplies";
        object data;

        if (transaction.Type == TransactionType.Sale)
        {
            data = new
            {
                CustomerId = transaction.PartnerId,
                Date = transaction.Date,
                SaleItems = transaction.Items
            };              
        }
        else
        {
            data = new
            {
                SupplierId = transaction.PartnerId,
                Date = transaction.Date,
                SupplyItems = transaction.Items
            };
        }

        var result = await _client.PostAsync<TransactionView, object>(endpoint, data);

        result.Type = transaction.Type;
        return result;
    }
}