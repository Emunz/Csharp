using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FormSubmission.Models;

namespace FormSubmission.Controllers
{
    public class UsersController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(string FirstName, string LastName, int Age, string Email, string Password)
        {
            User NewUser = new User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Age = Age,
                    Email = Email,
                    Password = Password,
                };
            
            if(TryValidateModel(NewUser) == false){
                ViewBag.ModelFields = ModelState.Values;
                return View("Index");
            } else {
                return RedirectToAction("Success");
            }
        }
    }
}