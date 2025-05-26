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
            this.lblshift = new System.Windows.Forms.Label();
            this.lblinfo = new System.Windows.Forms.Label();
            this.lbldate = new System.Windows.Forms.Label();
            this.lbluser = new System.Windows.Forms.Label();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.entryContainer = new System.Windows.Forms.Panel();
            this.EditContainer = new System.Windows.Forms.Panel();
            this.laporanContainer = new System.Windows.Forms.Panel();
            this.historycontainer = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.entrytimer = new System.Windows.Forms.Timer(this.components);
            this.edittimer = new System.Windows.Forms.Timer(this.components);
            this.jam = new System.Windows.Forms.Timer(this.components);
            this.historitimer = new System.Windows.Forms.Timer(this.components);
            this.laporantimer = new System.Windows.Forms.Timer(this.components);
            this.iconButton14 = new FontAwesome.Sharp.IconButton();
            this.dashboardButton = new FontAwesome.Sharp.IconButton();
            this.iconButton4 = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.penerimaanButton1 = new FontAwesome.Sharp.IconButton();
            this.entryButton = new FontAwesome.Sharp.IconButton();
            this.iconButton12 = new FontAwesome.Sharp.IconButton();
            this.iconButton11 = new FontAwesome.Sharp.IconButton();
            this.iconButton5 = new FontAwesome.Sharp.IconButton();
            this.iconButton6 = new FontAwesome.Sharp.IconButton();
            this.iconButton7 = new FontAwesome.Sharp.IconButton();
            this.iconButton8 = new FontAwesome.Sharp.IconButton();
            this.iconButton9 = new FontAwesome.Sharp.IconButton();
            this.Editbutton = new FontAwesome.Sharp.IconButton();
            this.iconButton10 = new FontAwesome.Sharp.IconButton();
            this.iconButton13 = new FontAwesome.Sharp.IconButton();
            this.iconButton15 = new FontAwesome.Sharp.IconButton();
            this.iconButton16 = new FontAwesome.Sharp.IconButton();
            this.iconButton17 = new FontAwesome.Sharp.IconButton();
            this.iconButton18 = new FontAwesome.Sharp.IconButton();
            this.btnPenerimaanForm = new FontAwesome.Sharp.IconButton();
            this.btnlaporan = new FontAwesome.Sharp.IconButton();
            this.iconButton24 = new FontAwesome.Sharp.IconButton();
            this.btnHistori = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.HamburgerButton = new FontAwesome.Sharp.IconButton();
            this.titlePanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.entryContainer.SuspendLayout();
            this.EditContainer.SuspendLayout();
            this.laporanContainer.SuspendLayout();
            this.historycontainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.panel4.Location = new System.Drawing.Point(315, 70);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1313, 825);
            this.panel4.TabIndex = 3;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.headerPanel.Controls.Add(this.lblshift);
            this.headerPanel.Controls.Add(this.lblinfo);
            this.headerPanel.Controls.Add(this.lbldate);
            this.headerPanel.Controls.Add(this.lbluser);
            this.headerPanel.Controls.Add(this.iconButton14);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.headerPanel.Location = new System.Drawing.Point(315, 20);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1313, 50);
            this.headerPanel.TabIndex = 3;
            // 
            // lblshift
            // 
            this.lblshift.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblshift.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblshift.ForeColor = System.Drawing.Color.White;
            this.lblshift.Location = new System.Drawing.Point(470, 0);
            this.lblshift.Margin = new System.Windows.Forms.Padding(20);
            this.lblshift.Name = "lblshift";
            this.lblshift.Size = new System.Drawing.Size(59, 50);
            this.lblshift.TabIndex = 2;
            this.lblshift.Text = "1";
            this.lblshift.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblinfo
            // 
            this.lblinfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblinfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblinfo.ForeColor = System.Drawing.Color.White;
            this.lblinfo.Location = new System.Drawing.Point(377, 0);
            this.lblinfo.Margin = new System.Windows.Forms.Padding(20);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(93, 50);
            this.lblinfo.TabIndex = 1;
            this.lblinfo.Text = "Shift :";
            this.lblinfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbldate
            // 
            this.lbldate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbldate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbldate.ForeColor = System.Drawing.Color.White;
            this.lbldate.Location = new System.Drawing.Point(0, 0);
            this.lbldate.Margin = new System.Windows.Forms.Padding(20);
            this.lbldate.Name = "lbldate";
            this.lbldate.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lbldate.Size = new System.Drawing.Size(377, 50);
            this.lbldate.TabIndex = 0;
            this.lbldate.Text = "Tanggal Dan Jam";
            this.lbldate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbluser
            // 
            this.lbluser.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbluser.Font = new System.Drawing.Font("Century Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbluser.ForeColor = System.Drawing.Color.IndianRed;
            this.lbluser.Location = new System.Drawing.Point(931, 0);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(325, 50);
            this.lbluser.TabIndex = 6;
            this.lbluser.Text = "Robert Danuarta [Manajer]";
            this.lbluser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.sidebarPanel.MaximumSize = new System.Drawing.Size(315, 0);
            this.sidebarPanel.MinimumSize = new System.Drawing.Size(60, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(315, 875);
            this.sidebarPanel.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.dashboardButton);
            this.flowLayoutPanel1.Controls.Add(this.entryContainer);
            this.flowLayoutPanel1.Controls.Add(this.EditContainer);
            this.flowLayoutPanel1.Controls.Add(this.laporanContainer);
            this.flowLayoutPanel1.Controls.Add(this.historycontainer);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 323);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(315, 552);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 47);
            this.panel1.TabIndex = 0;
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
            this.entryContainer.MaximumSize = new System.Drawing.Size(309, 345);
            this.entryContainer.MinimumSize = new System.Drawing.Size(309, 74);
            this.entryContainer.Name = "entryContainer";
            this.entryContainer.Size = new System.Drawing.Size(309, 74);
            this.entryContainer.TabIndex = 2;
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
            this.EditContainer.Location = new System.Drawing.Point(4, 209);
            this.EditContainer.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.EditContainer.MaximumSize = new System.Drawing.Size(309, 345);
            this.EditContainer.MinimumSize = new System.Drawing.Size(309, 74);
            this.EditContainer.Name = "EditContainer";
            this.EditContainer.Size = new System.Drawing.Size(309, 74);
            this.EditContainer.TabIndex = 8;
            // 
            // laporanContainer
            // 
            this.laporanContainer.Controls.Add(this.iconButton10);
            this.laporanContainer.Controls.Add(this.iconButton13);
            this.laporanContainer.Controls.Add(this.iconButton15);
            this.laporanContainer.Controls.Add(this.iconButton16);
            this.laporanContainer.Controls.Add(this.iconButton17);
            this.laporanContainer.Controls.Add(this.iconButton18);
            this.laporanContainer.Controls.Add(this.btnPenerimaanForm);
            this.laporanContainer.Controls.Add(this.btnlaporan);
            this.laporanContainer.Location = new System.Drawing.Point(3, 287);
            this.laporanContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.laporanContainer.MaximumSize = new System.Drawing.Size(309, 479);
            this.laporanContainer.MinimumSize = new System.Drawing.Size(309, 74);
            this.laporanContainer.Name = "laporanContainer";
            this.laporanContainer.Size = new System.Drawing.Size(309, 74);
            this.laporanContainer.TabIndex = 9;
            // 
            // historycontainer
            // 
            this.historycontainer.Controls.Add(this.iconButton24);
            this.historycontainer.Controls.Add(this.btnHistori);
            this.historycontainer.Location = new System.Drawing.Point(3, 365);
            this.historycontainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.historycontainer.MaximumSize = new System.Drawing.Size(309, 479);
            this.historycontainer.MinimumSize = new System.Drawing.Size(309, 74);
            this.historycontainer.Name = "historycontainer";
            this.historycontainer.Size = new System.Drawing.Size(309, 74);
            this.historycontainer.TabIndex = 10;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(315, 261);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(70, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 76);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(315, 62);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(70, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 62);
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
            // historitimer
            // 
            this.historitimer.Interval = 10;
            this.historitimer.Tick += new System.EventHandler(this.historitimer_Tick);
            // 
            // laporantimer
            // 
            this.laporantimer.Interval = 10;
            this.laporantimer.Tick += new System.EventHandler(this.laporantimer_Tick);
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
            this.iconButton14.Location = new System.Drawing.Point(1256, 0);
            this.iconButton14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton14.Name = "iconButton14";
            this.iconButton14.Size = new System.Drawing.Size(57, 50);
            this.iconButton14.TabIndex = 5;
            this.iconButton14.UseVisualStyleBackColor = false;
            this.iconButton14.Click += new System.EventHandler(this.iconButton14_Click);
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
            this.dashboardButton.Size = new System.Drawing.Size(309, 74);
            this.dashboardButton.TabIndex = 1;
            this.dashboardButton.Text = "     Dashboard";
            this.dashboardButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dashboardButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.dashboardButton.UseVisualStyleBackColor = true;
            this.dashboardButton.Click += new System.EventHandler(this.dashboardButton_Click);
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
            this.iconButton4.Size = new System.Drawing.Size(309, 74);
            this.iconButton4.TabIndex = 7;
            this.iconButton4.Text = "     Pemakaian \r\n     Material";
            this.iconButton4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton4.UseVisualStyleBackColor = true;
            this.iconButton4.Click += new System.EventHandler(this.iconButton4_Click);
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
            this.iconButton3.Size = new System.Drawing.Size(309, 49);
            this.iconButton3.TabIndex = 6;
            this.iconButton3.Text = "     Welding Pieces";
            this.iconButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton3.UseVisualStyleBackColor = true;
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click);
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
            this.iconButton2.Size = new System.Drawing.Size(309, 49);
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
            this.iconButton1.Size = new System.Drawing.Size(309, 49);
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
            this.penerimaanButton1.Size = new System.Drawing.Size(309, 49);
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
            this.entryButton.Size = new System.Drawing.Size(309, 74);
            this.entryButton.TabIndex = 2;
            this.entryButton.Text = "     Form Entry Data ";
            this.entryButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.entryButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.entryButton.UseVisualStyleBackColor = true;
            this.entryButton.Click += new System.EventHandler(this.entryButton_Click);
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
            this.iconButton12.Size = new System.Drawing.Size(309, 74);
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
            this.iconButton11.Size = new System.Drawing.Size(309, 49);
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
            this.iconButton5.Size = new System.Drawing.Size(309, 74);
            this.iconButton5.TabIndex = 7;
            this.iconButton5.Text = "     Pemakaian \r\n     Material";
            this.iconButton5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton5.UseVisualStyleBackColor = true;
            this.iconButton5.Click += new System.EventHandler(this.iconButton5_Click);
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
            this.iconButton6.Size = new System.Drawing.Size(309, 49);
            this.iconButton6.TabIndex = 6;
            this.iconButton6.Text = "     Welding Pieces";
            this.iconButton6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton6.UseVisualStyleBackColor = true;
            this.iconButton6.Click += new System.EventHandler(this.iconButton6_Click);
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
            this.iconButton7.Size = new System.Drawing.Size(309, 49);
            this.iconButton7.TabIndex = 5;
            this.iconButton7.Text = "     Pengiriman";
            this.iconButton7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton7.UseVisualStyleBackColor = true;
            this.iconButton7.Click += new System.EventHandler(this.iconButton7_Click);
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
            this.iconButton8.Size = new System.Drawing.Size(309, 49);
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
            this.iconButton9.Size = new System.Drawing.Size(309, 49);
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
            this.Editbutton.Size = new System.Drawing.Size(309, 74);
            this.Editbutton.TabIndex = 2;
            this.Editbutton.Text = "     Form Edit Data ";
            this.Editbutton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Editbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Editbutton.UseVisualStyleBackColor = true;
            this.Editbutton.Click += new System.EventHandler(this.Editbutton_Click);
            // 
            // iconButton10
            // 
            this.iconButton10.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton10.FlatAppearance.BorderSize = 0;
            this.iconButton10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton10.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton10.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton10.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton10.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton10.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton10.IconSize = 40;
            this.iconButton10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton10.Location = new System.Drawing.Point(0, 393);
            this.iconButton10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton10.Name = "iconButton10";
            this.iconButton10.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton10.Size = new System.Drawing.Size(309, 74);
            this.iconButton10.TabIndex = 9;
            this.iconButton10.Text = "     Koefisien \r\n     Material";
            this.iconButton10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton10.UseVisualStyleBackColor = true;
            // 
            // iconButton13
            // 
            this.iconButton13.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton13.FlatAppearance.BorderSize = 0;
            this.iconButton13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton13.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton13.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton13.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton13.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton13.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton13.IconSize = 40;
            this.iconButton13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton13.Location = new System.Drawing.Point(0, 344);
            this.iconButton13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton13.Name = "iconButton13";
            this.iconButton13.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton13.Size = new System.Drawing.Size(309, 49);
            this.iconButton13.TabIndex = 8;
            this.iconButton13.Text = "     Stok Material";
            this.iconButton13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton13.UseVisualStyleBackColor = true;
            // 
            // iconButton15
            // 
            this.iconButton15.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton15.FlatAppearance.BorderSize = 0;
            this.iconButton15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton15.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton15.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton15.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton15.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton15.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton15.IconSize = 40;
            this.iconButton15.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton15.Location = new System.Drawing.Point(0, 270);
            this.iconButton15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton15.Name = "iconButton15";
            this.iconButton15.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton15.Size = new System.Drawing.Size(309, 74);
            this.iconButton15.TabIndex = 7;
            this.iconButton15.Text = "     Pemakaian \r\n     Material";
            this.iconButton15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton15.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton15.UseVisualStyleBackColor = true;
            // 
            // iconButton16
            // 
            this.iconButton16.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton16.FlatAppearance.BorderSize = 0;
            this.iconButton16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton16.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton16.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton16.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton16.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton16.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton16.IconSize = 40;
            this.iconButton16.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton16.Location = new System.Drawing.Point(0, 221);
            this.iconButton16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton16.Name = "iconButton16";
            this.iconButton16.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton16.Size = new System.Drawing.Size(309, 49);
            this.iconButton16.TabIndex = 6;
            this.iconButton16.Text = "     Welding Pieces";
            this.iconButton16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton16.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton16.UseVisualStyleBackColor = true;
            // 
            // iconButton17
            // 
            this.iconButton17.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton17.FlatAppearance.BorderSize = 0;
            this.iconButton17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton17.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton17.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton17.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton17.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton17.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton17.IconSize = 40;
            this.iconButton17.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton17.Location = new System.Drawing.Point(0, 172);
            this.iconButton17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton17.Name = "iconButton17";
            this.iconButton17.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton17.Size = new System.Drawing.Size(309, 49);
            this.iconButton17.TabIndex = 5;
            this.iconButton17.Text = "     Pengiriman";
            this.iconButton17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton17.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton17.UseVisualStyleBackColor = true;
            // 
            // iconButton18
            // 
            this.iconButton18.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton18.FlatAppearance.BorderSize = 0;
            this.iconButton18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton18.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton18.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton18.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton18.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton18.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton18.IconSize = 40;
            this.iconButton18.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton18.Location = new System.Drawing.Point(0, 123);
            this.iconButton18.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton18.Name = "iconButton18";
            this.iconButton18.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton18.Size = new System.Drawing.Size(309, 49);
            this.iconButton18.TabIndex = 4;
            this.iconButton18.Text = "     Perbaikan";
            this.iconButton18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton18.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton18.UseVisualStyleBackColor = true;
            // 
            // btnPenerimaanForm
            // 
            this.btnPenerimaanForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPenerimaanForm.FlatAppearance.BorderSize = 0;
            this.btnPenerimaanForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPenerimaanForm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPenerimaanForm.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnPenerimaanForm.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.btnPenerimaanForm.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnPenerimaanForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPenerimaanForm.IconSize = 40;
            this.btnPenerimaanForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPenerimaanForm.Location = new System.Drawing.Point(0, 74);
            this.btnPenerimaanForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPenerimaanForm.Name = "btnPenerimaanForm";
            this.btnPenerimaanForm.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.btnPenerimaanForm.Size = new System.Drawing.Size(309, 49);
            this.btnPenerimaanForm.TabIndex = 3;
            this.btnPenerimaanForm.Text = "     Form Penerimaan";
            this.btnPenerimaanForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPenerimaanForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPenerimaanForm.UseVisualStyleBackColor = true;
            this.btnPenerimaanForm.Click += new System.EventHandler(this.btnPenerimaanForm_Click);
            // 
            // btnlaporan
            // 
            this.btnlaporan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnlaporan.FlatAppearance.BorderSize = 0;
            this.btnlaporan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlaporan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnlaporan.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnlaporan.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnlaporan.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnlaporan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnlaporan.IconSize = 40;
            this.btnlaporan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlaporan.Location = new System.Drawing.Point(0, 0);
            this.btnlaporan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnlaporan.Name = "btnlaporan";
            this.btnlaporan.Size = new System.Drawing.Size(309, 74);
            this.btnlaporan.TabIndex = 2;
            this.btnlaporan.Text = "     Laporan ";
            this.btnlaporan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlaporan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlaporan.UseVisualStyleBackColor = true;
            this.btnlaporan.Click += new System.EventHandler(this.btnlaporan_Click);
            // 
            // iconButton24
            // 
            this.iconButton24.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton24.FlatAppearance.BorderSize = 0;
            this.iconButton24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton24.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton24.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton24.IconChar = FontAwesome.Sharp.IconChar.PlaneArrival;
            this.iconButton24.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton24.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton24.IconSize = 40;
            this.iconButton24.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton24.Location = new System.Drawing.Point(0, 74);
            this.iconButton24.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconButton24.Name = "iconButton24";
            this.iconButton24.Padding = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.iconButton24.Size = new System.Drawing.Size(309, 49);
            this.iconButton24.TabIndex = 4;
            this.iconButton24.Text = "     Perbaikan";
            this.iconButton24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton24.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton24.UseVisualStyleBackColor = true;
            // 
            // btnHistori
            // 
            this.btnHistori.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHistori.FlatAppearance.BorderSize = 0;
            this.btnHistori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistori.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHistori.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnHistori.IconChar = FontAwesome.Sharp.IconChar.History;
            this.btnHistori.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnHistori.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHistori.IconSize = 40;
            this.btnHistori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistori.Location = new System.Drawing.Point(0, 0);
            this.btnHistori.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHistori.Name = "btnHistori";
            this.btnHistori.Size = new System.Drawing.Size(309, 74);
            this.btnHistori.TabIndex = 2;
            this.btnHistori.Text = "     Data History";
            this.btnHistori.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistori.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHistori.UseVisualStyleBackColor = true;
            this.btnHistori.Click += new System.EventHandler(this.btnHistori_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::GOS_FxApps.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(70, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(242, 181);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
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
            this.sidebarPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.entryContainer.ResumeLayout(false);
            this.EditContainer.ResumeLayout(false);
            this.laporanContainer.ResumeLayout(false);
            this.historycontainer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private Guna.UI2.WinForms.Guna2ControlBox ControlBoxButton;
        private FontAwesome.Sharp.IconButton iconButton7;
        private Label lbldate;
        private System.Windows.Forms.Timer jam;
        private Label lblinfo;
        public Label lblshift;
        private FontAwesome.Sharp.IconButton iconButton14;
        public Label lbluser;
        public Panel laporanContainer;
        private FontAwesome.Sharp.IconButton iconButton10;
        private FontAwesome.Sharp.IconButton iconButton13;
        private FontAwesome.Sharp.IconButton iconButton15;
        public FontAwesome.Sharp.IconButton iconButton16;
        private FontAwesome.Sharp.IconButton iconButton17;
        private FontAwesome.Sharp.IconButton iconButton18;
        private FontAwesome.Sharp.IconButton btnPenerimaanForm;
        private FontAwesome.Sharp.IconButton btnlaporan;
        private FontAwesome.Sharp.IconButton iconButton24;
        private FontAwesome.Sharp.IconButton btnHistori;
        public Panel historycontainer;
        private Timer historitimer;
        private Timer laporantimer;
    }
}
