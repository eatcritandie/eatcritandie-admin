using EatCritAndDie.Admin.Infrastructure.Extensions;
using EatCritAndDie.Admin.Infrastructure.Options;
using Microsoft.AspNetCore.Http;

namespace EatCritAndDie.Admin.Infrastructure.Providers.Discord;

public class DiscordClient : IDiscordClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _httpClient;
    private readonly Uri _baseAddress;
    private readonly DiscordOptions _options;

    public DiscordClient(IHttpContextAccessor httpContextAccessor, DiscordOptions options)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClient = new HttpClient();
        _baseAddress = new Uri(options.Uri);
        _options = options;
    }

    public Task<HttpResponseMessage> GetGuildRolesAsync()
    {
        var path = $"/guilds/{_options.GuildId}/roles";
        var uriBuilder = new UriBuilder
        {
            Scheme = _baseAddress.Scheme,
            Host = _baseAddress.Host,
            Path = _baseAddress.LocalPath + path
        };
        return GetAsync(uriBuilder.Uri.AbsoluteUri);
    }

    public Task<HttpResponseMessage> GetUserGuildMemberAsync()
    {
        var path = $"/users/@me/guilds/{_options.GuildId}/member";
        var uriBuilder = new UriBuilder
        {
            Scheme = _baseAddress.Scheme,
            Host = _baseAddress.Host,
            Path = _baseAddress.LocalPath + path
        };
        return GetAsync(uriBuilder.Uri.AbsoluteUri);
    }
    
    private async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        var accessToken = await _httpContextAccessor.HttpContext?.GetDiscordTokenAsync();
        var getRequest = new HttpRequestMessage(HttpMethod.Get, requestUri)
            .AddDefaultHeadersWithToken(accessToken);

        return await _httpClient.SendAsync(getRequest);
    }
    
    
}