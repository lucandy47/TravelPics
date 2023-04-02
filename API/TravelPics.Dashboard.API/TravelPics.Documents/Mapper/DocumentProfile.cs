using AutoMapper;
using TravelPics.Documents.Abstraction.DTO;
using TravelPics.Domains.Entities;

namespace TravelPics.Documents.Mapper
{
    public class DocumentProfile: Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentDTO>();
        }
    }
}
