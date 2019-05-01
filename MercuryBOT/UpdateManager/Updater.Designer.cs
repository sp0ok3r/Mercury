namespace MercuryBOT
{
    partial class Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update));
            this.txtBox_changelog = new MetroFramework.Controls.MetroTextBox();
            this.btn_installupdate = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.lbl_infoversion = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // txtBox_changelog
            // 
            // 
            // 
            // 
            this.txtBox_changelog.CustomButton.Image = null;
            this.txtBox_changelog.CustomButton.Location = new System.Drawing.Point(102, 2);
            this.txtBox_changelog.CustomButton.Name = "";
            this.txtBox_changelog.CustomButton.Size = new System.Drawing.Size(291, 291);
            this.txtBox_changelog.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_changelog.CustomButton.TabIndex = 1;
            this.txtBox_changelog.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_changelog.CustomButton.UseSelectable = true;
            this.txtBox_changelog.CustomButton.Visible = false;
            this.txtBox_changelog.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtBox_changelog.Lines = new string[0];
            this.txtBox_changelog.Location = new System.Drawing.Point(8, 131);
            this.txtBox_changelog.MaxLength = 32767;
            this.txtBox_changelog.Multiline = true;
            this.txtBox_changelog.Name = "txtBox_changelog";
            this.txtBox_changelog.PasswordChar = '\0';
            this.txtBox_changelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBox_changelog.SelectedText = "";
            this.txtBox_changelog.SelectionLength = 0;
            this.txtBox_changelog.SelectionStart = 0;
            this.txtBox_changelog.ShortcutsEnabled = true;
            this.txtBox_changelog.Size = new System.Drawing.Size(396, 296);
            this.txtBox_changelog.TabIndex = 10;
            this.txtBox_changelog.UseCustomBackColor = true;
            this.txtBox_changelog.UseCustomForeColor = true;
            this.txtBox_changelog.UseSelectable = true;
            this.txtBox_changelog.UseStyleColors = true;
            this.txtBox_changelog.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_changelog.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btn_installupdate
            // 
            this.btn_installupdate.ForeColor = System.Drawing.Color.White;
            this.btn_installupdate.Location = new System.Drawing.Point(329, 433);
            this.btn_installupdate.Name = "btn_installupdate";
            this.btn_installupdate.Size = new System.Drawing.Size(75, 23);
            this.btn_installupdate.TabIndex = 0;
            this.btn_installupdate.Text = "Install";
            this.btn_installupdate.UseCustomBackColor = true;
            this.btn_installupdate.UseSelectable = true;
            this.btn_installupdate.UseStyleColors = true;
            this.btn_installupdate.Click += new System.EventHandler(this.Btn_installupdate_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.metroLabel1.Location = new System.Drawing.Point(8, 109);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(85, 19);
            this.metroLabel1.TabIndex = 11;
            this.metroLabel1.Text = "Changelog:";
            this.metroLabel1.UseCustomBackColor = true;
            this.metroLabel1.UseCustomForeColor = true;
            this.metroLabel1.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.metroLabel2.Location = new System.Drawing.Point(8, 73);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(309, 25);
            this.metroLabel2.TabIndex = 12;
            this.metroLabel2.Text = "A new version of Mercury is available!";
            this.metroLabel2.UseCustomBackColor = true;
            this.metroLabel2.UseCustomForeColor = true;
            this.metroLabel2.UseStyleColors = true;
            // 
            // lbl_infoversion
            // 
            this.lbl_infoversion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_infoversion.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lbl_infoversion.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_infoversion.Location = new System.Drawing.Point(323, 74);
            this.lbl_infoversion.Name = "lbl_infoversion";
            this.lbl_infoversion.Size = new System.Drawing.Size(81, 25);
            this.lbl_infoversion.TabIndex = 46;
            this.lbl_infoversion.Text = "v";
            this.lbl_infoversion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_infoversion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_infoversion.UseCustomBackColor = true;
            this.lbl_infoversion.UseStyleColors = true;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 469);
            this.Controls.Add(this.lbl_infoversion);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.btn_installupdate);
            this.Controls.Add(this.txtBox_changelog);
            this.Controls.Add(this.metroLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Update";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "New Update Available";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Update_FormClosed);
            this.Load += new System.EventHandler(this.Update_Load);
            this.Shown += new System.EventHandler(this.Update_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtBox_changelog;
        private MetroFramework.Controls.MetroButton btn_installupdate;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel lbl_infoversion;
    }
}