using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Text.RegularExpressions;
using SteamWebAPI2.Interfaces;
using System.Diagnostics;
using MercuryBOT.Helpers;

namespace MercuryBOT.SteamProfileBackground
{
    public partial class ProfileBackground : MetroFramework.Forms.MetroForm
    {
        private string AppID;
        private readonly WebClient Web = new WebClient();

        public ProfileBackground()
        {
            InitializeComponent();
            this.Activate();
            this.FormBorderStyle = FormBorderStyle.None;
            this.components.SetStyle(this);
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void ProfileBackground_Load(object sender, EventArgs e)
        {
            picBox_steamBackground.Cursor = DefaultCursor;
            lbl_clickonimginfo.Visible = false;
        }

        private void BTN_GETBackground_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBox_steamprofile.Text)) {
                GETimage();
            }  
        }

        public void GETimage()
        {
            
            try
            {
                BTN_GETBackground.Enabled = false;

                var parser = new HtmlParser();
                StringBuilder doc = new StringBuilder(Web.DownloadString(txtBox_steamprofile.Text));

                var document = parser.ParseDocument(doc.ToString());
                var ee = document.QuerySelector("div.profile_background_image_content ").OuterHtml;

                StringBuilder myStringBuilder = new StringBuilder(document.QuerySelector("div.profile_background_image_content ").OuterHtml);
               
                var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase); //800iq
                foreach (Match m in linkParser.Matches(myStringBuilder.ToString()))
                {

                    picBox_steamBackground.ImageLocation = m.Value;
                    string result = Regex.Replace(m.Value, @"[^\d]", ""); //125iq
                    lbl_clickonimginfo.Visible = true;
                    picBox_steamBackground.Cursor = Cursors.Hand;

                    string re2 = ".*?(\\d+)";  // get int 
                    Regex getInteger = new Regex(re2, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    Match IntegerClear = getInteger.Match(m.Value);
                    if (IntegerClear.Success)
                    {
                        AppID = IntegerClear.Groups[1].ToString();
                    }
                    BTN_GETBackground.Enabled = true; 
                }
            }catch (Exception){
                picBox_steamBackground.Cursor = DefaultCursor;
                AppID = null;
                lbl_clickonimginfo.Visible = false;
                BTN_GETBackground.Enabled = true;
                picBox_steamBackground.Image = null;
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Steam profile cannot be private, sorry son.");
                return;
            }
        }

        private void PicBox_steamBackground_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AppID))
            {
                Process.Start("https://steamcommunity.com/market/search?appid=753&category_753_Game%5B%5D=tag_app_" + AppID + "&q=background");
                //Process.Start("steam://openurl/https://steamcommunity.com/market/search?appid=753&category_753_Game%5B%5D=tag_app_" + result + "&q=background"); 
            }
        }  
    }
}



