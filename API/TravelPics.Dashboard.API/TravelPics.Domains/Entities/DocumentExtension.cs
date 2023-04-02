namespace TravelPics.Domains.Entities
{
    public class DocumentExtension
    {
        public int Id { get; set; }
        public string Extension { get; set; }
        public string? Description { get; set; }
        public string ContentType { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public bool IsDeleted { get; set; }
        public DocumentExtension()
        {
            Documents = new List<Document>();
        }
    }
}
