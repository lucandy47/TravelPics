namespace TravelPics.MessageClient
{
    public interface IProducer
    {
        Task ProduceMessage<T>(string topic, T message, int? partition = null);
    }
}