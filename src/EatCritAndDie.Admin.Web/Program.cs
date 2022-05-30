using AspNet.Security.OAuth.Discord;
using EatCritAndDie.Admin.Application.Services.User;
using EatCritAndDie.Admin.Data.Context;
using EatCritAndDie.Admin.Infrastructure.Options;
using EatCritAndDie.Admin.Infrastructure.Providers.Discord;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EatCritAndDie.Admin.Web.Areas.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services
    .AddDbContext<EatCritAndDieDbContext>(options =>
        options.UseSqlite(connectionString,
            x => x.MigrationsAssembly(typeof(EatCritAndDieDbContext).Assembly.FullName)))
    .AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<EatCritAndDieDbContext>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie()
    // https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers/tree/dev/src/AspNet.Security.OAuth.Discord
    // https://vic485.xyz/2020/07/blazor-discord-oauth2/
    .AddDiscord(options =>
    {
        options.ClientId = builder.Configuration["Discord:ClientId"];
        options.ClientSecret = builder.Configuration["Discord:ClientSecret"];
        options.SaveTokens = true;
        options.Scope.Add("guilds");
        options.Scope.Add("guilds.members.read");
    });

// From: https://github.com/aspnet/Blazor/issues/1554
// Adds HttpContextAccessor
// Used to determine if a user is logged in
// and what their username is
builder.Services
    .AddHttpContextAccessor()
    .AddScoped<HttpContextAccessor>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .Configure<InitializeDbOptions>(builder.Configuration.GetSection(InitializeDbOptions.SectionName))
    .Configure<DiscordOptions>(builder.Configuration.GetSection(DiscordOptions.SectionName));

builder.Services
    .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>()
    // Options
    .AddTransient(s => s.GetRequiredService<IOptions<InitializeDbOptions>>().Value)
    .AddTransient(s => s.GetRequiredService<IOptions<DiscordOptions>>().Value)
    // Services
    .AddSingleton<IDiscordClient, DiscordClient>()
    .AddSingleton<IDiscordProvider, DiscordProvider>()
    .AddScoped<IUserService, UserService>();


// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0#environment-variables
builder.Configuration.AddEnvironmentVariables(prefix: "ECD_");

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// development only services
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
