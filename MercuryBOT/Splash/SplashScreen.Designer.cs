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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_info2 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_version
            // 
            this.lbl_version.Location = new System.Drawing.Point(23, 312);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(255, 19);
            this.lbl_version.Style = MetroFramework.MetroColorStyle.Purple;
            this.lbl_version.TabIndex = 3;
            this.lbl_version.Text = "version";
            this.lbl_version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_version.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_version.UseCustomBackColor = true;
            this.lbl_version.UseStyleColors = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(58, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 58);
            this.panel1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(302, 181);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lbl_info2
            // 
            this.lbl_info2.Location = new System.Drawing.Point(23, 236);
            this.lbl_info2.Name = "lbl_info2";
            this.lbl_info2.Size = new System.Drawing.Size(255, 19);
            this.lbl_info2.Style = MetroFramework.MetroColorStyle.Purple;
            this.lbl_info2.TabIndex = 8;
            this.lbl_info2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_info2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_info2.UseCustomBackColor = true;
            this.lbl_info2.UseStyleColors = true;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 351);
            this.Controls.Add(this.lbl_info2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SplashScreen";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Mercury - Splash";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.Shown += new System.EventHandler(this.SplashScreen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel lbl_version;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroLabel lbl_info2;
    }
}