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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbpilihdata = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.paneldata2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.datecari = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cbShift = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txttim = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelbukti = new Guna.UI2.WinForms.Guna2Panel();
            this.lbljumlahbukti = new System.Windows.Forms.Label();
            this.datecaribukti = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.shiftbukti = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panelsummary = new Guna.UI2.WinForms.Guna2Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.tanggalAkhir = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lbljumlahsummary = new System.Windows.Forms.Label();
            this.tanggalMulai = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnprint = new Guna.UI2.WinForms.Guna2Button();
            this.btnreset = new Guna.UI2.WinForms.Guna2Button();
            this.paneldata3 = new Guna.UI2.WinForms.Guna2Panel();
            this.txtcarimaterial = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbnamamaterial = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbljumlahdatamaterial = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tanggalAkhirmaterial = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.tanggalMulaimaterial = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.paneldata2.SuspendLayout();
            this.panelbukti.SuspendLayout();
            this.panelsummary.SuspendLayout();
            this.paneldata3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.cmbpilihdata);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1147, 57);
            this.panel1.TabIndex = 1;
            // 
            // cmbpilihdata
            // 
            this.cmbpilihdata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbpilihdata.BackColor = System.Drawing.Color.Transparent;
            this.cmbpilihdata.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbpilihdata.BorderRadius = 5;
            this.cmbpilihdata.BorderThickness = 2;
            this.cmbpilihdata.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbpilihdata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpilihdata.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbpilihdata.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbpilihdata.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbpilihdata.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmbpilihdata.ItemHeight = 30;
            this.cmbpilihdata.Items.AddRange(new object[] {
            "Penerimaan",
            "Perbaikan",
            "Pengiriman",
            "Welding Pieces (Detail Shift)",
            "Welding Pieces (Rekap Harian)",
            "Hasil Produksi & Pemakaian Material",
            "Summary Data for Anode ROD Repair",
            "Actual Quantity for Repaired ROD Assy",
            "Kondisi ROD Reject di Rod Repair Shop",
            "Actual Consumption Of Material & Part",
            "Kartu Stock Material",
            "Bukti Perubahan"});
            this.cmbpilihdata.Location = new System.Drawing.Point(831, 13);
            this.cmbpilihdata.Margin = new System.Windows.Forms.Padding(2);
            this.cmbpilihdata.Name = "cmbpilihdata";
            this.cmbpilihdata.Size = new System.Drawing.Size(294, 36);
            this.cmbpilihdata.TabIndex = 57;
            this.cmbpilihdata.SelectedIndexChanged += new System.EventHandler(this.cmbpilihdata_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(744, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 56;
            this.label3.Text = "Pilih Data";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(11, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Form Laporan";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 57);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1147, 837);
            this.panel3.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel2.Controls.Add(this.guna2Panel4, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1147, 837);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel4.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel4.BorderRadius = 15;
            this.guna2Panel4.BorderThickness = 2;
            this.guna2Panel4.Controls.Add(this.paneldata2);
            this.guna2Panel4.Controls.Add(this.panelbukti);
            this.guna2Panel4.Controls.Add(this.panelsummary);
            this.guna2Panel4.Controls.Add(this.reportViewer1);
            this.guna2Panel4.Controls.Add(this.btnprint);
            this.guna2Panel4.Controls.Add(this.btnreset);
            this.guna2Panel4.Controls.Add(this.paneldata3);
            this.guna2Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel4.FillColor = System.Drawing.Color.White;
            this.guna2Panel4.Location = new System.Drawing.Point(10, 2);
            this.guna2Panel4.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.guna2Panel4.Size = new System.Drawing.Size(1127, 827);
            this.guna2Panel4.TabIndex = 36;
            // 
            // paneldata2
            // 
            this.paneldata2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paneldata2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.paneldata2.BorderRadius = 15;
            this.paneldata2.Controls.Add(this.label4);
            this.paneldata2.Controls.Add(this.datecari);
            this.paneldata2.Controls.Add(this.cbShift);
            this.paneldata2.Controls.Add(this.label7);
            this.paneldata2.Controls.Add(this.label8);
            this.paneldata2.Controls.Add(this.txttim);
            this.paneldata2.Location = new System.Drawing.Point(11, 25);
            this.paneldata2.Margin = new System.Windows.Forms.Padding(2);
            this.paneldata2.Name = "paneldata2";
            this.paneldata2.Size = new System.Drawing.Size(887, 73);
            this.paneldata2.TabIndex = 56;
            this.paneldata2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(545, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 20);
            this.label4.TabIndex = 56;
            this.label4.Text = "Jumlah Data : 0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // datecari
            // 
            this.datecari.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.datecari.BorderRadius = 5;
            this.datecari.BorderThickness = 2;
            this.datecari.Checked = true;
            this.datecari.FillColor = System.Drawing.Color.White;
            this.datecari.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.datecari.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.datecari.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.datecari.Location = new System.Drawing.Point(10, 20);
            this.datecari.Margin = new System.Windows.Forms.Padding(2);
            this.datecari.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datecari.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datecari.Name = "datecari";
            this.datecari.Size = new System.Drawing.Size(261, 41);
            this.datecari.TabIndex = 46;
            this.datecari.Value = new System.DateTime(2025, 5, 20, 10, 10, 59, 90);
            // 
            // cbShift
            // 
            this.cbShift.BackColor = System.Drawing.Color.Transparent;
            this.cbShift.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbShift.BorderRadius = 5;
            this.cbShift.BorderThickness = 2;
            this.cbShift.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShift.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbShift.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbShift.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbShift.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbShift.ItemHeight = 30;
            this.cbShift.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbShift.Location = new System.Drawing.Point(326, 22);
            this.cbShift.Margin = new System.Windows.Forms.Padding(2);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(86, 36);
            this.cbShift.TabIndex = 55;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(276, 30);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 49;
            this.label7.Text = "Shift :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(416, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 20);
            this.label8.TabIndex = 50;
            this.label8.Text = "Tim :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txttim
            // 
            this.txttim.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txttim.BorderRadius = 4;
            this.txttim.BorderThickness = 2;
            this.txttim.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txttim.DefaultText = "";
            this.txttim.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txttim.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txttim.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txttim.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txttim.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttim.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txttim.ForeColor = System.Drawing.Color.Black;
            this.txttim.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttim.Location = new System.Drawing.Point(462, 26);
            this.txttim.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txttim.Name = "txttim";
            this.txttim.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txttim.PlaceholderText = "Axxxx";
            this.txttim.SelectedText = "";
            this.txttim.Size = new System.Drawing.Size(71, 29);
            this.txttim.TabIndex = 51;
            this.txttim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HurufOnly_KeyPress);
            // 
            // panelbukti
            // 
            this.panelbukti.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelbukti.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.panelbukti.BorderRadius = 15;
            this.panelbukti.Controls.Add(this.lbljumlahbukti);
            this.panelbukti.Controls.Add(this.datecaribukti);
            this.panelbukti.Controls.Add(this.shiftbukti);
            this.panelbukti.Controls.Add(this.label6);
            this.panelbukti.Location = new System.Drawing.Point(11, 25);
            this.panelbukti.Margin = new System.Windows.Forms.Padding(2);
            this.panelbukti.Name = "panelbukti";
            this.panelbukti.Size = new System.Drawing.Size(887, 73);
            this.panelbukti.TabIndex = 57;
            this.panelbukti.Visible = false;
            // 
            // lbljumlahbukti
            // 
            this.lbljumlahbukti.AutoSize = true;
            this.lbljumlahbukti.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbljumlahbukti.ForeColor = System.Drawing.Color.Black;
            this.lbljumlahbukti.Location = new System.Drawing.Point(433, 30);
            this.lbljumlahbukti.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbljumlahbukti.Name = "lbljumlahbukti";
            this.lbljumlahbukti.Size = new System.Drawing.Size(118, 20);
            this.lbljumlahbukti.TabIndex = 56;
            this.lbljumlahbukti.Text = "Jumlah Data : 0";
            this.lbljumlahbukti.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // datecaribukti
            // 
            this.datecaribukti.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.datecaribukti.BorderRadius = 5;
            this.datecaribukti.BorderThickness = 2;
            this.datecaribukti.Checked = true;
            this.datecaribukti.FillColor = System.Drawing.Color.White;
            this.datecaribukti.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.datecaribukti.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.datecaribukti.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.datecaribukti.Location = new System.Drawing.Point(10, 20);
            this.datecaribukti.Margin = new System.Windows.Forms.Padding(2);
            this.datecaribukti.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datecaribukti.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datecaribukti.Name = "datecaribukti";
            this.datecaribukti.Size = new System.Drawing.Size(261, 41);
            this.datecaribukti.TabIndex = 46;
            this.datecaribukti.Value = new System.DateTime(2025, 5, 20, 10, 10, 59, 90);
            // 
            // shiftbukti
            // 
            this.shiftbukti.BackColor = System.Drawing.Color.Transparent;
            this.shiftbukti.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.shiftbukti.BorderRadius = 5;
            this.shiftbukti.BorderThickness = 2;
            this.shiftbukti.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.shiftbukti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shiftbukti.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.shiftbukti.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.shiftbukti.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.shiftbukti.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.shiftbukti.ItemHeight = 30;
            this.shiftbukti.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.shiftbukti.Location = new System.Drawing.Point(327, 22);
            this.shiftbukti.Margin = new System.Windows.Forms.Padding(2);
            this.shiftbukti.Name = "shiftbukti";
            this.shiftbukti.Size = new System.Drawing.Size(86, 36);
            this.shiftbukti.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(277, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 49;
            this.label6.Text = "Shift :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelsummary
            // 
            this.panelsummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelsummary.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.panelsummary.BorderRadius = 15;
            this.panelsummary.Controls.Add(this.label9);
            this.panelsummary.Controls.Add(this.tanggalAkhir);
            this.panelsummary.Controls.Add(this.lbljumlahsummary);
            this.panelsummary.Controls.Add(this.tanggalMulai);
            this.panelsummary.Location = new System.Drawing.Point(11, 25);
            this.panelsummary.Margin = new System.Windows.Forms.Padding(2);
            this.panelsummary.Name = "panelsummary";
            this.panelsummary.Size = new System.Drawing.Size(887, 73);
            this.panelsummary.TabIndex = 60;
            this.panelsummary.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(264, 30);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 20);
            this.label9.TabIndex = 58;
            this.label9.Text = "s/d";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tanggalAkhir
            // 
            this.tanggalAkhir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tanggalAkhir.BorderRadius = 5;
            this.tanggalAkhir.BorderThickness = 2;
            this.tanggalAkhir.Checked = true;
            this.tanggalAkhir.CustomFormat = "MM/yyyy";
            this.tanggalAkhir.FillColor = System.Drawing.Color.White;
            this.tanggalAkhir.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tanggalAkhir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tanggalAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.tanggalAkhir.Location = new System.Drawing.Point(300, 20);
            this.tanggalAkhir.Margin = new System.Windows.Forms.Padding(2);
            this.tanggalAkhir.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.tanggalAkhir.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tanggalAkhir.Name = "tanggalAkhir";
            this.tanggalAkhir.Size = new System.Drawing.Size(250, 41);
            this.tanggalAkhir.TabIndex = 59;
            this.tanggalAkhir.Value = new System.DateTime(2025, 5, 20, 0, 0, 0, 0);
            // 
            // lbljumlahsummary
            // 
            this.lbljumlahsummary.AutoSize = true;
            this.lbljumlahsummary.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbljumlahsummary.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbljumlahsummary.Location = new System.Drawing.Point(555, 30);
            this.lbljumlahsummary.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbljumlahsummary.Name = "lbljumlahsummary";
            this.lbljumlahsummary.Size = new System.Drawing.Size(118, 20);
            this.lbljumlahsummary.TabIndex = 58;
            this.lbljumlahsummary.Text = "Jumlah Data : 0";
            this.lbljumlahsummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tanggalMulai
            // 
            this.tanggalMulai.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tanggalMulai.BorderRadius = 5;
            this.tanggalMulai.BorderThickness = 2;
            this.tanggalMulai.Checked = true;
            this.tanggalMulai.CustomFormat = "MM/yyyy";
            this.tanggalMulai.FillColor = System.Drawing.Color.White;
            this.tanggalMulai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tanggalMulai.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tanggalMulai.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.tanggalMulai.Location = new System.Drawing.Point(10, 20);
            this.tanggalMulai.Margin = new System.Windows.Forms.Padding(2);
            this.tanggalMulai.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.tanggalMulai.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tanggalMulai.Name = "tanggalMulai";
            this.tanggalMulai.Size = new System.Drawing.Size(250, 41);
            this.tanggalMulai.TabIndex = 57;
            this.tanggalMulai.Value = new System.DateTime(2025, 5, 20, 0, 0, 0, 0);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.Location = new System.Drawing.Point(11, 103);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1102, 709);
            this.reportViewer1.TabIndex = 60;
            // 
            // btnprint
            // 
            this.btnprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnprint.BorderRadius = 8;
            this.btnprint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnprint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnprint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnprint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnprint.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(902, 42);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(116, 38);
            this.btnprint.TabIndex = 47;
            this.btnprint.Text = "Print Data";
            this.btnprint.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // btnreset
            // 
            this.btnreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnreset.BorderRadius = 8;
            this.btnreset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnreset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnreset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnreset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnreset.Enabled = false;
            this.btnreset.FillColor = System.Drawing.Color.Red;
            this.btnreset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnreset.ForeColor = System.Drawing.Color.White;
            this.btnreset.Location = new System.Drawing.Point(1021, 42);
            this.btnreset.Margin = new System.Windows.Forms.Padding(2);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(92, 38);
            this.btnreset.TabIndex = 44;
            this.btnreset.Text = "Reset";
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            // 
            // paneldata3
            // 
            this.paneldata3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paneldata3.AutoScroll = true;
            this.paneldata3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.paneldata3.BorderRadius = 15;
            this.paneldata3.Controls.Add(this.label5);
            this.paneldata3.Controls.Add(this.tanggalAkhirmaterial);
            this.paneldata3.Controls.Add(this.tanggalMulaimaterial);
            this.paneldata3.Controls.Add(this.txtcarimaterial);
            this.paneldata3.Controls.Add(this.label1);
            this.paneldata3.Controls.Add(this.cmbnamamaterial);
            this.paneldata3.Controls.Add(this.lbljumlahdatamaterial);
            this.paneldata3.Location = new System.Drawing.Point(11, 25);
            this.paneldata3.Margin = new System.Windows.Forms.Padding(2);
            this.paneldata3.Name = "paneldata3";
            this.paneldata3.Size = new System.Drawing.Size(887, 73);
            this.paneldata3.TabIndex = 59;
            this.paneldata3.Visible = false;
            // 
            // txtcarimaterial
            // 
            this.txtcarimaterial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtcarimaterial.BorderRadius = 4;
            this.txtcarimaterial.BorderThickness = 0;
            this.txtcarimaterial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtcarimaterial.DefaultText = "";
            this.txtcarimaterial.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtcarimaterial.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtcarimaterial.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtcarimaterial.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtcarimaterial.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtcarimaterial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtcarimaterial.ForeColor = System.Drawing.Color.Black;
            this.txtcarimaterial.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtcarimaterial.Location = new System.Drawing.Point(649, 15);
            this.txtcarimaterial.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtcarimaterial.Name = "txtcarimaterial";
            this.txtcarimaterial.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtcarimaterial.PlaceholderText = "Axxxx";
            this.txtcarimaterial.SelectedText = "";
            this.txtcarimaterial.Size = new System.Drawing.Size(220, 29);
            this.txtcarimaterial.TabIndex = 61;
            this.txtcarimaterial.TextChanged += new System.EventHandler(this.txtcarimaterial_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(563, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 60;
            this.label1.Text = "Material :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbnamamaterial
            // 
            this.cmbnamamaterial.BackColor = System.Drawing.Color.Transparent;
            this.cmbnamamaterial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbnamamaterial.BorderRadius = 5;
            this.cmbnamamaterial.BorderThickness = 2;
            this.cmbnamamaterial.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbnamamaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbnamamaterial.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbnamamaterial.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbnamamaterial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbnamamaterial.ForeColor = System.Drawing.Color.Black;
            this.cmbnamamaterial.ItemHeight = 30;
            this.cmbnamamaterial.Location = new System.Drawing.Point(645, 11);
            this.cmbnamamaterial.Margin = new System.Windows.Forms.Padding(2);
            this.cmbnamamaterial.Name = "cmbnamamaterial";
            this.cmbnamamaterial.Size = new System.Drawing.Size(250, 36);
            this.cmbnamamaterial.TabIndex = 59;
            this.cmbnamamaterial.SelectedIndexChanged += new System.EventHandler(this.cmbnamamaterial_SelectedIndexChanged);
            // 
            // lbljumlahdatamaterial
            // 
            this.lbljumlahdatamaterial.AutoSize = true;
            this.lbljumlahdatamaterial.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbljumlahdatamaterial.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbljumlahdatamaterial.Location = new System.Drawing.Point(906, 19);
            this.lbljumlahdatamaterial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbljumlahdatamaterial.Name = "lbljumlahdatamaterial";
            this.lbljumlahdatamaterial.Size = new System.Drawing.Size(118, 20);
            this.lbljumlahdatamaterial.TabIndex = 58;
            this.lbljumlahdatamaterial.Text = "Jumlah Data : 0";
            this.lbljumlahdatamaterial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(264, 19);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 20);
            this.label5.TabIndex = 63;
            this.label5.Text = "s/d";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tanggalAkhirmaterial
            // 
            this.tanggalAkhirmaterial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tanggalAkhirmaterial.BorderRadius = 5;
            this.tanggalAkhirmaterial.BorderThickness = 2;
            this.tanggalAkhirmaterial.Checked = true;
            this.tanggalAkhirmaterial.CustomFormat = "MM/yyyy";
            this.tanggalAkhirmaterial.FillColor = System.Drawing.Color.White;
            this.tanggalAkhirmaterial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tanggalAkhirmaterial.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tanggalAkhirmaterial.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.tanggalAkhirmaterial.Location = new System.Drawing.Point(300, 9);
            this.tanggalAkhirmaterial.Margin = new System.Windows.Forms.Padding(2);
            this.tanggalAkhirmaterial.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.tanggalAkhirmaterial.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tanggalAkhirmaterial.Name = "tanggalAkhirmaterial";
            this.tanggalAkhirmaterial.Size = new System.Drawing.Size(250, 41);
            this.tanggalAkhirmaterial.TabIndex = 64;
            this.tanggalAkhirmaterial.Value = new System.DateTime(2025, 5, 20, 0, 0, 0, 0);
            // 
            // tanggalMulaimaterial
            // 
            this.tanggalMulaimaterial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tanggalMulaimaterial.BorderRadius = 5;
            this.tanggalMulaimaterial.BorderThickness = 2;
            this.tanggalMulaimaterial.Checked = true;
            this.tanggalMulaimaterial.CustomFormat = "MM/yyyy";
            this.tanggalMulaimaterial.FillColor = System.Drawing.Color.White;
            this.tanggalMulaimaterial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tanggalMulaimaterial.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tanggalMulaimaterial.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.tanggalMulaimaterial.Location = new System.Drawing.Point(10, 9);
            this.tanggalMulaimaterial.Margin = new System.Windows.Forms.Padding(2);
            this.tanggalMulaimaterial.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.tanggalMulaimaterial.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tanggalMulaimaterial.Name = "tanggalMulaimaterial";
            this.tanggalMulaimaterial.Size = new System.Drawing.Size(250, 41);
            this.tanggalMulaimaterial.TabIndex = 62;
            this.tanggalMulaimaterial.Value = new System.DateTime(2025, 5, 20, 0, 0, 0, 0);
            // 
            // printpenerimaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(1147, 894);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "printpenerimaan";
            this.Text = "printpenerimaan";
            this.Load += new System.EventHandler(this.printpenerimaan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            this.paneldata2.ResumeLayout(false);
            this.paneldata2.PerformLayout();
            this.panelbukti.ResumeLayout(false);
            this.panelbukti.PerformLayout();
            this.panelsummary.ResumeLayout(false);
            this.panelsummary.PerformLayout();
            this.paneldata3.ResumeLayout(false);
            this.paneldata3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Guna.UI2.WinForms.Guna2Button btnreset;
        private Guna.UI2.WinForms.Guna2DateTimePicker datecari;
        private Guna.UI2.WinForms.Guna2TextBox txttim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2Button btnprint;
        private Guna.UI2.WinForms.Guna2ComboBox cbShift;
        private Guna.UI2.WinForms.Guna2Panel paneldata2;
        private Guna.UI2.WinForms.Guna2ComboBox cmbpilihdata;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Panel paneldata3;
        private System.Windows.Forms.Label lbljumlahdatamaterial;
        private Guna.UI2.WinForms.Guna2ComboBox cmbnamamaterial;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtcarimaterial;
        private Guna.UI2.WinForms.Guna2Panel panelbukti;
        private System.Windows.Forms.Label lbljumlahbukti;
        private Guna.UI2.WinForms.Guna2DateTimePicker datecaribukti;
        private Guna.UI2.WinForms.Guna2ComboBox shiftbukti;
        private System.Windows.Forms.Label label6;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Guna.UI2.WinForms.Guna2Panel panelsummary;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2DateTimePicker tanggalAkhir;
        private System.Windows.Forms.Label lbljumlahsummary;
        private Guna.UI2.WinForms.Guna2DateTimePicker tanggalMulai;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2DateTimePicker tanggalAkhirmaterial;
        private Guna.UI2.WinForms.Guna2DateTimePicker tanggalMulaimaterial;
    }
}