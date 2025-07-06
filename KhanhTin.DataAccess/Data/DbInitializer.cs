using System.Linq;
using BCrypt.Net;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if admin account exists
            if (!context.SystemAccounts.Any())
            {
                var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("@@abc123@@");
                var admin = new SystemAccount
                {
                    AccountName = "Administrator",
                    AccountEmail = "admin@FUNewsManagementSystem.org",
                    AccountRole = 0, // Admin
                    AccountPassword = adminPasswordHash
                };

                context.SystemAccounts.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
