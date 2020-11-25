using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Capstone.Apis;
using Capstone.Classes;

namespace Capstone
{
    public class AccountServiceParent
    {
        //private  static HttpClient client = ApiHelper.ApiClient;



        private CallousHipposDb db = new CallousHipposDb();

        public object KeyDerivationPrf { get; private set; }


        public SerializableUser CreateAccount(string email, string pass)
        {
            if (IsValidEmail(email))
            {
                if (db.Users.Where(x => x.Email == email).Count() != 0)
                {
                    return new SerializableUser(-1);
                }
                else
                {
                    User user = new User(email, pass);
                    Guid guid = Guid.NewGuid();
                    // check for guid collisions, very unlikely 
                    while (db.Users.Where(x => x.EmailConfirmKey == guid).Count() > 0)
                    {
                        guid = Guid.NewGuid();
                    }
                    user.EmailConfirmKey = guid;
                    user.IsConfirmed = false;

                    User returnedUser = db.Users.Add(user);
                    returnedUser.Kitchens = new List<Kitchen>();
                    returnedUser.Kitchens.Add(db.Kitchens.Add(new Kitchen { Name = "Kitchen" }));
                    db.SaveChanges();
                    EmailClient emailClient = new EmailClient();
                    emailClient.SendConfirmEmail(user.Email, user.EmailConfirmKey);
                    return new SerializableUser(returnedUser);
                }
            }
            return new SerializableUser(-2);
        }

        public SerializableUser LoginAccount(string email, string pass)
        {
            return (new SerializableUser(db.Users.Where(x => x.Email == email && x.Password == pass).FirstOrDefault()));
        }

        public string ConfirmEmail(string key)
        {
            Guid keyGuid;
            Guid.TryParse(key, out keyGuid);
            Guid blankGuid = new Guid();
            if (keyGuid != blankGuid)
            {
                var user = db.Users.Where(x => x.EmailConfirmKey == keyGuid).FirstOrDefault();
                user.IsConfirmed = true;
                user.EmailConfirmKey = blankGuid; // can't set a null guid, closest thing
                db.SaveChanges();
                return "Success";
            }

            return "Failed";
        }

        public bool AnotherTest()
        {
            return true;
        }

