namespace WMS.WebUI.ViewModels.Transaction;

public class TransactionViewModel
{
    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public decimal TotalDue { get; set; }
    public int PartnerId { get; set; }
    public string Partner { get; set; }
    public DateTime Date { get; set; }

    public List<TransactionItemViewModel> Items { get; set; }
}
