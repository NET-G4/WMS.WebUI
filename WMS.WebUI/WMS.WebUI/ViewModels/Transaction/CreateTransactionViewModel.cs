using System.ComponentModel.DataAnnotations;

namespace WMS.WebUI.ViewModels.Transaction;

public class CreateTransactionViewModel
{
    [Required(ErrorMessage = "Transaction Type is required.")]
    public TransactionType Type { get; set; }

    [Required(ErrorMessage = "Partner Id is required.")]
    public int PartnerId { get; set; }

    [Required(ErrorMessage = "Transaction Date is required.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Select at least 1 item.")]
    public List<TransactionItemViewModel> Items { get; set; }
}
