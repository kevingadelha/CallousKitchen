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

        // remove shortly

        public async Task<string> RecipeTest()
        {
            string url = $"https://api.edamam.com/search?q=chicken&app_id=ffa3c67d&app_key=2f1c9181989a25dd44b03abe15600a3f&from=0&to=3&calories=591-722&health=alcohol-free";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    RecipeQueryModel RQModel = await response.Content.ReadAsAsync<RecipeQueryModel>();
                    for (int i = 0; i < RQModel.Hits.Count(); i++)
                    {
                        System.Diagnostics.Debug.WriteLine(i);
                        System.Diagnostics.Debug.WriteLine(RQModel.Hits[i].Recipe.Name);

                    }
                    return RQModel.Query;
                }
                else
                {
                    return "The request has failed";
                }
            }
        }
    }
}