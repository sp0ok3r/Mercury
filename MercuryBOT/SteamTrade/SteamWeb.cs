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

namespace MercuryBOT.SteamTrade
{
    /// <summary>
    /// SteamWeb class to create an API endpoint to the Steam Web.
    /// </summary>
    public class SteamWeb
    {
        /// <summary>
        /// Base steam community domain.
        /// </summary>
        public const string SteamCommunityDomain = "steamcommunity.com";

        /// <summary>
        /// Token of steam. Generated after login.
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Session id of Steam after Login.
        /// </summary>
        public string SessionID { get; private set; }

        /// <summary>
        /// Token secure as string. It is generated after the Login.
        /// </summary>
        public string TokenSecure { get; private set; }

        /// <summary>
        /// The Accept-Language header when sending all HTTP requests. Default value is determined according to the constructor caller thread's culture.
        /// </summary>
        public string AcceptLanguageHeader { get { return acceptLanguageHeader; } set { acceptLanguageHeader = value; } }
        private string acceptLanguageHeader = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en" ? Thread.CurrentThread.CurrentCulture.ToString() + ",en;q=0.8" : Thread.CurrentThread.CurrentCulture.ToString() + "," + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + ";q=0.8,en;q=0.6";


        /// <summary>
        /// CookieContainer to save all cookies during the Login. 
        /// </summary>
        private CookieContainer _cookies = new CookieContainer();

        /// <summary>
        /// This method is using the Request method to return the full http stream from a web request as string.
        /// </summary>
        /// <param name="url">URL of the http request.</param>
        /// <param name="method">Gets the HTTP data transfer method (such as GET, POST, or HEAD) used by the client.</param>
        /// <param name="data">A NameValueCollection including Headers added to the request.</param>
        /// <param name="ajax">A bool to define if the http request is an ajax request.</param>
        /// <param name="referer">Gets information about the URL of the client's previous request that linked to the current URL.</param>
        /// <param name="fetchError">If true, response codes other than HTTP 200 will still be returned, rather than throwing exceptions</param>
        /// <returns>The string of the http return stream.</returns>
        /// <remarks>If you want to know how the request method works, use: <see cref="SteamWeb.Request"/></remarks>
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

        /// <summary>
        /// Custom wrapper for creating a HttpWebRequest, edited for Steam.
        /// </summary>
        /// <param name="url">Gets information about the URL of the current request.</param>
        /// <param name="method">Gets the HTTP data transfer method (such as GET, POST, or HEAD) used by the client.</param>
        /// <param name="data">A NameValueCollection including Headers added to the request.</param>
        /// <param name="ajax">A bool to define if the http request is an ajax request.</param>
        /// <param name="referer">Gets information about the URL of the client's previous request that linked to the current URL.</param>
        /// <param name="fetchError">Return response even if its status code is not 200</param>
        /// <returns>An instance of a HttpWebResponse object.</returns>
        public HttpWebResponse Request(string url, string method, NameValueCollection data = null, bool ajax = true, string referer = "", bool fetchError = true)
        {
            // Append the data to the URL for GET-requests.
            bool isGetMethod = (method.ToLower() == "get");
            string dataString = (data == null ? null : String.Join("&", Array.ConvertAll(data.AllKeys, key =>
                // ReSharper disable once UseStringInterpolation
                string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(data[key]))
            )));

            // Example working with C# 6
            // string dataString = (data == null ? null : String.Join("&", Array.ConvertAll(data.AllKeys, key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(data[key])}" )));

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
                catch (WebException ex)
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
        
