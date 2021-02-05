using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.NotificationChannel.Entities;

namespace tourBD.NotificationChannel.Services
{
    public interface INotificationService : IService<Notification>
    {
        Task CreatePostNotificationAsync(string postId, Guid userId, string NotifierImageUrl, string Message);
    }
}
