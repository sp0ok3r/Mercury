/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

using MercuryBOT.User2Json;
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MercuryBOT
{
    public partial class EditAcc : MetroFramework.Forms.MetroForm
    {

        public EditAcc()
        {
            InitializeComponent();
            this.Activate();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void EditAcc_Load(object sender, EventArgs e)
        {

            EditSelected(Main.SelectedUser);
        }
        private ulong selectedSteamID = 0;
        public void EditSelected(string user)
        {
            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in list.Accounts)
            {
                if (a.username == user)
                {
                    txtBox_adminID.Text = (a.AdminID).ToString();
                    txtBox_user.Text = a.username;
                    txtBox_pw.Text = a.password;
                    txtBox_webapi.Text = a.APIWebKey;
                    selectedSteamID = a.SteamID;
                    toggle_chatLogger.Checked = a.ChatLogger;
                }
            }
            
            var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            if (Settingslist.startupAcc == selectedSteamID)
            {
                toggle_autoLogin.Enabled = true;
                toggle_autoLogin.Checked = true;
            }
            if (Settingslist.startupAcc != 0 && Settingslist.startupAcc != selectedSteamID)
            {
                toggle_autoLogin.Enabled = false;
            }
        }

        private void BTN_SUBMIT_Click(object sender, EventArgs e)
        {
            ulong IDConverted;
            switch (txtBox_adminID.Text)
            {
                case "":
                    IDConverted = 0;
                    break;
                default:
                    IDConverted = Convert.ToUInt64(txtBox_adminID.Text);
                    break;
            }

            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in list.Accounts)
            {
                if (a.username == txtBox_user.Text)
                {
                    a.AdminID = IDConverted;
                    a.password = txtBox_pw.Text;
                    a.APIWebKey = txtBox_webapi.Text;
                    a.ChatLogger = toggle_chatLogger.Checked;
                }
            }

            string output = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(Program.AccountsJsonFile, output);
            


            var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            string SaveAccStartup = JsonConvert.SerializeObject(Settingslist, Formatting.Indented);
            File.WriteAllText(Program.SettingsJsonFile, SaveAccStartup);


            Close();
        }

        private void MetroLink_AccountsJSONPath_Click(object sender, EventArgs e)
        {
            Process.Start(Program.AccountsJsonFile);
        }
    }
}
