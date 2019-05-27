/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using System;
using System.IO;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SteamKit2;
using System.Threading;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercuryBOT.SteamCommunity
{
    public class SteamWeb
    {

        public const string SteamCommunityDomain = "steamcommunity.com";

        public string Token { get; private set; }

        public string SessionID { get; private set; }


        public string TokenSecure { get; private set; }

        public string AcceptLanguageHeader { get { return acceptLanguageHeader; } set { acceptLanguageHeader = value; } }
        private string acceptLanguageHeader = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en" ? Thread.CurrentThread.CurrentCulture.ToString() + ",en;q=0.8" : Thread.CurrentThread.CurrentCulture.ToString() + "," + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + ";q=0.8,en;q=0.6";

        private CookieContainer _cookies = new CookieContainer();

        public string Fetch(string url, string method, NameValueCollection data = null, bool ajax = true, string referer = "", bool fetchError = true)
        {
            // Reading the response as stream and read it to the end. After that happened return the result as string.
            using (HttpWebResponse response = Request(url, method, data, ajax, referer, fetchError))
            {
                if (response == null)
                    return String.Empty;

                using (Stream responseStream = response.GetResponseStream())
                {
                    // If the response stream is null it cannot be read. So return an empty string.
                    if (responseStream == null)
                    {
                        return "";
                    }
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public HttpWebResponse Request(string url, string method, NameValueCollection data = null, bool ajax = true, string referer = "", bool fetchError = true)
        {
            // Append the data to the URL for GET-requests.
            bool isGetMethod = (method.ToLower() == "get");
            string dataString = (data == null ? null : String.Join("&", Array.ConvertAll(data.AllKeys, key =>
                // ReSharper disable once UseStringInterpolation
                string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(data[key]))
            )));

            // Append the dataString to the url if it is a GET request.
            if (isGetMethod && !string.IsNullOrEmpty(dataString))
            {
                url += (url.Contains("?") ? "&" : "?") + dataString;
            }

            // Setup the request.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json, text/javascript;q=0.9, */*;q=0.5";
            request.Headers[HttpRequestHeader.AcceptLanguage] = AcceptLanguageHeader;
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            // request.Host is set automatically.
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            request.Referer = string.IsNullOrEmpty(referer) ? "http://steamcommunity.com/trade/1" : referer;
            request.Timeout = 50000; // Timeout after 50 seconds.
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            // If the request is an ajax request we need to add various other Headers, defined below.
            if (ajax)
            {
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("X-Prototype-Version", "1.7");
            }

            // Cookies
            request.CookieContainer = _cookies;

            // If the request is a GET request return now the response. If not go on. Because then we need to apply data to the request.
            if (isGetMethod || string.IsNullOrEmpty(dataString))
            {
                try
                {
                    return request.GetResponse() as HttpWebResponse;
                }
                catch (WebException)
                {
                    return null;
                }
            }

            // Write the data to the body for POST and other methods.
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);
            request.ContentLength = dataBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(dataBytes, 0, dataBytes.Length);
            }

            // Get the response and return it.
            try
            {
                return request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                //this is thrown if response code is not 200
                if (fetchError)
                {
                    var resp = ex.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        Notification.NotifHelper.MessageBox.Show("Alert", "Login again or steamcommunity is down");
                        Console.WriteLine("For more info; visit this link : ");
                        Console.WriteLine(url);
                        return resp;
                    }
                }
                throw;
            }
        }

        public bool Authenticate(string myUniqueId, SteamClient client, string myLoginKey)
        {
            Token = TokenSecure = String.Empty;
            SessionID = Convert.ToBase64String(Encoding.UTF8.GetBytes(myUniqueId));

            _cookies = new CookieContainer();

            using (dynamic userAuth = WebAPI.GetInterface("ISteamUserAuth"))
            {

                // userAuth.Timeout = TimeSpan.FromSeconds(5);
                // Generate an AES session key.
                var sessionKey = CryptoHelper.GenerateRandomBlock(32);

                // rsa encrypt it with the public key for the universe we're on
                byte[] cryptedSessionKey = null;

                using (RSACrypto rsa = new RSACrypto(KeyDictionary.GetPublicKey(client.Universe)))
                {
                    cryptedSessionKey = rsa.Encrypt(sessionKey);
                }
                byte[] loginKey = new byte[20];
                Array.Copy(Encoding.ASCII.GetBytes(myLoginKey), loginKey, myLoginKey.Length);

                // AES encrypt the loginkey with our session key.
                byte[] cryptedLoginKey = CryptoHelper.SymmetricEncrypt(loginKey, sessionKey);

                KeyValue authResult;
                
                // Get the Authentification Result.
                try
                {
                    Dictionary<string, object> sessionArgs = new Dictionary<string, object>()
                    {
                        { "steamid", client.SteamID.ConvertToUInt64() },
                        { "sessionkey", cryptedSessionKey },
                        { "encrypted_loginkey", cryptedLoginKey }
                    };
                    authResult = userAuth.Call(HttpMethod.Post, "AuthenticateUser", args: sessionArgs);
                }
                catch (Exception hehe)
                {
                    Console.WriteLine("Unable to make AuthenticateUser API Request: {0}", hehe.Message);

                    Token = TokenSecure = null;
                    return false;
                }

                Token = authResult["token"].AsString();
                TokenSecure = authResult["tokensecure"].AsString();

                // Adding cookies to the cookie container.
                _cookies.Add(new Cookie("sessionid", SessionID, string.Empty, SteamCommunityDomain));
                _cookies.Add(new Cookie("steamLogin", Token, string.Empty, SteamCommunityDomain));
                _cookies.Add(new Cookie("steamLoginSecure", TokenSecure, string.Empty, SteamCommunityDomain));
                _cookies.Add(new Cookie("Steam_Language", "english", string.Empty, SteamCommunityDomain));

                Console.WriteLine("Crukies: "+VerifyCookies());
                return true;
            }
        }

        public void Authenticate(System.Collections.Generic.IEnumerable<Cookie> cookies)
        {
            var cookieContainer = new CookieContainer();
            string token = null;
            string tokenSecure = null;
            string sessionId = null;
            foreach (var cookie in cookies)
            {
                if (cookie.Name == "sessionid")
                    sessionId = cookie.Value;
                else if (cookie.Name == "steamLogin")
                    token = cookie.Value;
                else if (cookie.Name == "steamLoginSecure")
                    tokenSecure = cookie.Value;
                cookieContainer.Add(cookie);
            }
            if (token == null)
                throw new ArgumentException("Cookie with name \"steamLogin\" is not found.");
            if (tokenSecure == null)
                throw new ArgumentException("Cookie with name \"steamLoginSecure\" is not found.");
            if (sessionId == null)
                throw new ArgumentException("Cookie with name \"sessionid\" is not found.");
            Token = token;
            TokenSecure = tokenSecure;
            SessionID = sessionId;
            _cookies = cookieContainer;
        }

        public bool VerifyCookies()
        {
            using (HttpWebResponse response = Request("http://steamcommunity.com/", "HEAD"))
            {
                return response.Cookies["steamLogin"] == null || !response.Cookies["steamLogin"].Value.Equals("deleted");
            }
        }

        static void SubmitCookies(CookieContainer cookies)
        {
            HttpWebRequest w = WebRequest.Create("https://steamcommunity.com/") as HttpWebRequest;

            // Check, if the request is null.
            if (w == null)
            {
                return;
            }
            w.Method = "POST";
            w.ContentType = "application/x-www-form-urlencoded";
            w.CookieContainer = cookies;
            w.ContentLength = 0;
            w.GetResponse().Close();
        }

        private byte[] HexToByte(string hex)
        {
            if (hex.Length % 2 == 1)
            {
                throw new Exception("The binary key cannot have an odd number of digits");
            }

            byte[] arr = new byte[hex.Length >> 1];
            int l = hex.Length;

            for (int i = 0; i < (l >> 1); ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        private int GetHexVal(char hex)
        {
            int val = hex;
            return val - (val < 58 ? 48 : 55);
        }

        public bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GetRsaKey
    {
        public bool success { get; set; }
        public string publickey_mod { get; set; }
        public string publickey_exp { get; set; }
        public string timestamp { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SteamResult
    {
        public bool success { get; set; }
        public string message { get; set; }
        public bool captcha_needed { get; set; }
        public string captcha_gid { get; set; }
        public bool emailauth_needed { get; set; }
        public string emailsteamid { get; set; }
    }
}