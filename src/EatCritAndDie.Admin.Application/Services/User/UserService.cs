using EatCritAndDie.Admin.Infrastructure.Options;
using EatCritAndDie.Admin.Infrastructure.Providers.Discord;
using EatCritAndDie.Admin.Infrastructure.Providers.Discord.Models;

namespace EatCritAndDie.Admin.Application.Services.User;

public class UserService : IUserService
{
    private readonly IDiscordProvider _discordProvider;
    private readonly DiscordOptions _options;

    public UserService(IDiscordProvider discordProvider, DiscordOptions options)
    {
        _discordProvider = discordProvider;
        _options = options;
    }

    public async Task<Domain.Models.User> GetUserAsync()
    {
        var guildMember = await _discordProvider.GetUserGuildMemberAsync();
        return MapGuildMemberToUser(guildMember);
    }

    private Domain.Models.User MapGuildMemberToUser(GuildMember guildMember)
    {
        return new Domain.Models.User
        {
            UserId = guildMember.DiscordUser.Id,
            Email = guildMember.DiscordUser.Email,
            Username = guildMember.DiscordUser.Username,
            GuildNickname = guildMember.Nickname,
            Roles = guildMember.RoleIds
                .SelectMany(guildMemberRoleId => _options.Roles.Where(x => x.Value == guildMemberRoleId))
                .ToDictionary(x => x.Key, y => y.Value)
        };
    }
}