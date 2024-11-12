using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
namespace Mercury.MetroMessageBox
{
    public class CustomMetroMessageBoxControl : Form
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _defaultColor = Color.FromArgb(57, 179, 215);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _errorColor = Color.FromArgb(210, 50, 45);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _question = Color.FromArgb(71, 164, 71);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color _success = Color.FromArgb(71, 164, 71);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _warningColor = Color.FromArgb(237, 156, 40);

        private IContainer components;
        private MetroLabel messageLabel;
        private MetroButton metroButton1;
        private MetroButton metroButton2;
        private MetroButton metroButton3;
        private Panel pnlBottom;
        private Label titleLabel;
        private TableLayoutPanel tlpBody;

        public CustomMetroMessageBoxControl()
        {
            InitializeComponent();
            Properties = new CustomMetroMessageBoxProperties(this);
            StylizeButton(metroButton1);
            StylizeButton(metroButton2);
            StylizeButton(metroButton3);
            metroButton1.Click += button_Click;
            metroButton2.Click += button_Click;
            metroButton3.Click += button_Click;
        }

        public Panel Body { get; private set; }

        public CustomMetroMessageBoxProperties Properties { get; }

        public DialogResult Result { get; private set; }

        public void ArrangeApperance()
        {
            titleLabel.Text = Properties.Title;
            messageLabel.Text = Properties.Message;
            switch (Properties.Icon)
            {
                case MessageBoxIcon.Hand:
                    Body.BackColor = _errorColor;
                    break;
                case MessageBoxIcon.Exclamation:
                    Body.BackColor = _warningColor;
                    break;
            }

            switch (Properties.Buttons)
            {
                case MessageBoxButtons.OK:
                    EnableButton(metroButton1);
                    metroButton1.Text = "Ok";
                    metroButton1.Location = metroButton3.Location;
                    metroButton1.Tag = DialogResult.OK;
                    EnableButton(metroButton2, false);
                    EnableButton(metroButton3, false);
                    break;
                case MessageBoxButtons.OKCancel:
                    EnableButton(metroButton1);
                    metroButton1.Text = "Ok";
                    metroButton1.Location = metroButton2.Location;
                    metroButton1.Tag = DialogResult.OK;
                    EnableButton(metroButton2);
                    metroButton2.Text = "Cancel";
                    metroButton2.Location = metroButton3.Location;
                    metroButton2.Tag = DialogResult.Cancel;
                    EnableButton(metroButton3, false);
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    EnableButton(metroButton1);
                    metroButton1.Text = "Abort";
                    metroButton1.Tag = DialogResult.Abort;
                    EnableButton(metroButton2);
                    metroButton2.Text = "Retry";
                    metroButton2.Tag = DialogResult.Retry;
                    EnableButton(metroButton3);
                    metroButton3.Text = "Ignore";
                    metroButton3.Tag = DialogResult.Ignore;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    EnableButton(metroButton1);
                    metroButton1.Text = "Yes";
                    metroButton1.Tag = DialogResult.Yes;
                    EnableButton(metroButton2);
                    metroButton2.Text = "No";
                    metroButton2.Tag = DialogResult.No;
                    EnableButton(metroButton3);
                    metroButton3.Text = "Cancel";
                    metroButton3.Tag = DialogResult.Cancel;
                    break;
                case MessageBoxButtons.YesNo:
                    EnableButton(metroButton1);
                    metroButton1.Text = "Yes";
                    metroButton1.Location = metroButton2.Location;
                    metroButton1.Tag = DialogResult.Yes;
                    EnableButton(metroButton2);
                    metroButton2.Text = "No";
                    metroButton2.Location = metroButton3.Location;
                    metroButton2.Tag = DialogResult.No;
                    EnableButton(metroButton3, false);
                    break;
                case MessageBoxButtons.RetryCancel:
                    EnableButton(metroButton1);
                    metroButton1.Text = "Retry";
                    metroButton1.Location = metroButton2.Location;
                    metroButton1.Tag = DialogResult.Retry;
                    EnableButton(metroButton2);
                    metroButton2.Text = "Cancel";
                    metroButton2.Location = metroButton3.Location;
                    metroButton2.Tag = DialogResult.Cancel;
                    EnableButton(metroButton3, false);
                    break;
            }

            switch (Properties.Icon)
            {
                case MessageBoxIcon.Hand:
                    Body.BackColor = _errorColor;
                    break;
                case MessageBoxIcon.Question:
                    Body.BackColor = _question;
                    break;
                case MessageBoxIcon.Exclamation:
                    Body.BackColor = _warningColor;
                    break;
                case MessageBoxIcon.Asterisk:
                    Body.BackColor = _defaultColor;
                    break;
                default:
                    Body.BackColor = Color.DarkGray;
                    break;
            }
        }

        private void EnableButton(MetroButton button)
        {
            EnableButton(button, true);
        }

        private void EnableButton(MetroButton button, bool enabled)
        {
            button.Enabled = enabled;
            button.Visible = enabled;
        }

