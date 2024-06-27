using System.Collections.Generic;
using CityWasteManagement.Models;
using CityWasteManagement.Repositories;
using Startup.Models;

namespace CityWasteManagement.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public IEnumerable<Notification> GetAll(int pageNumber, int pageSize)
        {
            return _notificationRepository.GetAll(pageNumber, pageSize);
        }

        public Notification GetById(int id)
        {
            return _notificationRepository.GetById(id);
        }

        public Notification Add(Notification notification)
        {
            return _notificationRepository.Add(notification);
        }

        public void Update(Notification notification)
        {
            _notificationRepository.Update(notification);
        }

        public void Delete(int id)
        {
            _notificationRepository.Delete(id);
        }
    }
}
