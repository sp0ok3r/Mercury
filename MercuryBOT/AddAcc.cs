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
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Win32Interop.Methods;
using System.Drawing;

namespace MercuryBOT
{
    public partial class AddAcc : MetroFramework.Forms.MetroForm
    {
        public AddAcc()
        {
            InitializeComponent(); this.Activate();
            this.components.SetStyle(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));

            IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, btn_addAcc.Width, btn_addAcc.Height, 5, 5);
            btn_addAcc.Region = Region.FromHrgn(ptr);
            Gdi32.DeleteObject(ptr);


        }
        private void btn_addAcc_Click(object sender, EventArgs e)
        {
            AddAccJson(txtBox_adminID.Text, txtBox_AccUser.Text, txtBox_AccPW.Text);
        }

        public void AddAccJson(string Admin, string user, string password)
        {
            try
            {
                var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var a in list.Accounts)
                {
                    if (a.username == user)
                    {
                        InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Duplicate username found!");
                        return;
                    }
                }

                ulong UserSteamID = 0;
                ulong AdminConverted = 0;
                //if (Admin == "")
                if (string.IsNullOrEmpty(Admin))
                {
                    AdminConverted = 0;
                }
                else
                {
                    AdminConverted = Convert.ToUInt64(Admin);
                }

                list.Accounts.Add(new UserAccounts
                {
                    AdminID = AdminConverted,
                    LoginState = 1,
                    username = user,
                    password = password,
                    APIWebKey = "",//0
                    SteamID = UserSteamID,
                    ChatLogger = false,
                    Games = new List<Game>(),
                    AFKMessages = new List<UserSettings.CustomMessages>(),
                    MsgRecipients = new List<string>()

                });

                var convertedJson = JsonConvert.SerializeObject(list, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Populate,
                    Formatting = Formatting.Indented
                });

                File.WriteAllText(Program.AccountsJsonFile, convertedJson);
                Close();
            }
            catch
            {
                // log error
            }
        }

        #region link2json
        private void metroLink_AccountsJSONPath_Click(object sender, EventArgs e)
        {
            Process.Start(Program.AccountsJsonFile);
        }
        #endregion

    }
}
