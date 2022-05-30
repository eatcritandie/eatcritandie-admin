using System.Text.Json.Serialization;

namespace EatCritAndDie.Admin.Infrastructure.Providers.Discord.Models;

public class DiscordUser
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
}