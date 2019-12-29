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
using System.IO;
using System.Media;
using System.Net;
using System.Windows.Forms;
using Win32Interop.Methods;
using System.Drawing;

namespace MercuryBOT
{
    public partial class Update : MetroFramework.Forms.MetroForm
    {
        public Update(string up)
        {
            InitializeComponent();

            lbl_infoversion.Text = up;
            this.components.SetStyle(this);
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            
            IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, btn_installupdate.Width, btn_installupdate.Height, 5, 5);
            btn_installupdate.Region = Region.FromHrgn(ptr);
            Gdi32.DeleteObject(ptr);
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
            RetrieveChangelog();
        }

        private void RetrieveChangelog()
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
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "sp0ok3r.tk is down, please check for new updates in github!");
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
            //Process.Start("https://github.com/sp0ok3r/Mercury/releases/tag/" + lbl_infoversion.Text);
            Process.Start("https://github.com/sp0ok3r/Mercury/releases/");
        }
    }
}