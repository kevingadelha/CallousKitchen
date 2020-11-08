using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Capstone.Apis
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiClient = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        }
    }
}