namespace WMS.WebUI.QueryParams
{
    public class ProductQueryParameters
    {
        public string? Search { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? SupplyPrice { get; set; }
        public int? Category { get ; set; }
        public int? LowQuantityAmount { get; set;}
        public int? QuantityInStock { get; set; }
    }
}
