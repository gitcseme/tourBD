using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Services;
using tourBD.NotificationChannel.Services;

namespace tourBD.Web.Models
{
    public static class LayoutBaseModelLoaderHelper
    {
        public static async Task LoadBaseAsync(LayoutBaseModel model, Guid userId, INotificationService _notificationService, IPathService _pathService)
        {
            model.NewNotifications = await _notificationService.GetUnseenNotificationCount(userId);
            model.UserNotifications = (await _notificationService.GetUserNotifications(userId)).Select(n =>
                new NotificationViewModel
                {
                    Name = n.NotifierName,
                    ImageUrl = $"{_pathService.PictureFolder}{n.NotifierImageUrl}",
                    Message = n.Message.Length > 25 ? n.Message.Substring(0, 25) + "..." : n.Message,
                    Time = n.Time.ToShortTimeString() + ", " + n.Time.ToShortDateString(),
                    SourceLink = n.SourceLink,
                    IsSeen = n.Seen
                }).ToList();
        }
    }
}
