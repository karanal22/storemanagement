using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using StoreManagement.Common.Helper;
using StoreManagement.Common.Model;

namespace StoreManagement.Common.ExceptionHandler
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception == null)
                        return;

                    ApiErrorResponse errorResponse;
                    if (exception is AppException AppPortalException)
                    {
                        errorResponse = GetErrorResponse(AppPortalException);
                        context.Response.StatusCode = (int)errorResponse.HttpStatus;
                    }
                    else
                    {
                        // log errors and generate response based on exception
                        ILogger logger = loggerFactory.CreateLogger(typeof(ExceptionMiddleware));
                        logger.LogError(exception.ToString());

                        errorResponse = ExceptionResponse(exception);
                        context.Response.StatusCode = (int)errorResponse.HttpStatus;
                    }

                    await ReturnResponse(context, errorResponse);

                });
            });
        }

        public static HttpStatusCode GetHttpStatus(int code)
        {
            if (1000 <= code && code <= 1999) return HttpStatusCode.Unauthorized;
            if (2000 <= code && code <= 2999) return HttpStatusCode.BadRequest;
            if (3000 <= code && code <= 3999) return HttpStatusCode.NotFound;
            if (4000 <= code && code <= 5999) return HttpStatusCode.Conflict;
            return HttpStatusCode.InternalServerError;
        }

        private static ApiErrorResponse GetErrorResponse(AppException AppPortalException)
        {
            var errorResponse = AppPortalException.GetMessageModel();
            errorResponse.HttpStatus = GetHttpStatus(errorResponse.Error.Code);
            errorResponse.HttpError = GetHttpError(errorResponse.HttpStatus);
            errorResponse.TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return errorResponse;
        }

        private static string GetHttpError(HttpStatusCode httpStatusCode)
        {
            if (httpStatusCode == HttpStatusCode.BadRequest)
                return "Bad Request";
            if (httpStatusCode == HttpStatusCode.NotFound)
                return "Not Found";
            if (httpStatusCode == HttpStatusCode.Unauthorized)
                return "Unauthorized";
            if (httpStatusCode == HttpStatusCode.Conflict)
                return "Conflict";
            return "Internal server error";
        }

        private static ApiErrorResponse ExceptionResponse(Exception exception)
        {
            AppErrorCode errorCode;
            //TODO:Check for sql
            //if (exception is MySqlException)
            //{
            //    errorCode = AppErrorCode.MySqlException;
            //}
            //else
            //{
            //    //Todo: add more exception type to identify exception
            //    errorCode = AppErrorCode.Exception;
            //}
            errorCode = AppErrorCode.Exception;

            return new ApiErrorResponse
            {
                HttpStatus = HttpStatusCode.InternalServerError,
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                HttpError = GetHttpError(HttpStatusCode.InternalServerError),
                Error = new AppError
                {
                    Code = (int)errorCode,
                    Message = string.Format(EnumHelper.GetEnumDescription(errorCode), exception.Message)
                }
            };
        }

        private static async Task ReturnResponse(HttpContext context, ApiErrorResponse errorResponse)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, options));
        }
    }
}
