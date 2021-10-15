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
    public class ProductController : Controller
    {
        HelperAPI _api = new HelperAPI();
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Product> helper = new HelperRestApi<Product>();
            var result = helper.ConnectRestApiForList("https://localhost:44351/api/products/getallwithcategory", token);

            if (!result.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(result.Data);
        }


        [HttpGet]
        public ActionResult AddProduct()
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Category> helperForCategory = new HelperRestApi<Category>();
            var result = helperForCategory.ConnectRestApiForList("https://localhost:44351/api/categories/getall", token);

            if (result.Success)
            { 
                List<SelectListItem> valueCategory = (from x in result.Data
                                                select new SelectListItem
                                                {
                                                    Text = x.CategoryName,
                                                    Value = x.CategoryId.ToString()
                                                }).ToList();
                ViewBag.vlc = valueCategory;
            }

              
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product item)
        {
            item.IsActive = true;
            var token = HttpContext.Session.GetString("accessToken");
            var client = new RestClient("https://localhost:44351/api/products/add");
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
            return RedirectToAction("AddProduct", "Product");

        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Category> helperForCategory = new HelperRestApi<Category>();
            var result = helperForCategory.ConnectRestApiForList("https://localhost:44351/api/categories/getall", token);

            if (result.Success)
            {

                List<SelectListItem> valueCategory = (from x in result.Data
                                                      select new SelectListItem
                                                      {
                                                          Text = x.CategoryName,
                                                          Value = x.CategoryId.ToString()
                                                      }).ToList();
                ViewBag.vlc = valueCategory;
            }

            HelperRestApi<Product> helperForProduct = new HelperRestApi<Product>();
            var resultForProduct = helperForProduct.ConnectRestApi(String.Format("https://localhost:44351/api/products/getwithcategorybyid?id={0}", id), token);

            if (resultForProduct.Success)
            { 
                return View(resultForProduct.Data);
            }

            return RedirectToAction("Index","Product");
             
        }

        [HttpPost]
        public ActionResult EditProduct(Product item)
        {
            var token = HttpContext.Session.GetString("accessToken");
            var client = new RestClient("https://localhost:44351/api/products/update");
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
            return RedirectToAction("EditProduct", "Product",item.ProductId);

        }

        public ActionResult DeleteProduct(int id)
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Product> helperForProduct = new HelperRestApi<Product>();
            var result = helperForProduct.ConnectRestApi(String.Format("https://localhost:44351/api/products/delete?id={0}", id), token);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("urun/{category}/{name}/{id:int}")]
        public ActionResult ProductDetail(int id)
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Product> helperForProduct = new HelperRestApi<Product>();
            var result = helperForProduct.ConnectRestApiForList(String.Format("https://localhost:44351/api/products/getallwithcategory"), token);

            if (result.Success)
            {
                return View(result.Data.Where(x => x.ProductId == id).FirstOrDefault());
            }

            return RedirectToAction("Index", "Product");

        }
    }
}
