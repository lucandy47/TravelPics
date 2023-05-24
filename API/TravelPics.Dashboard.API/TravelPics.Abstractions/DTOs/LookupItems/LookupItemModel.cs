using TravelPics.Abstractions.Enums;

namespace TravelPics.Abstractions.DTOs.LookupItems
{
    public class LookupItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LookupItemTypeEnum EntityTypeId { get; set; }
    }
}
