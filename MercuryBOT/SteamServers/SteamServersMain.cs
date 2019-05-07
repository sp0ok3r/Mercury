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
using SteamServers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace MercuryBOT.SteamServers
{
    public partial class SteamServersMain : MetroFramework.Forms.MetroForm
    {
        private Stopwatch sw = new Stopwatch();

        public SteamServersMain()
        {
            InitializeComponent(); this.Activate();
            this.components.SetStyle(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void SteamServersMain_Load(object sender, EventArgs e)
        {
            TrolhaStatus.Tick += TrolhaStatus_Tick;
        }

        void TrolhaStatus_Tick(object sender, EventArgs e)
        {
            GatherSteamData();
            SteamMaintenanceDay();
        }

        private void SteamMaintenanceDay()
        {

            DayOfWeek dow = DateTime.Now.DayOfWeek;
            switch (dow)
            {
                //case DayOfWeek.Sunday:
                //    break;
                //case DayOfWeek.Monday:
                //    break;
                case DayOfWeek.Tuesday:
                    Title_AlertSteamMaintenance.Visible = true;
                    break;
                //case DayOfWeek.Wednesday:
                //    break;
                //case DayOfWeek.Thursday:
                //    break;
                //case DayOfWeek.Friday:
                //    break;
                //case DayOfWeek.Saturday:
                //    break;
                default:
                    Title_AlertSteamMaintenance.Visible = false;
                    break;
            }
        }


        private void GatherSteamData()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            try
            {
                using (var webClient = new System.Net.WebClient())
                {

                    var json = webClient.DownloadString("https://crowbar.steamstat.us/Barney");

                    var welcome = Welcome.FromJson(json);

                    metroLink_OnlineOnSteam.Text = welcome.Services.Online.Title.ToString();

                    metroLink_StatusStore.Text = welcome.Services.Store.Status.ToString();
                    metroLink_StatusCommunity.Text = welcome.Services.Community.Status.ToString();
                    metroLink_cms.Text = welcome.Services.Cms.Status.ToString();
                    metroLink_cmsWS.Text = welcome.Services.CmsWs.Status.ToString();
                    metroLink_StatusWebapi.Text = welcome.Services.Webapi.Status.ToString();
                    metroLink_StatusSteamDB.Text = welcome.Services.Database.Status.ToString();

                    metroLink_CSLogin.Text = welcome.Services.CsgoSessions.Status.ToString();
                    metroLink_CSInventories.Text = welcome.Services.CsgoCommunity.Status.ToString();
                    metroLink_CSMMSL.Text = welcome.Services.CsgoMmScheduler.Status.ToString();

                    metroLink_tf2api.Text = welcome.Services.Tf2.Status.ToString();
                    metroLink_dota2api.Text = welcome.Services.Dota2.Status.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SteamServersForm Exeption: "+e);
            }
        }

        private void MetroLink_steamstatUS_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamstat.us/");
        }


        private void Btn_showCSRegions_Click(object sender, EventArgs e)
        {
            Form showCSRegions = new CSRegions();
            showCSRegions.FormClosed += HandleFormshowCSRegionsClosed;
            showCSRegions.Show();
            Btn_showCSRegions.Enabled = false;
        }

        private void HandleFormshowCSRegionsClosed(Object sender, FormClosedEventArgs e)
        {
            Btn_showCSRegions.Enabled = true;
        }


        #region MetroLinksColors
        private void metroLink_OnlineOnSteam_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_OnlineOnSteam.Text == "Unknown")
            {
                metroLink_OnlineOnSteam.ForeColor = Color.Red;
            }else
            {
                metroLink_OnlineOnSteam.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_StatusStore_TextChanged(object sender, EventArgs e)
        {

            if (metroLink_StatusStore.Text == "major")
            {
                metroLink_StatusStore.ForeColor = Color.Red;
            }
            else if(metroLink_StatusStore.Text == "minor")
            {
                metroLink_StatusStore.ForeColor = Color.DarkOrange;
            }else
            {
                metroLink_StatusStore.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_StatusCommunity_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_StatusCommunity.Text == "major")
            {
                metroLink_StatusCommunity.ForeColor = Color.Red;
            }
            else if (metroLink_StatusCommunity.Text == "minor")
            {
                metroLink_StatusCommunity.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_StatusCommunity.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_cms_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_cms.Text == "major")
            {
                metroLink_cms.ForeColor = Color.Red;
            }
            else if (metroLink_cms.Text == "minor")
            {
                metroLink_cms.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_cms.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_cmsWS_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_cmsWS.Text == "major")
            {
                metroLink_cmsWS.ForeColor = Color.Red;
            }
            else if (metroLink_cmsWS.Text == "minor")
            {
                metroLink_cmsWS.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_cmsWS.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_StatusWebapi_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_StatusWebapi.Text == "major")
            {
                metroLink_StatusWebapi.ForeColor = Color.Red;
            }
            else if (metroLink_StatusWebapi.Text == "minor")
            {
                metroLink_StatusWebapi.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_StatusWebapi.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_StatusSteamDB_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_StatusSteamDB.Text == "major")
            {
                metroLink_StatusSteamDB.ForeColor = Color.Red;
            }
            else if (metroLink_StatusSteamDB.Text == "minor")
            {
                metroLink_StatusSteamDB.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_StatusSteamDB.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_tf2api_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_tf2api.Text == "major")
            {
                metroLink_tf2api.ForeColor = Color.Red;
            }
            else if (metroLink_tf2api.Text == "minor")
            {
                metroLink_tf2api.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_tf2api.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_dota2api_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_dota2api.Text == "major")
            {
                metroLink_dota2api.ForeColor = Color.Red;
            }
            else if (metroLink_dota2api.Text == "minor")
            {
                metroLink_dota2api.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_dota2api.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_CSLogin_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_CSLogin.Text == "major")
            {
                metroLink_CSLogin.ForeColor = Color.Red;
            }
            else if (metroLink_CSLogin.Text == "minor")
            {
                metroLink_CSLogin.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_CSLogin.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_CSInventories_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_CSInventories.Text == "major")
            {
                metroLink_CSInventories.ForeColor = Color.Red;
            }
            else if (metroLink_CSInventories.Text == "minor")
            {
                metroLink_CSInventories.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_CSInventories.ForeColor = Color.DarkGreen;
            }
        }

        private void metroLink_CSMMSL_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_CSMMSL.Text == "major")
            {
                metroLink_CSMMSL.ForeColor = Color.Red;
            }
            else if (metroLink_CSMMSL.Text == "minor")
            {
                metroLink_CSMMSL.ForeColor = Color.DarkOrange;
            }
            else
            {
                metroLink_CSMMSL.ForeColor = Color.DarkGreen;
            }
        }

        #endregion
    }
}


