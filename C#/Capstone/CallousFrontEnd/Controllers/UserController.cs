using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capstone.Classes;
using DocumentFormat.OpenXml.InkML;

namespace CallousFrontEnd.Controllers
{
    
    public class UserController : Controller
    {
        AccountService.AccountServiceMvcClient Client = new AccountService.AccountServiceMvcClient();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user) {

            Client.CreateAccountWithEmail(user.Username, user.Password, user.Email);

            return null;
        }
        [HttpGet]
        public ActionResult CreateUserView() {
            return View("CreateUser");
        }

        public ActionResult Login(string username, string password) {
            Client.LoginAccount(username, password);
            return null;
        }

        public async Task<ActionResult> AddFoodAsync(int kitchenId, string name, int count, DateTime? expDate) {
            await Client.AddFoodAsync(kitchenId, name, count, expDate);
            return null;
        }
        public async Task<ActionResult> GetFoodAsync(int id) {
            await Client.GetKitchensAsync(id);
            return null;
        }
    }
}