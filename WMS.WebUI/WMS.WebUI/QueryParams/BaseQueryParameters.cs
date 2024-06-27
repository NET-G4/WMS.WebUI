namespace WMS.WebUI.QueryParams
{
    public abstract class BaseQueryParameters
    {
        public string? Search { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 15;
    }
}
