using InGameProject.Business.Abstract;
using InGameProject.Business.Concrete;
using InGameProject.DataAccess.EntityFramework;
using InGameProject.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGameProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserManager _userService = new UserManager(new EfUserDal());

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userService.GetUserListBL();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetUserByIdBL(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("getbyemail")]
        public IActionResult GetByEmail(string email)
        {
            var result = _userService.GetUserByEmailBL(email);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = _userService.UserAddBL(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            var result = _userService.UserUpdateBL(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _userService.UserDeleteBL(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
