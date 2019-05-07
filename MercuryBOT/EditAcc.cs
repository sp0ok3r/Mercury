/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using MercuryBOT.Helpers;
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
            this.components.SetStyle(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
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


            if (Settingslist.startupAcc == selectedSteamID && Settingslist.startupAcc.ToString().Length > 0)
            {
                toggle_autoLogin.Enabled = true;
                toggle_autoLogin.Checked = true;
            }
            if (Settingslist.startupAcc != 0 && Settingslist.startupAcc != selectedSteamID)
            {
                toggle_autoLogin.Enabled = false;
            }
            if (selectedSteamID == 0)
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
            File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented));

            var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            
            if (!toggle_autoLogin.Checked){
                Settingslist.startupAcc = 0;
            }else{
                Settingslist.startupAcc = selectedSteamID;
            }

            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(Settingslist, Formatting.Indented));
            Close();
        }

        private void MetroLink_AccountsJSONPath_Click(object sender, EventArgs e)
        {
            Process.Start(Program.AccountsJsonFile);
        }

        private void btn_deleteAcc_Click(object sender, EventArgs e)
        {
            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));

            list.Accounts.RemoveAll(x => x.username == Main.SelectedUser);

            File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented));
            Close();
            Notification.NotifHelper.MessageBox.Show("Info", " Removed " + Main.SelectedUser + " from file.");
        }
    }
}
