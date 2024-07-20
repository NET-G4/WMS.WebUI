using Newtonsoft.Json;
using Syncfusion.EJ2.Diagrams;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
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
        _client.BaseAddress = new Uri("https://localhost:7097/api/");
    }

    public async Task<List<TransactionView>> GetTransactionsAsync(string? search, string? type)
    {
        var salesTask = _client.GetFromJsonAsync<List<SaleViewModel>>($"sales?search={search}");
        var suppliesTask = _client.GetFromJsonAsync<List<SupplyViewModel>>($"supplies?search={search}");

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

    public async Task<TransactionView> GetByIdAndTypeAsync(int id, TransactionType type)
    {
        TransactionView transaction;

        if (type == TransactionType.Sale)
        {
            var sale = await _client.GetFromJsonAsync<SaleViewModel>($"sales/{id}");
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
            var supply = await _client.GetFromJsonAsync<SupplyViewModel>($"supplies/{id}");
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
        HttpResponseMessage result;
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

        result = await _client.PostAsJsonAsync(endpoint, data);
        result.EnsureSuccessStatusCode();

        var json = await result.Content.ReadAsStringAsync();
        var createdTransaction = JsonConvert.DeserializeObject<TransactionView>(json);

        if (createdTransaction is null)
        {
            throw new InvalidCastException();
        }
        
        createdTransaction.Type = transaction.Type;
        return createdTransaction;
    }
}