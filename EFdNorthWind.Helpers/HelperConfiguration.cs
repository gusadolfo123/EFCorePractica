namespace EFdNorthWind.Helpers
{
    using Microsoft.Extensions.Configuration;

    public static class HelperConfiguration
    {
        public static AppConfiguration GetAppConfiguration(string configurationFile = "App.json")
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                                    .AddJsonFile(configurationFile, optional: true)
                                                    .Build();

            var result = configuration.Get<AppConfiguration>();

            return result;
        }
    }
}
