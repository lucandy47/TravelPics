using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TravelPics.MessageClient.Configs;
using ILogger = Serilog.ILogger;

namespace TravelPics.MessageClient.Kafka
{    
    public class Consumer: IConsumer
    {
        private readonly ILogger _logger;
        private ConsumerConfig _kafkaConsumerConfig;
        private readonly ConsumerConfiguration _consumerConfiguration;

        public Consumer(ILogger logger, IOptions<ConsumerConfiguration> consumerConfigurationOptions)
        {
            _logger = logger;
            _consumerConfiguration = consumerConfigurationOptions.Value;
            _kafkaConsumerConfig = KafkaConfiguration.ConsumerConfig(_consumerConfiguration);
        }

        public async Task ConsumeAsync(IEnumerable<string> topics, CancellationToken cancellationToken, Func<string, string, CancellationToken, Task<bool>> ProcessMessage)
        {
            try
            {
                using var consumer = new ConsumerBuilder<Null, string>(_kafkaConsumerConfig).Build();
                consumer.Subscribe(topics);

                _logger.Information("Start consuming messages for topics: {0}", JsonConvert.SerializeObject(topics));

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var responseMessage = consumer.Consume(cancellationToken);
                        _logger.Information($"Received: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");

                        var commitMessage = await ProcessMessage(responseMessage.Message.Value, responseMessage.Topic, cancellationToken);
                        if (commitMessage)
                        {
                            consumer.Commit(responseMessage);
                            _logger.Information($"Message commited: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");
                        }
                        else
                        {
                            _logger.Warning($"Message uncommited: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Could not consume message, reason: {0}", ex.Message);
                    }
                }
                consumer.Unsubscribe();
                consumer.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error: {0} in topics", ex.Message);
            }
            finally
            {
                _logger.Warning("Stop Consuming messages from topics: {0}", JsonConvert.SerializeObject(topics));
            }
        }

        public async Task ConsumeAsync(IEnumerable<string> topics, CancellationToken cancellationToken, Func<string, string, Task<bool>> ProcessMessage)
        {
            try
            {
                using var consumer = new ConsumerBuilder<Null, string>(_kafkaConsumerConfig).Build();
                consumer.Subscribe(topics);

                _logger.Information("Start consuming messages for topics: {0}", JsonConvert.SerializeObject(topics));

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var responseMessage = consumer.Consume(cancellationToken);
                        _logger.Information($"Received: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");

                        var commitMessage = await ProcessMessage(responseMessage.Message.Value, responseMessage.Topic);
                        if (commitMessage)
                        {
                            consumer.Commit(responseMessage);
                            _logger.Information($"Message commited: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");
                        }
                        else
                        {
                            _logger.Warning($"Message uncommited: {responseMessage.Message.Value}, topic: {responseMessage.Topic}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Could not consume message, reason: {0}", ex.Message);
                    }
                }
                consumer.Unsubscribe();
                consumer.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error: {0} in topics", ex.Message);
            }
            finally
            {
                _logger.Warning("Stop Consuming messages from topics: {0}", JsonConvert.SerializeObject(topics));
            }
        }
    }
}
