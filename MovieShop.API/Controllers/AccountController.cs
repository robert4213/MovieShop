using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterUser(UserRegisterRequestModel userRegisterRequestModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.CreateUser(userRegisterRequestModel);
                return Ok(response);
            }

            return BadRequest(new {message = "Please enter correct information"});
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            var response = await _userService.GetUserDetails(id);
            return response is null ? BadRequest(new {message = "No user found"}) : Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            if (!ModelState.IsValid) return BadRequest(new {message = "Miss Username/Password"});
            
            var response = await _userService.ValidateUser(loginRequestModel);
            return response is null ? BadRequest(new {message = "Wrong Username/Password"}) : Ok(response);
        }
    }
}
