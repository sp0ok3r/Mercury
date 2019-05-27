/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using MercuryBOT.Helpers;
using Win32Interop.Methods;

namespace MercuryBOT.FriendsList
{
    public partial class MsgRecipients : MetroFramework.Forms.MetroForm
    {
        public MsgRecipients()
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
        private void FriendsMsgReceiver_Load(object sender, EventArgs e)
        {
            RefreshRecipients();
        }
        private void btn_saveFriends_Click(object sender, EventArgs e)
        {
            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));

            foreach (var a in list.Accounts)
            {
                if (a.username == AccountLogin.CurrentUsername)
                {
                    foreach (DataGridViewRow row in FriendsList_Grid.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value) == true && a.MsgRecipients.Any(s => s.Contains(row.Cells[0].Value.ToString())) == false)
                        {
                            a.MsgRecipients.Add(row.Cells[0].Value.ToString());
                        }

                        if (Convert.ToBoolean(row.Cells[2].Value) == false)
                        {
                            a.MsgRecipients.Remove(row.Cells[0].Value.ToString());

                        }
                    }
                }
            }
            File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented));
            InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Saved");
            Close();
        }

        public void RefreshRecipients()
        {
            FriendsList_Grid.Rows.Clear();
            foreach (var f in AccountLogin.Friends)
            {
                var Name = AccountLogin.GetPersonaName(f.ConvertToUInt64());

                var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                bool ui = false;
                foreach (var a in list.Accounts)
                {
                    if (a.username == AccountLogin.CurrentUsername)
                    {
                        if(a.MsgRecipients == null)
                        {

                        }
                        if (a.MsgRecipients.Any(s => s.Contains(f.ConvertToUInt64().ToString())))
                        {
                            ui = true;
                        }
                    }
                }

                string[] row = { f.ConvertToUInt64().ToString(), Name, ui.ToString() };
                FriendsList_Grid.Rows.Add(row);
            }
        }

        private void FriendsList_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= FriendsList_Grid.Rows.Count)
            {
                return;
            }
            FriendsList_Grid.FirstDisplayedScrollingRowIndex = e.NewValue;
        }
    }
}