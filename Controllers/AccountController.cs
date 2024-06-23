using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DinoLoan.Entity;
using DinoLoan.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DinoLoan.Controllers
{
    public class AccountController : Controller
    {

       private readonly DinoloanDbContext _context;

        public AccountController(DinoloanDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel acctvm)
        {
            var user = _context.Accounts.FirstOrDefault(u => u.Username == acctvm.Username && u.Password == acctvm.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel acctvm)
        {
            if (acctvm.Password != acctvm.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View();
            }

            var existingUser = _context.Accounts.FirstOrDefault(u => u.Username == acctvm.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username already exists");
                return View();
            }

            var newUser = new Account
            {
                Username = acctvm.Username,
                Password = acctvm.Password
            };

            _context.Accounts.Add(newUser);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, newUser.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}