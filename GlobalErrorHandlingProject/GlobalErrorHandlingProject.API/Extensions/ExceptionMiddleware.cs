using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using global::Serilog;
using Serilog.Core;
using System.IO;
using Newtonsoft.Json;
using GlobalErrorHandlingProject.API.Exceptions;

namespace GlobalErrorHandlingProject.API.Extensions
  { 
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

   
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            var errorDetails = new ErrorDetails();
            errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";  
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (e.GetType() == typeof(ValidationException))
            {
                errorDetails.Message = e.Message;
                errorDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(ApplicationException))
            {
                errorDetails.Message = e.Message;
                errorDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                errorDetails.Message = e.Message;
                errorDetails.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else if (e.GetType() == typeof(SecurityException))
            {
                errorDetails.Message = e.Message;
                errorDetails.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                errorDetails.Message = "sorry mario the princess is in another castle.";
            }
            var serializeObject = JsonConvert.SerializeObject(errorDetails);
            Log.Error(serializeObject);
            await httpContext.Response.WriteAsync(serializeObject);
        }
    }
}
