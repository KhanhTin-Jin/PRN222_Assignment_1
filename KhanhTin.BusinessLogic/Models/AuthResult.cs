using System;

namespace KhanhTin.BusinessLogic.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public int Role { get; set; }
        public string ErrorMessage { get; set; }

        // Helper properties
        public string RoleName => Role == 0 ? "Admin" : Role == 1 ? "Staff" : "Lecturer";
        public bool IsAdmin => Role == 0;
        public bool IsStaff => Role == 1;
        public bool IsLecturer => Role == 2;

        // Static factory methods for common scenarios
        public static AuthResult SuccessResult(int accountId, string accountName, string email, int role)
        {
            return new AuthResult
            {
                Success = true,
                AccountId = accountId,
                AccountName = accountName,
                AccountEmail = email,
                Role = role
            };
        }

        public static AuthResult FailureResult(string errorMessage = "Authentication failed")
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
