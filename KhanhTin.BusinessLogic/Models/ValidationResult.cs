using System.Collections.Generic;
using System.Linq;

namespace KhanhTin.BusinessLogic.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        public void AddError(string propertyName, string errorMessage)
        {
            Errors.Add(new ValidationError(propertyName, errorMessage));
            IsValid = false;
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(new ValidationError(string.Empty, errorMessage));
            IsValid = false;
        }

        public List<string> GetErrorMessages()
        {
            return Errors.Select(e => e.ErrorMessage).ToList();
        }

        public List<string> GetErrorsForProperty(string propertyName)
        {
            return Errors.Where(e => e.PropertyName == propertyName)
                        .Select(e => e.ErrorMessage)
                        .ToList();
        }

        public static ValidationResult Success()
        {
            return new ValidationResult { IsValid = true };
        }

        public static ValidationResult Failure(string errorMessage)
        {
            var result = new ValidationResult();
            result.AddError(errorMessage);
            return result;
        }

        public static ValidationResult Failure(List<ValidationError> errors)
        {
            return new ValidationResult
            {
                IsValid = false,
                Errors = errors
            };
        }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public object AttemptedValue { get; set; }

        public ValidationError()
        {
        }

        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public ValidationError(string propertyName, string errorMessage, object attemptedValue)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            AttemptedValue = attemptedValue;
        }
    }
}