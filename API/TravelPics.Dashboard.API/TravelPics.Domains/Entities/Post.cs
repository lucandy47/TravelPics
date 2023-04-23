namespace TravelPics.Domains.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public virtual Location Location { get; set; }
        public string? Description { get; set; }
        public int CreatedById { get; set; }
        public virtual ICollection<Document> Photos { get; set; }
        public virtual User User { get; set; }
        public DateTimeOffset? PublishedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Post()
        {
            Photos = new List<Document>();
        }
    }
}
