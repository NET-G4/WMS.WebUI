namespace WMS.WebUI.QueryParams;

public class ProductQueryParameters : BaseQueryParameters
{
    public int? CategoryId { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public bool? IsLowQuantity { get; set; }
}
