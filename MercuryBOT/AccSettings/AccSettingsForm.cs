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
using System.Linq;
using Win32Interop.Methods;
using System.Drawing;
using MercuryBOT.SteamCommunity;
using Mercury;

namespace MercuryBOT.AccSettings
{
    public partial class AccSettingsForm : MetroFramework.Forms.MetroForm
    {
        public AccSettingsForm()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            foreach (var button in this.Controls.OfType<MetroFramework.Controls.MetroButton>())
            {
                IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, button.Width, button.Height, 5, 5);
                button.Region = Region.FromHrgn(ptr);
                Gdi32.DeleteObject(ptr);
            }
        }

        private void btn_setName_Click(object sender, EventArgs e)
        {
            HandleLogin.ChangeCurrentName(txtBox_nameChange.Text);
            txtBox_nameChange.Clear();
        }

        private void btn_clearuserAliases_Click(object sender, EventArgs e)
        {
            if (HandleLogin.IsLoggedIn == true)
            {
                Utils.ClearAliasesAsync();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged.");
            }
        }

        private void combox_states_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combox_uimodes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
