using System.Collections.Generic;
using System.Linq;
using System.Net;
using StoreManagement.Common.ExceptionHandler;

namespace StoreManagement.Common.Model
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(AppErrorCode errorCode, string message, string[] args)
        {
            Error = new AppError { Code = (int)errorCode, Message = message, Args = args?.ToArray() };
        }

        public ApiErrorResponse(AppError error, List<AppError> errors)
        {
            Error = error;
            Errors = errors;
        }

        public ApiErrorResponse()
        {
            Error = new AppError();
        }

        public long TimeStamp { get; set; }
        public HttpStatusCode HttpStatus { get; set; }
        public string HttpError { get; set; }
        public AppError Error { get; set; }
        public List<AppError> Errors { get; set; }
    }

    public class AppError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string[] Args { get; set; }
    }
}
