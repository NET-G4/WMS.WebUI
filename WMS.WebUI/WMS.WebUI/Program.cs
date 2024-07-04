using WMS.WebUI.Extensions;
using WMS.WebUI.Stores;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.Stores.Mocks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDashboardStore, DashboardStore>();
builder.Services.AddScoped<ICategoryStore, CategoryStore>();
builder.Services.AddScoped<IProductsStore, ProductStore>();
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
