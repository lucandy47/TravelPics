using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TravelPics.MessageClient.Configs;

namespace TravelPics.MessageClient.Kafka
{    
    public class Consumer: IConsumer
    {
        private readonly ILogger<Consumer> _logger;
        private ConsumerConfig _kafkaConsumerConfig;
        private readonly ConsumerConfiguration _consumerConfiguration;

        public Consumer(ILogger<Consumer> logger, IOptions<ConsumerConfiguration> consumerConfigurationOptions)
        {
            _logger = logger;
            _consumerConfiguration = consumerConfigurationOptions.Value;
            _kafkaConsumerConfig = KafkaConfiguration.ConsumerConfig(_consumerConfiguration);
        }

        public async Task ConsumeAsync(IEnumerable<string> topics, CancellationToken cancellationToken, Func<string, string, Task<bool>> ProcessMessage)
        {
            try
            {
                using var consumer = new ConsumerBuilder<Null, string>(_kafkaConsumerConfig).Build();
                consumer.Subscribe(topics);

                _logger.LogInformation("Start consuming messages for topics: {0}", JsonConvert.SerializeObject(topics));

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var responseMessage = consumer.Consume(cancellationToken);
                        _logger.LogInformation($"Received: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");

                        var commitMessage = await ProcessMessage(responseMessage.Message.Value, responseMessage.Topic);
                        if (commitMessage)
                        {
                            consumer.Commit(responseMessage);
                            _logger.LogInformation($"Message commited: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");
                        }
                        else
                        {
                            _logger.LogWarning($"Message uncommited: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Could not consume message, reason: {0}", ex.Message);
                    }
                }
                consumer.Unsubscribe();
                consumer.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {0} in topics", ex.Message);
            }
            finally
            {
                _logger.LogWarning("Stop Consuming messages from topics: {0}", JsonConvert.SerializeObject(topics));
            }
        }
    }
}
