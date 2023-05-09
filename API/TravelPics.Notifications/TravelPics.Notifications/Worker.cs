using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TravelPics.MessageClient;
using TravelPics.MessageClient.Models;
using TravelPics.Notifications.Configs;
using ILogger = Serilog.ILogger;

namespace TravelPics.Notifications
{
    public class Worker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IConsumer _consumer;
        private readonly EventHubConfig _eventHubConfig;
        public Worker(ILogger logger, IConsumer consumer, IOptions<EventHubConfig> eventHubOptions)
        {
            _logger = logger;
            _consumer = consumer;
            _eventHubConfig = eventHubOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.Information($"Worker running at: { DateTimeOffset.Now}" +
                    $"for topic : {_eventHubConfig.NotificationsTopic}");
                try
                {
                    if (string.IsNullOrWhiteSpace(_eventHubConfig.NotificationsTopic)) throw new ArgumentNullException(nameof(_eventHubConfig.NotificationsTopic), $"{nameof(_eventHubConfig.NotificationsTopic)} cannot be null");
                    await _consumer.ConsumeAsync(new List<string> { _eventHubConfig.NotificationsTopic }, stoppingToken, ProcessMessage);
                }
                catch(Exception ex)
                {
                    _logger.Error(ex, $"Unable to process Notifications");
                }
            }
        }

        private async Task<bool> ProcessMessage(string message, string topic)
        {
            var workflowMessageResult = JsonConvert.DeserializeObject<WorkflowMessage>(message);

            if (workflowMessageResult == null) return true;

            if (!workflowMessageResult.Type.Equals("Notification"))
            {
                return false;
            }

            if(!long.TryParse(workflowMessageResult.PayLoad, out long notificationLogMessageId)) throw new InvalidCastException($"Invalid notification log id -> {workflowMessageResult.PayLoad}");


            //to be continued
            return true;
        }
    }
}