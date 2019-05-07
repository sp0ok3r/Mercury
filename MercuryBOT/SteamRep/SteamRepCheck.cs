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
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                return;
            }
            else
            {
                Title_AlertScammer.Visible = true;
                picBox_SteamAvatar.ImageLocation = AccountLogin.GetAvatarLink(Convert.ToUInt64(Extensions.AllToSteamId32(txt_repSteamID.Text))));
                if (Extensions.SteamRep(Extensions.AllToSteamId32(txt_repSteamID.Text)) == true)
                {
                    Title_AlertScammer.BackColor = Color.DarkRed;
                    Title_AlertScammer.Text = "SCAMMER";
                }
                else
                {
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
            Process.Start("https://steamrep.com/search?q="+ txt_repSteamID.Text);

        }
    }
}
