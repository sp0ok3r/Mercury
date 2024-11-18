using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Helpers
{
    public class SteamWeb
    {
        public static string MOBILE_APP_USER_AGENT = "Dalvik/2.1.0 (Linux; U; Android 9; Valve Steam App Version/3)";
        public static string BROWSER_APP_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36";
        public static string http_agent = "okhttp/3.12.12";

        public static async Task<string> GETRequest(string url, CookieContainer cookies)
        {
            string response;
            using (CookieAwareWebClient wc = new CookieAwareWebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.CookieContainer = cookies;
                wc.Headers[HttpRequestHeader.UserAgent] = SteamWeb.http_agent;
                response = await wc.DownloadStringTaskAsync(url);
            }
            return response;
        }

        public static async Task<string> POSTRequest(string url, CookieContainer cookies, NameValueCollection body)
        {
            if (body == null)
                body = new NameValueCollection();

            string response;
            using (CookieAwareWebClient wc = new CookieAwareWebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.CookieContainer = cookies;
                wc.Headers[HttpRequestHeader.UserAgent] = SteamWeb.http_agent;
                byte[] result = await wc.UploadValuesTaskAsync(new Uri(url), "POST", body);
                response = Encoding.UTF8.GetString(result);
            }
            return response;
        }
    }
}