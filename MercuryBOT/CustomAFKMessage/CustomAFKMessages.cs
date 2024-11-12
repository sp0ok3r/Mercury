/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using Mercury;
using MercuryBOT.Helpers;
using MercuryBOT.UserSettings;
using MetroFramework.Controls;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Win32Interop.Methods;

namespace MercuryBOT.CustomMessages
{
    public partial class AFKMessages : MetroFramework.Forms.MetroForm
    {
        public static string SelectedMsg = "";


        public AFKMessages()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            foreach (var button in this.Controls.OfType<MetroButton>())
            {
                IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, button.Width, button.Height, 5, 5);
                button.Region = Region.FromHrgn(ptr);
                Gdi32.DeleteObject(ptr);
            }
        }


        public void AddMessage2File()
        {
            if (HandleLogin.IsLoggedIn == true && txtBox_customMSG.Text.Length != 0)
            {

                var ListsMessages = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var UserList in ListsMessages.Accounts)
                {
                    if (UserList.username == HandleLogin.CurrentUsername)
                    {
                        for (int i = 0; i < UserList.AFKMessages.Count; i++)
                        {
                            if (UserList.AFKMessages[i].Message == txtBox_customMSG.Text)
                            {
                                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Dont duplicate, Mercury dont like it :c");
                                txtBox_customMSG.Clear();
                                return;
                            }
                        }
                        UserSettings.CustomMessages NewMessage = new UserSettings.CustomMessages { Message = txtBox_customMSG.Text };
                        UserList.AFKMessages.Add(NewMessage);

                    }
                }

                File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(ListsMessages, Formatting.Indented));

                // listview_customMsgs.Items.Add(txtBox_customMSG.Text);
                SavedMsgs_Grid.Rows.Add(txtBox_customMSG.Text);
                txtBox_customMSG.Clear();

            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged! or Message too short");
            }
        }
        public void RemoveMessage4romFile()
        {
            if (SelectedMsg != "")
            {
                var ListsMessages = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var UserList in ListsMessages.Accounts)
                {
                    if (UserList.username == HandleLogin.CurrentUsername)
                    {
                        for (int i = 0; i < UserList.AFKMessages.Count; i++)
                        {
                            if (UserList.AFKMessages[i] == UserList.AFKMessages[SavedMsgs_Grid.SelectedRows[0].Cells[0].RowIndex])
                            {
                                UserList.AFKMessages.Remove(UserList.AFKMessages[i]);
                                SavedMsgs_Grid.Rows.RemoveAt(SavedMsgs_Grid.SelectedRows[0].Cells[0].RowIndex);
                            }
                        }
                    }
                }
                File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(ListsMessages, Formatting.Indented));
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "No message selected!");
            }
        }


        private void Btn_addMessage2json_Click(object sender, EventArgs e)
        {
            AddMessage2File();
        }

        private void AFKMessages_Load(object sender, EventArgs e)
        {
            if (HandleLogin.IsLoggedIn == true)
            {
                var ListsMessages = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var UserList in ListsMessages.Accounts)
                {
                    if (UserList.username == HandleLogin.CurrentUsername)
                    {
                        for (int i = 0; i < UserList.AFKMessages.Count; i++)
                        {
                            SavedMsgs_Grid.Rows.Add(UserList.AFKMessages[i].Message);
                        }
                    }
                }
                SavedMsgs_Grid.ClearSelection();
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        private void Btn_deleteSelected_Click(object sender, EventArgs e)
        {
            RemoveMessage4romFile();
        }

        private void SavedMsgs_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= SavedMsgs_Grid.Rows.Count)
            {
                return;
            }
            SavedMsgs_Grid.FirstDisplayedScrollingRowIndex = e.NewValue;
        }

        private void SavedMsgs_Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedMsg = SavedMsgs_Grid.SelectedRows[0].Cells[0].Value.ToString();
            lbl_selectedMessage.Text = "Selected: " + SelectedMsg;
        }

        private void txtBox_customMSG_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                AddMessage2File();
            }
            if (Keys.Delete == e.KeyCode)
            {
                RemoveMessage4romFile();
            }

        }
    }
}