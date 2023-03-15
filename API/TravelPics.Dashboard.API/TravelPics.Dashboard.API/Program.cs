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

                        var appConfigurationConnectionString = "Endpoint=https://travelpicsappconfiguration.azconfig.io;Id=/9U6-l0-s0:oHtG9HIFpsW3rua+iYCD;Secret=KcpyeMOfEhyp1/Uds6MXxzBvWl3Yj2SWPTtzvw7LRw8=";
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