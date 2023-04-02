using Microsoft.EntityFrameworkCore;
using TravelPics.Domains.DataAccess;
using TravelPics.Domains.Entities;

namespace TravelPics.Documents.Repositories
{
    public class DocumentsRepository: IDocumentsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DocumentsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(long documentId)
        {
            var documentToDelete = await _dbContext.Documents.FindAsync(documentId);

            if (documentToDelete != null)
            {
                documentToDelete.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Document?> GetDocument(long id)
        {
            var document = await _dbContext.Documents
                        .Include(x => x.DocumentExtension)
                        .Include(x => x.DocumentBlobContainer)
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();

            return document;
        }

        public async Task<DocumentBlobContainer?> GetDocumentBlobContainer(int id)
        {

            var docBlobContainer = await _dbContext.DocumentBlobContainers.FindAsync(id);

            return docBlobContainer;
        }

        public async Task<DocumentExtension?> GetDocumentExtension(string extension)
        {
            var docExtension = await _dbContext.DocumentExtensions.FirstOrDefaultAsync(x => x.Extension == extension);

            return docExtension;
        }

        public async Task SaveDocument(Document document)
        {
            document.IsDeleted = false;

            await _dbContext.Documents.AddAsync(document);

            await _dbContext.SaveChangesAsync();
        }
    }
}
