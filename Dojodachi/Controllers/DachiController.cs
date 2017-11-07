using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace Dojodachi.Controllers
{
    public class DachiController : Controller
    {
        [HttpGetAttribute]


        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("Happiness") == null){
                HttpContext.Session.SetInt32("Happiness", 20);
                HttpContext.Session.SetInt32("Fullness", 20);
                HttpContext.Session.SetInt32("Energy", 50);
                HttpContext.Session.SetInt32("Meals", 3);
                HttpContext.Session.SetString("Message", "You got a new pet! What would you like to do?");
            }
                
            int? HappinessVariable = HttpContext.Session.GetInt32("Happiness");
            int? FullnessVariable = HttpContext.Session.GetInt32("Fullness");
            int? EnergyVariable = HttpContext.Session.GetInt32("Energy");
            int? MealsVariable = HttpContext.Session.GetInt32("Meals");
            string MessageVariable = HttpContext.Session.GetString("Message");

            ViewBag.Happiness = HappinessVariable;
            ViewBag.Fullness = FullnessVariable;
            ViewBag.Energy = EnergyVariable;
            ViewBag.Meals = MealsVariable;
            ViewBag.Message = MessageVariable;
            ViewBag.Restart = "No";

            if(HappinessVariable == 0 || FullnessVariable == 0 || EnergyVariable == 0){
                HttpContext.Session.SetString("Message", "Oh no! Your pet has passed away! Would you like to restart?");
                MessageVariable = HttpContext.Session.GetString("Message");
                ViewBag.Message = MessageVariable;
                ViewBag.Restart = "Yes";
            }
            if(HappinessVariable >= 100 && FullnessVariable >= 100 && EnergyVariable >= 100){
                HttpContext.Session.SetString("Message", "Wow! You Won! Your pet is doing GREAT!");
                MessageVariable = HttpContext.Session.GetString("Message");
                ViewBag.Message = MessageVariable;
                ViewBag.Restart = "Yes";
            }
            return View();
        }

        Random rnd = new Random();

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            // Feeding your Dojodachi costs 1 meal and gains a random amount of fullness between 5 and 10 (you cannot feed your Dojodachi if you do not have meals)
            int? FullnessVariable = HttpContext.Session.GetInt32("Fullness");
            int? MealsVariable = HttpContext.Session.GetInt32("Meals");
            int LikeFeeding = rnd.Next(0,101);

            if(MealsVariable > 0){
                MealsVariable -= 1;
                if(LikeFeeding > 25){
                    int RandomFullness = rnd.Next(5,11);
                    int NewFullness =  RandomFullness + (int)FullnessVariable;
                    HttpContext.Session.SetInt32("Fullness", NewFullness);
                    HttpContext.Session.SetInt32("Meals", (int)MealsVariable);
                    HttpContext.Session.SetString("Message", $"You fed your pet! It gained {RandomFullness} Fullness!");
                } else {
                    HttpContext.Session.SetInt32("Meals", (int)MealsVariable);
                    HttpContext.Session.SetString("Message", "Your pet didn't want to eat!");
                } 
            } else if (MealsVariable == 0){
                HttpContext.Session.SetString("Message", "You are out of meals! Try working for some!");
            }
        
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            // Playing with your Dojodachi costs 5 energy and gains a random amount of happiness between 5 and 10
            int? HappinessVariable = HttpContext.Session.GetInt32("Happiness");
            int? EnergyVariable = HttpContext.Session.GetInt32("Energy");
            int LikePlaying = rnd.Next(0,101);

            if(EnergyVariable >=5){
                EnergyVariable -= 5;
                if(LikePlaying > 25){
                    int RandomHappiness = rnd.Next(5,11);
                    int NewHappiness =  RandomHappiness + (int)HappinessVariable;
                    HttpContext.Session.SetInt32("Happiness", NewHappiness);
                    HttpContext.Session.SetInt32("Energy", (int)EnergyVariable);
                    HttpContext.Session.SetString("Message", $"Your pet had fun playing! It gained {RandomHappiness} Happiness!");
                } else {
                    HttpContext.Session.SetInt32("Energy", (int)EnergyVariable);
                    HttpContext.Session.SetString("Message", "Your pet didn't want to play right now!");
                } 
            } else {
                HttpContext.Session.SetString("Message", "Your pet doesn't have enough energy to play! Try sleeping to recharge!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            // Sleeping earns 15 energy and decreases fullness and happiness each by 5
            int? HappinessVariable = HttpContext.Session.GetInt32("Happiness");
            int? FullnessVariable = HttpContext.Session.GetInt32("Fullness");
            int? EnergyVariable = HttpContext.Session.GetInt32("Energy");
            if(HappinessVariable >= 5 && FullnessVariable >= 5){
                HappinessVariable -= 5;
                FullnessVariable -= 5;
                int NewEnergy =  15 + (int)EnergyVariable;
                HttpContext.Session.SetInt32("Happiness",(int)HappinessVariable);
                HttpContext.Session.SetInt32("Fullness", (int)FullnessVariable);
                HttpContext.Session.SetInt32("Energy", NewEnergy);
                HttpContext.Session.SetString("Message", $"Your pet gained 15 Energy from sleeping! But it lost 5 Fullness and 5 Happiness");
            } else {
                HttpContext.Session.SetString("Message", "Your pet doesn't have enough fullness and happiness to sleep! Try feeding and playing with it!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            // Working costs 5 energy and earns between 1 and 3 meals
            int? EnergyVariable = HttpContext.Session.GetInt32("Energy");
            int? MealsVariable = HttpContext.Session.GetInt32("Meals");

            if(EnergyVariable >= 5){
                EnergyVariable -= 5;
                int RandomMeals = rnd.Next(1,4);
                int NewMeals =  RandomMeals + (int)MealsVariable;
                HttpContext.Session.SetInt32("Meals", NewMeals);
                HttpContext.Session.SetInt32("Energy", (int)EnergyVariable);
                HttpContext.Session.SetString("Message", $"Your pet earned {RandomMeals} meals from working!");
            } else {
                HttpContext.Session.SetString("Message", "Your pet doesn't have enough energy to work! Try sleeping to recharge!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("restart")]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // 20 happiness, 20 fullness, 50 energy, and 3 meals
    }
}