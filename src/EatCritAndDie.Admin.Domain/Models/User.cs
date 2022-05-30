namespace EatCritAndDie.Admin.Domain.Models;

public class User
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public string GuildNickname { get; set; }
    public string Email { get; set; }
    public Dictionary<string, ulong> Roles { get; set; }
}