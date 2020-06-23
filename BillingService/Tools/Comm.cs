using System;
using System.IO;
using System.Net;
using BillingService.Models;
using Newtonsoft.Json;

namespace BillingService.Tools {

    public class Comm {

        public static string endPoint = "http://localhost:5000/api/products/";

        public static product updateStockAmount (Guid product_id, uint new_stock) {
            string url = endPoint + product_id.ToString () + "/stock/" + new_stock.ToString ();
            var request = (HttpWebRequest) WebRequest.Create (url);
            request.Method = "PUT";
            var response = (HttpWebResponse) request.GetResponse ();
            var responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
            product prod = JsonConvert.DeserializeObject<product> (responseString);
            return prod;
        }

        public static product getProduct (Guid product_id) {
            string url = endPoint + product_id.ToString ();
            var request = (HttpWebRequest) WebRequest.Create (url);
            request.Method = "GET";
            var response = (HttpWebResponse) request.GetResponse ();
            var responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
            product prod = JsonConvert.DeserializeObject<product> (responseString);
            return prod;
        }
    }
}
