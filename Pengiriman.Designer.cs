using System.Drawing;
using System.Windows.Forms;

namespace GOS_FxApps
{
    partial class Pengiriman
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.txtrod10 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtrod9 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtrod8 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtrod7 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtrod6 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtrod5 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtrod4 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtrod3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtrod2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtrod1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnclear = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.btncari = new Guna.UI2.WinForms.Guna2Button();
            this.datecari = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtcari = new Guna.UI2.WinForms.Guna2TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 58);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(141, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pengiriman";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Form Entry Data  /";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(405, 656);
            this.panel2.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Controls.Add(this.guna2Panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(405, 656);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 2;
            this.guna2Panel1.Controls.Add(this.txtrod10);
            this.guna2Panel1.Controls.Add(this.txtrod9);
            this.guna2Panel1.Controls.Add(this.txtrod8);
            this.guna2Panel1.Controls.Add(this.txtrod7);
            this.guna2Panel1.Controls.Add(this.txtrod6);
            this.guna2Panel1.Controls.Add(this.txtrod5);
            this.guna2Panel1.Controls.Add(this.txtrod4);
            this.guna2Panel1.Controls.Add(this.txtrod3);
            this.guna2Panel1.Controls.Add(this.txtrod2);
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.txtrod1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(18, 2);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(376, 645);
            this.guna2Panel1.TabIndex = 2;
            // 
            // txtrod10
            // 
            this.txtrod10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod10.BorderRadius = 5;
            this.txtrod10.BorderThickness = 2;
            this.txtrod10.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod10.DefaultText = "";
            this.txtrod10.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod10.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod10.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod10.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod10.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod10.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod10.ForeColor = System.Drawing.Color.Black;
            this.txtrod10.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod10.Location = new System.Drawing.Point(51, 492);
            this.txtrod10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod10.Name = "txtrod10";
            this.txtrod10.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod10.PlaceholderText = "4xxxx";
            this.txtrod10.SelectedText = "";
            this.txtrod10.Size = new System.Drawing.Size(282, 41);
            this.txtrod10.TabIndex = 9;
            this.txtrod10.TextChanged += new System.EventHandler(this.txtrod10_TextChanged);
            this.txtrod10.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod10.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // txtrod9
            // 
            this.txtrod9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod9.BorderRadius = 5;
            this.txtrod9.BorderThickness = 2;
            this.txtrod9.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod9.DefaultText = "";
            this.txtrod9.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod9.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod9.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod9.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod9.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod9.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod9.ForeColor = System.Drawing.Color.Black;
            this.txtrod9.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod9.Location = new System.Drawing.Point(51, 443);
            this.txtrod9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod9.Name = "txtrod9";
            this.txtrod9.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod9.PlaceholderText = "4xxxx";
            this.txtrod9.SelectedText = "";
            this.txtrod9.Size = new System.Drawing.Size(282, 41);
            this.txtrod9.TabIndex = 8;
            this.txtrod9.TextChanged += new System.EventHandler(this.txtrod9_TextChanged);
            this.txtrod9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod9.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // txtrod8
            // 
            this.txtrod8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod8.BorderRadius = 5;
            this.txtrod8.BorderThickness = 2;
            this.txtrod8.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod8.DefaultText = "";
            this.txtrod8.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod8.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod8.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod8.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod8.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod8.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod8.ForeColor = System.Drawing.Color.Black;
            this.txtrod8.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod8.Location = new System.Drawing.Point(51, 394);
            this.txtrod8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod8.Name = "txtrod8";
            this.txtrod8.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod8.PlaceholderText = "4xxxx";
            this.txtrod8.SelectedText = "";
            this.txtrod8.Size = new System.Drawing.Size(282, 41);
            this.txtrod8.TabIndex = 7;
            this.txtrod8.TextChanged += new System.EventHandler(this.txtrod8_TextChanged);
            this.txtrod8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod8.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // txtrod7
            // 
            this.txtrod7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod7.BorderRadius = 5;
            this.txtrod7.BorderThickness = 2;
            this.txtrod7.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod7.DefaultText = "";
            this.txtrod7.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod7.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod7.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod7.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod7.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod7.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod7.ForeColor = System.Drawing.Color.Black;
            this.txtrod7.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod7.Location = new System.Drawing.Point(51, 345);
            this.txtrod7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod7.Name = "txtrod7";
            this.txtrod7.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod7.PlaceholderText = "4xxxx";
            this.txtrod7.SelectedText = "";
            this.txtrod7.Size = new System.Drawing.Size(282, 41);
            this.txtrod7.TabIndex = 6;
            this.txtrod7.TextChanged += new System.EventHandler(this.txtrod7_TextChanged);
            this.txtrod7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod7.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // txtrod6
            // 
            this.txtrod6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod6.BorderRadius = 5;
            this.txtrod6.BorderThickness = 2;
            this.txtrod6.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod6.DefaultText = "";
            this.txtrod6.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod6.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod6.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod6.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod6.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod6.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod6.ForeColor = System.Drawing.Color.Black;
            this.txtrod6.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod6.Location = new System.Drawing.Point(51, 297);
            this.txtrod6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod6.Name = "txtrod6";
            this.txtrod6.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod6.PlaceholderText = "4xxxx";
            this.txtrod6.SelectedText = "";
            this.txtrod6.Size = new System.Drawing.Size(282, 41);
            this.txtrod6.TabIndex = 5;
            this.txtrod6.TextChanged += new System.EventHandler(this.txtrod6_TextChanged);
            this.txtrod6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod6.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // txtrod5
            // 
            this.txtrod5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod5.BorderRadius = 5;
            this.txtrod5.BorderThickness = 2;
            this.txtrod5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod5.DefaultText = "";
            this.txtrod5.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod5.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod5.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod5.ForeColor = System.Drawing.Color.Black;
            this.txtrod5.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod5.Location = new System.Drawing.Point(51, 248);
            this.txtrod5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod5.Name = "txtrod5";
            this.txtrod5.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod5.PlaceholderText = "4xxxx";
            this.txtrod5.SelectedText = "";
            this.txtrod5.Size = new System.Drawing.Size(282, 41);
            this.txtrod5.TabIndex = 4;
            this.txtrod5.TextChanged += new System.EventHandler(this.txtrod5_TextChanged);
            this.txtrod5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod5.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // txtrod4
            // 
            this.txtrod4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod4.BorderRadius = 5;
            this.txtrod4.BorderThickness = 2;
            this.txtrod4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod4.DefaultText = "";
            this.txtrod4.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod4.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod4.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod4.ForeColor = System.Drawing.Color.Black;
            this.txtrod4.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod4.Location = new System.Drawing.Point(51, 199);
            this.txtrod4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod4.Name = "txtrod4";
            this.txtrod4.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod4.PlaceholderText = "4xxxx";
            this.txtrod4.SelectedText = "";
            this.txtrod4.Size = new System.Drawing.Size(282, 41);
            this.txtrod4.TabIndex = 3;
            this.txtrod4.TextChanged += new System.EventHandler(this.txtrod4_TextChanged);
            this.txtrod4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod4.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // txtrod3
            // 
            this.txtrod3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod3.BorderRadius = 5;
            this.txtrod3.BorderThickness = 2;
            this.txtrod3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod3.DefaultText = "";
            this.txtrod3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod3.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod3.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod3.ForeColor = System.Drawing.Color.Black;
            this.txtrod3.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod3.Location = new System.Drawing.Point(51, 150);
            this.txtrod3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod3.Name = "txtrod3";
            this.txtrod3.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod3.PlaceholderText = "4xxxx";
            this.txtrod3.SelectedText = "";
            this.txtrod3.Size = new System.Drawing.Size(282, 41);
            this.txtrod3.TabIndex = 2;
            this.txtrod3.TextChanged += new System.EventHandler(this.txtrod3_TextChanged);
            this.txtrod3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod3.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // txtrod2
            // 
            this.txtrod2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod2.BorderRadius = 5;
            this.txtrod2.BorderThickness = 2;
            this.txtrod2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod2.DefaultText = "";
            this.txtrod2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod2.ForeColor = System.Drawing.Color.Black;
            this.txtrod2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod2.Location = new System.Drawing.Point(51, 102);
            this.txtrod2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod2.Name = "txtrod2";
            this.txtrod2.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod2.PlaceholderText = "4xxxx";
            this.txtrod2.SelectedText = "";
            this.txtrod2.Size = new System.Drawing.Size(282, 41);
            this.txtrod2.TabIndex = 1;
            this.txtrod2.TextChanged += new System.EventHandler(this.txtrod2_TextChanged);
            this.txtrod2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod2.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(100, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Masukkan Nomor ROD";
            // 
            // txtrod1
            // 
            this.txtrod1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtrod1.BorderRadius = 5;
            this.txtrod1.BorderThickness = 2;
            this.txtrod1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrod1.DefaultText = "";
            this.txtrod1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtrod1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtrod1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtrod1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtrod1.ForeColor = System.Drawing.Color.Black;
            this.txtrod1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtrod1.Location = new System.Drawing.Point(51, 53);
            this.txtrod1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrod1.Name = "txtrod1";
            this.txtrod1.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtrod1.PlaceholderText = "4xxxx";
            this.txtrod1.SelectedText = "";
            this.txtrod1.Size = new System.Drawing.Size(282, 41);
            this.txtrod1.TabIndex = 0;
            this.txtrod1.TextChanged += new System.EventHandler(this.txtrod1_TextChanged);
            this.txtrod1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            this.txtrod1.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.btnclear);
            this.panel3.Controls.Add(this.guna2Button2);
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(405, 58);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(330, 656);
            this.panel3.TabIndex = 3;
            // 
            // btnclear
            // 
            this.btnclear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclear.BorderRadius = 8;
            this.btnclear.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnclear.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnclear.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnclear.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnclear.Enabled = false;
            this.btnclear.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnclear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnclear.ForeColor = System.Drawing.Color.White;
            this.btnclear.Location = new System.Drawing.Point(69, 595);
            this.btnclear.Margin = new System.Windows.Forms.Padding(2);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(122, 35);
            this.btnclear.TabIndex = 2;
            this.btnclear.Text = "Batal";
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // guna2Button2
            // 
            this.guna2Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Button2.BorderRadius = 8;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.Enabled = false;
            this.guna2Button2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.Location = new System.Drawing.Point(196, 595);
            this.guna2Button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(122, 35);
            this.guna2Button2.TabIndex = 0;
            this.guna2Button2.Text = "Kirim";
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel2.Controls.Add(this.guna2Panel2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(330, 592);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.guna2Panel2.BorderRadius = 15;
            this.guna2Panel2.BorderThickness = 2;
            this.guna2Panel2.Controls.Add(this.btncari);
            this.guna2Panel2.Controls.Add(this.datecari);
            this.guna2Panel2.Controls.Add(this.txtcari);
            this.guna2Panel2.Controls.Add(this.dataGridView1);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel2.FillColor = System.Drawing.Color.White;
            this.guna2Panel2.Location = new System.Drawing.Point(3, 2);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(316, 582);
            this.guna2Panel2.TabIndex = 2;
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
            this.btncari.Location = new System.Drawing.Point(244, 18);
            this.btncari.Margin = new System.Windows.Forms.Padding(2);
            this.btncari.Name = "btncari";
            this.btncari.Size = new System.Drawing.Size(60, 37);
            this.btncari.TabIndex = 2;
            this.btncari.Text = "Cari";
            this.btncari.Click += new System.EventHandler(this.btncari_Click);
            // 
            // datecari
            // 
            this.datecari.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.datecari.BorderRadius = 5;
            this.datecari.BorderThickness = 2;
            this.datecari.Checked = true;
            this.datecari.FillColor = System.Drawing.Color.White;
            this.datecari.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.datecari.ForeColor = System.Drawing.Color.Black;
            this.datecari.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.datecari.Location = new System.Drawing.Point(11, 16);
            this.datecari.Margin = new System.Windows.Forms.Padding(2);
            this.datecari.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datecari.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datecari.Name = "datecari";
            this.datecari.ShowCheckBox = true;
            this.datecari.Size = new System.Drawing.Size(178, 39);
            this.datecari.TabIndex = 0;
            this.datecari.Value = new System.DateTime(2025, 5, 20, 10, 10, 59, 90);
            // 
            // txtcari
            // 
            this.txtcari.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtcari.Location = new System.Drawing.Point(193, 16);
            this.txtcari.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtcari.Name = "txtcari";
            this.txtcari.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtcari.PlaceholderText = "Nomor ROD";
            this.txtcari.SelectedText = "";
            this.txtcari.Size = new System.Drawing.Size(47, 39);
            this.txtcari.TabIndex = 1;
            this.txtcari.TextOffset = new System.Drawing.Point(5, 0);
            this.txtcari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AngkaOnly_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
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
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.Gray;
            this.dataGridView1.Location = new System.Drawing.Point(11, 59);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(293, 512);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Pengiriman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(735, 714);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Pengiriman";
            this.Text = "Pengiriman";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Pengiriman_FormClosing);
            this.Load += new System.EventHandler(this.Pengiriman_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label2;
        private Label label1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox txtrod10;
        private Guna.UI2.WinForms.Guna2TextBox txtrod9;
        private Guna.UI2.WinForms.Guna2TextBox txtrod8;
        private Guna.UI2.WinForms.Guna2TextBox txtrod7;
        private Guna.UI2.WinForms.Guna2TextBox txtrod6;
        private Guna.UI2.WinForms.Guna2TextBox txtrod5;
        private Guna.UI2.WinForms.Guna2TextBox txtrod4;
        private Guna.UI2.WinForms.Guna2TextBox txtrod3;
        private Guna.UI2.WinForms.Guna2TextBox txtrod2;
        private Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtrod1;
        private Panel panel3;
        private TableLayoutPanel tableLayoutPanel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2Button btncari;
        private Guna.UI2.WinForms.Guna2DateTimePicker datecari;
        private Guna.UI2.WinForms.Guna2TextBox txtcari;
        private Guna.UI2.WinForms.Guna2Button btnclear;
    }
}