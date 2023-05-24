using TravelPics.Abstractions.DTOs.LookupItems;

namespace TravelPics.LookupItems.Repository
{
    public interface ILookupItemsRepository
    {
        Task<List<LookupItemModel>> FindLookupItems(string searchKeyword);
    }
}
