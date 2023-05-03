using AutoMapper;
using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Domains.Entities;

namespace TravelPics.Documents.Mapper
{
    public class DocumentProfile: Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentDTO>();
            CreateMap<DocumentDTO, Document>();
        }
    }
}
