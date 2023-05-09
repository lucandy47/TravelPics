namespace TravelPics.MessageClient.Configs
{
    public class ConsumerConfiguration
    {
        public string BootstrapServers { get; set; }
        public string Password { get; set; }
        public string ResourceGroupId { get; set; }
        public int? HeartbeatInterval { get; set; }
        public int? SessionTimeOut { get; set; }
        public int? MaxPollInterval { get; set; }

    }
}
