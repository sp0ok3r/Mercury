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
using MercuryBOT.SteamCommunity;
using MercuryBOT.SteamServers;
using MercuryBOT.User2Json;
using MercuryBOT.UserSettings;
using MetroFramework.Controls;
using Newtonsoft.Json;
using Steam4NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Win32Interop.Methods;

namespace MercuryBOT
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public static NotifyIcon M_NotifyIcon;
        public static string usernameJSON;
        public static string passwordJSON;
        public static string apikey;
        public static string SelectedUser, SelectedGame, SelectedFriend = String.Empty;
        private bool SteamClientState = false;

        #region MassMessageOldMethodHelpers
        private ISteam006 steam006;
        private StringBuilder version;
        private ISteamClient012 steamclient;
        private ISteamUser016 steamuser;
        private ISteamFriends013 steamfriends013;
        private ISteamFriends002 steamfriends002;
        private int user;
        private int pipe;

        private int LoadSteam()
        {
            if (Steamworks.Load(true))
            {
                Console.WriteLine("Ok, Steam Works!");
            }
            else
            {
                MessageBox.Show("Failed, Steam Works!");
                Console.WriteLine("Failed, Steam Works!");

                return -1;
            }

            steam006 = Steamworks.CreateSteamInterface<ISteam006>();
            TSteamError steamError = new TSteamError();
            version = new StringBuilder();
            steam006.ClearError(ref steamError);

            steamclient = Steamworks.CreateInterface<ISteamClient012>();
            pipe = steamclient.CreateSteamPipe();
            user = steamclient.ConnectToGlobalUser(pipe);
            steamuser = steamclient.GetISteamUser<ISteamUser016>(user, pipe);
            steamfriends013 = steamclient.GetISteamFriends<ISteamFriends013>(user, pipe);
            steamfriends002 = steamclient.GetISteamFriends<ISteamFriends002>(user, pipe);

            Console.WriteLine("\nSteam2 tests:");


            if (steam006 == null)
            {
                Console.WriteLine("steam006 is null !");
                return -1;
            }
            if (steamclient == null)
            {
                Console.WriteLine("steamclient is null !");
                return -1;
            }
            return 0;
        }
        #endregion


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
            this.Activate();
            this.components.SetStyle(this);
            RefreshAccountList();
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(1, 1, Width, Height + 5, 15, 15));

            IntPtr ptrLogout = Gdi32.CreateRoundRectRgn(1, 1, btn_logout.Width, btn_logout.Height, 5, 5);
            btn_logout.Region = Region.FromHrgn(ptrLogout);
            Gdi32.DeleteObject(ptrLogout);

            foreach (Control tab in MercuryTabControl.Controls)
            {
                TabPage tabPage = (TabPage)tab;
                foreach (Control control in tabPage.Controls.OfType<MetroButton>())
                {
                    IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, control.Width, control.Height, 5, 5);
                    control.Region = Region.FromHrgn(ptr);
                    Gdi32.DeleteObject(ptr);
                }
            }

            IntPtr ptr2 = Gdi32.CreateRoundRectRgn(1, 1, picBox_SteamAvatar.Width, picBox_SteamAvatar.Height, 5, 5);
            picBox_SteamAvatar.Region = Region.FromHrgn((IntPtr)Gdi32.CreateRoundRectRgn(1, 1, picBox_SteamAvatar.Width, picBox_SteamAvatar.Height, 5, 5));
            Gdi32.DeleteObject(ptr2);

            panel_steamStates.Region = Region.FromHrgn((IntPtr)Gdi32.CreateRoundRectRgn(1, 1, panel_steamStates.Width, panel_steamStates.Height, 5, 5));

            btn_logout.Visible = false;

            Trolha.Tick += Trolha_Tick;

            var age = 2019 - DateTime.Today.Year; // Calculate the mercury age. 2019-03-28 💔
            if (age < 0)
            {
                lbl_mercuryAge.Text = "MERCURY BOT © is " + age + " years old! ";
            }

        }

        public void Main_Load(object sender, EventArgs e)
        {
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            
            lbl_infoversion.Text = "v" + Program.Version.Replace("-", "");


            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            combox_Colors.SelectedIndex = SettingsList.startupColor;

            if (SettingsList.hideInTaskBar)
            {
                toggle_hideInTask.Checked = true;
                this.ShowInTaskbar = false;
            }
            else
            {
                this.ShowInTaskbar = true;
                toggle_hideInTask.Checked = false;
            }

            toggle_startWindows.Checked = SettingsList.startup;

            if (SettingsList.startMinimized)
            {
                chck_Minimized.Checked = true;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                chck_Minimized.Checked = false;
                this.WindowState = FormWindowState.Normal;
            }

            if (SettingsList.playsound)
            {
                toggle_playSound.Checked = true;
                Stream str = Properties.Resources.mercury_success;
                SoundPlayer snd = new SoundPlayer(str);
                snd.Play();
            }
            else
            {
                toggle_playSound.Checked = false;
            }

            foreach (TabPage item in this.MercuryTabControl.TabPages)
            {
                combox_defaultTab.Items.Add(item.Text.TrimEnd());
            }

            if (SettingsList.startupTab != -1)
            {
                combox_defaultTab.SelectedIndex = SettingsList.startupTab;
                MercuryTabControl.SelectedIndex = SettingsList.startupTab;
            }
            else
            {
                combox_defaultTab.SelectedIndex = 0;
                MercuryTabControl.SelectedIndex = 0;
            }


            //if (SettingsList.notificationEffect != string.Empty)
            //{
            //  //  combox_notifEffect.SelectedIndex = Extensions.notifEffects[combox_notifEffect.SelectedIndex];
            //}
            //else
            //{
            //    combox_notifEffect.SelectedIndex = 3;
            //}
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState && !this.ShowInTaskbar) // 254iq
            {
                Hide();
            }
        }
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
                        this.Enabled = false;
                        this.Hide();

                        Console.WriteLine("New update: " + updateCheck);
                        Form Update = new Update(updateCheck);
                        Update.Show();
                    }
                    else
                    {
                        this.Enabled = true;
                        var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

                        if (SettingsList.startupAcc != 0)
                        {
                            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));

                            foreach (var a in list.Accounts)
                            {
                                if (a.SteamID == SettingsList.startupAcc)
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
                Console.WriteLine("sp0ok3r.tk down or No internet connection");
                Notification.NotifHelper.MessageBox.Show("Alert", "No internet connection, restarting...");
                Process.Start("https://github.com/sp0ok3r/Mercury/releases");

                Process.Start(Application.ExecutablePath);
                Application.Exit();
            }
        }
        public void RefreshAccountList()
        {
            int i = 0;
            AccountsList_Grid.Rows.Clear();
            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts;
            foreach (var a in list.OrderByDescending(x => x.LastLoginTime))
            {
                bool LoginK = true;
                bool ApiWeb = true;

                if (string.IsNullOrEmpty(a.LoginKey) || a.LoginKey == "0")
                {
                    LoginK = false;
                }
                if (string.IsNullOrEmpty(a.APIWebKey) || a.APIWebKey == "0")
                {
                    ApiWeb = false;
                }

                string[] row = { a.username, (a.SteamID).ToString(), (LoginK).ToString(), (ApiWeb).ToString() };

                AccountsList_Grid.Rows.Add(row);

                if (a.password.Length != 0)
                {
                    AccountsList_Grid.Rows[i].Cells[0].Style.ForeColor = Color.White;
                    if (LoginK == true)
                    {
                        AccountsList_Grid.Rows[i].Cells[2].Style.ForeColor = Color.Green;
                    }
                    else
                    {
                        AccountsList_Grid.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                    }

                    if (ApiWeb == true)
                    {
                        AccountsList_Grid.Rows[i].Cells[3].Style.ForeColor = Color.Green;
                    }
                    else
                    {
                        AccountsList_Grid.Rows[i].Cells[3].Style.ForeColor = Color.Red;
                    }

                }



                toolStrip_Acc.DropDownItems.Add(a.username);
                i++;
            }
            AccountsList_ScrollBar.Maximum = AccountsList_Grid.Rows.Count;
            AccountsList_Grid.ClearSelection();
        }


        #region Buttons
        private void btn_admincmds_Click(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, ".pcoff - Shutdown PC.\n" +
                                                     ".close - Closes MercuryBOT process.\n" +
                                                     ".logoff - Logoff from current account.\n" +
                                                     ".non customname - Play a non - steam game(change customname to anything)\n" +
                                                     ".play customname - Play a non steam game and appids.", "Mercury - Admin Commands", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    if (string.IsNullOrEmpty(apikey) || apikey == "0")
                    {
                        InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Gathering your apikey and setting it! \n Just Gather Games again!");
                        Utils.gatherWebApiKey();
                        return;
                    }
                    else
                    {
                        Form AddAppIds = new SelectGames();
                        AddAppIds.FormClosed += HandleFormAddAppIdsClosed;
                        AddAppIds.Show();
                        btn_selectappids.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try again/Login again");
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
            Btn_getProfileBackG.Enabled = false;
        }

        private void btn_playNonSteamgame_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                if (txtBox_gameNonSteam.Text.Length < 50)
                {
                    Utils.PlayNonSteamGame(txtBox_gameNonSteam.Text + (AccountLogin.isInMercuryGroup ? "" : " ❤ MercuryBOT"));
                    btn_playnormal.Enabled = false;
                }
                else
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Only 50 chars allowed.");
                }
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
                btn_playnormal.Enabled = true;
                //combox_states.SelectedIndex = 0;
                //RefreshAccountList();

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



        private void btn_sendMsg2Friends_Click(object sender, EventArgs e) // activate later
        {
            if (chck_steam4net.Checked)
            {
                if (SteamClientState == false && Program.CurrentProcesses.FirstOrDefault(x => x.ProcessName == "Steam") != null)
                {
                    if (LoadSteam() == -1)
                    {
                        MessageBox.Show("you have to be logged in steam client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                SteamClientState = true;
                int quantasPrincesas = 0;
                string msg = txtBox_msg2Friends.Text;
                var friends = steamfriends013.GetFriendCount((int)EFriendFlags.k_EFriendFlagImmediate);

                var ad = AccountLogin.isInMercuryGroup ? "" : "\n MercuryBOT";

                byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
                for (int i = 0; i < friends; i++)
                {
                    var friendid = steamfriends013.GetFriendByIndex(i, (int)EFriendFlags.k_EFriendFlagImmediate);

                    if (chck_Send2Receipts.Checked)
                    {
                        foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
                        {
                            if (a.username == AccountLogin.CurrentUsername && a.MsgRecipients.Any(s => s.Contains(friendid.ConvertToUint64().ToString())))
                            {
                                quantasPrincesas++;
                                steamfriends002.SendMsgToFriend(friendid, Steam4NET.EChatEntryType.k_EChatEntryTypeChatMsg, Encoding.Default.GetBytes(txtBox_msg2Friends.Text + ad), (txtBox_msg2Friends.Text.Length) + 1);
                                Thread.Sleep(100);// my nigger needs some OXYGEN 😌 
                            }
                        }
                    }
                    else
                    {
                        quantasPrincesas++;
                        msgBytes = Encoding.UTF8.GetBytes(msg + ad);
                        steamfriends002.SendMsgToFriend(friendid, EChatEntryType.k_EChatEntryTypeChatMsg, msgBytes, (msgBytes.Length) + 1);
                        Thread.Sleep(100);// my nigger needs some OXYGEN 😌 
                    }
                }
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Sent message to " + quantasPrincesas + " friends!");
                btn_sendMsg2Friends.Enabled = true;
            }
            else
            {
                if (AccountLogin.IsLoggedIn == true)
                {
                    AccountLogin.SendMsg2AllFriends(txtBox_msg2Friends.Text, chck_Send2Receipts.Checked);
                    btn_sendMsg2Friends.Enabled = false;
                }
                else
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
                }
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

                if (string.IsNullOrEmpty(apikey) || apikey == "0")
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Gathering your apikey and setting it! \n Just Gather Friends again!");
                    Utils.gatherWebApiKey();
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

                lbl_totalFriends.Text = "count: " + FriendsList_Grid.Rows.Count;
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


        public void playNormal()
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                if (GamesList_Grid.Rows.Count <= 32)
                {
                    picBox_playing.Visible = true;
                    List<uint> gameuints = new List<uint>();

                    foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
                    {
                        if (a.username == AccountLogin.CurrentUsername)
                        {
                            for (int i = 0; i < a.Games.Count; i++)
                            {
                                gameuints.Add(a.Games[i].app_id); // tentar obter lista do json, para nao criar outra???
                            }

                            var ad = AccountLogin.isInMercuryGroup ? "" : " ❤ MercuryBOT";
                            if (chck_nonsteamNgames.Checked)
                            {
                                if (txtBox_gameNonSteam.Text.Length != 0)
                                {
                                    Utils.PlayGames(gameuints, txtBox_gameNonSteam.Text + ad);
                                }
                                else
                                {
                                    Utils.PlayGames(gameuints, ad);
                                }
                            }
                            else
                            {
                                Utils.PlayGames(gameuints, "Idling ⌛ MercuryBOT");
                                Thread.Sleep(2000);
                                Utils.PlayGames(gameuints, "disable");
                            }
                        }
                      //  Notification.NotifHelper.MessageBox.Show("Info", "Playing: "+a.Games.Count + "games.");
                    }
                    btn_playnormal.Enabled = false;
                    btn_playNonSteam.Enabled = false;
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


        private void btn_playnormal_Click(object sender, EventArgs e)
        {
            playNormal();
        }
        public void stopIdling()
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                Utils.StopGames();
                picBox_playing.Visible = false;
                btn_playnormal.Enabled = true;
                btn_playNonSteam.Enabled = true;
                Notification.NotifHelper.MessageBox.Show("Info", "Idling stopped");

            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }
        private void link_stopIdling_Click(object sender, EventArgs e)
        {
            stopIdling();
        }

        private void btn_addGameManually_Click(object sender, EventArgs e)
        {
            if (GamesList_Grid.Rows.Count >= 32)
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Max GameIDs: 32 - remove some");
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
                var CDKeysList = CDKeys_Grid.Rows.OfType<DataGridViewRow>().Select(
                    r => r.Cells.OfType<DataGridViewCell>().Select(c => c.Value).ToArray()).ToList();

                if (CDKeysList.Count != 0)
                {
                    btn_reddemkey.Enabled = false;
                    btn_addCDKey.Enabled = false;

                    for (int i = 0; i < CDKeysList.Count; i++)
                    {
                        AccountLogin.RedeemKey(CDKeysList[i].ToString());

                        using (FileStream fs = File.Create(Program.ExecutablePath))
                        {
                            File.WriteAllText(Program.ExecutablePath + @"\successfulKeys.txt", CDKeysList[i].ToString() + "\n");
                        }

                        Thread.Sleep(500);
                    }
                    InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Added to your library: " + CDKeysList.Count + " keys.");

                    CDKeysList.Clear();
                    CDKeys_Grid.Rows.Clear();
                    txtBox_redeemKey.Clear();
                    btn_reddemkey.Enabled = true;
                    btn_addCDKey.Enabled = true;
                    btn_importKeys.Enabled = true;
                }
                else
                {
                    Match match = new Regex(@"((?![^0-9]{12,}|[^A-z]{12,})([A-z0-9]{4,5}-?[A-z0-9]{4,5}-?[A-z0-9]{4,5}(-?[A-z0-9]{4,5}(-?[A-z0-9]{4,5})?)?))").Match(txtBox_redeemKey.Text);
                    if (!String.IsNullOrEmpty(txtBox_redeemKey.Text) && match.Success)
                    {
                        txtBox_redeemKey.Clear();
                        AccountLogin.RedeemKey(txtBox_redeemKey.Text);
                        InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Added to your library: 1 CDKey!");

                        using (FileStream fs = File.Create(Program.ExecutablePath))
                        {
                            File.WriteAllText(Program.ExecutablePath + @"\successfulKeys.txt", txtBox_redeemKey.Text + "\n");
                        }
                    }
                    else
                    {
                        txtBox_redeemKey.Clear();
                        Notification.NotifHelper.MessageBox.Show("Error", "Please write a CDKey Or Invalid.");
                    }
                }
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void btn_importKeys_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    string[] lines = File.ReadAllLines(fbd.FileName);
                    foreach (string line in lines)
                    {
                        CDKeys_Grid.Rows.Add(line);
                    }
                    btn_importKeys.Enabled = false;
                }
            }
            
        }

        private void btn_login2selected_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedUser))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Please select an account!");
                return;
            }
            doLogin(SelectedUser);
        }
        #endregion
        private void doLogin(string username)
        {
            foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
            {
                if (a.username == username)
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
            foreach (var Read in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
            {
                if (Read.username == AccountLogin.CurrentUsername)
                {
                    for (int i = 0; i < Read.Games.Count; i++)
                    {
                        string[] row = { Read.Games[i].name, (Read.Games[i].app_id).ToString() };

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
                SteamKit2.EPersonaState State = Extensions.statesList[combox_states.SelectedIndex];
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
                Process.Start("http://steamcommunity.com/profiles/" + AccountLogin.CurrentSteamID);
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

        #region HandleFormClose
        private void HandleFormEditAccClosed(Object sender, FormClosedEventArgs e)
        {
            RefreshAccountList();
            AccountsList_Grid.Refresh();
            btn_editAcc.Enabled = true;
        }

        private void HandleFormCommentsClosed(Object sender, FormClosedEventArgs e)
        {
            btn_commentsGather.Enabled = true;
        }

        private void HandleFormProfileBackgroundClosed(Object sender, FormClosedEventArgs e)
        {
            Btn_getProfileBackG.Enabled = true;
        }

        private void HandleFormAddAccClosed(Object sender, FormClosedEventArgs e)
        {
            RefreshAccountList();
            AccountsList_Grid.Refresh();
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

        private void HandleFormMsgRecipientsClosed(Object sender, FormClosedEventArgs e)
        {
            btn_MsgRecipients.Enabled = true;
        }
        private void HandleFormSteamRepClosed(Object sender, FormClosedEventArgs e)
        {
            btn_ProfileRepu.Enabled = true;
        }
        #endregion

        #region Mouse Move
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

        #region Links
        private void Link_steamgroup_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/groups/MercuryBOT");
        }

        private void pictureBox_Discord_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/7e7kuhp");
        }

        private void MetroLink_spkMusic_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=ZA1hSn-s44M");
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
            Process.Start("http://denricdenise.info/metroframework-faq/");
        }

        private void metroLink_spk_Click(object sender, EventArgs e)
        {
            Process.Start("http://steamcommunity.com/profiles/76561198041931474");
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
            Process.Start(Program.ExecutablePath);
        }

        private void pictureBox_Github_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sp0ok3r/Mercury");
        }

        private void link_reportBugFeature_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sp0ok3r/Mercury/issues");
        }

        private void pictureBox_Steam_Click(object sender, EventArgs e)
        {
            string MercuryGroup = "https://steamcommunity.com/groups/MercuryBOT";
            if (Program.CurrentProcesses.FirstOrDefault(x => x.ProcessName == "Steam") != null)
            {
                Process.Start("steam://openurl/" + MercuryGroup);
            }
            else
            {
                Process.Start(MercuryGroup);
            }
        }
        #endregion
        private void toolStrip_Logout_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.Logout();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

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
            Program.MercuryProcess.Refresh();

            link_currentRAM.Text = Program.MercuryProcess.PrivateMemorySize64.ToFileSize();
            link_memorysize.Text = Program.MercuryProcess.PagedSystemMemorySize64.ToFileSize();
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
            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            if (toggle_startWindows.Checked)
            {
                SettingsList.startup = true;

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("MercuryBOT", Program.ExecutablePath + @"\MercuryBOT.exe");
            }
            else
            {
                SettingsList.startup = false;

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.DeleteValue("MercuryBOT", false);
            }
            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(SettingsList, Formatting.Indented));
        }

        private void chck_Minimized_CheckedChanged(object sender, EventArgs e)
        {
            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            if (chck_Minimized.Checked)
            {
                SettingsList.startMinimized = true;
            }
            else
            {
                SettingsList.startMinimized = false;
            }
            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(SettingsList, Formatting.Indented));
        }

        private void toggle_hideInTask_CheckedChanged(object sender, EventArgs e)
        {
            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            if (toggle_hideInTask.Checked)
            {
                SettingsList.hideInTaskBar = true;
                this.ShowInTaskbar = false;
            }
            else
            {
                SettingsList.hideInTaskBar = false;
                this.ShowInTaskbar = true;
            }
            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(SettingsList, Formatting.Indented));
        }

        private void toggle_playSound_CheckedChanged(object sender, EventArgs e)
        {
            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));
            if (toggle_playSound.Checked)
            {
                SettingsList.playsound = true;
            }
            else
            {
                SettingsList.playsound = false;
            }
            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(SettingsList, Formatting.Indented));
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

        private void GamesList_Grid_MouseHover(object sender, EventArgs e)
        {
            //Saved by V.7: https://stackoverflow.com/a/53809925
            MongoTip.Show("Press DELETE to delete game SELECTED!\nPress END to delete various!", this, (Control.MousePosition.X - this.Location.X - 8), (Control.MousePosition.Y - this.Location.Y - 30), 500);
        }

        private void CurrentUserSafeUpdater()
        {
            // try
            // {
            if (AccountLogin.IsLoggedIn == true)
            {
                btn_logout.Visible = true;

                lbl_infoLogin.Text = "Trying to login...";

                if (AccountLogin.isSendingMsgs == false)
                {
                    btn_sendMsg2Friends.Enabled = true;
                }

                toggle_chatlogger.Checked = AccountLogin.ChatLogger;

                btn_login2selected.Enabled = false;
                Panel_UserInfo.Visible = true;

                btnLabel_PersonaAndFlag.Invoke(new Action(() => btnLabel_PersonaAndFlag.Text = AccountLogin.UserPersonaName));

                
                if (AccountLogin.UserPlaying)
                {
                    panel_steamStates.BackColor = Color.Green;
                }
                else
                {
                    panel_steamStates.BackColor = Color.LightSkyBlue;
                    picBox_playing.Visible = false;
                }

                if (picBox_SteamAvatar.Image == null && btnLabel_PersonaAndFlag.Image == null)
                {
                    picBox_SteamAvatar.ImageLocation = AccountLogin.GetAvatarLink(AccountLogin.CurrentSteamID);
                    byte[] data = Program.Web.DownloadData("https://www.countryflags.io/" + AccountLogin.UserCountry + "/flat/16.png");

                    MemoryStream ms = new MemoryStream(data);
                    btnLabel_PersonaAndFlag.Image = Image.FromStream(ms);
                }

                if (AccountLogin.isInMercuryGroup)
                {
                    
                }
                //  combox_states.SelectedIndex = AccountLogin.steamFriends.GetPersonaState;

                lbl_currentUsername.Invoke(new Action(() => lbl_currentUsername.Text = AccountLogin.CurrentUsername));
                lbl_infoLogin.Text = "Connected"; // return;
            }
            else
            {
                lbl_infoLogin.Text = "Not logged...";

                btn_logout.Visible = false;
                Panel_UserInfo.Visible = false;
                btn_login2selected.Enabled = true;

                picBox_SteamAvatar.Image = null;
                btnLabel_PersonaAndFlag.Image = null;
                panel_steamStates.BackColor = Color.Gray;
                picBox_SteamAvatar.BackColor = Color.FromArgb(255, 25, 25, 25);
                lbl_currentUsername.Invoke(new Action(() => lbl_currentUsername.Text = "None"));
                btnLabel_PersonaAndFlag.Invoke(new Action(() => btnLabel_PersonaAndFlag.Text = "None"));

                GamesList_Grid.Rows.Clear();// return;
            }
        }

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
            if (MercuryTabControl.SelectedTab == MercuryTabControl.TabPages["metroTab_Games"])
            {
                LoadGamesFromJSON();
            }
        }

        private void lbl_infoversion_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sp0ok3r/Mercury/releases");
            Process.Start("https://github.com/sp0ok3r/Mercury/issues");
        }

        private void btn_clearuserAliases_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                Utils.ClearAliases();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged.");
            }
        }
        private void btnLabel_PersonaAndFlag_Click(object sender, EventArgs e)
        {
            //Utils.ClearAliases();
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

        private void lbl_go2ChatLogs_Click(object sender, EventArgs e)
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
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged.");
            }
        }
        private void btn_clearUnreadMsg_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.steamFriends.RequestOfflineMessages();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged.");
            }
        }


        private void btn_MsgSelectFriends_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true && AccountLogin.Friends.Count != 0)
            {
                Form SMsgRecipients = new MsgRecipients();
                SMsgRecipients.FormClosed += HandleFormMsgRecipientsClosed;
                btn_MsgRecipients.Enabled = false;
                SMsgRecipients.Show();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged.");
            }
        }

        private void btn_ProfileRepu_Click(object sender, EventArgs e)
        {
            Form SteamRep = new SteamRep.SteamRepCheck();
            SteamRep.FormClosed += HandleFormSteamRepClosed;
            btn_ProfileRepu.Enabled = false;
            SteamRep.Show();
        }

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
            Mercury_notifyIcon.Icon = null;

            if (AccountLogin.IsLoggedIn == true)
            {
                AccountLogin.Logout();
            }

            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void combox_Colors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            SettingsList.startupColor = combox_Colors.SelectedIndex;

            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(SettingsList, new JsonSerializerSettings { Formatting = Formatting.Indented }));

            this.components.SetStyle(this);
            this.Refresh();
        }

        private void Mercury_notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Activate();
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void toolStrip_Acc_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (ToolStripMenuItem subItem in toolStrip_Acc.DropDownItems)
            {
                if (subItem.Selected)
                {
                    if (AccountLogin.IsLoggedIn)
                    {
                        AccountLogin.Logout();
                    }
                    doLogin(subItem.Text);
                }
            }
        }

        private void combox_defaultTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            SettingsList.startupTab = combox_defaultTab.SelectedIndex;

            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(SettingsList, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private void btn_userdata_Click(object sender, EventArgs e)
        {

            if (Extensions.SteamLocation == null)
            {
                btn_userdata.Enabled = false;
            }
            else
            {

                btn_userdata.Enabled = true;
                if (AccountLogin.IsLoggedIn == true)
                {
                   Process.Start(Extensions.SteamLocation + @"/userdata/" + Extensions.AllToSteamId3(AccountLogin.CurrentSteamID).Substring(1));
                }
                else
                {
                    Process.Start(Extensions.SteamLocation + @"\userdata");
                }
            }
        }

        private void btn_clearRecentapps_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true && AccountLogin.UserPlaying == false)
            {
                //AccountLogin.UserPlaying
                List<uint> ClearGameID = new List<uint>() { 635240, 635241, 635242 };

                foreach (uint id in ClearGameID)
                {
                    Utils.PlayNormal1App(id);
                    Thread.Sleep(1500);
                    Utils.PlayNormal1App(0);
                }
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged/playing!");
            }
        }

        private void combox_notifEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            var SettingsList = JsonConvert.DeserializeObject<MercurySettings>(File.ReadAllText(Program.SettingsJsonFile));

            //  SettingsList.notificationEffect = Extensions.notifEffects[combox_notifEffect.SelectedIndex];

            File.WriteAllText(Program.SettingsJsonFile, JsonConvert.SerializeObject(SettingsList, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private void btn_accSettings_Click(object sender, EventArgs e)
        {

        }

        private void picBox_playing_Click(object sender, EventArgs e)
        {
            stopIdling();
            MercuryTabControl.SelectedIndex = 3;
            
        }

        private void picBox_playing_MouseHover(object sender, EventArgs e)
        {
            picBox_playing.Image =Properties.Resources.stop_playing;
        }

        private void picBox_playing_MouseLeave(object sender, EventArgs e)
        {
            picBox_playing.Image = Properties.Resources.playing;
        }

        private void btn_idleSettings_Click(object sender, EventArgs e)
        {

        }

        private void btn_rateup_Click(object sender, EventArgs e)
        {

        }

        private void btn_steamLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedUser))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Please select an account!");
                return;
            }

            Extensions.KillSteam();
            foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
            {
                if (a.username == SelectedUser)
                {
                    if (string.IsNullOrEmpty(a.password))
                    {
                        InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Please add password to: " + a.username);
                        return;
                    }
                    LoginAccountInClient(a.username, a.password);
                }
            }

            
        }
        private void btn_addCDKey_Click(object sender, EventArgs e)
        {
            //thanks to https://regexr.com/3b63e
            Match match = new Regex(@"((?![^0-9]{12,}|[^A-z]{12,})([A-z0-9]{4,5}-?[A-z0-9]{4,5}-?[A-z0-9]{4,5}(-?[A-z0-9]{4,5}(-?[A-z0-9]{4,5})?)?))").Match(txtBox_redeemKey.Text);
            if (!string.IsNullOrEmpty(txtBox_redeemKey.Text) && match.Success)
            {
                txtBox_redeemKey.Clear();
                CDKeys_Grid.Rows.Add(txtBox_redeemKey.Text);
            }
            else
            {
                txtBox_redeemKey.Clear();
                Notification.NotifHelper.MessageBox.Show("Error", "Please write a CDKey Or Invalid.");
            }
        }

        private void CDKeys_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= CDKeys_Grid.Rows.Count)
            {
                return;
            }
            CDKeys_Grid.FirstDisplayedScrollingRowIndex = e.NewValue;
        }

        public static bool LoginAccountInClient(string user,string pw)
        {
            var steam = Path.Combine(Extensions.SteamLocation, "Steam.exe");
            if (!File.Exists(steam))
            {
                MessageBox.Show("Can't find Steam.exe in steam path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = steam,
                    Arguments = "-silent -login \"" + user + "\" \"" + pw.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\""
                });
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to start steam.\n\n" +
                    "Exception Details:\n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}