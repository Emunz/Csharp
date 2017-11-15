using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using IssaBankAccount.Models;
using System.Linq;

namespace IssaBankAccount.Controllers
{
    public class HomeController : Controller
    {
        private UsersContext _context;

        public HomeController(UsersContext context)
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
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User NewUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };

                _context.Add(NewUser);
                _context.SaveChanges();
                return RedirectToAction("Account");
            }
            return View("Index", model);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User RetrievedUser = _context.users.SingleOrDefault(user => user.Email == model.Email);
                if(RetrievedUser.Password == model.Password)
                {
                    return RedirectToAction("Account");
                }
            }
            return View("Index", model);
        }

        [HttpGet]
        [Route("account")]
        public IActionResult Account()
        {
            return View();
        }
    }
}
