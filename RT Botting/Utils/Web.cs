using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tolstoy_Toolkit
{
    internal class Web
    {
        static string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";
        static int timeout = 30000;

        //public static string POST(string url, string postData)
        //{
        //    var client = new RestClient(url);
        //    var request = new RestRequest("Resources", Method.Post);
        //    request.RequestFormat = DataFormat.Json;
        //    request.AddBody(postData);
        //    request.AddHeader("Accept-Language", "en-US,en;q=0.9");
        //    request.AddHeader("X-Requested-With", "XMLHttpRequest");
        //    request.AddHeader("User-Agent", userAgent);

        //    var response = client.Execute(request);
        //    return response.Content;
        //} 

        public static string POST(string url, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //request.Proxy = null;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "*/*";
            request.UserAgent = userAgent;
            request.Timeout = timeout;
            request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
           // request.Proxy = new WebProxy("94.19.8.166", 8080);
            var data = Encoding.UTF8.GetBytes(postData);
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = request.GetResponse();
            //string res = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
            return "";
        }

        public static string GET(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //request.Proxy = null;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.UserAgent = userAgent;
            request.Timeout = timeout;
            request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            // request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            var response = request.GetResponse();
            string res = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
            return res;
        }

        public static async Task<string> RequestGetAsync(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //request.Proxy = null;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.UserAgent = userAgent;
            request.Timeout = timeout;
            request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            WebResponse response = await request.GetResponseAsync();
            return ReadStreamFromResponse(response);
        }
        public static async Task<string> RequestPostAsync(string url, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //request.Proxy = null;
            request.Proxy = new WebProxy("51.15.242.202", 8888);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "*/*";
            request.UserAgent = userAgent;
            request.Timeout = timeout;
            request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");

            var data = Encoding.UTF8.GetBytes(postData);
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            return ReadStreamFromResponse(response);
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                return strContent;
            }
        }

        public static string GetSiteId(string url)
        {
            if (url.Contains("russian.rt.com/inotv"))
                return "3734";
            else if (url.Contains("russian.rt.com"))
                return "3724";
            else if (url.Contains("arabic.rt.com"))
                return "3725";
            else
                return "4194";
        }
    }
}
