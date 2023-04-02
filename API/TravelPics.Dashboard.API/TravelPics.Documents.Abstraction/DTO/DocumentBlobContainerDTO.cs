namespace TravelPics.Documents.Abstraction.DTO
{
    public class DocumentBlobContainerDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ContainerName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
