using Microsoft.Win32;

namespace MercuryBOT.Helpers
{
    public static class SteamPath
    {
        //Thanks to https://github.com/Rubberduckycooly/Sonic-1-2-Save-Editor
        public static string SteamLocation;

        public static void Init()
        {
            // Gets Steam's Registry Key
            var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Valve\\Steam");
            // If null then try get it from the 64-bit Registry
            if (key == null)
                key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                    .OpenSubKey("SOFTWARE\\Valve\\Steam");
            // Checks if the Key and Value exists.
            if (key != null && key.GetValue("SteamPath") is string)
                SteamLocation = key.GetValue("SteamPath").ToString();
        }
    }
}
