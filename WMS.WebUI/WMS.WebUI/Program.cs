using WMS.WebUI.Services;
using WMS.WebUI.Stores.DataStores;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.Stores.Mocks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDashboardStore, DashboardStore>();
builder.Services.AddScoped<ICategoryStore, CategoryStore>();
builder.Services.AddScoped<IProductsStore, ProductStore>();
builder.Services.AddScoped<ICustomerStore,CustomerStore>();
builder.Services.AddScoped<ISupplierStore,SupplierStore>();
builder.Services.AddScoped<ISaleStore,SaleStore>();
builder.Services.AddScoped<ISupplyStore, SupplyStore>();
builder.Services.AddSingleton<ApiClient>();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetValue<string>("Keys:Syncfusion")); 

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
    pattern: "{controller=Categories}/{action=Index}/{id?}");

app.Run();
