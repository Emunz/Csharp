using System;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace RandomPasscode.Controllers
{
    public class PasscodeController : Controller
    {
        private Random random = new Random();
        private string CreatePasscode(){
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 15)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGetAttribute]
        
        [HttpGet]
        [Route("")]
        public IActionResult index(){
            if(HttpContext.Session.GetInt32("count") == null){
                HttpContext.Session.SetInt32("count", 1);
                ViewBag.passcode = CreatePasscode();
                ViewBag.count = HttpContext.Session.GetInt32("count");
            } else {
                //if the page is refreshed instead of the button pressed
                int? count = HttpContext.Session.GetInt32("count");

            if ((int)count >= 1)
            {
                HttpContext.Session.SetInt32("count", ((int)++count));
                ViewBag.passcode = CreatePasscode();
                ViewBag.count = HttpContext.Session.GetInt32("count");
            }
            }
            return View();
        }
        [HttpGet]
        [Route("generate")]
        public IActionResult generator(){
            int? count = HttpContext.Session.GetInt32("count");

            if ((int)count >= 1)
            {
                HttpContext.Session.SetInt32("count", ((int)++count));
                ViewBag.passcode = CreatePasscode();
                ViewBag.count = HttpContext.Session.GetInt32("count");
            }
            return RedirectToAction(actionName: "Index");
        }
    }
}
