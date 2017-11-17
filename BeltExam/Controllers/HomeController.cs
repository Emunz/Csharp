using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore; //this is magic
using System.Linq;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private BeltContext _context;

        public HomeController(BeltContext context)
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
                    UpdatedAt = DateTime.Now,
                    Description = RegisteredUser.Description
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
            int? UserId = HttpContext.Session.GetInt32("UserId");
            User CurrentUser = _context.Users.SingleOrDefault(u => u.UserId == UserId);
            ViewBag.User = CurrentUser;

            if(UserId == null)
            {
                TempData["SignIn"] = "Please Register or Login";
                return RedirectToAction("Index");
            }

            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;

            List<Connection> AllConnections = _context.Connections.Include(c => c.Sender).Where(c => c.ReceiverId == UserId).ToList();
            List<Connection> AllFriends = AllConnections.Where(c => c.ConnectionStatus == 2).ToList();
            ViewBag.Friends = AllFriends;
            ViewBag.AllConnections = AllConnections;

            List<User> AllUsers = _context.Users.Where(u => u.UserId != CurrentUser.UserId).ToList();
            ViewBag.AllUsers = AllUsers;

            return View();
        }

        // ------------------------------------------ Create Objects ----------------------------------------------------

        [HttpGet] // change to HttpPost for the exam
        [Route("createconnection/{UserID}")]
        public IActionResult CreateConnection(int UserID)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            User CurrentUser = _context.Users.SingleOrDefault(u => u.UserId == UserId);
            
            Connection NewConnection = new Connection
                {
                    ConnectionStatus = 1,
                    SenderId = (int)UserId,
                    Sender = CurrentUser,
                    ReceiverId = UserID  
                };
            
            _context.Connections.Add(NewConnection);
            _context.SaveChanges();         

            TempData["InviteSent"] = "Your invitation was successfully sent!";


            return RedirectToAction("Dashboard");
        }

        //---------------------------------------------- Read/Render Pages -----------------------------------------------

        [HttpGet]
        [Route("profile/{UserID}")]
        public IActionResult YourProfile(int UserID)
        {
            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;

            int? UserId = HttpContext.Session.GetInt32("UserId");
            User CurrentUser = _context.Users.SingleOrDefault(u => u.UserId == UserId);
            ViewBag.User = CurrentUser;
            if(UserId == null)
            {
                TempData["SignIn"] = "Please Register or Login";
                return RedirectToAction("Index");
            }

            List<Connection> AllConnections = _context.Connections.Include(c => c.Sender).Where(c => c.ReceiverId == UserId).ToList();
            List<Connection> AllInvites = AllConnections.Where(c => c.ConnectionStatus == 1).ToList();
            List<Connection> AllFriends = AllConnections.Where(c => c.ConnectionStatus == 2).ToList();
            ViewBag.Invitations = AllInvites;
            ViewBag.Friends = AllFriends;

            return View();
        }

        [HttpGet]
        [Route("user/{UserID}")]
        public IActionResult UserProfile(int UserID)
        {
            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;

            int? UserId = HttpContext.Session.GetInt32("UserId");
            User CurrentUser = _context.Users.SingleOrDefault(u => u.UserId == UserID);
            ViewBag.User = CurrentUser;

            return View();
        }

        // ------------------------------------------ Update Objects ----------------------------------------------------

        [HttpGet]
        [Route("acceptinvitation/{UserID}")]
        public IActionResult AcceptInvitation(int UserID)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");

            List<Connection> AllConnections = _context.Connections.Include(c => c.Sender).Where(c => c.ReceiverId == UserId).ToList();
            Connection AcceptConnection = AllConnections.FirstOrDefault(c => c.SenderId == UserID);
            AcceptConnection.ConnectionStatus = 2;
            _context.SaveChanges();  
            return RedirectToAction("YourProfile", UserId);
        }

        // ------------------------------------------- Delete Objects ----------------------------------------------------

        [HttpGet]
        [Route("deleteinvitation/{UserID}")]
        public IActionResult DeleteInvitation(int UserID)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            
            List<Connection> AllConnections = _context.Connections.Include(c => c.Sender).Where(c => c.ReceiverId == UserId).ToList();
            Connection DeleteConnection = AllConnections.FirstOrDefault(c => c.SenderId == UserID);
            _context.Remove(DeleteConnection);
            _context.SaveChanges();  

            return RedirectToAction("YourProfile", UserId);
        }
    }
}
