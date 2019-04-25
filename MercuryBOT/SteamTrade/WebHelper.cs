/*_ ___  ______ ________    __ ______
\  \  /  / |   ___| |__ __|  /  \     |   ___|
 \  \/  /  |  |___     |  |    / /\ \    |  |__
  |    |   |   ___|    |  |   /  __  \    \__  \
 /	/\  \  |  |___     |  |  /  /  \  \   ___|  |
/__/  \__\ |______|    |__| /__/    \__\ |______|
Written by Paul "Xetas" Abramov
*/


using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Web;

namespace MercuryBOT.SteamTrade
{
    public class WebHelper
    {
        public CookieContainer m_CookieContainer;

        /// <summary>
        /// Get the response for the given url and data
        /// Using a stream and a streamreader we want to convert the response to a string so we can work with the response
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_data"></param>
        /// <param name="_isGetMethod"></param>
        /// <param name="_referer"></param>
        /// <returns></returns>
        public string GetStringFromRequest(string _url, NameValueCollection _data = null, bool _isGetMethod = true, string _referer = null)
        {
            using (HttpWebResponse response = SendWebRequest(_url, _data, _isGetMethod, _referer))
            {
                if (response == null)
                {
                    return "";
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                    {
                        return "";
                    }
                    else
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse the NameValueCollection we have passed into a string with a "&" between the data, this is the way steam wants it
        /// If it is a GET-Request and the datastring is not null, set a "?" between the passed url and the created datastring
        /// Create the request to the given URL
        /// Set the needed informations for the request
        /// If the request is a GET-Request or a POST-Request without data, start it instantly
        /// If the request is a POST-Request AND has data, convert the data into a stream so we can send the information via the request
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_data"></param>
        /// <param name="_isGetMethod"></param>
        /// <param name="_referer"></param>
        /// <returns></returns>
        public HttpWebResponse SendWebRequest(string _url, NameValueCollection _data = null, bool _isGetMethod = true, string _referer = null)
        {
            string dataString = _data == null ? null : string.Join("&", Array.ConvertAll(_data.AllKeys, _key => $"{HttpUtility.UrlEncode(_key)}={HttpUtility.UrlEncode(_data[_key])}"));

            if (_isGetMethod && !string.IsNullOrEmpty(dataString))
            {
                _url += "?" + dataString;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);

            request.Method = _isGetMethod ? "get" : "post";

            //  The Data has to be encoded, here we set the type which has to be used so we match the steam ones
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            //  Set the timeout to 50 seconds, so we do not stuck in the request
            request.Timeout = 50000;

            //  Set the referer thi will be needed if we are going to accept/decline tradeoffers
            request.Referer = string.IsNullOrEmpty(_referer) ? "http://steamcommunity/trade/1" : _referer;

            //  Set the cookieContainer, which holds our sessionID, steamLogin and steamLoginSecure, so steam knows we have a active session
            request.CookieContainer = m_CookieContainer;

            if (_isGetMethod || string.IsNullOrEmpty(dataString))
            {
                return TrySendRequest(request);
            }
            else
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);
                request.ContentLength = dataBytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(dataBytes, 0, dataBytes.Length);
                }

                return TrySendRequest(request);
            }
        }

        /// <summary>
        /// Beautify code
        /// 
        /// Try to get a response from the request we created
        /// Catch any error and return it as a webresponse
        /// </summary>
        /// <param name="_webRequest"></param>
        /// <returns></returns>
        private HttpWebResponse TrySendRequest(HttpWebRequest _webRequest)
        {
            try
            {
                return _webRequest.GetResponse() as HttpWebResponse;

               
            }
            catch (WebException exception)
            {
                HttpWebResponse response = exception.Response as HttpWebResponse;
                if (response != null)
                {
                    return response;
                }

                throw;
            }
        }
    }
}
