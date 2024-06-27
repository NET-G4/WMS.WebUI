namespace WMS.WebUI.QueryParams;

public class SupplierQueryParameters : BaseQueryParameters
{
    public decimal? BalanceGreaterThan { get; set; }
    public decimal? BalanceLessThan { get; set; }
}
