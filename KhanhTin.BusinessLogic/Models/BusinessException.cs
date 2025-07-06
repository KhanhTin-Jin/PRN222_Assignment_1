using System;
using System.Collections.Generic;

namespace KhanhTin.BusinessLogic.Models
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public object Data { get; set; }

        public BusinessException() : base()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BusinessException(string message, List<string> validationErrors) : base(message)
        {
            ValidationErrors = validationErrors;
        }

        public BusinessException(string message, string errorCode, List<string> validationErrors) : base(message)
        {
            ErrorCode = errorCode;
            ValidationErrors = validationErrors;
        }

        // Common business exceptions
        public static BusinessException NotFound(string entityName, int id)
        {
            return new BusinessException($"{entityName} with ID {id} was not found.", "ENTITY_NOT_FOUND");
        }

        public static BusinessException ValidationFailed(List<string> errors)
        {
            return new BusinessException("Validation failed.", "VALIDATION_FAILED", errors);
        }

        public static BusinessException Unauthorized(string action)
        {
            return new BusinessException($"You are not authorized to {action}.", "UNAUTHORIZED");
        }

        public static BusinessException DuplicateEntry(string fieldName, string value)
        {
            return new BusinessException($"A record with {fieldName} '{value}' already exists.", "DUPLICATE_ENTRY");
        }

        public static BusinessException CannotDelete(string entityName, string reason)
        {
            return new BusinessException($"Cannot delete {entityName}: {reason}", "CANNOT_DELETE");
        }
    }
}
