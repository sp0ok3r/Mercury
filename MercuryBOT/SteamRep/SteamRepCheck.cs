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

namespace MercuryBOT.SteamRep
{
    public partial class SteamRepCheck : MetroFramework.Forms.MetroForm
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        
        public SteamRepCheck()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void btn_checkUser_Click(object sender, EventArgs e)
        {
            ProgressSpinner_SteamRepDelay.Visible = true;

            if (string.IsNullOrEmpty(txt_repSteamID.Text) )//&& !txt_repSteamID.Text.StartsWith("765611")
            {
                Title_AlertScammer.Visible = false;
                ProgressSpinner_SteamRepDelay.Visible = false;
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Insert a valid SteamID64 \n Like: 76561198041931474");
                
            }
            else
            {
                var resp = new WebClient().DownloadString("https://steamcommunity.com/miniprofile/" + (Convert.ToUInt64(txt_repSteamID.Text) - 76561197960265728)); // 326iq
                picBox_SteamAvatar.ImageLocation = Regex.Match(resp, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value; // slow but 983iq

                string SteamRepJsonGather = new WebClient().DownloadString("http://steamrep.com/api/beta4/reputation/" + txt_repSteamID.Text + "?json=1&extended=1");

                var SteamRepJsonRead = SteamRepAPI.JsonSteamRep.FromJson(SteamRepJsonGather);

                switch (SteamRepJsonRead.Steamrep.Reputation.Summary)
                {
                    case "CAUTION":
                        Title_AlertScammer.Visible = true;
                        Title_AlertScammer.BackColor = Color.Yellow;
                        Title_AlertScammer.Text = "CAUTION";
                        break;
                    case "SCAMMER":
                        Title_AlertScammer.Visible = true;
                        Title_AlertScammer.BackColor = Color.DarkRed;
                        Title_AlertScammer.Text = "SCAMMER";
                        break;
                    default:
                        Title_AlertScammer.Visible = true;
                        Title_AlertScammer.BackColor = Color.DarkGreen;
                        Title_AlertScammer.Text = "CLEAN";
                        break;
                }
                lbl_checkUsername.Text = SteamRepJsonRead.Steamrep.Rawdisplayname;
                lbl_steamid32.Text = SteamRepJsonRead.Steamrep.SteamId32;
                lbl_steamID64.Text = SteamRepJsonRead.Steamrep.SteamId64;

                timer.Interval = 5000;
                timer.Tick += AntiSpam_Tick;
                timer.Start();
                btn_checkUser.Enabled = false;
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
