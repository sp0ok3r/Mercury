using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MercuryBOT.CustomMessages
{
    public partial class AFKMessages : MetroFramework.Forms.MetroForm
    {
        public AFKMessages()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void Btn_addMessage2json_Click(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true && txtBox_customMSG.Text.Length != 0)
            {

                var ListsMessages = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var UserList in ListsMessages.Accounts)
                {
                    if (UserList.username == AccountLogin.CurrentUsername)
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

                listview_customMsgs.Items.Add(txtBox_customMSG.Text);
                txtBox_customMSG.Clear();

            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged! or Message too short");
            }
        }

        private void AFKMessages_Load(object sender, EventArgs e)
        {
            if (AccountLogin.IsLoggedIn == true)
            {
                var ListsMessages = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var UserList in ListsMessages.Accounts)
                {
                    if (UserList.username == AccountLogin.CurrentUsername)
                    {
                        for (int i = 0; i < UserList.AFKMessages.Count; i++)
                        {
                            listview_customMsgs.Items.Add(UserList.AFKMessages[i].Message);
                        }
                    }
                }
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged!");
            }
        }

        int iSELECTED = -1;
        private void Btn_deleteSelected_Click(object sender, EventArgs e)
        {
            if (iSELECTED != -1)
            {
                var ListsMessages = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var UserList in ListsMessages.Accounts)
                {
                    if (UserList.username == AccountLogin.CurrentUsername)
                    {
                        for (int i = 0; i < UserList.AFKMessages.Count; i++)
                        {
                            if (UserList.AFKMessages[i] == UserList.AFKMessages[iSELECTED])
                            {
                                UserList.AFKMessages.Remove(UserList.AFKMessages[i]);
                                listview_customMsgs.Items.RemoveAt(i);
                            }
                        }
                    }
                }
                File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(ListsMessages, Formatting.Indented));
            }else{
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "No message selected!");
            }
        }

        private void Listview_customMsgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listview_customMsgs.SelectedItems.Count > 0)
            {
                btn_deleteSelected.Enabled = true;
                iSELECTED = listview_customMsgs.SelectedItems[0].Index;
                lbl_selectedMessage.Text = "Selected: "+ iSELECTED;
            }
            else
            {
                btn_deleteSelected.Enabled = false;
            }
        }
    }
}