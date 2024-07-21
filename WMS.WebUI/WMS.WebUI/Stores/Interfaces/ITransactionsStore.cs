using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.PartnerViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface ITransactionsStore
{
    Task<List<TransactionView>> GetTransactionsAsync(string? search, string? type);
    Task<TransactionView> GetByIdAndTypeAsync(int id, TransactionType type);
    Task<TransactionView> Create(CreateTransactionViewModel transaction);
}
