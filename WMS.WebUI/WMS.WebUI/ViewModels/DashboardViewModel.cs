using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WMS.WebUI.ViewModels;

public class DashboardViewModel
{
    public SummaryViewModel Summary { get; set; }
    public List<SalesByCategoryViewModel> SalesByCategories { get; set; }
    public List<SplineChart> SplineCharts { get; set; }
    public List<TransactionView> Transactions { get; set; }
}


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

public class SalesByCategoryViewModel
{
    public string Category { get; set; }
    public int SalesCount { get; set; }
}

public class SplineChart
{
    public string Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }
    public decimal Refunds { get; set; }
}

public class TransactionView
{
    public int Id { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}