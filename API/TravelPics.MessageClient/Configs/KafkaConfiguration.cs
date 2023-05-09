using Confluent.Kafka;

namespace TravelPics.MessageClient.Configs
{
    internal static class KafkaConfiguration
    {
        internal static ProducerConfig ProducerConfig(string brokerList, string password) => new ProducerConfig
        {
            BootstrapServers = brokerList,
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Plain,
            SaslUsername = "$ConnectionString",
            SaslPassword = password,
            Partitioner = Partitioner.Murmur2Random,
            StickyPartitioningLingerMs = 0,
            EnableIdempotence = true,
            LingerMs = 0,
        };

        internal static ConsumerConfig ConsumerConfig(ConsumerConfiguration configuration)
        {
            return new ConsumerConfig
            {
                BootstrapServers = configuration.BootstrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SocketTimeoutMs = 60000,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = configuration.Password,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = configuration.ResourceGroupId,
                IsolationLevel = IsolationLevel.ReadUncommitted,
                EnableAutoCommit = false,
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin,
                BrokerVersionFallback = "1.0.0"
            };
        }
    }
}
