using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using StoreManagement.Common.Helper;
using StoreManagement.Common.Model;

namespace StoreManagement.Common.ExceptionHandler
{
    [Serializable]
    public class AppException : Exception
    {
        private readonly ApiErrorResponse _errorResponse;
        public ApiErrorResponse ErrorResponse => _errorResponse;

        public AppException(AppErrorCode errorCode, string[] args = null)
        {
            var message = GetErrorMessage(errorCode, args);
            _errorResponse = new ApiErrorResponse(errorCode, message, args);
        }

        public AppException(ModelStateDictionary modelState)
        {
            var error = new AppError
            {
                Code = (int)AppErrorCode.InvalidParameters,
                Message = EnumHelper.GetEnumDescription(AppErrorCode.InvalidParameters)
            };

            var errors = modelState.Values.SelectMany(m => m.Errors)
                .Select(x =>
                {
                    var values = x.ErrorMessage.Split("~").ToList();
                    return new AppError
                    {
                        Code = Convert.ToInt32(values[0]),
                        Message = values.Count > 1 ? values[1] : string.Empty,
                        Args = values.Count > 2 ? JsonConvert.DeserializeObject<string[]>(values[2]) : null
                    };
                }).ToList();

            _errorResponse = new ApiErrorResponse(error, errors);
        }

        public ApiErrorResponse GetMessageModel()
        {
            return _errorResponse;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected AppException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            var errorResponse = (ApiErrorResponse)info.GetValue("AppException.ErrorResponse", typeof(ApiErrorResponse));
            _errorResponse = errorResponse;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("AppException.ErrorResponse", ErrorResponse, typeof(ApiErrorResponse));
        }

        private string GetErrorMessage(AppErrorCode errorCode, string[] args)
        {
            string message = EnumHelper.GetEnumDescription(errorCode);
            if (args == null || args.Length == 0)
                return message;

            return string.Format(message, args);
        }
    }
}
