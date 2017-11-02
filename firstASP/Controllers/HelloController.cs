using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace YourNamespace.Controllers
{
    public class HelloController : Controller
    {
        [HttpGetAttribute]

        [HttpGet]
        [Route("{First}/{Last}/{Age}/{Color}")]
        public JsonResult Index(string First, string Last, int Age, string Color)
        {
                var NewPerson = new {
                FirstName = First, 
                LastName = Last,
                CurrentAge = Age, 
                FavoriteColor = Color
            };

            return Json(NewPerson);
        }
    }
}
