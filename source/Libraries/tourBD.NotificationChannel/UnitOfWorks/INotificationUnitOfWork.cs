using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.NotificationChannel.Repositories;

namespace tourBD.NotificationChannel.UnitOfWorks
{
    public interface INotificationUnitOfWork : IUnitOfWorkBase
    {
        INotificationRepository NotificationRepository { get; }
    }
}
