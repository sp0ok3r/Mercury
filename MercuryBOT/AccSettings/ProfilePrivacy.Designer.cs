namespace MercuryBOT.AccSettings
{
    partial class ProfilePrivacy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfilePrivacy));
            this.btn_changeprofSettings = new MetroFramework.Controls.MetroButton();
            this.lbl_redeemkey = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.combox_profilePrivacy = new MetroFramework.Controls.MetroComboBox();
            this.combox_InventoryPrivacy = new MetroFramework.Controls.MetroComboBox();
            this.combox_Gifts = new MetroFramework.Controls.MetroComboBox();
            this.combox_ownedGames = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.combox_PlayTime = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.combox_FriendsList = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.combox_Comment = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.FidgetSpinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.olamongo = new MetroFramework.Controls.MetroPanel();
            this.CollectPrivacySettings = new System.ComponentModel.BackgroundWorker();
            this.olamongo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_changeprofSettings
            // 
            this.btn_changeprofSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btn_changeprofSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_changeprofSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_changeprofSettings.Location = new System.Drawing.Point(193, 356);
            this.btn_changeprofSettings.Name = "btn_changeprofSettings";
            this.btn_changeprofSettings.Size = new System.Drawing.Size(97, 43);
            this.btn_changeprofSettings.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_changeprofSettings.TabIndex = 2;
            this.btn_changeprofSettings.TabStop = false;
            this.btn_changeprofSettings.Text = "SET";
            this.btn_changeprofSettings.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_changeprofSettings.UseCustomBackColor = true;
            this.btn_changeprofSettings.UseCustomForeColor = true;
            this.btn_changeprofSettings.UseSelectable = true;
            this.btn_changeprofSettings.UseStyleColors = true;
            this.btn_changeprofSettings.Click += new System.EventHandler(this.btn_changeprofSettings_Click);
            // 
            // lbl_redeemkey
            // 
            this.lbl_redeemkey.AutoSize = true;
            this.lbl_redeemkey.BackColor = System.Drawing.Color.Transparent;
            this.lbl_redeemkey.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_redeemkey.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_redeemkey.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_redeemkey.Location = new System.Drawing.Point(84, 84);
            this.lbl_redeemkey.Name = "lbl_redeemkey";
            this.lbl_redeemkey.Size = new System.Drawing.Size(66, 25);
            this.lbl_redeemkey.TabIndex = 53;
            this.lbl_redeemkey.Text = "Profile:";
            this.lbl_redeemkey.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_redeemkey.UseCustomBackColor = true;
            this.lbl_redeemkey.UseStyleColors = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.SystemColors.Window;
            this.metroLabel1.Location = new System.Drawing.Point(58, 198);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(91, 25);
            this.metroLabel1.TabIndex = 54;
            this.metroLabel1.Text = "Inventory:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel1.UseCustomBackColor = true;
            this.metroLabel1.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.SystemColors.Window;
            this.metroLabel2.Location = new System.Drawing.Point(22, 229);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(127, 25);
            this.metroLabel2.TabIndex = 55;
            this.metroLabel2.Text = "InventoryGifts:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel2.UseCustomBackColor = true;
            this.metroLabel2.UseStyleColors = true;
            // 
            // combox_profilePrivacy
            // 
            this.combox_profilePrivacy.FormattingEnabled = true;
            this.combox_profilePrivacy.ItemHeight = 23;
            this.combox_profilePrivacy.Items.AddRange(new object[] {
            "Unknown",
            "Private",
            "FriendsOnly",
            "Public"});
            this.combox_profilePrivacy.Location = new System.Drawing.Point(157, 80);
            this.combox_profilePrivacy.Name = "combox_profilePrivacy";
            this.combox_profilePrivacy.Size = new System.Drawing.Size(121, 29);
            this.combox_profilePrivacy.TabIndex = 56;
            this.combox_profilePrivacy.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_profilePrivacy.UseSelectable = true;
            this.combox_profilePrivacy.SelectedIndexChanged += new System.EventHandler(this.combox_profilePrivacy_SelectedIndexChanged);
            // 
            // combox_InventoryPrivacy
            // 
            this.combox_InventoryPrivacy.FormattingEnabled = true;
            this.combox_InventoryPrivacy.ItemHeight = 23;
            this.combox_InventoryPrivacy.Items.AddRange(new object[] {
            "Unknown",
            "Private",
            "FriendsOnly",
            "Public"});
            this.combox_InventoryPrivacy.Location = new System.Drawing.Point(156, 194);
            this.combox_InventoryPrivacy.Name = "combox_InventoryPrivacy";
            this.combox_InventoryPrivacy.Size = new System.Drawing.Size(121, 29);
            this.combox_InventoryPrivacy.TabIndex = 57;
            this.combox_InventoryPrivacy.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_InventoryPrivacy.UseSelectable = true;
            // 
            // combox_Gifts
            // 
            this.combox_Gifts.FormattingEnabled = true;
            this.combox_Gifts.ItemHeight = 23;
            this.combox_Gifts.Items.AddRange(new object[] {
            "Unknown",
            "Private",
            "FriendsOnly",
            "Public"});
            this.combox_Gifts.Location = new System.Drawing.Point(156, 229);
            this.combox_Gifts.Name = "combox_Gifts";
            this.combox_Gifts.Size = new System.Drawing.Size(121, 29);
            this.combox_Gifts.TabIndex = 58;
            this.combox_Gifts.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_Gifts.UseSelectable = true;
            // 
            // combox_ownedGames
            // 
            this.combox_ownedGames.FormattingEnabled = true;
            this.combox_ownedGames.ItemHeight = 23;
            this.combox_ownedGames.Items.AddRange(new object[] {
            "Unknown",
            "Private",
            "FriendsOnly",
            "Public"});
            this.combox_ownedGames.Location = new System.Drawing.Point(156, 276);
            this.combox_ownedGames.Name = "combox_ownedGames";
            this.combox_ownedGames.Size = new System.Drawing.Size(121, 29);
            this.combox_ownedGames.TabIndex = 60;
            this.combox_ownedGames.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_ownedGames.UseSelectable = true;
            this.combox_ownedGames.SelectedIndexChanged += new System.EventHandler(this.combox_ownedGames_SelectedIndexChanged);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.SystemColors.Window;
            this.metroLabel3.Location = new System.Drawing.Point(22, 276);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(127, 25);
            this.metroLabel3.TabIndex = 59;
            this.metroLabel3.Text = "OwnedGames:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseStyleColors = true;
            // 
            // combox_PlayTime
            // 
            this.combox_PlayTime.FormattingEnabled = true;
            this.combox_PlayTime.ItemHeight = 23;
            this.combox_PlayTime.Items.AddRange(new object[] {
            "Unknown",
            "Private",
            "FriendsOnly",
            "Public"});
            this.combox_PlayTime.Location = new System.Drawing.Point(156, 311);
            this.combox_PlayTime.Name = "combox_PlayTime";
            this.combox_PlayTime.Size = new System.Drawing.Size(121, 29);
            this.combox_PlayTime.TabIndex = 62;
            this.combox_PlayTime.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_PlayTime.UseSelectable = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.ForeColor = System.Drawing.SystemColors.Window;
            this.metroLabel4.Location = new System.Drawing.Point(66, 311);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(83, 25);
            this.metroLabel4.TabIndex = 61;
            this.metroLabel4.Text = "Playtime:";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel4.UseCustomBackColor = true;
            this.metroLabel4.UseStyleColors = true;
            // 
            // combox_FriendsList
            // 
            this.combox_FriendsList.FormattingEnabled = true;
            this.combox_FriendsList.ItemHeight = 23;
            this.combox_FriendsList.Items.AddRange(new object[] {
            "Unknown",
            "Private",
            "FriendsOnly",
            "Public"});
            this.combox_FriendsList.Location = new System.Drawing.Point(156, 115);
            this.combox_FriendsList.Name = "combox_FriendsList";
            this.combox_FriendsList.Size = new System.Drawing.Size(121, 29);
            this.combox_FriendsList.TabIndex = 64;
            this.combox_FriendsList.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_FriendsList.UseSelectable = true;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.ForeColor = System.Drawing.SystemColors.Window;
            this.metroLabel5.Location = new System.Drawing.Point(51, 119);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(99, 25);
            this.metroLabel5.TabIndex = 63;
            this.metroLabel5.Text = "FriendsList:";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel5.UseCustomBackColor = true;
            this.metroLabel5.UseStyleColors = true;
            // 
            // combox_Comment
            // 
            this.combox_Comment.FormattingEnabled = true;
            this.combox_Comment.ItemHeight = 23;
            this.combox_Comment.Items.AddRange(new object[] {
            "FriendsOnly",
            "Public",
            "Private"});
            this.combox_Comment.Location = new System.Drawing.Point(156, 150);
            this.combox_Comment.Name = "combox_Comment";
            this.combox_Comment.Size = new System.Drawing.Size(121, 29);
            this.combox_Comment.TabIndex = 66;
            this.combox_Comment.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_Comment.UseSelectable = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel6.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.ForeColor = System.Drawing.SystemColors.Window;
            this.metroLabel6.Location = new System.Drawing.Point(51, 151);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(95, 25);
            this.metroLabel6.TabIndex = 65;
            this.metroLabel6.Text = "Comment:";
            this.metroLabel6.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel6.UseCustomBackColor = true;
            this.metroLabel6.UseStyleColors = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.Gray;
            this.richTextBox1.Location = new System.Drawing.Point(69, 179);
            this.richTextBox1.MaxLength = 50;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(195, 14);
            this.richTextBox1.TabIndex = 67;
            this.richTextBox1.Text = "─────────────────────────────────────────";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox2.DetectUrls = false;
            this.richTextBox2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.ForeColor = System.Drawing.Color.Gray;
            this.richTextBox2.Location = new System.Drawing.Point(69, 259);
            this.richTextBox2.MaxLength = 50;
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(195, 14);
            this.richTextBox2.TabIndex = 68;
            this.richTextBox2.Text = "─────────────────────────────────────────";
            // 
            // FidgetSpinner
            // 
            this.FidgetSpinner.Location = new System.Drawing.Point(60, 66);
            this.FidgetSpinner.Maximum = 100;
            this.FidgetSpinner.Name = "FidgetSpinner";
            this.FidgetSpinner.Size = new System.Drawing.Size(148, 160);
            this.FidgetSpinner.TabIndex = 69;
            this.FidgetSpinner.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FidgetSpinner.UseCustomBackColor = true;
            this.FidgetSpinner.UseSelectable = true;
            this.FidgetSpinner.UseStyleColors = true;
            // 
            // olamongo
            // 
            this.olamongo.Controls.Add(this.FidgetSpinner);
            this.olamongo.HorizontalScrollbarBarColor = true;
            this.olamongo.HorizontalScrollbarHighlightOnWheel = false;
            this.olamongo.HorizontalScrollbarSize = 10;
            this.olamongo.Location = new System.Drawing.Point(9, 63);
            this.olamongo.Name = "olamongo";
            this.olamongo.Size = new System.Drawing.Size(281, 287);
            this.olamongo.TabIndex = 70;
            this.olamongo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.olamongo.UseCustomBackColor = true;
            this.olamongo.VerticalScrollbarBarColor = true;
            this.olamongo.VerticalScrollbarHighlightOnWheel = false;
            this.olamongo.VerticalScrollbarSize = 10;
            // 
            // CollectPrivacySettings
            // 
            this.CollectPrivacySettings.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CollectPrivacySettings_DoWork);
            // 
            // ProfilePrivacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 410);
            this.Controls.Add(this.olamongo);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.combox_Comment);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.combox_FriendsList);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.combox_PlayTime);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.combox_ownedGames);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.combox_Gifts);
            this.Controls.Add(this.combox_InventoryPrivacy);
            this.Controls.Add(this.combox_profilePrivacy);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.lbl_redeemkey);
            this.Controls.Add(this.btn_changeprofSettings);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProfilePrivacy";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Change Profile Privacy";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.ProfilePrivacy_Load);
            this.olamongo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_changeprofSettings;
        private MetroFramework.Controls.MetroLabel lbl_redeemkey;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox combox_profilePrivacy;
        private MetroFramework.Controls.MetroComboBox combox_InventoryPrivacy;
        private MetroFramework.Controls.MetroComboBox combox_Gifts;
        private MetroFramework.Controls.MetroComboBox combox_ownedGames;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox combox_PlayTime;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox combox_FriendsList;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox combox_Comment;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private MetroFramework.Controls.MetroProgressSpinner FidgetSpinner;
        private MetroFramework.Controls.MetroPanel olamongo;
        private System.ComponentModel.BackgroundWorker CollectPrivacySettings;
    }
}