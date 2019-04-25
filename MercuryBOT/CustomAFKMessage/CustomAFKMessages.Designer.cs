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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AFKMessages));
            this.Btn_addMessage2json = new MetroFramework.Controls.MetroButton();
            this.listview_customMsgs = new MetroFramework.Controls.MetroListView();
            this.HeaderMessages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtBox_customMSG = new MetroFramework.Controls.MetroTextBox();
            this.lbl_msg2AllorAFK = new MetroFramework.Controls.MetroLabel();
            this.btn_deleteSelected = new MetroFramework.Controls.MetroButton();
            this.lbl_selectedMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Btn_addMessage2json
            // 
            this.Btn_addMessage2json.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Btn_addMessage2json.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_addMessage2json.ForeColor = System.Drawing.Color.White;
            this.Btn_addMessage2json.Location = new System.Drawing.Point(81, 430);
            this.Btn_addMessage2json.Name = "Btn_addMessage2json";
            this.Btn_addMessage2json.Size = new System.Drawing.Size(144, 34);
            this.Btn_addMessage2json.Style = MetroFramework.MetroColorStyle.Purple;
            this.Btn_addMessage2json.TabIndex = 43;
            this.Btn_addMessage2json.Text = "ADD AFK MESSAGE";
            this.Btn_addMessage2json.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Btn_addMessage2json.UseCustomBackColor = true;
            this.Btn_addMessage2json.UseCustomForeColor = true;
            this.Btn_addMessage2json.UseSelectable = true;
            this.Btn_addMessage2json.UseStyleColors = true;
            this.Btn_addMessage2json.Click += new System.EventHandler(this.Btn_addMessage2json_Click);
            // 
            // listview_customMsgs
            // 
            this.listview_customMsgs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.listview_customMsgs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listview_customMsgs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderMessages});
            this.listview_customMsgs.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.listview_customMsgs.FullRowSelect = true;
            this.listview_customMsgs.Location = new System.Drawing.Point(11, 136);
            this.listview_customMsgs.Name = "listview_customMsgs";
            this.listview_customMsgs.OwnerDraw = true;
            this.listview_customMsgs.Size = new System.Drawing.Size(209, 288);
            this.listview_customMsgs.Style = MetroFramework.MetroColorStyle.Purple;
            this.listview_customMsgs.TabIndex = 42;
            this.listview_customMsgs.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.listview_customMsgs.UseCompatibleStateImageBehavior = false;
            this.listview_customMsgs.UseCustomBackColor = true;
            this.listview_customMsgs.UseCustomForeColor = true;
            this.listview_customMsgs.UseSelectable = true;
            this.listview_customMsgs.UseStyleColors = true;
            this.listview_customMsgs.View = System.Windows.Forms.View.Details;
            this.listview_customMsgs.SelectedIndexChanged += new System.EventHandler(this.Listview_customMsgs_SelectedIndexChanged);
            // 
            // HeaderMessages
            // 
            this.HeaderMessages.Text = "Saved Custom Messages";
            this.HeaderMessages.Width = 209;
            // 
            // txtBox_customMSG
            // 
            // 
            // 
            // 
            this.txtBox_customMSG.CustomButton.Image = null;
            this.txtBox_customMSG.CustomButton.Location = new System.Drawing.Point(187, 1);
            this.txtBox_customMSG.CustomButton.Name = "";
            this.txtBox_customMSG.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_customMSG.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_customMSG.CustomButton.TabIndex = 1;
            this.txtBox_customMSG.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_customMSG.CustomButton.UseSelectable = true;
            this.txtBox_customMSG.CustomButton.Visible = false;
            this.txtBox_customMSG.ForeColor = System.Drawing.Color.White;
            this.txtBox_customMSG.Lines = new string[0];
            this.txtBox_customMSG.Location = new System.Drawing.Point(11, 107);
            this.txtBox_customMSG.MaxLength = 32767;
            this.txtBox_customMSG.Name = "txtBox_customMSG";
            this.txtBox_customMSG.PasswordChar = '\0';
            this.txtBox_customMSG.PromptText = "yeet";
            this.txtBox_customMSG.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_customMSG.SelectedText = "";
            this.txtBox_customMSG.SelectionLength = 0;
            this.txtBox_customMSG.SelectionStart = 0;
            this.txtBox_customMSG.ShortcutsEnabled = true;
            this.txtBox_customMSG.Size = new System.Drawing.Size(209, 23);
            this.txtBox_customMSG.Style = MetroFramework.MetroColorStyle.Purple;
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
            this.lbl_msg2AllorAFK.Location = new System.Drawing.Point(7, 83);
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
            this.btn_deleteSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_deleteSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_deleteSelected.ForeColor = System.Drawing.Color.White;
            this.btn_deleteSelected.Location = new System.Drawing.Point(11, 430);
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
            // lbl_selectedMessage
            // 
            this.lbl_selectedMessage.AutoSize = true;
            this.lbl_selectedMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(65)))), ((int)(((byte)(153)))));
            this.lbl_selectedMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_selectedMessage.Location = new System.Drawing.Point(78, 148);
            this.lbl_selectedMessage.Name = "lbl_selectedMessage";
            this.lbl_selectedMessage.Size = new System.Drawing.Size(52, 13);
            this.lbl_selectedMessage.TabIndex = 48;
            this.lbl_selectedMessage.Text = "Selected:";
            // 
            // AFKMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 480);
            this.Controls.Add(this.lbl_selectedMessage);
            this.Controls.Add(this.btn_deleteSelected);
            this.Controls.Add(this.txtBox_customMSG);
            this.Controls.Add(this.Btn_addMessage2json);
            this.Controls.Add(this.listview_customMsgs);
            this.Controls.Add(this.lbl_msg2AllorAFK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AFKMessages";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "AFK Messages";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.AFKMessages_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton Btn_addMessage2json;
        private MetroFramework.Controls.MetroListView listview_customMsgs;
        private System.Windows.Forms.ColumnHeader HeaderMessages;
        private MetroFramework.Controls.MetroTextBox txtBox_customMSG;
        private MetroFramework.Controls.MetroLabel lbl_msg2AllorAFK;
        private MetroFramework.Controls.MetroButton btn_deleteSelected;
        private System.Windows.Forms.Label lbl_selectedMessage;
    }
}