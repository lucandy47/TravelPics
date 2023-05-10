namespace TravelPics.MessageClient
{
    public interface IConsumer
    {
        public Task ConsumeAsync(IEnumerable<string> topics, CancellationToken cancellationToken, Func<string, string, Task<bool>> ProcessMessage);
    }
}
