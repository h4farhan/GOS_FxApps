namespace GOS_FxApps
{
    partial class formdialog
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.ControlBoxButton = new Guna.UI2.WinForms.Guna2ControlBox();
            this.pictureBoxPreview = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnya = new Guna.UI2.WinForms.Guna2Button();
            this.btnno = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.ControlBoxButton);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1132, 32);
            this.guna2Panel1.TabIndex = 1;
            // 
            // ControlBoxButton
            // 
            this.ControlBoxButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlBoxButton.FillColor = System.Drawing.Color.Transparent;
            this.ControlBoxButton.IconColor = System.Drawing.Color.Red;
            this.ControlBoxButton.Location = new System.Drawing.Point(1096, 0);
            this.ControlBoxButton.Margin = new System.Windows.Forms.Padding(2);
            this.ControlBoxButton.Name = "ControlBoxButton";
            this.ControlBoxButton.Size = new System.Drawing.Size(36, 32);
            this.ControlBoxButton.TabIndex = 2;
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderRadius = 10;
            this.pictureBoxPreview.ImageRotate = 0F;
            this.pictureBoxPreview.Location = new System.Drawing.Point(18, 47);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(1096, 517);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPreview.TabIndex = 2;
            this.pictureBoxPreview.TabStop = false;
            // 
            // btnya
            // 
            this.btnya.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnya.BorderRadius = 20;
            this.btnya.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnya.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnya.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnya.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnya.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.btnya.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnya.ForeColor = System.Drawing.Color.White;
            this.btnya.Image = global::GOS_FxApps.Properties.Resources.check;
            this.btnya.ImageSize = new System.Drawing.Size(30, 30);
            this.btnya.Location = new System.Drawing.Point(1045, 580);
            this.btnya.Name = "btnya";
            this.btnya.Size = new System.Drawing.Size(69, 49);
            this.btnya.TabIndex = 51;
            this.btnya.Visible = false;
            this.btnya.Click += new System.EventHandler(this.btnya_Click);
            // 
            // btnno
            // 
            this.btnno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnno.BorderRadius = 20;
            this.btnno.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnno.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnno.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnno.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnno.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.btnno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnno.ForeColor = System.Drawing.Color.White;
            this.btnno.Image = global::GOS_FxApps.Properties.Resources.close;
            this.btnno.Location = new System.Drawing.Point(970, 580);
            this.btnno.Name = "btnno";
            this.btnno.Size = new System.Drawing.Size(69, 49);
            this.btnno.TabIndex = 50;
            this.btnno.Visible = false;
            this.btnno.Click += new System.EventHandler(this.btnno_Click);
            // 
            // formdialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 647);
            this.Controls.Add(this.btnya);
            this.Controls.Add(this.btnno);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formdialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formdialog";
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ControlBox ControlBoxButton;
        public Guna.UI2.WinForms.Guna2PictureBox pictureBoxPreview;
        public Guna.UI2.WinForms.Guna2Button btnya;
        public Guna.UI2.WinForms.Guna2Button btnno;
    }
}