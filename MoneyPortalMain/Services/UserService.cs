using DataAccess.Repositories;
using MoneyPortalMain.DTOs;
using MoneyPortalMain.Models;
using System.Net.Http.Headers;

namespace MoneyPortalMain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;

        public UserService(IUserRepository userRepository, IConfiguration config, IHttpClientFactory clientFactory)
        {
            _userRepository = userRepository;
            _config = config;
            _clientFactory = clientFactory; 
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
                { "nickname", registrationInfo.Displayname }
            };

            var content = new FormUrlEncodedContent(values);

            var client = _clientFactory.CreateClient("Auth0");
            var response = await client.PostAsync("/dbconnections/signup", content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadFromJsonAsync<Auth0User>();

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
            var tokenResponseBody = await tokenResponse.Content.ReadFromJsonAsync<Auth0AccessToken>();

            // Get user profile (to get users email)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponseBody.access_token);
            var userResponse = await client.GetAsync("/api/v2/users/" + userId);
            userResponse.EnsureSuccessStatusCode();
            var userResponseBody = await userResponse.Content.ReadFromJsonAsync<Auth0User>();

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
    }
}
