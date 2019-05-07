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
        public SteamRepCheck()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void btn_checkUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_repSteamID.Text))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Insert SteamID");
                Title_AlertScammer.Visible = false;
                // return;
            }
            else
            {
                //Resolve Vanity URL

                var resp = new WebClient().DownloadString("https://steamcommunity.com/miniprofile/" + (Convert.ToUInt64(txt_repSteamID.Text) - 76561197960265728)); // 326iq
                picBox_SteamAvatar.ImageLocation = Regex.Match(resp, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value; // slow but 983iq

                if (Extensions.SteamRep(Extensions.AllToSteamId32(txt_repSteamID.Text)) == true)
                {
                    Title_AlertScammer.Visible = true;
                    Title_AlertScammer.BackColor = Color.DarkRed;
                    Title_AlertScammer.Text = "SCAMMER";
                }
                else
                {
                    Title_AlertScammer.Visible = true;
                    Title_AlertScammer.BackColor = Color.DarkGreen;
                    Title_AlertScammer.Text = "CLEAN AF";
                }
            }
        }

        private void metroLink_steamrep_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamrep.com/");
        }

        private void Title_AlertScammer_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamrep.com/search?q=" + txt_repSteamID.Text);
        }
    }
}
