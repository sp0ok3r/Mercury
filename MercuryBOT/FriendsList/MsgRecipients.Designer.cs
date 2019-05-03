namespace MercuryBOT.FriendsList
{
    partial class MsgRecipients
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FriendsList_ScrollBar = new MetroFramework.Controls.MetroScrollBar();
            this.FriendsList_Grid = new MetroFramework.Controls.MetroGrid();
            this.HFriendID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSelectFriend = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_saveFriends = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.FriendsList_Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // FriendsList_ScrollBar
            // 
            this.FriendsList_ScrollBar.LargeChange = 10;
            this.FriendsList_ScrollBar.Location = new System.Drawing.Point(402, 91);
            this.FriendsList_ScrollBar.Maximum = 100;
            this.FriendsList_ScrollBar.Minimum = 0;
            this.FriendsList_ScrollBar.MouseWheelBarPartitions = 10;
            this.FriendsList_ScrollBar.Name = "FriendsList_ScrollBar";
            this.FriendsList_ScrollBar.Orientation = MetroFramework.Controls.MetroScrollOrientation.Vertical;
            this.FriendsList_ScrollBar.ScrollbarSize = 15;
            this.FriendsList_ScrollBar.Size = new System.Drawing.Size(15, 369);
            this.FriendsList_ScrollBar.Style = MetroFramework.MetroColorStyle.Purple;
            this.FriendsList_ScrollBar.TabIndex = 41;
            this.FriendsList_ScrollBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FriendsList_ScrollBar.UseBarColor = true;
            this.FriendsList_ScrollBar.UseSelectable = true;
            this.FriendsList_ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FriendsList_ScrollBar_Scroll);
            // 
            // FriendsList_Grid
            // 
            this.FriendsList_Grid.AllowUserToAddRows = false;
            this.FriendsList_Grid.AllowUserToDeleteRows = false;
            this.FriendsList_Grid.AllowUserToResizeRows = false;
            this.FriendsList_Grid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.FriendsList_Grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FriendsList_Grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.FriendsList_Grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FriendsList_Grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.FriendsList_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FriendsList_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HFriendID,
            this.HName,
            this.HSelectFriend});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FriendsList_Grid.DefaultCellStyle = dataGridViewCellStyle5;
            this.FriendsList_Grid.EnableHeadersVisualStyles = false;
            this.FriendsList_Grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FriendsList_Grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.FriendsList_Grid.Location = new System.Drawing.Point(-43, 73);
            this.FriendsList_Grid.Name = "FriendsList_Grid";
            this.FriendsList_Grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FriendsList_Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.FriendsList_Grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.FriendsList_Grid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.FriendsList_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FriendsList_Grid.Size = new System.Drawing.Size(461, 387);
            this.FriendsList_Grid.TabIndex = 42;
            this.FriendsList_Grid.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FriendsList_Grid.UseCustomForeColor = true;
            this.FriendsList_Grid.UseStyleColors = true;
            // 
            // HFriendID
            // 
            this.HFriendID.HeaderText = "FRIENDID";
            this.HFriendID.Name = "HFriendID";
            this.HFriendID.Width = 120;
            // 
            // HName
            // 
            this.HName.HeaderText = "NAME";
            this.HName.Name = "HName";
            this.HName.Width = 203;
            // 
            // HSelectFriend
            // 
            this.HSelectFriend.HeaderText = "Send Message";
            this.HSelectFriend.Name = "HSelectFriend";
            // 
            // btn_saveFriends
            // 
            this.btn_saveFriends.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btn_saveFriends.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_saveFriends.Location = new System.Drawing.Point(236, 466);
            this.btn_saveFriends.Name = "btn_saveFriends";
            this.btn_saveFriends.Size = new System.Drawing.Size(158, 35);
            this.btn_saveFriends.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_saveFriends.TabIndex = 43;
            this.btn_saveFriends.TabStop = false;
            this.btn_saveFriends.Text = "SAVE";
            this.btn_saveFriends.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_saveFriends.UseCustomBackColor = true;
            this.btn_saveFriends.UseCustomForeColor = true;
            this.btn_saveFriends.UseSelectable = true;
            this.btn_saveFriends.UseStyleColors = true;
            this.btn_saveFriends.Click += new System.EventHandler(this.btn_saveFriends_Click);
            // 
            // MsgRecipients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 508);
            this.Controls.Add(this.btn_saveFriends);
            this.Controls.Add(this.FriendsList_ScrollBar);
            this.Controls.Add(this.FriendsList_Grid);
            this.MaximizeBox = false;
            this.Name = "MsgRecipients";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Select Message Recipients";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.FriendsMsgReceiver_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FriendsList_Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroScrollBar FriendsList_ScrollBar;
        private MetroFramework.Controls.MetroGrid FriendsList_Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn HFriendID;
        private System.Windows.Forms.DataGridViewTextBoxColumn HName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HSelectFriend;
        private MetroFramework.Controls.MetroButton btn_saveFriends;
    }
}