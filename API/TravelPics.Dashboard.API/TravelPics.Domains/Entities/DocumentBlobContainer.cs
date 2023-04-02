namespace TravelPics.Domains.Entities
{
    public class DocumentBlobContainer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ContainerName { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DocumentBlobContainer()
        {
            Documents = new List<Document>();
        }
    }
}
