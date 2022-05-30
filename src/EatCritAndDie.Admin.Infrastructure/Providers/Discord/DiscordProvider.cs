using System.Net.Http.Json;
using EatCritAndDie.Admin.Infrastructure.Extensions;
using EatCritAndDie.Admin.Infrastructure.Providers.Discord.Models;
using Microsoft.Extensions.Logging;

namespace EatCritAndDie.Admin.Infrastructure.Providers.Discord;

public class DiscordProvider : IDiscordProvider
{
    private readonly ILogger<DiscordProvider> _logger;
    private readonly IDiscordClient _client;

    public DiscordProvider(ILogger<DiscordProvider> logger, IDiscordClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<GuildMember> GetUserGuildMemberAsync()
    {
        var response = await _client.GetUserGuildMemberAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogHttpResponseError(response, "The guild member api get was not successful.");
        }

        return await response.Content.ReadFromJsonAsync<GuildMember>();
    }
    
    
}