namespace MercuryBOT.InfoForm
{
    partial class Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Info));
            this.txtBox_info = new System.Windows.Forms.TextBox();
            this.btn_okinfo = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_title = new MetroFramework.Controls.MetroLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtBox_info
            // 
            this.txtBox_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.txtBox_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_info.Cursor = System.Windows.Forms.Cursors.Help;
            this.txtBox_info.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_info.ForeColor = System.Drawing.Color.White;
            this.txtBox_info.HideSelection = false;
            this.txtBox_info.Location = new System.Drawing.Point(23, 54);
            this.txtBox_info.Multiline = true;
            this.txtBox_info.Name = "txtBox_info";
            this.txtBox_info.ReadOnly = true;
            this.txtBox_info.Size = new System.Drawing.Size(275, 97);
            this.txtBox_info.TabIndex = 7;
            this.txtBox_info.TabStop = false;
            // 
            // btn_okinfo
            // 
            this.btn_okinfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_okinfo.ForeColor = System.Drawing.Color.White;
            this.btn_okinfo.Location = new System.Drawing.Point(231, 157);
            this.btn_okinfo.Name = "btn_okinfo";
            this.btn_okinfo.Size = new System.Drawing.Size(80, 23);
            this.btn_okinfo.TabIndex = 8;
            this.btn_okinfo.TabStop = false;
            this.btn_okinfo.Text = "k";
            this.btn_okinfo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_okinfo.UseCustomBackColor = true;
            this.btn_okinfo.UseCustomForeColor = true;
            this.btn_okinfo.UseSelectable = true;
            this.btn_okinfo.UseStyleColors = true;
            this.btn_okinfo.Click += new System.EventHandler(this.Btn_okinfo_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(23, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 23);
            this.panel1.TabIndex = 10;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_title.Location = new System.Drawing.Point(23, 23);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(41, 25);
            this.lbl_title.TabIndex = 12;
            this.lbl_title.Text = "Info";
            this.lbl_title.UseCustomBackColor = true;
            this.lbl_title.UseStyleColors = true;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(273, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(49, 23);
            this.panel2.TabIndex = 11;
            // 
            // Info
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(321, 184);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_okinfo);
            this.Controls.Add(this.txtBox_info);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Info";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Notification";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Info_Load);
            this.Shown += new System.EventHandler(this.Info_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtBox_info;
        private MetroFramework.Controls.MetroButton btn_okinfo;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroLabel lbl_title;
        private System.Windows.Forms.Panel panel2;
    }
}