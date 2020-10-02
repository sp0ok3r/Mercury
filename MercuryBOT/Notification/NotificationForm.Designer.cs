namespace MercuryBOT.Notification
{
    partial class NotificationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationForm));
            this.lbl_time = new MetroFramework.Controls.MetroLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtBox_desc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(203, 54);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(38, 19);
            this.lbl_time.TabIndex = 0;
            this.lbl_time.Text = "13:37";
            this.lbl_time.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtBox_desc
            // 
            this.txtBox_desc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.txtBox_desc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_desc.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtBox_desc.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_desc.ForeColor = System.Drawing.Color.White;
            this.txtBox_desc.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtBox_desc.Location = new System.Drawing.Point(7, 7);
            this.txtBox_desc.MaxLength = 100;
            this.txtBox_desc.Multiline = true;
            this.txtBox_desc.Name = "txtBox_desc";
            this.txtBox_desc.ReadOnly = true;
            this.txtBox_desc.Size = new System.Drawing.Size(199, 66);
            this.txtBox_desc.TabIndex = 0;
            this.txtBox_desc.TabStop = false;
            // 
            // NotificationForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(240, 73);
            this.Controls.Add(this.txtBox_desc);
            this.Controls.Add(this.lbl_time);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "NotificationForm";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lbl_time;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtBox_desc;
    }
}