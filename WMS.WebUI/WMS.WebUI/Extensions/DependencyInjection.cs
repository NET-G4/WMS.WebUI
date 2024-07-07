using Syncfusion.Licensing;

namespace WMS.WebUI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSyncfusion(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Keys");
            var key = section.GetValue<string>("Syncfusion");

            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("Cannot register Syncfusion without key.");
            }

            SyncfusionLicenseProvider.RegisterLicense(key);

            return services;
        }
    }
}
