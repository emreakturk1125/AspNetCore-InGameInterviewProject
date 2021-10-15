using InGameProject.Business.Abstract;
using InGameProject.Business.Concrete;
using InGameProject.DataAccess.EntityFramework;
using InGameProject.Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class ProductsController : ControllerBase
    {

        ProductManager _productManager = new ProductManager(new EfProductDal());

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productManager.GetProductListBL();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getallforunittest")]
        public IActionResult GetAllForUnitTest()
        {
            var result = _productManager.GetProductListForUnitTestBL();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getallbystoredprocedure")]
        public IActionResult GetAllByStoredProcedure()
        {
            var result = _productManager.GetProductListByStoredProcedureBL();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getallwithcategory")]
        public IActionResult GetAllWithCategory()
        {
            var result = _productManager.GetProductListWithCategoryBL();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }    

        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productManager.GetProductByIdBL(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyidforunittest")]
        public IActionResult GetByIdFroUnitTest(int id)
        {
            var result = _productManager.GetProductForUnitTestByIdBL(id);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("getwithcategorybyid")]
        public IActionResult GetWithCategoryById(int id)
        {
            var result = _productManager.GetProductWithCategoryByIdBL(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productManager.ProductAddBL(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _productManager.ProductUpdateBL(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _productManager.ProductDeleteBL(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
