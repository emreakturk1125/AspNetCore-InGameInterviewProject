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
    public class UserController : Controller
    {
        HelperAPI _api = new HelperAPI();
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<User> helper = new HelperRestApi<User>();
            var result = helper.ConnectRestApiForList("https://localhost:44351/api/users/getall", token);

            if (!result.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(result.Data);
            
        }


        [HttpGet]
        public ActionResult AddUser()
        {
            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Role> helperForRole = new HelperRestApi<Role>();
            var result = helperForRole.ConnectRestApiForList("https://localhost:44351/api/roles/getall", token);

            if (result.Success)
            {
                List<SelectListItem> valueRoles= (from x in result.Data
                                                      select new SelectListItem
                                                      {
                                                          Text = x.RoleName,
                                                          Value = x.RoleId.ToString()
                                                      }).ToList();
                ViewBag.vlc = valueRoles;
            }


            return View();
             
        }

        [HttpPost]
        public ActionResult AddUser(User item)
        {

            item.IsActive = true;
            var token = HttpContext.Session.GetString("accessToken");
            var client = new RestClient("https://localhost:44351/api/users/add");
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
            return RedirectToAction("AddUser", "User");
             
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {

            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Role> helperForRole = new HelperRestApi<Role>();
            var result = helperForRole.ConnectRestApiForList("https://localhost:44351/api/roles/getall", token);

            if (result.Success)
            {

                List<SelectListItem> valueRoles = (from x in result.Data
                                                      select new SelectListItem
                                                      {
                                                          Text = x.RoleName,
                                                          Value = x.RoleId.ToString()
                                                      }).ToList();
                ViewBag.vlc = valueRoles;
            }

            HelperRestApi<User> helperForUser = new HelperRestApi<User>();
            var resultForUser = helperForUser.ConnectRestApi(String.Format("https://localhost:44351/api/users/getbyid?id={0}", id), token);

            if (resultForUser.Success)
            {
                return View(resultForUser.Data);
            }

            return RedirectToAction("Index", "User");

        }

        [HttpPost]
        public ActionResult EditUser(User item)
        {
            var token = HttpContext.Session.GetString("accessToken");
            var client = new RestClient("https://localhost:44351/api/users/update");
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
            return RedirectToAction("EditUser", "User", item.UserId);
             
        }

        public ActionResult DeleteUser(int id)
        {

            var token = HttpContext.Session.GetString("accessToken");
            HelperRestApi<Product> helperForProduct = new HelperRestApi<Product>();
            var result = helperForProduct.ConnectRestApi(String.Format("https://localhost:44351/api/users/delete?id={0}", id), token);
            return RedirectToAction("Index");
 
        }
    }
}
