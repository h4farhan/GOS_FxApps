using System.Drawing;
using System.Windows.Forms;

namespace GOS_FxApps
{
    partial class Dashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblubrepaired = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.chartUssageMaterial = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label21 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.chartRoundbar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label18 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel6 = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartUssageMaterial)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRoundbar)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.guna2Panel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.guna2Panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 34F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(62)))));
            this.label15.Location = new System.Drawing.Point(5, 38);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(312, 109);
            this.label15.TabIndex = 3;
            this.label15.Text = "1426";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.label14.Location = new System.Drawing.Point(5, 147);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(312, 33);
            this.label14.TabIndex = 2;
            this.label14.Text = "Pcs";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.label16.Location = new System.Drawing.Point(5, 5);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(312, 33);
            this.label16.TabIndex = 0;
            this.label16.Text = "Stok Batang Yang Sudah Diperbaiki";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblubrepaired
            // 
            this.lblubrepaired.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblubrepaired.Font = new System.Drawing.Font("Segoe UI", 34F, System.Drawing.FontStyle.Bold);
            this.lblubrepaired.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(62)))));
            this.lblubrepaired.Location = new System.Drawing.Point(5, 38);
            this.lblubrepaired.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblubrepaired.Name = "lblubrepaired";
            this.lblubrepaired.Size = new System.Drawing.Size(312, 109);
            this.lblubrepaired.TabIndex = 3;
            this.lblubrepaired.Text = "1426";
            this.lblubrepaired.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.label11.Location = new System.Drawing.Point(5, 147);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(312, 33);
            this.label11.TabIndex = 2;
            this.label11.Text = "Pcs";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.label13.Location = new System.Drawing.Point(5, 5);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(312, 33);
            this.label13.TabIndex = 0;
            this.label13.Text = "Stok Batang Yang Belum Diperbaiki";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 34F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(62)))));
            this.label3.Location = new System.Drawing.Point(5, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 109);
            this.label3.TabIndex = 3;
            this.label3.Text = "1426";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.label4.Location = new System.Drawing.Point(5, 147);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(312, 33);
            this.label4.TabIndex = 2;
            this.label4.Text = "Pcs";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "Jumlah Batang di Bengkel";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.chartUssageMaterial, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label21, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(972, 106);
            this.tableLayoutPanel7.TabIndex = 3;
            // 
            // chartUssageMaterial
            // 
            this.chartUssageMaterial.BackColor = System.Drawing.Color.Transparent;
            this.chartUssageMaterial.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Gainsboro;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.SelectionColor = System.Drawing.Color.Black;
            chartArea1.CursorY.SelectionColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.chartUssageMaterial.ChartAreas.Add(chartArea1);
            this.chartUssageMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartUssageMaterial.Location = new System.Drawing.Point(2, 45);
            this.chartUssageMaterial.Margin = new System.Windows.Forms.Padding(2);
            this.chartUssageMaterial.Name = "chartUssageMaterial";
            this.chartUssageMaterial.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.ChartArea = "ChartArea1";
            series1.Font = new System.Drawing.Font("Segoe UI", 8F);
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chartUssageMaterial.Series.Add(series1);
            this.chartUssageMaterial.Size = new System.Drawing.Size(968, 59);
            this.chartUssageMaterial.TabIndex = 12;
            this.chartUssageMaterial.Text = "ChartRoundbar";
            // 
            // label21
            // 
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(62)))));
            this.label21.Location = new System.Drawing.Point(2, 0);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(968, 43);
            this.label21.TabIndex = 11;
            this.label21.Text = "10 Material Dengan Stok Rendah";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 10;
            this.guna2Panel1.BorderThickness = 2;
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.label4);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(13, 3);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel1.Size = new System.Drawing.Size(322, 185);
            this.guna2Panel1.TabIndex = 0;
            // 
            // chartRoundbar
            // 
            this.chartRoundbar.BackColor = System.Drawing.Color.Transparent;
            this.chartRoundbar.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            chartArea2.AxisX.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            chartArea2.AxisY.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea2.CursorX.SelectionColor = System.Drawing.Color.Black;
            chartArea2.CursorY.LineColor = System.Drawing.Color.Black;
            chartArea2.CursorY.SelectionColor = System.Drawing.Color.Black;
            chartArea2.Name = "ChartArea1";
            chartArea2.ShadowColor = System.Drawing.Color.Black;
            this.chartRoundbar.ChartAreas.Add(chartArea2);
            this.chartRoundbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartRoundbar.Location = new System.Drawing.Point(2, 62);
            this.chartRoundbar.Margin = new System.Windows.Forms.Padding(2);
            this.chartRoundbar.Name = "chartRoundbar";
            this.chartRoundbar.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series2.ChartArea = "ChartArea1";
            series2.Font = new System.Drawing.Font("Segoe UI", 8F);
            series2.IsValueShownAsLabel = true;
            series2.IsVisibleInLegend = false;
            series2.IsXValueIndexed = true;
            series2.Name = "Series1";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chartRoundbar.Series.Add(series2);
            this.chartRoundbar.Size = new System.Drawing.Size(968, 258);
            this.chartRoundbar.TabIndex = 6;
            this.chartRoundbar.Text = "ChartRoundbar";
            // 
            // label18
            // 
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(62)))));
            this.label18.Location = new System.Drawing.Point(2, 0);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(968, 60);
            this.label18.TabIndex = 5;
            this.label18.Text = "Stok Round Bar Dan Welding Pieces";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.tableLayoutPanel1);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1008, 191);
            this.guna2Panel2.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.guna2Panel4, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.guna2Panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.guna2Panel3, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 191);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel3.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel3.BorderRadius = 10;
            this.guna2Panel3.BorderThickness = 2;
            this.guna2Panel3.Controls.Add(this.lblubrepaired);
            this.guna2Panel3.Controls.Add(this.label13);
            this.guna2Panel3.Controls.Add(this.label11);
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel3.FillColor = System.Drawing.Color.White;
            this.guna2Panel3.Location = new System.Drawing.Point(342, 3);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel3.Size = new System.Drawing.Size(322, 185);
            this.guna2Panel3.TabIndex = 1;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel4.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel4.BorderRadius = 10;
            this.guna2Panel4.BorderThickness = 2;
            this.guna2Panel4.Controls.Add(this.label15);
            this.guna2Panel4.Controls.Add(this.label16);
            this.guna2Panel4.Controls.Add(this.label14);
            this.guna2Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel4.FillColor = System.Drawing.Color.White;
            this.guna2Panel4.Location = new System.Drawing.Point(671, 3);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel4.Size = new System.Drawing.Size(322, 185);
            this.guna2Panel4.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chartRoundbar, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.89401F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.10599F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(972, 322);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.Controls.Add(this.guna2Panel5, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 191);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1008, 340);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel5.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel5.BorderRadius = 10;
            this.guna2Panel5.BorderThickness = 2;
            this.guna2Panel5.Controls.Add(this.tableLayoutPanel2);
            this.guna2Panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel5.FillColor = System.Drawing.Color.White;
            this.guna2Panel5.Location = new System.Drawing.Point(13, 4);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel5.Size = new System.Drawing.Size(982, 332);
            this.guna2Panel5.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.Controls.Add(this.guna2Panel6, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 531);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1008, 123);
            this.tableLayoutPanel4.TabIndex = 11;
            // 
            // guna2Panel6
            // 
            this.guna2Panel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel6.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel6.BorderRadius = 10;
            this.guna2Panel6.BorderThickness = 2;
            this.guna2Panel6.Controls.Add(this.tableLayoutPanel7);
            this.guna2Panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel6.FillColor = System.Drawing.Color.White;
            this.guna2Panel6.Location = new System.Drawing.Point(13, 3);
            this.guna2Panel6.Name = "guna2Panel6";
            this.guna2Panel6.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel6.Size = new System.Drawing.Size(982, 116);
            this.guna2Panel6.TabIndex = 12;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1008, 654);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartUssageMaterial)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRoundbar)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.guna2Panel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.guna2Panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Label label3;
        private Label label4;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label21;
        private Label label15;
        private Label label14;
        private Label label16;
        private Label lblubrepaired;
        private Label label11;
        private Label label13;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUssageMaterial;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRoundbar;
        private Label label18;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private TableLayoutPanel tableLayoutPanel4;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel6;
    }
}