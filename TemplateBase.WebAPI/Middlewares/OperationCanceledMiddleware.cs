using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace TemplateBase.WebAPI.Middlewares
{
    public class OperationCanceledMiddleware
    {
        private readonly RequestDelegate _next;
        private const int _clientClosedRequest = 499;

        public OperationCanceledMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                context.Response.StatusCode = _clientClosedRequest;
            }
        }
    }
}
