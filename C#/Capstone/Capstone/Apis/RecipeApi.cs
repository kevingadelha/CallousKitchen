using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Capstone.Apis
{
    public class RecipeApi
    {
        private readonly string AppiKey = "2f1c9181989a25dd44b03abe15600a3f";
        private readonly string AppId = "ffa3c67d";
        public async Task<string[]> GetRecipe(string search, int count, int caloriesMin = 0, int caloriesMax = 1000000)
        {
            List<string> results = new List<string>();
            //string url = $"https://api.edamam.com/search?q={search}&app_id{AppId}=&app_key={AppiKey}&from=0&to={count}&calories={caloriesMin}-{caloriesMax}";
            string url = $"https://api.edamam.com/search?q=chicken&app_idffa3c67d=&app_key=2f1c9181989a25dd44b03abe15600a3f&from=0&to=20&calories=3-500";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    RecipeQueryModel RQModel = await response.Content.ReadAsAsync<RecipeQueryModel>();
                    for (int i = 0; i < RQModel.Hits.Count(); i++)
                    {
                        results.Add(RQModel.Hits[i].Recipe.Name);

                    }
                    return results.ToArray();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}