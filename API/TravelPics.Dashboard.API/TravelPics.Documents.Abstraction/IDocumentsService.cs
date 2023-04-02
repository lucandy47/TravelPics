using TravelPics.Documents.Abstraction.DTO;

namespace TravelPics.Documents.Abstraction
{
    public interface IDocumentsService
    {
        Task<DocumentDTO?> GetDocumentDTO(long documentId, CancellationToken cancellationToken);
        public Task Save(DocumentDTO document, DocumentBlobContainerDTO container, CancellationToken cancellationToken);
        public Task Save(DocumentDTO document, DocumentBlobContainerDTO container, string containerBlobPath, CancellationToken cancellationToken);
        Task Delete(long documentIdId, CancellationToken cancellationToken);

    }
}