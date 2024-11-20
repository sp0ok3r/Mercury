namespace MercuryBOT.AccSettings
{
    partial class AccSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccSettingsForm));
            this.txtBox_nameChange = new MetroFramework.Controls.MetroTextBox();
            this.lbl_changeName = new MetroFramework.Controls.MetroLabel();
            this.btn_setName = new MetroFramework.Controls.MetroButton();
            this.btn_clearuserAliases = new MetroFramework.Controls.MetroButton();
            this.combox_uimodes = new MetroFramework.Controls.MetroComboBox();
            this.lbl_uimode = new MetroFramework.Controls.MetroLabel();
            this.combox_states = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel19 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // txtBox_nameChange
            // 
            this.txtBox_nameChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            // 
            // 
            // 
            this.txtBox_nameChange.CustomButton.Image = null;
            this.txtBox_nameChange.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.txtBox_nameChange.CustomButton.Name = "";
            this.txtBox_nameChange.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtBox_nameChange.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBox_nameChange.CustomButton.TabIndex = 1;
            this.txtBox_nameChange.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBox_nameChange.CustomButton.UseSelectable = true;
            this.txtBox_nameChange.CustomButton.Visible = false;
            this.txtBox_nameChange.ForeColor = System.Drawing.Color.White;
            this.txtBox_nameChange.Lines = new string[0];
            this.txtBox_nameChange.Location = new System.Drawing.Point(23, 79);
            this.txtBox_nameChange.MaxLength = 32;
            this.txtBox_nameChange.Multiline = true;
            this.txtBox_nameChange.Name = "txtBox_nameChange";
            this.txtBox_nameChange.PasswordChar = '\0';
            this.txtBox_nameChange.PromptText = "ma name jeff";
            this.txtBox_nameChange.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBox_nameChange.SelectedText = "";
            this.txtBox_nameChange.SelectionLength = 0;
            this.txtBox_nameChange.SelectionStart = 0;
            this.txtBox_nameChange.ShortcutsEnabled = true;
            this.txtBox_nameChange.Size = new System.Drawing.Size(132, 29);
            this.txtBox_nameChange.TabIndex = 28;
            this.txtBox_nameChange.UseCustomBackColor = true;
            this.txtBox_nameChange.UseCustomForeColor = true;
            this.txtBox_nameChange.UseSelectable = true;
            this.txtBox_nameChange.UseStyleColors = true;
            this.txtBox_nameChange.WaterMark = "ma name jeff";
            this.txtBox_nameChange.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBox_nameChange.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lbl_changeName
            // 
            this.lbl_changeName.AutoSize = true;
            this.lbl_changeName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_changeName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_changeName.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_changeName.Location = new System.Drawing.Point(19, 62);
            this.lbl_changeName.Name = "lbl_changeName";
            this.lbl_changeName.Size = new System.Drawing.Size(115, 19);
            this.lbl_changeName.TabIndex = 29;
            this.lbl_changeName.Text = "NAME CHANGER";
            this.lbl_changeName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_changeName.UseCustomBackColor = true;
            this.lbl_changeName.UseStyleColors = true;
            // 
            // btn_setName
            // 
            this.btn_setName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btn_setName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_setName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_setName.Location = new System.Drawing.Point(161, 79);
            this.btn_setName.Name = "btn_setName";
            this.btn_setName.Size = new System.Drawing.Size(27, 29);
            this.btn_setName.Style = MetroFramework.MetroColorStyle.Purple;
            this.btn_setName.TabIndex = 27;
            this.btn_setName.TabStop = false;
            this.btn_setName.Text = "✓";
            this.btn_setName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_setName.UseCustomBackColor = true;
            this.btn_setName.UseCustomForeColor = true;
            this.btn_setName.UseSelectable = true;
            this.btn_setName.UseStyleColors = true;
            this.btn_setName.Click += new System.EventHandler(this.btn_setName_Click);
            // 
            // btn_clearuserAliases
            // 
            this.btn_clearuserAliases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btn_clearuserAliases.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btn_clearuserAliases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_clearuserAliases.Location = new System.Drawing.Point(196, 79);
            this.btn_clearuserAliases.Name = "btn_clearuserAliases";
            this.btn_clearuserAliases.Size = new System.Drawing.Size(95, 29);
            this.btn_clearuserAliases.TabIndex = 42;
            this.btn_clearuserAliases.TabStop = false;
            this.btn_clearuserAliases.Text = "CLEAR \r\nUSER ALIASES";
            this.btn_clearuserAliases.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_clearuserAliases.UseCustomBackColor = true;
            this.btn_clearuserAliases.UseCustomForeColor = true;
            this.btn_clearuserAliases.UseSelectable = true;
            this.btn_clearuserAliases.UseStyleColors = true;
            this.btn_clearuserAliases.Click += new System.EventHandler(this.btn_clearuserAliases_Click);
            // 
            // combox_uimodes
            // 
            this.combox_uimodes.FormattingEnabled = true;
            this.combox_uimodes.ItemHeight = 23;
            this.combox_uimodes.Items.AddRange(new object[] {
            "Disabled",
            "BIG PICTURE 🎮",
            "VIRTUAL REALITY 😎",
            "SMARTPHONE 📱"});
            this.combox_uimodes.Location = new System.Drawing.Point(23, 212);
            this.combox_uimodes.Name = "combox_uimodes";
            this.combox_uimodes.Size = new System.Drawing.Size(132, 29);
            this.combox_uimodes.TabIndex = 44;
            this.combox_uimodes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_uimodes.UseCustomForeColor = true;
            this.combox_uimodes.UseSelectable = true;
            this.combox_uimodes.UseStyleColors = true;
            this.combox_uimodes.SelectedIndexChanged += new System.EventHandler(this.combox_uimodes_SelectedIndexChanged);
            // 
            // lbl_uimode
            // 
            this.lbl_uimode.AutoSize = true;
            this.lbl_uimode.BackColor = System.Drawing.Color.Transparent;
            this.lbl_uimode.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_uimode.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_uimode.Location = new System.Drawing.Point(19, 193);
            this.lbl_uimode.Name = "lbl_uimode";
            this.lbl_uimode.Size = new System.Drawing.Size(68, 19);
            this.lbl_uimode.TabIndex = 47;
            this.lbl_uimode.Text = "UI MODE";
            this.lbl_uimode.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lbl_uimode.UseCustomBackColor = true;
            this.lbl_uimode.UseStyleColors = true;
            // 
            // combox_states
            // 
            this.combox_states.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.combox_states.FormattingEnabled = true;
            this.combox_states.ItemHeight = 23;
            this.combox_states.Items.AddRange(new object[] {
            "Offline",
            "Online",
            "Busy",
            "Away",
            "Snooze 💤",
            "Looking To Trade",
            "Looking To Play",
            "Invisible"});
            this.combox_states.Location = new System.Drawing.Point(23, 149);
            this.combox_states.Name = "combox_states";
            this.combox_states.Size = new System.Drawing.Size(132, 29);
            this.combox_states.TabIndex = 43;
            this.combox_states.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.combox_states.UseCustomForeColor = true;
            this.combox_states.UseSelectable = true;
            this.combox_states.UseStyleColors = true;
            this.combox_states.SelectedIndexChanged += new System.EventHandler(this.combox_states_SelectedIndexChanged);
            // 
            // metroLabel19
            // 
            this.metroLabel19.AutoSize = true;
            this.metroLabel19.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel19.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel19.ForeColor = System.Drawing.SystemColors.Window;
            this.metroLabel19.Location = new System.Drawing.Point(19, 129);
            this.metroLabel19.Name = "metroLabel19";
            this.metroLabel19.Size = new System.Drawing.Size(111, 19);
            this.metroLabel19.TabIndex = 46;
            this.metroLabel19.Text = "STATE CHANGER";
            this.metroLabel19.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel19.UseCustomBackColor = true;
            this.metroLabel19.UseStyleColors = true;
            // 
            // AccSettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(314, 258);
            this.Controls.Add(this.combox_uimodes);
            this.Controls.Add(this.lbl_uimode);
            this.Controls.Add(this.combox_states);
            this.Controls.Add(this.metroLabel19);
            this.Controls.Add(this.btn_clearuserAliases);
            this.Controls.Add(this.txtBox_nameChange);
            this.Controls.Add(this.lbl_changeName);
            this.Controls.Add(this.btn_setName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AccSettingsForm";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Mercury - AccSettings";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox txtBox_nameChange;
        private MetroFramework.Controls.MetroLabel lbl_changeName;
        private MetroFramework.Controls.MetroButton btn_setName;
        private MetroFramework.Controls.MetroButton btn_clearuserAliases;
        private MetroFramework.Controls.MetroComboBox combox_uimodes;
        private MetroFramework.Controls.MetroLabel lbl_uimode;
        private MetroFramework.Controls.MetroComboBox combox_states;
        private MetroFramework.Controls.MetroLabel metroLabel19;
    }
}