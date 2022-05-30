using EatCritAndDie.Admin.Infrastructure.Providers.Discord.Models;

namespace EatCritAndDie.Admin.Infrastructure.Providers.Discord;

public interface IDiscordProvider
{
    Task<GuildMember> GetUserGuildMemberAsync();
}