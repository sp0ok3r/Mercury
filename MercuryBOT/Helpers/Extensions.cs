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
using System.IO;
using MetroFramework.Components;
using MetroFramework.Forms;
using Newtonsoft.Json;
using MercuryBOT.User2Json;
using MetroFramework;
using SteamKit2;
using System.Collections.Generic;
using System.Net;
using Microsoft.Win32;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MercuryBOT.Helpers
{
    public static class Extensions
    {
        #region Steam Extensions
        public static bool IsSteamid32(string input) => input.StartsWith("STEAM_0:");

        public static bool IsSteamid64(string input) => (input.Length == 17) && input.StartsWith("7656");

        public static bool IsSteamURL(string input)
        {
            string url = input.Replace("https://", "").Replace("http://", "");
            return url.Contains("steamcommunity.com/id/") || url.Contains("steamcommunity.com/profiles/");
        }

        public static string ToSteamID32(string input)
        {
            Int64 steamId1 = Convert.ToInt64(input.Substring(0)) % 2;
            Int64 steamId2a = Convert.ToInt64(input.Substring(0, 4)) - 7656;
            Int64 steamId2b = Convert.ToInt64(input.Substring(4)) - 1197960265728;
            steamId2b = steamId2b - steamId1;

            return "STEAM_0:" + steamId1 + ":" + ((steamId2a + steamId2b) / 2);
        }

        public static string ToSteamID64(string input)
        {
            string[] split = input.Replace("STEAM_", "").Split(':');
            return (76561197960265728 + (Convert.ToInt64(split[2]) * 2) + Convert.ToInt64(split[1])).ToString();
        }

        public static string ToSteamURL(string input) => $"https://steamcommunity.com/profiles/{(IsSteamid32(input) ? ToSteamID64(input) : input)}";


        public static string AllToSteamId32(string input)
        {
            if (IsSteamid32(input))
            {
                return input;
            }
            else if (IsSteamid64(input))
            {
                return ToSteamID32(input);
            }
            else if (IsSteamURL(input) && input.Contains("steamcommunity.com/profiles/"))
            {
                return ToSteamID32(input.Replace("https://steamcommunity.com/profiles/", "").Replace("http://steamcommunity.com/profiles/", ""));
            }
            else if (input.Contains("steamcommunity.com/id/"))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Invalid link (Link accepted: https://steamcommunity.com/profiles/1337)");
            }
            return String.Empty;

        }
        public static string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
        public static string AllToSteamId64(string input)
        {
            if (IsSteamid32(input))
            {
                return ToSteamID64(input); //("765" + (input + 61197960265728)); // test ???
            }
            else if (IsSteamid64(input))
            {
                return input;
            }
            else if (IsSteamURL(input) && input.Contains("steamcommunity.com/profiles/"))
            {
                return input.Replace("https://steamcommunity.com/profiles/", "").Replace("/", "");
            }
            else if (input.Contains("steamcommunity.com/id/"))
            {
                return ResolveVanityURL(input);
            }

            return String.Empty;
        }

        public static string AllToSteamId3(ulong input)
        {
            string id3 = new String(new SteamID(input).Render(true).Where(Char.IsDigit).ToArray());
            return id3;
        }

        public static string ResolveVanityURL(string ProfileURL)// fast way, without api key
        {
            var RespSteamProfile = new WebClient().DownloadString(ProfileURL + "?xml=1"); // 6520iq
            return Between(RespSteamProfile, "<steamID64>", "</steamID64>");
        }

        public static string ResolveAvatar(string steamid64)// fast way, without api key
        {
            var RespSteamProfile = new WebClient().DownloadString("https://steamcommunity.com/profiles/" + AllToSteamId64(steamid64) + "?xml=1"); // 6521iq
            return Between(RespSteamProfile, "<avatarMedium><![CDATA[", "]]></avatarMedium>");
        }

        public static List<EPersonaState> statesList = new List<EPersonaState> {
                        EPersonaState.Offline,
                        EPersonaState.Online,
                        EPersonaState.Busy,
                        EPersonaState.Away,
                        EPersonaState.Snooze,
                        EPersonaState.LookingToTrade,
                        EPersonaState.LookingToPlay,
                        EPersonaState.Invisible };

        public static string SteamLocation;

        public static void Init()
        {
            var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Valve\\Steam");

            if (key == null)
            {
                key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Valve\\Steam");
            }

            if (key != null && key.GetValue("SteamPath") is string)
            {
                SteamLocation = key.GetValue("SteamPath").ToString();
            }
        }


        public static bool KillSteam()
        {
            var p = new Process();


            if (Process.GetProcesses().Any(x => x.ProcessName == "steam"))
            {

                if (File.Exists(Extensions.SteamLocation + "/steam.exe"))
                {
                    p.StartInfo = new ProcessStartInfo(Extensions.SteamLocation + "/steam.exe", "-shutdown"); // pervent icon from staying on windows taskbar
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.Start();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return true;
            }

        }


        #endregion

        #region File Methods
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
        #endregion

        #region SafeInvokes
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
        #endregion

        #region Theme Methods
        private static MetroColorStyle FormStyle;
        public static void SetStyle(this IContainer container, MetroForm ownerForm)
        {
            if (!File.Exists(Program.SettingsJsonFile))
            {
                File.WriteAllText(Program.SettingsJsonFile, "{}");
            }

            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            FormStyle = (MetroColorStyle)Convert.ToUInt32(SettingsList.startupColor);

            if (container == null)
            {
                container = new Container();
            }
            var manager = new MetroStyleManager(container);
            manager.Owner = ownerForm;
            container.SetDefaultStyle(ownerForm, FormStyle);
        }
        public static void SetDefaultStyle(this IContainer contr, MetroForm owner, MetroColorStyle style)
        {
            MetroStyleManager manager = FindManager(contr, owner);
            manager.Style = style;
            owner.Style = style;
        }
        public static void SetDefaultTheme(this IContainer contr, MetroForm owner, MetroThemeStyle thme)
        {
            MetroStyleManager manager = FindManager(contr, owner);
            manager.Theme = thme;
        }
        private static MetroStyleManager FindManager(IContainer contr, MetroForm owner)
        {
            MetroStyleManager manager = null;
            foreach (IComponent item in contr.Components)
                try
                {
                    if (((MetroStyleManager)item).Owner == owner)
                    {
                        manager = (MetroStyleManager)item;
                    }
                }
                catch (InvalidCastException)
                {
                    //Console.WriteLine("[Theme] - Cannot convert some styles.");
                }
            return manager;
        }
        #endregion

        public static List<string> notifEffects = new List<string> {
                         Notification.NotificationForm.AW_HOR_POSITIVE.ToString(),
                         Notification.NotificationForm.AW_HOR_NEGATIVE.ToString(),
                         Notification.NotificationForm.AW_VER_POSITIVE.ToString(),
                         Notification.NotificationForm.AW_VER_NEGATIVE.ToString() };


        #region "Refresh Notification Area Icons"
        
        public const int WM_PAINT = 15;
        [DllImport("USER32.DLL")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int character, IntPtr lpsText);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        public static void RefreshTrayArea()
        {
            IntPtr shelltraywnd = FindWindow("Shell_TrayWnd", null);
            //Send WM_PAINT Message to paint System Tray which will refresh it.
            IntPtr systemTrayHandle = FindWindowEx(shelltraywnd, IntPtr.Zero, "TrayNotifyWnd", null);
            SendMessage(systemTrayHandle, WM_PAINT, 0, IntPtr.Zero);

            //SendMessage(shelltraywnd, WM_PAINT, 0, IntPtr.Zero);
        }


        #endregion




        #region csgoPath 
        /// <summary>
        /// Returns the location of the CS:GO installation, or null if it's unable to find it.  moritzuehling
        /// </summary>
        /// <returns></returns>
        public static string GetCSGODir()
        {
            string steamPath = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam", "SteamPath", "");
            string pathsFile = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");

            if (!File.Exists(pathsFile))
                return null;

            List<string> libraries = new List<string>();
            libraries.Add(Path.Combine(steamPath));

            var pathVDF = File.ReadAllLines(pathsFile);
            // Okay, this is not a full vdf-parser, but it seems to work pretty much, since the 
            // vdf-grammar is pretty easy. Hopefully it never breaks. I'm too lazy to write a full vdf-parser though. 
            Regex pathRegex = new Regex(@"\""(([^\""]*):\\([^\""]*))\""");
            foreach (var line in pathVDF)
            {
                if (pathRegex.IsMatch(line))
                {
                    string match = pathRegex.Matches(line)[0].Groups[1].Value;

                    // De-Escape vdf. 
                    libraries.Add(match.Replace("\\\\", "\\"));
                }
            }

            foreach (var library in libraries)
            {
                string csgoPath = Path.Combine(library, "steamapps\\common\\Counter-Strike Global Offensive\\csgo");
                if (Directory.Exists(csgoPath))
                {
                    return csgoPath;
                }
            }

            return null;
        }
        #endregion

    }
}