using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capstone.Classes;
using DocumentFormat.OpenXml.InkML;

namespace CallousFrontEnd.Controllers
{
    
    public class UserController : Controller
    {
        AccountService.AccountServiceClient Client = new AccountService.AccountServiceClient();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user) {



            return null;
        }
        [HttpGet]
        public ActionResult CreateUserView() {
            return View("CreateUser");
        }
    }
}