using System.ComponentModel;

namespace WMS.WebUI.ViewModels.SaleItemViewModels;

public class SaleItemViewModel
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    [DisplayName("Unit price")]
    public decimal UnitPrice { get; set; }
    public int ProductId { get; set; }
    public string Product { get; set; }
    public int SaleId { get; set; }
}
