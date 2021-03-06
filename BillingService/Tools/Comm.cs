using System;
using System.IO;
using System.Net;
using BillingService.Models;
using Newtonsoft.Json;

namespace BillingService.Tools {

    public class Comm {

        public static string endPoint = "http://prodcut_service:5000/api/products/";

        public static product updateStockAmount (Guid product_id, uint new_stock) {
            string url = endPoint + product_id.ToString () + "/stock/" + new_stock.ToString ();
            product prod;
            string responseString;
            try {
                var request = (HttpWebRequest) WebRequest.Create (url);
                request.Method = "PUT";
                var response = (HttpWebResponse) request.GetResponse ();
                responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
            } catch (WebException) {
                return null;
            }
            prod = JsonConvert.DeserializeObject<product> (responseString);
            return prod;
        }

        public static product getProduct (Guid product_id) {
            string url = endPoint + product_id.ToString ();
            product prod;
            string responseString;
            try {
                var request = (HttpWebRequest) WebRequest.Create (url);
                request.Method = "GET";
                var response = (HttpWebResponse) request.GetResponse ();
                responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
            } catch (WebException) {
                return null;
            }
            prod = JsonConvert.DeserializeObject<product> (responseString);
            return prod;
        }
    }
}
