using AutoMapper;
using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Domains.Entities;

namespace TravelPics.Notifications.Core.Mapper.Profiles
{
    public class NotificationsProfile : Profile
    {
        public NotificationsProfile()
        {
            CreateMap<InAppNotification, InAppNotificationDTO>();
            CreateMap<InAppNotificationDTO, InAppNotification>();

            CreateMap<NotificationLog, NotificationLogDTO>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.NotificationType, opt => opt.Ignore());

            CreateMap<NotificationLogDTO, NotificationLog>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.NotificationType, opt => opt.Ignore());
        }
    }
}
