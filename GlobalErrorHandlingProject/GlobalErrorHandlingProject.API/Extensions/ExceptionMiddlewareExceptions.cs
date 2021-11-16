using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using loggerservice;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using GlobalErrorHandlingProject.API.Exceptions;

namespace GlobalErrorHandlingProject.API.Extensions
{
    public static class ExceptionMiddlewareExceptions
    {
      public static void ConfigureExceptionHandler(this IApplicationBuilder app) 
        {
           
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                       // logger.LogError($"something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "GOKCEEE"
                        }
                        .ToString());
                    }
                });
            });
        
        }

        
    }
}
