namespace GOS_FxApps
{
    partial class historyPerbaikan
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
            this.tanggal1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblhalaman = new System.Windows.Forms.Label();
            this.btnleft = new Guna.UI2.WinForms.Guna2Button();
            this.btnright = new Guna.UI2.WinForms.Guna2Button();
            this.btncari = new Guna.UI2.WinForms.Guna2Button();
            this.txtcari = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbShift = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnreset = new Guna.UI2.WinForms.Guna2Button();
            this.lbljumlahdata = new System.Windows.Forms.Label();
            this.txtnama = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.tanggal2 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label74 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnprint = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tanggal1
            // 
            this.tanggal1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tanggal1.BorderRadius = 5;
            this.tanggal1.BorderThickness = 2;
            this.tanggal1.Checked = true;
            this.tanggal1.FillColor = System.Drawing.Color.White;
            this.tanggal1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tanggal1.ForeColor = System.Drawing.Color.Black;
            this.tanggal1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.tanggal1.Location = new System.Drawing.Point(2, 5);
            this.tanggal1.Margin = new System.Windows.Forms.Padding(2);
            this.tanggal1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.tanggal1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tanggal1.Name = "tanggal1";
            this.tanggal1.ShowCheckBox = true;
            this.tanggal1.Size = new System.Drawing.Size(238, 41);
            this.tanggal1.TabIndex = 51;
            this.tanggal1.Value = new System.DateTime(2025, 5, 20, 10, 10, 59, 90);
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel4.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel4.BorderRadius = 15;
            this.guna2Panel4.BorderThickness = 2;
            this.guna2Panel4.Controls.Add(this.dataGridView1);
            this.guna2Panel4.Controls.Add(this.guna2Panel1);
            this.guna2Panel4.FillColor = System.Drawing.Color.White;
            this.guna2Panel4.Location = new System.Drawing.Point(11, 153);
            this.guna2Panel4.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.guna2Panel4.Size = new System.Drawing.Size(1336, 510);
            this.guna2Panel4.TabIndex = 48;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
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
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.Gray;
            this.dataGridView1.Location = new System.Drawing.Point(11, 12);
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
            this.dataGridView1.Size = new System.Drawing.Size(1314, 421);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.lblhalaman);
            this.guna2Panel1.Controls.Add(this.btnleft);
            this.guna2Panel1.Controls.Add(this.btnright);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel1.Location = new System.Drawing.Point(11, 433);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1314, 65);
            this.guna2Panel1.TabIndex = 4;
            // 
            // lblhalaman
            // 
            this.lblhalaman.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblhalaman.AutoSize = true;
            this.lblhalaman.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhalaman.ForeColor = System.Drawing.Color.Black;
            this.lblhalaman.Location = new System.Drawing.Point(14, 26);
            this.lblhalaman.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblhalaman.Name = "lblhalaman";
            this.lblhalaman.Size = new System.Drawing.Size(12, 15);
            this.lblhalaman.TabIndex = 64;
            this.lblhalaman.Text = "-";
            this.lblhalaman.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnleft
            // 
            this.btnleft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnleft.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnleft.BorderRadius = 8;
            this.btnleft.BorderThickness = 2;
            this.btnleft.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnleft.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnleft.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnleft.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnleft.FillColor = System.Drawing.Color.White;
            this.btnleft.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnleft.ForeColor = System.Drawing.Color.White;
            this.btnleft.Image = global::GOS_FxApps.Properties.Resources.left_arrow;
            this.btnleft.Location = new System.Drawing.Point(1169, 15);
            this.btnleft.Margin = new System.Windows.Forms.Padding(2);
            this.btnleft.Name = "btnleft";
            this.btnleft.Size = new System.Drawing.Size(60, 37);
            this.btnleft.TabIndex = 64;
            this.btnleft.Click += new System.EventHandler(this.btnleft_Click);
            // 
            // btnright
            // 
            this.btnright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnright.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnright.BorderRadius = 8;
            this.btnright.BorderThickness = 2;
            this.btnright.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnright.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnright.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnright.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnright.FillColor = System.Drawing.Color.White;
            this.btnright.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnright.ForeColor = System.Drawing.Color.White;
            this.btnright.Image = global::GOS_FxApps.Properties.Resources.right_arrow;
            this.btnright.Location = new System.Drawing.Point(1241, 15);
            this.btnright.Margin = new System.Windows.Forms.Padding(2);
            this.btnright.Name = "btnright";
            this.btnright.Size = new System.Drawing.Size(60, 37);
            this.btnright.TabIndex = 44;
            this.btnright.Click += new System.EventHandler(this.btnright_Click);
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
            this.btncari.Location = new System.Drawing.Point(1078, 84);
            this.btncari.Margin = new System.Windows.Forms.Padding(2);
            this.btncari.Name = "btncari";
            this.btncari.Size = new System.Drawing.Size(60, 41);
            this.btncari.TabIndex = 49;
            this.btncari.Text = "Cari";
            this.btncari.Click += new System.EventHandler(this.btncari_Click);
            // 
            // txtcari
            // 
            this.txtcari.BackColor = System.Drawing.Color.Transparent;
            this.txtcari.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtcari.BorderRadius = 5;
            this.txtcari.BorderThickness = 2;
            this.txtcari.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtcari.DefaultText = "";
            this.txtcari.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtcari.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtcari.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtcari.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtcari.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtcari.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtcari.ForeColor = System.Drawing.Color.Black;
            this.txtcari.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtcari.IconLeft = global::GOS_FxApps.Properties.Resources.search;
            this.txtcari.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.txtcari.IconLeftSize = new System.Drawing.Size(15, 15);
            this.txtcari.Location = new System.Drawing.Point(522, 5);
            this.txtcari.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtcari.Name = "txtcari";
            this.txtcari.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtcari.PlaceholderText = "Nomor Rod";
            this.txtcari.SelectedText = "";
            this.txtcari.Size = new System.Drawing.Size(169, 41);
            this.txtcari.TabIndex = 50;
            this.txtcari.TextOffset = new System.Drawing.Point(5, 0);
            this.txtcari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hurufbesar_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1358, 57);
            this.panel1.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(125, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Perbaikan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data History  /";
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
            this.cbShift.ForeColor = System.Drawing.Color.Black;
            this.cbShift.ItemHeight = 30;
            this.cbShift.Items.AddRange(new object[] {
            "Shift",
            "1",
            "2",
            "3"});
            this.cbShift.Location = new System.Drawing.Point(986, 7);
            this.cbShift.Margin = new System.Windows.Forms.Padding(2);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(86, 36);
            this.cbShift.StartIndex = 0;
            this.cbShift.TabIndex = 58;
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
            this.btnreset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(53)))));
            this.btnreset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnreset.ForeColor = System.Drawing.Color.White;
            this.btnreset.Location = new System.Drawing.Point(1143, 84);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(66, 41);
            this.btnreset.TabIndex = 61;
            this.btnreset.Text = "Reset";
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            // 
            // lbljumlahdata
            // 
            this.lbljumlahdata.AutoSize = true;
            this.lbljumlahdata.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbljumlahdata.ForeColor = System.Drawing.Color.Black;
            this.lbljumlahdata.Location = new System.Drawing.Point(1076, 15);
            this.lbljumlahdata.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbljumlahdata.Name = "lbljumlahdata";
            this.lbljumlahdata.Size = new System.Drawing.Size(15, 20);
            this.lbljumlahdata.TabIndex = 65;
            this.lbljumlahdata.Text = "-";
            this.lbljumlahdata.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtnama
            // 
            this.txtnama.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtnama.BorderRadius = 5;
            this.txtnama.BorderThickness = 2;
            this.txtnama.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtnama.DefaultText = "";
            this.txtnama.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtnama.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtnama.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtnama.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtnama.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtnama.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtnama.ForeColor = System.Drawing.Color.Black;
            this.txtnama.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtnama.IconLeft = global::GOS_FxApps.Properties.Resources.search;
            this.txtnama.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.txtnama.IconLeftSize = new System.Drawing.Size(15, 15);
            this.txtnama.Location = new System.Drawing.Point(695, 5);
            this.txtnama.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtnama.Name = "txtnama";
            this.txtnama.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtnama.PlaceholderText = "Checker Tim Penginput";
            this.txtnama.SelectedText = "";
            this.txtnama.Size = new System.Drawing.Size(287, 41);
            this.txtnama.TabIndex = 67;
            this.txtnama.TextOffset = new System.Drawing.Point(5, 0);
            this.txtnama.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hurufbesar_KeyPress);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel2.AutoScroll = true;
            this.guna2Panel2.Controls.Add(this.lbljumlahdata);
            this.guna2Panel2.Controls.Add(this.cbShift);
            this.guna2Panel2.Controls.Add(this.txtnama);
            this.guna2Panel2.Controls.Add(this.txtcari);
            this.guna2Panel2.Controls.Add(this.tanggal2);
            this.guna2Panel2.Controls.Add(this.label74);
            this.guna2Panel2.Controls.Add(this.panel2);
            this.guna2Panel2.Controls.Add(this.tanggal1);
            this.guna2Panel2.Location = new System.Drawing.Point(11, 79);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1062, 69);
            this.guna2Panel2.TabIndex = 69;
            this.guna2Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel2_Paint);
            // 
            // tanggal2
            // 
            this.tanggal2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tanggal2.BorderRadius = 5;
            this.tanggal2.BorderThickness = 2;
            this.tanggal2.Checked = true;
            this.tanggal2.FillColor = System.Drawing.Color.White;
            this.tanggal2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tanggal2.ForeColor = System.Drawing.Color.Black;
            this.tanggal2.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.tanggal2.Location = new System.Drawing.Point(280, 5);
            this.tanggal2.Margin = new System.Windows.Forms.Padding(2);
            this.tanggal2.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.tanggal2.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tanggal2.Name = "tanggal2";
            this.tanggal2.ShowCheckBox = true;
            this.tanggal2.Size = new System.Drawing.Size(238, 41);
            this.tanggal2.TabIndex = 68;
            this.tanggal2.Value = new System.DateTime(2025, 5, 20, 10, 10, 59, 90);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label74.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label74.Location = new System.Drawing.Point(244, 15);
            this.label74.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(32, 20);
            this.label74.TabIndex = 70;
            this.label74.Text = "s/d";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(1096, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 36);
            this.panel2.TabIndex = 69;
            // 
            // btnprint
            // 
            this.btnprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnprint.BorderRadius = 6;
            this.btnprint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnprint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnprint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnprint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnprint.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(1214, 84);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(133, 41);
            this.btnprint.TabIndex = 70;
            this.btnprint.Text = "Ekspor Ke Excel";
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // historyPerbaikan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1358, 677);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.btnreset);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.guna2Panel4);
            this.Controls.Add(this.btncari);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "historyPerbaikan";
            this.Text = "historyPerbaikan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.historyPerbaikan_FormClosing);
            this.Load += new System.EventHandler(this.historyPerbaikan_Load);
            this.guna2Panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker tanggal1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2Button btncari;
        private Guna.UI2.WinForms.Guna2TextBox txtcari;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cbShift;
        private Guna.UI2.WinForms.Guna2Button btnreset;
        private System.Windows.Forms.Label lbljumlahdata;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblhalaman;
        private Guna.UI2.WinForms.Guna2Button btnleft;
        private Guna.UI2.WinForms.Guna2Button btnright;
        private Guna.UI2.WinForms.Guna2TextBox txtnama;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2DateTimePicker tanggal2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label74;
        private Guna.UI2.WinForms.Guna2Button btnprint;
    }
}