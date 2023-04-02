using TravelPics.Domains.Entities;

namespace TravelPics.Documents.Repositories
{
    public interface IDocumentsRepository
    {
        Task SaveDocument(Document document);
        
        Task<Document?> GetDocument(long id);

        Task<DocumentBlobContainer?> GetDocumentBlobContainer(int id);

        Task<DocumentExtension?> GetDocumentExtension(string extension);

        Task Delete(long documentId);   
    }
}
