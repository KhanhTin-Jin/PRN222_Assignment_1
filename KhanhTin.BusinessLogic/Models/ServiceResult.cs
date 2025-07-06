using System.Collections.Generic;

namespace KhanhTin.BusinessLogic.Models
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object Data { get; set; }

        public static ServiceResult SuccessResult(string message = "Operation completed successfully", object data = null)
        {
            return new ServiceResult
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ServiceResult FailureResult(string message, List<string> errors = null)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }

        public static ServiceResult ValidationFailure(List<string> validationErrors)
        {
            return new ServiceResult
            {
                Success = false,
                Message = "Validation failed",
                Errors = validationErrors
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public new T Data { get; set; }

        public static ServiceResult<T> SuccessResult(T data, string message = "Operation completed successfully")
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static new ServiceResult<T> FailureResult(string message, List<string> errors = null)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
