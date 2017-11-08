using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace LoginRegistration.Controllers
{
    public class UsersController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public UsersController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid){
                try 
                {
                    List<Dictionary<string, object>> RegisteredUser = _dbConnector.Query($"SELECT * FROM users WHERE email='{user.Email}'");
                    var CurrentUser = RegisteredUser[0];
                    if((string)CurrentUser["email"] == user.Email){
                        ViewBag.Error = "This email already belongs to a user!";
                        return View("Index");
                    }
                }
                catch 
                {
                    _dbConnector.Execute($"INSERT INTO users (first_name, last_name, email, password) VALUES('{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Password}')");

                    List<Dictionary<string, object>> CurrentUser = _dbConnector.Query($"SELECT * FROM users WHERE email='{user.Email}'");
                    
                    HttpContext.Session.SetString("UserName", user.FirstName);
                    ViewBag.UserName = user.FirstName;

                    return View("Success");
                }
                
            }

            return View("Index", user);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            if(ModelState.IsValid){
                List<Dictionary<string, object>> RegisteredUser = _dbConnector.Query($"SELECT * FROM users WHERE email='{email}'");
                var user = RegisteredUser[0];
                if((string)user["email"] == email && (string)user["password"] == password){
                    ViewBag.UserName = (string)user["first_name"];
                    return View("Success");
                }
                else {
                    ViewBag.Error = "The information does not match any registered users!";
                    return View("Index");
                } 
            }

            return View("Index");
        }
    }
}
