namespace TravelPics.Documents.Abstraction.DTO
{
    public class DocumentDTO
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public long UploadedById { get; set; }
        public int Size { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
