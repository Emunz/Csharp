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

        [HttpGet]
        [Route("wall")]
        public IActionResult Wall()
        {
            List<Dictionary<string, object>> AllMessages = _dbConnector.Query($"SELECT * FROM messages");
            List<Dictionary<string, object>> AllComments = _dbConnector.Query($"SELECT * FROM comments");

            ViewBag.UserName = HttpContext.Session.GetString("User_Name");
            return View();
        }

        [HttpPost]
        [Route("createmessage")]
        public IActionResult CreateMessage(Message message)
        {
            if(ModelState.IsValid) {
                return RedirectToAction("Wall");
            }

            return View("Wall", message);
        }

        [HttpPost]
        [Route("createcomment")]
        public IActionResult CreateComment(Comment comment)
        {
            if(ModelState.IsValid) {
                return RedirectToAction("Wall");
            }

            return View("Wall", comment);
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
                    var Current_User = CurrentUser[0];
                    
                    HttpContext.Session.SetString("User_Name", user.FirstName);
                    HttpContext.Session.SetInt32("User_ID", (int)Current_User["id"]);

                    return RedirectToAction("Wall");
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
                    HttpContext.Session.SetString("User_Name", (string)user["first_name"]);
                    HttpContext.Session.SetInt32("User_ID", (int)user["id"]);
                    return RedirectToAction("Wall");
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
