namespace MercuryBOT.SteamProfileBackground
{
    partial class ProfileBackground
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileBackground));
            this.BTN_GETBackground = new MetroFramework.Controls.MetroButton();
            this.txtBox_steamprofile = new MetroFramework.Controls.MetroTextBox();
            this.picBox_steamBackground = new System.Windows.Forms.PictureBox();
            this.lbl_clickonimginfo = new MetroFramework.Controls.MetroLabel();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_steamBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN_GETBackground
            // 
            this.BTN_GETBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BTN_GETBackground.ForeColor = System.Drawing.Color.White;
            this.BTN_GETBackground.Location = new System.Drawing.Point(256, 268);
            this.BTN_GETBackground.Name = "BTN_GETBackground";
            this.BTN_GETBackground.Size = new System.Drawing.Size(75, 23);
            this.BTN_GETBackground.Style = MetroFramework.MetroColorStyle.Purple;
            this.BTN_GETBackground.TabIndex = 0;
            this.BTN_GETBackground.Text = "GET";
            this.BTN_GETBackground.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.BTN_GETBackground.UseCustomBackColor = true;
            this.BTN_GETBackground.UseCustomForeColor = true;
            this.BTN_GETBackground.UseSelectable = true;
            this.BTN_GETBackground.UseStyleColors = true;
            this.BTN_GETBackground.Click += new System.EventHandler(this.BTN_GETBackground_Click);
            // 
            // txtBox_steamprofile
            // 
            // 
            // 
            // 
            this.txtBox_steamprofile.CustomButton.Image = null;
            this.txtBox_steamprofile.CustomButton.Location = new System.Drawing.Point(223, 1);
            this.txtBox_steamprofile.CustomButton.Name = "";
            this.txtBox_steamprofile.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_steamprofile.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_steamprofile.CustomButton.TabIndex = 1;
            this.txtBox_steamprofile.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_steamprofile.CustomButton.UseSelectable = true;
            this.txtBox_steamprofile.CustomButton.Visible = false;
            this.txtBox_steamprofile.ForeColor = System.Drawing.Color.White;
            this.txtBox_steamprofile.Lines = new string[0];
            this.txtBox_steamprofile.Location = new System.Drawing.Point(10, 268);
            this.txtBox_steamprofile.MaxLength = 32767;
            this.txtBox_steamprofile.Name = "txtBox_steamprofile";
            this.txtBox_steamprofile.PasswordChar = '\0';
            this.txtBox_steamprofile.PromptText = "https://steamcommunity.com/id/?/ or profile!";
            this.txtBox_steamprofile.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_steamprofile.SelectedText = "";
            this.txtBox_steamprofile.SelectionLength = 0;
            this.txtBox_steamprofile.SelectionStart = 0;
            this.txtBox_steamprofile.ShortcutsEnabled = true;
            this.txtBox_steamprofile.Size = new System.Drawing.Size(245, 23);
            this.txtBox_steamprofile.TabIndex = 1;
            this.txtBox_steamprofile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBox_steamprofile.UseCustomBackColor = true;
            this.txtBox_steamprofile.UseCustomForeColor = true;
            this.txtBox_steamprofile.UseSelectable = true;
            this.txtBox_steamprofile.UseStyleColors = true;
            this.txtBox_steamprofile.WaterMark = "https://steamcommunity.com/id/?/ or profile!";
            this.txtBox_steamprofile.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_steamprofile.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // picBox_steamBackground
            // 
            this.picBox_steamBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_steamBackground.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_steamBackground.InitialImage = null;
            this.picBox_steamBackground.Location = new System.Drawing.Point(10, 75);
            this.picBox_steamBackground.Name = "picBox_steamBackground";
            this.picBox_steamBackground.Size = new System.Drawing.Size(321, 187);
            this.picBox_steamBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_steamBackground.TabIndex = 2;
            this.picBox_steamBackground.TabStop = false;
            this.metroToolTip1.SetToolTip(this.picBox_steamBackground, "Click and go to market page!");
            this.picBox_steamBackground.Click += new System.EventHandler(this.PicBox_steamBackground_Click);
            // 
            // lbl_clickonimginfo
            // 
            this.lbl_clickonimginfo.AutoSize = true;
            this.lbl_clickonimginfo.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lbl_clickonimginfo.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_clickonimginfo.Location = new System.Drawing.Point(236, 60);
            this.lbl_clickonimginfo.Name = "lbl_clickonimginfo";
            this.lbl_clickonimginfo.Size = new System.Drawing.Size(100, 15);
            this.lbl_clickonimginfo.TabIndex = 46;
            this.lbl_clickonimginfo.Text = "Click on the image!";
            this.lbl_clickonimginfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_clickonimginfo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_clickonimginfo.UseCustomBackColor = true;
            this.lbl_clickonimginfo.UseStyleColors = true;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Default;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // ProfileBackground
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 301);
            this.Controls.Add(this.picBox_steamBackground);
            this.Controls.Add(this.txtBox_steamprofile);
            this.Controls.Add(this.BTN_GETBackground);
            this.Controls.Add(this.lbl_clickonimginfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProfileBackground";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Gather Profile Background";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProfileBackground_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_steamBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton BTN_GETBackground;
        private MetroFramework.Controls.MetroTextBox txtBox_steamprofile;
        private System.Windows.Forms.PictureBox picBox_steamBackground;
        internal MetroFramework.Controls.MetroLabel lbl_clickonimginfo;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}