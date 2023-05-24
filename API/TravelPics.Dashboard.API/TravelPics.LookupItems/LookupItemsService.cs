using TravelPics.Abstractions.DTOs.LookupItems;
using TravelPics.Abstractions.Interfaces;
using TravelPics.LookupItems.Repository;

namespace TravelPics.LookupItems
{
    public class LookupItemsService : ILookupItemsService
    {
        private readonly ILookupItemsRepository _lookupItemsRepository;

        public LookupItemsService(ILookupItemsRepository lookupItemsRepository)
        {
            _lookupItemsRepository = lookupItemsRepository;
        }
        public async Task<List<LookupItemModel>> FindLookupItemsAsync(string searchKeyword)
        {
            return await _lookupItemsRepository.FindLookupItems(searchKeyword);
        }
    }
}