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
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<InAppNotification> InAppNotifications { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
        public DbSet<NotificationStatus> NotificationStatuses { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.User);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.User);

            modelBuilder.Entity<User>()
                .HasMany(x => x.NotificationLogs)
                .WithOne(x => x.Receiver);

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

            modelBuilder.Entity<Post>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Photos)
                .WithOne(x => x.Post);

            modelBuilder.Entity<Post>()
                .HasOne(x => x.Location)
                .WithMany(x => x.Posts);

            modelBuilder.Entity<Post>()
                .HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .HasForeignKey(e => e.CreatedById);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.Post);

            modelBuilder.Entity<Location>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Location>()
                .Property(l => l.Latitude)
                .HasPrecision(12, 6);

            modelBuilder.Entity<Location>()
                .Property(l => l.Longitude)
                .HasPrecision(12, 6);

            modelBuilder.Entity<Like>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Like>()
                .Property(l => l.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<NotificationLog>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<NotificationLog>()
                .HasOne(n => n.NotificationType)
                .WithMany(n => n.NotificationLogs);

            modelBuilder.Entity<NotificationLog>()
                .HasOne(n => n.Status)
                .WithMany(n => n.NotificationLogs);

            modelBuilder.Entity<InAppNotification>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<InAppNotification>()
                .HasOne(ian => ian.NotificationLog)
                .WithMany(n => n.InAppNotifications);

            modelBuilder.Entity<NotificationStatus>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<NotificationType>()
                .HasKey(s => s.Id);

        }
    }
}
