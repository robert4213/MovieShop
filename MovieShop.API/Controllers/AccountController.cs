using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.Models.Responses;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
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
            if (!ModelState.IsValid) return BadRequest(new {message = "Invalid Username/Password"});
            
            var response = await _userService.ValidateUser(loginRequestModel);
            return response is null ? Unauthorized() : Ok(new {token = GenerateJWT(response)});
        }

        private string GenerateJWT(UserLoginResponseModel userLoginResponse)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userLoginResponse.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,userLoginResponse.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,userLoginResponse.LastName),
                new Claim(JwtRegisteredClaimNames.Email,userLoginResponse.Email)
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            var securityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenSettings:PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<double>("TokenSettings:ExpirationHours"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Audience = _configuration["TokenSettings:Audience"],
                Issuer = _configuration["TokenSettings:Issuer"],
                SigningCredentials = credentials,
                Expires = expires
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodeToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(encodeToken);

        }
    }
}
