using Auth0.AspNetCore.Authentication;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using MoneyPortalMain.Services;
using DataAccess.Repositories;
using MoneyPortalMain.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth0WebAppAuthentication(options => 
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddHttpClient("Auth0", config => 
{
    config.BaseAddress = new Uri("https://" + builder.Configuration["Auth0:Domain"]);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery();

builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlite(
        builder.Configuration.GetConnectionString("DbConnectionString")
    );
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<DataContext>("Database", HealthStatus.Degraded)
    .AddUrlGroup(
        new Uri("https://" + builder.Configuration["Auth0:Domain"]),
        "AuthenticationProvider",
        HealthStatus.Degraded,
        timeout: TimeSpan.FromSeconds(5)
    );

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapDefaultHealthChecks();

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
        var context = services.GetRequiredService<DataContext>();
        context.Database.Migrate();
        //await Seed.SeedData(context, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration");
    }
};

app.Run();
