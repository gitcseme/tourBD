using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.NotificationChannel.Entities;

namespace tourBD.NotificationChannel.Repositories
{
    public interface INotificationRepository : IRepositoryBase<Notification, Guid>
    {
        Task<List<Notification>> GetUserNotificationsAsync(Guid userId);
    }
}
