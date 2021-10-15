using FluentValidation.Results;
using InGameProject.Business.ValidationRules;
using InGameProject.Entities.Concrete;
using InGameProject.WebUI.Helpers;
using InGameProject.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InGameProject.WebUI.Controllers
{
    public class LoginController : Controller
    {
        HelperAPI _api = new HelperAPI();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserForLogin item)
        {

            UserValidator adminValidator = new UserValidator();
            ValidationResult resultValidation = adminValidator.Validate(item.User);

            if (resultValidation.IsValid)
            {

                var client = new RestClient("https://localhost:44351/api/auth/login");
                var requestToAuthLogin = new RestRequest(Method.POST);
                requestToAuthLogin.AddHeader("content-type", "application/json");
                requestToAuthLogin.AddJsonBody(new { useremail = item.User.UserEmail, userpassword = item.User.UserPassword });
                IRestResponse responseFromAuthLogin = client.Execute(requestToAuthLogin);
                var resultFromAuthLogin = JsonConvert.DeserializeObject<ServiceRoot<string>>(responseFromAuthLogin.Content);

                if (resultFromAuthLogin.Success)
                {

                    HelperRestApi<User> helperForProduct = new HelperRestApi<User>();
                    var resultFromUsers = helperForProduct.ConnectRestApi(String.Format("https://localhost:44351/api/users/getbyemail?email={0}", item.User.UserEmail), resultFromAuthLogin.Data);

                    HttpContext.Session.SetString("accessToken", resultFromAuthLogin.Data); // Token Session

                    if (resultFromUsers.Success)
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,$"{resultFromUsers.Data.UserName} {resultFromUsers.Data.UserSurname}"),
                        new Claim(ClaimTypes.Role,resultFromUsers.Data.UserRole.RoleName)
                    };

                        var identity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme
                            );

                        var principle = new ClaimsPrincipal(identity);
                        var props = new AuthenticationProperties();
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, props).Wait();

                    }

                    var cookieOptions = new CookieOptions();
                    if (item.IsRemember)
                    {
                        cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddHours(1)
                        };

                    }
                    else
                    {
                        cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(-1)
                        };

                    }
                    Response.Cookies.Append("email", item.User.UserEmail, cookieOptions);
                    Response.Cookies.Append("password", item.User.UserPassword, cookieOptions);

                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in resultValidation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult ForgetPassword(string email)
        {

            HelperRestApi<string> helperForProduct = new HelperRestApi<string>();
            var result= helperForProduct.ConnectRestApi(String.Format("https://localhost:44351/api/auth/newpassword?email={0}", email), null);

            if (!result.Success)
            {
                return RedirectToAction("ForgetPassword", "Login");
            }

            HelperMailSender.MailSender(email,result.Data);
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }


}
