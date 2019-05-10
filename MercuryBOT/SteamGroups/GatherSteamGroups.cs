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
using MetroFramework.Controls;
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
                AccountLogin.LeaveGroup((GroupSelected).ToString(), GroupNameSelected);
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

            }
            else
            {
                btn_exitSelected.Enabled = false;
                btn_exitfromAll.Enabled = false;
                foreach (KeyValuePair<ulong, string> group in AccountLogin.ClanDictionary)
                {
                    AccountLogin.LeaveGroup((group.Key).ToString(), group.Value);

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
                    AccountLogin.MakeGroupAnnouncement(GroupSelected, txtBox_title.Text, txtBox_Annonbody.Text);
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
                AccountLogin.setGroupPlayerOfTheWeek(GroupSelected, Extensions.AllToSteamId32(txt_potwSteamID.Text));
            }
        }

        private void btn_gatherFromProfile_Click(object sender, EventArgs e)
        {
            string resp = AccountLogin.steamWeb.Fetch("https://steamcommunity.com/id/sp0okER/?xml=1", "GET"); // CHANGE
            if (resp != String.Empty)
            {
                string pattern = "(<groupID64>)(.*?)(</groupID64>)";

                Regex gids = new Regex(pattern, RegexOptions.Compiled);
                MatchCollection matches = gids.Matches(resp);

                foreach (Match match in matches)
                {
                    try
                    {
                        Console.WriteLine(match.Groups[0].Value);
                    }
                    catch { }
                }
            }
        }

        private void btn_joinAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBox_groupidsFile.Text))
            {
                btn_joinAll.Enabled = false;
                var lines = File.ReadLines(txtBox_groupidsFile.Text);
                foreach (var line in lines)
                {
                    AccountLogin.JoinGroup(line);
                    Thread.Sleep(5);
                }
                btn_joinAll.Enabled = true;
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Joined all groups in file!");
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error","Please select the file location");
            }
        }

        private void link_setfile_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtBox_groupidsFile.Text = fbd.SelectedPath;
                }
            }
        }
    }
}