using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using restauranter.Models;
using System.Linq;

namespace restauranter.Controllers
{
    public class HomeController : Controller
    {

        private ReviewsContext _context;
 
        public HomeController(ReviewsContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpGet]
        [Route("reviews")]
        public IActionResult Reviews()
        {
            List<Review> AllReviews = _context.reviews.ToList();
            ViewBag.Reviews = AllReviews;
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Review newReview)
        {
            if(TryValidateModel(newReview))
            {
                Review createReview = new Review 
                {
                    reviewer_name = newReview.reviewer_name,
                    restaurant_name = newReview.restaurant_name,
                    review = newReview.review ,
                    date = newReview.date,
                    stars = newReview.stars
                };
                if(newReview.date > DateTime.Now)
                {
                    ViewBag.Error= "Your Date of Visit must be in the past";
                    return View("Index");
                }
                else
                {
                    _context.Add(newReview);
                    _context.SaveChanges();
                    return RedirectToAction("Reviews");
                }
                

            }
            ViewBag.Errors = ModelState.Values;
            return View("Index");
            
        }
    }
}
