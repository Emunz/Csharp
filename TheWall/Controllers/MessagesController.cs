using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace LoginRegistration.Controllers
{
    public class MessagesController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public MessagesController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpPost]
        [Route("createmessage")]
        public IActionResult CreateMessage(Message message)
        {
            if(ModelState.IsValid) {
                return RedirectToAction("Wall", "Users");
            }

            return RedirectToAction("Wall", "Users", message);
        }
    }
}
