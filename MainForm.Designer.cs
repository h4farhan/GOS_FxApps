using System.Drawing;
using System.Windows.Forms;

namespace GOS_FxApps
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.ControlBoxButton = new Guna.UI2.WinForms.Guna2ControlBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lbluser = new System.Windows.Forms.Label();
            this.iconButton14 = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbldate = new System.Windows.Forms.Label();
            this.lblshift = new System.Windows.Forms.Label();
            this.lblinfo = new System.Windows.Forms.Label();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dashboardButton = new FontAwesome.Sharp.IconButton();
            this.entryContainer = new System.Windows.Forms.Panel();
            this.iconButton4 = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.penerimaanButton1 = new FontAwesome.Sharp.IconButton();
            this.entryButton = new FontAwesome.Sharp.IconButton();
            this.EditContainer = new System.Windows.Forms.Panel();
            this.iconButton12 = new FontAwesome.Sharp.IconButton();
            this.iconButton11 = new FontAwesome.Sharp.IconButton();
            this.iconButton5 = new FontAwesome.Sharp.IconButton();
            this.iconButton6 = new FontAwesome.Sharp.IconButton();
            this.iconButton7 = new FontAwesome.Sharp.IconButton();
            this.iconButton8 = new FontAwesome.Sharp.IconButton();
            this.iconButton9 = new FontAwesome.Sharp.IconButton();
            this.Editbutton = new FontAwesome.Sharp.IconButton();
            this.btnlaporan = new FontAwesome.Sharp.IconButton();
            this.btnhistori = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.HamburgerButton = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.entrytimer = new System.Windows.Forms.Timer(this.components);
            this.edittimer = new System.Windows.Forms.Timer(this.components);
            this.jam = new System.Windows.Forms.Timer(this.components);
            this.titlePanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.entryContainer.SuspendLayout();
            this.EditContainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.titlePanel.Controls.Add(this.ControlBoxButton);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(1628, 20);
            this.titlePanel.TabIndex = 1;
            // 
            // ControlBoxButton
            // 
            this.ControlBoxButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlBoxButton.FillColor = System.Drawing.Color.Transparent;
            this.ControlBoxButton.IconColor = System.Drawing.Color.Red;
            this.ControlBoxButton.Location = new System.Drawing.Point(1595, 0);
            this.ControlBoxButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ControlBoxButton.Name = "ControlBoxButton";
            this.ControlBoxButton.Size = new System.Drawing.Size(33, 20);
            this.ControlBoxButton.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(342, 70);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1286, 825);
            this.panel4.TabIndex = 3;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint_1);
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.headerPanel.Controls.Add(this.lbluser);
            this.headerPanel.Controls.Add(this.iconButton14);
            this.headerPanel.Controls.Add(this.tableLayoutPanel3);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.headerPanel.Location = new System.Drawing.Point(342, 20);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1286, 50);
            this.headerPanel.TabIndex = 3;
            // 
            // lbluser
            // 
            this.lbluser.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbluser.Font = new System.Drawing.Font("Century Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbluser.ForeColor = System.Drawing.Color.IndianRed;
            this.lbluser.Location = new System.Drawing.Point(884, 0);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(345, 50);
            this.lbluser.TabIndex = 6;
            this.lbluser.Text = "Robert Danuarta [Manajer]";
            this.lbluser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iconButton14
            // 
            this.iconButton14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.iconButton14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iconButton14.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconButton14.FlatAppearance.BorderSize = 0;
            this.iconButton14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton14.ForeColor = System.Drawing.Color.Transparent;
            this.iconButton14.IconChar = FontAwesome.Sharp.IconChar.UserCircle;
            this.iconButton14.IconColor = System.Drawing.Color.Aqua;
            this.iconButton14.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton14.IconSize = 40;
            this.iconButton14.Location = new System.Drawing.Point(1229, 0);
            this.iconButton14.Name = "iconButton14";
            this.iconButton14.Size = new System.Drawing.Size(57, 50);
            this.iconButton14.TabIndex = 5;
            this.iconButton14.UseVisualStyleBackColor = false;
            this.iconButton14.Click += new System.EventHandler(this.iconButton14_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.92184F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.07815F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel3.Controls.Add(this.lbldate, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblshift, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblinfo, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(606, 50);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // lbldate
            // 
            this.lbldate.AutoSize = true;
            this.lbldate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbldate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbldate.ForeColor = System.Drawing.Color.White;
            this.lbldate.Location = new System.Drawing.Point(3, 0);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(489, 50);
            this.lbldate.TabIndex = 0;
            this.lbldate.Text = "Tanggal Dan Jam";
            this.lbldate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblshift
            // 
            this.lblshift.AutoSize = true;
            this.lblshift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblshift.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblshift.ForeColor = System.Drawing.Color.White;
            this.lblshift.Location = new System.Drawing.Point(565, 0);
            this.lblshift.Name = "lblshift";
            this.lblshift.Size = new System.Drawing.Size(38, 50);
            this.lblshift.TabIndex = 2;
            this.lblshift.Text = "1";
            this.lblshift.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblinfo
            // 
            this.lblinfo.AutoSize = true;
            this.lblinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblinfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblinfo.ForeColor = System.Drawing.Color.White;
            this.lblinfo.Location = new System.Drawing.Point(498, 0);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(61, 50);
            this.lblinfo.TabIndex = 1;
            this.lblinfo.Text = "Shift :";
            this.lblinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.sidebarPanel.Controls.Add(this.flowLayoutPanel1);
            this.sidebarPanel.Controls.Add(this.tableLayoutPanel2);
            this.sidebarPanel.Controls.Add(this.tableLayoutPanel1);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 20);
            this.sidebarPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sidebarPanel.MaximumSize = new System.Drawing.Size(400, 0);
            this.sidebarPanel.MinimumSize = new System.Drawing.Size(60, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(342, 875);
            this.sidebarPanel.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.dashboardButton);
            this.flowLayoutPanel1.Controls.Add(this.entryContainer);
            this.flowLayoutPanel1.Controls.Add(this.EditContainer);
            this.flowLayoutPanel1.Controls.Add(this.btnlaporan);
            this.flowLayoutPanel1.Controls.Add(this.btnhistori);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 323);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(342, 552);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 47);
            this.panel1.TabIndex = 0;
            // 
            // dashboardButton
            // 
            this.dashboardButton.FlatAppearance.BorderSize = 0;
            this.dashboardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashboardButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dashboardButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.dashboardButton.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            this.dashboardButton.IconColor = System.Drawing.Color.WhiteSmoke;
            this.dashboardButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.dashboardButton.IconSize = 40;
            this.dashboardButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dashboardButton.Location = new System.Drawing.Point(3, 53);
            this.dashboardButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dashboardButton.Name = "dashboardButton";
            this.dashboardButton.Size = new System.Drawing.Size(395, 74);
            this.dashboardButton.TabIndex = 1;
            this.dashboardButton.Text = "     Dashboard";
            this.dashboardButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dashboardButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.dashboardButton.UseVisualStyleBackColor = true;
            this.dashboardButton.Click += new System.EventHandler(this.dashboardButton_Click);
            // 
            // entryContainer
            // 
            this.entryContainer.Controls.Add(this.iconButton4);
            this.entryContainer.Controls.Add(this.iconButton3);
            this.entryContainer.Controls.Add(this.iconButton2);
            this.entryContainer.Controls.Add(this.iconButton1);
            this.entryContainer.Controls.Add(this.penerimaanButton1);
            this.entryContainer.Controls.Add(this.entryButton);
            this.entryContainer.Location = new System.Drawing.Point(3, 131);
            this.entryContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.entryContainer.MaximumSize = new System.Drawing.Size(387, 345);
            this.entryContainer.MinimumSize = new System.Drawing.Size(387, 74);
            this.entryContainer.Name = "entryContainer";
            this.entryContainer.Size = new System.Drawing.Size(387, 74);
            this.entryContainer.TabIndex = 2;
            // 
            // iconButton4
            // 
            this.iconButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton4.FlatAppearance.BorderSize = 0;
            this.iconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton4.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton4.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton4.IconSize = 40;
            this.iconButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.Location = new System.Drawing.Point(0, 270);
            this.iconButton4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton4.Name = "iconButton4";
            this.iconButton4.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton4.Size = new System.Drawing.Size(387, 74);
            this.iconButton4.TabIndex = 7;
            this.iconButton4.Text = "     Pemakaian \r\n     Material";
            this.iconButton4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton4.UseVisualStyleBackColor = true;
            // 
            // iconButton3
            // 
            this.iconButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton3.FlatAppearance.BorderSize = 0;
            this.iconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton3.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.IconSize = 40;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.Location = new System.Drawing.Point(0, 221);
            this.iconButton3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton3.Size = new System.Drawing.Size(387, 49);
            this.iconButton3.TabIndex = 6;
            this.iconButton3.Text = "     Welding Pieces";
            this.iconButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton3.UseVisualStyleBackColor = true;
            // 
            // iconButton2
            // 
            this.iconButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton2.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 40;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(0, 172);
            this.iconButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton2.Size = new System.Drawing.Size(387, 49);
            this.iconButton2.TabIndex = 5;
            this.iconButton2.Text = "     Pengiriman";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton2.UseVisualStyleBackColor = true;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // iconButton1
            // 
            this.iconButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton1.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 40;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(0, 123);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton1.Size = new System.Drawing.Size(387, 49);
            this.iconButton1.TabIndex = 4;
            this.iconButton1.Text = "     Perbaikan";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = true;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // penerimaanButton1
            // 
            this.penerimaanButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.penerimaanButton1.FlatAppearance.BorderSize = 0;
            this.penerimaanButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.penerimaanButton1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.penerimaanButton1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.penerimaanButton1.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.penerimaanButton1.IconColor = System.Drawing.Color.WhiteSmoke;
            this.penerimaanButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.penerimaanButton1.IconSize = 40;
            this.penerimaanButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.penerimaanButton1.Location = new System.Drawing.Point(0, 74);
            this.penerimaanButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.penerimaanButton1.Name = "penerimaanButton1";
            this.penerimaanButton1.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.penerimaanButton1.Size = new System.Drawing.Size(387, 49);
            this.penerimaanButton1.TabIndex = 3;
            this.penerimaanButton1.Text = "     Penerimaan";
            this.penerimaanButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.penerimaanButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.penerimaanButton1.UseVisualStyleBackColor = true;
            this.penerimaanButton1.Click += new System.EventHandler(this.penerimaanButton1_Click);
            // 
            // entryButton
            // 
            this.entryButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.entryButton.FlatAppearance.BorderSize = 0;
            this.entryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.entryButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.entryButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.entryButton.IconChar = FontAwesome.Sharp.IconChar.FileDownload;
            this.entryButton.IconColor = System.Drawing.Color.WhiteSmoke;
            this.entryButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.entryButton.IconSize = 40;
            this.entryButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.entryButton.Location = new System.Drawing.Point(0, 0);
            this.entryButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.entryButton.Name = "entryButton";
            this.entryButton.Size = new System.Drawing.Size(387, 74);
            this.entryButton.TabIndex = 2;
            this.entryButton.Text = "     Form Entry Data ";
            this.entryButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.entryButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.entryButton.UseVisualStyleBackColor = true;
            this.entryButton.Click += new System.EventHandler(this.entryButton_Click);
            // 
            // EditContainer
            // 
            this.EditContainer.Controls.Add(this.iconButton12);
            this.EditContainer.Controls.Add(this.iconButton11);
            this.EditContainer.Controls.Add(this.iconButton5);
            this.EditContainer.Controls.Add(this.iconButton6);
            this.EditContainer.Controls.Add(this.iconButton7);
            this.EditContainer.Controls.Add(this.iconButton8);
            this.EditContainer.Controls.Add(this.iconButton9);
            this.EditContainer.Controls.Add(this.Editbutton);
            this.EditContainer.Location = new System.Drawing.Point(3, 209);
            this.EditContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditContainer.MaximumSize = new System.Drawing.Size(387, 479);
            this.EditContainer.MinimumSize = new System.Drawing.Size(387, 74);
            this.EditContainer.Name = "EditContainer";
            this.EditContainer.Size = new System.Drawing.Size(387, 74);
            this.EditContainer.TabIndex = 8;
            // 
            // iconButton12
            // 
            this.iconButton12.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton12.FlatAppearance.BorderSize = 0;
            this.iconButton12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton12.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton12.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton12.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton12.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton12.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton12.IconSize = 40;
            this.iconButton12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton12.Location = new System.Drawing.Point(0, 393);
            this.iconButton12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton12.Name = "iconButton12";
            this.iconButton12.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton12.Size = new System.Drawing.Size(387, 74);
            this.iconButton12.TabIndex = 9;
            this.iconButton12.Text = "     Koefisien \r\n     Material";
            this.iconButton12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton12.UseVisualStyleBackColor = true;
            // 
            // iconButton11
            // 
            this.iconButton11.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton11.FlatAppearance.BorderSize = 0;
            this.iconButton11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton11.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton11.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton11.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton11.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton11.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton11.IconSize = 40;
            this.iconButton11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton11.Location = new System.Drawing.Point(0, 344);
            this.iconButton11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton11.Name = "iconButton11";
            this.iconButton11.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton11.Size = new System.Drawing.Size(387, 49);
            this.iconButton11.TabIndex = 8;
            this.iconButton11.Text = "     Stok Material";
            this.iconButton11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton11.UseVisualStyleBackColor = true;
            // 
            // iconButton5
            // 
            this.iconButton5.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton5.FlatAppearance.BorderSize = 0;
            this.iconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton5.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton5.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton5.IconSize = 40;
            this.iconButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.Location = new System.Drawing.Point(0, 270);
            this.iconButton5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton5.Size = new System.Drawing.Size(387, 74);
            this.iconButton5.TabIndex = 7;
            this.iconButton5.Text = "     Pemakaian \r\n     Material";
            this.iconButton5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton5.UseVisualStyleBackColor = true;
            // 
            // iconButton6
            // 
            this.iconButton6.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton6.FlatAppearance.BorderSize = 0;
            this.iconButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton6.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton6.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton6.IconSize = 40;
            this.iconButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton6.Location = new System.Drawing.Point(0, 221);
            this.iconButton6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton6.Name = "iconButton6";
            this.iconButton6.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton6.Size = new System.Drawing.Size(387, 49);
            this.iconButton6.TabIndex = 6;
            this.iconButton6.Text = "     Welding Pieces";
            this.iconButton6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton6.UseVisualStyleBackColor = true;
            // 
            // iconButton7
            // 
            this.iconButton7.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton7.FlatAppearance.BorderSize = 0;
            this.iconButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton7.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton7.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton7.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton7.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton7.IconSize = 40;
            this.iconButton7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton7.Location = new System.Drawing.Point(0, 172);
            this.iconButton7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton7.Name = "iconButton7";
            this.iconButton7.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton7.Size = new System.Drawing.Size(387, 49);
            this.iconButton7.TabIndex = 5;
            this.iconButton7.Text = "     Pengiriman";
            this.iconButton7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton7.UseVisualStyleBackColor = true;
            // 
            // iconButton8
            // 
            this.iconButton8.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton8.FlatAppearance.BorderSize = 0;
            this.iconButton8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton8.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton8.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton8.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton8.IconSize = 40;
            this.iconButton8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton8.Location = new System.Drawing.Point(0, 123);
            this.iconButton8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton8.Name = "iconButton8";
            this.iconButton8.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton8.Size = new System.Drawing.Size(387, 49);
            this.iconButton8.TabIndex = 4;
            this.iconButton8.Text = "     Perbaikan";
            this.iconButton8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton8.UseVisualStyleBackColor = true;
            this.iconButton8.Click += new System.EventHandler(this.iconButton8_Click);
            // 
            // iconButton9
            // 
            this.iconButton9.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton9.FlatAppearance.BorderSize = 0;
            this.iconButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton9.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton9.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton9.IconSize = 40;
            this.iconButton9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton9.Location = new System.Drawing.Point(0, 74);
            this.iconButton9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton9.Name = "iconButton9";
            this.iconButton9.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton9.Size = new System.Drawing.Size(387, 49);
            this.iconButton9.TabIndex = 3;
            this.iconButton9.Text = "     Penerimaan";
            this.iconButton9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton9.UseVisualStyleBackColor = true;
            this.iconButton9.Click += new System.EventHandler(this.iconButton9_Click);
            // 
            // Editbutton
            // 
            this.Editbutton.Dock = System.Windows.Forms.DockStyle.Top;
            this.Editbutton.FlatAppearance.BorderSize = 0;
            this.Editbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Editbutton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Editbutton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Editbutton.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.Editbutton.IconColor = System.Drawing.Color.WhiteSmoke;
            this.Editbutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Editbutton.IconSize = 40;
            this.Editbutton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Editbutton.Location = new System.Drawing.Point(0, 0);
            this.Editbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Editbutton.Name = "Editbutton";
            this.Editbutton.Size = new System.Drawing.Size(387, 74);
            this.Editbutton.TabIndex = 2;
            this.Editbutton.Text = "     Form Edit Data ";
            this.Editbutton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Editbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Editbutton.UseVisualStyleBackColor = true;
            this.Editbutton.Click += new System.EventHandler(this.Editbutton_Click);
            // 
            // btnlaporan
            // 
            this.btnlaporan.FlatAppearance.BorderSize = 0;
            this.btnlaporan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlaporan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnlaporan.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnlaporan.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            this.btnlaporan.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnlaporan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnlaporan.IconSize = 40;
            this.btnlaporan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlaporan.Location = new System.Drawing.Point(3, 287);
            this.btnlaporan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnlaporan.Name = "btnlaporan";
            this.btnlaporan.Size = new System.Drawing.Size(387, 74);
            this.btnlaporan.TabIndex = 9;
            this.btnlaporan.Text = "     Form Laporan";
            this.btnlaporan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlaporan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlaporan.UseVisualStyleBackColor = true;
            // 
            // btnhistori
            // 
            this.btnhistori.FlatAppearance.BorderSize = 0;
            this.btnhistori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhistori.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnhistori.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnhistori.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            this.btnhistori.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnhistori.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnhistori.IconSize = 40;
            this.btnhistori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnhistori.Location = new System.Drawing.Point(3, 365);
            this.btnhistori.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnhistori.Name = "btnhistori";
            this.btnhistori.Size = new System.Drawing.Size(387, 74);
            this.btnhistori.TabIndex = 10;
            this.btnhistori.Text = "     Data Histori";
            this.btnhistori.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnhistori.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnhistori.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 62);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(342, 261);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::GOS_FxApps.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(70, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(269, 181);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(70, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 76);
            this.label2.TabIndex = 2;
            this.label2.Text = "PT\r\nGENTANUSA GEMILANG";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.HamburgerButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(342, 62);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // HamburgerButton
            // 
            this.HamburgerButton.BackColor = System.Drawing.Color.Transparent;
            this.HamburgerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HamburgerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HamburgerButton.ForeColor = System.Drawing.Color.Transparent;
            this.HamburgerButton.IconChar = FontAwesome.Sharp.IconChar.Navicon;
            this.HamburgerButton.IconColor = System.Drawing.Color.WhiteSmoke;
            this.HamburgerButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.HamburgerButton.IconSize = 30;
            this.HamburgerButton.Location = new System.Drawing.Point(3, 2);
            this.HamburgerButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HamburgerButton.Name = "HamburgerButton";
            this.HamburgerButton.Size = new System.Drawing.Size(61, 58);
            this.HamburgerButton.TabIndex = 0;
            this.HamburgerButton.UseVisualStyleBackColor = false;
            this.HamburgerButton.Click += new System.EventHandler(this.HamburgerButton_Click_1);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(70, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 62);
            this.label1.TabIndex = 1;
            this.label1.Text = "Powered by :\r\nPT Digital Transformasi Industri";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // entrytimer
            // 
            this.entrytimer.Interval = 10;
            this.entrytimer.Tick += new System.EventHandler(this.entrytimer_Tick);
            // 
            // edittimer
            // 
            this.edittimer.Interval = 10;
            this.edittimer.Tick += new System.EventHandler(this.edittimer_Tick);
            // 
            // jam
            // 
            this.jam.Enabled = true;
            this.jam.Interval = 1000;
            this.jam.Tick += new System.EventHandler(this.jam_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1628, 895);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.titlePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.titlePanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.sidebarPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.entryContainer.ResumeLayout(false);
            this.EditContainer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel titlePanel;
        private Panel panel4;
        private Panel headerPanel;
        private Panel sidebarPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private FontAwesome.Sharp.IconButton HamburgerButton;
        private TableLayoutPanel tableLayoutPanel2;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton dashboardButton;
        private Panel entryContainer;
        private FontAwesome.Sharp.IconButton penerimaanButton1;
        private FontAwesome.Sharp.IconButton entryButton;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.Timer entrytimer;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButton3;
        public Panel EditContainer;
        private FontAwesome.Sharp.IconButton iconButton5;
        public FontAwesome.Sharp.IconButton iconButton6;
        private FontAwesome.Sharp.IconButton iconButton8;
        private FontAwesome.Sharp.IconButton iconButton9;
        private FontAwesome.Sharp.IconButton Editbutton;
        private FontAwesome.Sharp.IconButton iconButton12;
        private FontAwesome.Sharp.IconButton iconButton11;
        private System.Windows.Forms.Timer edittimer;
        private FontAwesome.Sharp.IconButton btnlaporan;
        public FontAwesome.Sharp.IconButton btnhistori;
        private Guna.UI2.WinForms.Guna2ControlBox ControlBoxButton;
        private FontAwesome.Sharp.IconButton iconButton7;
        private Label lbldate;
        private System.Windows.Forms.Timer jam;
        private Label lblinfo;
        public Label lblshift;
        private TableLayoutPanel tableLayoutPanel3;
        private FontAwesome.Sharp.IconButton iconButton14;
        public Label lbluser;
    }
}
