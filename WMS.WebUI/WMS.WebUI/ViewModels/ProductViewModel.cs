namespace WMS.WebUI.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal SalePrice { get; set; }
    public decimal SupplyPrice { get; set; }
    public int QuantityInStock { get; set; }
    public int LowQuantityAmount { get; set; }

    public string Category { get; set; }
    public int CategoryId { get; set; }
}
