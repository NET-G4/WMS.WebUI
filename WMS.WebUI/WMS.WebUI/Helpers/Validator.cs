using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Helpers;

public static class Validator
{
    public static T NotNull<T>(T value) where T : class
    {
        ArgumentNullException.ThrowIfNull(value);

        return value;
    }
}
