using WMS.WebUI.Extensions;
using WMS.WebUI.Filters;
using WMS.WebUI.Stores;
using WMS.WebUI.Stores.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => 
    options.Filters.Add(new ExceptionFilter()));
builder.Services.AddScoped<IDashboardStore, DashboardStore>();
builder.Services.AddScoped<ICategoryStore, CategoryStore>();
builder.Services.AddScoped<IProductsStore, ProductStore>();
builder.Services.AddScoped<ITransactionsStore, TransactionsStore>();
builder.Services.AddSyncfusion(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
