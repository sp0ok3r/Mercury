/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MercuryBOT
{
    public partial class AddAcc : MetroFramework.Forms.MetroForm
    {
      
        public AddAcc()
        {
            InitializeComponent(); this.Activate();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
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
                var EmptyGameList = new List<Game>();
                var EmptyCustomMessagesList = new List<UserSettings.CustomMessages>();
                //if (Admin == "")
                if (string.IsNullOrEmpty(Admin))
                {
                    Admin = "undefined";
                }else{
                    AdminConverted = Convert.ToUInt64(Admin);
                }

                list.Accounts.Add(new UserAccounts
                {
                    AdminID = AdminConverted,
                    username = user,
                    password = password,
                    LoginKey = "0",
                    APIWebKey = "0",
                    SteamID = UserSteamID,
                    Games = EmptyGameList,
                    AFKMessages = EmptyCustomMessagesList,
                    ChatLogger = false
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
