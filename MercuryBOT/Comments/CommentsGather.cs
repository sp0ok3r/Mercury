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
            lbl_totalComments.Visible = false;
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
                    foreach (var pair in AccountLogin.ClanDictionary)
                    {
                        combox_ProfileURLorGroupID.Items.Add(pair.Value);
                    }
                    break;
            }
            SelectedProfileORClan = combox_GatherProfileOrGroup.SelectedItem.ToString();
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", " ", RegexOptions.Compiled);
        }

        public void GatherComments()
        {

            lbl_totalCommentsInGrid.Invoke((MethodInvoker)delegate
            {
                lbl_totalCommentsInGrid.Text = "Total Row Count: 0";
            });
            lbl_totalComments.Invoke((MethodInvoker)delegate
            {
                lbl_totalComments.Text = "Total Count: 0";
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
                string ProfileORGroupComments = "https://steamcommunity.com/comment/" + SelectedProfileORClan + "/render/" + CheckProfileGroupInfo + "/-1/?count=" + txtBox_Comments2GetCount.Text;
                
                var parser = new HtmlParser();

                var json = Web.DownloadString(ProfileORGroupComments);
                var renderComments = RenderComments.FromJson(json);

                var document = parser.ParseDocument(renderComments.CommentsHtml);
                var eCommentList = document.QuerySelectorAll("div.commentthread_comment");


                lbl_totalComments.Invoke((MethodInvoker)delegate
                {
                    lbl_totalComments.Text = "Total Count: " + renderComments.TotalCount.ToString();
                });


                foreach (var eComment in eCommentList)
                {
                    var CommentID       = eComment.QuerySelector("div.commentthread_comment_text").GetAttribute("id").Replace("comment_content_", "");
                    var CommentContent  = RemoveSpecialCharacters(eComment.QuerySelector("div.commentthread_comment_text").TextContent.Trim());
                    var Author          = eComment.QuerySelector("a[class='hoverunderline commentthread_author_link']").GetAttribute("href").Replace("https://steamcommunity.com/profiles/", "").Replace("https://steamcommunity.com/id/", "");

                    var Time            = eComment.QuerySelector("span.commentthread_comment_timestamp").GetAttribute("title").Replace("https://steamcommunity.com/profiles/", ""); // title=convertido
                    int index           = Time.IndexOf("@"); if (index > 0) { Time = Time.Substring(0, index); } // remove hours, only stay date

                    string[] row        = { CommentID, CommentContent, Author, Time };

                    GridCommentsData.Invoke((MethodInvoker)delegate
                    {
                        GridCommentsData.Rows.Add(row.Distinct().ToArray());
                    });


                    string[] arrayComments = CommentContent.Split(' ');

                    //bool result = Author.Any(x => !char.IsLetter(x));


                    if (chck_containsWords.Checked && txtBox_filterWords.Text.Length != 0)
                    {
                        foreach (string item in arrayComments)
                        {
                            string[] filterSelectedWords = txtBox_filterWords.Text.Split(',');

                            if (chck_ignoreCase.Checked && filterSelectedWords.Contains(item, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("AnyCase - DELETED: " + CommentID + " ||  " + CommentContent + "\n");
                                // AccountLogin.DeleteSelectedComment(CommentID, ProfileOrClan);


                                Thread.Sleep(5);
                            }
                            else if (filterSelectedWords.Contains(item))
                            {
                                Console.WriteLine("DELETED: " + CommentID + "\n");

                                // AccountLogin.DeleteSelectedComment(CommentID, ProfileOrClan);
                                Thread.Sleep(5);
                            }
                        }
                    }
                }

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
                lbl_totalComments.Invoke((MethodInvoker)delegate
                {
                    lbl_totalComments.Visible = true;
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


                Console.WriteLine("ww: "+e);
            }
        }

        public string Url2ID(string profileURL)
        {

            var parser = new HtmlParser();
            var html = Web.DownloadString("https://steamcommunity.com/id/sp0okER/");

            var document = parser.ParseDocument(html);
            //    var steamID64Clean = document.DocumentElement.QuerySelector("div[class='commentthread_paging has_view_all_link']").GetAttribute("id").Replace("commentthread_Profile_", "").Replace("_pagecontrols", "");
            var steamID64Clean = document.DocumentElement.QuerySelector("div[class='commentthread_paging has_view_all_link']").GetAttribute("id").Replace("commentthread_Profile_", "").Replace("_pagecontrols", "");

            return steamID64Clean;

            // }
            // catch (Exception)
            // {
            //     return "0";
            // }
        }

        public string IF_PROFILE_PRIVATE(string ProfileURL)// meter api key da config
        {
            var html = Web.DownloadString("http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=&vanityurl=" + ProfileURL);
            var steamID64Clean = html.Replace('"', ' ').Replace("{ response :{ steamid :", "").Replace(" , success :1}}", "").Trim();

            return steamID64Clean;
            //add try maybe

        }

        private void Btn_doTask_Click(object sender, EventArgs e)
        {
            //aa();
            //GatherComments();
            CollectComments.RunWorkerAsync();
        }

        private void CommentsGather_Shown(object sender, EventArgs e)
        {

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
                                              ? AccountLogin.ClanDictionary.ElementAt(combox_ProfileURLorGroupID.SelectedIndex).Key.ToString()
                                                : AccountLogin.CurrentSteamID.ToString(); // 666iq
        }
    }
}