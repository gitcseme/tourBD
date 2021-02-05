using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.NotificationChannel.Entities;

namespace tourBD.NotificationChannel.Repositories
{
    public interface INotificationRepository : IRepositoryBase<Notification, Guid>
    {
    }
}
