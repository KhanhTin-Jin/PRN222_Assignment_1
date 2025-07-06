using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Models;

namespace KhanhTin.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResult> AuthenticateAsync(string email, string password);
        IEnumerable<AccountDto> GetAllAccounts();
        AccountDto GetAccountById(int id);
        void CreateAccount(AccountCreateDto dto);
        void UpdateAccount(AccountUpdateDto dto);
        bool DeleteAccount(int id);
        bool IsEmailUnique(string email, int? excludeId = null);
        void UpdateProfile(ProfileUpdateDto dto);
    }
}
