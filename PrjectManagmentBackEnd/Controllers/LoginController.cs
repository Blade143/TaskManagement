using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BAL.DTO;
using BAL.Interfaces;
using BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PrjectManagmentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IUserService _uservice;

        public LoginController(IUserService service, IConfiguration conf)
        {
            this._uservice = service;
            this._config = conf;
        }

        private UserDTO AuthenticateUser(UserLoginModel loginModel)
        {
            UserDTO user = _uservice.GetUserByLogin(loginModel.Login);

            if (user != null && loginModel.Password == user.Password)
            {
                return user;
            }
            return null;
        }

        private string GenerateJSONWebToken(UserDTO userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("ID", userInfo.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // GET: api/Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_uservice.GetUsers());
        }

        // POST: api/Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody]UserLoginModel login)
        {
            ActionResult response = Unauthorized("Not Found");
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString, user = user });
            }

            return response;
        }
    }
}
