namespace MercuryBOT.Splash
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.lbl_version = new MetroFramework.Controls.MetroLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_info2 = new MetroFramework.Controls.MetroLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_MercuryLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MercuryLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_version
            // 
            this.lbl_version.Location = new System.Drawing.Point(23, 312);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(255, 19);
            this.lbl_version.TabIndex = 3;
            this.lbl_version.Text = "version";
            this.lbl_version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_version.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_version.UseCustomBackColor = true;
            this.lbl_version.UseStyleColors = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(59, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 49);
            this.panel1.TabIndex = 6;
            // 
            // lbl_info2
            // 
            this.lbl_info2.Location = new System.Drawing.Point(23, 213);
            this.lbl_info2.Name = "lbl_info2";
            this.lbl_info2.Size = new System.Drawing.Size(255, 19);
            this.lbl_info2.TabIndex = 8;
            this.lbl_info2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_info2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_info2.UseCustomBackColor = true;
            this.lbl_info2.UseStyleColors = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MercuryBOT.Properties.Resources.SteamLogo;
            this.pictureBox1.Location = new System.Drawing.Point(239, 301);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox_MercuryLogo
            // 
            this.pictureBox_MercuryLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.pictureBox_MercuryLogo.Image = global::MercuryBOT.Properties.Resources.MercuryLogoWhite2;
            this.pictureBox_MercuryLogo.Location = new System.Drawing.Point(23, 88);
            this.pictureBox_MercuryLogo.Name = "pictureBox_MercuryLogo";
            this.pictureBox_MercuryLogo.Size = new System.Drawing.Size(255, 122);
            this.pictureBox_MercuryLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_MercuryLogo.TabIndex = 48;
            this.pictureBox_MercuryLogo.TabStop = false;
            // 
            // SplashScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(301, 351);
            this.Controls.Add(this.pictureBox_MercuryLogo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_info2);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Mercury - Splash";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TransparencyKey = System.Drawing.Color.Green;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.Shown += new System.EventHandler(this.SplashScreen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MercuryLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroLabel lbl_version;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroLabel lbl_info2;
        private System.Windows.Forms.PictureBox pictureBox_MercuryLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}