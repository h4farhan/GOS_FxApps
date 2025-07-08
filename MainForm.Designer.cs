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
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbluser = new System.Windows.Forms.Label();
            this.iconButton14 = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblshift = new System.Windows.Forms.Label();
            this.lblinfo = new System.Windows.Forms.Label();
            this.lbldate = new System.Windows.Forms.Label();
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
            this.iconButton7 = new FontAwesome.Sharp.IconButton();
            this.iconButton8 = new FontAwesome.Sharp.IconButton();
            this.iconButton9 = new FontAwesome.Sharp.IconButton();
            this.Editbutton = new FontAwesome.Sharp.IconButton();
            this.btnlaporan = new FontAwesome.Sharp.IconButton();
            this.historycontainer = new System.Windows.Forms.Panel();
            this.iconButton24 = new FontAwesome.Sharp.IconButton();
            this.btnHistori = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.HamburgerButton = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.entrytimer = new System.Windows.Forms.Timer(this.components);
            this.edittimer = new System.Windows.Forms.Timer(this.components);
            this.jam = new System.Windows.Forms.Timer(this.components);
            this.historitimer = new System.Windows.Forms.Timer(this.components);
            this.titlePanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.entryContainer.SuspendLayout();
            this.EditContainer.SuspendLayout();
            this.historycontainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.titlePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.titlePanel.Controls.Add(this.ControlBoxButton);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Margin = new System.Windows.Forms.Padding(2);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(1221, 30);
            this.titlePanel.TabIndex = 1;
            // 
            // ControlBoxButton
            // 
            this.ControlBoxButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlBoxButton.FillColor = System.Drawing.Color.Transparent;
            this.ControlBoxButton.IconColor = System.Drawing.Color.Red;
            this.ControlBoxButton.Location = new System.Drawing.Point(1192, 0);
            this.ControlBoxButton.Margin = new System.Windows.Forms.Padding(2);
            this.ControlBoxButton.Name = "ControlBoxButton";
            this.ControlBoxButton.Size = new System.Drawing.Size(25, 26);
            this.ControlBoxButton.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(236, 80);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(985, 647);
            this.panel4.TabIndex = 3;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headerPanel.Controls.Add(this.panel3);
            this.headerPanel.Controls.Add(this.panel2);
            this.headerPanel.Controls.Add(this.lbldate);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.headerPanel.Location = new System.Drawing.Point(236, 30);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(985, 50);
            this.headerPanel.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.lbluser);
            this.panel3.Controls.Add(this.iconButton14);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(628, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(353, 46);
            this.panel3.TabIndex = 8;
            // 
            // lbluser
            // 
            this.lbluser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbluser.Font = new System.Drawing.Font("Century Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbluser.ForeColor = System.Drawing.Color.IndianRed;
            this.lbluser.Location = new System.Drawing.Point(0, 0);
            this.lbluser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(300, 42);
            this.lbluser.TabIndex = 8;
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
            this.iconButton14.Location = new System.Drawing.Point(300, 0);
            this.iconButton14.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton14.Name = "iconButton14";
            this.iconButton14.Size = new System.Drawing.Size(49, 42);
            this.iconButton14.TabIndex = 7;
            this.iconButton14.UseVisualStyleBackColor = false;
            this.iconButton14.Click += new System.EventHandler(this.iconButton14_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblshift);
            this.panel2.Controls.Add(this.lblinfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(283, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(166, 46);
            this.panel2.TabIndex = 7;
            // 
            // lblshift
            // 
            this.lblshift.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblshift.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblshift.ForeColor = System.Drawing.Color.White;
            this.lblshift.Location = new System.Drawing.Point(114, 0);
            this.lblshift.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.lblshift.Name = "lblshift";
            this.lblshift.Size = new System.Drawing.Size(44, 42);
            this.lblshift.TabIndex = 4;
            this.lblshift.Text = "1";
            this.lblshift.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblinfo
            // 
            this.lblinfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblinfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblinfo.ForeColor = System.Drawing.Color.White;
            this.lblinfo.Location = new System.Drawing.Point(0, 0);
            this.lblinfo.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(114, 42);
            this.lblinfo.TabIndex = 3;
            this.lblinfo.Text = "Shift Aktual :";
            this.lblinfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbldate
            // 
            this.lbldate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbldate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbldate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbldate.ForeColor = System.Drawing.Color.White;
            this.lbldate.Location = new System.Drawing.Point(0, 0);
            this.lbldate.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.lbldate.Name = "lbldate";
            this.lbldate.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lbldate.Size = new System.Drawing.Size(283, 46);
            this.lbldate.TabIndex = 0;
            this.lbldate.Text = "Tanggal Dan Jam";
            this.lbldate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.sidebarPanel.Controls.Add(this.flowLayoutPanel1);
            this.sidebarPanel.Controls.Add(this.tableLayoutPanel2);
            this.sidebarPanel.Controls.Add(this.tableLayoutPanel1);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 30);
            this.sidebarPanel.Margin = new System.Windows.Forms.Padding(2);
            this.sidebarPanel.MaximumSize = new System.Drawing.Size(236, 0);
            this.sidebarPanel.MinimumSize = new System.Drawing.Size(45, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(236, 697);
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
            this.flowLayoutPanel1.Controls.Add(this.historycontainer);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 262);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(236, 435);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 38);
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
            this.dashboardButton.Location = new System.Drawing.Point(2, 44);
            this.dashboardButton.Margin = new System.Windows.Forms.Padding(2);
            this.dashboardButton.Name = "dashboardButton";
            this.dashboardButton.Size = new System.Drawing.Size(232, 60);
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
            this.entryContainer.Location = new System.Drawing.Point(2, 108);
            this.entryContainer.Margin = new System.Windows.Forms.Padding(2);
            this.entryContainer.MaximumSize = new System.Drawing.Size(232, 280);
            this.entryContainer.MinimumSize = new System.Drawing.Size(232, 60);
            this.entryContainer.Name = "entryContainer";
            this.entryContainer.Size = new System.Drawing.Size(232, 60);
            this.entryContainer.TabIndex = 2;
            // 
            // iconButton4
            // 
            this.iconButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton4.FlatAppearance.BorderSize = 0;
            this.iconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton4.IconChar = FontAwesome.Sharp.IconChar.Warehouse;
            this.iconButton4.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton4.IconSize = 40;
            this.iconButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.Location = new System.Drawing.Point(0, 220);
            this.iconButton4.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton4.Name = "iconButton4";
            this.iconButton4.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton4.Size = new System.Drawing.Size(232, 60);
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
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.Wind;
            this.iconButton3.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.IconSize = 40;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.Location = new System.Drawing.Point(0, 180);
            this.iconButton3.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton3.Size = new System.Drawing.Size(232, 40);
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
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.PlaneDeparture;
            this.iconButton2.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 40;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(0, 140);
            this.iconButton2.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton2.Size = new System.Drawing.Size(232, 40);
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
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Toolbox;
            this.iconButton1.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 40;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(0, 100);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton1.Size = new System.Drawing.Size(232, 40);
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
            this.penerimaanButton1.Location = new System.Drawing.Point(0, 60);
            this.penerimaanButton1.Margin = new System.Windows.Forms.Padding(2);
            this.penerimaanButton1.Name = "penerimaanButton1";
            this.penerimaanButton1.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.penerimaanButton1.Size = new System.Drawing.Size(232, 40);
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
            this.entryButton.Margin = new System.Windows.Forms.Padding(2);
            this.entryButton.Name = "entryButton";
            this.entryButton.Size = new System.Drawing.Size(232, 60);
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
            this.EditContainer.Controls.Add(this.iconButton7);
            this.EditContainer.Controls.Add(this.iconButton8);
            this.EditContainer.Controls.Add(this.iconButton9);
            this.EditContainer.Controls.Add(this.Editbutton);
            this.EditContainer.Location = new System.Drawing.Point(3, 172);
            this.EditContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditContainer.MaximumSize = new System.Drawing.Size(232, 241);
            this.EditContainer.MinimumSize = new System.Drawing.Size(232, 60);
            this.EditContainer.Name = "EditContainer";
            this.EditContainer.Size = new System.Drawing.Size(232, 60);
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
            this.iconButton12.Location = new System.Drawing.Point(0, 180);
            this.iconButton12.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton12.Name = "iconButton12";
            this.iconButton12.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton12.Size = new System.Drawing.Size(232, 60);
            this.iconButton12.TabIndex = 9;
            this.iconButton12.Text = "     Koefisien \r\n     Material";
            this.iconButton12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton12.UseVisualStyleBackColor = true;
            // 
            // iconButton7
            // 
            this.iconButton7.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton7.FlatAppearance.BorderSize = 0;
            this.iconButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconButton7.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton7.IconChar = FontAwesome.Sharp.IconChar.PlaneDeparture;
            this.iconButton7.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton7.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton7.IconSize = 40;
            this.iconButton7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton7.Location = new System.Drawing.Point(0, 140);
            this.iconButton7.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton7.Name = "iconButton7";
            this.iconButton7.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton7.Size = new System.Drawing.Size(232, 40);
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
            this.iconButton8.IconChar = FontAwesome.Sharp.IconChar.Toolbox;
            this.iconButton8.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton8.IconSize = 40;
            this.iconButton8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton8.Location = new System.Drawing.Point(0, 100);
            this.iconButton8.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton8.Name = "iconButton8";
            this.iconButton8.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton8.Size = new System.Drawing.Size(232, 40);
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
            this.iconButton9.Location = new System.Drawing.Point(0, 60);
            this.iconButton9.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton9.Name = "iconButton9";
            this.iconButton9.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton9.Size = new System.Drawing.Size(232, 40);
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
            this.Editbutton.Margin = new System.Windows.Forms.Padding(2);
            this.Editbutton.Name = "Editbutton";
            this.Editbutton.Size = new System.Drawing.Size(232, 60);
            this.Editbutton.TabIndex = 2;
            this.Editbutton.Text = "     Form Edit Data ";
            this.Editbutton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Editbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Editbutton.UseVisualStyleBackColor = true;
            this.Editbutton.Click += new System.EventHandler(this.Editbutton_Click);
            // 
            // btnlaporan
            // 
            this.btnlaporan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnlaporan.FlatAppearance.BorderSize = 0;
            this.btnlaporan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlaporan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnlaporan.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnlaporan.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.btnlaporan.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnlaporan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnlaporan.IconSize = 40;
            this.btnlaporan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlaporan.Location = new System.Drawing.Point(2, 236);
            this.btnlaporan.Margin = new System.Windows.Forms.Padding(2);
            this.btnlaporan.Name = "btnlaporan";
            this.btnlaporan.Size = new System.Drawing.Size(232, 60);
            this.btnlaporan.TabIndex = 11;
            this.btnlaporan.Text = "     Laporan ";
            this.btnlaporan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlaporan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlaporan.UseVisualStyleBackColor = true;
            this.btnlaporan.Click += new System.EventHandler(this.btnlaporan_Click);
            // 
            // historycontainer
            // 
            this.historycontainer.Controls.Add(this.iconButton24);
            this.historycontainer.Controls.Add(this.btnHistori);
            this.historycontainer.Location = new System.Drawing.Point(2, 300);
            this.historycontainer.Margin = new System.Windows.Forms.Padding(2);
            this.historycontainer.MaximumSize = new System.Drawing.Size(232, 389);
            this.historycontainer.MinimumSize = new System.Drawing.Size(232, 60);
            this.historycontainer.Name = "historycontainer";
            this.historycontainer.Size = new System.Drawing.Size(232, 60);
            this.historycontainer.TabIndex = 12;
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
            this.iconButton24.Location = new System.Drawing.Point(0, 60);
            this.iconButton24.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton24.Name = "iconButton24";
            this.iconButton24.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.iconButton24.Size = new System.Drawing.Size(232, 40);
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
            this.btnHistori.Margin = new System.Windows.Forms.Padding(2);
            this.btnHistori.Name = "btnHistori";
            this.btnHistori.Size = new System.Drawing.Size(232, 60);
            this.btnHistori.TabIndex = 2;
            this.btnHistori.Text = "     Data History";
            this.btnHistori.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistori.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHistori.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 50);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(236, 212);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::GOS_FxApps.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(52, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(182, 146);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(52, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 62);
            this.label2.TabIndex = 2;
            this.label2.Text = "PT\r\nGENTANUSA GEMILANG";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.HamburgerButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(236, 50);
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
            this.HamburgerButton.Location = new System.Drawing.Point(2, 2);
            this.HamburgerButton.Margin = new System.Windows.Forms.Padding(2);
            this.HamburgerButton.Name = "HamburgerButton";
            this.HamburgerButton.Size = new System.Drawing.Size(46, 46);
            this.HamburgerButton.TabIndex = 0;
            this.HamburgerButton.UseVisualStyleBackColor = false;
            this.HamburgerButton.Click += new System.EventHandler(this.HamburgerButton_Click_1);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(52, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 50);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 727);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.titlePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.titlePanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.sidebarPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.entryContainer.ResumeLayout(false);
            this.EditContainer.ResumeLayout(false);
            this.historycontainer.ResumeLayout(false);
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
        public Panel entryContainer;
        private FontAwesome.Sharp.IconButton penerimaanButton1;
        private FontAwesome.Sharp.IconButton entryButton;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.Timer entrytimer;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButton3;
        public Panel EditContainer;
        private FontAwesome.Sharp.IconButton iconButton8;
        private FontAwesome.Sharp.IconButton iconButton9;
        private FontAwesome.Sharp.IconButton Editbutton;
        private FontAwesome.Sharp.IconButton iconButton12;
        private System.Windows.Forms.Timer edittimer;
        private Guna.UI2.WinForms.Guna2ControlBox ControlBoxButton;
        private FontAwesome.Sharp.IconButton iconButton7;
        private Label lbldate;
        private System.Windows.Forms.Timer jam;
        private Timer historitimer;
        private FontAwesome.Sharp.IconButton btnlaporan;
        public Panel historycontainer;
        private FontAwesome.Sharp.IconButton iconButton24;
        private FontAwesome.Sharp.IconButton btnHistori;
        private Panel panel3;
        public Label lbluser;
        private FontAwesome.Sharp.IconButton iconButton14;
        private Panel panel2;
        public Label lblshift;
        private Label lblinfo;
    }
}
