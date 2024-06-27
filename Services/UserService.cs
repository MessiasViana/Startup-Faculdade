using CityWasteManagement.Models;
using CityWasteManagement.Repositories;
using Startup.Models;

namespace CityWasteManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetByUserName(string userName)
        {
            return _userRepository.GetByUserName(userName);
        }

        public User Add(User user)
        {
            return _userRepository.Add(user);
        }
    }
}
