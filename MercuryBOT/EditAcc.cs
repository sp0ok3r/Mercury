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
using Win32Interop.Methods;
using System.Drawing;
using System.Linq;

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
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));

            foreach (var button in this.Controls.OfType<MetroFramework.Controls.MetroButton>())
            {
                IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, button.Width, button.Height, 5, 5);
                button.Region = Region.FromHrgn(ptr);
                Gdi32.DeleteObject(ptr);
            }
        }

        private void EditAcc_Load(object sender, EventArgs e)
        {
            EditSelected(Main.SelectedUser);
            //combox_defaultState.SelectedIndex = 1; ver se tem numero antes
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
                    combox_defaultState.SelectedIndex = a.LoginState;
                    txtBox_user.Text = a.username;
                    txtBox_pw.Text = a.password;
                    txtBox_webapi.Text = a.APIWebKey;
                    selectedSteamID = a.SteamID;
                    toggle_chatLogger.Checked = a.ChatLogger;
                }
            }

            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));


            if (SettingsList.startupAcc == selectedSteamID && SettingsList.startupAcc.ToString().Length > 0)
            {
                toggle_autoLogin.Enabled = true;
                toggle_autoLogin.Checked = true;

            }
            if (SettingsList.startupAcc != 0 && SettingsList.startupAcc != selectedSteamID)
            {
                toggle_autoLogin.Enabled = false;
            }
            
            if (selectedSteamID == 0)
            {
                toggle_autoLogin.Enabled = false;
                toggle_autoLogin.Checked = false; // added
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
                    a.LoginState = combox_defaultState.SelectedIndex;
                    a.password = txtBox_pw.Text;
                    a.APIWebKey = txtBox_webapi.Text;
                    a.ChatLogger = toggle_chatLogger.Checked;
                }
            }
            File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented));

            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            
            if (!toggle_autoLogin.Checked){
                SettingsList.startupAcc = 0;
            }else{
                SettingsList.startupAcc = selectedSteamID;
            }
            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(SettingsList, Formatting.Indented));
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
