using MercuryBOT.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MercuryBOT.SteamGroups
{
    public partial class GatherSteamGroups : MetroFramework.Forms.MetroForm
    {
        public GatherSteamGroups()
        {
            InitializeComponent();
            Region = Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
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
            }
        }

        private void btn_exitfromAll_Click(object sender, EventArgs e)
        {
            if (GroupSelected == "None")
            {

            }
            else
            {
                btn_exitSelected.Enabled = false;
                btn_exitfromAll.Enabled = false;
                foreach (KeyValuePair<ulong, string> group in AccountLogin.ClanDictionary)
                {
                    AccountLogin.LeaveGroup((group.Key).ToString(), group.Value);

                    Thread.Sleep(1);
                    Console.WriteLine("DDDDeleted");
                }
                btn_exitSelected.Enabled = true;
                btn_exitfromAll.Enabled = true;
                FlashWindow.Flash(this);
                //InfoForm.InfoHelper.CustomMessageBox.Show("Left successfully " + GroupNameSelected + " !");
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
    }
}


