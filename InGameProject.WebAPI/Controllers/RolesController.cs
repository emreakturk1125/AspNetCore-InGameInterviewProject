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
    public class RolesController : ControllerBase
    {

        RoleManager _roleService = new RoleManager(new EfRoleDal());

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _roleService.GetRoleListBL();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _roleService.GetRoleByIdBL(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Role role)
        {
            var result = _roleService.RoleAddBL(role);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Role role)
        {
            var result = _roleService.RoleUpdateBL(role);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _roleService.RoleDeleteBL(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
