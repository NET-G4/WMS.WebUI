namespace WMS.WebUI.ViewModels.Transaction;

public class TransactionItemViewModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Product { get; set; } = string.Empty;
}
