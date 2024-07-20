using Syncfusion.EJ2.Calendars;

namespace WMS.WebUI.QueryParams;

public class TransactionQueryParameters : BaseQueryParameters
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
