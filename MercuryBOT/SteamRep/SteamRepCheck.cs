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
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using Win32Interop.Methods;
using System.Drawing;
using System.Linq;

namespace MercuryBOT.SteamRep
{
    public partial class SteamRepCheck : MetroFramework.Forms.MetroForm
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public SteamRepCheck()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = System.Drawing.Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            foreach (var button in this.Controls.OfType<MetroFramework.Controls.MetroButton>())
            {
                IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, button.Width, button.Height, 5, 5);
                button.Region = Region.FromHrgn(ptr);
                Gdi32.DeleteObject(ptr);
            }
        }
        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        private void btn_checkUser_Click(object sender, EventArgs e)
        {
            ProgressSpinner_SteamRepDelay.Visible = true;

            if (string.IsNullOrEmpty(txt_repSteamID.Text))//&& !txt_repSteamID.Text.StartsWith("765611")
            {
                Title_AlertScammer.Visible = false;
                ProgressSpinner_SteamRepDelay.Visible = false;
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Insert a steam profile url / id32-64");
            }
            else
            {
                var finalID = Extensions.AllToSteamId64(txt_repSteamID.Text);

                if (finalID != String.Empty)
                {
                    //var avatar = Regex.Match(RespSteamProfile, "<avatarIcon>(.*?)</avatarIcon>").Groups[0];
                    using (WebClient a = new WebClient())
                    {
                        var resp = a.DownloadString("https://steamcommunity.com/miniprofile/" + (Convert.ToUInt64(finalID) - 76561197960265728)); // 326iq
                        picBox_SteamAvatar.ImageLocation = Regex.Match(resp, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase | RegexOptions.Compiled).Groups[1].Value; // slow but 983iq

                        string SteamRepJsonGather = a.DownloadString("http://steamrep.com/api/beta4/reputation/" + finalID + "?json=1&extended=1");

                        var SteamRepJsonRead = SteamRepAPI.JsonSteamRep.FromJson(SteamRepJsonGather);

                        Title_AlertScammer.Visible = true;
                        switch (SteamRepJsonRead.Steamrep.Reputation.Summary)
                        {
                            case "CAUTION":
                                Title_AlertScammer.BackColor = Color.Yellow;
                                Title_AlertScammer.Text = "CAUTION";
                                break;
                            case "SCAMMER":
                                Title_AlertScammer.BackColor = Color.DarkRed;
                                Title_AlertScammer.Text = "SCAMMER";
                                break;
                            default:
                                Title_AlertScammer.BackColor = Color.DarkGreen;
                                Title_AlertScammer.Text = "CLEAN";
                                break;
                        }
                        lbl_checkUsername.Text = SteamRepJsonRead.Steamrep.Rawdisplayname;
                        lbl_steamid32.Text     = SteamRepJsonRead.Steamrep.SteamId32;
                        lbl_steamID64.Text     = finalID;
                    }
                    timer.Interval = 5000;
                    timer.Tick += AntiSpam_Tick;
                    timer.Start();
                    btn_checkUser.Enabled = false;
                }
            }
        }

        private void metroLink_steamrep_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamrep.com/");
            Process.Start("https://forums.steamrep.com/threads/steamrep-web-api-beta4-legacy-public.114688/");
        }

        private void Title_AlertScammer_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamrep.com/search?q=" + txt_repSteamID.Text);
        }

        private void AntiSpam_Tick(object sender, EventArgs e)
        {
            ProgressSpinner_SteamRepDelay.Visible = false;

            btn_checkUser.Enabled = true;
            timer.Stop();
        }

        private void picBox_SteamAvatar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_repSteamID.Text) && txt_repSteamID.Text.StartsWith("765611"))
            {
                Process.Start("http://steamcommunity.com/profiles/" + txt_repSteamID);
            }
        }
    }
}