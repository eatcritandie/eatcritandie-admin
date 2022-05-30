namespace EatCritAndDie.Admin.Infrastructure.Options;

public class DiscordOptions
{
    public const string SectionName = "Discord";
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Uri { get; set; }
    public ulong GuildId { get; set; }
    public Dictionary<string, ulong> Roles { get; set; }
}