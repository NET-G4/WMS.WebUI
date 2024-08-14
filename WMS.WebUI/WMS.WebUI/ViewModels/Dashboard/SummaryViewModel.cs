using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WMS.WebUI.ViewModels.Dashboard;

public class SummaryViewModel
{
    [DisplayFormat(DataFormatString = "{0:C0}")]
    public decimal Revenue { get; set; }
    public int LowQuantityProducts { get; set; }
    public int CustomersAmount { get; set; }

    public string FormattedRevenue
    {
        get
        {
            if (Revenue > 999999999 || Revenue < -999999999)
            {
                return Revenue.ToString("0,,,.###B", CultureInfo.InvariantCulture);
            }
            else if (Revenue > 999999 || Revenue < -999999)
            {
                return Revenue.ToString("0,,.##M", CultureInfo.InvariantCulture);
            }
            else if (Revenue > 999 || Revenue < -999)
            {
                return Revenue.ToString("0,.#K", CultureInfo.InvariantCulture);
            }
            else
            {
                return Revenue.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
