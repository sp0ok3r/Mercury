namespace MercuryBOT.GamesGather
{
    partial class SelectGames
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectGames));
            this.txtBox_Game2Find = new MetroFramework.Controls.MetroTextBox();
            this.list_main_game = new MetroFramework.Controls.MetroListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lbl_selgames_count = new MetroFramework.Controls.MetroLabel();
            this.progreeBar_GatherGames = new MetroFramework.Controls.MetroProgressBar();
            this.btn_selectAll = new MetroFramework.Controls.MetroButton();
            this.lbl_find = new MetroFramework.Controls.MetroLabel();
            this.lbl_totalfoldersize = new MetroFramework.Controls.MetroLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MongoToolTip = new MetroFramework.Components.MetroToolTip();
            this.link_GamesIMGPath = new MetroFramework.Controls.MetroLink();
            this.SuspendLayout();
            // 
            // txtBox_Game2Find
            // 
            this.txtBox_Game2Find.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            // 
            // 
            // 
            this.txtBox_Game2Find.CustomButton.Image = null;
            this.txtBox_Game2Find.CustomButton.Location = new System.Drawing.Point(289, 1);
            this.txtBox_Game2Find.CustomButton.Name = "";
            this.txtBox_Game2Find.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtBox_Game2Find.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_Game2Find.CustomButton.TabIndex = 1;
            this.txtBox_Game2Find.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_Game2Find.CustomButton.UseSelectable = true;
            this.txtBox_Game2Find.CustomButton.Visible = false;
            this.txtBox_Game2Find.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtBox_Game2Find.Lines = new string[0];
            this.txtBox_Game2Find.Location = new System.Drawing.Point(674, 52);
            this.txtBox_Game2Find.MaxLength = 32767;
            this.txtBox_Game2Find.Name = "txtBox_Game2Find";
            this.txtBox_Game2Find.PasswordChar = '\0';
            this.txtBox_Game2Find.PromptText = "Name or ID";
            this.txtBox_Game2Find.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_Game2Find.SelectedText = "";
            this.txtBox_Game2Find.SelectionLength = 0;
            this.txtBox_Game2Find.SelectionStart = 0;
            this.txtBox_Game2Find.ShortcutsEnabled = true;
            this.txtBox_Game2Find.Size = new System.Drawing.Size(311, 23);
            this.txtBox_Game2Find.TabIndex = 2;
            this.txtBox_Game2Find.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MongoToolTip.SetToolTip(this.txtBox_Game2Find, "Press ENTER to find!");
            this.txtBox_Game2Find.UseCustomBackColor = true;
            this.txtBox_Game2Find.UseCustomForeColor = true;
            this.txtBox_Game2Find.UseSelectable = true;
            this.txtBox_Game2Find.WaterMark = "Name or ID";
            this.txtBox_Game2Find.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_Game2Find.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtBox_Game2Find.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_Game2Find_KeyDown);
            // 
            // list_main_game
            // 
            this.list_main_game.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.list_main_game.BackgroundImageTiled = true;
            this.list_main_game.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.list_main_game.ForeColor = System.Drawing.SystemColors.ControlText;
            this.list_main_game.FullRowSelect = true;
            this.list_main_game.LargeImageList = this.imageList1;
            this.list_main_game.Location = new System.Drawing.Point(23, 81);
            this.list_main_game.MultiSelect = false;
            this.list_main_game.Name = "list_main_game";
            this.list_main_game.OwnerDraw = true;
            this.list_main_game.Size = new System.Drawing.Size(962, 303);
            this.list_main_game.SmallImageList = this.imageList1;
            this.list_main_game.TabIndex = 4;
            this.list_main_game.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MongoToolTip.SetToolTip(this.list_main_game, "Double click to add selected GAME!");
            this.list_main_game.UseCompatibleStateImageBehavior = false;
            this.list_main_game.UseCustomBackColor = true;
            this.list_main_game.UseCustomForeColor = true;
            this.list_main_game.UseSelectable = true;
            this.list_main_game.View = System.Windows.Forms.View.List;
            this.list_main_game.SelectedIndexChanged += new System.EventHandler(this.list_main_game_SelectedIndexChanged);
            this.list_main_game.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.list_main_game_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(184, 69);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lbl_selgames_count
            // 
            this.lbl_selgames_count.AutoSize = true;
            this.lbl_selgames_count.ForeColor = System.Drawing.Color.White;
            this.lbl_selgames_count.Location = new System.Drawing.Point(20, 381);
            this.lbl_selgames_count.Name = "lbl_selgames_count";
            this.lbl_selgames_count.Size = new System.Drawing.Size(65, 19);
            this.lbl_selgames_count.TabIndex = 5;
            this.lbl_selgames_count.Text = "Loading...";
            this.lbl_selgames_count.UseCustomBackColor = true;
            this.lbl_selgames_count.UseStyleColors = true;
            // 
            // progreeBar_GatherGames
            // 
            this.progreeBar_GatherGames.Location = new System.Drawing.Point(-7, -5);
            this.progreeBar_GatherGames.Name = "progreeBar_GatherGames";
            this.progreeBar_GatherGames.Size = new System.Drawing.Size(1016, 10);
            this.progreeBar_GatherGames.TabIndex = 9;
            this.progreeBar_GatherGames.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btn_selectAll
            // 
            this.btn_selectAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_selectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_selectAll.ForeColor = System.Drawing.Color.White;
            this.btn_selectAll.Location = new System.Drawing.Point(867, 390);
            this.btn_selectAll.Name = "btn_selectAll";
            this.btn_selectAll.Size = new System.Drawing.Size(118, 23);
            this.btn_selectAll.TabIndex = 13;
            this.btn_selectAll.Text = "SELECT ALL";
            this.btn_selectAll.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_selectAll.UseCustomBackColor = true;
            this.btn_selectAll.UseCustomForeColor = true;
            this.btn_selectAll.UseSelectable = true;
            this.btn_selectAll.Click += new System.EventHandler(this.Btn_selectAll_Click);
            // 
            // lbl_find
            // 
            this.lbl_find.AutoSize = true;
            this.lbl_find.ForeColor = System.Drawing.Color.White;
            this.lbl_find.Location = new System.Drawing.Point(640, 56);
            this.lbl_find.Name = "lbl_find";
            this.lbl_find.Size = new System.Drawing.Size(37, 19);
            this.lbl_find.TabIndex = 14;
            this.lbl_find.Text = "Find:";
            this.lbl_find.UseCustomBackColor = true;
            this.lbl_find.UseStyleColors = true;
            // 
            // lbl_totalfoldersize
            // 
            this.lbl_totalfoldersize.AutoSize = true;
            this.lbl_totalfoldersize.ForeColor = System.Drawing.Color.White;
            this.lbl_totalfoldersize.Location = new System.Drawing.Point(20, 63);
            this.lbl_totalfoldersize.Name = "lbl_totalfoldersize";
            this.lbl_totalfoldersize.Size = new System.Drawing.Size(108, 19);
            this.lbl_totalfoldersize.TabIndex = 15;
            this.lbl_totalfoldersize.Text = "Folder Total Size:";
            this.lbl_totalfoldersize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_totalfoldersize.UseCustomBackColor = true;
            this.lbl_totalfoldersize.UseStyleColors = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(-1, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1012, 10);
            this.panel1.TabIndex = 16;
            // 
            // MongoToolTip
            // 
            this.MongoToolTip.Style = MetroFramework.MetroColorStyle.Purple;
            this.MongoToolTip.StyleManager = null;
            this.MongoToolTip.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MongoToolTip.UseAnimation = false;
            this.MongoToolTip.UseFading = false;
            // 
            // link_GamesIMGPath
            // 
            this.link_GamesIMGPath.Location = new System.Drawing.Point(832, 388);
            this.link_GamesIMGPath.Name = "link_GamesIMGPath";
            this.link_GamesIMGPath.Size = new System.Drawing.Size(29, 28);
            this.link_GamesIMGPath.TabIndex = 26;
            this.link_GamesIMGPath.Text = "📁";
            this.link_GamesIMGPath.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.link_GamesIMGPath.UseCustomBackColor = true;
            this.link_GamesIMGPath.UseSelectable = true;
            this.link_GamesIMGPath.Click += new System.EventHandler(this.metroLink_GamesIMGPath_Click);
            // 
            // SelectGames
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 419);
            this.Controls.Add(this.link_GamesIMGPath);
            this.Controls.Add(this.progreeBar_GatherGames);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.list_main_game);
            this.Controls.Add(this.btn_selectAll);
            this.Controls.Add(this.lbl_selgames_count);
            this.Controls.Add(this.txtBox_Game2Find);
            this.Controls.Add(this.lbl_totalfoldersize);
            this.Controls.Add(this.lbl_find);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SelectGames";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Mercury - SELECT GAMES 🛒";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectGames_FormClosed);
            this.Load += new System.EventHandler(this.SelectGames_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox txtBox_Game2Find;
        private MetroFramework.Controls.MetroListView list_main_game;
        private MetroFramework.Controls.MetroLabel lbl_selgames_count;
        private System.Windows.Forms.ImageList imageList1;
        private MetroFramework.Controls.MetroProgressBar progreeBar_GatherGames;
        private MetroFramework.Controls.MetroButton btn_selectAll;
        private MetroFramework.Controls.MetroLabel lbl_find;
        private MetroFramework.Controls.MetroLabel lbl_totalfoldersize;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Components.MetroToolTip MongoToolTip;
        private MetroFramework.Controls.MetroLink link_GamesIMGPath;
    }
}