        /// <remarks>Should this one doesnt work anymore, use <see cref="SteamWeb.DoLogin"/></remarks>
        /// <param name="myUniqueId">Id what you get to login.</param>
        /// <param name="client">An instance of a SteamClient.</param>
        /// <param name="myLoginKey">Login Key of your account.</param>
        /// <returns>A bool, which is true if the login was successful.</returns>
        public bool Authenticate(string myUniqueId, SteamClient client, string myLoginKey)
        {

            // Fix "The request was aborted: Could not create SSL/TLS secure channel" error
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
           // ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            Token = TokenSecure = String.Empty;
            SessionID = Convert.ToBase64String(Encoding.UTF8.GetBytes(myUniqueId));

            _cookies = new CookieContainer();

            using (dynamic userAuth = WebAPI.GetInterface("ISteamUserAuth"))
            {
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
                    authResult = userAuth.AuthenticateUser(
                        steamid: client.SteamID.ConvertToUInt64(),
                        sessionkey: HttpUtility.UrlEncode(cryptedSessionKey),
                        encrypted_loginkey: HttpUtility.UrlEncode(cryptedLoginKey),
                        method: "POST",
                        secure: true
                        );

                }
                catch (Exception)
                {
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

        /// <summary>
        /// Helper method to verify our precious cookies.
        /// </summary>
        /// <returns>true if cookies are correct; false otherwise</returns>
        public bool VerifyCookies()
        {
            using (HttpWebResponse response = Request("http://steamcommunity.com/", "HEAD"))
            {
                return response.Cookies["steamLogin"] == null || !response.Cookies["steamLogin"].Value.Equals("deleted");
            }
        }

        /// <summary>
        /// Method to submit cookies to Steam after Login.
        /// </summary>
        /// <param name="cookies">Cookiecontainer which contains cookies after the login to Steam.</param>
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
            // Added content-length because it is required.
            w.ContentLength = 0;
            w.GetResponse().Close();
        }

        /// <summary>
        /// Method to convert a Hex to a byte.
        /// </summary>
        /// <param name="hex">Input parameter as string.</param>
        /// <returns>The byte value.</returns>
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

        /// <summary>
        /// Get the Hex value as int out of an char.
        /// </summary>
        /// <param name="hex">Input parameter.</param>
        /// <returns>A Hex Value as int.</returns>
        private int GetHexVal(char hex)
        {
            int val = hex;
            return val - (val < 58 ? 48 : 55);
        }

        /// <summary>
        /// Method to allow all certificates.
        /// </summary>
        /// <param name="sender">An object that contains state information for this validation.</param>
        /// <param name="certificate">The certificate used to authenticate the remote party.</param>
        /// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="policyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>Always true to accept all certificates.</returns>
        public bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
    }

    // JSON Classes
    // These classes are used to deserialize response strings from the login:
    // Example of a return string: {"success":true,"publickey_mod":"XXXX87144BF5B2CABFEC24E35655FDC5E438D6064E47D33A3531F3AAB195813E316A5D8AAB1D8A71CB7F031F801200377E8399C475C99CBAFAEFF5B24AE3CF64BXXXXB2FDBA3BC3974D6DCF1E760F8030AB5AB40FA8B9D193A8BEB43AA7260482EAD5CE429F718ED06B0C1F7E063FE81D4234188657DB40EEA4FAF8615111CD3E14CAF536CXXXX3C104BE060A342BF0C9F53BAAA2A4747E43349FF0518F8920664F6E6F09FE41D8D79C884F8FD037276DED0D1D1D540A2C2B6639CF97FF5180E3E75224EXXXX56AAA864EEBF9E8B35B80E25B405597219BFD90F3AD9765D81D148B9500F12519F1F96828C12AEF77D948D0DC9FDAF8C7CC73527ADE7C7F0FF33","publickey_exp":"010001","timestamp":"241590850000","steamid":"7656119824534XXXX","token_gid":"c35434c0c07XXXX"}

    /// <summary>
    /// Class to Deserialize the json response strings of the getResKey request. See: <see cref="SteamWeb.DoLogin"/>
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GetRsaKey
    {
        public bool success { get; set; }

        public string publickey_mod { get; set; }

        public string publickey_exp { get; set; }

        public string timestamp { get; set; }
    }

    // Examples:
    // For not accepted SteamResult:
    // {"success":false,"requires_twofactor":false,"message":"","emailauth_needed":true,"emaildomain":"gmail.com","emailsteamid":"7656119824534XXXX"}
    // For accepted SteamResult:
    // {"success":true,"requires_twofactor":false,"login_complete":true,"transfer_url":"https:\/\/store.steampowered.com\/login\/transfer","transfer_parameters":{"steamid":"7656119824534XXXX","token":"XXXXC39589A9XXXXCB60D651EFXXXX85578AXXXX","auth":"XXXXf1d9683eXXXXc76bdc1888XXXX29","remember_login":false,"webcookie":"XXXX4C33779A4265EXXXXC039D3512DA6B889D2F","token_secure":"XXXX63F43AA2CXXXXC703441A312E1B14AC2XXXX"}}

    /// <summary>
    /// Class to Deserialize the json response strings after the login. See: <see cref="SteamWeb.DoLogin"/>
    /// </summary>
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
