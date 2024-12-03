﻿using Desafio.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Desafio.API.Filters
{
    public class AsyncExceptionFilter : IAsyncExceptionFilter
    {
        private readonly INotificationContext _notificationContext;
        public AsyncExceptionFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            Exception e = context.Exception;
            while (e.InnerException != null) e = e.InnerException;

            _notificationContext.AddNotification("Erro", e.Message);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = "Operação não realizada. Detalhes:",
                content = _notificationContext.Notifications
            };

            context.Result = new JsonResult(response);

            return Task.CompletedTask;
        }
    }
}