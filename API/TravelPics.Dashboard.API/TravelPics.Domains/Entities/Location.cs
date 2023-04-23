namespace TravelPics.Domains.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public Location()
        {
            Posts = new List<Post>();
        }
    }
}
