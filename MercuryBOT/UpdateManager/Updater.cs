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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MercuryBOT
{
    public partial class Update : MetroFramework.Forms.MetroForm
    {
        public Update()
        {
            InitializeComponent(); this.Activate();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void Update_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void Update_Shown(object sender, EventArgs e)
        {
            FlashWindow.Flash(this);
            RetrieveChangelog();
        }

        public void RetrieveChangelog()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    txtBox_changelog.Text += client.DownloadString(Program.spkDomain + "update-changelog.php");
                }
            }
            catch (Exception)
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "sp0ok3r.tk is down, entering in another link!");
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
            Process.Start(Program.spkDomain + "downloads/MercuryBOT-release.zip");
        }
    }
}