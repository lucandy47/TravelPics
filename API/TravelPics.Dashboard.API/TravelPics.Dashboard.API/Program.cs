namespace TravelPics.Dashboard.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {

                        var appConfigurationConnectionString = "";
                        config
                        .AddAzureAppConfiguration(options =>
                        {
                            options.Connect(appConfigurationConnectionString)
                            .ConfigureRefresh((refreshOptions) =>
                            {
                                refreshOptions.Register(key: "Settings:Sentinel", refreshAll: true);
                                refreshOptions.SetCacheExpiration(TimeSpan.FromSeconds(5));
                            })
                            .UseFeatureFlags();

                        });
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}