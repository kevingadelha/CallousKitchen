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
            return View("Login");
        }
        public async Task<ActionResult> AddFoodAsync(int kitchenId, string name, int count, DateTime? expDate)
        {
            await Client.AddFoodAsync(kitchenId, name, count, expDate);
            return null;
        }
        public async Task<ActionResult> GetFoodAsync(int id)
        {
            await Client.GetKitchensAsync(id);
            return null;
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
            return PartialView("AddEditKitchenPartial", kitchenUser);
        }

        [HttpPost]
        public ActionResult AddEditKitchen(KitchenUser kitchenUser)
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            Client.AddKitchen(userId, kitchenUser.kitchen.Name);
            ViewBag.UserId = userId;

            List<SerializableKitchen> kitchens = Client.GetKitchens(userId).ToList();
            // return PartialView("UserKitchenPartialView", kitchens);
            return AccountView(new UserSessionModel { Id = userId, Username = HttpContext.Session.GetString("Username") });

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

            return AccountView(new UserSessionModel { Id = userId, Username = HttpContext.Session.GetString("Username") });
        }
    }


}