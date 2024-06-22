namespace WMS.WebUI.QueryParams;

public class ProductQueryParameters
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; } = 10;
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public bool? LowQuantityInStock { get; set; }
}
