using System.Collections.Generic;
using CityWasteManagement.Models;
using Startup.Models;

namespace CityWasteManagement.Services
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetAll(int pageNumber, int pageSize);
        Notification GetById(int id);
        Notification Add(Notification notification);
        void Update(Notification notification);
        void Delete(int id);
    }
}

