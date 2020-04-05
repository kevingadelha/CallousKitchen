using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CallousFrontEnd.Models;

namespace CallousFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            BarcodeService.BarcodeServiceClient client = new BarcodeService.BarcodeServiceClient();
            System.Diagnostics.Debug.WriteLine(client.TestMethod());
            //ViewBag.Test = client.GetBarcodeDataAsync("007797508006");
            // await ViewBag.Test = client.GetBarcodeDataAsync("007797508006");
            //await vTask.Run(() => client.GetBarcodeDataAsync("007797508006")).Result.ToString();
          //  System.Diagnostics.Debug.WriteLine(test);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
