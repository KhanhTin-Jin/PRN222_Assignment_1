using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using KhanhTin.BusinessLogic.Models;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhanhTin.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AuthResult> AuthenticateAsync(string email, string password)
        {
            var account = await _accountRepository.GetByEmailAsync(email);
            if (account == null)
            {
                return AuthResult.FailureResult("Account not found");
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(password, account.AccountPassword);
            if (!passwordValid)
            {
                return AuthResult.FailureResult("Invalid password");
            }

            return AuthResult.SuccessResult(
                account.AccountID,
                account.AccountName,
                account.AccountEmail,
                account.AccountRole
            );
        }

        public IEnumerable<AccountDto> GetAllAccounts()
        {
            return _accountRepository.GetAll().Select(a => new AccountDto
            {
                AccountID = a.AccountID,
                AccountName = a.AccountName,
                AccountEmail = a.AccountEmail,
                AccountRole = a.AccountRole
            });
        }

        public AccountDto GetAccountById(int id)
        {
            var account = _accountRepository.GetById(id);
            if (account == null)
            {
                return null;
            }

            return new AccountDto
            {
                AccountID = account.AccountID,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole
            };
        }

        public void CreateAccount(AccountCreateDto dto)
        {
            var account = new SystemAccount
            {
                AccountName = dto.AccountName,
                AccountEmail = dto.AccountEmail,
                AccountRole = dto.AccountRole,
                AccountPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _accountRepository.Add(account);
            _accountRepository.SaveChanges();
        }

        public void UpdateAccount(AccountUpdateDto dto)
        {
            var account = _accountRepository.GetById(dto.AccountID);
            if (account == null)
            {
                return;
            }

            account.AccountName = dto.AccountName;
            account.AccountEmail = dto.AccountEmail;
            account.AccountRole = dto.AccountRole;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                account.AccountPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            _accountRepository.Update(account);
            _accountRepository.SaveChanges();
        }

        public void UpdateProfile(ProfileUpdateDto dto)
        {
            var account = _accountRepository.GetById(dto.AccountID);
            if (account == null)
            {
                return;
            }

            account.AccountName = dto.AccountName;
            account.AccountEmail = dto.AccountEmail;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                account.AccountPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            _accountRepository.Update(account);
            _accountRepository.SaveChanges();
        }

        public bool DeleteAccount(int id)
        {
            try
            {
                _accountRepository.Delete(id);
                _accountRepository.SaveChanges();
                return true; // Return true only if SaveChanges() is successful
            }
            catch (DbUpdateException)
            {
                // This will catch foreign key constraint violations and other database update errors.
                return false; // Return false if an error occurs, signaling failure to the controller.
            }
        }

        public bool IsEmailUnique(string email, int? excludeId = null)
        {
            return !_accountRepository.EmailExists(email, excludeId);
        }
    }
}
