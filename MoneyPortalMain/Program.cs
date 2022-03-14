using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using MoneyPortalMain.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlite(
        builder.Configuration.GetConnectionString("DbConnectionString")
    );
});

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use((context, next) =>
{
    var token = context.Request.Cookies.FirstOrDefault(x => x.Key == "jwttoken");

    if (token.Value != null) context.Request.Headers.Add("Authorization", "Bearer " + token.Value);

    return next.Invoke();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var context = services.GetRequiredService<DataContext>();
        context.Database.Migrate();
        await Seed.SeedData(context, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration");
    }
};

app.Run();
