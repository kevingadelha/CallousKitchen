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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public List<Product> products { get; set; }
        private const string fileName = "expiryinfo.json";
        private readonly string filePath = Environment.CurrentDirectory;
        //private pathfile = Path.Combine(Directory.GetCurrentDirectory(), "\\expiryinfo.json");
        public string resultExpiry;
        public string resultStorage;

        DateTime dateToday = DateTime.Now;
        DateTime firstExpiryDate;
        DateTime finalExpiryDate;
        DateTime normalExpiryDate;

        string minExpiryRange;
        string maxExpiryRange;
        string normalExpiryRange;

        string minTemperatureRange;
        string maxTemperatureRange;
        string normalTemperatureRange;





        string dateToDisplay;

        public string GetData(string crop)
        {
            string json = System.IO.File.ReadAllText("C:\\Users\\Vlad\\Documents\\GitHub\\CallousHippo\\C#\\Capstone\\Capstone\\expiryinfo.json");
            products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(json);

            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            format.NumberDecimalSeparator = ".";



            foreach (var item in products)
            {
                if (crop.ToLower().Equals(item.CROP.ToLower()))
                {

                    //no barcode expiry date
                    if (item.STORAGELIFE.Contains("-"))
                    {
                        minExpiryRange = item.STORAGELIFE.Substring(0, item.STORAGELIFE.IndexOf("-"));
                        maxExpiryRange = item.STORAGELIFE.Substring(item.STORAGELIFE.IndexOf("-") + 1);
                        resultExpiry = minExpiryRange + "-" + maxExpiryRange;
                        firstExpiryDate = dateToday.AddDays(Convert.ToDouble(minExpiryRange));
                        finalExpiryDate = dateToday.AddDays(Convert.ToDouble(maxExpiryRange));

                        resultExpiry = firstExpiryDate.ToShortDateString() + " - " + finalExpiryDate.ToShortDateString();

                    }
                    else
                    {
                        normalExpiryRange = item.STORAGELIFE;
                        normalExpiryDate = dateToday.AddDays(Convert.ToDouble(normalExpiryRange));

                        resultExpiry = normalExpiryDate.ToShortDateString();
                    }


                    string minTemperatureRange = "0";
                    string maxTemperatureRange = "0";
                    string normalTemperatureRange = "0";
                    string finalTemperature = "0";

                    //no barcode storage type
                    if (item.TEMPERATURE.Contains("-"))
                    {
                        minTemperatureRange = item.TEMPERATURE.Substring(0, item.TEMPERATURE.LastIndexOf("-"));
                        maxTemperatureRange = item.TEMPERATURE.Substring(item.TEMPERATURE.LastIndexOf("-") + 1);


                        finalTemperature = Math.Round((double.Parse(minTemperatureRange, format) + double.Parse(minTemperatureRange, format)) / 2).ToString();


                    }
                    else
                    {
                        normalTemperatureRange = item.TEMPERATURE;

                        finalTemperature = normalTemperatureRange;
                    }





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




                }
            }


            return string.Format(resultExpiry + " || " + resultStorage);

        }
        public class Product
        {
            public string CROP { get; set; }
            public string TEMPERATURE { get; set; }
            public string STORAGELIFE { get; set; }
        }
    }
}
