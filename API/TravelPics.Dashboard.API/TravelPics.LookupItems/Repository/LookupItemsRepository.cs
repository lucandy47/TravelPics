using Microsoft.EntityFrameworkCore;
using TravelPics.Abstractions.DTOs.LookupItems;
using TravelPics.Domains.DataAccess;

namespace TravelPics.LookupItems.Repository
{
    public class LookupItemsRepository : ILookupItemsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LookupItemsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LookupItemModel>> FindLookupItems(string searchKeyword)
        {
            var lookupItems = new List<LookupItemModel>();
            var addedLocationNames = new HashSet<string>();

            var users = await _dbContext.Users
                .Where(u => u.FirstName.ToLower().Contains(searchKeyword.ToLower()) ||
                            u.LastName.ToLower().Contains(searchKeyword.ToLower()) ||
                            u.Email.ToLower().Contains(searchKeyword.ToLower()))
                .ToListAsync();

            var locations = await _dbContext.Locations
                .Where(l => l.Address.ToLower().Contains(searchKeyword.ToLower()))
                .ToListAsync();

            if (users.Any())
            {
                lookupItems.AddRange(users.Select(u => new LookupItemModel
                {
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    EntityTypeId = Abstractions.Enums.LookupItemTypeEnum.USER
                }));
            }

            if (locations.Any())
            {
                foreach (var location in locations)
                {
                    if (!addedLocationNames.Contains(location.Address))
                    {
                        lookupItems.Add(new LookupItemModel
                        {
                            Id = location.Id,
                            Name = location.Address ?? "",
                            EntityTypeId = Abstractions.Enums.LookupItemTypeEnum.LOCATION
                        });

                        addedLocationNames.Add(location.Address);
                    }
                }
            }

            return lookupItems;
        }


    }
}
