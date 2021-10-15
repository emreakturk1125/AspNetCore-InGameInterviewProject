using InGameProject.Business.Abstract;
using InGameProject.Business.Concrete;
using InGameProject.DataAccess.EntityFramework;
using InGameProject.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGameProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    { 
        AuthManager _authManager = new AuthManager(new EfUserDal()); 
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            var result = _authManager.Login(user.UserEmail, user.UserPassword);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        { 
            var registerResult = _authManager.Register(user);
            if (registerResult.Success)
            {
                return Ok(registerResult);
            }

            return BadRequest(registerResult);
        }

        [HttpPost("newpassword")]
        public IActionResult NewPassword(string email)
        {
            var result = _authManager.NewPassword(email);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
