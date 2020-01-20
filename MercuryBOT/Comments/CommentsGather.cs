/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AngleSharp.Html.Parser;
using System.Threading;
using SteamComments;
using AngleSharp.Text;
using MercuryBOT.Helpers;
using Win32Interop.Methods;
using MetroFramework.Controls;
using System.Collections.Generic;

namespace MercuryBOT
{
    public partial class CommentsGather : MetroFramework.Forms.MetroForm
    {
        private readonly WebClient Web = new WebClient();
        private string CheckProfileGroupInfo;
        private string SelectedProfileORClan;
        public CommentsGather()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            foreach (var button in this.Controls.OfType<MetroButton>())
            {
                IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, button.Width, button.Height, 5, 5);
                button.Region = Region.FromHrgn(ptr);
                Gdi32.DeleteObject(ptr);
            }
        }
        private void CommentsGather_Load(object sender, EventArgs e)
        {
            ProgressSpinner_LoadComments.Visible = false;

            lbl_totalCommentsInGrid.Visible = false;
            // lbl_totalComments.Visible = false;
        }

        private void Combox_profileOrClan_SelectedIndexChanged(object sender, EventArgs e)
        {
            combox_ProfileURLorGroupID.Items.Clear();
            switch (combox_GatherProfileOrGroup.SelectedIndex)
            {
                case 0://Profile
                    combox_ProfileURLorGroupID.Items.Add(AccountLogin.CurrentSteamID.ToString());
                    combox_ProfileURLorGroupID.SelectedIndex = 0;
                    break;
                case 1://Clan
                    foreach (var Officers in AccountLogin.OfficerClanDictionary)
                    {
                        foreach (var Groups in AccountLogin.ClanDictionary)
                        {
                            if (Officers.Key == Groups.Key)
                            {
                                combox_ProfileURLorGroupID.Items.Add(Groups.Value);
                            }
                        }
                    }
                    break;
            }
            SelectedProfileORClan = combox_GatherProfileOrGroup.SelectedItem.ToString();
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", " ", RegexOptions.Compiled);
        }

        class cmts
        {
            public string CommentContent { get; set; }
            public string Author { get; set; }
            public string Time { get; set; }
        }

        Dictionary<string, cmts> myList = new Dictionary<string, cmts>();


        public void GatherComments()
        {
            lbl_totalCommentsInGrid.Invoke((MethodInvoker)delegate
            {
                lbl_totalCommentsInGrid.Text = "Total Row Count: 0";
            });
            ProgressSpinner_LoadComments.Invoke((MethodInvoker)delegate
            {
                ProgressSpinner_LoadComments.Visible = true;
            });

            if (string.IsNullOrEmpty(SelectedProfileORClan) || string.IsNullOrEmpty(txtBox_Comments2GetCount.Text))
            {
                Console.WriteLine("Please select the profile/group.");
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Please select the profile/group.");
                return;
            }
            if (SelectedProfileORClan == "My Profile")
            {
                SelectedProfileORClan = "Profile";
            }else if (SelectedProfileORClan == "My Group")
            {
                SelectedProfileORClan = "Clan";
            }

            GridCommentsData.Invoke((MethodInvoker)delegate
            {
                GridCommentsData.Rows.Clear();
            });
            btn_doTask.Invoke((MethodInvoker)delegate
            {
                btn_doTask.Enabled = false;
            });

            try
            {
                string ProfileORGroupComments = "https://steamcommunity.com/comment/"+SelectedProfileORClan+ "/render/" + CheckProfileGroupInfo + "/-1/?count=" + txtBox_Comments2GetCount.Text;

                var parser = new HtmlParser();

                var json = Web.DownloadString(ProfileORGroupComments);
                var renderComments = RenderComments.FromJson(json);

                var document = parser.ParseDocument(renderComments.CommentsHtml);
                var eCommentList = document.QuerySelectorAll("div.commentthread_comment");


                //lbl_totalComments.Invoke((MethodInvoker)delegate
                //{
                //    lbl_totalComments.Text = "Total Count: " + renderComments.TotalCount.ToString();
                //});


                foreach (var eComment in eCommentList)
                {
                    var CommentID = eComment.QuerySelector("div.commentthread_comment_text").GetAttribute("id").Replace("comment_content_", "");
                    var CommentContent = RemoveSpecialCharacters(eComment.QuerySelector("div.commentthread_comment_text").TextContent.Trim());
                    var Author = eComment.QuerySelector("a[class='hoverunderline commentthread_author_link']").GetAttribute("href").Replace("https://steamcommunity.com/profiles/", "").Replace("https://steamcommunity.com/id/", "");

                    var Time = eComment.QuerySelector("span.commentthread_comment_timestamp").GetAttribute("title").Replace("https://steamcommunity.com/profiles/", ""); // title=convertido
                    int index = Time.IndexOf("@"); if (index > 0) { Time = Time.Substring(0, index); } // remove hours, only stay date


                    string[] arrayComments = CommentContent.Split(' ');

                    //string[] row = { CommentID, CommentContent, Author, Time };
                    //GridCommentsData.Invoke((MethodInvoker)delegate
                    //{
                    //    GridCommentsData.Rows.Add(row.Distinct().ToArray());
                    //});


                    List<string> vals = new List<string>();//PERVENT DUPLICATE KEY
                    
                    if (chck_containsWords.Checked && txtBox_filterWords.Text.Length != 0)
                    {
                        foreach (string item in arrayComments)
                        {
                            string[] filterSelectedWords = txtBox_filterWords.Text.Split(',');


                            if (filterSelectedWords.Contains(item, StringComparison.OrdinalIgnoreCase) && !vals.Contains(CommentID))
                            {
                                Console.WriteLine("DELETED: " + CommentID + " ||  " + CommentContent + "\n");
                                myList.Add(CommentID, new cmts { CommentContent = CommentContent, Author = Author, Time = Time });
                                vals.Add(CommentID);

                                //AccountLogin.DeleteSelectedComment(CommentID, SelectedProfileORClan);

                                Thread.Sleep(100);
                            }
                        }
                    }
                }


                GridCommentsData.Invoke((MethodInvoker)delegate
                {
                    foreach (KeyValuePair<string, cmts> item in myList)
                    {
                        GridCommentsData.Rows.Add(item.Key, item.Value.CommentContent, item.Value.Author, item.Value.Time);
                    }
                });

                lbl_totalCommentsInGrid.Invoke((MethodInvoker)delegate
                {
                    lbl_totalCommentsInGrid.Text = "Total Row Count:" + GridCommentsData.Rows.Count.ToString();
                });
                CommentsList_ScrollBar.Invoke((MethodInvoker)delegate
                {
                    CommentsList_ScrollBar.Maximum = GridCommentsData.Rows.Count;
                });
                ProgressSpinner_LoadComments.Invoke((MethodInvoker)delegate
                {
                    ProgressSpinner_LoadComments.Visible = false;
                });
                lbl_totalCommentsInGrid.Invoke((MethodInvoker)delegate
                {
                    lbl_totalCommentsInGrid.Visible = true;
                });
                btn_doTask.Invoke((MethodInvoker)delegate
                {
                    btn_doTask.Enabled = true;
                });

            }
            catch (Exception e)
            {
                btn_doTask.Invoke((MethodInvoker)delegate
                {
                    btn_doTask.Enabled = true;
                });
                ProgressSpinner_LoadComments.Invoke((MethodInvoker)delegate
                {
                    ProgressSpinner_LoadComments.Visible = false;
                });


                Console.WriteLine("Error: " + e);
            }
        }

        private void Btn_doTask_Click(object sender, EventArgs e)
        {
            CollectComments.RunWorkerAsync();
        }

        private void CollectComments_DoWork(object sender, DoWorkEventArgs e)
        {
            GatherComments();
        }

        private void CommentsGather_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void CommentsList_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= GridCommentsData.Rows.Count)
            {
                return;
            }
            GridCommentsData.FirstDisplayedScrollingRowIndex = e.NewValue;
        }

        private void combox_ProfileURLorGroupID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckProfileGroupInfo = (!combox_ProfileURLorGroupID.SelectedItem.ToString().StartsWith("765611"))
                                     ? AccountLogin.ClanDictionary.ElementAt(combox_ProfileURLorGroupID.SelectedIndex).Key.ToString() // bug lista officers troca index
                                     : AccountLogin.CurrentSteamID.ToString(); // 666iq
        }
    }
}