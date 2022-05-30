namespace EatCritAndDie.Admin.Infrastructure.Providers.Discord;

public interface IDiscordClient
{
    Task<HttpResponseMessage> GetGuildRolesAsync();
    Task<HttpResponseMessage> GetUserGuildMemberAsync();
}