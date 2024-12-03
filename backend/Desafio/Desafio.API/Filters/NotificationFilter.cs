using Desafio.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace Desafio.API.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificationContext;

        public NotificationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var response = new
                {
                    success = false,
                    message = "Operação não realizada. Detalhes:",
                    content = _notificationContext.Notifications
                };

                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));

                return;
            }

            await next();
        }
    }
}
