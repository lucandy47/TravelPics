using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TravelPics.MessageClient.Configs;
using ILogger = Serilog.ILogger;

namespace TravelPics.MessageClient.Kafka
{
    public class Producer : IProducer
    {
        private readonly ILogger _logger;
        private ProducerConfig _kafkaProducerConfig;
        private readonly ProducerConfiguration _producerConfiguration;

        public Producer(ILogger logger, IOptions<ProducerConfiguration> producerConfigurationOptions)
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

            var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = messageSerialized
            });

            if(deliveryResult != null)
            {
                LogDeliveryStatus(deliveryResult);
            }
            else
            {
                _logger.Error("Could not send the message!");
            }
        }

        private void LogDeliveryStatus(DeliveryResult<Null, String> deliveryResult)
        {
            _logger.Information(
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
