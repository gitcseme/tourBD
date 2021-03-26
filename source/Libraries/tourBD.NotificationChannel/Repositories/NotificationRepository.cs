using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<Notification>> GetUserNotificationsAsync(Guid userId)
        {
            return await _DbSet.Where(n => n.ReceiverId == userId).Take(5).ToListAsync();
        }
    }
}
