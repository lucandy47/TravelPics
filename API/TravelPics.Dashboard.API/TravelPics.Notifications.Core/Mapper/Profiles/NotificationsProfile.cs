using AutoMapper;
using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Domains.Entities;

namespace TravelPics.Notifications.Core.Mapper.Profiles
{
    public class NotificationsProfile : Profile
    {
        public NotificationsProfile()
        {
            CreateMap<InAppNotification, InAppNotificationDTO>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
            CreateMap<InAppNotificationDTO, InAppNotification>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());

        }
    }
}
