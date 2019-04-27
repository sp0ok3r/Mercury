/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

using MercuryBOT.AccSettings;
using MercuryBOT.FriendsList;
using MercuryBOT.GamesGather;
using MercuryBOT.Helpers;
using MercuryBOT.SteamServers;
using MercuryBOT.User2Json;
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using SteamKit2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace MercuryBOT
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {

        readonly Process mercurycess = Process.GetCurrentProcess();
        public static NotifyIcon M_NotifyIcon;
        public static string usernameJSON;
        public static string passwordJSON;
        public static string apikey;

        public static bool Main_isLoggedIn = false;
        public static string SelectedUser = "";
        public static string SelectedGame = "";
        public static string SelectedFriend = "";

        private void BTN_RESTART_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.Logout();
            }

            Mercury_notifyIcon.Icon = null;
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        




        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.Logout();
            }
            Mercury_notifyIcon.Icon = null;
            Environment.Exit(1);
        }

        [Obsolete]
        private void RafadexAutoUpdate600IQ()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string updateCheck = client.DownloadString(Program.spkDomain + "update.php");

                    if (updateCheck != Program.Version)
                    {
                        this.Hide();
                        this.Enabled = false;
                        Console.WriteLine("New update: " + updateCheck);
                        Form Update = new Update(updateCheck);
                        Update.Show();
                    }
                    else
                    {
                        this.Enabled = true;
                        var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

                        if (Settingslist.startupAcc != 0)
                        {
                            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));

                            foreach (var a in list.Accounts)
                            {
                                if (a.SteamID == Settingslist.startupAcc)
                                {
                                    usernameJSON = a.username;
                                    passwordJSON = a.password;
                                }
                            }
                            // Start Login
                            Thread doLogin = new Thread(() => AccountLogin.UserSettingsGather(usernameJSON, passwordJSON));
                            doLogin.Start();
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("sp0ok3r.tk down :c");
                InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "sp0ok3r.tk down :c. Try https://github.com/sp0ok3r/Mercury/releases");
            }
        }


        [Obsolete]
        private void Main_Shown(object sender, EventArgs e)
        {
            RafadexAutoUpdate600IQ();
            M_NotifyIcon = this.Mercury_notifyIcon;
        }


        [Obsolete]
        public Main()
        {
            InitializeComponent();
            btn_logout.Visible = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.components.SetStyle(this);
            Region = Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));

            metroTabControl.SelectedTab = metroTab_AddAcc;

            Trolha.Tick += Trolha_Tick;
            TrolhaCommunity.Tick += CommunityConnection_Tick;


            // Calculate the mercury age. 2019-03-28 💔
            var age = 2019 - DateTime.Today.Year;
            lbl_mercuryAge.Text = "MERCURY BOT © is " + age + " years old! ";

        }

        public void Main_Load(object sender, EventArgs e)
        {
            this.Activate();
            lbl_infoversion.Text = "v" + Program.Version;
            RefreshAccountList();

            var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            combox_Colors.SelectedIndex = Settingslist.startupColor;

            if (Settingslist.startup)
            {
                toggle_startWindows.Checked = true;
            }
            else
            {
                toggle_startWindows.Checked = false;
            }

            if (Settingslist.startMinimized)
            {
                chck_Minimized.Checked = true;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                chck_Minimized.Checked = false;
                this.WindowState = FormWindowState.Normal;
            }

            if (Settingslist.playsound)
            {
                toggle_playSound.Checked = true;
                Stream str = Properties.Resources.mercury_success;
                SoundPlayer snd = new SoundPlayer(str);
                snd.Play();
            }
            else { toggle_playSound.Checked = false; }
        }

        public void RefreshAccountList()
        {
            AccountsList_Grid.Rows.Clear();
            foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
            {
                bool LoginK = true;
                if (string.IsNullOrEmpty(a.LoginKey) || a.LoginKey == "0")
                {
                    LoginK = false;
                }

                bool ApiWeb = true;
                if (string.IsNullOrEmpty(a.APIWebKey) || a.APIWebKey == "0")
                {
                    ApiWeb = false;
                }
                string[] row = { a.username, (a.SteamID).ToString(), (LoginK).ToString(), (ApiWeb).ToString() };
                AccountsList_Grid.Rows.Add(row);

            }
            AccountsList_ScrollBar.Maximum = AccountsList_Grid.Rows.Count;
            AccountsList_Grid.ClearSelection();
        }


        private void btn_admincmds_Click(object sender, EventArgs e)
        {
            btn_admincmds.Enabled = false;
            Form AdminCMDS = new AdminCMDS.AdminCMDSForm();
            AdminCMDS.FormClosed += HandleFormAdminCMDSClosed;
            AdminCMDS.Show();
        }

        private void btn_addAcc_Click(object sender, EventArgs e)
        {
            btn_addAcc.Enabled = false;
            Form AddAcc = new AddAcc();
            AddAcc.FormClosed += HandleFormAddAccClosed;
            AddAcc.Show();
        }

        private void btn_selectappids_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                try
                {
                    foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
                    {
                        if (a.username == AccountLogin.CurrentUsername)
                        {
                            apikey = a.APIWebKey;
                        }
                    }

                    //if (apikey == "undefined" || apikey.Length == 0)

                    if (string.IsNullOrEmpty(apikey) || apikey == "0")
                    {
                        AccountLogin.gatherWebApiKey();
                        InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Gathering your apikey and setting it! \n Just Gather Games again!");
                        //Process.Start("https://steamcommunity.com/dev/apikey");
                        
                         return;
                    }
                    else
                    {
                        Form AddAppIds = new SelectGames();
                        AddAppIds.FormClosed += HandleFormAddAppIdsClosed;
                        AddAppIds.Show();// qd nao tem key isto da erro
                        btn_selectappids.Enabled = false;
                    }

                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee);
                }
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }

        }


        private void btn_editAcc_Click(object sender, EventArgs e)
        {
            if (AccountsList_Grid.SelectedRows.Count > 0)
            {
                SelectedUser = AccountsList_Grid.SelectedRows[0].Cells[0].Value.ToString();
                btn_editAcc.Enabled = false;
                Form EditAcc = new EditAcc();
                EditAcc.FormClosed += HandleFormEditAccClosed;
                EditAcc.Show();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Please select an account!");
            }
        }

        private void btn_commentsGather_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                Form Comments = new CommentsGather();
                Comments.FormClosed += HandleFormCommentsClosed;
                Comments.Show();
                btn_commentsGather.Enabled = false;
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void Btn_getProfileBackG_Click(object sender, EventArgs e)
        {
            Form ProfileBackground = new SteamProfileBackground.ProfileBackground();
            ProfileBackground.FormClosed += HandleFormProfileBackgroundClosed;
            ProfileBackground.Show();
        }

        private void btn_playgame_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.PlayNonSteamGame(txtBox_gameNonSteam.Text + " | MercuryBOT");
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.Logout();
                RefreshAccountList();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void chck_afk_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_afk.Checked && AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.AwayMsg = true;
                AccountLogin.MessageString = txtBox_msg2Friends.Text;

                chck_CustomAFKMessages.Enabled = false; txtBox_msg2Friends.Enabled = false; btn_sendMsg2Friends.Enabled = false;
            }
            else
            {
                chck_CustomAFKMessages.Enabled = true; txtBox_msg2Friends.Enabled = true; btn_sendMsg2Friends.Enabled = true;

                AccountLogin.AwayMsg = false;
                AccountLogin.MessageString = "**not defined**";
            }
        }

        private void chck_CustomAFKMessages_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_CustomAFKMessages.Checked && AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.AwayCustomMessageList = true;
                chck_afk.Enabled = false; txtBox_msg2Friends.Enabled = false; btn_sendMsg2Friends.Enabled = false;
            }
            else
            {
                chck_afk.Enabled = true; txtBox_msg2Friends.Enabled = true; btn_sendMsg2Friends.Enabled = true;
                AccountLogin.AwayCustomMessageList = false;
            }
        }



        private void btn_setName_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.ChangeCurrentName(txtBox_nameChange.Text);
                txtBox_nameChange.Clear();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }

        }

        private void btn_sendMsg2Friends_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.SendMsg2AllFriends(txtBox_msg2Friends.Text);
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }

        }

        private void txtBox_msg2Friends_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode) // DELETE to delete selected
            {
                txtBox_msg2Friends.Clear();
            }
        }


        private async void btn_loadFriends_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {

                var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var a in list.Accounts)
                {
                    if (a.username == AccountLogin.CurrentUsername)
                    {
                        apikey = a.APIWebKey;
                    }
                }
                // if (apikey == "undefined" || apikey.Length == 0)
                if (string.IsNullOrEmpty(apikey) || apikey == "0")
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Register your api key and set it on Accounts.json");
                    AccountLogin.gatherWebApiKey(); // secalhar fazer isto no login
                    //Process.Start("https://steamcommunity.com/dev/apikey");
                    return;
                }
                var steamInterface = new SteamWebAPI2.Interfaces.SteamUser(apikey);
                ProgressSpinner_FriendsList.Visible = true;
                btn_loadFriends.Enabled = false;
                BTN_RemoveFriend.Enabled = false;
                FriendsList_Grid.Rows.Clear();

                foreach (var f in AccountLogin.Friends)
                {
                    DateTime playerSummaryData;
                    var playerSummaryResponse = await steamInterface.GetPlayerSummaryAsync(f.ConvertToUInt64());
                    if (playerSummaryResponse != null)
                    {
                        playerSummaryData = playerSummaryResponse.Data.LastLoggedOffDate;

                        var Name = AccountLogin.GetPersonaName(f.ConvertToUInt64());
                        string[] row = { f.ConvertToUInt64().ToString(), Name, playerSummaryData.ToString() };
                        FriendsList_Grid.Rows.Add(row);
                    }
                }

               // lbl_totalFriends.Text = ": " + FriendsList_Grid.Rows.Count;
                ProgressSpinner_FriendsList.Visible = false;
                btn_loadFriends.Enabled = true;
                BTN_RemoveFriend.Enabled = true;
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }

        }

        private void FriendsList_Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedFriend = FriendsList_Grid.SelectedRows[0].Cells[0].Value.ToString();
            lbl_friendSelected.Text = "Selected: " + SelectedFriend;
        }

        private void FriendsList_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= FriendsList_Grid.Rows.Count)
            {
                return;
            }
            FriendsList_Grid.FirstDisplayedScrollingRowIndex = e.NewValue;
        }
        private void BTN_RemoveFriend_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                if (SelectedFriend != "")
                {
                    ListFriends.Remove(Convert.ToUInt64(SelectedFriend));
                    AccountLogin.RemoveFriend(Convert.ToUInt64(SelectedFriend));
                    FriendsList_Grid.Rows.RemoveAt(FriendsList_Grid.SelectedRows[0].Cells[0].RowIndex);
                }
                else
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Select a friend!");

                }

            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }

        }


        private void btn_playnormal_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                if (GamesList_Grid.Rows.Count <= 32)
                {
                    List<uint> gameuints = new List<uint>();

                    foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
                    {
                        if (a.username == AccountLogin.CurrentUsername)
                        {
                            for (int i = 0; i < a.Games.Count; i++)
                            {
                                gameuints.Add(a.Games[i].app_id); // tentar obter lista do json, para nao criar outra???
                            }

                            if (chck_nonsteamNgames.Checked && txtBox_gameNonSteam.Text.Length != 0)
                            {
                                AccountLogin.PlayGames(gameuints, txtBox_gameNonSteam.Text + " | MercuryBOT");
                            }
                            else
                            {
                                AccountLogin.PlayGames(gameuints, "disable");
                            }
                        }
                    }

                }
                else
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "You can only play 32 games, sorry bro D:");
                }
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }

        }

        private void link_stopIdling_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.StopGames();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }

        }

        private void btn_addGameManually_Click(object sender, EventArgs e)
        {
            if (GamesList_Grid.Rows.Count >= 32)
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Max GAMES: 32 - remove some");
                return;
            }
            else
            {
                int txtBox_gameIDAdd_out;
                if (txtBox_gameIDAdd.Text.Length > 0 && int.TryParse(txtBox_gameIDAdd.Text, out txtBox_gameIDAdd_out))
                {
                    var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                    uint gameidConverted = Convert.ToUInt32(txtBox_gameIDAdd.Text);
                    foreach (var json in list.Accounts)
                    {
                        if (json.username == AccountLogin.CurrentUsername)
                        {
                            Game NewGame = new Game { app_id = gameidConverted, name = "Manually Added" };
                            json.Games.Add(NewGame);
                            txtBox_gameIDAdd.Clear();
                        }
                    }
                    string output = JsonConvert.SerializeObject(list, Formatting.Indented);
                    File.WriteAllText(Program.AccountsJsonFile, output);

                    LoadGamesFromJSON();
                }
                else
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Please write the appid!");
                    txtBox_gameIDAdd.Clear();
                    return;
                }
            }

        }

        private void btn_reddemkey_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.RedeemKey(txtBox_redeemKey.Text);
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Added to your library!");
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void btn_login2selected_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedUser))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Please select an account!");
                return;
            }

            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));

            foreach (var a in list.Accounts)
            {
                if (a.username == SelectedUser)
                {
                    if (string.IsNullOrEmpty(a.password))
                    {
                        InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Please add password to: " + a.username);
                        return;
                    }
                    usernameJSON = a.username;
                    passwordJSON = a.password;
                }
            }
            // Start Login
            Thread doLogin = new Thread(() => AccountLogin.UserSettingsGather(usernameJSON, passwordJSON));
            doLogin.Start();
            btn_login2selected.Enabled = false;
            lbl_infoLogin.Text = "Trying to login...";

        }

        private void listview_accounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listview_accounts.SelectedItems.Count > 0)
            //{
            //    AccSelected = listview_accounts.SelectedItems[0].Index;
            //    var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            //    int find = 0;
            //    foreach (var a in list.Accounts)
            //    {
            //        if (find == AccSelected)
            //        {
            //            SelectedUser = list.Accounts[find].username;
            //        }
            //        find++;
            //    }
            //}
        }

        private void combox_uimodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                switch (combox_uimodes.SelectedIndex)
                {
                    case 0:
                        AccountLogin.UIMode(0);
                        AccountLogin.ChangePersonaFlags(0);
                        break;
                    case 1:
                        AccountLogin.ChangePersonaFlags(1024); //BP
                        break;
                    case 2:
                        AccountLogin.ChangePersonaFlags(2048); //VR
                        break;
                    case 3:
                        AccountLogin.UIMode(2); // phone
                        break;
                }
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        public void LoadGamesFromJSON()
        {
            GamesList_Grid.Rows.Clear();
            foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
            {
                if (a.username == AccountLogin.CurrentUsername)
                {
                    for (int i = 0; i < a.Games.Count; i++)
                    {
                        string[] row = { a.Games[i].name, (a.Games[i].app_id).ToString() };

                        GamesList_Grid.Rows.Add(row);
                    }
                }
            }
            lbl_countGames.Text = "count: " + GamesList_Grid.Rows.Count;
        }

        private void btn_loadGamesFromJSON_Click(object sender, EventArgs e)
        {
            LoadGamesFromJSON();
        }

        private void combox_states_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                EPersonaState State = ExtraInfo.statesList[combox_states.SelectedIndex];
                AccountLogin.ChangeCurrentState(State);
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void PicBox_SteamAvatar_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                Process.Start("http://steamcommunity.com/profiles/" + AccountLogin.steamID);
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void Btn_steamStatus_Click(object sender, EventArgs e)
        {
            Form SteamServersMain = new SteamServersMain();
            SteamServersMain.FormClosed += HandleFormSteamStatusClosed;
            SteamServersMain.Show();

            Btn_steamStatus.Enabled = false;

        }


        //public void StartList()
        //{
        //    listView1.Items.Clear();
        //    foreach (ListFriends o in ListFriends.Get())
        //    {
        //        string[] row = { o.Name, (o.SID).ToString(), o.Status };

        //        var listViewItem = new System.Windows.Forms.ListViewItem(row);
        //        listViewItem.UseItemStyleForSubItems = false;

        //        listViewItem.SubItems[0].ForeColor = Color.DeepSkyBlue;
        //        listViewItem.SubItems[1].ForeColor = Color.DodgerBlue;
        //        listViewItem.SubItems[2].ForeColor = Color.DarkOrange;

        //        listView1.Items.Add(listViewItem);
        //    }
        //}


        //public void ToListViewItem()
        //{
        //    Console.WriteLine("updating/..");


        //    //item.SubItems[0] - name
        //    //item.SubItems[1] - id
        //    //item.SubItems[2] - relacao
        //    //item.SubItems[3] - status
        //    foreach (ListViewItem item in listView1.Items)
        //    {

        //        foreach (ListFriends o in ListFriends.Get())
        //        {
        //            var id = item.SubItems[1];

        //            if (id.Text == o.SID.ToString() && item.SubItems[2].Text != o.Status)
        //            {
        //                //item.SubItems[0] = new ListViewItem.ListViewSubItem() { Text = NewName };
        //                item.SubItems[2] = new ListViewItem.ListViewSubItem() { Text = o.Status };
        //            }
        //        }
        //        //break;
        //    }
        //}

        #region HandleFormClosed
        private void HandleFormEditAccClosed(Object sender, FormClosedEventArgs e)
        {
            btn_editAcc.Enabled = true;
        }

        private void HandleFormCommentsClosed(Object sender, FormClosedEventArgs e)
        {
            btn_commentsGather.Enabled = true;
        }
        private void HandleFormProfileBackgroundClosed(Object sender, FormClosedEventArgs e)
        {
            btn_commentsGather.Enabled = true;
        }

        private void HandleFormAddAccClosed(Object sender, FormClosedEventArgs e)
        {
            btn_addAcc.Enabled = true;
        }

        private void HandleFormAddAppIdsClosed(Object sender, FormClosedEventArgs e)
        {
            LoadGamesFromJSON();
            btn_selectappids.Enabled = true;
        }

        private void HandleFormSteamStatusClosed(Object sender, FormClosedEventArgs e)
        {
            Btn_steamStatus.Enabled = true;
        }

        private void HandleFormAFKMessagesClosed(Object sender, FormClosedEventArgs e)
        {
            btn_addMsgForm.Enabled = true;
        }

        private void HandleFormGatherSteamGroupsClosed(Object sender, FormClosedEventArgs e)
        {
            btn_exitgroups.Enabled = true;
        }

        private void HandleFormAdminCMDSClosed(Object sender, FormClosedEventArgs e)
        {
            btn_admincmds.Enabled = true;
        }
        private void HandleFormProfilePrivacySettingClosed(Object sender, FormClosedEventArgs e)
        {
            btn_changeprofSettings.Enabled = true;
        }



        #endregion

        #region PictureBox Move
        //https://stackoverflow.com/users/3879008/giangpzo
        private bool draging = false;
        private Point pointClicked;
        private void _MouseMove(object sender, MouseEventArgs e)
        {
            if (draging)
            {
                Point pointMoveTo;
                pointMoveTo = this.PointToScreen(new Point(e.X, e.Y));
                pointMoveTo.Offset(-pointClicked.X, -pointClicked.Y);
                this.Location = pointMoveTo;
            }
        }

        private void _MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draging = true;
                pointClicked = new Point(e.X, e.Y);
            }
            else
            {
                draging = false;
            }
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            draging = false;
        }
        #endregion

        #region MetroLinks

        private void Link_steamgroup_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/groups/MercuryBOT");
        }

        private void Link_discord_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/7e7kuhp");
        }

        private void MetroLink_spkMusic_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=3mACun803qU");
        }

        private void metroLink_Json_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.newtonsoft.com/json");
        }

        private void metroLink_SteamKit2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/SteamRE/SteamKit");
        }

        private void metroLink_WebApi2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/babelshift/SteamWebAPI2");
        }

        private void metroLink_Metro_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/thielj/MetroFramework");
        }

        private void metroLink_spk_Click(object sender, EventArgs e)
        {
            Process.Start("http://steamcommunity.com/profiles/76561198041931474");
        }

        private void metroLink_azzda_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/profiles/76561198177157710/");
        }

        private void MetroLink_microsoft_Click(object sender, EventArgs e)
        {
            Process.Start("https://visualstudio.microsoft.com");
        }

        private void MetroLink_csharp_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.microsoft.com/dotnet/");
        }

        private void MetroLink_AccountsJSONPath_Click(object sender, EventArgs e)
        {
            Process.Start(Program.AccountsJsonFile);
        }

        private void link_github_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sp0ok3r/Mercury");
        }

        #endregion

        #region toolStrip
        private void toolStrip_CloseMercury_Click(object sender, EventArgs e)
        {
            Mercury_notifyIcon.Icon = null;
            Environment.Exit(1);
        }

        private void toolStrip_OpenMercury_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        #endregion

        #region Timer
        void CommunityConnection_Tick(object sender, EventArgs e)
        {
            WebCookieCheck();
        }
        private void WebCookieCheck()
        {
            if (AccountLogin.IsLoggedIn == true)
            {

                if (AccountLogin.IsWebLoggedIn)
                {
                    TrolhaCommunity.Stop();
                    return;
                }

                if (!AccountLogin.CheckCookies())
                {
                    Console.WriteLine("Cookies were out of date, requesting new user nonce ...");
                }
            }
        }
           
        void Trolha_Tick(object sender, EventArgs e)
        {
            CurrentUserSafeUpdater();
            mercurycess.Refresh();

            link_currentRAM.Text = mercurycess.PrivateMemorySize64.ToFileSize();
            link_memorysize.Text = mercurycess.PagedSystemMemorySize64.ToFileSize();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //  AccountLogin.gatherWebApiKey();
        }

        private void btn_exitgroups_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                btn_exitgroups.Enabled = false;
                Form exitGroups = new SteamGroups.GatherSteamGroups();
                exitGroups.FormClosed += HandleFormGatherSteamGroupsClosed;
                exitGroups.Show();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void btn_joinGroups_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                btn_joinGroups.Enabled = false;
                var lines = File.ReadLines(Program.ExecutablePath + @"\" + AccountLogin.CurrentSteamID + "-GroupsIDS.txt");
                foreach (var line in lines)
                {
                    AccountLogin.JoinGroup(line);
                    Thread.Sleep(5);
                }
                btn_joinGroups.Enabled = true;
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Joined all groups in file!");
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void btn_acclistOpenprofile_Click(object sender, EventArgs e)
        {
            if (AccountsList_Grid.SelectedRows.Count > 0)
            {
                Process.Start("http://steamcommunity.com/profiles/" + AccountsList_Grid.SelectedRows[0].Cells[1].Value.ToString());
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Select an account!");
            }
        }

        private void AccountsList_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= AccountsList_Grid.Rows.Count)
            {
                return;
            }
            AccountsList_Grid.FirstDisplayedScrollingRowIndex = e.NewValue;
        }

        private void toggle_startWindows_CheckedChanged(object sender, EventArgs e)
        {
            var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            if (toggle_startWindows.Checked)
            {
                Settingslist.startup = true;

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("MercuryBOT", Program.ExecutablePath + @"\MercuryBOT.exe");
            }
            else
            {
                Settingslist.startup = false;

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.DeleteValue("MercuryBOT", false);
            }
            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(Settingslist, Formatting.Indented));
        }

        private void chck_Minimized_CheckedChanged(object sender, EventArgs e)
        {
            var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            if (chck_Minimized.Checked)
            {
                Settingslist.startMinimized = true;
            }
            else
            {
                Settingslist.startMinimized = false;
            }
            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(Settingslist, Formatting.Indented));
        }

        private void toggle_playSound_CheckedChanged(object sender, EventArgs e)
        {
            var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            if (toggle_playSound.Checked)
            {
                Settingslist.playsound = true;
            }
            else
            {
                Settingslist.playsound = false;
            }
            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(Settingslist, Formatting.Indented));
        }

        private void AccountsList_Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedUser = AccountsList_Grid.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void GamesList_Grid_KeyDown(object sender, KeyEventArgs e)
        {
            var ListsGames = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            if (Keys.Delete == e.KeyCode) // DELETE to delete selected
            {
                foreach (var UserList in ListsGames.Accounts)
                {
                    if (UserList.username == AccountLogin.CurrentUsername)
                    {
                        for (int i = 0; i < UserList.Games.Count; i++)
                        {

                            if (UserList.Games[i].app_id.ToString() == GamesList_Grid.SelectedRows[0].Cells[1].Value.ToString())
                            {
                                UserList.Games.Remove(UserList.Games[i]);
                                GamesList_Grid.Rows.RemoveAt(i);
                                lbl_countGames.Text = "count: " + GamesList_Grid.Rows.Count;
                            }
                        }
                    }
                }
                File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(ListsGames, Formatting.Indented));
            }

            if (Keys.End == e.KeyCode) // END to delete all
            {
                foreach (var UserList in ListsGames.Accounts)
                {
                    if (UserList.username == AccountLogin.CurrentUsername)
                    {
                        for (int i = 0; i < UserList.Games.Count; i++)
                        {
                            UserList.Games.Remove(UserList.Games[i]);
                            GamesList_Grid.Rows.RemoveAt(i);
                            lbl_countGames.Text = "count: " + GamesList_Grid.Rows.Count;
                        }
                    }
                }
                File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(ListsGames, Formatting.Indented));
            }
        }

        private void GamesList_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= GamesList_Grid.Rows.Count)
            {
                return;
            }
            GamesList_Grid.FirstDisplayedScrollingRowIndex = e.NewValue;
        }

        private void GamesList_Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedGame = GamesList_Grid.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void CurrentUserSafeUpdater()
        {
            try
            {
                if (AccountLogin.IsLoggedIn == true)
                {
                    btn_logout.Visible = true;
                    toggle_chatlogger.Checked = AccountLogin.ChatLogger;
                    lbl_infoLogin.Text = "Trying to login...";
                    Main_isLoggedIn = true;
                    // ToListViewItem();
                    btn_login2selected.Enabled = false;
                    Panel_UserInfo.Visible = true;

                    byte[] data = new WebClient().DownloadData("https://www.countryflags.io/" + AccountLogin.UserCountry + "/flat/16.png");
                    MemoryStream ms = new MemoryStream(data);
                    btnLabel_PersonaAndFlag.Image = Image.FromStream(ms);

                    btnLabel_PersonaAndFlag.Invoke(new Action(() => btnLabel_PersonaAndFlag.Text = AccountLogin.UserPersonaName));

                    panel_steamStates.BackColor = Color.LightSkyBlue;
                    picBox_SteamAvatar.ImageLocation = AccountLogin.GetAvatarLink(AccountLogin.CurrentSteamID);
                    lbl_currentUsername.Invoke(new Action(() => lbl_currentUsername.Text = AccountLogin.CurrentUsername));
                    lbl_infoLogin.Text = "Connected";
                }
                else
                {
                    btn_logout.Visible = false;
                    lbl_infoLogin.Text = "Not logged...";
                    Main_isLoggedIn = false;
                    btn_login2selected.Enabled = true;
                    Panel_UserInfo.Visible = false;
                    btnLabel_PersonaAndFlag.Image = Properties.Resources.notloggedFlag;
                    btnLabel_PersonaAndFlag.Invoke(new Action(() => btnLabel_PersonaAndFlag.Text = "None"));

                    GamesList_Grid.Rows.Clear();
                    panel_steamStates.BackColor = Color.Gray;
                    picBox_SteamAvatar.BackColor = Color.FromArgb(255, 25, 25, 25);
                    lbl_currentUsername.Invoke(new Action(() => lbl_currentUsername.Text = "None"));
                    //  return;
                }
            }
            catch (Exception e)
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", e.ToString());
                Console.WriteLine(e);
            }
        }
        #endregion


        private void btn_addMsgForm_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                btn_addMsgForm.Enabled = false;
                Form CustomAFK = new CustomMessages.AFKMessages();
                CustomAFK.FormClosed += HandleFormAFKMessagesClosed;
                CustomAFK.Show();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void metroTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl.SelectedTab == metroTabControl.TabPages["metroTab_Games"])
            {
                LoadGamesFromJSON();
            }
        }

        private void lbl_infoversion_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sp0ok3r/Mercury/releases");
        }

        private void btn_clearuserAliases_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.ClearAliases();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged.");
            }

        }

        private void toggle_chatlogger_CheckedChanged(object sender, EventArgs e)
        {

            if (AccountLogin.IsLoggedIn == true && toggle_chatlogger.Checked)
            {
                AccountLogin.ChatLogger = true;
            }
            else
            {
                AccountLogin.ChatLogger = false;
            }
        }

        private void link_chatlogs_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                Process.Start(Program.ChatLogsFolder + @"\" + AccountLogin.steamID);
            }
            else
            {
                Process.Start(Program.ChatLogsFolder);
            }
        }

        private void btn_changeprofSettings_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                Form ProfilePrivacySetting = new ProfilePrivacy();
                ProfilePrivacySetting.FormClosed += HandleFormProfilePrivacySettingClosed;
                btn_changeprofSettings.Enabled = false;
                ProfilePrivacySetting.Show();
                //verify **********************+ strange error

            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged.");
            }
        }
        #region Restart picture
        private void picBox_Restart_MouseHover(object sender, EventArgs e)
        {
            picBox_Restart.Image = Properties.Resources.Restart_MouseHover;
        }

        private void picBox_Restart_MouseLeave(object sender, EventArgs e)
        {
            picBox_Restart.Image = Properties.Resources.Restart_Normal;
        }

        private void picBox_Restart_Click(object sender, EventArgs e)
        {
            picBox_Restart.Image = Properties.Resources.Restart_Click;
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.Logout();
            }

            Mercury_notifyIcon.Icon = null;
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }
        #endregion

        private void combox_Colors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Settingslist = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            Settingslist.startupColor = combox_Colors.SelectedIndex;

            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(Settingslist, new JsonSerializerSettings { Formatting = Formatting.Indented }));

            this.components.SetStyle(this);
            this.Refresh();
        }
    }
}


/* dont delete
         * backup read and save json 
        string path = Program.ExecutablePath2 + @"\Accounts.json";
        var myJsonString = File.ReadAllText(path);

        var objectToSerialize = new RootObject();
        var list = JsonConvert.DeserializeObject<RootObject>(myJsonString);

            foreach (var a in list.Accounts)
            {
                if (a.username == user)
                {
                    a.LoginKey = callback.LoginKey;
                    Console.WriteLine("[" + BOTNAME + "] key: " + a.LoginKey);
                    NewloginKey=callback.LoginKey;
                }
        }
        string output = JsonConvert.SerializeObject(list, Formatting.Indented);
        File.WriteAllText(path, output);
*/
