using TravelPics.Documents.Abstraction.DTO;
using TravelPics.Domains.Entities;

namespace TravelPics.Documents.Abstraction
{
    public interface IDocumentsService
    {
        Task<DocumentDTO?> GetDocumentDTO(long documentId, CancellationToken cancellationToken);
        public Task<Document> ComputeDocument(DocumentDTO document, DocumentBlobContainerDTO container, CancellationToken cancellationToken);
        public Task<Document> ComputeDocument(DocumentDTO document, DocumentBlobContainerDTO container, string containerBlobPath, CancellationToken cancellationToken);
        Task Delete(long documentIdId, CancellationToken cancellationToken);
        public Task UploadPhotos(List<Document> photos, DocumentBlobContainerDTO container, CancellationToken cancellationToken);
    }
}