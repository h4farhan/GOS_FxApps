namespace GOS_FxApps
{
    partial class formkamera
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.ControlBoxButton = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnya = new Guna.UI2.WinForms.Guna2Button();
            this.btnno = new Guna.UI2.WinForms.Guna2Button();
            this.btncapture = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBoxPreview = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnopenfile = new FontAwesome.Sharp.IconPictureBox();
            this.shadowform = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnopenfile)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.ControlBoxButton);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1132, 32);
            this.guna2Panel1.TabIndex = 0;
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
            // btnya
            // 
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
            this.btnya.Location = new System.Drawing.Point(664, 581);
            this.btnya.Name = "btnya";
            this.btnya.Size = new System.Drawing.Size(69, 49);
            this.btnya.TabIndex = 4;
            this.btnya.Visible = false;
            this.btnya.Click += new System.EventHandler(this.btnya_Click);
            // 
            // btnno
            // 
            this.btnno.BorderRadius = 20;
            this.btnno.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnno.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnno.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnno.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnno.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.btnno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnno.ForeColor = System.Drawing.Color.White;
            this.btnno.Image = global::GOS_FxApps.Properties.Resources.close;
            this.btnno.Location = new System.Drawing.Point(405, 581);
            this.btnno.Name = "btnno";
            this.btnno.Size = new System.Drawing.Size(69, 49);
            this.btnno.TabIndex = 3;
            this.btnno.Visible = false;
            this.btnno.Click += new System.EventHandler(this.btnno_Click);
            // 
            // btncapture
            // 
            this.btncapture.BorderRadius = 20;
            this.btncapture.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btncapture.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btncapture.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btncapture.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btncapture.Enabled = false;
            this.btncapture.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.btncapture.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btncapture.ForeColor = System.Drawing.Color.White;
            this.btncapture.Image = global::GOS_FxApps.Properties.Resources.photo_camera_interface_symbol_for_button;
            this.btncapture.ImageSize = new System.Drawing.Size(30, 30);
            this.btncapture.Location = new System.Drawing.Point(497, 578);
            this.btncapture.Name = "btncapture";
            this.btncapture.Size = new System.Drawing.Size(143, 54);
            this.btncapture.TabIndex = 2;
            this.btncapture.Click += new System.EventHandler(this.btncapture_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderRadius = 10;
            this.pictureBoxPreview.ImageRotate = 0F;
            this.pictureBoxPreview.Location = new System.Drawing.Point(20, 47);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(1096, 517);
            this.pictureBoxPreview.TabIndex = 1;
            this.pictureBoxPreview.TabStop = false;
            // 
            // btnopenfile
            // 
            this.btnopenfile.BackColor = System.Drawing.Color.Transparent;
            this.btnopenfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnopenfile.ForeColor = System.Drawing.Color.Black;
            this.btnopenfile.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            this.btnopenfile.IconColor = System.Drawing.Color.Black;
            this.btnopenfile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnopenfile.IconSize = 44;
            this.btnopenfile.Location = new System.Drawing.Point(20, 583);
            this.btnopenfile.Name = "btnopenfile";
            this.btnopenfile.Size = new System.Drawing.Size(44, 44);
            this.btnopenfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnopenfile.TabIndex = 48;
            this.btnopenfile.TabStop = false;
            this.btnopenfile.Click += new System.EventHandler(this.btnopenfile_Click);
            // 
            // shadowform
            // 
            this.shadowform.BorderRadius = 20;
            this.shadowform.ContainerControl = this;
            this.shadowform.DockIndicatorTransparencyValue = 0.6D;
            this.shadowform.TransparentWhileDrag = true;
            // 
            // formkamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 647);
            this.Controls.Add(this.btnopenfile);
            this.Controls.Add(this.btnya);
            this.Controls.Add(this.btnno);
            this.Controls.Add(this.btncapture);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formkamera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formkamera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formkamera_FormClosing);
            this.Load += new System.EventHandler(this.formkamera_Load);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnopenfile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2PictureBox pictureBoxPreview;
        private Guna.UI2.WinForms.Guna2Button btncapture;
        private Guna.UI2.WinForms.Guna2Button btnno;
        private Guna.UI2.WinForms.Guna2Button btnya;
        private FontAwesome.Sharp.IconPictureBox btnopenfile;
        private Guna.UI2.WinForms.Guna2ControlBox ControlBoxButton;
        private Guna.UI2.WinForms.Guna2BorderlessForm shadowform;
    }
}