using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.NotificationChannel.Entities;
using tourBD.NotificationChannel.UnitOfWorks;

namespace tourBD.NotificationChannel.Services
{
    public class NotificationService : INotificationService
    {
        private INotificationUnitOfWork _notificationUnitOfWork { get; }

        public NotificationService(INotificationUnitOfWork notificationUnitOfWork)
        {
            _notificationUnitOfWork = notificationUnitOfWork;
        }

        public async Task CreateAsync(Notification entity)
        {
            await _notificationUnitOfWork.NotificationRepository.AddAsync(entity);
            await _notificationUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Notification entity)
        {
            await _notificationUnitOfWork.NotificationRepository.RemoveAsync(entity);
            await _notificationUnitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _notificationUnitOfWork.Dispose();
        }

        public async Task EditAsync(Notification entity)
        {
            await _notificationUnitOfWork.NotificationRepository.EditAsync(entity);
            await _notificationUnitOfWork.SaveAsync();
        }

        public Notification Get(Guid Id)
        {
            return _notificationUnitOfWork.NotificationRepository.Get(Id);
        }

        public async Task<Notification> GetAsync(Guid Id)
        {
            return await _notificationUnitOfWork.NotificationRepository.GetAsync(Id);
        }
    }
}
