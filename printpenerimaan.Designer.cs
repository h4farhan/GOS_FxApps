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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btnprint = new Guna.UI2.WinForms.Guna2Button();
            this.btncari = new Guna.UI2.WinForms.Guna2Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.paneldata1 = new Guna.UI2.WinForms.Guna2Panel();
            this.jlhpanel1 = new System.Windows.Forms.Label();
            this.datecaripemakaian = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.paneldata3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbnamamaterial = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbljumlahdatamaterial = new System.Windows.Forms.Label();
            this.datematerial = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.paneldata2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.paneldata1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(1008, 57);
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
            "Welding Pieces",
            "Hasil Produksi & Pemakaian Material",
            "Summary Data for Anode ROD Repair",
            "Actual Quantity for Repaired ROD Assy",
            "Kondisi ROD Reject di Rod Repair Shop",
            "Kartu Stock Material"});
            this.cmbpilihdata.Location = new System.Drawing.Point(692, 13);
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
            this.label3.Location = new System.Drawing.Point(605, 21);
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
            this.panel3.Size = new System.Drawing.Size(1008, 837);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1008, 837);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel4.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel4.BorderRadius = 15;
            this.guna2Panel4.BorderThickness = 2;
            this.guna2Panel4.Controls.Add(this.paneldata2);
            this.guna2Panel4.Controls.Add(this.btnprint);
            this.guna2Panel4.Controls.Add(this.btncari);
            this.guna2Panel4.Controls.Add(this.dataGridView1);
            this.guna2Panel4.Controls.Add(this.paneldata1);
            this.guna2Panel4.Controls.Add(this.paneldata3);
            this.guna2Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel4.FillColor = System.Drawing.Color.White;
            this.guna2Panel4.Location = new System.Drawing.Point(10, 2);
            this.guna2Panel4.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.guna2Panel4.Size = new System.Drawing.Size(988, 827);
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
            this.paneldata2.Size = new System.Drawing.Size(738, 73);
            this.paneldata2.TabIndex = 56;
            this.paneldata2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(524, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 20);
            this.label4.TabIndex = 56;
            this.label4.Text = "Jumlah Data : 40000000";
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
            this.datecari.Size = new System.Drawing.Size(237, 41);
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
            this.cbShift.Location = new System.Drawing.Point(305, 22);
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
            this.label7.Location = new System.Drawing.Point(255, 30);
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
            this.label8.Location = new System.Drawing.Point(395, 30);
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
            this.txttim.Location = new System.Drawing.Point(441, 26);
            this.txttim.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txttim.Name = "txttim";
            this.txttim.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txttim.PlaceholderText = "Axxxx";
            this.txttim.SelectedText = "";
            this.txttim.Size = new System.Drawing.Size(71, 29);
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
            this.btnprint.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(859, 42);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(116, 38);
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
            this.btncari.Enabled = false;
            this.btncari.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btncari.ForeColor = System.Drawing.Color.White;
            this.btncari.Location = new System.Drawing.Point(763, 42);
            this.btncari.Margin = new System.Windows.Forms.Padding(2);
            this.btncari.Name = "btncari";
            this.btncari.Size = new System.Drawing.Size(92, 38);
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
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.Gray;
            this.dataGridView1.Location = new System.Drawing.Point(11, 102);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(964, 717);
            this.dataGridView1.TabIndex = 1;
            // 
            // paneldata1
            // 
            this.paneldata1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paneldata1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.paneldata1.BorderRadius = 15;
            this.paneldata1.Controls.Add(this.jlhpanel1);
            this.paneldata1.Controls.Add(this.datecaripemakaian);
            this.paneldata1.Location = new System.Drawing.Point(11, 25);
            this.paneldata1.Margin = new System.Windows.Forms.Padding(2);
            this.paneldata1.Name = "paneldata1";
            this.paneldata1.Size = new System.Drawing.Size(527, 73);
            this.paneldata1.TabIndex = 57;
            this.paneldata1.Visible = false;
            // 
            // jlhpanel1
            // 
            this.jlhpanel1.AutoSize = true;
            this.jlhpanel1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.jlhpanel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.jlhpanel1.Location = new System.Drawing.Point(266, 30);
            this.jlhpanel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.jlhpanel1.Name = "jlhpanel1";
            this.jlhpanel1.Size = new System.Drawing.Size(172, 20);
            this.jlhpanel1.TabIndex = 58;
            this.jlhpanel1.Text = "Jumlah Data : 4000000";
            this.jlhpanel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // datecaripemakaian
            // 
            this.datecaripemakaian.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.datecaripemakaian.BorderRadius = 5;
            this.datecaripemakaian.BorderThickness = 2;
            this.datecaripemakaian.Checked = true;
            this.datecaripemakaian.CustomFormat = "MM/yyyy";
            this.datecaripemakaian.FillColor = System.Drawing.Color.White;
            this.datecaripemakaian.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.datecaripemakaian.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.datecaripemakaian.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datecaripemakaian.Location = new System.Drawing.Point(11, 20);
            this.datecaripemakaian.Margin = new System.Windows.Forms.Padding(2);
            this.datecaripemakaian.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datecaripemakaian.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datecaripemakaian.Name = "datecaripemakaian";
            this.datecaripemakaian.Size = new System.Drawing.Size(242, 41);
            this.datecaripemakaian.TabIndex = 57;
            this.datecaripemakaian.Value = new System.DateTime(2025, 5, 20, 0, 0, 0, 0);
            this.datecaripemakaian.MouseDown += new System.Windows.Forms.MouseEventHandler(this.datecaripemakaian_MouseDown);
            // 
            // paneldata3
            // 
            this.paneldata3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paneldata3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.paneldata3.BorderRadius = 15;
            this.paneldata3.Controls.Add(this.guna2TextBox1);
            this.paneldata3.Controls.Add(this.label1);
            this.paneldata3.Controls.Add(this.cmbnamamaterial);
            this.paneldata3.Controls.Add(this.lbljumlahdatamaterial);
            this.paneldata3.Controls.Add(this.datematerial);
            this.paneldata3.Location = new System.Drawing.Point(11, 25);
            this.paneldata3.Margin = new System.Windows.Forms.Padding(2);
            this.paneldata3.Name = "paneldata3";
            this.paneldata3.Size = new System.Drawing.Size(738, 73);
            this.paneldata3.TabIndex = 59;
            this.paneldata3.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(201, 30);
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
            this.cmbnamamaterial.Location = new System.Drawing.Point(277, 22);
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
            this.lbljumlahdatamaterial.Location = new System.Drawing.Point(541, 30);
            this.lbljumlahdatamaterial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbljumlahdatamaterial.Name = "lbljumlahdatamaterial";
            this.lbljumlahdatamaterial.Size = new System.Drawing.Size(172, 20);
            this.lbljumlahdatamaterial.TabIndex = 58;
            this.lbljumlahdatamaterial.Text = "Jumlah Data : 4000000";
            this.lbljumlahdatamaterial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // datematerial
            // 
            this.datematerial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.datematerial.BorderRadius = 5;
            this.datematerial.BorderThickness = 2;
            this.datematerial.Checked = true;
            this.datematerial.CustomFormat = "MM/yyyy";
            this.datematerial.FillColor = System.Drawing.Color.White;
            this.datematerial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.datematerial.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.datematerial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datematerial.Location = new System.Drawing.Point(11, 22);
            this.datematerial.Margin = new System.Windows.Forms.Padding(2);
            this.datematerial.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datematerial.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datematerial.Name = "datematerial";
            this.datematerial.Size = new System.Drawing.Size(179, 36);
            this.datematerial.TabIndex = 57;
            this.datematerial.Value = new System.DateTime(2025, 5, 20, 0, 0, 0, 0);
            this.datematerial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.datematerial_MouseDown);
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2TextBox1.BorderRadius = 4;
            this.guna2TextBox1.BorderThickness = 0;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.guna2TextBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(281, 25);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2TextBox1.PlaceholderText = "Axxxx";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(220, 29);
            this.guna2TextBox1.TabIndex = 61;
            // 
            // printpenerimaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(1008, 894);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.paneldata1.ResumeLayout(false);
            this.paneldata1.PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Button btncari;
        private Guna.UI2.WinForms.Guna2DateTimePicker datecari;
        private Guna.UI2.WinForms.Guna2TextBox txttim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2Button btnprint;
        private Guna.UI2.WinForms.Guna2ComboBox cbShift;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2Panel paneldata2;
        private Guna.UI2.WinForms.Guna2ComboBox cmbpilihdata;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Panel paneldata1;
        private Guna.UI2.WinForms.Guna2DateTimePicker datecaripemakaian;
        private System.Windows.Forms.Label jlhpanel1;
        private Guna.UI2.WinForms.Guna2Panel paneldata3;
        private System.Windows.Forms.Label lbljumlahdatamaterial;
        private Guna.UI2.WinForms.Guna2DateTimePicker datematerial;
        private Guna.UI2.WinForms.Guna2ComboBox cmbnamamaterial;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
    }
}