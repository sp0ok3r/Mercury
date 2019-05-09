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
using System.Windows.Forms;
using Win32Interop.Methods;

namespace MercuryBOT.SteamServers
{
    public partial class CSRegions : MetroFramework.Forms.MetroForm
    {
        public CSRegions()
        {
            InitializeComponent(); this.Activate();
            this.components.SetStyle(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void CSRegions_Load(object sender, EventArgs e)
        {
            TrolhaCSRegions.Tick += TrolhaCSRegions_Tick;
            
        }

        void TrolhaCSRegions_Tick(object sender, EventArgs e)
        {
            GatherCSRegionsData();
        }

        private void GatherCSRegionsData()
        {
            try
            {
                using (var webClient = new System.Net.WebClient())
                {

                    var json = webClient.DownloadString("https://crowbar.steamstat.us/Barney");

                    var welcome = Welcome.FromJson(json);

                    metroLink_us_northcentral.Text = welcome.Services.CsgoUsNorthcentral.Status.ToString();
                    metroLink_us_northeast.Text    = welcome.Services.CsgoUsNortheast.Status.ToString();
                    metroLink_us_northwest.Text    = welcome.Services.CsgoUsNorthwest.Status.ToString();
                    metroLink_us_southeast.Text    = welcome.Services.CsgoUsSoutheast.Status.ToString();
                    metroLink_us_southwest.Text    = welcome.Services.CsgoUsSouthwest.Status.ToString();

                    metroLink_brazil.Text          = welcome.Services.CsgoBrazil.Status.ToString();
                    metroLink_peru.Text            = welcome.Services.CsgoPeru.Status.ToString();
                    metroLink_chile.Text           = welcome.Services.CsgoChile.Status.ToString();

                    metroLink_emirates.Text        = welcome.Services.CsgoEmirates.Status.ToString();
                    metroLink_spain.Text           = welcome.Services.CsgoSpain.Status.ToString();
                    metroLink_poland.Text          = welcome.Services.CsgoPoland.Status.ToString();
                    metroLink_eu_east.Text         = welcome.Services.CsgoEuEast.Status.ToString();
                    metroLink_eu_north.Text        = welcome.Services.CsgoEuNorth.Status.ToString();
                    metroLink_eu_west.Text         = welcome.Services.CsgoEuWest.Status.ToString();
                    
                    metroLink_south_africa.Text    = welcome.Services.CsgoSouthAfrica.Status.ToString();

                    metroLink_india_east.Text      = welcome.Services.CsgoIndiaEast.Status.ToString();
                    metroLink_india.Text           = welcome.Services.CsgoIndia.Status.ToString();
                    
                    metroLink_china_guangzhou.Text = welcome.Services.CsgoChinaGuangzhou.Status.ToString();
                    metroLink_china_shanghai.Text  = welcome.Services.CsgoChinaShanghai.Status.ToString();
                    metroLink_china_tianjin.Text   = welcome.Services.CsgoChinaTianjin.Status.ToString();
                    metroLink_singapore.Text       = welcome.Services.CsgoSingapore.Status.ToString();
                    metroLink_hong_kong.Text       = welcome.Services.CsgoHongKong.Status.ToString();
                    metroLink_japan.Text           = welcome.Services.CsgoJapan.Status.ToString();

                    metroLink_australia.Text = welcome.Services.CsgoAustralia.Status.ToString();
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E);
            }
        }

        private void MetroLink_steamstatUS_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamstat.us/");
        }

        private void MetroLabel_poland_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=1EPlFdNa1bQ");
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=kJQP7kiw5Fk");
      
        }

        #region PictureBox Move
        //https://stackoverflow.com/users/3879008/giangpzo
        private bool draging = false;
        private Point pointClicked;
        private void PicBox_MAP_MouseMove(object sender, MouseEventArgs e)
        {
            if (draging)
            {
                Point pointMoveTo;
                pointMoveTo = this.PointToScreen(new Point(e.X, e.Y));
                pointMoveTo.Offset(-pointClicked.X, -pointClicked.Y);
                this.Location = pointMoveTo;
            }
        }

