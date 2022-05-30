using System.Net.Http.Headers;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace EatCritAndDie.Admin.Infrastructure.Extensions;

public static class HttpRequestExtensions
{
    public static HttpRequestMessage AddDefaultHeadersWithToken(this HttpRequestMessage request, string token)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Add("ContentType", "application/json");
        return request;
    }
    
    public static async Task<string> GetDiscordTokenAsync(this HttpContext context)
    {
        return await context.GetTokenAsync(DiscordAuthenticationDefaults.AuthenticationScheme, "access_token");
    }
}