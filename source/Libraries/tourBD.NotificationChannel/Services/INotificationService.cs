﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.NotificationChannel.Entities;

namespace tourBD.NotificationChannel.Services
{
    public interface INotificationService : IService<Notification>
    {
        Task<List<Notification>> GetUserNotifications(Guid userId);
        Task CreatePostNotificationAsync(Guid notifierId, string notifierName, string notifierImageUrl, string message, Guid receiverId);
    }
}
