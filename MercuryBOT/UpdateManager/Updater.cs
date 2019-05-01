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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MercuryBOT.Helpers.Extensions;
using static MercuryBOT.User2Json.GitHubApi;

namespace MercuryBOT
{
    public partial class Update : MetroFramework.Forms.MetroForm
    {
        public Update(string up)
        {

            InitializeComponent(); this.Activate();
            lbl_infoversion.Text = up;
            this.components.SetStyle(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void Update_Load(object sender, EventArgs e)
        {
            this.Activate();
            Stream str = Properties.Resources.mercury_update;
            SoundPlayer snd = new SoundPlayer(str);
            snd.Play();
        }

        private void Update_Shown(object sender, EventArgs e)
        {
            FlashWindow.Flash(this);
            RetrieveChangelog();
        }

        private string DownloadLink;
        private void RetrieveChangelog()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Mercury Client");
                    client.Encoding = Encoding.UTF8;

                    Uri uri = new Uri("https://api.github.com/repos/sp0ok3r/Mercury/releases");
                    string releases = client.DownloadString(uri);
                    foreach (var g in JsonConvert.DeserializeObject<List<GithubRelease>>(releases))
                    {
                        txtBox_changelog.Text += g.body;
                        DownloadLink = g.assets[0].browser_download_url;
                    }
                }
            }
            catch (Exception)
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "github error");
                Process.Start("https://github.com/sp0ok3r/Mercury/releases");
            }
        }


        private void Update_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Btn_installupdate_Click(object sender, EventArgs e)
        {
            Process.Start(Program.ExecutablePath);
            Process.Start(DownloadLink);
        }
    }
}