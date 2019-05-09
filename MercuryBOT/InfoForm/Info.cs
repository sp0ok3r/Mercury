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
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using Win32Interop.Methods;
using System.Drawing;

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
            this.components.SetStyle(this);
            this.lbl_title.Text = title;
            this.txtBox_info.Text = description;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));

            foreach (Button control in this.Controls.OfType<MetroButton>())
            {
                IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, control.Width, control.Height, 5, 5);
                control.Region = Region.FromHrgn(ptr);
                Gdi32.DeleteObject(ptr);
            }
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
