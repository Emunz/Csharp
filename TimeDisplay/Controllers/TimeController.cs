using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
 
namespace TimeDisplay.Controllers
{
    public class TimeController : Controller
    {
        [HttpGetAttribute]
        
        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            string CurrentDateAndTime = DateTime.Now.ToString("MMM dd, yyyy, hh:mm tt");
            ViewBag.message = CurrentDateAndTime;
            return View();
        }
    }
}
