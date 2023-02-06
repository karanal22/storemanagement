using StoreManagement.Common.Helper;

namespace StoreManagement.Common.ExceptionHandler
{
    public static class ValidationMessageResource
    {
        public static string Required => $"{(int)AppErrorCode.Required}~{EnumHelper.GetEnumDescription(AppErrorCode.Required)}~[\"{{0}}\"]";
    }
}
