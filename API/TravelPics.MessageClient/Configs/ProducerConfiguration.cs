using System.ComponentModel.DataAnnotations;

namespace TravelPics.MessageClient.Configs
{
    public class ProducerConfiguration
    {
        [Required]
        public string BrokerList { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