        public async Task<bool> EditUserDietaryRestrictions(int id, bool vegan, bool vegetarian, List<string> allergies)
        {
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            user.Vegan = vegan;
            user.Vegetarian = vegetarian;
            user.Allergies = string.Join("|", allergies);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditUserPassword(int id, string password)
        {
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            user.Password = password;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditUserEmail(int id, string email)
        {
            if (db.Users.Where(x => x.Email == email).Count() != 0)
            {
                return false;
            }
            else
            {
                var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                user.Email = email;
                await db.SaveChangesAsync();
                return true;
            }
        }

        public int AddKitchen(int userId, string name)
        {
            Kitchen kitchen = db.Kitchens.Add(new Kitchen() { Name = name });
            db.Users.Where(x => x.Id == userId).FirstOrDefault().Kitchens.Add(kitchen);
            db.SaveChanges();
            return kitchen.Id;
        }
        public List<SerializableFood> GetPrimaryInventory(int userId)
        {
            return db.Users.Where(x => x.Id == userId).FirstOrDefault()?.Kitchens?.FirstOrDefault()?.Inventory?
                .Select(o => new SerializableFood(o)).ToList();
        }
        public List<SerializableKitchen> GetKitchens(int userId)
        {
            return db.Users.Where(x => x.Id == userId).FirstOrDefault()?.Kitchens?
                .Select(o => new SerializableKitchen(o)).ToList();
        }
        public List<SerializableFood> GetInventory(int kitchenId)
        {
            return db.Kitchens.Where(x => x.Id == kitchenId).FirstOrDefault()?.Inventory?
                .Select(o => new SerializableFood(o)).ToList();
        }

        public List<Storage> GetStorages()
        {
            List<Storage> storages = new List<Storage>();
            storages.Add(Storage.Fridge);
            storages.Add(Storage.Freezer);
            storages.Add(Storage.Pantry);
            storages.Add(Storage.Cupboard);
            storages.Add(Storage.Cellar);
            storages.Add(Storage.Other);

            return storages;
        }

        public SerializableUser GetSerializableUser(int id)
        {
            SerializableUser user = new SerializableUser(db.Users.Where(x => x.Id == id).FirstOrDefault());
            return user;
        }

        public string DoesThisEvenWork()
        {

            return "yes";
        }

        public User GetUser(int id)
        {
            return (db.Users.Where(x => x.Id == id).FirstOrDefault());
        }

        public Task<string> GetBarcodeData(string barcode)
        {
            OpenFoodFacts openFoodFacts = new OpenFoodFacts();
            return openFoodFacts.LoadBarcode(barcode);
        }

        public Task<Models.SerializableFoodFactsProductModel> GetAllOpenFoodFacts(string barcode)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            OpenFoodFacts openFoodFacts = new OpenFoodFacts();
            return openFoodFacts.LoadAllBarcodeData(barcode);

        }

        public List<string> GenerateShoppingList(int kichenId)
        {
            List<string> shoppingList;
            shoppingList = db.Kitchens.Where(x => x.Id == kichenId).FirstOrDefault().Inventory.Where(i => i.Favourite == true && i.Quantity < 3).Select(n => n.Name).ToList();
            return shoppingList;
        }

        public Models.SerializableRecipeModel[] SearchRecipesUser(string search, int count, int userId)
        {
            var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            List<string> diets = user.Allergies?.Split('|')?.ToList() ?? new List<string>();
            if (user.Vegan)
                diets.Add("vegan");
            if (user.Vegetarian)
                diets.Add("vegetarian");
            return SearchRecipesRanked(search, count, diets, user.Kitchens.FirstOrDefault().Id);
        }

        // Author Peter Szadurski
        public Task<Models.SerializableRecipeModel[]> SearchRecipes(string search, int count, List<string> diets)
        {
            RecipeApi recipeApi = new RecipeApi();
            return recipeApi.GetRecipe(search, count, diets.ToArray());
        }
        // Author Peter Szadurski

        public Models.SerializableRecipeModel[] SearchRecipesRanked(string search, int count, List<string> diets, int kitchenId)
        {
            RecipeApi recipeApi = new RecipeApi();
            Models.SerializableRecipeModel[] recipes = recipeApi.GetRecipe(search, count, diets.ToArray()).Result;
            List<Food> foods = db.Kitchens.Where(x => x.Id == kitchenId).FirstOrDefault().Inventory;

            foreach (var r in recipes)
            {
                /*foreach (var i in r.Ingredients)
                {
                    foreach (var f in foods)
                    {
                        if (f.Name.ToLower() == i.Name.ToLower())
                        {
                            i.Score = 2;
                            break;
                        }
                        else if (f.Name.ToLower().Contains(i.Name.ToLower()))
                        {
                            i.Score = 1;
                        }
                    }
                    r.Score += i.Score;
                }
                */
                foreach (var i in r.EdamanIngredients)
                {
                    int tempScore = 0;
                    foreach (var f in foods)
                    {

                        if (f.Name.ToLower() == i.Name.ToLower())
                        {
                            i.Score = 2;

                        }
                        else if (i.Score != 2 && (f.Name.ToLower().Contains(i.Name.ToLower()) || i.Name.ToLower().Contains(f.Name.ToLower())))
                        {
                            i.Score = 1;
                        }
                        if (i.Score > 0 && f.ExpiryDate.HasValue)
                        {
                            DateTime date1 = f.ExpiryDate.Value;
                            DateTime date2 = DateTime.Now.AddDays(3);
                            TimeSpan time = date2 - date1;
                            if (time.Days > 0)
                                i.Score += time.Days;
                        }
                        if (i.Score > tempScore)
                        {
                            tempScore = i.Score;
                            i.Score = tempScore;
                        }

                    }

                    r.Score += tempScore;
                }

            }
            recipes = recipes.OrderByDescending(x => x.Score).ToArray();

            return recipes;
        }

        public Models.SerializableRecipeModel[] FeelingLucky(int count, List<string> diets, int kitchenId)
        {
            string searchString = "";
            List<Food> foods = db.Kitchens.Where(x => x.Id == kitchenId).FirstOrDefault().Inventory;
            Models.SerializableRecipeModel[] recipes = null;
            int take = 5;
            int itemNumber = 1;
            while (recipes == null || recipes.Count() == 0)
            {
                //Try with several items and decrease the amount on each failure
                if (take > 0)
                {
                    var expiringFoods = foods.OrderByDescending(x => x.ExpiryDate).Take(take).ToList();
                    foreach (var f in expiringFoods)
                    {
                        searchString += f.Name + "+";
                    }
                    recipes = SearchRecipesRanked(searchString, count, diets, kitchenId);
                    take--;
                }
                //Try all individual items
                else if (itemNumber < foods.Count)
                {
                    recipes = SearchRecipesRanked(foods[itemNumber].Name, count, diets, kitchenId);
                    itemNumber++;
                }
                //If all else fails, give up
                else
                {
                    break;
                }
            }
            return recipes ?? new Models.SerializableRecipeModel[0];
        }

        public Models.SerializableRecipeModel[] FeelingLuckyUser(int count, int userId)
        {
            var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            List<string> diets = user.Allergies?.Split('|')?.ToList() ?? new List<string>();
            if (user.Vegan)
                diets.Add("vegan");
            if (user.Vegetarian)
                diets.Add("vegetarian");
            return FeelingLucky(count, diets, user.Kitchens.FirstOrDefault().Id);
        }


        public async Task<bool> AddFood(int userId, int kitchenId, string name, int quantity, DateTime? expiryDate)
        {
            //Don't let user add food if they're not confirmed
            if (!db.Users.Where(x => x.Id == userId).FirstOrDefault().IsConfirmed)
                return false;
            db.Kitchens.Where(x => x.Id == kitchenId).FirstOrDefault().Inventory
                .Add(db.Foods.Add(new Food() { Name = name, Quantity = quantity, ExpiryDate = expiryDate, Vegetarian = -1, Vegan = -1, Calories = -1 }));
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddFoodComplete(int userId, int kitchenId, string name, string storage, DateTime? expiryDate, double quantity, string quantityClassifier, int vegan, int vegetarian, List<string> ingredients, List<string> traces, bool favourite)
        {
            //Don't let user add food if they're not confirmed
            if (!db.Users.Where(x => x.Id == userId).FirstOrDefault().IsConfirmed)
                return false;
            var food = new Food(name, (Storage)Enum.Parse(typeof(Storage), storage, true), expiryDate, quantity, quantityClassifier, vegan, vegetarian, ingredients, traces, favourite);
            db.Kitchens.Where(x => x.Id == kitchenId).FirstOrDefault().Inventory
                .Add(db.Foods.Add(food));
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EatFood(int id, double quantity)
        {
            var item = db.Foods.Where(x => x.Id == id).FirstOrDefault();
            if (quantity == 0 && !item.Favourite)
            {
                db.Foods.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditFood(int id, string name, double quantity, DateTime? expiryDate)
        {
            var item = db.Foods.Where(x => x.Id == id).FirstOrDefault();
            //Assume that if the user is editing the food they don't want to delete by having the quantity be zero
            item.Name = name;
            item.Quantity = quantity;
            //Assume that the user editing the food means they are resetting their initial quantity
            item.InitialQuantity = quantity;
            item.ExpiryDate = expiryDate;
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> FavouriteFood(int foodId, bool favourite)
        {
            var item = db.Foods.Where(x => x.Id == foodId).FirstOrDefault();
            item.Favourite = favourite;
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveItem(int id)
        {
            var item = db.Foods.Where(x => x.Id == id).FirstOrDefault();
            db.Foods.Remove(item);
            await db.SaveChangesAsync();
            return true;
        }

        //I'm just going to leave this test method here since I keep needing it
        public List<SerializableUser> Test()
        {
            return db.Users.ToList().Select(o => new SerializableUser(o)).ToList();
        }

        //returns true if email is valid, false if invalid
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            //Normalize the domain
            try
            {
                //checks an email for matches with regular expression
                //in this case it looks for "@" and domain name
                //if there were matches - delegates domain name proccessing to DomainMapper function
                //replaces email part that's complies with regex (@gmail.com) with DomainMapper return string

                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    //Domain part of an email can have international characters and they are converted to PunyCode
                    //bücher.com --> xn--bcher-kva.com
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    //I am not sure how it works, but Group[1] (not 0) is linked to first group (@)
                    //Group[2] is linked to domain name


                    //return "@" and proccessed domain name
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }


            //check if normalized email is matched to regex which checks for correct email address
            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public string HashPassword(string password)
        {
            //I am hashing using Rcf2898DeriveBytes method


            //There is another algorithm used in ASP.NET Core - PBKDF2

            //it detects the operating system and tries to choose optimal implementation
            //according to microsoft it offers 10x througput compared to  Rcf2898DeriveBytes
            //it supports more hashing algorithms - HMACSHA256, HMACSHA512



            //creating a salt value which is later appended to a password
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //use password+salt combination to get hash with help of Rcf2898DeriveBytes method
            var hashvalue = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = hashvalue.GetBytes(20);


            //combine salt+hash (it will be needed to verify a user password from his input)
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //final (combined) password which will be saved into the database
            string finalPassword = Convert.ToBase64String(hashBytes);
            return finalPassword;
        }

        public bool IsValidPassword(string databaseHashPassword, string userPassword)
        {
            //get hashBytes from database
            byte[] hashBytes = Convert.FromBase64String(databaseHashPassword);
            byte[] salt = new byte[16];
            //extract salt from combined password
            Array.Copy(hashBytes, 0, salt, 0, 16);

            //get hashvalue from input and salt
            var hashvalue = new Rfc2898DeriveBytes(userPassword, salt, 10000);
            byte[] hash = hashvalue.GetBytes(20);


            //compare hashvalue in database to hashvalue generated from salt and user password
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
        public Food GetFood(int id)
        {
            return db.Foods.Where(x => x.Id == id).FirstOrDefault();
        }


        public bool DemoDb(string pass)
        {
            if (pass != "67X7C@&Aej*hS%")
            {
                return false;
            }
            else
            {
                // Truncate
                db.CaloriesInDays.RemoveRange(db.CaloriesInDays);
                db.Foods.RemoveRange(db.Foods);
                db.Kitchens.RemoveRange(db.Kitchens);
                db.Foods.RemoveRange(db.Foods);
                db.Users.RemoveRange(db.Users);
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Users, RESEED, 0);");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Foods, RESEED, 0);");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Kitchens, RESEED, 0);");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT (CaloriesInDays, RESEED, 0);");
                db.SaveChanges();

                //Create User
                User demoUser = new User("demo@example.com", "pass");
                demoUser.EmailConfirmKey = new Guid();
                demoUser.IsConfirmed = true;
                db.Users.Add(demoUser);
                db.SaveChanges();

                // Kitchens
                demoUser.Kitchens = new List<Kitchen>();
                demoUser.Kitchens.Add(db.Kitchens.Add(new Kitchen { Name = "Kitchen", Inventory = new List<Food>() }));
                db.SaveChanges();

                // Add Food

                var kitch = demoUser.Kitchens.FirstOrDefault().Inventory;

                kitch.Add(db.Foods.Add(new Food() { Name = "Butter", Quantity = 200, ExpiryDate = DateTime.Now.AddDays(7), Vegetarian = 1, Vegan = 0, Calories = -1, Storage = Storage.Fridge, QuantityClassifier = "g", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Garilic", Quantity = 7, ExpiryDate = DateTime.Now.AddDays(3), Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Fridge, QuantityClassifier = "item", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Steak", Quantity = 12, ExpiryDate = DateTime.Now.AddDays(3), Vegetarian = 0, Vegan = 0, Calories = -1, Storage = Storage.Fridge, QuantityClassifier = "oz", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Egg", Quantity = 11, ExpiryDate = DateTime.Now.AddDays(14), Vegetarian = 1, Vegan = 0, Calories = -1, Storage = Storage.Fridge, Favourite = true, QuantityClassifier = "item", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Apple", Quantity = 3, ExpiryDate = DateTime.Now.AddDays(4), Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Pantry, QuantityClassifier = "item", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Salt", Quantity = 10000, ExpiryDate = null, Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Cupboard, Favourite = true, QuantityClassifier = "g", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Pepper", Quantity = 1256, ExpiryDate = null, Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Cupboard, Favourite = true, QuantityClassifier = "g", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Cinnamon", Quantity = 300, ExpiryDate = null, Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Pantry, Favourite = true, QuantityClassifier = "g", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Heavy Cream", Quantity = 0.5, ExpiryDate = DateTime.Now.AddDays(20), Vegetarian = 1, Vegan = 0, Calories = -1, Storage = Storage.Fridge, Favourite = true, QuantityClassifier = "L", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Whole Milk", Quantity = 2, ExpiryDate = DateTime.Now.AddDays(14), Vegetarian = 1, Vegan = 0, Calories = -1, Storage = Storage.Fridge, Favourite = false, QuantityClassifier = "L", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Almond Milk", Quantity = 2, ExpiryDate = DateTime.Now.AddDays(14), Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Fridge, Favourite = false, QuantityClassifier = "L", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Coldbrew Coffee", Quantity = 1, ExpiryDate = DateTime.Now.AddDays(14), Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Fridge, Favourite = true, QuantityClassifier = "L", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Ground Beef", Quantity = 300, ExpiryDate = DateTime.Now.AddDays(25), Vegetarian = 0, Vegan = 0, Calories = -1, Storage = Storage.Freezer, Favourite = true, QuantityClassifier = "lb", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Orange Juice", Quantity = 1, ExpiryDate = DateTime.Now.AddDays(25), Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Fridge, Favourite = true, QuantityClassifier = "gallon", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Red Wine", Quantity = 750, ExpiryDate = null, Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Cellar, Favourite = true, QuantityClassifier = "mL", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Canned Chilli", Quantity = 3, ExpiryDate = null, Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Pantry, Favourite = true, QuantityClassifier = "item", Ingredients = "", Traces = "" }));
                kitch.Add(db.Foods.Add(new Food() { Name = "Canned Beans", Quantity = 3, ExpiryDate = null, Vegetarian = 1, Vegan = 1, Calories = -1, Storage = Storage.Pantry, Favourite = true, QuantityClassifier = "item", Ingredients = "", Traces = "" }));


                db.SaveChanges();
                return true;
            }
        }
    }
}
