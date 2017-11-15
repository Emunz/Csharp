using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore; //this is magic
using System.Linq;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingContext _context;

        public HomeController(WeddingContext context)
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
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;

            int? UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = UserId;

            List<Wedding> AllWeddings = _context.Weddings.Include(h => h.Host).Include(g => g.Guests).ToList();
            ViewBag.AllWeddings = AllWeddings;

            List<Guest> AllGuests = _context.Guests.Include(u => u.WeddingGuest).ToList();
            ViewBag.Guests = AllGuests;
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("addwedding")]
        public IActionResult AddWedding()
        {
            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;
            return View();
        }

        [HttpGet]
        [Route("deletewedding/{WeddingID}")]
        public IActionResult DeleteWedding(int WeddingID)
        {
            Wedding CurrentWedding = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == WeddingID);
            _context.Remove(CurrentWedding);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("wedding/{WeddingID}")]
        public IActionResult SingleWedding(int WeddingID)
        {
            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;

            int? UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = UserId;

            Wedding SingleWedding = _context.Weddings.Include(h => h.Host).SingleOrDefault(wed => wed.WeddingId == WeddingID);
            ViewBag.Wedding = SingleWedding;

            List<Guest> AllGuests = _context.Guests.Include(u => u.WeddingGuest).Where(guest => guest.WeddingId == SingleWedding.WeddingId).ToList();
            ViewBag.Guests = AllGuests;
            return View();
        }

        [HttpGet]
        [Route("attendwedding/{WeddingID}")]
        public IActionResult AttendWedding(int WeddingID)
        {
            Wedding CurrentWedding = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == WeddingID);
            int? UserID = HttpContext.Session.GetInt32("UserId");
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == UserID);

            Guest NewGuest = new Guest
                {
                    WeddingGuestId = CurrentUser.UserId,
                    WeddingGuest = CurrentUser,
                    WeddingId = CurrentWedding.WeddingId,
                    Wedding = CurrentWedding
                };

            _context.Guests.Add(NewGuest);
            _context.SaveChanges();

            return RedirectToAction("SingleWedding", WeddingID);
        }

        [HttpGet]
        [Route("unattendwedding/{WeddingID}")]
        public IActionResult UnattendWedding(int WeddingID)
        {
            int? UserID = HttpContext.Session.GetInt32("UserId");
            Wedding CurrentWedding = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == WeddingID);
            List<Guest> AllGuests = _context.Guests.Where(guest => guest.WeddingId == CurrentWedding.WeddingId).ToList();
            Guest CurrentGuest = AllGuests.SingleOrDefault(u => u.WeddingGuestId == UserID);

            _context.Guests.Remove(CurrentGuest);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [Route("createwedding")]
        public IActionResult CreateWedding(WeddingViewModel RegisteredWedding)
        {
            if (ModelState.IsValid)
            {
                if (RegisteredWedding.Date <= DateTime.Now)
                {
                    string UserName = HttpContext.Session.GetString("UserName");
                    ViewBag.UserName = UserName;
                    ViewBag.Error = "The Wedding Date has to be in the future!";
                    return View("AddWedding");
                }
                else
                {
                    int? CurrentUserId = HttpContext.Session.GetInt32("UserId");
                    User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == (int)CurrentUserId);

                    Wedding NewWedding = new Wedding
                    {
                        WedderOne = RegisteredWedding.WedderOne,
                        WedderTwo = RegisteredWedding.WedderTwo,
                        Date = RegisteredWedding.Date,
                        Address = RegisteredWedding.Address,
                        HostId = CurrentUser.UserId,
                        Host = CurrentUser
                    };

                    _context.Weddings.Add(NewWedding);
                    _context.SaveChanges();
                    // Handle Success
                    return RedirectToAction("Dashboard");
                }
            }

            string UserFirstName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = UserFirstName;
            return View("AddWedding");
        }
    }
}
