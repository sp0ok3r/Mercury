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
using System.Windows.Forms;
using Win32Interop.Methods;
using System.Drawing;
using System.Linq;

namespace MercuryBOT
{
    public partial class SteamGuard : MetroFramework.Forms.MetroForm
    {
        public static string AuthCode;

        public SteamGuard(string EmailorPhone, string user)
        {
            InitializeComponent(); this.Activate();
            this.FormBorderStyle = FormBorderStyle.None;
            this.components.SetStyle(this);
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));

            foreach (var button in this.Controls.OfType<MetroFramework.Controls.MetroButton>())
            {
                IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, button.Width, button.Height, 5, 5);
                button.Region = Region.FromHrgn(ptr);
                Gdi32.DeleteObject(ptr);
            }

            lbl_account.Text = user;
            if (EmailorPhone == "Phone")
            {
                lbl_infoemailorPhone.Text = "Enter your two-factor authentication code";
                lbl_emojiInfo.Text = "📱";
            }
            else
            {
                lbl_infoemailorPhone.Text = "Enter Steam Guard code from your email";
                lbl_emojiInfo.Text = "📧";
                MongoToolTip.SetToolTip(lbl_emojiInfo, EmailorPhone);
            }
        }

        private void SteamGuard_Load(object sender, EventArgs e)
        {
            txtBox_Code.Focus();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (AuthCode != "")
            {
                AuthCode = txtBox_Code.Text;
                
                this.Close();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            AccountLogin.Logout();
            this.Close();
        }

        private void SteamGuard_Shown(object sender, EventArgs e)
        {
            this.Activate();
            // Stream str = Properties.Resources.;
            // SoundPlayer snd = new SoundPlayer(str);
            // snd.Play();
        }
    }
}
