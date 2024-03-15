using Microsoft.Extensions.Localization;
using System.Text.Json;
using Hope.Core.Common;
using Hope.Core.Common.GlobalException;
namespace Hope.Api.Middlewares
{
    public class GlobleErrorHandlerMiddleware 
    {
        private readonly IStringLocalizer<GlobleErrorHandlerMiddleware> localizer;
        private readonly RequestDelegate _next;
        public GlobleErrorHandlerMiddleware( RequestDelegate next, IStringLocalizer<GlobleErrorHandlerMiddleware> localizer)
        {
            _next = next;
            this.localizer = localizer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try {
              await _next(context);  
                
                var statuscode=context.Response.StatusCode;
                
                if(statuscode==401)
                     throw new UnAuthrizedException("UnAuthorized"); 
                
            }
            catch(Exception e) {

                var json = JsonSerializer.Serialize(await Response.FailureAsync(e.InnerException.Message,e.Message));
                context.Response.ContentType = "application/json";
                context?.Response.WriteAsync(json);
            }
        }
    }
}
