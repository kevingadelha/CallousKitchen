using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capstone.Classes;
using CallousFrontEnd.Models;


namespace CallousFrontEnd.Controllers
{

    public class UserController : Controller
    {
        AccountService.AccountServiceMvcClient Client = new AccountService.AccountServiceMvcClient();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user)
        {

            Client.CreateAccountWithEmail(user.Username, user.Password, user.Email);

            return null;
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
            user.Kitchens = Client.GetKitchens(userSession.Id).ToList();

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
            return PartialView("UserKitchenPartialView", kitchens);
        }

        [HttpGet]
        public ActionResult AddEditKitchenView(KitchenUser kitchenUser)
        {
            ViewBag.UserId = kitchenUser.UserId;
            return PartialView("AddEditKitchenPartial", kitchenUser);
        }
        [HttpPost]
        public void AddEditKitchen(KitchenUser kitchenUser)
        {
            //int userId = ViewBag.UserId;
           // System.Diagnostics.Debug.WriteLine("UserId bag: " + userId);
            System.Diagnostics.Debug.WriteLine("UserId bag: " + kitchenUser.UserId);
        }
    }


}