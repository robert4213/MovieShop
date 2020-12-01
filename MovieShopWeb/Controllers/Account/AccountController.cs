using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Requests;

namespace MovieShopWeb.Controllers.Account
{
    public class AccountController:Controller
    {
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model,string test,int value, string email,string empty)
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            return View();
        }
    }
}