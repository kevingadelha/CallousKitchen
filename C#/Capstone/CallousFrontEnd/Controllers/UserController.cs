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
        public ActionResult CreateUser(Capstone.Classes.User user, IFormCollection form)
        {
            List<string> allergiesList = new List<string>();

            foreach (var a in Allergies.GetAllergies())
            {
                if (form[a] == "on")
                {
                    allergiesList.Add(a);
                }
            }
            if (form["other"] != "" || form["other"].Count() != 0)
            {
                allergiesList.Add(form["other"]);
            }


            SerializableUser serializableUser = Client.CreateAccount(user.Email, user.Password);
            if (serializableUser.Id > 0)
            {
                Client.EditUserDietaryRestrictions(serializableUser.Id, form["Diet"] == "Vegan", form["Diet"] == "Vegetarian", allergiesList.ToArray());
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

        [HttpGet]
        public ActionResult Settings()
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            ViewBag.UserId = userId;
            var user = Client.GetSerializableUser(userId);
            string[] selected = new string[3];
            if (user.Vegan)
            {
                selected[2] = "checked";
            }
            else if (user.Vegetarian)
            {
                selected[1] = "checked";
            }
            else
            {
                selected[0] = "checked";
            }
            List<string> aM = new List<string>();
            foreach (var a in Allergies.GetAllergies())
            {
                if (user.Allergies.Contains(a))
                {
                    aM.Add("checked");
                }
                else
                {
                    aM.Add("");
                }
            }

            // set other

            if (!Allergies.GetAllergies().Contains(user.Allergies.LastOrDefault()))
            {
                ViewBag.Other = user.Allergies.LastOrDefault();
            }


            ViewBag.Checked = aM;
            ViewBag.Selected = selected;
            return View("Settings", user);
        }

        [HttpPost]
        public ActionResult Settings(SerializableUser user, IFormCollection form)
        {
            ViewBag.UserId = user.Id;

            List<string> allergiesList = new List<string>();

            foreach (var a in Allergies.GetAllergies())
            {
                if (form[a] == "on")
                {
                    allergiesList.Add(a);
                }
            }

            if (form["other"] != "" || form["other"].Count() != 0)
            {
                allergiesList.Add(form["other"]);
            }

            try
            {
                Client.EditUserDietaryRestrictions(user.Id, form["Diet"] == "Vegan", form["Diet"] == "Vegetarian", allergiesList.ToArray());
                ViewBag.Result = "Settings Changed";
            }
            catch
            {
                ViewBag.Result = "There was a problem saving data.";
            }

            user = Client.GetSerializableUser(user.Id);

            // set other
            if (!Allergies.GetAllergies().Contains(user.Allergies.LastOrDefault()))
            {
                ViewBag.Other = user.Allergies.LastOrDefault();
            }

            string[] selected = new string[3];
            if (user.Vegan)
            {
                selected[2] = "checked";
            }
            else if (user.Vegetarian)
            {
                selected[1] = "checked";
            }
            else
            {
                selected[0] = "checked";
            }
            List<string> aM = new List<string>();
            foreach (var a in Allergies.GetAllergies())
            {
                if (user.Allergies.Contains(a))
                {
                    aM.Add("checked");
                }
                else
                {
                    aM.Add("");
                }
            }


            ViewBag.Checked = aM;
            ViewBag.Selected = selected;
            return View("Settings", user);

        }




        [HttpPost]
        public ActionResult Login(LoginModel login)
        {

            SerializableUser serializableUser = Client.LoginAccount(login.Username, login.Password);
            if (serializableUser.Id != -1)
            {
                UserSessionModel user = new UserSessionModel { Id = serializableUser.Id, Username = login.Username };
                System.Diagnostics.Debug.WriteLine("UserId: " + serializableUser.Id);
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
            TempData["userId"] = userSession.Id;
            return RedirectToAction("HomeView");
        }

        public ActionResult KitchenView()
        {
            TempData["userId"] = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            return HomeView();
        }

        public ActionResult HomeView()
        {
            if (TempData["userId"] != null)
            {
                int userId = (int)TempData["userId"];
                TempData.Keep();
                SerializableUser user = Client.GetSerializableUser(userId);
                ViewBag.UserId = user.Id;

                ViewBag.IsVegan = user.Vegan.ToString();
                ViewBag.IsVeg = user.Vegetarian.ToString();
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
        [HttpPost]
        public ActionResult KitchenPartialView(List<SerializableKitchen> kitchens)
        {
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            ViewBag.UserId = userId;
            var user = Client.GetSerializableUser(userId);
            ViewBag.IsVegan = user.Vegan.ToString();
            System.Diagnostics.Debug.WriteLine("Vegan: " + user.Vegan.ToString());
            ViewBag.IsVeg = user.Vegetarian.ToString();
            System.Diagnostics.Debug.WriteLine("Veg: " + user.Vegetarian.ToString());

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
                    Client.AddFoodComplete(userId, foodKitchen.KitchenId, foodKitchen.Food.Name, foodKitchen.Food.Storage.ToString(), foodKitchen.Food.ExpiryDate, foodKitchen.Food.Quantity, foodKitchen.Food.QuantityClassifier, foodKitchen.Food.Vegan, foodKitchen.Food.Vegetarian, new string[0], new string[0], foodKitchen.Food.Favourite);
                }
                else // edit food
                {
                    Client.EditFood(foodKitchen.Food.Id, foodKitchen.Food.Name, foodKitchen.Food.Quantity, foodKitchen.Food.QuantityClassifier, foodKitchen.Food.Storage.ToString(), foodKitchen.Food.ExpiryDate);
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
            List<SerializableRecipeModel> recs = new List<SerializableRecipeModel>();
            try
            {
                recs = Client.SearchRecipesUser(search, 100, Id).ToList();
            }
            catch { }
            return PartialView("_RecipeResultView", recs.ToArray()) ;
        }

        [HttpPost]
        public ActionResult FeelingLucky(string search)
        {


            search = System.Web.HttpUtility.UrlEncode(search);
            int Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            List<SerializableRecipeModel> recs = new List<SerializableRecipeModel>();
            try
            {
                recs = Client.FeelingLuckyUser(100, Id).ToList();
            }
            catch
            {

            }
            return PartialView("_RecipeResultView", recs.ToArray());
        }

        [HttpPost]
        public ActionResult ShoppingList()
        {
            int Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            var kitchen = Client.GetKitchens(Id).FirstOrDefault();
            ViewBag.kID = kitchen.Id;
            var sL = kitchen.Inventory.OrderByDescending(x => x.Favourite).ToList();
            return PartialView("_ShoppingListPartial",sL);
        }

        [HttpPost]
        async public Task<string> SetShoppingList(List<SerializableFood> shoppingList, IFormCollection form)
        {
            int Id = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            int kID = Convert.ToInt32(form["kID"]);
            var sL = Client.GetKitchens(Id).FirstOrDefault().Inventory.OrderByDescending(x => x.Favourite).ToList();

            await Client.EditShoppingListMultipleAsync(kID, shoppingList.ToArray());

            return "Shopping List Updated";
        }


        [HttpGet]
        public ActionResult AddEditFoodView(int kId, int fId)
        {
            ViewBag.KitchenId = kId;
            FoodKitchen foodKitchen = new FoodKitchen();
            foodKitchen.KitchenId = kId;
            ViewBag.StorageTypesList = Client.GetStorages();
            List<string> qClassifiers = new List<string> { "item", "g", "mg", "kg",
                "mL", "L", "oz", "fl. oz.", "gallon", "lb" };
            ViewBag.Classifier = qClassifiers;
            ViewBag.VegVegan = DropdownModel.VegVeganDropdown();
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

                Client.EatFood(food.Id, food.Quantity);


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
            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            ViewBag.UserId = userId;
            var user = Client.GetSerializableUser(userId);
            ViewBag.IsVegan = user.Vegan.ToString();
            ViewBag.IsVeg = user.Vegetarian.ToString();

            KitchenModel kM = new KitchenModel();




            return PartialView("UserKitchenPartialView", user.Kitchens.ToList());
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
