using CleanControllers.API.Enums;
using CleanControllers.API.Extensions;
using CleanControllers.API.Settings.NotificationSettings;
using System.Text.Json;

namespace CleanControllers.API.Middlewares;

public sealed class NotificationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new List<Notification>()
            {
                new()
                {
                    Key = nameof(EMessage.UnexpectedError),
                    Message = EMessage.UnexpectedError.Description()
                }
            };

            var responseJsonString = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(responseJsonString);
        }
    }
}
