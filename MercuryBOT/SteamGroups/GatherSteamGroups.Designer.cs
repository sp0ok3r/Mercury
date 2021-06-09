namespace MercuryBOT.SteamGroups
{
    partial class GatherSteamGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GatherSteamGroups));
            this.btn_exitSelected = new MetroFramework.Controls.MetroButton();
            this.btn_exitfromAll = new MetroFramework.Controls.MetroButton();
            this.ClanList_ScrollBar = new MetroFramework.Controls.MetroScrollBar();
            this.btn_save2file = new MetroFramework.Controls.MetroButton();
            this.txtBox_Annonbody = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.txt_potwSteamID = new MetroFramework.Controls.MetroTextBox();
            this.btn_groupAnnouncement = new MetroFramework.Controls.MetroButton();
            this.txtBox_title = new MetroFramework.Controls.MetroTextBox();
            this.btn_potw = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtBox_gName = new MetroFramework.Controls.MetroTextBox();
            this.MercuryTabControlGroups = new MetroFramework.Controls.MetroTabControl();
            this.metroTab_Tasks = new MetroFramework.Controls.MetroTabPage();
            this.ProgressSpinner_MassInvite = new MetroFramework.Controls.MetroProgressSpinner();
            this.btn_massInvite = new MetroFramework.Controls.MetroButton();
            this.GridClanData = new MetroFramework.Controls.MetroGrid();
            this.GROUP_ID3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metroTab_Settings = new MetroFramework.Controls.MetroTabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.ProgressSpinner_JoinAllGroups = new MetroFramework.Controls.MetroProgressSpinner();
            this.btn_gatherFromProfile = new MetroFramework.Controls.MetroButton();
            this.txtBox_groupidsFile = new MetroFramework.Controls.MetroTextBox();
            this.lbl_pathGIDS = new MetroFramework.Controls.MetroLabel();
            this.txtBox_profileGrabIDS = new MetroFramework.Controls.MetroTextBox();
            this.btn_joinAll = new MetroFramework.Controls.MetroButton();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.link_setfile = new MetroFramework.Controls.MetroLink();
            this.lbl_groupSelected = new MetroFramework.Controls.MetroLabel();
            this.metroPanel10 = new MetroFramework.Controls.MetroPanel();
            this.MongoToolTip = new MetroFramework.Components.MetroToolTip();
            this.txt_totalgroups = new MetroFramework.Controls.MetroLabel();
            this.chk_copygroupid3 = new MetroFramework.Controls.MetroCheckBox();
            this.MercuryTabControlGroups.SuspendLayout();
            this.metroTab_Tasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridClanData)).BeginInit();
            this.metroTab_Settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_exitSelected
            // 
            this.btn_exitSelected.DisplayFocus = true;
            this.btn_exitSelected.ForeColor = System.Drawing.Color.White;
            this.btn_exitSelected.Location = new System.Drawing.Point(371, 468);
            this.btn_exitSelected.Name = "btn_exitSelected";
            this.btn_exitSelected.Size = new System.Drawing.Size(71, 39);
            this.btn_exitSelected.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_exitSelected.TabIndex = 15;
            this.btn_exitSelected.Text = "LEAVE \r\nSELECTED";
            this.btn_exitSelected.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_exitSelected.UseCustomForeColor = true;
            this.btn_exitSelected.UseSelectable = true;
            this.btn_exitSelected.UseStyleColors = true;
            this.btn_exitSelected.Click += new System.EventHandler(this.btn_exitSelected_Click);
            // 
            // btn_exitfromAll
            // 
            this.btn_exitfromAll.DisplayFocus = true;
            this.btn_exitfromAll.ForeColor = System.Drawing.Color.White;
            this.btn_exitfromAll.Location = new System.Drawing.Point(308, 468);
            this.btn_exitfromAll.Name = "btn_exitfromAll";
            this.btn_exitfromAll.Size = new System.Drawing.Size(57, 39);
            this.btn_exitfromAll.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_exitfromAll.TabIndex = 16;
            this.btn_exitfromAll.Text = "LEAVE  \r\nALL";
            this.btn_exitfromAll.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_exitfromAll.UseCustomForeColor = true;
            this.btn_exitfromAll.UseSelectable = true;
            this.btn_exitfromAll.UseStyleColors = true;
            this.btn_exitfromAll.Click += new System.EventHandler(this.btn_exitfromAll_Click);
            // 
            // ClanList_ScrollBar
            // 
            this.ClanList_ScrollBar.LargeChange = 10;
            this.ClanList_ScrollBar.Location = new System.Drawing.Point(427, 21);
            this.ClanList_ScrollBar.Maximum = 100;
            this.ClanList_ScrollBar.Minimum = 0;
            this.ClanList_ScrollBar.MouseWheelBarPartitions = 10;
            this.ClanList_ScrollBar.Name = "ClanList_ScrollBar";
            this.ClanList_ScrollBar.Orientation = MetroFramework.Controls.MetroScrollOrientation.Vertical;
            this.ClanList_ScrollBar.ScrollbarSize = 15;
            this.ClanList_ScrollBar.Size = new System.Drawing.Size(15, 412);
            this.ClanList_ScrollBar.Style = MetroFramework.MetroColorStyle.Purple;
            this.ClanList_ScrollBar.TabIndex = 23;
            this.ClanList_ScrollBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ClanList_ScrollBar.UseBarColor = true;
            this.ClanList_ScrollBar.UseSelectable = true;
            this.ClanList_ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ClanList_ScrollBar_Scroll);
            // 
            // btn_save2file
            // 
            this.btn_save2file.DisplayFocus = true;
            this.btn_save2file.ForeColor = System.Drawing.Color.White;
            this.btn_save2file.Location = new System.Drawing.Point(90, 468);
            this.btn_save2file.Name = "btn_save2file";
            this.btn_save2file.Size = new System.Drawing.Size(74, 39);
            this.btn_save2file.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_save2file.TabIndex = 24;
            this.btn_save2file.Text = "SAVE IDS \r\nTO FILE";
            this.btn_save2file.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_save2file.UseCustomForeColor = true;
            this.btn_save2file.UseSelectable = true;
            this.btn_save2file.UseStyleColors = true;
            this.btn_save2file.Click += new System.EventHandler(this.btn_save2file_Click);
            // 
            // txtBox_Annonbody
            // 
            // 
            // 
            // 
            this.txtBox_Annonbody.CustomButton.Image = null;
            this.txtBox_Annonbody.CustomButton.Location = new System.Drawing.Point(128, 1);
            this.txtBox_Annonbody.CustomButton.Name = "";
            this.txtBox_Annonbody.CustomButton.Size = new System.Drawing.Size(81, 81);
            this.txtBox_Annonbody.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_Annonbody.CustomButton.TabIndex = 1;
            this.txtBox_Annonbody.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_Annonbody.CustomButton.UseSelectable = true;
            this.txtBox_Annonbody.CustomButton.Visible = false;
            this.txtBox_Annonbody.Lines = new string[0];
            this.txtBox_Annonbody.Location = new System.Drawing.Point(149, 293);
            this.txtBox_Annonbody.MaxLength = 32767;
            this.txtBox_Annonbody.Multiline = true;
            this.txtBox_Annonbody.Name = "txtBox_Annonbody";
            this.txtBox_Annonbody.PasswordChar = '\0';
            this.txtBox_Annonbody.PromptText = "my friends!";
            this.txtBox_Annonbody.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_Annonbody.SelectedText = "";
            this.txtBox_Annonbody.SelectionLength = 0;
            this.txtBox_Annonbody.SelectionStart = 0;
            this.txtBox_Annonbody.ShortcutsEnabled = true;
            this.txtBox_Annonbody.Size = new System.Drawing.Size(210, 83);
            this.txtBox_Annonbody.TabIndex = 31;
            this.txtBox_Annonbody.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBox_Annonbody.UseSelectable = true;
            this.txtBox_Annonbody.UseStyleColors = true;
            this.txtBox_Annonbody.WaterMark = "my friends!";
            this.txtBox_Annonbody.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_Annonbody.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(51, 236);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(100, 19);
            this.metroLabel4.TabIndex = 30;
            this.metroLabel4.Text = "Announcement:";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel4.UseStyleColors = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Enabled = false;
            this.metroLabel3.Location = new System.Drawing.Point(47, 425);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(119, 19);
            this.metroLabel3.TabIndex = 29;
            this.metroLabel3.Text = "Player of the week:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel3.UseStyleColors = true;
            // 
            // txt_potwSteamID
            // 
            // 
            // 
            // 
            this.txt_potwSteamID.CustomButton.Image = null;
            this.txt_potwSteamID.CustomButton.Location = new System.Drawing.Point(250, 1);
            this.txt_potwSteamID.CustomButton.Name = "";
            this.txt_potwSteamID.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_potwSteamID.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_potwSteamID.CustomButton.TabIndex = 1;
            this.txt_potwSteamID.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_potwSteamID.CustomButton.UseSelectable = true;
            this.txt_potwSteamID.CustomButton.Visible = false;
            this.txt_potwSteamID.Lines = new string[0];
            this.txt_potwSteamID.Location = new System.Drawing.Point(51, 444);
            this.txt_potwSteamID.MaxLength = 32767;
            this.txt_potwSteamID.Name = "txt_potwSteamID";
            this.txt_potwSteamID.PasswordChar = '\0';
            this.txt_potwSteamID.PromptText = "STEAMID(32/64)/PROFILE URL";
            this.txt_potwSteamID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_potwSteamID.SelectedText = "";
            this.txt_potwSteamID.SelectionLength = 0;
            this.txt_potwSteamID.SelectionStart = 0;
            this.txt_potwSteamID.ShortcutsEnabled = true;
            this.txt_potwSteamID.Size = new System.Drawing.Size(272, 23);
            this.txt_potwSteamID.TabIndex = 28;
            this.txt_potwSteamID.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_potwSteamID.UseSelectable = true;
            this.txt_potwSteamID.UseStyleColors = true;
            this.txt_potwSteamID.WaterMark = "STEAMID(32/64)/PROFILE URL";
            this.txt_potwSteamID.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_potwSteamID.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btn_groupAnnouncement
            // 
            this.btn_groupAnnouncement.Location = new System.Drawing.Point(263, 382);
            this.btn_groupAnnouncement.Name = "btn_groupAnnouncement";
            this.btn_groupAnnouncement.Size = new System.Drawing.Size(96, 23);
            this.btn_groupAnnouncement.TabIndex = 27;
            this.btn_groupAnnouncement.Text = "POST";
            this.btn_groupAnnouncement.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_groupAnnouncement.UseSelectable = true;
            this.btn_groupAnnouncement.UseStyleColors = true;
            this.btn_groupAnnouncement.Click += new System.EventHandler(this.btn_groupAnnouncement_Click);
            // 
            // txtBox_title
            // 
            // 
            // 
            // 
            this.txtBox_title.CustomButton.Image = null;
            this.txtBox_title.CustomButton.Location = new System.Drawing.Point(188, 1);
            this.txtBox_title.CustomButton.Name = "";
            this.txtBox_title.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_title.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_title.CustomButton.TabIndex = 1;
            this.txtBox_title.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_title.CustomButton.UseSelectable = true;
            this.txtBox_title.CustomButton.Visible = false;
            this.txtBox_title.Lines = new string[0];
            this.txtBox_title.Location = new System.Drawing.Point(149, 258);
            this.txtBox_title.MaxLength = 32767;
            this.txtBox_title.Name = "txtBox_title";
            this.txtBox_title.PasswordChar = '\0';
            this.txtBox_title.PromptText = "Hello!";
            this.txtBox_title.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_title.SelectedText = "";
            this.txtBox_title.SelectionLength = 0;
            this.txtBox_title.SelectionStart = 0;
            this.txtBox_title.ShortcutsEnabled = true;
            this.txtBox_title.Size = new System.Drawing.Size(210, 23);
            this.txtBox_title.TabIndex = 37;
            this.txtBox_title.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBox_title.UseSelectable = true;
            this.txtBox_title.UseStyleColors = true;
            this.txtBox_title.WaterMark = "Hello!";
            this.txtBox_title.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_title.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btn_potw
            // 
            this.btn_potw.Location = new System.Drawing.Point(329, 444);
            this.btn_potw.Name = "btn_potw";
            this.btn_potw.Size = new System.Drawing.Size(30, 23);
            this.btn_potw.TabIndex = 34;
            this.btn_potw.Text = "SET";
            this.btn_potw.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_potw.UseSelectable = true;
            this.btn_potw.UseStyleColors = true;
            this.btn_potw.Click += new System.EventHandler(this.btn_potw_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(109, 290);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(42, 19);
            this.metroLabel1.TabIndex = 35;
            this.metroLabel1.Text = "Body:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(115, 262);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(36, 19);
            this.metroLabel2.TabIndex = 36;
            this.metroLabel2.Text = "Title:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtBox_gName
            // 
            this.txtBox_gName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            // 
            // 
            // 
            this.txtBox_gName.CustomButton.Image = null;
            this.txtBox_gName.CustomButton.Location = new System.Drawing.Point(420, 1);
            this.txtBox_gName.CustomButton.Name = "";
            this.txtBox_gName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_gName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_gName.CustomButton.TabIndex = 1;
            this.txtBox_gName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_gName.CustomButton.UseSelectable = true;
            this.txtBox_gName.CustomButton.Visible = false;
            this.txtBox_gName.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtBox_gName.Lines = new string[0];
            this.txtBox_gName.Location = new System.Drawing.Point(0, 439);
            this.txtBox_gName.MaxLength = 32767;
            this.txtBox_gName.Name = "txtBox_gName";
            this.txtBox_gName.PasswordChar = '\0';
            this.txtBox_gName.PromptText = "🔍 Search";
            this.txtBox_gName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_gName.SelectedText = "";
            this.txtBox_gName.SelectionLength = 0;
            this.txtBox_gName.SelectionStart = 0;
            this.txtBox_gName.ShortcutsEnabled = true;
            this.txtBox_gName.Size = new System.Drawing.Size(442, 23);
            this.txtBox_gName.TabIndex = 33;
            this.txtBox_gName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBox_gName.UseCustomBackColor = true;
            this.txtBox_gName.UseCustomForeColor = true;
            this.txtBox_gName.UseSelectable = true;
            this.txtBox_gName.WaterMark = "🔍 Search";
            this.txtBox_gName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_gName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_gName.TextChanged += new System.EventHandler(this.txtBox_gName_TextChanged);
            this.txtBox_gName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_gName_KeyDown);
            // 
            // MercuryTabControlGroups
            // 
            this.MercuryTabControlGroups.Controls.Add(this.metroTab_Tasks);
            this.MercuryTabControlGroups.Controls.Add(this.metroTab_Settings);
            this.MercuryTabControlGroups.Location = new System.Drawing.Point(13, 53);
            this.MercuryTabControlGroups.Multiline = true;
            this.MercuryTabControlGroups.Name = "MercuryTabControlGroups";
            this.MercuryTabControlGroups.SelectedIndex = 0;
            this.MercuryTabControlGroups.ShowToolTips = true;
            this.MercuryTabControlGroups.Size = new System.Drawing.Size(450, 549);
            this.MercuryTabControlGroups.TabIndex = 35;
            this.MercuryTabControlGroups.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MercuryTabControlGroups.UseSelectable = true;
            // 
            // metroTab_Tasks
            // 
            this.metroTab_Tasks.BackColor = System.Drawing.Color.Transparent;
            this.metroTab_Tasks.Controls.Add(this.ClanList_ScrollBar);
            this.metroTab_Tasks.Controls.Add(this.ProgressSpinner_MassInvite);
            this.metroTab_Tasks.Controls.Add(this.btn_massInvite);
            this.metroTab_Tasks.Controls.Add(this.txtBox_gName);
            this.metroTab_Tasks.Controls.Add(this.btn_save2file);
            this.metroTab_Tasks.Controls.Add(this.btn_exitfromAll);
            this.metroTab_Tasks.Controls.Add(this.btn_exitSelected);
            this.metroTab_Tasks.Controls.Add(this.GridClanData);
            this.metroTab_Tasks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroTab_Tasks.HorizontalScrollbarBarColor = true;
            this.metroTab_Tasks.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTab_Tasks.HorizontalScrollbarSize = 10;
            this.metroTab_Tasks.Location = new System.Drawing.Point(4, 38);
            this.metroTab_Tasks.Name = "metroTab_Tasks";
            this.metroTab_Tasks.Size = new System.Drawing.Size(442, 507);
            this.metroTab_Tasks.TabIndex = 6;
            this.metroTab_Tasks.Text = "CUSTOM TASKS";
            this.metroTab_Tasks.UseCustomBackColor = true;
            this.metroTab_Tasks.VerticalScrollbarBarColor = true;
            this.metroTab_Tasks.VerticalScrollbarHighlightOnWheel = false;
            this.metroTab_Tasks.VerticalScrollbarSize = 10;
            // 
            // ProgressSpinner_MassInvite
            // 
            this.ProgressSpinner_MassInvite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ProgressSpinner_MassInvite.Location = new System.Drawing.Point(22, 469);
            this.ProgressSpinner_MassInvite.Maximum = 100;
            this.ProgressSpinner_MassInvite.Name = "ProgressSpinner_MassInvite";
            this.ProgressSpinner_MassInvite.Size = new System.Drawing.Size(41, 37);
            this.ProgressSpinner_MassInvite.TabIndex = 43;
            this.ProgressSpinner_MassInvite.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ProgressSpinner_MassInvite.UseCustomBackColor = true;
            this.ProgressSpinner_MassInvite.UseCustomForeColor = true;
            this.ProgressSpinner_MassInvite.UseSelectable = true;
            this.ProgressSpinner_MassInvite.UseStyleColors = true;
            this.ProgressSpinner_MassInvite.Visible = false;
            // 
            // btn_massInvite
            // 
            this.btn_massInvite.Location = new System.Drawing.Point(0, 468);
            this.btn_massInvite.Name = "btn_massInvite";
            this.btn_massInvite.Size = new System.Drawing.Size(84, 39);
            this.btn_massInvite.TabIndex = 38;
            this.btn_massInvite.Text = "MASS INVITE";
            this.btn_massInvite.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_massInvite.UseSelectable = true;
            this.btn_massInvite.UseStyleColors = true;
            this.btn_massInvite.Click += new System.EventHandler(this.btn_massInvite_Click);
            // 
            // GridClanData
            // 
            this.GridClanData.AllowUserToAddRows = false;
            this.GridClanData.AllowUserToDeleteRows = false;
            this.GridClanData.AllowUserToOrderColumns = true;
            this.GridClanData.AllowUserToResizeColumns = false;
            this.GridClanData.AllowUserToResizeRows = false;
            this.GridClanData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.GridClanData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridClanData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridClanData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridClanData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridClanData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridClanData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GROUP_ID3,
            this.ColumnID,
            this.content});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridClanData.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridClanData.EnableHeadersVisualStyles = false;
            this.GridClanData.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.GridClanData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.GridClanData.Location = new System.Drawing.Point(-41, 6);
            this.GridClanData.MultiSelect = false;
            this.GridClanData.Name = "GridClanData";
            this.GridClanData.ReadOnly = true;
            this.GridClanData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridClanData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridClanData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridClanData.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.GridClanData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridClanData.Size = new System.Drawing.Size(483, 427);
            this.GridClanData.TabIndex = 14;
            this.GridClanData.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.GridClanData.UseCustomBackColor = true;
            this.GridClanData.UseCustomForeColor = true;
            this.GridClanData.UseStyleColors = true;
            this.GridClanData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridClanData_CellContentClick);
            this.GridClanData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridClanData_CellContentDoubleClick);
            // 
            // GROUP_ID3
            // 
            this.GROUP_ID3.Frozen = true;
            this.GROUP_ID3.HeaderText = "GROUP ID3";
            this.GROUP_ID3.Name = "GROUP_ID3";
            this.GROUP_ID3.ReadOnly = true;
            this.GROUP_ID3.Width = 105;
            // 
            // ColumnID
            // 
            this.ColumnID.Frozen = true;
            this.ColumnID.HeaderText = "GROUP ID 64bits";
            this.ColumnID.Name = "ColumnID";
            this.ColumnID.ReadOnly = true;
            this.ColumnID.Width = 130;
            // 
            // content
            // 
            this.content.Frozen = true;
            this.content.HeaderText = "GROUP NAME";
            this.content.Name = "content";
            this.content.ReadOnly = true;
            this.content.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.content.Width = 630;
            // 
            // metroTab_Settings
            // 
            this.metroTab_Settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.metroTab_Settings.Controls.Add(this.richTextBox2);
            this.metroTab_Settings.Controls.Add(this.ProgressSpinner_JoinAllGroups);
            this.metroTab_Settings.Controls.Add(this.txtBox_title);
            this.metroTab_Settings.Controls.Add(this.btn_gatherFromProfile);
            this.metroTab_Settings.Controls.Add(this.txtBox_groupidsFile);
            this.metroTab_Settings.Controls.Add(this.lbl_pathGIDS);
            this.metroTab_Settings.Controls.Add(this.txtBox_profileGrabIDS);
            this.metroTab_Settings.Controls.Add(this.txt_potwSteamID);
            this.metroTab_Settings.Controls.Add(this.btn_joinAll);
            this.metroTab_Settings.Controls.Add(this.metroLabel5);
            this.metroTab_Settings.Controls.Add(this.btn_potw);
            this.metroTab_Settings.Controls.Add(this.txtBox_Annonbody);
            this.metroTab_Settings.Controls.Add(this.metroLabel2);
            this.metroTab_Settings.Controls.Add(this.metroLabel4);
            this.metroTab_Settings.Controls.Add(this.metroLabel1);
            this.metroTab_Settings.Controls.Add(this.btn_groupAnnouncement);
            this.metroTab_Settings.Controls.Add(this.metroLabel3);
            this.metroTab_Settings.Controls.Add(this.link_setfile);
            this.metroTab_Settings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.metroTab_Settings.HorizontalScrollbarBarColor = true;
            this.metroTab_Settings.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTab_Settings.HorizontalScrollbarSize = 10;
            this.metroTab_Settings.Location = new System.Drawing.Point(4, 35);
            this.metroTab_Settings.Name = "metroTab_Settings";
            this.metroTab_Settings.Size = new System.Drawing.Size(442, 510);
            this.metroTab_Settings.Style = MetroFramework.MetroColorStyle.Purple;
            this.metroTab_Settings.TabIndex = 10;
            this.metroTab_Settings.Text = "OTHER TASKS";
            this.metroTab_Settings.UseCustomBackColor = true;
            this.metroTab_Settings.UseStyleColors = true;
            this.metroTab_Settings.VerticalScrollbarBarColor = true;
            this.metroTab_Settings.VerticalScrollbarHighlightOnWheel = false;
            this.metroTab_Settings.VerticalScrollbarSize = 10;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox2.DetectUrls = false;
            this.richTextBox2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.ForeColor = System.Drawing.Color.Gray;
            this.richTextBox2.Location = new System.Drawing.Point(51, 193);
            this.richTextBox2.MaxLength = 50;
            this.richTextBox2.Multiline = false;
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(321, 14);
            this.richTextBox2.TabIndex = 69;
            this.richTextBox2.Text = "─────────────────────────────────────────";
            // 
            // ProgressSpinner_JoinAllGroups
            // 
            this.ProgressSpinner_JoinAllGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ProgressSpinner_JoinAllGroups.Location = new System.Drawing.Point(343, 118);
            this.ProgressSpinner_JoinAllGroups.Maximum = 100;
            this.ProgressSpinner_JoinAllGroups.Name = "ProgressSpinner_JoinAllGroups";
            this.ProgressSpinner_JoinAllGroups.Size = new System.Drawing.Size(29, 23);
            this.ProgressSpinner_JoinAllGroups.TabIndex = 42;
            this.ProgressSpinner_JoinAllGroups.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ProgressSpinner_JoinAllGroups.UseCustomBackColor = true;
            this.ProgressSpinner_JoinAllGroups.UseCustomForeColor = true;
            this.ProgressSpinner_JoinAllGroups.UseSelectable = true;
            this.ProgressSpinner_JoinAllGroups.UseStyleColors = true;
            this.ProgressSpinner_JoinAllGroups.Visible = false;
            // 
            // btn_gatherFromProfile
            // 
            this.btn_gatherFromProfile.DisplayFocus = true;
            this.btn_gatherFromProfile.ForeColor = System.Drawing.Color.White;
            this.btn_gatherFromProfile.Location = new System.Drawing.Point(321, 56);
            this.btn_gatherFromProfile.Name = "btn_gatherFromProfile";
            this.btn_gatherFromProfile.Size = new System.Drawing.Size(74, 23);
            this.btn_gatherFromProfile.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_gatherFromProfile.TabIndex = 37;
            this.btn_gatherFromProfile.Text = "Save To File";
            this.btn_gatherFromProfile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_gatherFromProfile.UseCustomForeColor = true;
            this.btn_gatherFromProfile.UseSelectable = true;
            this.btn_gatherFromProfile.UseStyleColors = true;
            this.btn_gatherFromProfile.Click += new System.EventHandler(this.btn_gatherFromProfile_Click);
            // 
            // txtBox_groupidsFile
            // 
            // 
            // 
            // 
            this.txtBox_groupidsFile.CustomButton.Image = null;
            this.txtBox_groupidsFile.CustomButton.Location = new System.Drawing.Point(248, 1);
            this.txtBox_groupidsFile.CustomButton.Name = "";
            this.txtBox_groupidsFile.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_groupidsFile.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_groupidsFile.CustomButton.TabIndex = 1;
            this.txtBox_groupidsFile.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_groupidsFile.CustomButton.UseSelectable = true;
            this.txtBox_groupidsFile.CustomButton.Visible = false;
            this.txtBox_groupidsFile.Lines = new string[0];
            this.txtBox_groupidsFile.Location = new System.Drawing.Point(45, 118);
            this.txtBox_groupidsFile.MaxLength = 32767;
            this.txtBox_groupidsFile.Name = "txtBox_groupidsFile";
            this.txtBox_groupidsFile.PasswordChar = '\0';
            this.txtBox_groupidsFile.PromptText = "X:\\file_with_groupids.txt";
            this.txtBox_groupidsFile.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_groupidsFile.SelectedText = "";
            this.txtBox_groupidsFile.SelectionLength = 0;
            this.txtBox_groupidsFile.SelectionStart = 0;
            this.txtBox_groupidsFile.ShortcutsEnabled = true;
            this.txtBox_groupidsFile.Size = new System.Drawing.Size(270, 23);
            this.txtBox_groupidsFile.TabIndex = 36;
            this.txtBox_groupidsFile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBox_groupidsFile.UseSelectable = true;
            this.txtBox_groupidsFile.UseStyleColors = true;
            this.txtBox_groupidsFile.WaterMark = "X:\\file_with_groupids.txt";
            this.txtBox_groupidsFile.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_groupidsFile.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtBox_groupidsFile.Click += new System.EventHandler(this.txtBox_groupidsFile_Click);
            // 
            // lbl_pathGIDS
            // 
            this.lbl_pathGIDS.AutoSize = true;
            this.lbl_pathGIDS.Location = new System.Drawing.Point(42, 98);
            this.lbl_pathGIDS.Name = "lbl_pathGIDS";
            this.lbl_pathGIDS.Size = new System.Drawing.Size(89, 19);
            this.lbl_pathGIDS.TabIndex = 35;
            this.lbl_pathGIDS.Text = "Join from file:";
            this.lbl_pathGIDS.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtBox_profileGrabIDS
            // 
            // 
            // 
            // 
            this.txtBox_profileGrabIDS.CustomButton.Image = null;
            this.txtBox_profileGrabIDS.CustomButton.Location = new System.Drawing.Point(248, 1);
            this.txtBox_profileGrabIDS.CustomButton.Name = "";
            this.txtBox_profileGrabIDS.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_profileGrabIDS.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_profileGrabIDS.CustomButton.TabIndex = 1;
            this.txtBox_profileGrabIDS.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_profileGrabIDS.CustomButton.UseSelectable = true;
            this.txtBox_profileGrabIDS.CustomButton.Visible = false;
            this.txtBox_profileGrabIDS.Lines = new string[0];
            this.txtBox_profileGrabIDS.Location = new System.Drawing.Point(45, 56);
            this.txtBox_profileGrabIDS.MaxLength = 32767;
            this.txtBox_profileGrabIDS.Name = "txtBox_profileGrabIDS";
            this.txtBox_profileGrabIDS.PasswordChar = '\0';
            this.txtBox_profileGrabIDS.PromptText = "STEAMID(32/64)/PROFILE URL";
            this.txtBox_profileGrabIDS.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_profileGrabIDS.SelectedText = "";
            this.txtBox_profileGrabIDS.SelectionLength = 0;
            this.txtBox_profileGrabIDS.SelectionStart = 0;
            this.txtBox_profileGrabIDS.ShortcutsEnabled = true;
            this.txtBox_profileGrabIDS.Size = new System.Drawing.Size(270, 23);
            this.txtBox_profileGrabIDS.TabIndex = 39;
            this.txtBox_profileGrabIDS.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBox_profileGrabIDS.UseSelectable = true;
            this.txtBox_profileGrabIDS.UseStyleColors = true;
            this.txtBox_profileGrabIDS.WaterMark = "STEAMID(32/64)/PROFILE URL";
            this.txtBox_profileGrabIDS.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_profileGrabIDS.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btn_joinAll
            // 
            this.btn_joinAll.DisplayFocus = true;
            this.btn_joinAll.ForeColor = System.Drawing.Color.White;
            this.btn_joinAll.Location = new System.Drawing.Point(321, 118);
            this.btn_joinAll.Name = "btn_joinAll";
            this.btn_joinAll.Size = new System.Drawing.Size(74, 23);
            this.btn_joinAll.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_joinAll.TabIndex = 34;
            this.btn_joinAll.Text = "Join All";
            this.btn_joinAll.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_joinAll.UseCustomForeColor = true;
            this.btn_joinAll.UseSelectable = true;
            this.btn_joinAll.UseStyleColors = true;
            this.btn_joinAll.Click += new System.EventHandler(this.btn_joinAll_Click);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(41, 36);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(161, 19);
            this.metroLabel5.TabIndex = 38;
            this.metroLabel5.Text = "User Profile Groups Fetch:";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // link_setfile
            // 
            this.link_setfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link_setfile.Location = new System.Drawing.Point(218, 136);
            this.link_setfile.Name = "link_setfile";
            this.link_setfile.Size = new System.Drawing.Size(103, 23);
            this.link_setfile.TabIndex = 40;
            this.link_setfile.Text = "get file location";
            this.link_setfile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.link_setfile.UseSelectable = true;
            this.link_setfile.UseStyleColors = true;
            this.link_setfile.Click += new System.EventHandler(this.link_setfile_Click);
            // 
            // lbl_groupSelected
            // 
            this.lbl_groupSelected.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lbl_groupSelected.Location = new System.Drawing.Point(128, 47);
            this.lbl_groupSelected.Name = "lbl_groupSelected";
            this.lbl_groupSelected.Size = new System.Drawing.Size(174, 15);
            this.lbl_groupSelected.TabIndex = 38;
            this.lbl_groupSelected.Text = "None";
            this.lbl_groupSelected.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbl_groupSelected.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MongoToolTip.SetToolTip(this.lbl_groupSelected, "Selected");
            this.lbl_groupSelected.UseStyleColors = true;
            // 
            // metroPanel10
            // 
            this.metroPanel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.metroPanel10.HorizontalScrollbarBarColor = true;
            this.metroPanel10.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel10.HorizontalScrollbarSize = 10;
            this.metroPanel10.Location = new System.Drawing.Point(-1, 608);
            this.metroPanel10.Name = "metroPanel10";
            this.metroPanel10.Size = new System.Drawing.Size(475, 12);
            this.metroPanel10.TabIndex = 39;
            this.metroPanel10.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel10.UseCustomBackColor = true;
            this.metroPanel10.UseCustomForeColor = true;
            this.metroPanel10.VerticalScrollbarBarColor = true;
            this.metroPanel10.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel10.VerticalScrollbarSize = 10;
            // 
            // MongoToolTip
            // 
            this.MongoToolTip.Style = MetroFramework.MetroColorStyle.Default;
            this.MongoToolTip.StyleManager = null;
            this.MongoToolTip.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txt_totalgroups
            // 
            this.txt_totalgroups.AutoSize = true;
            this.txt_totalgroups.FontSize = MetroFramework.MetroLabelSize.Small;
            this.txt_totalgroups.Location = new System.Drawing.Point(403, 71);
            this.txt_totalgroups.Name = "txt_totalgroups";
            this.txt_totalgroups.Size = new System.Drawing.Size(33, 15);
            this.txt_totalgroups.TabIndex = 45;
            this.txt_totalgroups.Text = "Total:";
            this.txt_totalgroups.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txt_totalgroups.UseCustomBackColor = true;
            this.txt_totalgroups.UseStyleColors = true;
            // 
            // chk_copygroupid3
            // 
            this.chk_copygroupid3.AutoSize = true;
            this.chk_copygroupid3.Location = new System.Drawing.Point(319, 6);
            this.chk_copygroupid3.Name = "chk_copygroupid3";
            this.chk_copygroupid3.Size = new System.Drawing.Size(140, 15);
            this.chk_copygroupid3.TabIndex = 44;
            this.chk_copygroupid3.Text = "Copy ID3 to Clipboard";
            this.chk_copygroupid3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chk_copygroupid3.UseSelectable = true;
            this.chk_copygroupid3.UseStyleColors = true;
            // 
            // GatherSteamGroups
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(472, 618);
            this.Controls.Add(this.chk_copygroupid3);
            this.Controls.Add(this.metroPanel10);
            this.Controls.Add(this.lbl_groupSelected);
            this.Controls.Add(this.txt_totalgroups);
            this.Controls.Add(this.MercuryTabControlGroups);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GatherSteamGroups";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Mercury - STEAM GROUPS";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Shown += new System.EventHandler(this.GatherSteamGroups_Shown);
            this.MercuryTabControlGroups.ResumeLayout(false);
            this.metroTab_Tasks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridClanData)).EndInit();
            this.metroTab_Settings.ResumeLayout(false);
            this.metroTab_Settings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton btn_exitSelected;
        private MetroFramework.Controls.MetroButton btn_exitfromAll;
        private MetroFramework.Controls.MetroScrollBar ClanList_ScrollBar;
        private MetroFramework.Controls.MetroButton btn_save2file;
        private MetroFramework.Controls.MetroTextBox txtBox_Annonbody;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox txt_potwSteamID;
        private MetroFramework.Controls.MetroButton btn_groupAnnouncement;
        private MetroFramework.Controls.MetroButton btn_potw;
        private MetroFramework.Controls.MetroTextBox txtBox_title;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox txtBox_gName;
        private MetroFramework.Controls.MetroTabControl MercuryTabControlGroups;
        private MetroFramework.Controls.MetroTabPage metroTab_Tasks;
        private MetroFramework.Controls.MetroTabPage metroTab_Settings;
        private MetroFramework.Controls.MetroLabel lbl_groupSelected;
        private MetroFramework.Controls.MetroPanel metroPanel10;
        private MetroFramework.Controls.MetroTextBox txtBox_profileGrabIDS;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroButton btn_gatherFromProfile;
        private MetroFramework.Controls.MetroLabel lbl_pathGIDS;
        private MetroFramework.Controls.MetroButton btn_joinAll;
        private MetroFramework.Components.MetroToolTip MongoToolTip;
        private MetroFramework.Controls.MetroLink link_setfile;
        private MetroFramework.Controls.MetroTextBox txtBox_groupidsFile;
        private MetroFramework.Controls.MetroButton btn_massInvite;
        private MetroFramework.Controls.MetroProgressSpinner ProgressSpinner_JoinAllGroups;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private MetroFramework.Controls.MetroProgressSpinner ProgressSpinner_MassInvite;
        private MetroFramework.Controls.MetroGrid GridClanData;
        private System.Windows.Forms.DataGridViewTextBoxColumn GROUP_ID3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn content;
        private MetroFramework.Controls.MetroLabel txt_totalgroups;
        private MetroFramework.Controls.MetroCheckBox chk_copygroupid3;
    }
}