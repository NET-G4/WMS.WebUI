using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WMS.WebUI.Extensions;
using WMS.WebUI.Filters;
using WMS.WebUI.Services;
using WMS.WebUI.Services.Interfaces;
using WMS.WebUI.Stores;
using WMS.WebUI.Stores.Interfaces;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
    options.Filters.Add(new ExceptionFilter()));
builder.Services.AddSingleton<ApiClient>();
builder.Services.AddSingleton<RolesService>();
builder.Services.AddScoped<IDashboardStore, DashboardStore>();
builder.Services.AddScoped<ICategoryStore, CategoryStore>();
builder.Services.AddScoped<IProductsStore, ProductStore>();
builder.Services.AddScoped<ITransactionsStore, TransactionsStore>();
builder.Services.AddScoped<IUserAgentService, UserAgentService>();
builder.Services.AddSyncfusion(builder.Configuration);
builder.Services
    .AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                // Override the default behavior.
                context.HandleResponse();

                // Redirect to login page
                context.Response.Redirect("/Account/Login");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri("https://wms-webui-configurations.vault.azure.net/"),
        new DefaultAzureCredential());

    //builder.Host.UseSerilog((context, configuration) => configuration
    //    .MinimumLevel.Debug()
    //    .WriteTo.ApplicationInsights(
    //        new TelemetryConfiguration
    //        {
    //            InstrumentationKey = "99999930-e6b4-46ed-95cb-73ef51449b94",
    //            ConnectionString = "InstrumentationKey=99999930-e6b4-46ed-95cb-73ef51449b94;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/;ApplicationId=8837aba4-c7d2-4e4b-a012-01a48d5216ba"
    //        },
    //        TelemetryConverter.Traces));
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
