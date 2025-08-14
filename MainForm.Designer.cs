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
            this.historycontainer = new Guna.UI2.WinForms.Guna2Panel();
            this.iconButton13 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton5 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton6 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton10 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton11 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton24 = new Guna.UI2.WinForms.Guna2Button();
            this.btnHistori = new Guna.UI2.WinForms.Guna2Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbluser = new System.Windows.Forms.Label();
            this.iconButton14 = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblshift = new System.Windows.Forms.Label();
            this.lblinfo = new System.Windows.Forms.Label();
            this.lbldate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.sidebarPanel = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dashboardButton = new Guna.UI2.WinForms.Guna2Button();
            this.entryContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.iconButton15 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton4 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton3 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton2 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton1 = new Guna.UI2.WinForms.Guna2Button();
            this.penerimaanButton1 = new Guna.UI2.WinForms.Guna2Button();
            this.entryButton = new Guna.UI2.WinForms.Guna2Button();
            this.EditContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.iconButton12 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton8 = new Guna.UI2.WinForms.Guna2Button();
            this.iconButton9 = new Guna.UI2.WinForms.Guna2Button();
            this.Editbutton = new Guna.UI2.WinForms.Guna2Button();
            this.btnlaporan = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.titlePanel.SuspendLayout();
            this.historycontainer.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.entryContainer.SuspendLayout();
            this.EditContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.titlePanel.Controls.Add(this.guna2ControlBox1);
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
            this.ControlBoxButton.Location = new System.Drawing.Point(1176, 0);
            this.ControlBoxButton.Margin = new System.Windows.Forms.Padding(2);
            this.ControlBoxButton.Name = "ControlBoxButton";
            this.ControlBoxButton.Size = new System.Drawing.Size(45, 30);
            this.ControlBoxButton.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(254, 80);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(967, 647);
            this.panel4.TabIndex = 3;
            // 
            // historycontainer
            // 
            this.historycontainer.BackColor = System.Drawing.Color.Transparent;
            this.historycontainer.BorderRadius = 8;
            this.historycontainer.Controls.Add(this.iconButton13);
            this.historycontainer.Controls.Add(this.iconButton5);
            this.historycontainer.Controls.Add(this.iconButton6);
            this.historycontainer.Controls.Add(this.iconButton10);
            this.historycontainer.Controls.Add(this.iconButton11);
            this.historycontainer.Controls.Add(this.iconButton24);
            this.historycontainer.Controls.Add(this.btnHistori);
            this.historycontainer.FillColor = System.Drawing.Color.WhiteSmoke;
            this.historycontainer.Location = new System.Drawing.Point(6, 264);
            this.historycontainer.MaximumSize = new System.Drawing.Size(232, 308);
            this.historycontainer.MinimumSize = new System.Drawing.Size(232, 55);
            this.historycontainer.Name = "historycontainer";
            this.historycontainer.Size = new System.Drawing.Size(232, 55);
            this.historycontainer.TabIndex = 14;
            // 
            // iconButton13
            // 
            this.iconButton13.BackColor = System.Drawing.Color.Transparent;
            this.iconButton13.BorderRadius = 5;
            this.iconButton13.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton13.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton13.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton13.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton13.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton13.FillColor = System.Drawing.Color.Transparent;
            this.iconButton13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton13.ForeColor = System.Drawing.Color.Black;
            this.iconButton13.Image = global::GOS_FxApps.Properties.Resources.material_management;
            this.iconButton13.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton13.ImageSize = new System.Drawing.Size(35, 35);
            this.iconButton13.Location = new System.Drawing.Point(0, 255);
            this.iconButton13.Name = "iconButton13";
            this.iconButton13.Size = new System.Drawing.Size(232, 50);
            this.iconButton13.TabIndex = 20;
            this.iconButton13.Text = "Stok Material";
            this.iconButton13.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton13.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton13.Click += new System.EventHandler(this.iconButton13_Click_1);
            // 
            // iconButton5
            // 
            this.iconButton5.BackColor = System.Drawing.Color.Transparent;
            this.iconButton5.BorderRadius = 5;
            this.iconButton5.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton5.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton5.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton5.FillColor = System.Drawing.Color.Transparent;
            this.iconButton5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton5.ForeColor = System.Drawing.Color.Black;
            this.iconButton5.Image = global::GOS_FxApps.Properties.Resources.recycled_material;
            this.iconButton5.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton5.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton5.Location = new System.Drawing.Point(0, 215);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Size = new System.Drawing.Size(232, 40);
            this.iconButton5.TabIndex = 19;
            this.iconButton5.Text = "Pemakaian Material";
            this.iconButton5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton5.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton5.Click += new System.EventHandler(this.iconButton5_Click_1);
            // 
            // iconButton6
            // 
            this.iconButton6.BackColor = System.Drawing.Color.Transparent;
            this.iconButton6.BorderRadius = 5;
            this.iconButton6.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton6.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton6.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton6.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton6.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton6.FillColor = System.Drawing.Color.Transparent;
            this.iconButton6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton6.ForeColor = System.Drawing.Color.Black;
            this.iconButton6.Image = global::GOS_FxApps.Properties.Resources.welding_machine;
            this.iconButton6.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton6.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton6.Location = new System.Drawing.Point(0, 175);
            this.iconButton6.Name = "iconButton6";
            this.iconButton6.Size = new System.Drawing.Size(232, 40);
            this.iconButton6.TabIndex = 18;
            this.iconButton6.Text = "Welding Pieces";
            this.iconButton6.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton6.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton6.Click += new System.EventHandler(this.iconButton6_Click_1);
            // 
            // iconButton10
            // 
            this.iconButton10.BackColor = System.Drawing.Color.Transparent;
            this.iconButton10.BorderRadius = 5;
            this.iconButton10.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton10.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton10.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton10.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton10.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton10.FillColor = System.Drawing.Color.Transparent;
            this.iconButton10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton10.ForeColor = System.Drawing.Color.Black;
            this.iconButton10.Image = global::GOS_FxApps.Properties.Resources.delivery;
            this.iconButton10.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton10.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton10.Location = new System.Drawing.Point(0, 135);
            this.iconButton10.Name = "iconButton10";
            this.iconButton10.Size = new System.Drawing.Size(232, 40);
            this.iconButton10.TabIndex = 17;
            this.iconButton10.Text = "Pengiriman";
            this.iconButton10.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton10.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton10.Click += new System.EventHandler(this.iconButton10_Click_1);
            // 
            // iconButton11
            // 
            this.iconButton11.BackColor = System.Drawing.Color.Transparent;
            this.iconButton11.BorderRadius = 5;
            this.iconButton11.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton11.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton11.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton11.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton11.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton11.FillColor = System.Drawing.Color.Transparent;
            this.iconButton11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton11.ForeColor = System.Drawing.Color.Black;
            this.iconButton11.Image = global::GOS_FxApps.Properties.Resources.tool_box;
            this.iconButton11.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton11.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton11.Location = new System.Drawing.Point(0, 95);
            this.iconButton11.Name = "iconButton11";
            this.iconButton11.Size = new System.Drawing.Size(232, 40);
            this.iconButton11.TabIndex = 16;
            this.iconButton11.Text = "Perbaikan";
            this.iconButton11.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton11.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton11.Click += new System.EventHandler(this.iconButton11_Click_1);
            // 
            // iconButton24
            // 
            this.iconButton24.BackColor = System.Drawing.Color.Transparent;
            this.iconButton24.BorderRadius = 5;
            this.iconButton24.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton24.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton24.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton24.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton24.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton24.FillColor = System.Drawing.Color.Transparent;
            this.iconButton24.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton24.ForeColor = System.Drawing.Color.Black;
            this.iconButton24.Image = global::GOS_FxApps.Properties.Resources.delivery_service1;
            this.iconButton24.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton24.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton24.Location = new System.Drawing.Point(0, 55);
            this.iconButton24.Name = "iconButton24";
            this.iconButton24.Size = new System.Drawing.Size(232, 40);
            this.iconButton24.TabIndex = 15;
            this.iconButton24.Text = "Penerimaan";
            this.iconButton24.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton24.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton24.Click += new System.EventHandler(this.iconButton24_Click_1);
            // 
            // btnHistori
            // 
            this.btnHistori.BorderRadius = 5;
            this.btnHistori.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHistori.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHistori.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHistori.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHistori.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHistori.FillColor = System.Drawing.Color.White;
            this.btnHistori.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHistori.ForeColor = System.Drawing.Color.Black;
            this.btnHistori.Image = global::GOS_FxApps.Properties.Resources.history;
            this.btnHistori.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHistori.ImageSize = new System.Drawing.Size(30, 30);
            this.btnHistori.Location = new System.Drawing.Point(0, 0);
            this.btnHistori.Name = "btnHistori";
            this.btnHistori.Size = new System.Drawing.Size(232, 55);
            this.btnHistori.TabIndex = 14;
            this.btnHistori.Text = "Riwayat Data";
            this.btnHistori.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHistori.TextOffset = new System.Drawing.Point(5, 0);
            this.btnHistori.Click += new System.EventHandler(this.btnHistori_Click);
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.headerPanel.Controls.Add(this.panel3);
            this.headerPanel.Controls.Add(this.panel2);
            this.headerPanel.Controls.Add(this.lbldate);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.headerPanel.Location = new System.Drawing.Point(254, 30);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(967, 50);
            this.headerPanel.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbluser);
            this.panel3.Controls.Add(this.iconButton14);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(449, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(518, 50);
            this.panel3.TabIndex = 8;
            // 
            // lbluser
            // 
            this.lbluser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbluser.Font = new System.Drawing.Font("Century Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbluser.ForeColor = System.Drawing.Color.Black;
            this.lbluser.Location = new System.Drawing.Point(0, 0);
            this.lbluser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(469, 50);
            this.lbluser.TabIndex = 8;
            this.lbluser.Text = "Robert Danuarta [Manajer]";
            this.lbluser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iconButton14
            // 
            this.iconButton14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.iconButton14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iconButton14.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconButton14.FlatAppearance.BorderSize = 0;
            this.iconButton14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton14.ForeColor = System.Drawing.Color.Transparent;
            this.iconButton14.IconChar = FontAwesome.Sharp.IconChar.UserCircle;
            this.iconButton14.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(132)))));
            this.iconButton14.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton14.IconSize = 40;
            this.iconButton14.Location = new System.Drawing.Point(469, 0);
            this.iconButton14.Margin = new System.Windows.Forms.Padding(2);
            this.iconButton14.Name = "iconButton14";
            this.iconButton14.Size = new System.Drawing.Size(49, 50);
            this.iconButton14.TabIndex = 7;
            this.iconButton14.UseVisualStyleBackColor = false;
            this.iconButton14.Click += new System.EventHandler(this.iconButton14_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblshift);
            this.panel2.Controls.Add(this.lblinfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(283, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(166, 50);
            this.panel2.TabIndex = 7;
            // 
            // lblshift
            // 
            this.lblshift.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblshift.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblshift.ForeColor = System.Drawing.Color.Black;
            this.lblshift.Location = new System.Drawing.Point(114, 0);
            this.lblshift.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.lblshift.Name = "lblshift";
            this.lblshift.Size = new System.Drawing.Size(44, 50);
            this.lblshift.TabIndex = 4;
            this.lblshift.Text = "1";
            this.lblshift.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblinfo
            // 
            this.lblinfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblinfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblinfo.ForeColor = System.Drawing.Color.Black;
            this.lblinfo.Location = new System.Drawing.Point(0, 0);
            this.lblinfo.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(114, 50);
            this.lblinfo.TabIndex = 3;
            this.lblinfo.Text = "Shift Aktual :";
            this.lblinfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbldate
            // 
            this.lbldate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbldate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbldate.ForeColor = System.Drawing.Color.Black;
            this.lbldate.Location = new System.Drawing.Point(0, 0);
            this.lbldate.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.lbldate.Name = "lbldate";
            this.lbldate.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbldate.Size = new System.Drawing.Size(283, 50);
            this.lbldate.TabIndex = 0;
            this.lbldate.Text = "Tanggal Dan Jam";
            this.lbldate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 10);
            this.panel1.TabIndex = 0;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(243, 212);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::GOS_FxApps.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(52, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(189, 146);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(52, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 62);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(243, 50);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // HamburgerButton
            // 
            this.HamburgerButton.BackColor = System.Drawing.Color.Transparent;
            this.HamburgerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HamburgerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HamburgerButton.ForeColor = System.Drawing.Color.Transparent;
            this.HamburgerButton.IconChar = FontAwesome.Sharp.IconChar.Navicon;
            this.HamburgerButton.IconColor = System.Drawing.Color.Black;
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
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(52, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "Powered by :\r\nPT Digital Transformasi Industri";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // entrytimer
            // 
            this.entrytimer.Interval = 1;
            this.entrytimer.Tick += new System.EventHandler(this.entrytimer_Tick);
            // 
            // edittimer
            // 
            this.edittimer.Interval = 1;
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
            this.historitimer.Interval = 1;
            this.historitimer.Tick += new System.EventHandler(this.historitimer_Tick);
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sidebarPanel.ColumnCount = 2;
            this.sidebarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.sidebarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sidebarPanel.Controls.Add(this.guna2Panel1, 1, 0);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 30);
            this.sidebarPanel.MaximumSize = new System.Drawing.Size(254, 0);
            this.sidebarPanel.MinimumSize = new System.Drawing.Size(60, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.RowCount = 2;
            this.sidebarPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sidebarPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.sidebarPanel.Size = new System.Drawing.Size(254, 697);
            this.sidebarPanel.TabIndex = 4;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 10;
            this.guna2Panel1.BorderThickness = 2;
            this.guna2Panel1.Controls.Add(this.flowLayoutPanel1);
            this.guna2Panel1.Controls.Add(this.tableLayoutPanel2);
            this.guna2Panel1.Controls.Add(this.tableLayoutPanel1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(8, 3);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(243, 686);
            this.guna2Panel1.TabIndex = 0;
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
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(243, 424);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // dashboardButton
            // 
            this.dashboardButton.BorderRadius = 8;
            this.dashboardButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.dashboardButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.dashboardButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.dashboardButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.dashboardButton.FillColor = System.Drawing.Color.White;
            this.dashboardButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dashboardButton.ForeColor = System.Drawing.Color.Black;
            this.dashboardButton.Image = global::GOS_FxApps.Properties.Resources.dashboard;
            this.dashboardButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dashboardButton.ImageSize = new System.Drawing.Size(30, 30);
            this.dashboardButton.Location = new System.Drawing.Point(6, 20);
            this.dashboardButton.Name = "dashboardButton";
            this.dashboardButton.Size = new System.Drawing.Size(231, 55);
            this.dashboardButton.TabIndex = 13;
            this.dashboardButton.Text = "Dashboard";
            this.dashboardButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dashboardButton.TextOffset = new System.Drawing.Point(5, 0);
            this.dashboardButton.Click += new System.EventHandler(this.dashboardButton_Click_1);
            // 
            // entryContainer
            // 
            this.entryContainer.BackColor = System.Drawing.Color.Transparent;
            this.entryContainer.BorderRadius = 8;
            this.entryContainer.Controls.Add(this.iconButton15);
            this.entryContainer.Controls.Add(this.iconButton4);
            this.entryContainer.Controls.Add(this.iconButton3);
            this.entryContainer.Controls.Add(this.iconButton2);
            this.entryContainer.Controls.Add(this.iconButton1);
            this.entryContainer.Controls.Add(this.penerimaanButton1);
            this.entryContainer.Controls.Add(this.entryButton);
            this.entryContainer.FillColor = System.Drawing.Color.WhiteSmoke;
            this.entryContainer.Location = new System.Drawing.Point(6, 81);
            this.entryContainer.MaximumSize = new System.Drawing.Size(231, 297);
            this.entryContainer.MinimumSize = new System.Drawing.Size(231, 55);
            this.entryContainer.Name = "entryContainer";
            this.entryContainer.Size = new System.Drawing.Size(231, 55);
            this.entryContainer.TabIndex = 15;
            // 
            // iconButton15
            // 
            this.iconButton15.BorderRadius = 8;
            this.iconButton15.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton15.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton15.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton15.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton15.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton15.FillColor = System.Drawing.Color.Transparent;
            this.iconButton15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton15.ForeColor = System.Drawing.Color.Black;
            this.iconButton15.Image = global::GOS_FxApps.Properties.Resources.corporate;
            this.iconButton15.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton15.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton15.Location = new System.Drawing.Point(0, 255);
            this.iconButton15.Name = "iconButton15";
            this.iconButton15.Size = new System.Drawing.Size(231, 40);
            this.iconButton15.TabIndex = 19;
            this.iconButton15.Text = "Butt Ratio dan Man Power";
            this.iconButton15.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton15.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton15.Click += new System.EventHandler(this.iconButton15_Click_1);
            // 
            // iconButton4
            // 
            this.iconButton4.BorderRadius = 8;
            this.iconButton4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton4.FillColor = System.Drawing.Color.Transparent;
            this.iconButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton4.ForeColor = System.Drawing.Color.Black;
            this.iconButton4.Image = global::GOS_FxApps.Properties.Resources.recycled_material;
            this.iconButton4.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton4.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton4.Location = new System.Drawing.Point(0, 215);
            this.iconButton4.Name = "iconButton4";
            this.iconButton4.Size = new System.Drawing.Size(231, 40);
            this.iconButton4.TabIndex = 18;
            this.iconButton4.Text = "Pemakaian Material";
            this.iconButton4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton4.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton4.Click += new System.EventHandler(this.iconButton4_Click_1);
            // 
            // iconButton3
            // 
            this.iconButton3.BorderRadius = 8;
            this.iconButton3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton3.FillColor = System.Drawing.Color.Transparent;
            this.iconButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton3.ForeColor = System.Drawing.Color.Black;
            this.iconButton3.Image = global::GOS_FxApps.Properties.Resources.welding_machine;
            this.iconButton3.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton3.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton3.Location = new System.Drawing.Point(0, 175);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Size = new System.Drawing.Size(231, 40);
            this.iconButton3.TabIndex = 20;
            this.iconButton3.Text = "Welding Pieces";
            this.iconButton3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton3.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click_1);
            // 
            // iconButton2
            // 
            this.iconButton2.BorderRadius = 8;
            this.iconButton2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton2.FillColor = System.Drawing.Color.Transparent;
            this.iconButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton2.ForeColor = System.Drawing.Color.Black;
            this.iconButton2.Image = global::GOS_FxApps.Properties.Resources.delivery;
            this.iconButton2.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton2.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton2.Location = new System.Drawing.Point(0, 135);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(231, 40);
            this.iconButton2.TabIndex = 17;
            this.iconButton2.Text = "Pengiriman";
            this.iconButton2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton2.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click_1);
            // 
            // iconButton1
            // 
            this.iconButton1.BorderRadius = 8;
            this.iconButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton1.FillColor = System.Drawing.Color.Transparent;
            this.iconButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton1.ForeColor = System.Drawing.Color.Black;
            this.iconButton1.Image = global::GOS_FxApps.Properties.Resources.tool_box;
            this.iconButton1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton1.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton1.Location = new System.Drawing.Point(0, 95);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(231, 40);
            this.iconButton1.TabIndex = 16;
            this.iconButton1.Text = "Perbaikan";
            this.iconButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton1.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click_1);
            // 
            // penerimaanButton1
            // 
            this.penerimaanButton1.BorderRadius = 8;
            this.penerimaanButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.penerimaanButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.penerimaanButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.penerimaanButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.penerimaanButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.penerimaanButton1.FillColor = System.Drawing.Color.Transparent;
            this.penerimaanButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.penerimaanButton1.ForeColor = System.Drawing.Color.Black;
            this.penerimaanButton1.Image = global::GOS_FxApps.Properties.Resources.delivery_service;
            this.penerimaanButton1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.penerimaanButton1.ImageSize = new System.Drawing.Size(30, 30);
            this.penerimaanButton1.Location = new System.Drawing.Point(0, 55);
            this.penerimaanButton1.Name = "penerimaanButton1";
            this.penerimaanButton1.Size = new System.Drawing.Size(231, 40);
            this.penerimaanButton1.TabIndex = 15;
            this.penerimaanButton1.Text = "Penerimaan";
            this.penerimaanButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.penerimaanButton1.TextOffset = new System.Drawing.Point(5, 0);
            this.penerimaanButton1.Click += new System.EventHandler(this.penerimaanButton1_Click_1);
            // 
            // entryButton
            // 
            this.entryButton.BorderRadius = 8;
            this.entryButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.entryButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.entryButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.entryButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.entryButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.entryButton.FillColor = System.Drawing.Color.White;
            this.entryButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.entryButton.ForeColor = System.Drawing.Color.Black;
            this.entryButton.Image = global::GOS_FxApps.Properties.Resources.data_entry;
            this.entryButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.entryButton.ImageSize = new System.Drawing.Size(30, 30);
            this.entryButton.Location = new System.Drawing.Point(0, 0);
            this.entryButton.Name = "entryButton";
            this.entryButton.Size = new System.Drawing.Size(231, 55);
            this.entryButton.TabIndex = 14;
            this.entryButton.Text = "Form Entry Data";
            this.entryButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.entryButton.TextOffset = new System.Drawing.Point(5, 0);
            this.entryButton.Click += new System.EventHandler(this.entryButton_Click_1);
            // 
            // EditContainer
            // 
            this.EditContainer.BorderRadius = 8;
            this.EditContainer.Controls.Add(this.iconButton12);
            this.EditContainer.Controls.Add(this.iconButton8);
            this.EditContainer.Controls.Add(this.iconButton9);
            this.EditContainer.Controls.Add(this.Editbutton);
            this.EditContainer.FillColor = System.Drawing.Color.WhiteSmoke;
            this.EditContainer.Location = new System.Drawing.Point(6, 142);
            this.EditContainer.MaximumSize = new System.Drawing.Size(231, 178);
            this.EditContainer.MinimumSize = new System.Drawing.Size(231, 55);
            this.EditContainer.Name = "EditContainer";
            this.EditContainer.Size = new System.Drawing.Size(231, 55);
            this.EditContainer.TabIndex = 14;
            // 
            // iconButton12
            // 
            this.iconButton12.BorderRadius = 8;
            this.iconButton12.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton12.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton12.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton12.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton12.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton12.FillColor = System.Drawing.Color.Transparent;
            this.iconButton12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton12.ForeColor = System.Drawing.Color.Black;
            this.iconButton12.Image = global::GOS_FxApps.Properties.Resources.optimization;
            this.iconButton12.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton12.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton12.Location = new System.Drawing.Point(0, 135);
            this.iconButton12.Name = "iconButton12";
            this.iconButton12.Size = new System.Drawing.Size(231, 40);
            this.iconButton12.TabIndex = 17;
            this.iconButton12.Text = "Koefisiensi Material";
            this.iconButton12.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton12.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton12.Click += new System.EventHandler(this.iconButton12_Click);
            // 
            // iconButton8
            // 
            this.iconButton8.BorderRadius = 8;
            this.iconButton8.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton8.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton8.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton8.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton8.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton8.FillColor = System.Drawing.Color.Transparent;
            this.iconButton8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton8.ForeColor = System.Drawing.Color.Black;
            this.iconButton8.Image = global::GOS_FxApps.Properties.Resources.tool_box;
            this.iconButton8.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton8.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton8.Location = new System.Drawing.Point(0, 95);
            this.iconButton8.Name = "iconButton8";
            this.iconButton8.Size = new System.Drawing.Size(231, 40);
            this.iconButton8.TabIndex = 16;
            this.iconButton8.Text = "Perbaikan";
            this.iconButton8.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton8.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton8.Click += new System.EventHandler(this.iconButton8_Click_1);
            // 
            // iconButton9
            // 
            this.iconButton9.BorderRadius = 8;
            this.iconButton9.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.iconButton9.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.iconButton9.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iconButton9.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.iconButton9.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton9.FillColor = System.Drawing.Color.Transparent;
            this.iconButton9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.iconButton9.ForeColor = System.Drawing.Color.Black;
            this.iconButton9.Image = global::GOS_FxApps.Properties.Resources.delivery_service1;
            this.iconButton9.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton9.ImageSize = new System.Drawing.Size(30, 30);
            this.iconButton9.Location = new System.Drawing.Point(0, 55);
            this.iconButton9.Name = "iconButton9";
            this.iconButton9.Size = new System.Drawing.Size(231, 40);
            this.iconButton9.TabIndex = 15;
            this.iconButton9.Text = "Penerimaan";
            this.iconButton9.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.iconButton9.TextOffset = new System.Drawing.Point(5, 0);
            this.iconButton9.Click += new System.EventHandler(this.iconButton9_Click_1);
            // 
            // Editbutton
            // 
            this.Editbutton.BorderRadius = 8;
            this.Editbutton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Editbutton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Editbutton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Editbutton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Editbutton.Dock = System.Windows.Forms.DockStyle.Top;
            this.Editbutton.FillColor = System.Drawing.Color.White;
            this.Editbutton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Editbutton.ForeColor = System.Drawing.Color.Black;
            this.Editbutton.Image = global::GOS_FxApps.Properties.Resources.edit_tools;
            this.Editbutton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Editbutton.ImageSize = new System.Drawing.Size(30, 30);
            this.Editbutton.Location = new System.Drawing.Point(0, 0);
            this.Editbutton.Name = "Editbutton";
            this.Editbutton.Size = new System.Drawing.Size(231, 55);
            this.Editbutton.TabIndex = 14;
            this.Editbutton.Text = "Form Edit Data";
            this.Editbutton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Editbutton.TextOffset = new System.Drawing.Point(5, 0);
            this.Editbutton.Click += new System.EventHandler(this.Editbutton_Click_1);
            // 
            // btnlaporan
            // 
            this.btnlaporan.BorderRadius = 8;
            this.btnlaporan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnlaporan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnlaporan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnlaporan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnlaporan.FillColor = System.Drawing.Color.White;
            this.btnlaporan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnlaporan.ForeColor = System.Drawing.Color.Black;
            this.btnlaporan.Image = global::GOS_FxApps.Properties.Resources.pdf;
            this.btnlaporan.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnlaporan.ImageSize = new System.Drawing.Size(30, 30);
            this.btnlaporan.Location = new System.Drawing.Point(6, 203);
            this.btnlaporan.Name = "btnlaporan";
            this.btnlaporan.Size = new System.Drawing.Size(231, 55);
            this.btnlaporan.TabIndex = 14;
            this.btnlaporan.Text = "Laporan";
            this.btnlaporan.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnlaporan.TextOffset = new System.Drawing.Point(5, 0);
            this.btnlaporan.Click += new System.EventHandler(this.btnlaporan_Click_1);
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.Black;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1137, 0);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(39, 30);
            this.guna2ControlBox1.TabIndex = 1;
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
            this.historycontainer.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.sidebarPanel.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.entryContainer.ResumeLayout(false);
            this.EditContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel titlePanel;
        private Panel panel4;
        private Panel headerPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private FontAwesome.Sharp.IconButton HamburgerButton;
        private TableLayoutPanel tableLayoutPanel2;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private System.Windows.Forms.Timer entrytimer;
        private System.Windows.Forms.Timer edittimer;
        private Guna.UI2.WinForms.Guna2ControlBox ControlBoxButton;
        public Label lbldate;
        private System.Windows.Forms.Timer jam;
        private Timer historitimer;
        private Panel panel3;
        public Label lbluser;
        private FontAwesome.Sharp.IconButton iconButton14;
        private Panel panel2;
        public Label lblshift;
        private Label lblinfo;
        private TableLayoutPanel sidebarPanel;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        public Guna.UI2.WinForms.Guna2Button dashboardButton;
        private FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Button entryButton;
        private Guna.UI2.WinForms.Guna2Button iconButton15;
        private Guna.UI2.WinForms.Guna2Button iconButton4;
        private Guna.UI2.WinForms.Guna2Button iconButton2;
        private Guna.UI2.WinForms.Guna2Button iconButton1;
        private Guna.UI2.WinForms.Guna2Button penerimaanButton1;
        private Guna.UI2.WinForms.Guna2Button iconButton3;
        private Guna.UI2.WinForms.Guna2Button iconButton12;
        private Guna.UI2.WinForms.Guna2Button iconButton8;
        private Guna.UI2.WinForms.Guna2Button iconButton9;
        private Guna.UI2.WinForms.Guna2Button Editbutton;
        private Guna.UI2.WinForms.Guna2Button btnlaporan;
        private Guna.UI2.WinForms.Guna2Button iconButton13;
        private Guna.UI2.WinForms.Guna2Button iconButton5;
        private Guna.UI2.WinForms.Guna2Button iconButton6;
        private Guna.UI2.WinForms.Guna2Button iconButton10;
        private Guna.UI2.WinForms.Guna2Button iconButton11;
        private Guna.UI2.WinForms.Guna2Button iconButton24;
        private Guna.UI2.WinForms.Guna2Button btnHistori;
        public Guna.UI2.WinForms.Guna2Panel entryContainer;
        public Guna.UI2.WinForms.Guna2Panel EditContainer;
        public Guna.UI2.WinForms.Guna2Panel historycontainer;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}
