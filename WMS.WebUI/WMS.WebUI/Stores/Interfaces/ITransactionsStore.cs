using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.PartnerViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface ITransactionsStore
{
    Task<List<TransactionView>> GetTransactionsAsync(string? search, string? type);
    Task<TransactionView> GetByIdAndTypeAsync(int id, TransactionType type);
    Task<TransactionView> CreateAsync(CreateTransactionViewModel transaction);
    Task UpdateAsync(TransactionView transaction);
    Task DeleteAsync(int id,TransactionType type);
}
