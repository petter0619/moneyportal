using MoneyPortalMain.DTOs;
using System.Security.Claims;

namespace MoneyPortalMain.Services
{
    public interface IUserService
    {
        public Task<int> AddNewUser(RegisterDto registrationInfo);
        public Task RequestUserPasswordChange(string userId);
        public UserInfoDto GetUserDtoFromHttpContext();
        public string GetDisplayName();
        public string GetDisplayImage();
        public string GetAuthId();
        public bool IsAuthenticated();
        public IEnumerable<Claim> GetUserClaims();
    }
}
