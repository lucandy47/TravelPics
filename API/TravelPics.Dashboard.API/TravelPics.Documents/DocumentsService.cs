using Azure;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Polly;
using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Documents.Configs;
using TravelPics.Documents.Repositories;
using TravelPics.Domains.Entities;

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
        private async Task<BlobContainerClient> GetBlobContainerClient(string containerName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync();
            return blobContainerClient;
        }

        public async Task Delete(long documentIdId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DocumentDTO?> GetDocumentDTO(long documentId, CancellationToken cancellationToken)
        {
            var documentEntity = await _documentRepository.GetDocument(documentId);

            if (documentEntity == null) return null;

            var blobContainerClient = await GetBlobContainerClient(documentEntity.DocumentBlobContainer.ContainerName);
            var blobClient = blobContainerClient.GetBlobClient(documentEntity.BlobUri);

            if (!await blobClient.ExistsAsync(cancellationToken)) throw new Exception("The specified blob file does not exist.");

            var content = await _blobRetryPolicy.ExecuteAsync(async (cancellationToken) =>
            {
                return await blobClient.DownloadContentAsync(cancellationToken);
            }, cancellationToken);

            return new DocumentDTO
            {
                Id = documentEntity.Id,
                Content = content.Value.Content.ToArray(),
                UploadedById = documentEntity.UploadedById,
                FileName = documentEntity.FileName,
                Size = content.Value.Content.ToArray().Length
            };
        }

        public async Task<Document> ComputeDocument(DocumentDTO document, DocumentBlobContainerDTO container, bool isProfileImage, CancellationToken cancellationToken)
        {
            return await ComputeDocument(document, container, "", isProfileImage, cancellationToken);
        }

        public async Task<Document> ComputeDocument(DocumentDTO document, DocumentBlobContainerDTO container, string containerBlobPath, bool isProfileImage, CancellationToken cancellationToken)
        {
            if (document.Content.Length == 0) throw new Exception($"The document content cannot be empty.");

            var docBlobContainer = await _documentRepository.GetDocumentBlobContainer(container.Id);
            if (docBlobContainer == null) throw new Exception($"Unsupported {nameof(DocumentBlobContainer)}: {container}");


            var fileExtension = Path.GetExtension(document.FileName);
            if (string.IsNullOrWhiteSpace(fileExtension)) throw new Exception($"Could not extract file extension from '{document.FileName}'");

            var docExt = await _documentRepository.GetDocumentExtension(fileExtension.Replace(".", ""));
            if (docExt == null) throw new Exception($"Unsupported document extension: '{fileExtension}'");
            var blobFileName = $"{document.FileName.Split(".")[0]}.{docExt.Extension.ToLower()}";

            var docEntity = new Document
            {
                BlobFileName = blobFileName,
                BlobUri = $"{docBlobContainer.ContainerName}/{containerBlobPath}/{blobFileName}",
                Size = document.Content.Length,
                DocumentExtension = docExt,
                DocumentBlobContainer = docBlobContainer,
                Content = document.Content,
                FileName = document.FileName,
                UploadedById = document.UploadedById,
                CreatedOn = DateTimeOffset.Now,
                IsProfileImage = isProfileImage,
            };

            return docEntity;
        }

        public async Task UploadPhotos(List<Document> photos, DocumentBlobContainerDTO container, CancellationToken cancellationToken)
        {
            if(!photos.Any()) throw new Exception($"No documents to be saved.");

            var docBlobContainer = await _documentRepository.GetDocumentBlobContainer(container.Id);
            if (docBlobContainer == null) throw new Exception($"Unsupported {nameof(DocumentBlobContainer)}: {container}");

            var blobContainerClient = await GetBlobContainerClient(docBlobContainer.ContainerName);

            foreach(var photo in photos)
            {
                var blobClient = blobContainerClient.GetBlobClient(photo.BlobUri);
                var saveDocumentTask = await _blobRetryPolicy.ExecuteAsync(async (cancellationToken) =>
                {
                    await blobClient.UploadAsync(new BinaryData(photo.Content), cancellationToken);
                }, cancellationToken)
                .ContinueWith(async (continuation) =>
                {
                    if (continuation.IsFaulted)
                    {
                        if (continuation.Exception != null)
                        {
                            throw new Exception($"Unable to save Document '{photo.BlobFileName}' to Blob Storage {photo.BlobUri}.", continuation.Exception);
                        }
                        throw new Exception($"Unable to save Document '{photo.BlobFileName}' to Blob Storage {photo.BlobUri}.");
                    }
                });
            }

            //try
            //{
            //    await _documentRepository.SaveDocument(docEntity);
            //}
            //catch (Exception ex)
            //{
            //    var deleteBlobResponse = await blobClient.DeleteAsync();
            //    throw new Exception($"Unable to save Document '{blobFileName}' to database.", ex);
            //}
        }
    }
}