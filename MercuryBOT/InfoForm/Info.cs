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
using System.Configuration;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace MercuryBOT.InfoForm
{
    public partial class Info :  MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// https://stackoverflow.com/questions/6932792/how-to-create-a-custom-messagebox
        /// </summary>
        /// <param name="title"></param>
        public Info(string title, string description)
        {
            InitializeComponent();
            this.lbl_title.Text = title;
            this.txtBox_info.Text = description;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void Info_Load(object sender, EventArgs e)
        {
            
        }
       
        private void Btn_okinfo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Info_Shown(object sender, EventArgs e)
        {
            Stream str = Properties.Resources.mercury_alert;
            SoundPlayer snd = new SoundPlayer(str);
            snd.Play();
        }
    }
}
