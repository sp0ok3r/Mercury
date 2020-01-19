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
using MercuryBOT.SteamCommunity;
using MetroFramework.Controls;
using SteamKit2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Win32Interop.Methods;

namespace MercuryBOT.SteamGroups
{
    public partial class GatherSteamGroups : MetroFramework.Forms.MetroForm
    {
        public GatherSteamGroups()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            this.MercuryTabControlGroups.SelectedIndex = 0;

            foreach (Control tab in MercuryTabControlGroups.Controls)
            {
                TabPage tabPage = (TabPage)tab;
                foreach (Control control in tabPage.Controls.OfType<MetroButton>())
                {
                    IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, control.Width, control.Height, 5, 5);
                    control.Region = Region.FromHrgn(ptr);
                    Gdi32.DeleteObject(ptr);
                }
            }

        }

        private void ClanList_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= GridClanData.Rows.Count)
            {
                return;
            }
            GridClanData.FirstDisplayedScrollingRowIndex = e.NewValue;
        }

        public void RefreshClanList()
        {
            GridClanData.Rows.Clear();
            AccountLogin.UserClanIDS();
            foreach (KeyValuePair<ulong, string> group in AccountLogin.ClanDictionary)
            {
                string[] row = { (group.Key).ToString(), group.Value };
                GridClanData.Rows.Add(row);
            }
            ClanList_ScrollBar.Maximum = GridClanData.Rows.Count;
        }

        private void GatherSteamGroups_Shown(object sender, EventArgs e)
        {
            RefreshClanList();
        }

        string GroupSelected = "None";

        string GroupNameSelected = "None";

        private void btn_exitSelected_Click(object sender, EventArgs e)
        {
            if (GroupSelected == "None")
            {

            }
            else
            {
                btn_exitfromAll.Enabled = false;
                SteamCommunity.Utils.LeaveGroup((GroupSelected).ToString(), GroupNameSelected);
                AccountLogin.ClanDictionary.Remove(Convert.ToUInt64(GroupSelected));

                GridClanData.Rows.RemoveAt(GridClanData.SelectedRows[0].Cells[0].RowIndex);
                btn_exitfromAll.Enabled = true;
                RefreshClanList();
            }
        }

        private void GridClanData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridClanData.SelectedRows.Count > 0)
            {
                GroupSelected = GridClanData.SelectedRows[0].Cells[0].Value + string.Empty;
                GroupNameSelected = GridClanData.SelectedRows[0].Cells[1].Value + string.Empty;
                lbl_groupSelected.Text = "Selected: " + GroupNameSelected;
            }
        }

        private void btn_exitfromAll_Click(object sender, EventArgs e)
        {
            if (GroupSelected == "None")
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Select a group.");
            }else{
                btn_exitSelected.Enabled = false;
                btn_exitfromAll.Enabled = false;
                foreach (KeyValuePair<ulong, string> group in AccountLogin.ClanDictionary)
                {
                    SteamCommunity.Utils.LeaveGroup((group.Key).ToString(), group.Value);

                    Thread.Sleep(30);
                    Console.WriteLine("DDDDeleted");
                }
                btn_exitSelected.Enabled = true;
                btn_exitfromAll.Enabled = true;
                Notification.NotifHelper.MessageBox.Show("Info", "Left successfully " + GroupNameSelected + " !");
            }
        }

        private void btn_save2file_Click(object sender, EventArgs e)
        {
            using (TextWriter tw = new StreamWriter(AccountLogin.CurrentSteamID + "-GroupsIDS.txt"))
            {
                btn_save2file.Enabled = false;
                foreach (KeyValuePair<ulong, string> group in AccountLogin.ClanDictionary)
                {
                    tw.WriteLine((group.Key).ToString());
                }
                btn_save2file.Enabled = true;
                Process.Start(Program.ExecutablePath + @"\" + AccountLogin.CurrentSteamID + "-GroupsIDS.txt");
            }
        }

        private void btn_groupAnnouncement_Click(object sender, EventArgs e)
        {
            if (GroupSelected == "None")
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Select a group.");
            }
            else
            {
                if (string.IsNullOrEmpty(txtBox_title.Text) || string.IsNullOrEmpty(txtBox_Annonbody.Text))
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Please write the title or the body.");
                    return;
                }
                else
                {
                    SteamCommunity.Utils.MakeGroupAnnouncement(GroupSelected, txtBox_title.Text, txtBox_Annonbody.Text);
                }
            }
        }

        private void txtBox_gName_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                if (txtBox_gName.Text == "")
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Write the group name please.");
                }
                else
                {
                    int rowIndex = -1;
                    foreach (DataGridViewRow row in GridClanData.Rows)
                    {

                        if (row.Cells[1].Value.ToString().StartsWith(txtBox_gName.Text))
                        {

                            rowIndex = row.Index;
                            GridClanData.Rows[rowIndex].Selected = true;
                            GridClanData.FirstDisplayedScrollingRowIndex = rowIndex;
                            break;
                        }
                    }
                }
            }
        }

        private void txtBox_gName_TextChanged(object sender, EventArgs e)
        {

            int rowIndex = -1;
            foreach (DataGridViewRow row in GridClanData.Rows)
            {

                if (row.Cells[1].Value.ToString().Contains(txtBox_gName.Text))
                {

                    rowIndex = row.Index;
                    GridClanData.Rows[rowIndex].Selected = true;
                    GridClanData.FirstDisplayedScrollingRowIndex = rowIndex;
                    break;
                }
                // Console.WriteLine(rowIndex);
            }
        }

        private void btn_potw_Click(object sender, EventArgs e)
        {
            if (GroupSelected == "None" || string.IsNullOrEmpty(txt_potwSteamID.Text))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Select a group/Insert SteamID");
            }
            else
            {
                SteamCommunity.Utils.setGroupPlayerOfTheWeek(GroupSelected, Extensions.AllToSteamId32(txt_potwSteamID.Text));
            }
        }

        private void btn_gatherFromProfile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBox_profileGrabIDS.Text))
            {
                string steamid64 = Extensions.AllToSteamId64(txtBox_profileGrabIDS.Text);
                string resp = AccountLogin.steamWeb.Fetch("https://steamcommunity.com/profiles/" + steamid64 + "/?xml=1", "GET"); // CHANGE
                if (resp != string.Empty)
                {
                    using (TextWriter tw = new StreamWriter(steamid64 + "_GroupsIDS.txt"))
                    {
                        {
                            foreach (Match groupsIDS in Regex.Matches(resp, @"<groupID64>(.*?)</groupID64>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                            {
                                tw.WriteLine(groupsIDS.Groups[1].Value);
                            }
                        }
                    }
                    //Process.Start(Program.ExecutablePath + @"\" + steamid64 + "_GroupsIDS.txt");
                    InfoForm.InfoHelper.CustomMessageBox.Show("Info", "All group ids saved!");
                }
            }
            else
            {
                Console.WriteLine("nao");
            }
        }
        private void btn_joinAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBox_groupidsFile.Text))
            {
                ProgressSpinner_JoinAllGroups.Visible = true;
                int groupsJoined = 0;
                btn_joinAll.Enabled = false;
                string[] lines = File.ReadAllLines(txtBox_groupidsFile.Text);
                foreach (string line in lines)
                {
                    SteamCommunity.Utils.JoinGroup(line);
                    groupsJoined++;
                    Thread.Sleep(25);
                }
                ProgressSpinner_JoinAllGroups.Visible = false;
                btn_joinAll.Enabled = true;
                link_setfile.Enabled = true;
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Joined all groups in file!");
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Please select the file location");
            }
        }

        private void link_setfile_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    txtBox_groupidsFile.Text = fbd.FileName;
                }
            }
            if(txtBox_groupidsFile.Text.Length != 0)
            {
                link_setfile.Enabled = false;
            }
        }

        private void btn_massInvite_Click(object sender, EventArgs e)
        {
            if (GroupSelected == "None")
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Select a group.");
            }
            else
            {
                ProgressSpinner_MassInvite.Visible = true;
                btn_massInvite.Enabled = false;
                for (int i = 0; i <= AccountLogin.steamFriends.GetFriendCount(); i++)
                {
                    SteamID allfriends = AccountLogin.steamFriends.GetFriendByIndex(i);
                    if (allfriends.ConvertToUInt64() != 0)
                    {
                        {
                            Utils.GroupInvite(Convert.ToUInt64(GroupSelected), allfriends.ConvertToUInt64());
                            Thread.Sleep(500);
                        }
                    }
                }
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "All users invited");
                ProgressSpinner_MassInvite.Visible = false;
                btn_massInvite.Enabled = true;
            }
        }

        private void txtBox_groupidsFile_Click(object sender, EventArgs e)
        {
            if (txtBox_groupidsFile.Text.Length == 0)
            {
                link_setfile.Enabled = true;
            }
        }
    }
}