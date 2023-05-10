using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TravelPics.MessageClient.Configs;

namespace TravelPics.MessageClient.Kafka
{
    public class Producer : IProducer
    {
        private readonly ILogger<Producer> _logger;
        private ProducerConfig _kafkaProducerConfig;
        private readonly ProducerConfiguration _producerConfiguration;

        public Producer(ILogger<Producer> logger, IOptions<ProducerConfiguration> producerConfigurationOptions)
        {
            _logger= logger;
            _producerConfiguration = producerConfigurationOptions.Value;
            _kafkaProducerConfig = KafkaConfiguration.ProducerConfig(_producerConfiguration.BrokerList, _producerConfiguration.Password);
        }
        public async Task ProduceMessage<T>(string topic, T message, int? partition = null)
        {
            using var producer = new ProducerBuilder<Null, string>(_kafkaProducerConfig)
                .SetValueSerializer(Serializers.Utf8)
                .Build();

            var messageSerialized = JsonConvert.SerializeObject(message);

            try
            {
                var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string>
                {
                    Value = messageSerialized
                });

                if (deliveryResult != null)
                {
                    LogDeliveryStatus(deliveryResult);
                }
                else
                {
                    _logger.LogError("Could not send the message!");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Could not send the message!");
            }
            
        }

        private void LogDeliveryStatus(DeliveryResult<Null, String> deliveryResult)
        {
            _logger.LogInformation(
                $@"Message sent:
                    Value: {deliveryResult.Value},
                    Topic: {deliveryResult.Topic},
                    Partition: {deliveryResult?.Partition}
                    .....................................
                    KafkaMessage: {deliveryResult.Message},
                    Status: {deliveryResult.Status}
                ");
        }
    }
}
