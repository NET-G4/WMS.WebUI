using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.Transaction;

namespace WMS.WebUI.Stores.Interfaces;

public interface ITransactionsStore
{
    Task<List<TransactionViewModel>> GetTransactionsAsync(string? search, string? type);
    Task<List<PartnerViewModel>> GetPartnersAsync();
    Task<TransactionViewModel> GetByIdAndTypeAsync(int id, TransactionType type);
    Task<TransactionViewModel> Create(CreateTransactionViewModel transaction);
}
