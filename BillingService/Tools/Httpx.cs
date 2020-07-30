using System.IO;
using System.Net;
using System.Text;

namespace gate.Tools {
    public class Httpx {
        public static string Get (string url) {
            string responseString;
            try {
                var request = (HttpWebRequest) WebRequest.Create (url);
                request.Method = "GET";
                var response = (HttpWebResponse) request.GetResponse ();
                responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
            } catch {
                return null;
            }
            return responseString;
        }

        public static string Post (string url, string data) {
            var buffer = Encoding.ASCII.GetBytes (data);
            string responseString;
            try {
                var request = (HttpWebRequest) WebRequest.Create (url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = buffer.Length;

                using (var stream = request.GetRequestStream ()) {
                    stream.Write (buffer, 0, buffer.Length);
                }

                var response = (HttpWebResponse) request.GetResponse ();
                responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
            } catch {
                return null;
            }
            return responseString;
        }
    }

}
