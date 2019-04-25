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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace MercuryBOT.Helpers
{
    public static class Extensions
    {
        public static DateTime GetTime(string timeStamp)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var lTime = long.Parse($@"{timeStamp}0000000");
            var toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        public class SteamRep
        {
            public bool isSCAM(string steamId32)
            {
                string api = "http://steamrep.com/id2rep.php?steamID32=" + steamId32;
                WebClient client = new WebClient();
                string result = client.DownloadString(api);
                return (result.IndexOf("SCAMMER") > -1);
            }
        }


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
               (
                   int nLeftRect,     // x-coordinate of upper-left corner
                   int nTopRect,      // y-coordinate of upper-left corner
                   int nRightRect,    // x-coordinate of lower-right corner
                   int nBottomRect,   // y-coordinate of lower-right corner
                   int nWidthEllipse, // height of ellipse
                   int nHeightEllipse // width of ellipse
               );




        //https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
        public static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string SizeSuffix(long value, int decimalPlaces = 0)
        {
            if (value < 0)
            {
                throw new ArgumentException("Bytes should not be negative", "value");
            }
            var mag = (int)Math.Max(0, Math.Log(value, 1024));
            var adjustedSize = Math.Round(value / Math.Pow(1024, mag), decimalPlaces);
            return String.Format("{0} {1}", adjustedSize, SizeSuffixes[mag]);
        }
        //
        public static string ToFileSize(this int source)
        {
            return ToFileSize(Convert.ToInt64(source));
        }

        public static string ToFileSize(this long source)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(source);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " GB");
            }
            else if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " MB");
            }
            else if (bytes >= byteConversion) //KB Range
            {
                return string.Concat(Math.Round(bytes / byteConversion, 2), " KB");
            }
            else //Bytes
            {
                return string.Concat(bytes, " Bytes");
            }
        }


        public static void Invoke(ISynchronizeInvoke sync, Action action)
        {
            if (!sync.InvokeRequired)
            {
                action();
            }
            else
            {
                object[] args = new object[] { };
                sync.Invoke(action, args);
            }
        }

        public static void SafeInvoke(this Action action)
        {
            if (action == null)
                return;

            action();
        }

        public static void SafeInvoke<T>(this Action<T> action, T obj)
        {
            if (action == null)
                return;

            action(obj);
        }

        public static void SafeInvoke(this EventHandler eventHandler, object sender, EventArgs args)
        {
            if (eventHandler == null)
                return;

            eventHandler(sender, args);
        }

        public static void SafeInvoke<T>(this EventHandler<T> eventHandler, object sender, T args)
            where T : EventArgs
        {
            if (eventHandler == null)
                return;

            eventHandler(sender, args);
        }

        public static void SafeInvoke(this NotifyCollectionChangedEventHandler eventHandler, object sender, NotifyCollectionChangedEventArgs args)
        {
            if (eventHandler == null)
                return;

            eventHandler(sender, args);
        }

        public static void SafeInvoke(this PropertyChangedEventHandler eventHandler, object sender, PropertyChangedEventArgs args)
        {
            if (eventHandler == null)
                return;

            eventHandler(sender, args);
        }
    }
}
