using Mercury;
using SteamKit2.Authentication;
using SteamKit2.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteamKit2.Internal.CMsgRemoteClientBroadcastStatus;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
   

namespace Mercury
{
    public class DeviceAuth : IAuthenticator
    {

        public Task<string> GetDeviceCodeAsync(bool previousCodeWasIncorrect)
        {
            MercuryBOT.SteamGuard SteamGuard = new MercuryBOT.SteamGuard("Phone", HandleLogin.CurrentUsername, previousCodeWasIncorrect);
            SteamGuard.ShowDialog();

            bool UserInputCode = true;
            while (UserInputCode)
            {
                if (MercuryBOT.SteamGuard.AuthCode.Length == 5)
                {
                    UserInputCode = false;
                }
            }


            return Task.FromResult(MercuryBOT.SteamGuard.AuthCode.Trim());
        }

        public Task<string> GetEmailCodeAsync(string email, bool previousCodeWasIncorrect)
        {
            MercuryBOT.SteamGuard SteamGuard = new MercuryBOT.SteamGuard("Email", HandleLogin.CurrentUsername, previousCodeWasIncorrect);// add user name
            SteamGuard.ShowDialog();

            bool UserInputCode = true;
            while (UserInputCode)
            {
                if (MercuryBOT.SteamGuard.AuthCode.Length == 5) // Wait for user input
                {
                    UserInputCode = false;
                }
            }

            return Task.FromResult(MercuryBOT.SteamGuard.AuthCode);
        }

        public Task<bool> AcceptDeviceConfirmationAsync()
        {
            //InfoForm.InfoHelper.CustomMessageBox.Show("🔒 Steam Guard", "Please accept the device confirmation request on Steam Mobile App.");

            return Task.FromResult(false);//false to force 2fa code
        }

        public EAuthSessionGuardType NeedGuardType()
        {
            return EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceConfirmation;
        }
    }
}