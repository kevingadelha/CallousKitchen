using Capstone.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.App_Code
{
    public class Initializer
    {
        public static void AppInitialize()
        {
            ApiHelper.InitializeClient();
        }
    }
}