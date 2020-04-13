using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CallousFrontEnd.Models;
using AccountService;

namespace CallousFrontEnd.Controllers
{

    public class UserController : Controller
    {
        AccountService.AccountServiceMvcClient Client = new AccountService.AccountServiceMvcClient();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Capstone.Classes.User user)
        {

            int id = Client.CreateAccountWithEmail(user.Username, user.Password, user.Email);
            if (id > 0)
            {
                UserSessionModel userSession = new UserSessionModel { Id = id, Username = user.Username };
                HttpContext.Session.SetInt32("UserId", id);
                HttpContext.Session.SetString("Username", user.Username);
                return AccountView(userSession);
            }
            return RedirectToAction("LoginView");
        }
        [HttpGet]
        public ActionResult CreateUserView()
        {

            return View("CreateUser");
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {

            int id = Client.LoginAccount(login.Username, login.Password);
            if (id != -1)
            {
                UserSessionModel user = new UserSessionModel { Id = id, Username = login.Username };
                HttpContext.Session.SetInt32("UserId", id);
                HttpContext.Session.SetString("Username", login.Username);
                ViewBag.UserSession = user;


                //return RedirectToAction("AccountView", user);
                if (login.Remember)
                {
                    //Setup the cookie
                    HttpContext.Response.Cookies.Append("Username", login.Username);
                    HttpContext.Response.Cookies.Append("Password", login.Password);
                }
                return AccountView(user);
            }
            return RedirectToAction("LoginView");
        }

        [HttpPost]
        public ActionResult AccountView(UserSessionModel userSession)
        {
            System.Diagnostics.Debug.WriteLine("Account View");
            SerializableUser user = Client.GetSerializableUser(userSession.Id);
            ViewBag.UserId = user.Id;
            //user.Kitchens = Client.GetKitchens(userSession.Id).ToList();
            user.Kitchens = Client.GetKitchens(userSession.Id);
            return View("Account", user);
        }
        public ActionResult LoginView()
        {
            if (HttpContext.Request.Cookies.ContainsKey("Username"))
            {
                string username = HttpContext.Request.Cookies["Username"];
                string password = HttpContext.Request.Cookies["Password"];
                return Login(new LoginModel { Username = username, Password = password, Remember = true });

            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            // destory all cookies
            if (HttpContext.Request.Cookies.ContainsKey("Username"))
            {
                HttpContext.Response.Cookies.Delete("Username");
                HttpContext.Response.Cookies.Delete("Password");
            }
            ViewData.Clear();
            return View("Login");

        }


        public ActionResult KitchenPartialView(List<SerializableKitchen> kitchens)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            return PartialView("UserKitchenPartialView", kitchens);
        }

        [HttpGet]
        public ActionResult AddEditKitchenView(KitchenUser kitchenUser)
        {
            ViewBag.UserId = kitchenUser.UserId;
            //return PartialView("AddEditKitchenPartial", kitchenUser);
            UserSessionModel user = new UserSessionModel { Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault(), Username = HttpContext.Session.GetString("Username") };

            return AccountView(user);

        }

        [HttpPost]
        public ActionResult AddEditKitchen(KitchenUser kitchenUser)
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            Client.AddKitchen(userId, kitchenUser.kitchen.Name);
            ViewBag.UserId = userId;

            List<SerializableKitchen> kitchens = Client.GetKitchens(userId).ToList();
            UserSessionModel user = new UserSessionModel
            {
                Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault(),
                Username = HttpContext.Session.GetString("Username")
            };
            return AccountView(user);
            //return KitchenPartialView(kitchens);
        }
        [HttpGet]
        public ActionResult AddEditFoodView(int kId, int fId)
        {
            ViewBag.KitchenId = kId;
            FoodKitchen foodKitchen = new FoodKitchen();
            foodKitchen.KitchenId = kId;

            if (fId != 0)
            {
                foodKitchen.Food = Client.GetFood(fId);
            }
            else
            {
                foodKitchen.Food = new Food();
            }
            return PartialView("AddEditFoodPartial", foodKitchen);
        }
        [HttpPost]
        public ActionResult AddEditFood(FoodKitchen foodKitchen)
        {

            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            ViewBag.UserId = userId;
            if (foodKitchen.Food.Id == 0) // add food
            {
                Client.AddFood(foodKitchen.KitchenId, foodKitchen.Food.Name, (int)foodKitchen.Food.Quantity, foodKitchen.Food.ExpiryDate);
            }
            else // edit food
            {
                Client.EditFood(foodKitchen.Food.Id, foodKitchen.Food.Name, (int)foodKitchen.Food.Quantity, foodKitchen.Food.ExpiryDate);
            }



            UserSessionModel user = new UserSessionModel
            {
                Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault(),
                Username = HttpContext.Session.GetString("Username")
            };
            return AccountView(user);
            //List<SerializableKitchen> kitchens = Client.GetKitchens(userId).ToList();
            //return KitchenPartialView(kitchens);
        }

        [HttpGet]
        public ActionResult EatFoodView(int fId)
        {
            var food = Client.GetFood(fId);

            return PartialView("EatFoodPartial", food);
        }

        [HttpPost]
        public ActionResult EatFood(Food food)
        {
            if (food.Quantity >= 0)
            {
                Client.EatFood(food.Id, (int)food.Quantity);
            }
            else
            {
                Client.RemoveItem(food.Id);
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            //List<SerializableKitchen> kitchens = Client.GetKitchens((int)ViewBag.UserId).ToList();
            //return KitchenPartialView(kitchens);

            UserSessionModel user = new UserSessionModel
            {
                Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault(),
                Username = HttpContext.Session.GetString("Username")
            };
            return AccountView(user);
        }

        [HttpDelete]
        public ActionResult DeleteFood(int fId)
        {
            Client.RemoveItem(fId);
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            List<SerializableKitchen> kitchens = Client.GetKitchens((int)ViewBag.UserId).ToList();

            return KitchenPartialView(kitchens);
        }
    }


}