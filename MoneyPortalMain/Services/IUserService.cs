using MoneyPortalMain.DTOs;

namespace MoneyPortalMain.Services
{
    public interface IUserService
    {
        public Task<int> AddNewUser(RegisterDto registrationInfo);
        public Task RequestUserPasswordChange(string userId);
    }
}
