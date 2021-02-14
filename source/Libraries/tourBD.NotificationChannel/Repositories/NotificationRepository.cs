using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.NotificationChannel.Contexts;
using tourBD.NotificationChannel.Entities;

namespace tourBD.NotificationChannel.Repositories
{
    public class NotificationRepository : RepositoryBase<Notification, Guid, NotificationContext>, INotificationRepository
    {
        public NotificationRepository(NotificationContext context) : base(context)
        {
        }
    }
}
