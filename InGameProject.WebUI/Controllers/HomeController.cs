using InGameProject.Entities.Concrete;
using InGameProject.WebUI.Helpers;
using InGameProject.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InGameProject.WebUI.Controllers
{
    [Authorize(Roles = "product_view,user")]
    public class HomeController : Controller
    {
        HelperAPI _api = new HelperAPI();

        public IActionResult Index()
        {
             
            var token = HttpContext.Session.GetString("accessToken");

            HelperRestApi<Product> helperForProduct = new HelperRestApi<Product>();
            ViewBag.ProductCount = helperForProduct.ConnectRestApiForList("https://localhost:44351/api/products/getall", token).Data.Count;

            HelperRestApi<Category> helperForCategory = new HelperRestApi<Category>();
            ViewBag.CategoryCount = helperForCategory.ConnectRestApiForList("https://localhost:44351/api/categories/getall", token).Data.Count;

            HelperRestApi<User> helperForUser = new HelperRestApi<User>();
            ViewBag.UserCount = helperForUser.ConnectRestApiForList("https://localhost:44351/api/users/getall", token).Data.Count;

            HelperRestApi<Role> helperForRole = new HelperRestApi<Role>();
            ViewBag.RoleCount = helperForRole.ConnectRestApiForList("https://localhost:44351/api/roles/getall", token).Data.Count;

            return View();
        }

       
         
    }
}
