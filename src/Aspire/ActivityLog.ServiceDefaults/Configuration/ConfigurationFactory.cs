using Microsoft.Extensions.Configuration;

namespace ActivityLog.ServiceDefaults.Configuration;

public static class ConfigurationFactory
{
    public static TConfiguration BindAndGet<TConfiguration>(IConfiguration configuration, string key, Func<TConfiguration> func)
    {
        var option = func();
        
        configuration.Bind(key, option);
        
        return option;
    }
}
