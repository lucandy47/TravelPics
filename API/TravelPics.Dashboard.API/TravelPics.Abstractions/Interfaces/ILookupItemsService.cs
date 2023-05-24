using TravelPics.Abstractions.DTOs.LookupItems;

namespace TravelPics.Abstractions.Interfaces
{
    public interface ILookupItemsService
    {
        Task<List<LookupItemModel>> FindLookupItemsAsync(string searchKeyword); 
    }
}
