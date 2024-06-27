namespace WMS.WebUI.QueryParams;

public class CustomerQueryParameters : BaseQueryParameters
{
    public decimal? BalanceGreaterThan { get; set; }
    public decimal? BalanceLessThan { get; set; }
}
