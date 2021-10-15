using InGameProject.Entities.Concrete;
using InGameProject.WebUI.Helpers;
using InGameProject.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InGameProject.WebUI.Controllers
{
    [Authorize(Roles = "product_view,user")]
    public class CategoryController : Controller
    {
        HelperAPI _api = new HelperAPI();
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Category> helper = new HelperRestApi<Category>();
            var response = helper.ConnectRestApiForList("https://localhost:44351/api/categories/getall", token);

            if (!response.Success)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(response.Data);
        }


        [HttpGet]
        public ActionResult AddCategory()
        {

            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Category> helper = new HelperRestApi<Category>();
            var result = helper.ConnectRestApiForList("https://localhost:44351/api/categories/getall", token);

            if (result.Success)
            {
                List<SelectListItem> valueRoles = (from x in result.Data
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
                ViewBag.vlc = valueRoles;
            }


            return View();
             
        }

        [HttpPost]
        public ActionResult AddCategory(Category item)
        {

            item.IsActive = true;
            var token = HttpContext.Session.GetString("accessToken");
            var client = new RestClient("https://localhost:44351/api/categories/add");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {token}");
            var content = JsonConvert.SerializeObject(item);
            request.AddJsonBody(content);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<ServiceRoot<string>>(response.Content);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("AddCategory", "Category");

        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {

            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Category> helper= new HelperRestApi<Category>();
            var result = helper.ConnectRestApiForList("https://localhost:44351/api/categories/getall", token);

            if (result.Success)
            {

                List<SelectListItem> valueRoles = (from x in result.Data
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
                ViewBag.vlc = valueRoles;
            }

            HelperRestApi<Category> helperForCategory = new HelperRestApi<Category>();
            var resultForUser = helperForCategory.ConnectRestApi(String.Format("https://localhost:44351/api/categories/getbyid?id={0}", id), token);

            if (resultForUser.Success)
            {
                return View(resultForUser.Data);
            }

            return RedirectToAction("Index", "Category");

        }

        [HttpPost]
        public ActionResult EditCategory(Category item)
        {
            var token = HttpContext.Session.GetString("accessToken");
            var client = new RestClient("https://localhost:44351/api/categories/update");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {token}");
            var content = JsonConvert.SerializeObject(item);
            request.AddJsonBody(content);
            IRestResponse responseFromAuthLogin = client.Execute(request);
            var result = JsonConvert.DeserializeObject<ServiceRoot<string>>(responseFromAuthLogin.Content);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditCategory", "Category", item.CategoryId);

        }

        public ActionResult DeleteCategory(int id)
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Product> helperForProduct = new HelperRestApi<Product>();
            var result = helperForProduct.ConnectRestApi(String.Format("https://localhost:44351/api/categories/delete?id={0}", id), token);
            return RedirectToAction("Index");
             
        }
    }
}
