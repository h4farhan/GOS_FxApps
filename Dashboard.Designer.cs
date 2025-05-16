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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            label18 = new Label();
            chartRoundbar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tableLayoutPanel4 = new TableLayoutPanel();
            label1 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            panel5 = new Panel();
            label15 = new Label();
            label14 = new Label();
            label16 = new Label();
            panel4 = new Panel();
            label12 = new Label();
            label11 = new Label();
            label13 = new Label();
            panel3 = new Panel();
            label9 = new Label();
            label8 = new Label();
            label10 = new Label();
            panel2 = new Panel();
            label6 = new Label();
            label5 = new Label();
            label7 = new Label();
            panel1 = new Panel();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            label17 = new Label();
            chartLowestStock = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tableLayoutPanel7 = new TableLayoutPanel();
            label21 = new Label();
            label19 = new Label();
            tableLayoutPanel8 = new TableLayoutPanel();
            tableLayoutPanel10 = new TableLayoutPanel();
            dgvRepaired = new Guna.UI2.WinForms.Guna2DataGridView();
            label22 = new Label();
            tableLayoutPanel9 = new TableLayoutPanel();
            label20 = new Label();
            dgvReceived = new Guna.UI2.WinForms.Guna2DataGridView();
            chartUssageMaterial = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartStockMaterial = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartRoundbar).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartLowestStock).BeginInit();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRepaired).BeginInit();
            tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReceived).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartUssageMaterial).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartStockMaterial).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1159, 500);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel6, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(805, 494);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(label18, 0, 0);
            tableLayoutPanel6.Controls.Add(chartRoundbar, 0, 1);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 200);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(799, 291);
            tableLayoutPanel6.TabIndex = 4;
            // 
            // label18
            // 
            label18.Dock = DockStyle.Fill;
            label18.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label18.ForeColor = Color.Gainsboro;
            label18.Location = new Point(3, 0);
            label18.Name = "label18";
            label18.Size = new Size(793, 30);
            label18.TabIndex = 5;
            label18.Text = "Stock Round Bar and Welding Pieces";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // chartRoundbar
            // 
            chartRoundbar.BackColor = Color.Transparent;
            chartRoundbar.BorderlineColor = Color.Transparent;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea1.AxisX.LabelStyle.ForeColor = Color.Gainsboro;
            chartArea1.AxisX.LineColor = Color.Gainsboro;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea1.AxisY.LabelStyle.ForeColor = Color.Gainsboro;
            chartArea1.AxisY.LineColor = Color.Gainsboro;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.LineColor = Color.Gainsboro;
            chartArea1.AxisY.TitleForeColor = Color.Gainsboro;
            chartArea1.BackColor = Color.Transparent;
            chartArea1.BackSecondaryColor = Color.Transparent;
            chartArea1.Name = "ChartArea1";
            chartRoundbar.ChartAreas.Add(chartArea1);
            chartRoundbar.Dock = DockStyle.Fill;
            chartRoundbar.Location = new Point(3, 33);
            chartRoundbar.Name = "chartRoundbar";
            chartRoundbar.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Font = new Font("Segoe UI", 8F);
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.LabelForeColor = Color.Gainsboro;
            series1.Name = "Series1";
            chartRoundbar.Series.Add(series1);
            chartRoundbar.Size = new Size(793, 255);
            chartRoundbar.TabIndex = 6;
            chartRoundbar.Text = "ChartRoundbar";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(label1, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(799, 191);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label1.ForeColor = Color.Gainsboro;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(793, 29);
            label1.TabIndex = 2;
            label1.Text = "Stock Amount of Rod";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 5;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel5.Controls.Add(panel5, 4, 0);
            tableLayoutPanel5.Controls.Add(panel4, 3, 0);
            tableLayoutPanel5.Controls.Add(panel3, 2, 0);
            tableLayoutPanel5.Controls.Add(panel2, 1, 0);
            tableLayoutPanel5.Controls.Add(panel1, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 32);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.Padding = new Padding(0, 5, 0, 0);
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(793, 156);
            tableLayoutPanel5.TabIndex = 3;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.Fixed3D;
            panel5.Controls.Add(label15);
            panel5.Controls.Add(label14);
            panel5.Controls.Add(label16);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(635, 8);
            panel5.Name = "panel5";
            panel5.Size = new Size(155, 145);
            panel5.TabIndex = 4;
            // 
            // label15
            // 
            label15.Dock = DockStyle.Fill;
            label15.Font = new Font("Segoe UI", 34F, FontStyle.Bold);
            label15.ForeColor = Color.DarkTurquoise;
            label15.Location = new Point(0, 55);
            label15.Name = "label15";
            label15.Size = new Size(151, 64);
            label15.TabIndex = 3;
            label15.Text = "1426";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.BackColor = Color.Transparent;
            label14.Dock = DockStyle.Bottom;
            label14.Font = new Font("Segoe UI", 10F);
            label14.ForeColor = Color.DarkTurquoise;
            label14.Location = new Point(0, 119);
            label14.Name = "label14";
            label14.Size = new Size(151, 22);
            label14.TabIndex = 2;
            label14.Text = "Pcs";
            label14.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            label16.Dock = DockStyle.Top;
            label16.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label16.ForeColor = Color.DarkTurquoise;
            label16.Location = new Point(0, 0);
            label16.Name = "label16";
            label16.Size = new Size(151, 55);
            label16.TabIndex = 0;
            label16.Text = "Stock of \r\nRepaired rods";
            label16.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(label12);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(label13);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(477, 8);
            panel4.Name = "panel4";
            panel4.Size = new Size(152, 145);
            panel4.TabIndex = 3;
            // 
            // label12
            // 
            label12.Dock = DockStyle.Fill;
            label12.Font = new Font("Segoe UI", 34F, FontStyle.Bold);
            label12.ForeColor = Color.DarkOrange;
            label12.Location = new Point(0, 55);
            label12.Name = "label12";
            label12.Size = new Size(148, 64);
            label12.TabIndex = 3;
            label12.Text = "1426";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.BackColor = Color.Transparent;
            label11.Dock = DockStyle.Bottom;
            label11.Font = new Font("Segoe UI", 10F);
            label11.ForeColor = Color.DarkOrange;
            label11.Location = new Point(0, 119);
            label11.Name = "label11";
            label11.Size = new Size(148, 22);
            label11.TabIndex = 2;
            label11.Text = "Pcs";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            label13.Dock = DockStyle.Top;
            label13.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label13.ForeColor = Color.DarkOrange;
            label13.Location = new Point(0, 0);
            label13.Name = "label13";
            label13.Size = new Size(148, 55);
            label13.TabIndex = 0;
            label13.Text = "Stock of \r\nunrepaired rods";
            label13.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(label10);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(319, 8);
            panel3.Name = "panel3";
            panel3.Size = new Size(152, 145);
            panel3.TabIndex = 2;
            // 
            // label9
            // 
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Segoe UI", 34F, FontStyle.Bold);
            label9.ForeColor = Color.RoyalBlue;
            label9.Location = new Point(0, 55);
            label9.Name = "label9";
            label9.Size = new Size(148, 64);
            label9.TabIndex = 3;
            label9.Text = "1426";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.BackColor = Color.Transparent;
            label8.Dock = DockStyle.Bottom;
            label8.Font = new Font("Segoe UI", 10F);
            label8.ForeColor = Color.RoyalBlue;
            label8.Location = new Point(0, 119);
            label8.Name = "label8";
            label8.Size = new Size(148, 22);
            label8.TabIndex = 2;
            label8.Text = "Pcs";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            label10.Dock = DockStyle.Top;
            label10.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label10.ForeColor = Color.RoyalBlue;
            label10.Location = new Point(0, 0);
            label10.Name = "label10";
            label10.Size = new Size(148, 55);
            label10.TabIndex = 0;
            label10.Text = "Rods Shipped \r\nthis month";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label7);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(161, 8);
            panel2.Name = "panel2";
            panel2.Size = new Size(152, 145);
            panel2.TabIndex = 1;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 34F, FontStyle.Bold);
            label6.ForeColor = Color.DarkViolet;
            label6.Location = new Point(0, 55);
            label6.Name = "label6";
            label6.Size = new Size(148, 64);
            label6.TabIndex = 3;
            label6.Text = "1426";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Dock = DockStyle.Bottom;
            label5.Font = new Font("Segoe UI", 10F);
            label5.ForeColor = Color.DarkViolet;
            label5.Location = new Point(0, 119);
            label5.Name = "label5";
            label5.Size = new Size(148, 22);
            label5.TabIndex = 2;
            label5.Text = "Pcs";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.Dock = DockStyle.Top;
            label7.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label7.ForeColor = Color.DarkViolet;
            label7.Location = new Point(0, 0);
            label7.Name = "label7";
            label7.Size = new Size(148, 55);
            label7.TabIndex = 0;
            label7.Text = "Rods Repaired \r\nthis month";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(152, 145);
            panel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 34F, FontStyle.Bold);
            label3.ForeColor = Color.DeepPink;
            label3.Location = new Point(0, 55);
            label3.Name = "label3";
            label3.Size = new Size(148, 64);
            label3.TabIndex = 3;
            label3.Text = "1426";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Dock = DockStyle.Bottom;
            label4.Font = new Font("Segoe UI", 10F);
            label4.ForeColor = Color.DeepPink;
            label4.Location = new Point(0, 119);
            label4.Name = "label4";
            label4.Size = new Size(148, 22);
            label4.TabIndex = 2;
            label4.Text = "Pcs";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.ForeColor = Color.DeepPink;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(148, 55);
            label2.TabIndex = 0;
            label2.Text = "Rods Received \r\nthis month";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(label17, 0, 0);
            tableLayoutPanel3.Controls.Add(chartLowestStock, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(814, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(342, 494);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // label17
            // 
            label17.Dock = DockStyle.Fill;
            label17.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label17.ForeColor = Color.Gainsboro;
            label17.Location = new Point(3, 0);
            label17.Name = "label17";
            label17.Size = new Size(336, 30);
            label17.TabIndex = 5;
            label17.Text = "5 Materials with the lowest stock";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // chartLowestStock
            // 
            chartLowestStock.BackColor = Color.Transparent;
            chartLowestStock.BackgroundImageLayout = ImageLayout.None;
            chartArea2.Area3DStyle.Enable3D = true;
            chartArea2.Area3DStyle.Rotation = 180;
            chartArea2.AxisX.LineColor = Color.Transparent;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX2.LineColor = Color.Transparent;
            chartArea2.AxisX2.MajorGrid.Enabled = false;
            chartArea2.AxisY.LineColor = Color.Transparent;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY2.LineColor = Color.Transparent;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.BackColor = Color.Transparent;
            chartArea2.BackSecondaryColor = Color.Transparent;
            chartArea2.BorderColor = Color.Transparent;
            chartArea2.Name = "ChartArea1";
            chartLowestStock.ChartAreas.Add(chartArea2);
            chartLowestStock.Dock = DockStyle.Fill;
            legend1.Alignment = StringAlignment.Far;
            legend1.BackColor = Color.Transparent;
            legend1.BackSecondaryColor = Color.Transparent;
            legend1.BorderColor = Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Font = new Font("Segoe UI", 8F);
            legend1.ForeColor = Color.Gainsboro;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            chartLowestStock.Legends.Add(legend1);
            chartLowestStock.Location = new Point(3, 33);
            chartLowestStock.Name = "chartLowestStock";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            series2.IsValueShownAsLabel = true;
            series2.IsXValueIndexed = true;
            series2.LabelBackColor = Color.Transparent;
            series2.LabelBorderColor = Color.Transparent;
            series2.LabelBorderWidth = 0;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            dataPoint1.Color = Color.Aqua;
            dataPoint2.Color = Color.Violet;
            dataPoint3.Color = Color.Orange;
            dataPoint4.Color = Color.Yellow;
            dataPoint5.Color = Color.Lime;
            series2.Points.Add(dataPoint1);
            series2.Points.Add(dataPoint2);
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            chartLowestStock.Series.Add(series2);
            chartLowestStock.Size = new Size(336, 458);
            chartLowestStock.TabIndex = 4;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(chartStockMaterial, 0, 3);
            tableLayoutPanel7.Controls.Add(chartUssageMaterial, 0, 1);
            tableLayoutPanel7.Controls.Add(label21, 0, 2);
            tableLayoutPanel7.Controls.Add(label19, 0, 0);
            tableLayoutPanel7.Controls.Add(tableLayoutPanel8, 0, 4);
            tableLayoutPanel7.Dock = DockStyle.Top;
            tableLayoutPanel7.Location = new Point(0, 500);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 5;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 350F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 350F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 400F));
            tableLayoutPanel7.Size = new Size(1159, 1142);
            tableLayoutPanel7.TabIndex = 3;
            // 
            // label21
            // 
            label21.Dock = DockStyle.Fill;
            label21.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label21.ForeColor = Color.Gainsboro;
            label21.Location = new Point(3, 400);
            label21.Name = "label21";
            label21.Size = new Size(1153, 50);
            label21.TabIndex = 8;
            label21.Text = "Stock of Materials";
            label21.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            label19.Dock = DockStyle.Fill;
            label19.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label19.ForeColor = Color.Gainsboro;
            label19.Location = new Point(3, 0);
            label19.Name = "label19";
            label19.Size = new Size(1153, 50);
            label19.TabIndex = 6;
            label19.Text = "Material usage this month";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Controls.Add(tableLayoutPanel10, 1, 0);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel9, 0, 0);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(3, 803);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Size = new Size(1153, 394);
            tableLayoutPanel8.TabIndex = 0;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.ColumnCount = 1;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Controls.Add(dgvRepaired, 0, 1);
            tableLayoutPanel10.Controls.Add(label22, 0, 0);
            tableLayoutPanel10.Dock = DockStyle.Fill;
            tableLayoutPanel10.Location = new Point(579, 3);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 2;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(571, 388);
            tableLayoutPanel10.TabIndex = 2;
            // 
            // dgvRepaired
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvRepaired.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvRepaired.BackgroundColor = Color.FromArgb(26, 26, 26);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvRepaired.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvRepaired.ColumnHeadersHeight = 4;
            dgvRepaired.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvRepaired.DefaultCellStyle = dataGridViewCellStyle3;
            dgvRepaired.Dock = DockStyle.Fill;
            dgvRepaired.GridColor = Color.FromArgb(231, 229, 255);
            dgvRepaired.Location = new Point(3, 53);
            dgvRepaired.Name = "dgvRepaired";
            dgvRepaired.RowHeadersVisible = false;
            dgvRepaired.Size = new Size(565, 332);
            dgvRepaired.TabIndex = 11;
            dgvRepaired.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvRepaired.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvRepaired.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvRepaired.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvRepaired.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvRepaired.ThemeStyle.BackColor = Color.FromArgb(26, 26, 26);
            dgvRepaired.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvRepaired.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvRepaired.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRepaired.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvRepaired.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvRepaired.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRepaired.ThemeStyle.HeaderStyle.Height = 4;
            dgvRepaired.ThemeStyle.ReadOnly = false;
            dgvRepaired.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvRepaired.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRepaired.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvRepaired.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvRepaired.ThemeStyle.RowsStyle.Height = 25;
            dgvRepaired.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvRepaired.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // label22
            // 
            label22.Dock = DockStyle.Fill;
            label22.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label22.ForeColor = Color.Gainsboro;
            label22.Location = new Point(3, 0);
            label22.Name = "label22";
            label22.Size = new Size(565, 50);
            label22.TabIndex = 9;
            label22.Text = "Anode rods aepaired this month";
            label22.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.ColumnCount = 1;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Controls.Add(label20, 0, 0);
            tableLayoutPanel9.Controls.Add(dgvReceived, 0, 1);
            tableLayoutPanel9.Dock = DockStyle.Fill;
            tableLayoutPanel9.Location = new Point(3, 3);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 2;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Size = new Size(570, 388);
            tableLayoutPanel9.TabIndex = 1;
            // 
            // label20
            // 
            label20.Dock = DockStyle.Fill;
            label20.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label20.ForeColor = Color.Gainsboro;
            label20.Location = new Point(3, 0);
            label20.Name = "label20";
            label20.Size = new Size(564, 50);
            label20.TabIndex = 9;
            label20.Text = "Anode rods received this month";
            label20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvReceived
            // 
            dataGridViewCellStyle4.BackColor = Color.White;
            dgvReceived.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvReceived.BackgroundColor = Color.FromArgb(26, 26, 26);
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvReceived.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvReceived.ColumnHeadersHeight = 4;
            dgvReceived.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvReceived.DefaultCellStyle = dataGridViewCellStyle6;
            dgvReceived.Dock = DockStyle.Fill;
            dgvReceived.GridColor = Color.FromArgb(231, 229, 255);
            dgvReceived.Location = new Point(3, 53);
            dgvReceived.Name = "dgvReceived";
            dgvReceived.RowHeadersVisible = false;
            dgvReceived.Size = new Size(564, 332);
            dgvReceived.TabIndex = 10;
            dgvReceived.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvReceived.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvReceived.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvReceived.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvReceived.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvReceived.ThemeStyle.BackColor = Color.FromArgb(26, 26, 26);
            dgvReceived.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvReceived.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvReceived.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvReceived.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvReceived.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvReceived.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvReceived.ThemeStyle.HeaderStyle.Height = 4;
            dgvReceived.ThemeStyle.ReadOnly = false;
            dgvReceived.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvReceived.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReceived.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvReceived.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvReceived.ThemeStyle.RowsStyle.Height = 25;
            dgvReceived.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvReceived.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // chartUssageMaterial
            // 
            chartUssageMaterial.BackColor = Color.Transparent;
            chartUssageMaterial.BorderlineColor = Color.Transparent;
            chartArea4.AxisX.IsLabelAutoFit = false;
            chartArea4.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea4.AxisX.LabelStyle.ForeColor = Color.Gainsboro;
            chartArea4.AxisX.LineColor = Color.Gainsboro;
            chartArea4.AxisX.MajorGrid.Enabled = false;
            chartArea4.AxisY.IsLabelAutoFit = false;
            chartArea4.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea4.AxisY.LabelStyle.ForeColor = Color.Gainsboro;
            chartArea4.AxisY.LineColor = Color.Gainsboro;
            chartArea4.AxisY.MajorGrid.Enabled = false;
            chartArea4.AxisY.MajorTickMark.LineColor = Color.Gainsboro;
            chartArea4.AxisY.TitleForeColor = Color.Gainsboro;
            chartArea4.BackColor = Color.Transparent;
            chartArea4.BackSecondaryColor = Color.Transparent;
            chartArea4.Name = "ChartArea1";
            chartUssageMaterial.ChartAreas.Add(chartArea4);
            chartUssageMaterial.Dock = DockStyle.Fill;
            chartUssageMaterial.Location = new Point(3, 53);
            chartUssageMaterial.Name = "chartUssageMaterial";
            chartUssageMaterial.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series4.ChartArea = "ChartArea1";
            series4.Font = new Font("Segoe UI", 8F);
            series4.IsValueShownAsLabel = true;
            series4.IsVisibleInLegend = false;
            series4.IsXValueIndexed = true;
            series4.LabelForeColor = Color.Gainsboro;
            series4.Name = "Series1";
            chartUssageMaterial.Series.Add(series4);
            chartUssageMaterial.Size = new Size(1153, 344);
            chartUssageMaterial.TabIndex = 9;
            chartUssageMaterial.Text = "ChartRoundbar";
            // 
            // chartStockMaterial
            // 
            chartStockMaterial.BackColor = Color.Transparent;
            chartStockMaterial.BorderlineColor = Color.Transparent;
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea3.AxisX.LabelStyle.ForeColor = Color.Gainsboro;
            chartArea3.AxisX.LineColor = Color.Gainsboro;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea3.AxisY.LabelStyle.ForeColor = Color.Gainsboro;
            chartArea3.AxisY.LineColor = Color.Gainsboro;
            chartArea3.AxisY.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorTickMark.LineColor = Color.Gainsboro;
            chartArea3.AxisY.TitleForeColor = Color.Gainsboro;
            chartArea3.BackColor = Color.Transparent;
            chartArea3.BackSecondaryColor = Color.Transparent;
            chartArea3.Name = "ChartArea1";
            chartStockMaterial.ChartAreas.Add(chartArea3);
            chartStockMaterial.Dock = DockStyle.Fill;
            chartStockMaterial.Location = new Point(3, 453);
            chartStockMaterial.Name = "chartStockMaterial";
            chartStockMaterial.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series3.ChartArea = "ChartArea1";
            series3.Font = new Font("Segoe UI", 8F);
            series3.IsValueShownAsLabel = true;
            series3.IsVisibleInLegend = false;
            series3.IsXValueIndexed = true;
            series3.LabelForeColor = Color.Gainsboro;
            series3.Name = "Series1";
            chartStockMaterial.Series.Add(series3);
            chartStockMaterial.Size = new Size(1153, 344);
            chartStockMaterial.TabIndex = 10;
            chartStockMaterial.Text = "ChartRoundbar";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(26, 26, 26);
            ClientSize = new Size(1176, 1100);
            Controls.Add(tableLayoutPanel7);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Dashboard";
            Text = "Dashboard";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartRoundbar).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartLowestStock).EndInit();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRepaired).EndInit();
            tableLayoutPanel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReceived).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartUssageMaterial).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartStockMaterial).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label22;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label18;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel5;
        private Panel panel5;
        private Label label15;
        private Label label14;
        private Label label16;
        private Panel panel4;
        private Label label12;
        private Label label11;
        private Label label13;
        private Panel panel3;
        private Label label9;
        private Label label8;
        private Label label10;
        private Panel panel2;
        private Label label6;
        private Label label5;
        private Label label7;
        private Panel panel1;
        private Label label3;
        private Label label4;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label17;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLowestStock;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label19;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel9;
        private Label label21;
        private Label label20;
        private Guna.UI2.WinForms.Guna2DataGridView dgvReceived;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRepaired;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRoundbar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStockMaterial;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUssageMaterial;
    }
}