using System.Text.Json.Serialization;

namespace EatCritAndDie.Admin.Infrastructure.Providers.Discord.Models;

public class GuildMember
{
    [JsonPropertyName("user")]
    public DiscordUser DiscordUser { get; set; }
    [JsonPropertyName("nick")]
    public string Nickname { get; set; }
    [JsonPropertyName("roles")]
    public IEnumerable<ulong> RoleIds { get; set; }
}