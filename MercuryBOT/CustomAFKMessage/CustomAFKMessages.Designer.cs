namespace MercuryBOT.CustomMessages
{
    partial class AFKMessages
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AFKMessages));
            this.Btn_addMessage2json = new MetroFramework.Controls.MetroButton();
            this.txtBox_customMSG = new MetroFramework.Controls.MetroTextBox();
            this.lbl_msg2AllorAFK = new MetroFramework.Controls.MetroLabel();
            this.btn_deleteSelected = new MetroFramework.Controls.MetroButton();
            this.SavedMsgs_Grid = new MetroFramework.Controls.MetroGrid();
            this.HFriendID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SavedMsgs_ScrollBar = new MetroFramework.Controls.MetroScrollBar();
            this.lbl_selectedMessage = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.SavedMsgs_Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_addMessage2json
            // 
            this.Btn_addMessage2json.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Btn_addMessage2json.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_addMessage2json.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.Btn_addMessage2json.Location = new System.Drawing.Point(75, 433);
            this.Btn_addMessage2json.Name = "Btn_addMessage2json";
            this.Btn_addMessage2json.Size = new System.Drawing.Size(144, 34);
            this.Btn_addMessage2json.Style = MetroFramework.MetroColorStyle.Purple;
            this.Btn_addMessage2json.TabIndex = 43;
            this.Btn_addMessage2json.TabStop = false;
            this.Btn_addMessage2json.Text = "ADD AFK MESSAGE";
            this.Btn_addMessage2json.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Btn_addMessage2json.UseCustomBackColor = true;
            this.Btn_addMessage2json.UseCustomForeColor = true;
            this.Btn_addMessage2json.UseSelectable = true;
            this.Btn_addMessage2json.UseStyleColors = true;
            this.Btn_addMessage2json.Click += new System.EventHandler(this.Btn_addMessage2json_Click);
            // 
            // txtBox_customMSG
            // 
            // 
            // 
            // 
            this.txtBox_customMSG.CustomButton.Image = null;
            this.txtBox_customMSG.CustomButton.Location = new System.Drawing.Point(206, 1);
            this.txtBox_customMSG.CustomButton.Name = "";
            this.txtBox_customMSG.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_customMSG.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_customMSG.CustomButton.TabIndex = 1;
            this.txtBox_customMSG.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_customMSG.CustomButton.UseSelectable = true;
            this.txtBox_customMSG.CustomButton.Visible = false;
            this.txtBox_customMSG.ForeColor = System.Drawing.Color.White;
            this.txtBox_customMSG.Lines = new string[0];
            this.txtBox_customMSG.Location = new System.Drawing.Point(5, 97);
            this.txtBox_customMSG.MaxLength = 32767;
            this.txtBox_customMSG.Name = "txtBox_customMSG";
            this.txtBox_customMSG.PasswordChar = '\0';
            this.txtBox_customMSG.PromptText = "yeet";
            this.txtBox_customMSG.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_customMSG.SelectedText = "";
            this.txtBox_customMSG.SelectionLength = 0;
            this.txtBox_customMSG.SelectionStart = 0;
            this.txtBox_customMSG.ShortcutsEnabled = true;
            this.txtBox_customMSG.Size = new System.Drawing.Size(228, 23);
            this.txtBox_customMSG.TabIndex = 44;
            this.txtBox_customMSG.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBox_customMSG.UseCustomBackColor = true;
            this.txtBox_customMSG.UseCustomForeColor = true;
            this.txtBox_customMSG.UseSelectable = true;
            this.txtBox_customMSG.UseStyleColors = true;
            this.txtBox_customMSG.WaterMark = "yeet";
            this.txtBox_customMSG.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_customMSG.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lbl_msg2AllorAFK
            // 
            this.lbl_msg2AllorAFK.AutoSize = true;
            this.lbl_msg2AllorAFK.BackColor = System.Drawing.Color.Transparent;
            this.lbl_msg2AllorAFK.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_msg2AllorAFK.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_msg2AllorAFK.Location = new System.Drawing.Point(7, 73);
            this.lbl_msg2AllorAFK.Name = "lbl_msg2AllorAFK";
            this.lbl_msg2AllorAFK.Size = new System.Drawing.Size(82, 25);
            this.lbl_msg2AllorAFK.TabIndex = 45;
            this.lbl_msg2AllorAFK.Text = "Message:";
            this.lbl_msg2AllorAFK.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_msg2AllorAFK.UseCustomBackColor = true;
            this.lbl_msg2AllorAFK.UseCustomForeColor = true;
            // 
            // btn_deleteSelected
            // 
            this.btn_deleteSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btn_deleteSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_deleteSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_deleteSelected.Location = new System.Drawing.Point(23, 433);
            this.btn_deleteSelected.Name = "btn_deleteSelected";
            this.btn_deleteSelected.Size = new System.Drawing.Size(46, 34);
            this.btn_deleteSelected.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_deleteSelected.TabIndex = 46;
            this.btn_deleteSelected.Text = "X";
            this.btn_deleteSelected.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_deleteSelected.UseCustomBackColor = true;
            this.btn_deleteSelected.UseCustomForeColor = true;
            this.btn_deleteSelected.UseSelectable = true;
            this.btn_deleteSelected.UseStyleColors = true;
            this.btn_deleteSelected.Click += new System.EventHandler(this.Btn_deleteSelected_Click);
            // 
            // SavedMsgs_Grid
            // 
            this.SavedMsgs_Grid.AllowUserToAddRows = false;
            this.SavedMsgs_Grid.AllowUserToDeleteRows = false;
            this.SavedMsgs_Grid.AllowUserToResizeRows = false;
            this.SavedMsgs_Grid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.SavedMsgs_Grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SavedMsgs_Grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.SavedMsgs_Grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SavedMsgs_Grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SavedMsgs_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SavedMsgs_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HFriendID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SavedMsgs_Grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.SavedMsgs_Grid.EnableHeadersVisualStyles = false;
            this.SavedMsgs_Grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SavedMsgs_Grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.SavedMsgs_Grid.Location = new System.Drawing.Point(-44, 126);
            this.SavedMsgs_Grid.Name = "SavedMsgs_Grid";
            this.SavedMsgs_Grid.ReadOnly = true;
            this.SavedMsgs_Grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SavedMsgs_Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.SavedMsgs_Grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SavedMsgs_Grid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SavedMsgs_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SavedMsgs_Grid.Size = new System.Drawing.Size(283, 280);
            this.SavedMsgs_Grid.TabIndex = 49;
            this.SavedMsgs_Grid.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SavedMsgs_Grid.UseCustomForeColor = true;
            this.SavedMsgs_Grid.UseStyleColors = true;
            this.SavedMsgs_Grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SavedMsgs_Grid_CellClick);
            // 
            // HFriendID
            // 
            this.HFriendID.HeaderText = "Saved Custom Messages";
            this.HFriendID.Name = "HFriendID";
            this.HFriendID.ReadOnly = true;
            this.HFriendID.Width = 250;
            // 
            // SavedMsgs_ScrollBar
            // 
            this.SavedMsgs_ScrollBar.LargeChange = 10;
            this.SavedMsgs_ScrollBar.Location = new System.Drawing.Point(222, 143);
            this.SavedMsgs_ScrollBar.Maximum = 100;
            this.SavedMsgs_ScrollBar.Minimum = 0;
            this.SavedMsgs_ScrollBar.MouseWheelBarPartitions = 10;
            this.SavedMsgs_ScrollBar.Name = "SavedMsgs_ScrollBar";
            this.SavedMsgs_ScrollBar.Orientation = MetroFramework.Controls.MetroScrollOrientation.Vertical;
            this.SavedMsgs_ScrollBar.ScrollbarSize = 15;
            this.SavedMsgs_ScrollBar.Size = new System.Drawing.Size(15, 263);
            this.SavedMsgs_ScrollBar.Style = MetroFramework.MetroColorStyle.Purple;
            this.SavedMsgs_ScrollBar.TabIndex = 50;
            this.SavedMsgs_ScrollBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SavedMsgs_ScrollBar.UseBarColor = true;
            this.SavedMsgs_ScrollBar.UseSelectable = true;
            this.SavedMsgs_ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SavedMsgs_ScrollBar_Scroll);
            // 
            // lbl_selectedMessage
            // 
            this.lbl_selectedMessage.AutoSize = true;
            this.lbl_selectedMessage.BackColor = System.Drawing.Color.Transparent;
            this.lbl_selectedMessage.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_selectedMessage.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_selectedMessage.Location = new System.Drawing.Point(5, 403);
            this.lbl_selectedMessage.Name = "lbl_selectedMessage";
            this.lbl_selectedMessage.Size = new System.Drawing.Size(75, 19);
            this.lbl_selectedMessage.TabIndex = 51;
            this.lbl_selectedMessage.Text = "Selected: ...";
            this.lbl_selectedMessage.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_selectedMessage.UseCustomBackColor = true;
            this.lbl_selectedMessage.UseStyleColors = true;
            // 
            // AFKMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 480);
            this.Controls.Add(this.lbl_selectedMessage);
            this.Controls.Add(this.SavedMsgs_ScrollBar);
            this.Controls.Add(this.SavedMsgs_Grid);
            this.Controls.Add(this.btn_deleteSelected);
            this.Controls.Add(this.txtBox_customMSG);
            this.Controls.Add(this.Btn_addMessage2json);
            this.Controls.Add(this.lbl_msg2AllorAFK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AFKMessages";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "AFK Messages";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.AFKMessages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SavedMsgs_Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton Btn_addMessage2json;
        private MetroFramework.Controls.MetroTextBox txtBox_customMSG;
        private MetroFramework.Controls.MetroLabel lbl_msg2AllorAFK;
        private MetroFramework.Controls.MetroButton btn_deleteSelected;
        private MetroFramework.Controls.MetroGrid SavedMsgs_Grid;
        private MetroFramework.Controls.MetroScrollBar SavedMsgs_ScrollBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn HFriendID;
        private MetroFramework.Controls.MetroLabel lbl_selectedMessage;
    }
}