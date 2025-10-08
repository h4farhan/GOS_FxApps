namespace GOS_FxApps
{
    partial class aturjam
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
            this.date = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cbjam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.shadowform = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.btnset = new Guna.UI2.WinForms.Guna2Button();
            this.btncancel = new Guna.UI2.WinForms.Guna2Button();
            this.cbmenit = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnreset = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // date
            // 
            this.date.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.date.BorderRadius = 5;
            this.date.BorderThickness = 2;
            this.date.Checked = true;
            this.date.FillColor = System.Drawing.Color.White;
            this.date.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.date.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.date.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.date.Location = new System.Drawing.Point(36, 108);
            this.date.Margin = new System.Windows.Forms.Padding(2);
            this.date.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.date.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(389, 41);
            this.date.TabIndex = 47;
            this.date.Value = new System.DateTime(2025, 5, 20, 10, 10, 59, 90);
            // 
            // cbjam
            // 
            this.cbjam.BackColor = System.Drawing.Color.Transparent;
            this.cbjam.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbjam.BorderRadius = 5;
            this.cbjam.BorderThickness = 2;
            this.cbjam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbjam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbjam.FillColor = System.Drawing.Color.Gainsboro;
            this.cbjam.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbjam.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbjam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbjam.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbjam.ItemHeight = 30;
            this.cbjam.Location = new System.Drawing.Point(36, 200);
            this.cbjam.Margin = new System.Windows.Forms.Padding(2);
            this.cbjam.Name = "cbjam";
            this.cbjam.Size = new System.Drawing.Size(125, 36);
            this.cbjam.StartIndex = 0;
            this.cbjam.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(31, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 28);
            this.label2.TabIndex = 59;
            this.label2.Text = "Atur Tanggal dan Shift";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(32, 76);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 21);
            this.label6.TabIndex = 60;
            this.label6.Text = "Tanggal";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(32, 167);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 21);
            this.label1.TabIndex = 61;
            this.label1.Text = "Jam";
            // 
            // shadowform
            // 
            this.shadowform.BorderRadius = 20;
            this.shadowform.ContainerControl = this;
            this.shadowform.DockIndicatorTransparencyValue = 0.7D;
            this.shadowform.TransparentWhileDrag = true;
            // 
            // btnset
            // 
            this.btnset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnset.BorderRadius = 8;
            this.btnset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.btnset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnset.ForeColor = System.Drawing.Color.White;
            this.btnset.Location = new System.Drawing.Point(419, 282);
            this.btnset.Margin = new System.Windows.Forms.Padding(2);
            this.btnset.Name = "btnset";
            this.btnset.Size = new System.Drawing.Size(116, 38);
            this.btnset.TabIndex = 63;
            this.btnset.Text = "Set";
            this.btnset.Click += new System.EventHandler(this.btnset_Click);
            // 
            // btncancel
            // 
            this.btncancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncancel.BorderRadius = 8;
            this.btncancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btncancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btncancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btncancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btncancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btncancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(307, 282);
            this.btncancel.Margin = new System.Windows.Forms.Padding(2);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(108, 38);
            this.btncancel.TabIndex = 62;
            this.btncancel.Text = "Cancel";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // cbmenit
            // 
            this.cbmenit.BackColor = System.Drawing.Color.Transparent;
            this.cbmenit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbmenit.BorderRadius = 5;
            this.cbmenit.BorderThickness = 2;
            this.cbmenit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbmenit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmenit.FillColor = System.Drawing.Color.Gainsboro;
            this.cbmenit.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbmenit.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbmenit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbmenit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbmenit.ItemHeight = 30;
            this.cbmenit.Location = new System.Drawing.Point(174, 200);
            this.cbmenit.Margin = new System.Windows.Forms.Padding(2);
            this.cbmenit.Name = "cbmenit";
            this.cbmenit.Size = new System.Drawing.Size(125, 36);
            this.cbmenit.StartIndex = 0;
            this.cbmenit.TabIndex = 64;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(170, 167);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 21);
            this.label3.TabIndex = 65;
            this.label3.Text = "Menit";
            // 
            // btnreset
            // 
            this.btnreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnreset.BorderRadius = 8;
            this.btnreset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnreset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnreset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnreset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnreset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnreset.ForeColor = System.Drawing.Color.White;
            this.btnreset.Location = new System.Drawing.Point(36, 282);
            this.btnreset.Margin = new System.Windows.Forms.Padding(2);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(108, 38);
            this.btnreset.TabIndex = 66;
            this.btnreset.Text = "Reset";
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            // 
            // aturjam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(568, 347);
            this.Controls.Add(this.btnreset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbmenit);
            this.Controls.Add(this.btnset);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbjam);
            this.Controls.Add(this.date);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "aturjam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "aturjam";
            this.Load += new System.EventHandler(this.aturjam_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker date;
        private Guna.UI2.WinForms.Guna2ComboBox cbjam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2BorderlessForm shadowform;
        private Guna.UI2.WinForms.Guna2Button btnset;
        private Guna.UI2.WinForms.Guna2Button btncancel;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox cbmenit;
        private Guna.UI2.WinForms.Guna2Button btnreset;
    }
}