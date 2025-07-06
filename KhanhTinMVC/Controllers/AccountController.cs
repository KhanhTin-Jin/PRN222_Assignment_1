using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KhanhTinMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginDto());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.AuthenticateAsync(model.Email, model.Password);
                if (result.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, result.AccountName),
                        new Claim(ClaimTypes.NameIdentifier, result.AccountId.ToString()),
                        new Claim(ClaimTypes.Role, result.Role.ToString()),
                        new Claim(ClaimTypes.Email, result.AccountEmail)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Invalid login attempt.");
            }

            return View(model);
        }

        [Authorize(Roles = "0")] // Admin only
        public IActionResult ManageAccounts()
        {
            var accounts = _accountService.GetAllAccounts().ToList();
            return View(accounts);
        }

        [Authorize(Roles = "0")] // Admin only
        public IActionResult Create()
        {
            return View(new AccountCreateDto());
        }

        [HttpPost]
        [Authorize(Roles = "0")] // Admin only
        [ValidateAntiForgeryToken]
        public IActionResult Create(AccountCreateDto model)
        {
            if (ModelState.IsValid)
            {
                if (!_accountService.IsEmailUnique(model.AccountEmail))
                {
                    ModelState.AddModelError("AccountEmail", "Email already exists");
                    return View(model);
                }

                _accountService.CreateAccount(model);
                TempData["SuccessMessage"] = "Account created successfully.";
                return RedirectToAction(nameof(ManageAccounts));
            }

            return View(model);
        }

        [Authorize(Roles = "0")] // Admin only
        public IActionResult Edit(int id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            var model = new AccountUpdateDto
            {
                AccountID = account.AccountID,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "0")] // Admin only
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AccountUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                if (!_accountService.IsEmailUnique(model.AccountEmail, model.AccountID))
                {
                    ModelState.AddModelError("AccountEmail", "Email already exists");
                    return View(model);
                }

                _accountService.UpdateAccount(model);
                TempData["SuccessMessage"] = "Account updated successfully.";
                return RedirectToAction(nameof(ManageAccounts));
            }

            return View(model);
        }

        // --- UPDATED DELETE ACTION ---
        [HttpPost]
        [Authorize(Roles = "0")]
        [Route("Accounts/Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id.ToString() == currentUserId)
            {
                TempData["ErrorMessage"] = "You cannot delete your own account.";
                return RedirectToAction(nameof(ManageAccounts));
            }

            // Call the service method and check the boolean result
            bool wasDeleted = _accountService.DeleteAccount(id);

            if (wasDeleted)
            {
                TempData["SuccessMessage"] = "Account deleted successfully.";
            }
            else
            {
                // This message is shown if the database prevents deletion (e.g., due to foreign key)
                TempData["ErrorMessage"] = "This account cannot be deleted because it is associated with existing news articles.";
            }
            
            return RedirectToAction(nameof(ManageAccounts));
        }

        [Authorize(Roles = "1")] // Staff only
        public IActionResult Profile()
        {
            int accountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var account = _accountService.GetAccountById(accountId);
            if (account == null)
            {
                return NotFound();
            }

            var model = new ProfileUpdateDto
            {
                AccountID = account.AccountID,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "1")] // Staff only
        [ValidateAntiForgeryToken]
        public IActionResult Profile(ProfileUpdateDto model)
        {
            int accountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (model.AccountID != accountId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                if (!_accountService.IsEmailUnique(model.AccountEmail, model.AccountID))
                {
                    ModelState.AddModelError("AccountEmail", "Email already exists");
                    return View(model);
                }

                _accountService.UpdateProfile(model);
                TempData["SuccessMessage"] = "Profile updated successfully.";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
