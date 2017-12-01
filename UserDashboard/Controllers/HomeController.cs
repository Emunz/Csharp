using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UserDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore; //this is magic
using System.Linq;

namespace UserDashboard.Controllers
{
    public class HomeController : Controller
    {
        private DashboardContext _context;

        public HomeController(DashboardContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        // --------------------------HERE IS THE LOGIN AND REG METHODS --------------------------------------------------------------
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel RegisteredUser)
        {
            try
            {
                User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == RegisteredUser.Email);
                if (CurrentUser != null)
                {
                    ViewBag.Error = "This email is already used for a registered user";
                    return View("Index");
                }
            }
            catch
            {
            }

            if (ModelState.IsValid)
            {
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                User NewUser = new User
                {
                    FirstName = RegisteredUser.FirstName,
                    LastName = RegisteredUser.LastName,
                    Email = RegisteredUser.Email,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                NewUser.Password = hasher.HashPassword(NewUser, RegisteredUser.Password);

                _context.Users.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetString("UserName", NewUser.FirstName);
                HttpContext.Session.SetInt32("UserId", NewUser.UserId);

                return RedirectToAction("Dashboard");
            }
            return View("Index");

        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password)
        {
            try
            {
                var CurrentUser = _context.Users.SingleOrDefault(user => user.Email == Email);
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                if (0 != hasher.VerifyHashedPassword(CurrentUser, CurrentUser.Password, Password))
                {
                    HttpContext.Session.SetString("UserName", CurrentUser.FirstName);
                    HttpContext.Session.SetInt32("UserId", CurrentUser.UserId);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.Error = "The password does not match the email";
                    return View("Index");
                }
            }
            catch
            {
                ViewBag.Error = "There is no registered user with this email!";
                return View("Index");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        // ----------------------------- HOME PAGE -------------------------------------------------------------------

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;

            int? UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = UserId;

            return View();
        }

        // ------------------------------------------ Create Objects ----------------------------------------------------

        [HttpGet] // change to HttpPost for the exam
        [Route("creatething")]
        public IActionResult CreateThing()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                TempData["SignIn"] = "Please Register or Login";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Dashboard");
        }

        //---------------------------------------------- Read/Render Pages -----------------------------------------------

        [HttpGet]
        [Route("addthing")]
        public IActionResult AddThing()
        {
            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;

            int? UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = UserId;

            return View();
        }

        // ------------------------------------------ Update Objects ----------------------------------------------------



        // ------------------------------------------- Delete Objects ----------------------------------------------------
    }
}
