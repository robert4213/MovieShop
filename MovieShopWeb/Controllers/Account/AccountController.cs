using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShopWeb.Controllers.Account
{
    public class AccountController:Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            //only when all validation is true, we need to proceed further
            if (ModelState.IsValid)
            {
                //send model to service
                await _userService.CreateUser(model);
            }
            
            return RedirectToAction("Login","Account");
        }
        
        [HttpGet]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid) return View();
            
            var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);
            
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
            
            // successfully enter username & password
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname,  user.LastName),
                new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString())
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),new AuthenticationProperties
                {
                    IsPersistent = true
                });
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyAccount()
        {
            return View();
        }
    }
}