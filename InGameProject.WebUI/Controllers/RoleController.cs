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
    [Authorize(Roles = "product_view")]
    public class RoleController : Controller
    {
        HelperAPI _api = new HelperAPI();
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Role> helperForProduct = new HelperRestApi<Role>();
            var result = helperForProduct.ConnectRestApiForList("https://localhost:44351/api/roles/getall", token);

            if (!result.Success)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(result.Data);
        }


        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(Role item)
        {

            item.IsActive = true;
            var token = HttpContext.Session.GetString("accessToken");
            var client = new RestClient("https://localhost:44351/api/roles/add");
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
            return RedirectToAction("AddRole", "Role");

        }

        [HttpGet]
        public ActionResult EditRole(int id)
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Role> helperForProduct = new HelperRestApi<Role>();
            var resultForProduct = helperForProduct.ConnectRestApi(String.Format("https://localhost:44351/api/roles/getbyid?id={0}", id), token);

            if (resultForProduct.Success)
            {
                return View(resultForProduct.Data);
            }

            return RedirectToAction("Index", "Role");
             
        }

        [HttpPost]
        public ActionResult EditRole(Role item)
        {
            var token = HttpContext.Session.GetString("accessToken");
            var client = new RestClient("https://localhost:44351/api/roles/update");
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
            return RedirectToAction("EditRole", "Role", item.RoleId);

        }

        public ActionResult DeleteRole(int id)
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Product> helperForProduct = new HelperRestApi<Product>();
            var result = helperForProduct.ConnectRestApi(String.Format("https://localhost:44351/api/roles/delete?id={0}", id), token);
            return RedirectToAction("Index");
        }
    }
}
