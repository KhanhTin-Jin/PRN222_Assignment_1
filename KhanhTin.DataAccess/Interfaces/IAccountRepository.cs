using System.Collections.Generic;
using System.Threading.Tasks;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Interfaces
{
    public interface IAccountRepository
    {
        Task<SystemAccount> GetByEmailAsync(string email);
        IEnumerable<SystemAccount> GetAll();
        SystemAccount GetById(int id);
        void Add(SystemAccount account);
        void Update(SystemAccount account);
        void Delete(int id);
        bool EmailExists(string email, int? excludeId = null);
        void SaveChanges();
    }
}
