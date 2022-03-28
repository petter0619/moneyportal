using DataAccess.Repositories;
using MoneyPortalMain.DTOs;
using MoneyPortalMain.Models;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MoneyPortalMain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IUserRepository userRepository, 
            IConfiguration config, 
            IHttpClientFactory clientFactory,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userRepository = userRepository;
            _config = config;
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddNewUser(RegisterDto registrationInfo)
        {
            // Add user to Auth0
            var values = new Dictionary<string, string>
            {
                { "client_id", _config.GetValue<string>("Auth0:ClientId") },
                { "connection", "Username-Password-Authentication" },

                { "email", registrationInfo.Email },
                { "password", registrationInfo.Password },
                { "given_name", registrationInfo.FirstName },
                { "family_name", registrationInfo.LastName },
                { "name", $"{registrationInfo.FirstName} {registrationInfo.LastName}" },
                { "nickname", registrationInfo.DisplayName }
            };

            var content = new FormUrlEncodedContent(values);

            var client = _clientFactory.CreateClient("Auth0");
            var response = await client.PostAsync("/dbconnections/signup", content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadFromJsonAsync<Auth0UserDto>();

            // Add user to database
            var createdUsers = _userRepository.AddNewUser(responseBody._id);

            return createdUsers;
        }

        public async Task RequestUserPasswordChange(string userId)
        {
            var client = _clientFactory.CreateClient("Auth0");

            // Get access token
            var tokenValues = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "audience", "https://" + _config.GetValue<string>("Auth0:Domain") + "/api/v2/" },
                { "client_id", _config.GetValue<string>("Auth0:ClientId") },
                { "client_secret", _config.GetValue<string>("Auth0:ClientSecret") },
            };

            var tokenContent = new FormUrlEncodedContent(tokenValues);
            var tokenResponse = await client.PostAsync("/oauth/token", tokenContent);
            tokenResponse.EnsureSuccessStatusCode();
            var tokenResponseBody = await tokenResponse.Content.ReadFromJsonAsync<Auth0AccessTokenDto>();

            // Get user profile (to get users email)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponseBody.access_token);
            var userResponse = await client.GetAsync("/api/v2/users/" + userId);
            userResponse.EnsureSuccessStatusCode();
            var userResponseBody = await userResponse.Content.ReadFromJsonAsync<Auth0UserDto>();

            // Change password
            var changePasswordValues = new Dictionary<string, string>
            {
                { "client_id", _config.GetValue<string>("Auth0:ClientId") },
                { "connection", "Username-Password-Authentication" },
                { "email", userResponseBody.email }
            };

            var changePasswordContent = new FormUrlEncodedContent(changePasswordValues);
            var changePasswordResponse = await client.PostAsync("/dbconnections/change_password", changePasswordContent);
            changePasswordResponse.EnsureSuccessStatusCode();
        }

        public UserInfoDto GetUserDtoFromHttpContext()
        {
            if (IsAuthenticated())
            {
                return new UserInfoDto()
                {
                    AuthId = GetUserClaims().FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.Replace("auth0|", ""),
                    DisplayImage = GetUserClaims().FirstOrDefault(c => c.Type == "picture")?.Value,
                    DisplayName = GetUserClaims().FirstOrDefault(c => c.Type == "nickname")?.Value,
                };
            }

            return null;
        }

        public string GetDisplayName()
        {
            if (IsAuthenticated())
            {
                return GetUserClaims()
                    .FirstOrDefault(c => c.Type == "nickname")?
                    .Value;
            }

            return null;
        }

        public string GetDisplayImage()
        {
            if (IsAuthenticated())
            {
                return GetUserClaims()
                    .FirstOrDefault(c => c.Type == "picture")?
                    .Value;
            }

            return null;
        }
        public string GetAuthId()
        {
            if (IsAuthenticated())
            {
                return GetUserClaims()
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                    .Value
                    .Replace("auth0|", "");
            }

            return null;
        }

        public bool IsAuthenticated()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null) return false;

            var user = _httpContextAccessor.HttpContext?.User;

            if (user != null && user.Identity != null)
            {
                return user.Identity.IsAuthenticated;
            }

            return false;
        }

        public IEnumerable<Claim> GetUserClaims()
        {
            if (IsAuthenticated())
            {
                return _httpContextAccessor
                    .HttpContext?
                    .User
                    .Claims;
            }

            return new List<Claim>();
        }
    }
}
