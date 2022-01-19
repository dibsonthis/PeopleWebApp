using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleWebApp.Models;

namespace PeopleWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnURL)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var signIn = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);

            if (!signIn.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Login attempt unsuccessful");
                return View();
                
            }

            if (!String.IsNullOrEmpty(returnURL) && Url.IsLocalUrl(returnURL)) // to combat Open Redirect Attacks
            {
                return Redirect(returnURL);
            }

            return RedirectToAction("Index", "People");
        }

        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
            {
                return Json(true);
            }

            return Json($"Email {email} is unavailable");
        }


    }
}
