using Syncfusion.Licensing;
using WMS.WebUI.Configurations;

namespace WMS.WebUI.Extensions;

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

        services.AddOptions<ApiConfiguration>()
            .Bind(configuration.GetSection(ApiConfiguration.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        SyncfusionLicenseProvider.RegisterLicense(key);

        return services;
    }
}
