using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Capstone.Apis
{
    public class OpenFoodFacts
    {
        public async Task<string> LoadBarcode(string barcode)
        {
            string url = $"http://world.openfoodfacts.org/api/v0/product/{barcode}.json";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    FoodFactsModel FFModel = await response.Content.ReadAsAsync<FoodFactsModel>();
                    return FFModel.Product.Name;
                }
                else
                {
                    return "The request has failed";
                }
            }

        }
        public async Task<SerializedFoodFactsProductModel> LoadAllBarcodeData(string barcode)
        {
            string url = $"http://world.openfoodfacts.org/api/v0/product/{barcode}.json";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    FoodFactsModel FFModel = await response.Content.ReadAsAsync<FoodFactsModel>();
                    SerializedFoodFactsProductModel serializedFoodFactsProductModel = new SerializedFoodFactsProductModel(FFModel.Product);
                    return serializedFoodFactsProductModel;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}