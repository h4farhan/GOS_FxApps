namespace GOS_FxApps
{
    partial class printpenerimaan
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbpilihdata = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.paneldata = new Guna.UI2.WinForms.Guna2Panel();
            this.datecari = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cbShift = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txttim = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnprint = new Guna.UI2.WinForms.Guna2Button();
            this.btncari = new Guna.UI2.WinForms.Guna2Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.paneldata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbpilihdata);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 70);
            this.panel1.TabIndex = 1;
            // 
            // cmbpilihdata
            // 
            this.cmbpilihdata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbpilihdata.BackColor = System.Drawing.Color.Transparent;
            this.cmbpilihdata.BorderRadius = 5;
            this.cmbpilihdata.BorderThickness = 2;
            this.cmbpilihdata.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbpilihdata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpilihdata.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.cmbpilihdata.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbpilihdata.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbpilihdata.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbpilihdata.ForeColor = System.Drawing.Color.White;
            this.cmbpilihdata.ItemHeight = 30;
            this.cmbpilihdata.Items.AddRange(new object[] {
            "Penerimaan",
            "Perbaikan",
            "Pengiriman",
            "Welding Pieces",
            "Pemakaian Material"});
            this.cmbpilihdata.Location = new System.Drawing.Point(1027, 16);
            this.cmbpilihdata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbpilihdata.Name = "cmbpilihdata";
            this.cmbpilihdata.Size = new System.Drawing.Size(286, 36);
            this.cmbpilihdata.TabIndex = 57;
            this.cmbpilihdata.SelectedIndexChanged += new System.EventHandler(this.cmbpilihdata_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.label3.Location = new System.Drawing.Point(916, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 56;
            this.label3.Text = "Pilih Data";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(117, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Form Penerimaan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Laporan >";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 70);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1344, 1030);
            this.panel3.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel2.Controls.Add(this.guna2Panel4, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1344, 1030);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.guna2Panel4.BorderRadius = 15;
            this.guna2Panel4.BorderThickness = 2;
            this.guna2Panel4.Controls.Add(this.paneldata);
            this.guna2Panel4.Controls.Add(this.btnprint);
            this.guna2Panel4.Controls.Add(this.btncari);
            this.guna2Panel4.Controls.Add(this.dataGridView1);
            this.guna2Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel4.Location = new System.Drawing.Point(14, 2);
            this.guna2Panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Padding = new System.Windows.Forms.Padding(15);
            this.guna2Panel4.Size = new System.Drawing.Size(1316, 1019);
            this.guna2Panel4.TabIndex = 36;
            // 
            // paneldata
            // 
            this.paneldata.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.paneldata.BorderRadius = 15;
            this.paneldata.BorderThickness = 2;
            this.paneldata.Controls.Add(this.datecari);
            this.paneldata.Controls.Add(this.cbShift);
            this.paneldata.Controls.Add(this.label7);
            this.paneldata.Controls.Add(this.label8);
            this.paneldata.Controls.Add(this.txttim);
            this.paneldata.Location = new System.Drawing.Point(18, 18);
            this.paneldata.Name = "paneldata";
            this.paneldata.Size = new System.Drawing.Size(622, 90);
            this.paneldata.TabIndex = 56;
            this.paneldata.Visible = false;
            // 
            // datecari
            // 
            this.datecari.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.datecari.BorderRadius = 5;
            this.datecari.BorderThickness = 2;
            this.datecari.Checked = true;
            this.datecari.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.datecari.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.datecari.ForeColor = System.Drawing.Color.White;
            this.datecari.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.datecari.Location = new System.Drawing.Point(23, 20);
            this.datecari.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datecari.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datecari.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datecari.Name = "datecari";
            this.datecari.ShowCheckBox = true;
            this.datecari.Size = new System.Drawing.Size(205, 50);
            this.datecari.TabIndex = 46;
            this.datecari.Value = new System.DateTime(2025, 5, 20, 10, 10, 59, 90);
            // 
            // cbShift
            // 
            this.cbShift.BackColor = System.Drawing.Color.Transparent;
            this.cbShift.BorderRadius = 5;
            this.cbShift.BorderThickness = 2;
            this.cbShift.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShift.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.cbShift.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbShift.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbShift.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbShift.ForeColor = System.Drawing.Color.White;
            this.cbShift.ItemHeight = 30;
            this.cbShift.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbShift.Location = new System.Drawing.Point(309, 27);
            this.cbShift.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(113, 36);
            this.cbShift.TabIndex = 55;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.label7.Location = new System.Drawing.Point(243, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 25);
            this.label7.TabIndex = 49;
            this.label7.Text = "Shift :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.label8.Location = new System.Drawing.Point(440, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 25);
            this.label8.TabIndex = 50;
            this.label8.Text = "Tim :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txttim
            // 
            this.txttim.BorderRadius = 5;
            this.txttim.BorderThickness = 2;
            this.txttim.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txttim.DefaultText = "";
            this.txttim.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txttim.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txttim.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txttim.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txttim.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.txttim.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttim.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txttim.ForeColor = System.Drawing.Color.White;
            this.txttim.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttim.Location = new System.Drawing.Point(501, 27);
            this.txttim.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txttim.Name = "txttim";
            this.txttim.PlaceholderText = "Axxxx";
            this.txttim.SelectedText = "";
            this.txttim.Size = new System.Drawing.Size(95, 36);
            this.txttim.TabIndex = 51;
            this.txttim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HurufOnly_KeyPress);
            // 
            // btnprint
            // 
            this.btnprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnprint.BorderRadius = 8;
            this.btnprint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnprint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnprint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnprint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnprint.Enabled = false;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(1191, 40);
            this.btnprint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(108, 47);
            this.btnprint.TabIndex = 47;
            this.btnprint.Text = "Print Data";
            this.btnprint.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // btncari
            // 
            this.btncari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncari.BorderRadius = 8;
            this.btncari.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btncari.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btncari.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btncari.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btncari.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btncari.ForeColor = System.Drawing.Color.White;
            this.btncari.Location = new System.Drawing.Point(1087, 40);
            this.btncari.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btncari.Name = "btncari";
            this.btncari.Size = new System.Drawing.Size(97, 47);
            this.btncari.TabIndex = 44;
            this.btncari.Text = "Cari";
            this.btncari.Click += new System.EventHandler(this.btncari_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.Gray;
            this.dataGridView1.Location = new System.Drawing.Point(15, 125);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1284, 884);
            this.dataGridView1.TabIndex = 1;
            // 
            // printpenerimaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(1344, 1100);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "printpenerimaan";
            this.Text = "printpenerimaan";
            this.Load += new System.EventHandler(this.printpenerimaan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            this.paneldata.ResumeLayout(false);
            this.paneldata.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Guna.UI2.WinForms.Guna2Button btncari;
        private Guna.UI2.WinForms.Guna2DateTimePicker datecari;
        private Guna.UI2.WinForms.Guna2TextBox txttim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2Button btnprint;
        private Guna.UI2.WinForms.Guna2ComboBox cbShift;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2Panel paneldata;
        private Guna.UI2.WinForms.Guna2ComboBox cmbpilihdata;
        private System.Windows.Forms.Label label3;
    }
}