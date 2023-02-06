using System.Collections.Generic;
using System.Net;
using StoreManagement.Common.ExceptionHandler;

namespace StoreManagement.Common.Model
{
    public class ErrorCode
    {
        public AppErrorCode Code { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class ErrorGroup
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string HttpStatus => HttpStatusCode.ToString();
        public List<ErrorCode> Errors { get; set; }
    }
}
