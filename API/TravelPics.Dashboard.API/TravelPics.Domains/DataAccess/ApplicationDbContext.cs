using Microsoft.EntityFrameworkCore;
using TravelPics.Domains.Entities;

namespace TravelPics.Domains.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentExtension> DocumentExtensions { get; set; }
        public DbSet<DocumentBlobContainer> DocumentBlobContainers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Document>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Document>()
                .HasOne(x => x.DocumentExtension)
                .WithMany(x => x.Documents);

            modelBuilder.Entity<Document>()
                .HasOne(x => x.DocumentBlobContainer)
                .WithMany(x => x.Documents);

            modelBuilder.Entity<DocumentExtension>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<DocumentBlobContainer>()
                .HasKey(x => x.Id);
        }
    }
}
