namespace MercuryBOT
{
    partial class SteamGuard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamGuard));
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btn_submit = new MetroFramework.Controls.MetroButton();
            this.txtBox_Code = new MetroFramework.Controls.MetroTextBox();
            this.BTN_CANCEL = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.ForeColor = System.Drawing.Color.White;
            this.metroLabel1.Location = new System.Drawing.Point(23, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(208, 26);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Purple;
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Enter Steam-Guard Code:\r\n";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel1.UseCustomForeColor = true;
            this.metroLabel1.UseStyleColors = true;
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(311, 101);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(89, 27);
            this.btn_submit.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_submit.TabIndex = 3;
            this.btn_submit.Text = "SUBMIT";
            this.btn_submit.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_submit.UseSelectable = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // txtBox_Code
            // 
            // 
            // 
            // 
            this.txtBox_Code.CustomButton.Image = null;
            this.txtBox_Code.CustomButton.Location = new System.Drawing.Point(152, 1);
            this.txtBox_Code.CustomButton.Name = "";
            this.txtBox_Code.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_Code.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_Code.CustomButton.TabIndex = 1;
            this.txtBox_Code.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_Code.CustomButton.UseSelectable = true;
            this.txtBox_Code.CustomButton.Visible = false;
            this.txtBox_Code.ForeColor = System.Drawing.Color.White;
            this.txtBox_Code.Lines = new string[0];
            this.txtBox_Code.Location = new System.Drawing.Point(226, 63);
            this.txtBox_Code.MaxLength = 10;
            this.txtBox_Code.Name = "txtBox_Code";
            this.txtBox_Code.PasswordChar = '\0';
            this.txtBox_Code.PromptText = "GABEN";
            this.txtBox_Code.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_Code.SelectedText = "";
            this.txtBox_Code.SelectionLength = 0;
            this.txtBox_Code.SelectionStart = 0;
            this.txtBox_Code.ShortcutsEnabled = true;
            this.txtBox_Code.Size = new System.Drawing.Size(174, 23);
            this.txtBox_Code.Style = MetroFramework.MetroColorStyle.Purple;
            this.txtBox_Code.TabIndex = 0;
            this.txtBox_Code.UseCustomBackColor = true;
            this.txtBox_Code.UseCustomForeColor = true;
            this.txtBox_Code.UseSelectable = true;
            this.txtBox_Code.UseStyleColors = true;
            this.txtBox_Code.WaterMark = "GABEN";
            this.txtBox_Code.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_Code.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // BTN_CANCEL
            // 
            this.BTN_CANCEL.Location = new System.Drawing.Point(23, 101);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(89, 27);
            this.BTN_CANCEL.Style = MetroFramework.MetroColorStyle.Purple;
            this.BTN_CANCEL.TabIndex = 5;
            this.BTN_CANCEL.Text = "CANCEL";
            this.BTN_CANCEL.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.BTN_CANCEL.UseSelectable = true;
            this.BTN_CANCEL.Click += new System.EventHandler(this.BTN_CANCEL_Click);
            // 
            // SteamGuard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 140);
            this.Controls.Add(this.BTN_CANCEL);
            this.Controls.Add(this.txtBox_Code);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.metroLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SteamGuard";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Mercury - Steam Guard";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.SteamGuard_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton btn_submit;
        private MetroFramework.Controls.MetroTextBox txtBox_Code;
        private MetroFramework.Controls.MetroButton BTN_CANCEL;
    }
}