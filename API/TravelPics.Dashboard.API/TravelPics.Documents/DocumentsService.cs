using Azure;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Polly;
using TravelPics.Documents.Abstraction;
using TravelPics.Documents.Abstraction.DTO;
using TravelPics.Documents.Configs;
using TravelPics.Documents.Repositories;

namespace TravelPics.Documents
{
    public class DocumentsService: IDocumentsService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerConfig _blobContainerConfig;
        private readonly IDocumentsRepository _documentRepository;
        private readonly IAsyncPolicy _blobRetryPolicy;

        public DocumentsService(IOptions<BlobContainerConfig> blobContainerConfig, 
            IDocumentsRepository documentRepository)
        {
            _blobContainerConfig = blobContainerConfig.Value;
            _blobServiceClient = new BlobServiceClient(_blobContainerConfig.StorageConnectionString);
            _documentRepository = documentRepository;
            _blobRetryPolicy = Policy.Handle<RequestFailedException>().WaitAndRetryAsync(new[]
                {TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10) });
        }

        public async Task Delete(long documentIdId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DocumentDTO?> GetDocumentDTO(long documentId)
        {
            throw new NotImplementedException();
        }

        public async Task<long> Save(DocumentDTO document, DocumentBlobContainerDTO container, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<long> Save(DocumentDTO document, DocumentBlobContainerDTO container, string containerBlobPath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}