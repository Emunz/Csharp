using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace LoginRegistration.Controllers
{
    public class CommentsController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public CommentsController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        // GET: /Home/
        

        [HttpPost]
        [Route("createcomment")]
        public IActionResult CreateComment(Comment comment)
        {
            if(ModelState.IsValid) {
                return RedirectToAction("Wall", "Users");
            }

            return RedirectToAction("Wall", "Users", comment);
        }
    }
}
