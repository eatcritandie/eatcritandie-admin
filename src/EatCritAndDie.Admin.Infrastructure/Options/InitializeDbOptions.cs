namespace EatCritAndDie.Admin.Infrastructure.Options;

public class InitializeDbOptions
{
    public const string SectionName = "SeedData";
    
    public List<InitializeDbUser> Users { get; set; }
}

public class InitializeDbUser
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}