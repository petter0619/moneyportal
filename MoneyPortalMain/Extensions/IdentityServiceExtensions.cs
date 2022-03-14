using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace MoneyPortalMain.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddIdentityCore<AppUser>(opt =>
                {
                    // Allows you to alter how difficult the password must be
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();

            return services;
        }

    }
}
