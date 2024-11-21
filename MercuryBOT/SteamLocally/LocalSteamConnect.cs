using Mercury.MetroMessageBox;
using MercuryBOT;
using Steam4NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.SteamLocally
{
    public class LocalSteamConnect
    {

        private static ISteamClient012 _steamClient012;
        private static ISteamApps001 _steamApps001;

        private static bool ConnectToSteam()
        {
            if (!Steamworks.Load(true))
            {
                CustomMetroMessageBox.Show(Main.ActiveForm,"Steamworks failed to load.");
                return false;
            }

            _steamClient012 = Steamworks.CreateInterface<ISteamClient012>();
            if (_steamClient012 == null)
            {
                CustomMetroMessageBox.Show(Main.ActiveForm, "Failed to create Steam Client inferface.");
                return false;
            }

            int pipe = _steamClient012.CreateSteamPipe();
            if (pipe == 0)
            {
                CustomMetroMessageBox.Show(Main.ActiveForm, "Failed to create Steam pipe.");
                return false;
            }

            int user = _steamClient012.ConnectToGlobalUser(pipe);
            if (user == 0)
            {
                CustomMetroMessageBox.Show(Main.ActiveForm, "Failed to connect to Steam user. (No game in this Account!)");
                return false;
            }

            _steamApps001 = _steamClient012.GetISteamApps<ISteamApps001>(user, pipe);
            if (_steamApps001 == null)
            {
                CustomMetroMessageBox.Show(Main.ActiveForm, "Failed to create Steam Apps inferface.");
                return false;
            }

            return true;
        }
    }
}