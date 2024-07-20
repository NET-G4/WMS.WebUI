using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Mocks;

public class MockDashboardStore : IDashboardStore
{
    public Task<DashboardViewModel> Get()
    {
        var dashboard = new DashboardViewModel();
        dashboard.Summary = new SummaryViewModel
        {
            Revenue = 150,
            CustomersAmount = 50,
            LowQuantityProducts = 40
        };
        dashboard.SplineCharts = new List<SplineChart>
        {
            new SplineChart
            {
                Month = "January",
                Refunds = 10,
                Income = 50,
                Expense = 45
            },
            new SplineChart
            {
                Month = "February",
                Refunds = 25,
                Income = 44,
                Expense = 45
            },
            new SplineChart
            {
                Month = "March",
                Refunds = 50,
                Income = 350,
                Expense = 245
            },
            new SplineChart
            {
                Month = "April",
                Refunds = 150,
                Income = 550,
                Expense = 445
            },
            new SplineChart
            {
                Month = "May",
                Refunds = 43,
                Income = 66,
                Expense = 12
            },
            new SplineChart
            {
                Month = "Jun",
                Refunds = 80,
                Income = 85,
                Expense = 46
            },
            new SplineChart
            {
                Month = "Jul",
                Refunds = 22,
                Income = 120,
                Expense = 110
            },
            new SplineChart
            {
                Month = "Aug",
                Refunds = 90,
                Income = 40,
                Expense = 80
            },
            new SplineChart
            {
                Month = "Sep",
                Refunds = 70,
                Income = 150,
                Expense = 45
            },
            new SplineChart
            {
                Month = "Oct",
                Refunds = 210,
                Income = 150,
                Expense = 345
            },
            new SplineChart
            {
                Month = "Nov",
                Refunds = 210,
                Income = 250,
                Expense = 145
            },
            new SplineChart
            {
                Month = "Dec",
                Refunds = 70,
                Income = 95,
                Expense = 65
            },
        };
        dashboard.SalesByCategories = new List<SalesByCategoryViewModel>
        {
            new SalesByCategoryViewModel
            {
                Category = "Drinks",
                SalesCount = 40
            },
            new SalesByCategoryViewModel
            {
                Category = "Sweets",
                SalesCount = 30
            },
            new SalesByCategoryViewModel
            {
                Category = "Fruits",
                SalesCount = 140
            },
            new SalesByCategoryViewModel
            {
                Category = "Sports",
                SalesCount = 350
            },
            new SalesByCategoryViewModel
            {
                Category = "Clothes",
                SalesCount = 200
            },
            new SalesByCategoryViewModel
            {
                Category = "Jewellery",
                SalesCount = 120
            },
            new SalesByCategoryViewModel
            {
                Category = "Household goods",
                SalesCount = 312
            },
            new SalesByCategoryViewModel
            {
                Category = "Appliances",
                SalesCount = 49
            },
            new SalesByCategoryViewModel
            {
                Category = "Furnitures",
                SalesCount = 98
            },
        };
        dashboard.Transactions = new List<TransactionView>
        {
            new TransactionView
            {
                Id = 1,
                TotalDue = 500,
                Date = DateTime.Now,
                Type = TransactionType.Sale
            },
            new TransactionView
            {
                Id = 2,
                TotalDue = 300,
                Date = DateTime.Now,
                Type = TransactionType.Sale
            },
            new TransactionView
            {
                Id = 3,
                TotalDue = 459,
                Date = DateTime.Now,
                Type = TransactionType.Supply
            },
            new TransactionView
            {
                Id = 4,
                TotalDue = 500,
                Date = DateTime.Now,
                Type = TransactionType.Supply
            },
            new TransactionView
            {
                Id = 5,
                TotalDue = 250,
                Date = DateTime.Now,
                Type = TransactionType.Refund
            },
            new TransactionView
            {
                Id = 7,
                TotalDue = 200,
                Date = DateTime.Now,
                Type = TransactionType.Sale
            },
        };

        return Task.FromResult(dashboard);
    }
}
