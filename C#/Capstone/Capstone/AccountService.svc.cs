using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using Capstone.Apis;
using Capstone.Classes;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Capstone
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AccountService.svc or AccountService.svc.cs at the Solution Explorer and start debugging.
    public class AccountService : IAccountService
    {
        private CallousHipposDb db = new CallousHipposDb();

        public object KeyDerivationPrf { get; private set; }

        public SerializableUser CreateAccountWithEmail(string userName, string pass, string email)
        {
            if (IsValidEmail(email))
            {
                if (db.Users.Where(x => x.Email == email && x.Username == userName).Count() != 0)
                {
                    return new SerializableUser(-1);
                }
                else
                {
                    User user = new User(userName,email,pass);
                    User returnedUser = db.Users.Add(user);
                    returnedUser.Kitchens.Add(new Kitchen { Name = "Kitchen" });
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Debug.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Debug.WriteLine($"- Property: \"{ve.PropertyName}\", Value: \"{eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName)}\", Error: \"{ve.ErrorMessage}\"");
                            }
                        }
                    }
                    return new SerializableUser(returnedUser);
                }
            }
            return new SerializableUser(-2);
        }

        //temporary solution for creating an account without an email
        public SerializableUser CreateAccount(string userName, string pass)
        {
            return CreateAccountWithEmail(userName, pass, userName);
        }

        public SerializableUser LoginAccount(string userName, string pass)
        {
            return (new SerializableUser(db.Users.Where(x => x.Username == userName && x.Password == pass).FirstOrDefault()));
        }

        public async Task<bool> EditUserPreferences(int id, string username, string email, string password, bool vegetarian, bool vegan, bool calorieTracker, List<string> allergies)
        {
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            user.Username = username;
            user.Email = email;
            user.Password = password;
            user.Vegan = vegan;
            user.Vegetarian = vegetarian;
                user.Allergies = string.Join("|", allergies);
            await db.SaveChangesAsync();
            return true;
        }

        public int AddKitchen(int userId, string name)
        {
            Kitchen kitchen = db.Kitchens.Add(new Kitchen() { Name = name });
            db.Users.Where(x => x.Id == userId).FirstOrDefault().Kitchens.Add(kitchen);
            db.SaveChanges();
            return kitchen.Id;
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




        public Task<string> GetBarcodeData(string barcode)
        {
            OpenFoodFacts openFoodFacts = new OpenFoodFacts();
            return openFoodFacts.LoadBarcode(barcode);
        }
        public async Task<bool> AddFood(int kitchenId, string name, int quantity, DateTime? expiryDate)
        {
            db.Kitchens.Where(x => x.Id == kitchenId).FirstOrDefault().Inventory
                .Add(db.Foods.Add(new Food() { Name = name, Quantity = quantity, ExpiryDate = expiryDate, Vegetarian = -1, Vegan = -1, Calories = -1 }));
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddFoodComplete(int kitchenId, string name, int quantity, DateTime? expiryDate, int vegan, int vegetarian, int calories, List<string> ingredients, List<string> traces)
        {
            db.Kitchens.Where(x => x.Id == kitchenId).FirstOrDefault().Inventory
                .Add(db.Foods.Add(new Food(name,expiryDate,quantity,vegan,vegetarian,ingredients,traces,calories)));
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EatFood(int id, int quantity)
        {
            var item = db.Foods.Where(x => x.Id == id).FirstOrDefault();
            item.Quantity = quantity;
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditFood(int id, string name, int quantity, DateTime? expiryDate)
        {
            var item = db.Foods.Where(x => x.Id == id).FirstOrDefault();
            item.Name = name;
            item.Quantity = quantity;
            item.ExpiryDate = expiryDate;
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

        // Author Peter Szadurski
        public Task<Models.SerializedFoodFactsProductModel> GetAllOpenFoodFacts(string barcode)
        {
            OpenFoodFacts openFoodFacts = new OpenFoodFacts();
            return openFoodFacts.LoadAllBarcodeData(barcode);
        }

        // Author Peter Szadurski
        public Task<string[]> SearchRecipes(string search, int count, int caloriesMin = 0, int caloriesMax = 0)
        {
            RecipeApi recipeApi = new RecipeApi();
            return recipeApi.GetRecipe(search, count, caloriesMin, caloriesMax);
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
            for (int i=0; i < 20; i++)
            {
                if (hashBytes[i+16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

  
    }
}
