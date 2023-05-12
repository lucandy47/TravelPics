using AutoMapper;
using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Domains.Entities;
using TravelPics.Domains.Enums;

namespace TravelPics.Notifications.Core.Mapper.Profiles
{
    public class NotificationsProfile : Profile
    {
        public NotificationsProfile()
        {
            CreateMap<InAppNotification, InAppNotificationDTO>();
            CreateMap<InAppNotificationDTO, InAppNotification>();

            CreateMap<NotificationLog, NotificationLogDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (NotificationStatusEnum)src.Status.Id))
                .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => (NotificationTypeEnum)src.NotificationType.Id));

            CreateMap<NotificationLogDTO, NotificationLog>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.NotificationType, opt => opt.Ignore());
        }
    }
}