        private void PicBox_MAP_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draging = true;
                pointClicked = new Point(e.X, e.Y);
            }
            else
            {
                draging = false;
            }
        }

        private void PicBox_MAP_MouseUp(object sender, MouseEventArgs e)
        {
            draging = false;
        }

        #endregion

        private void metroLink_us_southwest_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_us_southwest.Text == "Major")
            {
                metroLink_us_southwest.LinkColor = Color.Red;
            }
            else if (metroLink_us_southwest.Text == "Minor")
            {
                metroLink_us_southwest.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_us_southwest.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_us_northcentral_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_us_northcentral.Text == "Major")
            {
                metroLink_us_northcentral.LinkColor = Color.Red;
            }
            else if (metroLink_us_northcentral.Text == "Minor")
            {
                metroLink_us_northcentral.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_us_northcentral.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_us_northwest_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_us_northwest.Text == "Major")
            {
                metroLink_us_northwest.LinkColor = Color.Red;
            }
            else if (metroLink_us_northwest.Text == "Minor")
            {
                metroLink_us_northwest.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_us_northwest.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_us_northeast_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_us_northeast.Text == "Major")
            {
                metroLink_us_northeast.LinkColor = Color.Red;
            }
            else if (metroLink_us_northeast.Text == "Minor")
            {
                metroLink_us_northeast.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_us_northeast.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_us_southeast_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_us_southeast.Text == "Major")
            {
                metroLink_us_southeast.LinkColor = Color.Red;
            }
            else if (metroLink_us_southeast.Text == "Minor")
            {
                metroLink_us_southeast.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_us_southeast.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_peru_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_peru.Text == "Major")
            {
                metroLink_peru.LinkColor = Color.Red;
            }
            else if (metroLink_peru.Text == "Minor")
            {
                metroLink_peru.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_peru.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_brazil_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_brazil.Text == "Major")
            {
                metroLink_brazil.LinkColor = Color.Red;
            }
            else if (metroLink_brazil.Text == "Minor")
            {
                metroLink_brazil.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_brazil.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_chile_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_chile.Text == "Major")
            {
                metroLink_chile.LinkColor = Color.Red;
            }
            else if (metroLink_chile.Text == "Minor")
            {
                metroLink_chile.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_chile.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_spain_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_spain.Text == "Major")
            {
                metroLink_spain.LinkColor = Color.Red;
            }
            else if (metroLink_spain.Text == "Minor")
            {
                metroLink_spain.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_spain.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_eu_east_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_eu_east.Text == "Major")
            {
                metroLink_eu_east.LinkColor = Color.Red;
            }
            else if (metroLink_eu_east.Text == "Minor")
            {
                metroLink_eu_east.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_eu_east.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_emirates_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_emirates.Text == "Major")
            {
                metroLink_emirates.LinkColor = Color.Red;
            }
            else if (metroLink_emirates.Text == "Minor")
            {
                metroLink_emirates.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_emirates.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_south_africa_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_south_africa.Text == "Major")
            {
                metroLink_south_africa.LinkColor = Color.Red;
            }
            else if (metroLink_south_africa.Text == "Minor")
            {
                metroLink_south_africa.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_south_africa.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_eu_north_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_eu_north.Text == "Major")
            {
                metroLink_eu_north.LinkColor = Color.Red;
            }
            else if (metroLink_eu_north.Text == "Minor")
            {
                metroLink_eu_north.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_eu_north.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_poland_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_poland.Text == "Major")
            {
                metroLink_poland.LinkColor = Color.Red;
            }
            else if (metroLink_poland.Text == "Minor")
            {
                metroLink_poland.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_poland.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_eu_west_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_eu_west.Text == "Major")
            {
                metroLink_eu_west.LinkColor = Color.Red;
            }
            else if (metroLink_eu_west.Text == "Minor")
            {
                metroLink_eu_west.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_eu_west.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_india_east_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_india_east.Text == "Major")
            {
                metroLink_india_east.LinkColor = Color.Red;
            }
            else if (metroLink_india_east.Text == "Minor")
            {
                metroLink_india_east.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_india_east.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_india_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_india.Text == "Major")
            {
                metroLink_india.LinkColor = Color.Red;
            }
            else if (metroLink_india.Text == "Minor")
            {
                metroLink_india.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_india.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_singapore_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_singapore.Text == "Major")
            {
                metroLink_singapore.LinkColor = Color.Red;
            }
            else if (metroLink_singapore.Text == "Minor")
            {
                metroLink_singapore.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_singapore.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_australia_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_australia.Text == "Major")
            {
                metroLink_australia.LinkColor = Color.Red;
            }
            else if (metroLink_australia.Text == "Minor")
            {
                metroLink_australia.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_australia.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_china_guangzhou_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_china_guangzhou.Text == "Major")
            {
                metroLink_china_guangzhou.LinkColor = Color.Red;
            }
            else if (metroLink_china_guangzhou.Text == "Minor")
            {
                metroLink_china_guangzhou.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_china_guangzhou.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_china_shanghai_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_china_shanghai.Text == "Major")
            {
                metroLink_china_shanghai.LinkColor = Color.Red;
            }
            else if (metroLink_china_shanghai.Text == "Minor")
            {
                metroLink_china_shanghai.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_china_shanghai.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_china_tianjin_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_china_tianjin.Text == "Major")
            {
                metroLink_china_tianjin.LinkColor = Color.Red;
            }
            else if (metroLink_china_tianjin.Text == "Minor")
            {
                metroLink_china_tianjin.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_china_tianjin.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_hong_kong_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_hong_kong.Text == "Major")
            {
                metroLink_hong_kong.LinkColor = Color.Red;
            }
            else if (metroLink_hong_kong.Text == "Minor")
            {
                metroLink_hong_kong.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_hong_kong.LinkColor = Color.DarkGreen;
            }
        }

        private void metroLink_japan_TextChanged(object sender, EventArgs e)
        {
            if (metroLink_japan.Text == "Major")
            {
                metroLink_japan.LinkColor = Color.Red;
            }
            else if (metroLink_japan.Text == "Minor")
            {
                metroLink_japan.LinkColor = Color.DarkOrange;
            }
            else
            {
                metroLink_japan.LinkColor = Color.DarkGreen;
            }
        }


    }
}