        public void SetDefaultButton()
        {
            switch (Properties.DefaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    if (metroButton1 == null || !metroButton1.Enabled)
                        break;
                    metroButton1.Focus();
                    break;
                case MessageBoxDefaultButton.Button2:
                    if (metroButton2 == null || !metroButton2.Enabled)
                        break;
                    metroButton2.Focus();
                    break;
                case MessageBoxDefaultButton.Button3:
                    if (metroButton3 == null || !metroButton3.Enabled)
                        break;
                    metroButton3.Focus();
                    break;
            }
        }

        private void button_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            StylizeButton((MetroButton)sender, true);
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            StylizeButton((MetroButton)sender);
        }

        private void StylizeButton(MetroButton button)
        {
            StylizeButton(button, false);
        }

        private void StylizeButton(MetroButton button, bool hovered)
        {
            button.Cursor = Cursors.Hand;
            button.MouseClick -= button_MouseClick;
            button.MouseClick += button_MouseClick;
            button.MouseEnter -= button_MouseEnter;
            button.MouseEnter += button_MouseEnter;
            button.MouseLeave -= button_MouseLeave;
            button.MouseLeave += button_MouseLeave;
            
            button.Theme = MetroThemeStyle.Dark;
            button.UseStyleColors = true;
            button.UseCustomBackColor = false;
            button.TabStop = false;
        }

        private void button_Click(object sender, EventArgs e)
        {
            var metroButton = (MetroButton)sender;
            if (!metroButton.Enabled)
                return;
            Result = (DialogResult)metroButton.Tag;
            Hide();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Body = new Panel();
            tlpBody = new TableLayoutPanel();
            messageLabel = new MetroLabel();
            titleLabel = new Label();
            metroButton1 = new MetroButton();
            metroButton3 = new MetroButton();
            metroButton2 = new MetroButton();
            pnlBottom = new Panel();
            Body.SuspendLayout();
            tlpBody.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            Body.BackColor = Color.DarkGray;
            Body.Controls.Add(tlpBody);
            Body.Dock = DockStyle.Fill;
            Body.Location = new Point(0, 0);
            Body.Margin = new Padding(0);
            Body.Name = "Body";
            Body.Size = new Size(804, 211);
            Body.TabIndex = 2;
            tlpBody.ColumnCount = 3;
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            tlpBody.Controls.Add(messageLabel, 1, 2);
            tlpBody.Controls.Add(titleLabel, 1, 1);
            tlpBody.Controls.Add(pnlBottom, 1, 3);
            tlpBody.Dock = DockStyle.Fill;
            tlpBody.Location = new Point(0, 0);
            tlpBody.Name = "tlpBody";
            tlpBody.RowCount = 4;
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 5f));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
            tlpBody.Size = new Size(804, 211);
            tlpBody.TabIndex = 6;
            messageLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            messageLabel.BackColor = Color.Transparent;
            messageLabel.ForeColor = Color.White;
            messageLabel.Location = new Point(83, 30);
            messageLabel.Margin = new Padding(3, 0, 0, 0);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(640, 141);
            messageLabel.WrapToLine = true;
            messageLabel.TabIndex = 0;
            messageLabel.Text = "message here";
            messageLabel.FontSize = MetroLabelSize.Tall;
            //messageLabel.UseStyleColors = true;
            messageLabel.UseCustomBackColor = true;
            titleLabel.AutoSize = true;
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Font = new Font("Segoe UI Semibold", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.WhiteSmoke;
            titleLabel.Location = new Point(80, 5);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(125, 25);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "message title";

            metroButton1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton1.BackColor = Color.FromArgb(34, 34, 34);
            metroButton1.FontWeight = MetroButtonWeight.Regular;
            metroButton1.Location = new Point(357, 1);
            metroButton1.Name = "metroButton1";
            metroButton1.Size = new Size(90, 26);
            metroButton1.TabIndex = 3;
            metroButton1.Text = "button 1";
            metroButton1.UseSelectable = true;

            metroButton3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton3.FontWeight = MetroButtonWeight.Regular;
            metroButton3.Location = new Point(553, 1);
            metroButton3.Name = "metroButton3";
            metroButton3.Size = new Size(90, 26);
            metroButton3.TabIndex = 5;
            metroButton3.Text = "button 3";
            metroButton3.UseSelectable = true;

            metroButton2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton2.FontWeight = MetroButtonWeight.Regular;
            metroButton2.Location = new Point(455, 1);
            metroButton2.Name = "metroButton2";
            metroButton2.Size = new Size(90, 26);
            metroButton2.TabIndex = 4;
            metroButton2.Text = "button 2";
            metroButton2.UseSelectable = true;

            pnlBottom.BackColor = Color.Transparent;
            pnlBottom.Controls.Add(metroButton2);
            pnlBottom.Controls.Add(metroButton1);
            pnlBottom.Controls.Add(metroButton3);
            pnlBottom.Dock = DockStyle.Fill;
            pnlBottom.Location = new Point(80, 171);
            pnlBottom.Margin = new Padding(0);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(643, 40);
            pnlBottom.TabIndex = 2;
            AutoScaleDimensions = new SizeF(8f, 21f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 211);
            ControlBox = false;
            Controls.Add(Body);
            Font = new Font("Segoe UI Light", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = nameof(MetroMessageBoxControl);
            StartPosition = FormStartPosition.Manual;
            Body.ResumeLayout(false);
            tlpBody.ResumeLayout(false);
            tlpBody.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}