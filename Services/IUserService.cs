using CityWasteManagement.Models;
using Startup.Models;

namespace CityWasteManagement.Services
{
    public interface IUserService
    {
        User GetByUserName(string userName);
        User Add(User user);
    }
}
