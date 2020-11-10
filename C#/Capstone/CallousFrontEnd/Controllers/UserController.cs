using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CallousFrontEnd.Models;
using AccountService;
using System.Diagnostics;
//using AccountServiceOther;

namespace CallousFrontEnd.Controllers
{

    public class UserController : Controller
    {

            AccountService.AccountServiceMvcClient Client = new AccountService.AccountServiceMvcClient();
        // AccountServiceOther.AccountServiceMvcClient Client = new AccountServiceMvcClient();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Capstone.Classes.User user)
        {
            
            SerializableUser serializableUser = Client.CreateAccount(user.Email, user.Password);
            if (serializableUser.Id > 0)
            {
                UserSessionModel userSession = new UserSessionModel { Id = serializableUser.Id, Username = user.Email };
                HttpContext.Session.SetInt32("UserId", serializableUser.Id);
                HttpContext.Session.SetString("Username", user.Email);
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

            SerializableUser serializableUser = Client.LoginAccount(login.Username, login.Password);
            if (serializableUser.Id != -1)
            {
                UserSessionModel user = new UserSessionModel { Id = serializableUser.Id, Username = login.Username };
                HttpContext.Session.SetInt32("UserId", serializableUser.Id);
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

        [HttpPost]
        public ActionResult AccountView(UserSessionModel userSession)
        {
            System.Diagnostics.Debug.WriteLine("Account View");
            TempData["userId"] = userSession.Id;
            return RedirectToAction("HomeView");
        }

        public ActionResult HomeView()
        {
            if (TempData["userId"] != null)
            {
                int userId = (int)TempData["userId"];
                TempData.Keep();
                SerializableUser user = Client.GetSerializableUser(userId);
                ViewBag.UserId = user.Id;
                //user.Kitchens = Client.GetKitchens(userSession.Id).ToList();
                user.Kitchens = Client.GetKitchens(userId);
                return View("Account", user);
            }
            else
            {
                return View("Login");
            }
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
            return RedirectToAction("LoginView");

        }


        // Author Peter Szadurski
        public ActionResult KitchenPartialView(List<SerializableKitchen> kitchens)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();

            ViewBag.StoragesList = Client.GetStorages();
            KitchenModel kM = new KitchenModel();
            kM.Kitchens = kitchens;
            kM.Storages = Client.GetStorages().ToList();
            


            return PartialView("UserKitchenPartialView", kM);
        }

        [HttpPost]
        public ActionResult AddEditKitchen(KitchenUser kitchenUser)
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            try
            {
                Client.AddKitchen(userId, kitchenUser.kitchen.Name);
                ViewBag.UserId = userId;

                List<SerializableKitchen> kitchens = Client.GetKitchens(userId).ToList();
            }
            catch
            {
                Debug.WriteLine("eat failed");
            }
            UserSessionModel user = new UserSessionModel
            {
                Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault(),
                Username = HttpContext.Session.GetString("Username")
            };
            return AccountView(user);
            //return KitchenPartialView(kitchens);
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
        public ActionResult AddEditFood(FoodKitchen foodKitchen)
        {

            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            ViewBag.UserId = userId;
            

            try
            {
                if (foodKitchen.Food.Id == 0) // add food
                {
                    // FIX THIS
                    string[] Ingredients = {};
                    string[] Traces = { };
                    Client.AddFoodComplete(foodKitchen.KitchenId, foodKitchen.Food.Name,foodKitchen.Food.Storage.ToString() , foodKitchen.Food.ExpiryDate, (int)foodKitchen.Food.Quantity, foodKitchen.Food.QuantityClassifier, -1,-1, Ingredients, Traces, foodKitchen.Food.Favourite);
                }
                else // edit food
                {
                    Client.EditFood(foodKitchen.Food.Id, foodKitchen.Food.Name, (int)foodKitchen.Food.Quantity, foodKitchen.Food.ExpiryDate);
                }
            }
            catch
            {
                Debug.WriteLine("eat failed");
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
        public ActionResult RecipeSearchView()
        {
            int Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            int kId = Client.GetKitchens(Id).FirstOrDefault().Id;
            ViewBag.KitchenId = kId;

            return PartialView("_RecipeSearchPartial");
        }

        [HttpPost]
        public ActionResult SearchRecipes(string search)
        {


            search = System.Web.HttpUtility.UrlEncode(search);
            int Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            SerializableRecipeModel[] recs = Client.SearchRecipesUser(search, 5, Id);

            return PartialView("_RecipeResultView", recs);
        }

        [HttpGet]
        public ActionResult AddEditFoodView(int kId, int fId)
        {
            ViewBag.KitchenId = kId;
            FoodKitchen foodKitchen = new FoodKitchen();
            foodKitchen.KitchenId = kId;
            ViewBag.StorageTypesList = Client.GetStorages();


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
        public ActionResult EatFood(Food food)
        {
            try
            {
                if (food.Quantity >= 0)
                {
                    Client.EatFood(food.Id, (int)food.Quantity);
                }
                else
                {
                    Client.RemoveItem(food.Id);
                }
            }
            catch
            {
                Debug.WriteLine("eat failed");
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

        [HttpGet]
        public ActionResult EatFoodView(int fId)
        {
            var food = Client.GetFood(fId);

            return PartialView("EatFoodPartial", food);
        }

        [HttpDelete]
        public ActionResult DeleteFood(int fId)
        {
            try
            {
                Client.RemoveItem(fId);
            }
            catch
            {
                Debug.WriteLine("remove failed");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            List<SerializableKitchen> kitchens = Client.GetKitchens((int)ViewBag.UserId).ToList();

            return KitchenPartialView(kitchens);
        }

        [HttpGet]
        public string GetBarcodeData(string barcode)
        {
            // chop off leading zeros

          //  barcode = barcode.TrimStart('0');
            string test = Client.GetBarcodeData(barcode);
            return test;
        }
    }


}
