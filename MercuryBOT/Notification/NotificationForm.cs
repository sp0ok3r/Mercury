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
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32Interop.Methods;

namespace MercuryBOT.Notification
{
    public partial class NotificationForm : MetroFramework.Forms.MetroForm
    {
        //protected override void WndProc(ref Message m)
        //{
        //    // Ignore all messages that try to set the focus.
        //    if (m.Msg != 0x7)
        //    {
        //        base.WndProc(ref m);
        //    }
        //}

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)0x84)
                m.Result = (IntPtr)(-1);
            else
                base.WndProc(ref m);
        }


        private int posStart;
        private bool openNotify = false;

        //Thanks to Kirtan Patel
        //Constants
        // Animates the window from left to right. This flag can be used with roll or slide animation.
        public const int AW_HOR_POSITIVE = 0X1;

        // Animates the window from right to left. This flag can be used with roll or slide animation.
        public const int AW_HOR_NEGATIVE = 0X2;

        // Animates the window from top to bottom. This flag can be used with roll or slide animation.
        public const int AW_VER_POSITIVE = 0X4;

        // Animates the window from bottom to top. This flag can be used with roll or slide animation.
        public const int AW_VER_NEGATIVE = 0X8;

        // Makes the window appear to collapse inward if AW_HIDE is used or expand outward if the AW_HIDE is not used.
        public const int AW_CENTER = 0X10;
        // Hides the window. By default, the window is shown.
        public const int AW_HIDE = 0x10000;
        // Activates the window.
        public const int AW_ACTIVATE = 0X20000;
        // Uses slide animation. By default, roll animation is used.
        public const int AW_SLIDE = 0X40000;
        // Uses a fade effect. This flag can be used only if hwnd is a top-level window.
        public const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        public NotificationForm(string title, string desc)
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 0, 0));

            // this.lbl_title.Text = title;
            this.txtBox_desc.Text = desc;

            lbl_time.Text = DateTime.Now.ToString("HH:mm");
        }

        private enum TaskBarLocation { TOP, BOTTOM, LEFT, RIGHT }

        private TaskBarLocation GetTaskBarLocation()
        {
            TaskBarLocation taskBarLocation = TaskBarLocation.BOTTOM;
            bool taskBarOnTopOrBottom = (Screen.PrimaryScreen.WorkingArea.Width == Screen.PrimaryScreen.Bounds.Width);
            if (taskBarOnTopOrBottom)
            {
                if (Screen.PrimaryScreen.WorkingArea.Top > 0) taskBarLocation = TaskBarLocation.TOP;
            }
            else
            {
                if (Screen.PrimaryScreen.WorkingArea.Left > 0)
                {
                    taskBarLocation = TaskBarLocation.LEFT;
                }
                else
                {
                    taskBarLocation = TaskBarLocation.RIGHT;
                }
            }
            return taskBarLocation;
        }


        protected override void OnLoad(EventArgs e)
        {
            txtBox_desc.Select(0, -1);
            posStart = Screen.PrimaryScreen.WorkingArea.Bottom - (this.Height);
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Size.Width - 1, posStart);

            //Animate form
            Notify();
           

        }
        private void Notify()
        {
            if (openNotify == false)
            {
                //json read 
                AnimateWindow(this.Handle, 500, AW_SLIDE | AW_VER_NEGATIVE);
                openNotify = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            openNotify = false;
            Close();
        }
    }
}