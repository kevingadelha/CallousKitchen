using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Capstone
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ExpiryEstimationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ExpiryEstimationService.svc or ExpiryEstimationService.svc.cs at the Solution Explorer and start debugging.
    public class ExpiryEstimationService : IExpiryEstimationService
    {
        public List<Product> products { get; set; }
        Result result = new Result();
        DateTime dateToday = DateTime.Now;

        DateTime expiryDate;

        string minExpiryRange;
        string maxExpiryRange;
        string normalExpiryRange;
        public string resultExpiry;
        public string resultStorage;


        public DateTime DoWork(string productName)
        {
           return GetDateStorage(productName);

        }

        public DateTime GetDateStorage(string crop)
        {
            products = getJsonData();
            goThroughJsonData(crop, products);
            return result.RESULTEXPIRY;
        }






        public Result goThroughJsonData(string product, List<Product> products)
        {
            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            format.NumberDecimalSeparator = ".";


            foreach (var item in products)
            {
                //if (product.ToLower().Equals(item.CROP.ToLower()))
                if (item.CROP.ToLower().Contains(product.ToLower()))
                {

                    if (item.STORAGELIFE.Contains("-"))
                    {
                        minExpiryRange = item.STORAGELIFE.Substring(0, item.STORAGELIFE.IndexOf("-"));
                        maxExpiryRange = item.STORAGELIFE.Substring(item.STORAGELIFE.IndexOf("-") + 1);
                        resultExpiry = Math.Round((double.Parse(minExpiryRange) + double.Parse(maxExpiryRange)) / 2).ToString();

                        if (Convert.ToDouble(resultExpiry) > 30)
                        {

                            expiryDate = dateToday.AddDays(30);
                            resultExpiry = expiryDate.ToShortDateString();
                        }
                        else
                        {
                            expiryDate = dateToday.AddDays(Convert.ToDouble(resultExpiry));
                            resultExpiry = expiryDate.ToShortDateString();
                        }

                    }
                    else
                    {
                        normalExpiryRange = item.STORAGELIFE;
                        if (Convert.ToDouble(normalExpiryRange) > 30)
                        {
                            expiryDate = dateToday.AddDays(30);
                            resultExpiry = expiryDate.ToShortDateString();
                        }
                        else
                        {
                            expiryDate = dateToday.AddDays(Convert.ToDouble(normalExpiryRange));

                            resultExpiry = expiryDate.ToShortDateString();
                        }
                    }


                    string minTemperatureRange = "0";
                    string maxTemperatureRange = "0";
                    string normalTemperatureRange = "0";
                    string finalTemperature = "0";

                    if (item.TEMPERATURE.Contains("-"))
                    {
                        minTemperatureRange = item.TEMPERATURE.Substring(0, item.TEMPERATURE.LastIndexOf("-"));
                        maxTemperatureRange = item.TEMPERATURE.Substring(item.TEMPERATURE.LastIndexOf("-") + 1);


                        finalTemperature = Math.Round((double.Parse(minTemperatureRange, format) + double.Parse(maxTemperatureRange, format)) / 2).ToString();


                    }
                    else
                    {
                        normalTemperatureRange = item.TEMPERATURE;

                        finalTemperature = normalTemperatureRange;
                    }



                    if (double.Parse(finalTemperature, format) >= -18 && double.Parse(finalTemperature, format) <= -2)
                    {
                        resultStorage = "freezer";
                    }
                    else
                    if (double.Parse(finalTemperature, format) >= -2 && double.Parse(finalTemperature, format) <= 7)
                    {
                        resultStorage = "refridgerator";
                    }
                    else
                    if (double.Parse(finalTemperature, format) > 7 && double.Parse(finalTemperature, format) <= 14)
                    {
                        resultStorage = "cellar";
                    }
                    else if (double.Parse(finalTemperature, format) > 14)
                    {
                        resultStorage = "pantry";
                    }
                    result.RESULTEXPIRY = DateTime.Parse(resultExpiry);
                    result.RESULTSTORAGE = resultStorage;
                    return result;
                }
            }

            result.RESULTEXPIRY = dateToday.AddDays(7);

            return result;
        }






        public List<Product> getJsonData()
        {
            string json = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "expiryinfo.json");
            products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(json);
            return products;
        }

        public class Product
        {
            public string CROP { get; set; }
            public string TEMPERATURE { get; set; }
            public string STORAGELIFE { get; set; }
        }
        public class Result
        {
            public string RESULTSTORAGE { get; set; }
            public DateTime RESULTEXPIRY { get; set; }
        }

    }
}
