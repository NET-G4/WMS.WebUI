using System.ComponentModel;
using WMS.WebUI.ViewModels.SaleItemViewModels;

namespace WMS.WebUI.ViewModels.SaleViewModels;

public class SaleViewModel
{
    public int Id { get; set; }

    [DisplayName("Total due")]
    public decimal TotalDue { get; set; }
    
    [DisplayName("Total paid")]
    public decimal TotalPaid { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public string Customer { get; set; }
    public virtual ICollection<SaleItemViewModel> SaleItems { get; set; }
    public SaleViewModel() => SaleItems = [];
}
