namespace TravelPics.Domains.Entities
{
    public class Document
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public long UploadedById { get; set; }
        public virtual DocumentExtension DocumentExtension { get; set; }
        public string BlobFileName { get; set; }
        public virtual DocumentBlobContainer DocumentBlobContainer { get; set; }
        public string BlobUri { get; set; }
        public int Size { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Post Post { get; set; }
        public bool? IsProfileImage { get; set; }
    }
}
