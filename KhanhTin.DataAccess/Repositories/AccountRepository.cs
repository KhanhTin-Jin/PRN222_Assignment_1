using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KhanhTin.DataAccess.Data;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public AccountRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public async Task<SystemAccount> GetByEmailAsync(string email)
        {
            return await _dbcontext.SystemAccounts
                .FirstOrDefaultAsync(a => a.AccountEmail == email);
        }

        public IEnumerable<SystemAccount> GetAll()
        {
            return _dbcontext.SystemAccounts.ToList();
        }

        public SystemAccount GetById(int id)
        {
            return _dbcontext.SystemAccounts.Find(id);
        }

        public void Add(SystemAccount account)
        {
            _dbcontext.SystemAccounts.Add(account);
        }

        public void Update(SystemAccount account)
        {
            _dbcontext.SystemAccounts.Update(account);
        }

        public void Delete(int id)
        {
            var account = _dbcontext.SystemAccounts.Find(id);
            if (account != null)
            {
                _dbcontext.SystemAccounts.Remove(account);
            }
        }

        public bool EmailExists(string email, int? excludeId = null)
        {
            return excludeId.HasValue
                ? _dbcontext.SystemAccounts.Any(a => a.AccountEmail == email && a.AccountID != excludeId)
                : _dbcontext.SystemAccounts.Any(a => a.AccountEmail == email);
        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }
    }
}
