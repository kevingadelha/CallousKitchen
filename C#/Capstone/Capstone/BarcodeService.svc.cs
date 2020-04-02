using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Capstone.Apis;

namespace Capstone
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BarcodeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BarcodeService.svc or BarcodeService.svc.cs at the Solution Explorer and start debugging.
    public class BarcodeService : IBarcodeService
    {
       private CallousHippoEntities db = new CallousHippoEntities();
        public Task<string> GetBarcodeData(string barcode)
        {
            OpenFoodFacts openFoodFacts = new OpenFoodFacts();
            return openFoodFacts.LoadBarcode(barcode);
        }
        public async Task<bool> AddItem(string barcode, string name, int userId, int count)
        {
            Product product = new Product();
            Inventory userProduct = new Inventory { Count = count, UserId = userId };

            // check if the barcode already exists with matching name
            if (db.Products.Where(x => x.ProductName == name && x.Barcode == barcode).Count() == 0)
            {
                db.Products.Add(new Product { Barcode = barcode,  ProductName = name });
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            userProduct.ProdId =
                    db.Products.Where(x => x.ProductName == name && x.Barcode == barcode).FirstOrDefault().ProdId;
            System.Diagnostics.Debug.WriteLine(userProduct);
            db.Inventories.Add(userProduct);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditItem(int id, int count)
        {
            var item = db.Inventories.Where(x => x.Id == id).FirstOrDefault();
            item.Count = count;
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveItem(int id)
        {
            var item = db.Inventories.Where(x => x.Id == id).FirstOrDefault();
            db.Inventories.Remove(item);
            await db.SaveChangesAsync();
            return true;
        }

    }
}
