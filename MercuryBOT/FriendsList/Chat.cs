using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MercuryBOT.FriendsList
{
    public partial class Chat : MetroFramework.Forms.MetroForm
    {


        public Chat()
        {
            InitializeComponent();


        }

        private void Chat_Load(object sender, EventArgs e)
        {
            //chat_friendAvatar.BackgroundImage
            //chat_Messages.Text
            //chat_UserMessage.Text
            //btn_chatSendMessage
            //chat_ScrollBar

        }
        private void chat_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue >= chat_Messages.Lines.Count())
            {
                return;
            }
            chat_Messages.TabIndex = e.NewValue;
        }
    }
}
