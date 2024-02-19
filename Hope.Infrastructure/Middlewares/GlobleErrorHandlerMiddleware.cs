using Hope.Core.Common;
using Hope.Domain.Common.GlobalException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace Hope.Infrastructure.Middlewares
{
    public class GlobleErrorHandlerMiddleware 
    {
        private readonly RequestDelegate _next;
        public GlobleErrorHandlerMiddleware( RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try {
              await _next(context);  
                
                var statuscode=context.Response.StatusCode;
                
                if(statuscode==401)
                     throw new UnAuthrizedException("UnAuthorized"); 
                
                else if(statuscode==400)
                     throw new BadRequestException("Bad Request");

              
            }
            catch(Exception e) {

                ProblemDetails details;

                if (e.GetType() == typeof(UnAuthrizedException) || e.GetType() == typeof(BadRequestException))
                {
                    details = new ProblemDetails()
                    {
                        Status = context.Response.StatusCode,
                        Title = e.Message,
                    };
                    
                }
                else
                {
                    details = new ProblemDetails()
                    {
                        Status = 500,
                        Title = "Server Error",
                    };
                }
                var json = JsonSerializer.Serialize(details);
                context.Response.ContentType = "application/json";
                context.Response.WriteAsync(json);
            }
        }
    }
}
