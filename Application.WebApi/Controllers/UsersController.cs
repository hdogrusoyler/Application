using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Blog.WebApi.Models;
using Application.Business.Abstract;
using Application.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            var userNames = new List<string>();
            var users = _userService.GetAll();
            foreach (var user in users)
            {
                userNames.Add(user.Name);
            }
            return Ok(userNames);
        }

        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {
            var user = _userService.GetById(id);
            if (user != null)
            {
                var userName = user.Name;
                return Ok(userName);
            }

            return Ok("There is no user");
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            _userService.Delete(user);
            return StatusCode(201);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] UserModel userModel)
        {
            if (!_userService.UserExists(userModel.UserName))
            {
                ModelState.AddModelError("UserName", "Username is incorrect");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                Name = userModel.UserName
            };

            var createdUser = _userService.Update(userToCreate, userModel.Password);
            return StatusCode(201);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] UserModel userModel)
        {
            if (_userService.UserExists(userModel.UserName))
            {
                ModelState.AddModelError("UserName","Username already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                Name = userModel.UserName
            };

            var createdUser = _userService.Add(userToCreate, userModel.Password);
            return StatusCode(201);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login([FromBody] UserModel userModel)
        {
            var user = _userService.Login(userModel.UserName, userModel.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                }),
                Audience = "http://localhost:4200",
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                    , SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
    }
}
