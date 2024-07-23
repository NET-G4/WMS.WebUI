using Newtonsoft.Json;
using Syncfusion.EJ2.Diagrams;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Transactions;
using WMS.WebUI.Mappings;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.PartnerViewModels;

namespace WMS.WebUI.Stores;

public class TransactionsStore : ITransactionsStore
{
    private readonly HttpClient _client;

    public TransactionsStore(IConfiguration configuration)
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri(configuration["WMSApiUrl"]);
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
                
    public async Task<TransactionView> CreateAsync(CreateTransactionViewModel transaction)
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
    public async Task UpdateAsync(TransactionView transaction)
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

        result = await _client.PutAsJsonAsync(endpoint + $"/{transaction.Id}", data);
        result.EnsureSuccessStatusCode();
    }
    public async Task DeleteAsync(int id,TransactionType type)
    {
        HttpResponseMessage result;
        var endpoint = type == TransactionType.Sale
            ? "sales"
            : "supplies";

        result = await _client.DeleteAsync(endpoint + $"/{id}");
        result.EnsureSuccessStatusCode();
    }
}