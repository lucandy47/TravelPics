using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Abstractions.Interfaces;
using TravelPics.MessageClient;
using TravelPics.Notifications.Configs;
using TravelPics.Notifications.Processors.Processors;

namespace TravelPics.Notifications
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer _consumer;
        private readonly EventHubConfig _eventHubConfig;
        private readonly IEnumerable<INotificationProcessor> _notificationProcessors;
        private readonly INotificationsService _notificationsService;

        public Worker(ILogger<Worker> logger, 
            IConsumer consumer, 
            IOptions<EventHubConfig> eventHubOptions,
            IEnumerable<INotificationProcessor> notificationProcessors,
            INotificationsService notificationsService)
        {
            _logger = logger;
            _consumer = consumer;
            _eventHubConfig = eventHubOptions.Value;
            _notificationProcessors = notificationProcessors;
            _notificationsService = notificationsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: { DateTimeOffset.Now}" +
                    $"for topic : {_eventHubConfig.NotificationsTopic}");
                try
                {
                    if (string.IsNullOrWhiteSpace(_eventHubConfig.NotificationsTopic)) throw new ArgumentNullException(nameof(_eventHubConfig.NotificationsTopic), $"{nameof(_eventHubConfig.NotificationsTopic)} cannot be null");
                    await _consumer.ConsumeAsync(new List<string> { _eventHubConfig.NotificationsTopic }, stoppingToken, ProcessMessage);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, $"Unable to process Notifications");
                }
            }
        }

        private async Task<bool> ProcessMessage(string message, string topic)
        {
            var notificationLogDTO = JsonConvert.DeserializeObject<NotificationLogDTO>(message);

            if (notificationLogDTO == null) return true;

            if (string.IsNullOrWhiteSpace(notificationLogDTO.Payload)) throw new ArgumentNullException($"Payload cannot be null");

            try
            {
                await _notificationsService.UpdateNotificationStatus(notificationLogDTO.Id, Domains.Enums.NotificationStatusEnum.InProgress);

                var processor = _notificationProcessors.FirstOrDefault(p => p.NotificationTypeName == notificationLogDTO.NotificationType);

                if (processor == null) throw new Exception($"Invalid notification type ID: {(int)notificationLogDTO.NotificationType}.");

                var result = await processor.Run(notificationLogDTO);

                await _notificationsService.UpdateNotificationStatus(notificationLogDTO.Id, Domains.Enums.NotificationStatusEnum.Received);

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to process NotificationLogId: {notificationLogDTO.Id}", ex);
                if (notificationLogDTO != null)
                {
                    await _notificationsService.UpdateNotificationStatus(notificationLogDTO.Id, Domains.Enums.NotificationStatusEnum.Failed);
                }
                return true;
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Notifications Worker has started.");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Notifications Worker has stopped.");
            return base.StopAsync(cancellationToken);
        }
    }
}