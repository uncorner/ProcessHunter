using Microsoft.Extensions.Configuration;

namespace ProcessHunter;

internal static class ConfigurationExtensions
{
    public static string GetMainProcessName(this IConfiguration config)
    {
        return config["MainProcessName"]!;
    }
    
    public static string GetUnwantedProcessName(this IConfiguration config)
    {
        return config["UnwantedProcessName"]!;
    }

    public static int GetCheckingTimeout(this IConfiguration config)
    {
        return config.GetValue<int>("CheckingTimeout");
    }
    
}
