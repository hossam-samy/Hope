using Hope.Core.Common;
using Hope.Domain.Common;
using Hope.Domain.Common.GlobalException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Tls;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace Hope.Infrastructure.Middlewares
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
                     throw new UnAuthrizedException(localizer["UnAuthorized"]); 
                
            }
            catch(Exception e) {

                var json = JsonSerializer.Serialize(await Response.FailureAsync(localizer["Faild"],e.Message));
                context.Response.ContentType = "application/json";
                context?.Response.WriteAsync(json);
            }
        }
    }
}
