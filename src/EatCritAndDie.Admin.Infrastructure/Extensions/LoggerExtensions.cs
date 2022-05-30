using Microsoft.Extensions.Logging;

namespace EatCritAndDie.Admin.Infrastructure.Extensions;

public static class LoggerExtensions
{
    public static async void LogHttpResponseError(this ILogger logger, HttpResponseMessage response, string message)
    {
        logger.LogError(
            $"Message: {message}. Status Code: {response.StatusCode}. Response Content: {await response.Content.ReadAsStringAsync()}.");
    }

    public static async void LogHttpResponseWarning(this ILogger logger, HttpResponseMessage response, string message)
    {
        logger.LogWarning(
            $"Message: {message}. Status Code: {response.StatusCode}. Response Content: {await response.Content.ReadAsStringAsync()}.");
    }
}