using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Domains.Entities;

namespace TravelPics.Abstractions.Interfaces
{
    public interface IDocumentsService
    {
        Task<DocumentDTO?> GetDocumentDTO(long documentId, CancellationToken cancellationToken);
        public Task<Document> ComputeDocument(DocumentDTO document, DocumentBlobContainerDTO container, bool isProfileImage, CancellationToken cancellationToken);
        public Task<Document> ComputeDocument(DocumentDTO document, DocumentBlobContainerDTO container, string containerBlobPath, bool isProfileImage, CancellationToken cancellationToken);
        Task Delete(long documentIdId, CancellationToken cancellationToken);
        public Task UploadPhotos(List<Document> photos, DocumentBlobContainerDTO container, CancellationToken cancellationToken);
    }
}
