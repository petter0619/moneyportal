using DataAccess.Repositories;

namespace MoneyPortalMain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AddNewUser(string authId)
        {
            var createdUsers = _userRepository.AddNewUser(authId);
            return createdUsers;
        }
    }
}
