using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductService.Contexts;
using ProductService.Models;
using ProductService.Tools;

namespace ProductService.Repositories {

    public class ProductRepository : IProductRepository {

        private readonly ProductDatabaseContext _context;
        public static string uri = "http://billing_service:5002/api/billing";

        public ProductRepository (ProductDatabaseContext context) {
            _context = context;
        }

        public void addProduct (product newProduct) {
            newProduct.productRegisterDate = DateTime.Now;
            _context.product.Add (newProduct);
            _context.SaveChanges ();
        }

        public product getProduct (Guid productId) {
            return _context.product.AsNoTracking ().Where (c => c.productId == productId).FirstOrDefault ();
        }

        public product updateProduct (product newProduct) {
            _context.Entry (newProduct).State = EntityState.Modified;
            _context.SaveChanges ();
            return newProduct;
        }

        public product deleteProduct (Guid productId) {
            product selected = getProduct (productId);
            _context.product.Remove (selected);
            _context.SaveChanges ();
            return selected;
        }

        public IEnumerable<product> getAllProducts () {
            return _context.product.OrderBy (c => c.productName).ToList ();
        }

        public bool productExistsById (Guid productId) {
            return _context.product.Any (n => n.productId == productId);
        }

        public bool productExistsByName (string productName) {
            return _context.product.Any (n => n.productName == productName);
        }

        public (List<sale>, CustomError) buyProduct (IEnumerable<Guid> productIds) {
            string json = JsonConvert.SerializeObject (productIds);
            var data = Encoding.ASCII.GetBytes (json);
            string responseString = "";
            CustomError err;
            List<sale> sales;
            try {
                var request = (HttpWebRequest) WebRequest.Create (uri);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream ()) {
                    stream.Write (data, 0, data.Length);
                }
                var response = (HttpWebResponse) request.GetResponse ();
                responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
            } catch (WebException) {
                return (null, new CustomError ("Fatal Error", "Billing Service Down", "99"));
            }
            try {
                sales = JsonConvert.DeserializeObject<List<sale>> (responseString);
            } catch {
                sales = null;
            }
            try {
                err = JsonConvert.DeserializeObject<CustomError> (responseString);
            } catch {
                err = null;
            }
            return (sales, err);
        }

        public List<sale> getBill (Guid bill_id) {
            string url = uri + "/" + bill_id.ToString ();
            string responseString;
            try {
                var request = (HttpWebRequest) WebRequest.Create (url);
                request.Method = "GET";
                var response = (HttpWebResponse) request.GetResponse ();
                responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
            } catch (WebException) {
                return null;
            }
            List<sale> sales = JsonConvert.DeserializeObject<List<sale>> (responseString);
            return sales;
        }
    }
}
