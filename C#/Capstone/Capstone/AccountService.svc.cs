﻿using System;
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

namespace Capstone
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AccountService.svc or AccountService.svc.cs at the Solution Explorer and start debugging.
    public class AccountService : IAccountService
    {
        private CallousHipposDb db = new CallousHipposDb();

        public int CreateAccountWithEmail(string userName, string pass, string email) {
            if (db.Users.Where(x => x.Email == email && x.Username == userName).Count() != 0)
            {
                return -1;
            }
            else {
                User user = new User { Username = userName, Email = email, Password = pass, GuiltLevel = 1 };
                User returnedUser = db.Users.Add(user);
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
                return returnedUser.Id;
            }

        }

        //temporary solution for creating an account without an email
        public int CreateAccount(string userName, string pass)
        {
            return CreateAccountWithEmail(userName, pass, userName);
        }

        public int LoginAccount(string userName, string pass)
        {
            return (db.Users.Where(x => x.Username == userName && x.Password == pass).FirstOrDefault()?.Id??-1);
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
                .Add(db.Foods.Add(new Food() { Name = name, Quantity = quantity, ExpiryDate = expiryDate }));
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
        public List<SerializableFood> Test()
        {
            return db.Kitchens.Where(x => x.Id == 1).FirstOrDefault().Inventory.Select(o => new SerializableFood(o)).ToList();
        }
    }
}
