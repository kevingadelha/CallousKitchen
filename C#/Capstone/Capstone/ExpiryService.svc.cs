using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ExpiryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ExpiryService.svc or ExpiryService.svc.cs at the Solution Explorer and start debugging.
    public class ExpiryService : IExpiryService
    {
        public List<Product> products { get; set; }
        public string resultExpiry;
        public string resultStorage;

        DateTime dateToday = DateTime.Now;
        DateTime normalExpiryDate;

        string minExpiryRange;
        string maxExpiryRange;
        string normalExpiryRange;
        Result result = new Result();


        public string GetDateStorage(string crop)
        {
            products = getJsonData();
            checkJsonData(crop, products);
            return string.Format(result.RESULTEXPIRY + " || " + result.RESULTSTORAGE);

        }


        public List<Product> getJsonData()
        {
            string json = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "expiryinfo.json");
            products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(json);
            return products;
        }


        public Result checkJsonData(string crop, List<Product> products)
        {
            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            format.NumberDecimalSeparator = ".";


            foreach (var item in products)
            {
                if (crop.ToLower().Equals(item.CROP.ToLower()))
                {

                    if (item.STORAGELIFE.Contains("-"))
                    {
                        minExpiryRange = item.STORAGELIFE.Substring(0, item.STORAGELIFE.IndexOf("-"));
                        maxExpiryRange = item.STORAGELIFE.Substring(item.STORAGELIFE.IndexOf("-") + 1);
                        resultExpiry = Math.Round((double.Parse(minExpiryRange) + double.Parse(maxExpiryRange)) / 2).ToString();

                        if (Convert.ToDouble(resultExpiry) > 30)
                        {

                            normalExpiryDate = dateToday.AddDays(30);
                            resultExpiry = normalExpiryDate.ToShortDateString();
                        }
                        else
                        {
                            normalExpiryDate = dateToday.AddDays(Convert.ToDouble(resultExpiry));
                            resultExpiry = normalExpiryDate.ToShortDateString();
                        }

                    }
                    else
                    {
                        normalExpiryRange = item.STORAGELIFE;
                        if (Convert.ToDouble(normalExpiryRange) > 30)
                        {
                            normalExpiryDate = dateToday.AddDays(30);
                            resultExpiry = normalExpiryDate.ToShortDateString();
                        }
                        else
                        {
                            normalExpiryDate = dateToday.AddDays(Convert.ToDouble(normalExpiryRange));

                            resultExpiry = normalExpiryDate.ToShortDateString();
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
                    result.RESULTEXPIRY = resultExpiry;
                    result.RESULTSTORAGE = resultStorage;
                    return result;
                }
            }

            return result;
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
            public string RESULTEXPIRY { get; set; }
        }
    }


}
