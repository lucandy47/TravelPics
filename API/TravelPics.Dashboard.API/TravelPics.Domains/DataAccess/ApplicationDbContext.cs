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
    }
}
