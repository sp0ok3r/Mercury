namespace MercuryBOT.FriendsList
{
    partial class Chat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChatTabs = new MetroFramework.Controls.MetroTabControl();
            this.chat_ScrollBar = new MetroFramework.Controls.MetroScrollBar();
            this.btn_chatSendMessage = new MetroFramework.Controls.MetroButton();
            this.chat_friendAvatar = new System.Windows.Forms.PictureBox();
            this.chat_UserMessage = new MetroFramework.Controls.MetroTextBox();
            this.chat_Messages = new MetroFramework.Controls.MetroTextBox();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.chat_friendName = new MetroFramework.Controls.MetroLabel();
            this.chat_friendState = new MetroFramework.Controls.MetroLabel();
            this.ChatTabs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chat_friendAvatar)).BeginInit();
            this.metroTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChatTabs
            // 
            this.ChatTabs.Controls.Add(this.metroTabPage1);
            this.ChatTabs.Location = new System.Drawing.Point(1, 24);
            this.ChatTabs.Name = "ChatTabs";
            this.ChatTabs.SelectedIndex = 0;
            this.ChatTabs.Size = new System.Drawing.Size(506, 425);
            this.ChatTabs.TabIndex = 7;
            this.ChatTabs.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ChatTabs.UseSelectable = true;
            // 
            // chat_ScrollBar
            // 
            this.chat_ScrollBar.LargeChange = 10;
            this.chat_ScrollBar.Location = new System.Drawing.Point(479, 84);
            this.chat_ScrollBar.Maximum = 100;
            this.chat_ScrollBar.Minimum = 0;
            this.chat_ScrollBar.MouseWheelBarPartitions = 10;
            this.chat_ScrollBar.Name = "chat_ScrollBar";
            this.chat_ScrollBar.Orientation = MetroFramework.Controls.MetroScrollOrientation.Vertical;
            this.chat_ScrollBar.ScrollbarSize = 10;
            this.chat_ScrollBar.Size = new System.Drawing.Size(10, 235);
            this.chat_ScrollBar.TabIndex = 3;
            this.chat_ScrollBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chat_ScrollBar.UseSelectable = true;
            this.chat_ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.chat_ScrollBar_Scroll);
            // 
            // btn_chatSendMessage
            // 
            this.btn_chatSendMessage.Location = new System.Drawing.Point(424, 327);
            this.btn_chatSendMessage.Name = "btn_chatSendMessage";
            this.btn_chatSendMessage.Size = new System.Drawing.Size(65, 51);
            this.btn_chatSendMessage.TabIndex = 1;
            this.btn_chatSendMessage.Text = "SEND";
            this.btn_chatSendMessage.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_chatSendMessage.UseSelectable = true;
            // 
            // chat_friendAvatar
            // 
            this.chat_friendAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.chat_friendAvatar.Image = global::MercuryBOT.Properties.Resources.github_logo;
            this.chat_friendAvatar.Location = new System.Drawing.Point(15, 14);
            this.chat_friendAvatar.Name = "chat_friendAvatar";
            this.chat_friendAvatar.Size = new System.Drawing.Size(71, 64);
            this.chat_friendAvatar.TabIndex = 4;
            this.chat_friendAvatar.TabStop = false;
            // 
            // chat_UserMessage
            // 
            // 
            // 
            // 
            this.chat_UserMessage.CustomButton.Image = null;
            this.chat_UserMessage.CustomButton.Location = new System.Drawing.Point(353, 2);
            this.chat_UserMessage.CustomButton.Name = "";
            this.chat_UserMessage.CustomButton.Size = new System.Drawing.Size(47, 47);
            this.chat_UserMessage.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.chat_UserMessage.CustomButton.TabIndex = 1;
            this.chat_UserMessage.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.chat_UserMessage.CustomButton.UseSelectable = true;
            this.chat_UserMessage.CustomButton.Visible = false;
            this.chat_UserMessage.Lines = new string[0];
            this.chat_UserMessage.Location = new System.Drawing.Point(15, 327);
            this.chat_UserMessage.MaxLength = 32767;
            this.chat_UserMessage.Multiline = true;
            this.chat_UserMessage.Name = "chat_UserMessage";
            this.chat_UserMessage.PasswordChar = '\0';
            this.chat_UserMessage.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.chat_UserMessage.SelectedText = "";
            this.chat_UserMessage.SelectionLength = 0;
            this.chat_UserMessage.SelectionStart = 0;
            this.chat_UserMessage.ShortcutsEnabled = true;
            this.chat_UserMessage.Size = new System.Drawing.Size(403, 52);
            this.chat_UserMessage.TabIndex = 2;
            this.chat_UserMessage.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chat_UserMessage.UseSelectable = true;
            this.chat_UserMessage.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.chat_UserMessage.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // chat_Messages
            // 
            // 
            // 
            // 
            this.chat_Messages.CustomButton.Image = null;
            this.chat_Messages.CustomButton.Location = new System.Drawing.Point(238, 1);
            this.chat_Messages.CustomButton.Name = "";
            this.chat_Messages.CustomButton.Size = new System.Drawing.Size(235, 235);
            this.chat_Messages.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.chat_Messages.CustomButton.TabIndex = 1;
            this.chat_Messages.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.chat_Messages.CustomButton.UseSelectable = true;
            this.chat_Messages.CustomButton.Visible = false;
            this.chat_Messages.Lines = new string[0];
            this.chat_Messages.Location = new System.Drawing.Point(15, 84);
            this.chat_Messages.MaxLength = 32767;
            this.chat_Messages.Multiline = true;
            this.chat_Messages.Name = "chat_Messages";
            this.chat_Messages.PasswordChar = '\0';
            this.chat_Messages.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.chat_Messages.SelectedText = "";
            this.chat_Messages.SelectionLength = 0;
            this.chat_Messages.SelectionStart = 0;
            this.chat_Messages.ShortcutsEnabled = true;
            this.chat_Messages.Size = new System.Drawing.Size(474, 237);
            this.chat_Messages.TabIndex = 0;
            this.chat_Messages.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chat_Messages.UseSelectable = true;
            this.chat_Messages.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.chat_Messages.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.chat_friendState);
            this.metroTabPage1.Controls.Add(this.chat_friendName);
            this.metroTabPage1.Controls.Add(this.chat_ScrollBar);
            this.metroTabPage1.Controls.Add(this.chat_UserMessage);
            this.metroTabPage1.Controls.Add(this.btn_chatSendMessage);
            this.metroTabPage1.Controls.Add(this.chat_Messages);
            this.metroTabPage1.Controls.Add(this.chat_friendAvatar);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(498, 383);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "metroTabPage1";
            this.metroTabPage1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // chat_friendName
            // 
            this.chat_friendName.AutoSize = true;
            this.chat_friendName.Location = new System.Drawing.Point(92, 14);
            this.chat_friendName.Name = "chat_friendName";
            this.chat_friendName.Size = new System.Drawing.Size(86, 19);
            this.chat_friendName.TabIndex = 7;
            this.chat_friendName.Text = "Friend Name";
            this.chat_friendName.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // chat_friendState
            // 
            this.chat_friendState.AutoSize = true;
            this.chat_friendState.Location = new System.Drawing.Point(92, 33);
            this.chat_friendState.Name = "chat_friendState";
            this.chat_friendState.Size = new System.Drawing.Size(47, 19);
            this.chat_friendState.TabIndex = 8;
            this.chat_friendState.Text = "Online";
            this.chat_friendState.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 454);
            this.Controls.Add(this.ChatTabs);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Chat";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Chat_Load);
            this.ChatTabs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chat_friendAvatar)).EndInit();
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl ChatTabs;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroLabel chat_friendState;
        private MetroFramework.Controls.MetroLabel chat_friendName;
        private MetroFramework.Controls.MetroScrollBar chat_ScrollBar;
        private MetroFramework.Controls.MetroTextBox chat_UserMessage;
        private MetroFramework.Controls.MetroButton btn_chatSendMessage;
        private MetroFramework.Controls.MetroTextBox chat_Messages;
        private System.Windows.Forms.PictureBox chat_friendAvatar;
    }
}