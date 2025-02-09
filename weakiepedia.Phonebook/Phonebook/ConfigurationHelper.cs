using Microsoft.Extensions.Configuration;

namespace Phonebook;

public static class ConfigurationHelper
{
    private static IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("config.json", optional: false, reloadOnChange: true)
        .Build();
    
    public static string GetConnectionString()
    {
        return configuration["ConnectionString"];
    }

    public static string GetUserEmail()
    {
        return configuration["UserEmail"];
    }

    public static string GetUserPassword()
    {
        return configuration["UserPassword"];
    }
    
    public static string GetTwilioAccountSid()
    {
        return configuration["TwilioAccountSid"];
    }

    public static string GetTwilioAuthToken()
    {
        return configuration["TwilioAuthToken"];
    }

    public static string GetTwilioPhoneNumber()
    {
        return configuration["TwilioPhoneNumber"];
    }
}