namespace GOS_FxApps
{
    partial class formnotifikasi
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
            this.panelNotif = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.shadowform = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.btntiga = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // panelNotif
            // 
            this.panelNotif.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNotif.AutoScroll = true;
            this.panelNotif.BackColor = System.Drawing.Color.Transparent;
            this.panelNotif.Location = new System.Drawing.Point(17, 36);
            this.panelNotif.Name = "panelNotif";
            this.panelNotif.Size = new System.Drawing.Size(289, 467);
            this.panelNotif.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Notifikasi";
            // 
            // shadowform
            // 
            this.shadowform.BorderRadius = 5;
            this.shadowform.ContainerControl = this;
            this.shadowform.DockIndicatorTransparencyValue = 0.6D;
            this.shadowform.TransparentWhileDrag = true;
            // 
            // btntiga
            // 
            this.btntiga.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btntiga.BorderRadius = 10;
            this.btntiga.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btntiga.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btntiga.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btntiga.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btntiga.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btntiga.FillColor = System.Drawing.Color.Transparent;
            this.btntiga.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btntiga.ForeColor = System.Drawing.Color.White;
            this.btntiga.Image = global::GOS_FxApps.Properties.Resources.dots;
            this.btntiga.ImageSize = new System.Drawing.Size(30, 30);
            this.btntiga.Location = new System.Drawing.Point(261, 8);
            this.btntiga.Name = "btntiga";
            this.btntiga.PressedColor = System.Drawing.Color.Transparent;
            this.btntiga.Size = new System.Drawing.Size(45, 22);
            this.btntiga.TabIndex = 7;
            this.btntiga.Visible = false;
            this.btntiga.Click += new System.EventHandler(this.btntiga_Click);
            // 
            // formnotifikasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(324, 515);
            this.Controls.Add(this.btntiga);
            this.Controls.Add(this.panelNotif);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formnotifikasi";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "formnotifikasi";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formnotifikasi_FormClosing);
            this.Load += new System.EventHandler(this.formnotifikasi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelNotif;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2BorderlessForm shadowform;
        public Guna.UI2.WinForms.Guna2Button btntiga;
    }
}