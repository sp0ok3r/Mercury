using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Helpers
{
    public class SessionData
    {
        public static ulong SteamID { get; set; }

        public static string AccessToken { get; set; }

        public static string RefreshToken { get; set; }

        public static string SessionID { get; set; }

        public static async Task RefreshAccessToken()
        {
            if (string.IsNullOrEmpty(RefreshToken))
                throw new Exception("Refresh token is empty");

            if (IsTokenExpired(RefreshToken))
                throw new Exception("Refresh token is expired");

            string responseStr;
            try
            {
                var postData = new NameValueCollection();
                postData.Add("refresh_token", RefreshToken);
                postData.Add("steamid", SteamID.ToString());
                responseStr = await SteamWeb.POSTRequest("https://api.steampowered.com/IAuthenticationService/GenerateAccessTokenForApp/v1/", null, postData);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to refresh token: " + ex.Message);
            }

            var response = JsonConvert.DeserializeObject<GenerateAccessTokenForAppResponse>(responseStr);
            AccessToken = response.Response.AccessToken;
        }

        public static bool IsAccessTokenExpired()
        {
            if (string.IsNullOrEmpty(AccessToken))
                return true;

            return IsTokenExpired(AccessToken);
        }

        public static bool IsRefreshTokenExpired()
        {
            if (string.IsNullOrEmpty(RefreshToken))
                return true;

            return IsTokenExpired(RefreshToken);
        }

        private static bool IsTokenExpired(string token)
        {
            var tokenComponents = token.Split('.');
            // Fix up base64url to normal base64
            var base64 = tokenComponents[1].Replace('-', '+').Replace('_', '/');

            if (base64.Length % 4 != 0)
            {
                base64 += new string('=', 4 - base64.Length % 4);
            }

            var payloadBytes = Convert.FromBase64String(base64);
            var jwt = JsonConvert.DeserializeObject<SteamAccessToken>(System.Text.Encoding.UTF8.GetString(payloadBytes));

            // Compare expire time of the token to the current time
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() > jwt.exp;
        }

        public static CookieContainer GetCookies()
        {
            if (SessionID == null)
                SessionID = GenerateSessionID();

            var cookies = new CookieContainer();
            foreach (string domain in new string[] { "steamcommunity.com" , "store.steampowered.com", "help.steampowered.com" })
            {
                cookies.Add(new Cookie("steamLoginSecure", GetSteamLoginSecure(), "/", domain));
                cookies.Add(new Cookie("sessionid", SessionID, "/", domain));
                // cookies.Add(new Cookie("Steam_Language", "english", "/", domain));
            }
            return cookies;
        }

        public static string GetSteamLoginSecure()
        {
            if (AccessToken.Length > 0)
            {
                return SteamID.ToString() + "%7C%7C" + AccessToken;
            }
            else
            {
                return null;
            }
        }

        private static string GenerateSessionID()
        {
            return GetRandomHexNumber(24);
        }

        private static string GetRandomHexNumber(int digits)
        {
            Random random = new Random();
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        private class SteamAccessToken
        {
            public long exp { get; set; }
        }

        private class GenerateAccessTokenForAppResponse
        {
            [JsonProperty("response")]
            public GenerateAccessTokenForAppResponseResponse Response;
        }

        private class GenerateAccessTokenForAppResponseResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
        }
    }
}