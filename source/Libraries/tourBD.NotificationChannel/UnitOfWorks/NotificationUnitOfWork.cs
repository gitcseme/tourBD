using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.NotificationChannel.Contexts;
using tourBD.NotificationChannel.Repositories;

namespace tourBD.NotificationChannel.UnitOfWorks
{
    public class NotificationUnitOfWork : UnitOfWorkBase<NotificationContext>, INotificationUnitOfWork
    {
        public NotificationUnitOfWork(string connectionString, string migrationAssemblyName) 
            : base(connectionString, migrationAssemblyName)
        {
            NotificationRepository = new NotificationRepository(_dbContext);
        }

        public INotificationRepository NotificationRepository { get; private set; }
    }
}
