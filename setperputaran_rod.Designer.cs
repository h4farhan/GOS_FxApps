namespace GOS_FxApps
{
    partial class setperputaran_rod
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
            this.shadowform = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblhari = new System.Windows.Forms.Label();
            this.txthari = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnedit = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.ControlBoxButton = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shadowform
            // 
            this.shadowform.BorderRadius = 20;
            this.shadowform.ContainerControl = this;
            this.shadowform.DockIndicatorTransparencyValue = 0.6D;
            this.shadowform.TransparentWhileDrag = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(699, 67);
            this.label1.TabIndex = 0;
            this.label1.Text = "Perputaran ROD ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblhari
            // 
            this.lblhari.AutoSize = true;
            this.lblhari.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblhari.Font = new System.Drawing.Font("Segoe UI Semibold", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhari.Location = new System.Drawing.Point(3, 67);
            this.lblhari.Name = "lblhari";
            this.lblhari.Size = new System.Drawing.Size(699, 105);
            this.lblhari.TabIndex = 1;
            this.lblhari.Text = "- Hari";
            this.lblhari.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txthari
            // 
            this.txthari.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txthari.BackColor = System.Drawing.Color.Transparent;
            this.txthari.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txthari.BorderRadius = 5;
            this.txthari.BorderThickness = 2;
            this.txthari.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txthari.DefaultText = "";
            this.txthari.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txthari.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txthari.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txthari.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txthari.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txthari.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txthari.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txthari.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txthari.Location = new System.Drawing.Point(28, 234);
            this.txthari.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txthari.Name = "txthari";
            this.txthari.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txthari.PlaceholderText = "Masukkan Jumlah Hari";
            this.txthari.SelectedText = "";
            this.txthari.Size = new System.Drawing.Size(490, 41);
            this.txthari.TabIndex = 2;
            this.txthari.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.txthari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            // 
            // btnedit
            // 
            this.btnedit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnedit.BorderRadius = 8;
            this.btnedit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnedit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnedit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnedit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnedit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnedit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnedit.ForeColor = System.Drawing.Color.White;
            this.btnedit.Location = new System.Drawing.Point(522, 234);
            this.btnedit.Margin = new System.Windows.Forms.Padding(2);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(155, 41);
            this.btnedit.TabIndex = 3;
            this.btnedit.Text = "Edit Data";
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.ControlBoxButton);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(705, 32);
            this.guna2Panel1.TabIndex = 4;
            // 
            // ControlBoxButton
            // 
            this.ControlBoxButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlBoxButton.FillColor = System.Drawing.Color.Transparent;
            this.ControlBoxButton.IconColor = System.Drawing.Color.Red;
            this.ControlBoxButton.Location = new System.Drawing.Point(669, 0);
            this.ControlBoxButton.Margin = new System.Windows.Forms.Padding(2);
            this.ControlBoxButton.Name = "ControlBoxButton";
            this.ControlBoxButton.Size = new System.Drawing.Size(36, 32);
            this.ControlBoxButton.TabIndex = 2;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Separator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Separator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2Separator1.FillThickness = 2;
            this.guna2Separator1.Location = new System.Drawing.Point(0, 32);
            this.guna2Separator1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(705, 8);
            this.guna2Separator1.TabIndex = 62;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblhari, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 40);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.95349F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.04651F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(705, 172);
            this.tableLayoutPanel1.TabIndex = 63;
            // 
            // setperputaran_rod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 346);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnedit);
            this.Controls.Add(this.txthari);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "setperputaran_rod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "setperputaran_rod";
            this.Load += new System.EventHandler(this.setperputaran_rod_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm shadowform;
        private System.Windows.Forms.Label lblhari;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txthari;
        private Guna.UI2.WinForms.Guna2Button btnedit;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ControlBox ControlBoxButton;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}