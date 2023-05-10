using TravelPics.Notifications;
using TravelPics.MessageClient.Configs;
using TravelPics.Notifications.Configs;
using TravelPics.MessageClient;
using TravelPics.MessageClient.Kafka;
using TravelPics.Notifications.Processors.Processors;
using TravelPics.Notifications.Core.Repository;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Notifications.Core;
using Microsoft.EntityFrameworkCore;
using TravelPics.Domains.DataAccess;
using Microsoft.AspNetCore.Hosting;
using TravelPics.Notifications.Core.Mapper.Profiles;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((builder) =>
    {

        var appConfigurationConnectionString = "Endpoint=https://travelpicsappconfiguration.azconfig.io;Id=/9U6-l0-s0:oHtG9HIFpsW3rua+iYCD;Secret=KcpyeMOfEhyp1/Uds6MXxzBvWl3Yj2SWPTtzvw7LRw8=";
        builder.AddAzureAppConfiguration(options =>
        {
            options.Connect(appConfigurationConnectionString)
            .ConfigureRefresh((refreshOptions) =>
            {
                refreshOptions.Register(key: "Settings:Sentinel", refreshAll: true);
                refreshOptions.SetCacheExpiration(TimeSpan.FromSeconds(5));
            })
            .UseFeatureFlags();
        }).Build();
    })
    .ConfigureServices((context, services) =>
    {
        var configRoute = context.Configuration;

        var connectionString = configRoute.GetConnectionString("TravelPicsDB");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

        services.AddTransient<IConsumer, Consumer>();

        services.AddTransient<INotificationProcessor, InAppNotificationProcessor>();

        services.AddScoped<INotificationsRepository, NotificationsRepository>();
        services.AddScoped<INotificationsService, NotificationsService>();

        services.AddOptions<BlobContainerConfig>()
        .Configure(options =>
        {
            options.StorageConnectionString = configRoute.GetValue<string>("ConnectionStrings:BlobStorage");
        }).ValidateDataAnnotations();

        services.AddOptions<ConsumerConfiguration>()
            .Configure((options) =>
            {
                options.BootstrapServers = configRoute.GetValue<string>("EventHub:Endpoint");
                options.Password = configRoute.GetValue<string>("EventHub:ConnectionString");
                options.ResourceGroupId = configRoute.GetValue<string>("EventHub:NotificationGroupId");
            }).ValidateDataAnnotations();

        services.AddOptions<EventHubConfig>().Configure(options =>
        {
            configRoute.GetSection(EventHubConfig.SectionName).Bind(options);
        }).ValidateDataAnnotations();

        services.AddAzureAppConfiguration();
        services.AddHostedService<Worker>();

        services.AddAutoMapper(typeof(Worker));
        services.AddAutoMapper(typeof(NotificationsProfile));

    })
    .Build();

await host.RunAsync();

