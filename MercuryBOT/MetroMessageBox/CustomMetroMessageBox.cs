using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Mercury.MetroMessageBox
{
    public static class CustomMetroMessageBox
    {
        public static DialogResult Show(IWin32Window owner, string message)
        {
            return Show(owner, message, "Notification", 211);
        }

        public static DialogResult Show(IWin32Window owner, string message, int height)
        {
            return Show(owner, message, "Notification", height);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title)
        {
            return Show(owner, message, title, MessageBoxButtons.OK, 211);
        }

        public static DialogResult Show(
            IWin32Window owner,
            string message,
            string title,
            int height)
        {
            return Show(owner, message, title, MessageBoxButtons.OK, height);
        }

        public static DialogResult Show(
            IWin32Window owner,
            string message,
            string title,
            MessageBoxButtons buttons)
        {
            return Show(owner, message, title, buttons, MessageBoxIcon.None, 211);
        }

        public static DialogResult Show(
            IWin32Window owner,
            string message,
            string title,
            MessageBoxButtons buttons,
            int height)
        {
            return Show(owner, message, title, buttons, MessageBoxIcon.None, height);
        }

        public static DialogResult Show(
            IWin32Window owner,
            string message,
            string title,
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            return Show(owner, message, title, buttons, icon, MessageBoxDefaultButton.Button1, 211);
        }

        public static DialogResult Show(
            IWin32Window owner,
            string message,
            string title,
            MessageBoxButtons buttons,
            MessageBoxIcon icon,
            int height)
        {
            return Show(owner, message, title, buttons, icon, MessageBoxDefaultButton.Button1, height);
        }

        public static DialogResult Show(
            IWin32Window owner,
            string message,
            string title,
            MessageBoxButtons buttons,
            MessageBoxIcon icon,
            MessageBoxDefaultButton defaultbutton)
        {
            return Show(owner, message, title, buttons, icon, defaultbutton, 211);
        }

        public static DialogResult Show(
            IWin32Window owner,
            string message,
            string title,
            MessageBoxButtons buttons,
            MessageBoxIcon icon,
            MessageBoxDefaultButton defaultbutton,
            int height)
        {

            var dialogResult = DialogResult.None;
            if (owner != null)
            {
                var form = !(owner is Form) ? ((ContainerControl)owner).ParentForm : (Form)owner;
                switch (icon)
                {
                    case MessageBoxIcon.Hand:
                        SystemSounds.Hand.Play();
                        break;
                    case MessageBoxIcon.Question:
                        SystemSounds.Beep.Play();
                        break;
                    case MessageBoxIcon.Exclamation:
                        SystemSounds.Exclamation.Play();
                        break;
                    default:
                        SystemSounds.Asterisk.Play();
                        break;
                }

                var messageBoxControl = new CustomMetroMessageBoxControl();
                messageBoxControl.ForeColor = Color.FromArgb(45, 51, 59);
                messageBoxControl.BackColor = Color.Orange;
                messageBoxControl.Properties.Buttons = buttons;
                messageBoxControl.Properties.DefaultButton = defaultbutton;
                messageBoxControl.Properties.Icon = icon;
                messageBoxControl.Properties.Message = message;
                messageBoxControl.Properties.Title = title;
                messageBoxControl.Padding = new Padding(0, 0, 0, 0);
                messageBoxControl.ControlBox = false;
                messageBoxControl.ShowInTaskbar = false;
                messageBoxControl.TopMost = true;
                messageBoxControl.Size = new Size(form.Size.Width, height);
                messageBoxControl.Location = new Point(form.Location.X,
                    form.Location.Y + (form.Height - messageBoxControl.Height) / 2);
                messageBoxControl.ArrangeApperance();
                Convert.ToInt32(Math.Floor(messageBoxControl.Size.Height * 0.28));
                var num = (int)messageBoxControl.ShowDialog();
                messageBoxControl.BringToFront();
                messageBoxControl.SetDefaultButton();
                dialogResult = messageBoxControl.Result;
                messageBoxControl.Dispose();
            }

            return dialogResult;
        }

        private static void ModalState(CustomMetroMessageBoxControl control)
        {
            do
            {
                ;
            } while (control.Visible);
        }
    }
}