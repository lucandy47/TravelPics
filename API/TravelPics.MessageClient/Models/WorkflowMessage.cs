namespace TravelPics.MessageClient.Models
{
    public class WorkflowMessage
    {
        public Guid CorrelationId { get; set; }
        public string Type { get; set; }
        public string? PayLoad { get; set; }
        public DateTimeOffset GemeratedOn { get; set; }
    }
}
