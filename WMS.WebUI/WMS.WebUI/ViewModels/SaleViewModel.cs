namespace WMS.WebUI.ViewModels;

public class SaleViewModel
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public decimal TotalDue { get; init; }
    public decimal TotalPaid { get; init; }

    public string Customer { get; init; }
    public int CustomerId { get; init; }

    public List<TransactionItem> SaleItems { get; set; }
}