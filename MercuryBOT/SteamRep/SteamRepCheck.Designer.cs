namespace MercuryBOT.SteamRep
{
    partial class SteamRepCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamRepCheck));
            this.btn_checkUser = new MetroFramework.Controls.MetroButton();
            this.lbl_checkUsername = new MetroFramework.Controls.MetroLabel();
            this.txt_repSteamID = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.metroLink_steamrep = new MetroFramework.Controls.MetroLink();
            this.Title_AlertScammer = new MetroFramework.Controls.MetroTile();
            this.lbl_steamid32 = new MetroFramework.Controls.MetroLabel();
            this.lbl_steamID64 = new MetroFramework.Controls.MetroLabel();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.picBox_SteamAvatar = new System.Windows.Forms.PictureBox();
            this.ProgressSpinner_SteamRepDelay = new MetroFramework.Controls.MetroProgressSpinner();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.metroLink1 = new MetroFramework.Controls.MetroLink();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_SteamAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_checkUser
            // 
            this.btn_checkUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btn_checkUser.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btn_checkUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_checkUser.Location = new System.Drawing.Point(147, 244);
            this.btn_checkUser.Name = "btn_checkUser";
            this.btn_checkUser.Size = new System.Drawing.Size(140, 40);
            this.btn_checkUser.Style = MetroFramework.MetroColorStyle.Black;
            this.btn_checkUser.TabIndex = 10;
            this.btn_checkUser.TabStop = false;
            this.btn_checkUser.Text = "CHECK USER";
            this.btn_checkUser.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_checkUser.UseCustomBackColor = true;
            this.btn_checkUser.UseCustomForeColor = true;
            this.btn_checkUser.UseSelectable = true;
            this.btn_checkUser.UseStyleColors = true;
            this.btn_checkUser.Click += new System.EventHandler(this.btn_checkUser_Click);
            // 
            // lbl_checkUsername
            // 
            this.lbl_checkUsername.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lbl_checkUsername.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_checkUsername.Location = new System.Drawing.Point(79, 141);
            this.lbl_checkUsername.Name = "lbl_checkUsername";
            this.lbl_checkUsername.Size = new System.Drawing.Size(200, 18);
            this.lbl_checkUsername.TabIndex = 44;
            this.lbl_checkUsername.Text = "None";
            this.lbl_checkUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_checkUsername.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_checkUsername.UseStyleColors = true;
            // 
            // txt_repSteamID
            // 
            // 
            // 
            // 
            this.txt_repSteamID.CustomButton.Image = null;
            this.txt_repSteamID.CustomButton.Location = new System.Drawing.Point(185, 1);
            this.txt_repSteamID.CustomButton.Name = "";
            this.txt_repSteamID.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_repSteamID.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_repSteamID.CustomButton.TabIndex = 1;
            this.txt_repSteamID.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_repSteamID.CustomButton.UseSelectable = true;
            this.txt_repSteamID.CustomButton.Visible = false;
            this.txt_repSteamID.Lines = new string[0];
            this.txt_repSteamID.Location = new System.Drawing.Point(80, 79);
            this.txt_repSteamID.MaxLength = 54;
            this.txt_repSteamID.Name = "txt_repSteamID";
            this.txt_repSteamID.PasswordChar = '\0';
            this.txt_repSteamID.PromptText = "steamURL/id32-64";
            this.txt_repSteamID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_repSteamID.SelectedText = "";
            this.txt_repSteamID.SelectionLength = 0;
            this.txt_repSteamID.SelectionStart = 0;
            this.txt_repSteamID.ShortcutsEnabled = true;
            this.txt_repSteamID.Size = new System.Drawing.Size(207, 23);
            this.txt_repSteamID.TabIndex = 1;
            this.txt_repSteamID.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_repSteamID.UseSelectable = true;
            this.txt_repSteamID.UseStyleColors = true;
            this.txt_repSteamID.WaterMark = "steamURL/id32-64";
            this.txt_repSteamID.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_repSteamID.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Enabled = false;
            this.metroLabel3.Location = new System.Drawing.Point(29, 81);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(50, 19);
            this.metroLabel3.TabIndex = 46;
            this.metroLabel3.Text = "Profile:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel3.UseStyleColors = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(-1, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 82;
            this.label2.Text = "Powered by:";
            // 
            // metroLink_steamrep
            // 
            this.metroLink_steamrep.Location = new System.Drawing.Point(61, 306);
            this.metroLink_steamrep.Name = "metroLink_steamrep";
            this.metroLink_steamrep.Size = new System.Drawing.Size(91, 19);
            this.metroLink_steamrep.TabIndex = 83;
            this.metroLink_steamrep.Text = "steamrep.com";
            this.metroLink_steamrep.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.metroLink_steamrep.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLink_steamrep.UseCustomBackColor = true;
            this.metroLink_steamrep.UseSelectable = true;
            this.metroLink_steamrep.Click += new System.EventHandler(this.metroLink_steamrep_Click);
            // 
            // Title_AlertScammer
            // 
            this.Title_AlertScammer.ActiveControl = null;
            this.Title_AlertScammer.BackColor = System.Drawing.Color.DarkGreen;
            this.Title_AlertScammer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Title_AlertScammer.ForeColor = System.Drawing.Color.Black;
            this.Title_AlertScammer.Location = new System.Drawing.Point(23, 198);
            this.Title_AlertScammer.Name = "Title_AlertScammer";
            this.Title_AlertScammer.Size = new System.Drawing.Size(264, 39);
            this.Title_AlertScammer.TabIndex = 86;
            this.Title_AlertScammer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Title_AlertScammer.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Title_AlertScammer.TileImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Title_AlertScammer.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.Title_AlertScammer.UseCustomBackColor = true;
            this.Title_AlertScammer.UseCustomForeColor = true;
            this.Title_AlertScammer.UseSelectable = true;
            this.Title_AlertScammer.Visible = false;
            this.Title_AlertScammer.Click += new System.EventHandler(this.Title_AlertScammer_Click);
            // 
            // lbl_steamid32
            // 
            this.lbl_steamid32.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lbl_steamid32.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_steamid32.Location = new System.Drawing.Point(79, 159);
            this.lbl_steamid32.Name = "lbl_steamid32";
            this.lbl_steamid32.Size = new System.Drawing.Size(200, 18);
            this.lbl_steamid32.TabIndex = 87;
            this.lbl_steamid32.Text = "None";
            this.lbl_steamid32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_steamid32.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_steamid32.UseCustomBackColor = true;
            // 
            // lbl_steamID64
            // 
            this.lbl_steamID64.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lbl_steamID64.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_steamID64.Location = new System.Drawing.Point(79, 173);
            this.lbl_steamID64.Name = "lbl_steamID64";
            this.lbl_steamID64.Size = new System.Drawing.Size(200, 18);
            this.lbl_steamID64.TabIndex = 88;
            this.lbl_steamID64.Text = "None";
            this.lbl_steamID64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_steamID64.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // richTextBox6
            // 
            this.richTextBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.richTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox6.DetectUrls = false;
            this.richTextBox6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox6.ForeColor = System.Drawing.Color.Gray;
            this.richTextBox6.Location = new System.Drawing.Point(23, 108);
            this.richTextBox6.MaxLength = 50;
            this.richTextBox6.Multiline = false;
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.ReadOnly = true;
            this.richTextBox6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox6.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox6.Size = new System.Drawing.Size(264, 14);
            this.richTextBox6.TabIndex = 89;
            this.richTextBox6.Text = "─────────────────────────────────────────";
            // 
            // picBox_SteamAvatar
            // 
            this.picBox_SteamAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.picBox_SteamAvatar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_SteamAvatar.Location = new System.Drawing.Point(23, 141);
            this.picBox_SteamAvatar.Name = "picBox_SteamAvatar";
            this.picBox_SteamAvatar.Size = new System.Drawing.Size(50, 50);
            this.picBox_SteamAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_SteamAvatar.TabIndex = 23;
            this.picBox_SteamAvatar.TabStop = false;
            this.picBox_SteamAvatar.Click += new System.EventHandler(this.picBox_SteamAvatar_Click);
            // 
            // ProgressSpinner_SteamRepDelay
            // 
            this.ProgressSpinner_SteamRepDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ProgressSpinner_SteamRepDelay.Location = new System.Drawing.Point(184, 247);
            this.ProgressSpinner_SteamRepDelay.Maximum = 100;
            this.ProgressSpinner_SteamRepDelay.Name = "ProgressSpinner_SteamRepDelay";
            this.ProgressSpinner_SteamRepDelay.Size = new System.Drawing.Size(69, 31);
            this.ProgressSpinner_SteamRepDelay.TabIndex = 90;
            this.ProgressSpinner_SteamRepDelay.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.ProgressSpinner_SteamRepDelay, "SPAM = IP BAN");
            this.ProgressSpinner_SteamRepDelay.UseCustomBackColor = true;
            this.ProgressSpinner_SteamRepDelay.UseCustomForeColor = true;
            this.ProgressSpinner_SteamRepDelay.UseSelectable = true;
            this.ProgressSpinner_SteamRepDelay.UseStyleColors = true;
            this.ProgressSpinner_SteamRepDelay.Visible = false;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLink1
            // 
            this.metroLink1.Location = new System.Drawing.Point(1, 288);
            this.metroLink1.Name = "metroLink1";
            this.metroLink1.Size = new System.Drawing.Size(274, 19);
            this.metroLink1.TabIndex = 91;
            this.metroLink1.Text = "SteamRep\'s API will be retired June 15th, 2025";
            this.metroLink1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.metroLink1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLink1.UseCustomBackColor = true;
            this.metroLink1.UseSelectable = true;
            // 
            // SteamRepCheck
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(307, 323);
            this.Controls.Add(this.metroLink1);
            this.Controls.Add(this.ProgressSpinner_SteamRepDelay);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.lbl_steamID64);
            this.Controls.Add(this.picBox_SteamAvatar);
            this.Controls.Add(this.lbl_steamid32);
            this.Controls.Add(this.Title_AlertScammer);
            this.Controls.Add(this.btn_checkUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.metroLink_steamrep);
            this.Controls.Add(this.txt_repSteamID);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.lbl_checkUsername);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SteamRepCheck";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Mercury - SteamRep";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picBox_SteamAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_checkUser;
        private MetroFramework.Controls.MetroLabel lbl_checkUsername;
        private MetroFramework.Controls.MetroTextBox txt_repSteamID;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroLink metroLink_steamrep;
        private MetroFramework.Controls.MetroTile Title_AlertScammer;
        private MetroFramework.Controls.MetroLabel lbl_steamid32;
        private MetroFramework.Controls.MetroLabel lbl_steamID64;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.PictureBox picBox_SteamAvatar;
        private MetroFramework.Controls.MetroProgressSpinner ProgressSpinner_SteamRepDelay;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Controls.MetroLink metroLink1;
    }
}