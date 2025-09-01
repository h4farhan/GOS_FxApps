using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;
using DrawingPoint = System.Drawing.Point;

namespace GOS_FxApps
{
    public partial class Dashboard : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public static Dashboard Instance;
             
        public class RoundedPanel : Panel
            {
                public int BorderRadius { get; set; } = 15;

                protected override void OnPaint(PaintEventArgs e)
                {
                    base.OnPaint(e);

                    using (GraphicsPath path = new GraphicsPath())
                    {
                        Rectangle rect = this.ClientRectangle;
                        int radius = BorderRadius;

                        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                        path.CloseAllFigures();

                        this.Region = new Region(path);
                    }
                }
        }

        public Dashboard()
        {
            InitializeComponent();
        }

        private void LoadNotifikasi()
        {
            int scrollPosition = panelNotif.VerticalScroll.Value;

            panelNotif.SuspendLayout();
            panelNotif.Controls.Clear();

            List<(string Text, DateTime Waktu, Color WarnaText, Color WarnaWaktu)> notifList =
                new List<(string, DateTime, Color, Color)>();

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                conn.Open();

                string query2 = @"SELECT TOP 1 bstok, bpe1, bpe2, bbe1, bbe2, wpe1, wpe2, wbe1, wbe2, updated_at 
                          FROM Rb_Stok ORDER BY id_stok DESC";

                using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                {
                    Dictionary<string, int> stokData = new Dictionary<string, int>();
                    DateTime waktuRb = DateTime.Now;

                    if (reader2.Read())
                    {
                        string[] kolomList = { "bstok", "bpe1", "bpe2", "bbe1", "bbe2", "wpe1", "wpe2", "wbe1", "wbe2" };

                        foreach (string kolom in kolomList)
                        {
                            stokData[kolom] = Convert.ToInt32(reader2[kolom]);
                        }

                        waktuRb = Convert.ToDateTime(reader2["updated_at"]);
                    }
                    reader2.Close();

                    foreach (var item in stokData)
                    {
                        using (SqlCommand cmdMin = new SqlCommand(
                            "SELECT namaTampilan, min_stok FROM setmin_Rb WHERE kode = @kode", conn))
                        {
                            cmdMin.Parameters.AddWithValue("@kode", item.Key);
                            using (SqlDataReader rdrMin = cmdMin.ExecuteReader())
                            {
                                if (rdrMin.Read())
                                {
                                    string nama = rdrMin["namaTampilan"].ToString();
                                    int minStok = Convert.ToInt32(rdrMin["min_stok"]);

                                    if (item.Value < minStok)
                                    {
                                        notifList.Add((
                                            $"{nama} Stok Rendah ({item.Value}/{minStok})",
                                            waktuRb, 
                                            Color.FromArgb(255, 0, 0),
                                            Color.Gray
                                        ));
                                    }
                                }
                            }
                        }
                    }
                }

                string query1 = @"SELECT namaBarang, jumlahStok, min_stok, updated_at 
                          FROM stok_material WHERE jumlahStok < min_stok";

                using (SqlCommand cmd1 = new SqlCommand(query1, conn))
                using (SqlDataReader reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        string nama = reader1["namaBarang"].ToString();
                        int stok = Convert.ToInt32(reader1["jumlahStok"]);
                        int minStok = Convert.ToInt32(reader1["min_stok"]);
                        DateTime waktu = Convert.ToDateTime(reader1["updated_at"]);

                        notifList.Add((
                            $"Material {nama} Stok Rendah ({stok}/{minStok})",
                            waktu,
                            Color.FromArgb(255, 0, 0),
                            Color.Gray
                        ));
                    }
                }
            }
            var sortedNotif = notifList
            .OrderByDescending(n => n.Waktu)
            .ThenByDescending(n => n.Text) 
            .ToList();

            foreach (var notif in sortedNotif)
            {
                AddNotifPanel(
                    notif.Text,
                    notif.Waktu.ToString("dd MMM yyyy HH:mm"),
                    notif.WarnaText,
                    notif.WarnaWaktu
                );
            }

            panelNotif.AutoScroll = true;
            panelNotif.ResumeLayout();
            panelNotif.VerticalScroll.Value = Math.Min(scrollPosition, panelNotif.VerticalScroll.Maximum);
            panelNotif.PerformLayout();
        }

        private void AddNotifPanel(string text, string waktu, Color warnaText, Color warnaWaktu)
        {
            RoundedPanel itemPanel = new RoundedPanel();
            itemPanel.Width = panelNotif.Width - 25;
            itemPanel.Height = text.Contains("Material") ? 130 : 110;
            itemPanel.BackColor = Color.WhiteSmoke;
            itemPanel.Margin = new Padding(3);
            itemPanel.Padding = new Padding(10);
            itemPanel.BorderRadius = 10;
            itemPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; 

            Panel teksPanel = new Panel();
            teksPanel.Dock = DockStyle.Fill;
            teksPanel.BackColor = Color.WhiteSmoke;
            teksPanel.Padding = new Padding(5);

            Label icon = new Label();
            icon.Text = "🔔";
            icon.Font = new Font("Segoe UI Emoji", 16);
            icon.Dock = DockStyle.Left;
            icon.Width = 40;
            icon.TextAlign = ContentAlignment.MiddleCenter;

            Label lblText = new Label();
            lblText.Text = text;
            lblText.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            lblText.ForeColor = warnaText;
            lblText.AutoSize = true;
            lblText.MaximumSize = new Size(teksPanel.Width - 10, 0);
            lblText.Dock = DockStyle.Top;

            Label lblWaktu = new Label();
            lblWaktu.Text = waktu;
            lblWaktu.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblWaktu.ForeColor = warnaWaktu;
            lblWaktu.AutoSize = true;
            lblWaktu.Dock = DockStyle.Bottom;

            teksPanel.Controls.Add(lblWaktu); 
            teksPanel.Controls.Add(lblText); 
            teksPanel.Controls.Add(icon);     

            itemPanel.Controls.Add(teksPanel);

            panelNotif.Controls.Add(itemPanel);
        }
        private void LoadPanel()
        {
            try
            {
                conn.Open();
                int jumlah1 = 0;
                int jumlah2 = 0;
                int jumlah3 = 0;

                string query1 = "SELECT COUNT(*) FROM penerimaan_s";
                using (SqlCommand cmd1 = new SqlCommand(query1, conn))
                {
                    jumlah1 = Convert.ToInt32(cmd1.ExecuteScalar());
                    lblubrepaired.Text = jumlah1.ToString();
                }

                string query2 = "SELECT COUNT(*) FROM perbaikan_s";
                using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                {
                    jumlah2 = Convert.ToInt32(cmd2.ExecuteScalar());
                    label15.Text = jumlah2.ToString();
                }

                jumlah3 = jumlah1 + jumlah2;
                label3.Text = jumlah3.ToString();
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            { 
                conn.Close();
            }
        }

        private void LoadchartRB()
        {
            double rbStock = 0;
            double rbSawinge1 = 0;
            double rbSawinge2 = 0;
            double rblathee1 = 0;
            double rblathee2 = 0;
            double wpsawinge1 = 0;
            double wpsawinge2 = 0;
            double wplathee1 = 0;
            double wplathee2 = 0;

            try
            {
                conn.Open();
                string query = @"
            SELECT TOP 1 bstok, bpe1, bpe2, bbe1, bbe2, wpe1, wpe2, wbe1, wbe2, tanggal, id_stok
            FROM Rb_Stok
            WHERE tanggal >= @tanggal AND tanggal < DATEADD(DAY, 1, @tanggal)
            ORDER BY id_stok DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tanggal", tanggal.Value.Date);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rbStock = dr["bstok"] != DBNull.Value ? Convert.ToDouble(dr["bstok"]) : 0;
                            rbSawinge1 = dr["bpe1"] != DBNull.Value ? Convert.ToDouble(dr["bpe1"]) : 0;
                            rbSawinge2 = dr["bpe2"] != DBNull.Value ? Convert.ToDouble(dr["bpe2"]) : 0;
                            rblathee1 = dr["bbe1"] != DBNull.Value ? Convert.ToDouble(dr["bbe1"]) : 0;
                            rblathee2 = dr["bbe2"] != DBNull.Value ? Convert.ToDouble(dr["bbe2"]) : 0;
                            wpsawinge1 = dr["wpe1"] != DBNull.Value ? Convert.ToDouble(dr["wpe1"]) : 0;
                            wpsawinge2 = dr["wpe2"] != DBNull.Value ? Convert.ToDouble(dr["wpe2"]) : 0;
                            wplathee1 = dr["wbe1"] != DBNull.Value ? Convert.ToDouble(dr["wbe1"]) : 0;
                            wplathee2 = dr["wbe2"] != DBNull.Value ? Convert.ToDouble(dr["wbe2"]) : 0;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            // ===== Grafik =====
            chartUssageMaterial.Series.Clear();
            chartUssageMaterial.ChartAreas.Clear();

            ChartArea area = new ChartArea("MainArea");
            area.AxisX.Interval = 1;
            area.AxisY.Minimum = 0;

            area.AxisX.MajorGrid.LineWidth = 0;
            area.AxisY.MajorGrid.LineWidth = 0;

            area.AxisX.MinorGrid.LineWidth = 0;
            area.AxisY.MinorGrid.LineWidth = 0;
            chartUssageMaterial.ChartAreas.Add(area);
            Series series = new Series
            {
                Name = "DataSeries",
                ChartType = SeriesChartType.Column,
                IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            series.Points.AddXY("Roundbar Stock", rbStock);
            series.Points.AddXY("Roundbar Sawing E1", rbSawinge1);
            series.Points.AddXY("Roundbar Sawing E2", rbSawinge2);
            series.Points.AddXY("Roundbar Lathe E1", rblathee1);
            series.Points.AddXY("Roundbar Lathe E2", rblathee2);
            series.Points.AddXY("Welding Pieces Sawing E1", wpsawinge1);
            series.Points.AddXY("Welding Pieces Sawing E2", wpsawinge2);
            series.Points.AddXY("Welding Pieces Lathe E1", wplathee1);
            series.Points.AddXY("Welding Pieces Lathe E2", wplathee2);

            chartUssageMaterial.Series.Add(series);
        }
        private void LoadchartRBByMonth()
        {
            double rbStock = 0, rbSawinge1 = 0, rbSawinge2 = 0,
                   rblathee1 = 0, rblathee2 = 0,
                   wpsawinge1 = 0, wpsawinge2 = 0,
                   wplathee1 = 0, wplathee2 = 0;

            try
            {
                conn.Open();

                // Ambil tanggal awal bulan dari DateTimePicker "bulan"
                DateTime awalBulan = new DateTime(datebulan.Value.Year, datebulan.Value.Month, 1);

                string query = @"
        SELECT 
            SUM(bstok) AS bstok,
            SUM(bpe1) AS bpe1,
            SUM(bpe2) AS bpe2,
            SUM(bbe1) AS bbe1,
            SUM(bbe2) AS bbe2,
            SUM(wpe1) AS wpe1,
            SUM(wpe2) AS wpe2,
            SUM(wbe1) AS wbe1,
            SUM(wbe2) AS wbe2
        FROM Rb_Stok
        WHERE tanggal >= @awalBulan 
          AND tanggal < DATEADD(MONTH, 1, @awalBulan)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@awalBulan", awalBulan);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rbStock = dr["bstok"] != DBNull.Value ? Convert.ToDouble(dr["bstok"]) : 0;
                            rbSawinge1 = dr["bpe1"] != DBNull.Value ? Convert.ToDouble(dr["bpe1"]) : 0;
                            rbSawinge2 = dr["bpe2"] != DBNull.Value ? Convert.ToDouble(dr["bpe2"]) : 0;
                            rblathee1 = dr["bbe1"] != DBNull.Value ? Convert.ToDouble(dr["bbe1"]) : 0;
                            rblathee2 = dr["bbe2"] != DBNull.Value ? Convert.ToDouble(dr["bbe2"]) : 0;
                            wpsawinge1 = dr["wpe1"] != DBNull.Value ? Convert.ToDouble(dr["wpe1"]) : 0;
                            wpsawinge2 = dr["wpe2"] != DBNull.Value ? Convert.ToDouble(dr["wpe2"]) : 0;
                            wplathee1 = dr["wbe1"] != DBNull.Value ? Convert.ToDouble(dr["wbe1"]) : 0;
                            wplathee2 = dr["wbe2"] != DBNull.Value ? Convert.ToDouble(dr["wbe2"]) : 0;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            // ===== Grafik =====
            chartUssageMaterial.Series.Clear();
            chartUssageMaterial.ChartAreas.Clear();

            ChartArea area = new ChartArea("MainArea");
            area.AxisX.Interval = 1;
            area.AxisY.Minimum = 0;
            area.AxisX.MajorGrid.LineWidth = 0;
            area.AxisY.MajorGrid.LineWidth = 0;
            chartUssageMaterial.ChartAreas.Add(area);

            Series series = new Series
            {
                Name = "DataSeries",
                ChartType = SeriesChartType.Column,
                IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            series.Points.AddXY("Roundbar Stock", rbStock);
            series.Points.AddXY("Roundbar Sawing E1", rbSawinge1);
            series.Points.AddXY("Roundbar Sawing E2", rbSawinge2);
            series.Points.AddXY("Roundbar Lathe E1", rblathee1);
            series.Points.AddXY("Roundbar Lathe E2", rblathee2);
            series.Points.AddXY("Welding Pieces Sawing E1", wpsawinge1);
            series.Points.AddXY("Welding Pieces Sawing E2", wpsawinge2);
            series.Points.AddXY("Welding Pieces Lathe E1", wplathee1);
            series.Points.AddXY("Welding Pieces Lathe E2", wplathee2);

            chartUssageMaterial.Series.Add(series);
        }
        private void LoadchartRBByYear()
        {
            double rbStock = 0, rbSawinge1 = 0, rbSawinge2 = 0,
                   rblathee1 = 0, rblathee2 = 0,
                   wpsawinge1 = 0, wpsawinge2 = 0,
                   wplathee1 = 0, wplathee2 = 0;

            try
            {
                conn.Open();

                string query = @"
                SELECT 
                    SUM(bstok) AS bstok,
                    SUM(bpe1) AS bpe1,
                    SUM(bpe2) AS bpe2,
                    SUM(bbe1) AS bbe1,
                    SUM(bbe2) AS bbe2,
                    SUM(wpe1) AS wpe1,
                    SUM(wpe2) AS wpe2,
                    SUM(wbe1) AS wbe1,
                    SUM(wbe2) AS wbe2
                FROM Rb_Stok
                WHERE YEAR(tanggal) = @tahun";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tahun", datebulan.Value.Year);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rbStock = dr["bstok"] != DBNull.Value ? Convert.ToDouble(dr["bstok"]) : 0;
                            rbSawinge1 = dr["bpe1"] != DBNull.Value ? Convert.ToDouble(dr["bpe1"]) : 0;
                            rbSawinge2 = dr["bpe2"] != DBNull.Value ? Convert.ToDouble(dr["bpe2"]) : 0;
                            rblathee1 = dr["bbe1"] != DBNull.Value ? Convert.ToDouble(dr["bbe1"]) : 0;
                            rblathee2 = dr["bbe2"] != DBNull.Value ? Convert.ToDouble(dr["bbe2"]) : 0;
                            wpsawinge1 = dr["wpe1"] != DBNull.Value ? Convert.ToDouble(dr["wpe1"]) : 0;
                            wpsawinge2 = dr["wpe2"] != DBNull.Value ? Convert.ToDouble(dr["wpe2"]) : 0;
                            wplathee1 = dr["wbe1"] != DBNull.Value ? Convert.ToDouble(dr["wbe1"]) : 0;
                            wplathee2 = dr["wbe2"] != DBNull.Value ? Convert.ToDouble(dr["wbe2"]) : 0;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            chartUssageMaterial.Series.Clear();
            chartUssageMaterial.ChartAreas.Clear();

            ChartArea area = new ChartArea("MainArea");
            area.AxisX.Interval = 1;
            area.AxisY.Minimum = 0;
            area.AxisX.MajorGrid.LineWidth = 0;
            area.AxisY.MajorGrid.LineWidth = 0;
            chartUssageMaterial.ChartAreas.Add(area);

            Series series = new Series
            {
                Name = "DataSeries",
                ChartType = SeriesChartType.Column,
                IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            series.Points.AddXY("Roundbar Stock", rbStock);
            series.Points.AddXY("Roundbar Sawing E1", rbSawinge1);
            series.Points.AddXY("Roundbar Sawing E2", rbSawinge2);
            series.Points.AddXY("Roundbar Lathe E1", rblathee1);
            series.Points.AddXY("Roundbar Lathe E2", rblathee2);
            series.Points.AddXY("Welding Pieces Sawing E1", wpsawinge1);
            series.Points.AddXY("Welding Pieces Sawing E2", wpsawinge2);
            series.Points.AddXY("Welding Pieces Lathe E1", wplathee1);
            series.Points.AddXY("Welding Pieces Lathe E2", wplathee2);

            chartUssageMaterial.Series.Add(series);
        }

        private void LoadChartPenerimaanHarian()
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                       totalS = 0, totalD = 0, totalB = 0,
                       totalBA = 0, totalCR = 0, totalM = 0,
                       totalR = 0, totalC = 0, totalRL = 0;

                try
                {
                    conn.Open();
                    string query = @"
                SELECT
                    SUM(e1) AS TotalE1,
                    SUM(e2) AS TotalE2,
                    SUM(e3) AS TotalE3,
                    SUM(s)  AS TotalS,
                    SUM(d)  AS TotalD,
                    SUM(b)  AS TotalB,
                    SUM(ba) AS TotalBA,
                    SUM(cr) AS TotalCR,
                    SUM(m)  AS TotalM,
                    SUM(r)  AS TotalR,
                    SUM(c)  AS TotalC,
                    SUM(rl) AS TotalRL
                FROM penerimaan_p
                WHERE CAST(tanggal_penerimaan AS DATE) = @tanggal";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tanggal", tanggal.Value.Date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                totalE1 = dr["TotalE1"] != DBNull.Value ? Convert.ToDouble(dr["TotalE1"]) : 0;
                                totalE2 = dr["TotalE2"] != DBNull.Value ? Convert.ToDouble(dr["TotalE2"]) : 0;
                                totalE3 = dr["TotalE3"] != DBNull.Value ? Convert.ToDouble(dr["TotalE3"]) : 0;
                                totalS = dr["TotalS"] != DBNull.Value ? Convert.ToDouble(dr["TotalS"]) : 0;
                                totalD = dr["TotalD"] != DBNull.Value ? Convert.ToDouble(dr["TotalD"]) : 0;
                                totalB = dr["TotalB"] != DBNull.Value ? Convert.ToDouble(dr["TotalB"]) : 0;
                                totalBA = dr["TotalBA"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA"]) : 0;
                                totalCR = dr["TotalCR"] != DBNull.Value ? Convert.ToDouble(dr["TotalCR"]) : 0;
                                totalM = dr["TotalM"] != DBNull.Value ? Convert.ToDouble(dr["TotalM"]) : 0;
                                totalR = dr["TotalR"] != DBNull.Value ? Convert.ToDouble(dr["TotalR"]) : 0;
                                totalC = dr["TotalC"] != DBNull.Value ? Convert.ToDouble(dr["TotalC"]) : 0;
                                totalRL = dr["TotalRL"] != DBNull.Value ? Convert.ToDouble(dr["TotalRL"]) : 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                // ===== Refresh Chart =====
                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.ChartAreas.Clear();

                ChartArea area = new ChartArea("MainArea");
                area.AxisX.Interval = 1;
                area.AxisY.Minimum = 0;
                area.AxisX.MajorGrid.LineWidth = 0;
                area.AxisY.MajorGrid.LineWidth = 0;
                chartUssageMaterial.ChartAreas.Add(area);

                Series series = new Series("Jumlah Harian")
                {
                    ChartType = SeriesChartType.Column,
                    IsXValueIndexed = true,
                    IsValueShownAsLabel = true,
                    LabelForeColor = Color.Black
                };

                series.Points.AddXY("E1", totalE1);
                series.Points.AddXY("E2", totalE2);
                series.Points.AddXY("E3", totalE3);
                series.Points.AddXY("S", totalS);
                series.Points.AddXY("D", totalD);
                series.Points.AddXY("B", totalB);
                series.Points.AddXY("BA", totalBA);
                series.Points.AddXY("CR", totalCR);
                series.Points.AddXY("M", totalM);
                series.Points.AddXY("R", totalR);
                series.Points.AddXY("C", totalC);
                series.Points.AddXY("RL", totalRL);

                chartUssageMaterial.Series.Add(series);
            }
        }
        private void LoadChartPenerimaanBulanan()
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                       totalS = 0, totalD = 0, totalB = 0,
                       totalBA = 0, totalCR = 0, totalM = 0,
                       totalR = 0, totalC = 0, totalRL = 0;

                try
                {
                    conn.Open();
                    string query = @"
                SELECT
                    SUM(e1) AS TotalE1,
                    SUM(e2) AS TotalE2,
                    SUM(e3) AS TotalE3,
                    SUM(s)  AS TotalS,
                    SUM(d)  AS TotalD,
                    SUM(b)  AS TotalB,
                    SUM(ba) AS TotalBA,
                    SUM(cr) AS TotalCR,
                    SUM(m)  AS TotalM,
                    SUM(r)  AS TotalR,
                    SUM(c)  AS TotalC,
                    SUM(rl) AS TotalRL
                FROM penerimaan_p
                WHERE MONTH(tanggal_penerimaan) = @bulan
                  AND YEAR(tanggal_penerimaan) = @tahun";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@tahun", datebulan.Value.Year);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                totalE1 = dr["TotalE1"] != DBNull.Value ? Convert.ToDouble(dr["TotalE1"]) : 0;
                                totalE2 = dr["TotalE2"] != DBNull.Value ? Convert.ToDouble(dr["TotalE2"]) : 0;
                                totalE3 = dr["TotalE3"] != DBNull.Value ? Convert.ToDouble(dr["TotalE3"]) : 0;
                                totalS = dr["TotalS"] != DBNull.Value ? Convert.ToDouble(dr["TotalS"]) : 0;
                                totalD = dr["TotalD"] != DBNull.Value ? Convert.ToDouble(dr["TotalD"]) : 0;
                                totalB = dr["TotalB"] != DBNull.Value ? Convert.ToDouble(dr["TotalB"]) : 0;
                                totalBA = dr["TotalBA"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA"]) : 0;
                                totalCR = dr["TotalCR"] != DBNull.Value ? Convert.ToDouble(dr["TotalCR"]) : 0;
                                totalM = dr["TotalM"] != DBNull.Value ? Convert.ToDouble(dr["TotalM"]) : 0;
                                totalR = dr["TotalR"] != DBNull.Value ? Convert.ToDouble(dr["TotalR"]) : 0;
                                totalC = dr["TotalC"] != DBNull.Value ? Convert.ToDouble(dr["TotalC"]) : 0;
                                totalRL = dr["TotalRL"] != DBNull.Value ? Convert.ToDouble(dr["TotalRL"]) : 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                // ===== Refresh Chart =====
                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.ChartAreas.Clear();

                ChartArea area = new ChartArea("MainArea");
                area.AxisX.Interval = 1;
                area.AxisY.Minimum = 0;
                area.AxisX.MajorGrid.LineWidth = 0;
                area.AxisY.MajorGrid.LineWidth = 0;
                chartUssageMaterial.ChartAreas.Add(area);

                Series series = new Series("Jumlah Bulanan")
                {
                    ChartType = SeriesChartType.Column,
                    IsXValueIndexed = true,
                    IsValueShownAsLabel = true,
                    LabelForeColor = Color.Black
                };

                series.Points.AddXY("E1", totalE1);
                series.Points.AddXY("E2", totalE2);
                series.Points.AddXY("E3", totalE3);
                series.Points.AddXY("S", totalS);
                series.Points.AddXY("D", totalD);
                series.Points.AddXY("B", totalB);
                series.Points.AddXY("BA", totalBA);
                series.Points.AddXY("CR", totalCR);
                series.Points.AddXY("M", totalM);
                series.Points.AddXY("R", totalR);
                series.Points.AddXY("C", totalC);
                series.Points.AddXY("RL", totalRL);

                chartUssageMaterial.Series.Add(series);
            }
        }
        private void LoadChartPenerimaanTahunan()
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                       totalS = 0, totalD = 0, totalB = 0,
                       totalBA = 0, totalCR = 0, totalM = 0,
                       totalR = 0, totalC = 0, totalRL = 0;

                try
                {
                    conn.Open();
                    string query = @"
                SELECT
                    SUM(e1) AS TotalE1,
                    SUM(e2) AS TotalE2,
                    SUM(e3) AS TotalE3,
                    SUM(s)  AS TotalS,
                    SUM(d)  AS TotalD,
                    SUM(b)  AS TotalB,
                    SUM(ba) AS TotalBA,
                    SUM(cr) AS TotalCR,
                    SUM(m)  AS TotalM,
                    SUM(r)  AS TotalR,
                    SUM(c)  AS TotalC,
                    SUM(rl) AS TotalRL
                FROM penerimaan_p
                WHERE YEAR(tanggal_penerimaan) = @tahun";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tahun", datebulan.Value.Year);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                totalE1 = dr["TotalE1"] != DBNull.Value ? Convert.ToDouble(dr["TotalE1"]) : 0;
                                totalE2 = dr["TotalE2"] != DBNull.Value ? Convert.ToDouble(dr["TotalE2"]) : 0;
                                totalE3 = dr["TotalE3"] != DBNull.Value ? Convert.ToDouble(dr["TotalE3"]) : 0;
                                totalS = dr["TotalS"] != DBNull.Value ? Convert.ToDouble(dr["TotalS"]) : 0;
                                totalD = dr["TotalD"] != DBNull.Value ? Convert.ToDouble(dr["TotalD"]) : 0;
                                totalB = dr["TotalB"] != DBNull.Value ? Convert.ToDouble(dr["TotalB"]) : 0;
                                totalBA = dr["TotalBA"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA"]) : 0;
                                totalCR = dr["TotalCR"] != DBNull.Value ? Convert.ToDouble(dr["TotalCR"]) : 0;
                                totalM = dr["TotalM"] != DBNull.Value ? Convert.ToDouble(dr["TotalM"]) : 0;
                                totalR = dr["TotalR"] != DBNull.Value ? Convert.ToDouble(dr["TotalR"]) : 0;
                                totalC = dr["TotalC"] != DBNull.Value ? Convert.ToDouble(dr["TotalC"]) : 0;
                                totalRL = dr["TotalRL"] != DBNull.Value ? Convert.ToDouble(dr["TotalRL"]) : 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                // ===== Refresh Chart =====
                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.ChartAreas.Clear();

                ChartArea area = new ChartArea("MainArea");
                area.AxisX.Interval = 1;
                area.AxisY.Minimum = 0;
                area.AxisX.MajorGrid.LineWidth = 0;
                area.AxisY.MajorGrid.LineWidth = 0;
                chartUssageMaterial.ChartAreas.Add(area);

                Series series = new Series("Jumlah Tahunan")
                {
                    ChartType = SeriesChartType.Column,
                    IsXValueIndexed = true,
                    IsValueShownAsLabel = true,
                    LabelForeColor = Color.Black
                };

                series.Points.AddXY("E1", totalE1);
                series.Points.AddXY("E2", totalE2);
                series.Points.AddXY("E3", totalE3);
                series.Points.AddXY("S", totalS);
                series.Points.AddXY("D", totalD);
                series.Points.AddXY("B", totalB);
                series.Points.AddXY("BA", totalBA);
                series.Points.AddXY("CR", totalCR);
                series.Points.AddXY("M", totalM);
                series.Points.AddXY("R", totalR);
                series.Points.AddXY("C", totalC);
                series.Points.AddXY("RL", totalRL);

                chartUssageMaterial.Series.Add(series);
            }
        }

        private void loadChartPerbaikanHarian()
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                       totalS = 0, totalD = 0, totalB = 0,
                       totalBA = 0, totalCR = 0, totalM = 0,
                       totalR = 0, totalC = 0, totalRL = 0,
                       totalBA1 = 0, totalE4 = 0;

                try
                {
                    conn.Open();
                    string query = @"
                SELECT
                    SUM(e1_jumlah) AS TotalE1,
                    SUM(e2_jumlah) AS TotalE2,
                    SUM(e3) AS TotalE3,
                    SUM(e4) AS TotalE4,
                    SUM(s)  AS TotalS,
                    SUM(d)  AS TotalD,
                    SUM(b)  AS TotalB,
                    SUM(ba) AS TotalBA,
                    SUM(ba1) AS TotalBA1,
                    SUM(cr) AS TotalCR,
                    SUM(m)  AS TotalM,
                    SUM(r)  AS TotalR,
                    SUM(c)  AS TotalC,
                    SUM(rl) AS TotalRL
                FROM perbaikan_p
                WHERE CAST(tanggal_perbaikan AS DATE) = @tanggal";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tanggal", tanggal.Value.Date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                totalE1 = dr["TotalE1"] != DBNull.Value ? Convert.ToDouble(dr["TotalE1"]) : 0;
                                totalE2 = dr["TotalE2"] != DBNull.Value ? Convert.ToDouble(dr["TotalE2"]) : 0;
                                totalE3 = dr["TotalE3"] != DBNull.Value ? Convert.ToDouble(dr["TotalE3"]) : 0;
                                totalE4 = dr["TotalE4"] != DBNull.Value ? Convert.ToDouble(dr["TotalE4"]) : 0;
                                totalS = dr["TotalS"] != DBNull.Value ? Convert.ToDouble(dr["TotalS"]) : 0;
                                totalD = dr["TotalD"] != DBNull.Value ? Convert.ToDouble(dr["TotalD"]) : 0;
                                totalB = dr["TotalB"] != DBNull.Value ? Convert.ToDouble(dr["TotalB"]) : 0;
                                totalBA = dr["TotalBA"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA"]) : 0;
                                totalBA1 = dr["TotalBA1"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA1"]) : 0;
                                totalCR = dr["TotalCR"] != DBNull.Value ? Convert.ToDouble(dr["TotalCR"]) : 0;
                                totalM = dr["TotalM"] != DBNull.Value ? Convert.ToDouble(dr["TotalM"]) : 0;
                                totalR = dr["TotalR"] != DBNull.Value ? Convert.ToDouble(dr["TotalR"]) : 0;
                                totalC = dr["TotalC"] != DBNull.Value ? Convert.ToDouble(dr["TotalC"]) : 0;
                                totalRL = dr["TotalRL"] != DBNull.Value ? Convert.ToDouble(dr["TotalRL"]) : 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                // ===== Refresh Chart =====
                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.ChartAreas.Clear();

                ChartArea area = new ChartArea("MainArea");
                area.AxisX.Interval = 1;
                area.AxisY.Minimum = 0;
                area.AxisX.MajorGrid.LineWidth = 0;
                area.AxisY.MajorGrid.LineWidth = 0;
                chartUssageMaterial.ChartAreas.Add(area);

                Series series = new Series("Jumlah Perbaikan Harian")
                {
                    ChartType = SeriesChartType.Column,
                    IsXValueIndexed = true,
                    IsValueShownAsLabel = true,
                    LabelForeColor = Color.Black
                };

                series.Points.AddXY("E1", totalE1);
                series.Points.AddXY("E2", totalE2);
                series.Points.AddXY("E3", totalE3);
                series.Points.AddXY("E4", totalE4);
                series.Points.AddXY("S", totalS);
                series.Points.AddXY("D", totalD);
                series.Points.AddXY("B", totalB);
                series.Points.AddXY("BA", totalBA);
                series.Points.AddXY("BA1", totalBA1);
                series.Points.AddXY("CR", totalCR);
                series.Points.AddXY("M", totalM);
                series.Points.AddXY("R", totalR);
                series.Points.AddXY("C", totalC);
                series.Points.AddXY("RL", totalRL);

                chartUssageMaterial.Series.Add(series);
            }
        }
        private void LoadChartPerbaikanBulanan()
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                       totalS = 0, totalD = 0, totalB = 0,
                       totalBA = 0, totalCR = 0, totalM = 0,
                       totalR = 0, totalC = 0, totalRL = 0,
                       totalBA1 = 0, totalE4 = 0;

                try
                {
                    conn.Open();
                    string query = @"
                SELECT
                    SUM(e1_jumlah) AS TotalE1,
                    SUM(e2_jumlah) AS TotalE2,
                    SUM(e3) AS TotalE3,
                    SUM(e4) AS TotalE4,
                    SUM(s)  AS TotalS,
                    SUM(d)  AS TotalD,
                    SUM(b)  AS TotalB,
                    SUM(ba) AS TotalBA,
                    SUM(ba1) AS TotalBA1,
                    SUM(cr) AS TotalCR,
                    SUM(m)  AS TotalM,
                    SUM(r)  AS TotalR,
                    SUM(c)  AS TotalC,
                    SUM(rl) AS TotalRL
                FROM perbaikan_p
                WHERE MONTH(tanggal_perbaikan) = @bulan AND YEAR(tanggal_perbaikan) = @tahun";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@tahun", datebulan.Value.Year);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                totalE1 = dr["TotalE1"] != DBNull.Value ? Convert.ToDouble(dr["TotalE1"]) : 0;
                                totalE2 = dr["TotalE2"] != DBNull.Value ? Convert.ToDouble(dr["TotalE2"]) : 0;
                                totalE3 = dr["TotalE3"] != DBNull.Value ? Convert.ToDouble(dr["TotalE3"]) : 0;
                                totalE4 = dr["TotalE4"] != DBNull.Value ? Convert.ToDouble(dr["TotalE4"]) : 0;
                                totalS = dr["TotalS"] != DBNull.Value ? Convert.ToDouble(dr["TotalS"]) : 0;
                                totalD = dr["TotalD"] != DBNull.Value ? Convert.ToDouble(dr["TotalD"]) : 0;
                                totalB = dr["TotalB"] != DBNull.Value ? Convert.ToDouble(dr["TotalB"]) : 0;
                                totalBA = dr["TotalBA"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA"]) : 0;
                                totalBA1 = dr["TotalBA1"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA1"]) : 0;
                                totalCR = dr["TotalCR"] != DBNull.Value ? Convert.ToDouble(dr["TotalCR"]) : 0;
                                totalM = dr["TotalM"] != DBNull.Value ? Convert.ToDouble(dr["TotalM"]) : 0;
                                totalR = dr["TotalR"] != DBNull.Value ? Convert.ToDouble(dr["TotalR"]) : 0;
                                totalC = dr["TotalC"] != DBNull.Value ? Convert.ToDouble(dr["TotalC"]) : 0;
                                totalRL = dr["TotalRL"] != DBNull.Value ? Convert.ToDouble(dr["TotalRL"]) : 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                // ===== Refresh Chart =====
                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.ChartAreas.Clear();

                ChartArea area = new ChartArea("MainArea");
                area.AxisX.Interval = 1;
                area.AxisY.Minimum = 0;
                area.AxisX.MajorGrid.LineWidth = 0;
                area.AxisY.MajorGrid.LineWidth = 0;

                area.AxisX.MinorGrid.LineWidth = 0;
                area.AxisY.MinorGrid.LineWidth = 0;
                chartUssageMaterial.ChartAreas.Add(area);

                Series series = new Series("Jumlah Perbaikan Bulanan")
                {
                    ChartType = SeriesChartType.Column,
                    IsXValueIndexed = true,
                    IsValueShownAsLabel = true
                };

                series.Points.AddXY("E1", totalE1);
                series.Points.AddXY("E2", totalE2);
                series.Points.AddXY("E3", totalE3);
                series.Points.AddXY("E4", totalE4);
                series.Points.AddXY("S", totalS);
                series.Points.AddXY("D", totalD);
                series.Points.AddXY("B", totalB);
                series.Points.AddXY("BA", totalBA);
                series.Points.AddXY("BA1", totalBA1);
                series.Points.AddXY("CR", totalCR);
                series.Points.AddXY("M", totalM);
                series.Points.AddXY("R", totalR);
                series.Points.AddXY("C", totalC);
                series.Points.AddXY("RL", totalRL);

                chartUssageMaterial.Series.Add(series);
            }
        }
        private void LoadChartPerbaikanTahunan()
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                       totalS = 0, totalD = 0, totalB = 0,
                       totalBA = 0, totalCR = 0, totalM = 0,
                       totalR = 0, totalC = 0, totalRL = 0,
                       totalBA1 = 0, totalE4 = 0;

                try
                {
                    conn.Open();
                    string query = @"
                SELECT
                    SUM(e1_jumlah) AS TotalE1,
                    SUM(e2_jumlah) AS TotalE2,
                    SUM(e3) AS TotalE3,
                    SUM(e4) AS TotalE4,
                    SUM(s)  AS TotalS,
                    SUM(d)  AS TotalD,
                    SUM(b)  AS TotalB,
                    SUM(ba) AS TotalBA,
                    SUM(ba1) AS TotalBA1,
                    SUM(cr) AS TotalCR,
                    SUM(m)  AS TotalM,
                    SUM(r)  AS TotalR,
                    SUM(c)  AS TotalC,
                    SUM(rl) AS TotalRL
                FROM perbaikan_p
                WHERE YEAR(tanggal_perbaikan) = @tahun";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tahun", datebulan.Value.Year);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                totalE1 = dr["TotalE1"] != DBNull.Value ? Convert.ToDouble(dr["TotalE1"]) : 0;
                                totalE2 = dr["TotalE2"] != DBNull.Value ? Convert.ToDouble(dr["TotalE2"]) : 0;
                                totalE3 = dr["TotalE3"] != DBNull.Value ? Convert.ToDouble(dr["TotalE3"]) : 0;
                                totalE4 = dr["TotalE4"] != DBNull.Value ? Convert.ToDouble(dr["TotalE4"]) : 0;
                                totalS = dr["TotalS"] != DBNull.Value ? Convert.ToDouble(dr["TotalS"]) : 0;
                                totalD = dr["TotalD"] != DBNull.Value ? Convert.ToDouble(dr["TotalD"]) : 0;
                                totalB = dr["TotalB"] != DBNull.Value ? Convert.ToDouble(dr["TotalB"]) : 0;
                                totalBA = dr["TotalBA"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA"]) : 0;
                                totalBA1 = dr["TotalBA1"] != DBNull.Value ? Convert.ToDouble(dr["TotalBA1"]) : 0;
                                totalCR = dr["TotalCR"] != DBNull.Value ? Convert.ToDouble(dr["TotalCR"]) : 0;
                                totalM = dr["TotalM"] != DBNull.Value ? Convert.ToDouble(dr["TotalM"]) : 0;
                                totalR = dr["TotalR"] != DBNull.Value ? Convert.ToDouble(dr["TotalR"]) : 0;
                                totalC = dr["TotalC"] != DBNull.Value ? Convert.ToDouble(dr["TotalC"]) : 0;
                                totalRL = dr["TotalRL"] != DBNull.Value ? Convert.ToDouble(dr["TotalRL"]) : 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                // ===== Refresh Chart =====
                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.ChartAreas.Clear();

                ChartArea area = new ChartArea("MainArea");
                area.AxisX.Interval = 1;
                area.AxisY.Minimum = 0;
                area.AxisX.MajorGrid.LineWidth = 0;
                area.AxisY.MajorGrid.LineWidth = 0;

                area.AxisX.MinorGrid.LineWidth = 0;
                area.AxisY.MinorGrid.LineWidth = 0;
                chartUssageMaterial.ChartAreas.Add(area);

                Series series = new Series("Jumlah Perbaikan Tahunan")
                {
                    ChartType = SeriesChartType.Column,
                    IsXValueIndexed = true,
                    IsValueShownAsLabel = true
                };

                series.Points.AddXY("E1", totalE1);
                series.Points.AddXY("E2", totalE2);
                series.Points.AddXY("E3", totalE3);
                series.Points.AddXY("E4", totalE4);
                series.Points.AddXY("S", totalS);
                series.Points.AddXY("D", totalD);
                series.Points.AddXY("B", totalB);
                series.Points.AddXY("BA", totalBA);
                series.Points.AddXY("BA1", totalBA1);
                series.Points.AddXY("CR", totalCR);
                series.Points.AddXY("M", totalM);
                series.Points.AddXY("R", totalR);
                series.Points.AddXY("C", totalC);
                series.Points.AddXY("RL", totalRL);

                chartUssageMaterial.Series.Add(series);
            }
        }


        private void LoadChartMaterialCostHarian()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartmaterialcostharian", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Hari", tanggal.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggal.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggal.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart harian: " + ex.Message);
            }
        }
        private void LoadChartMaterialCostBulan()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartmaterialcostbulan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5; 
                axisX.ScrollBar.Enabled = true; 
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart: " + ex.Message);
            }
        }
        private void LoadChartMaterialCostTahun()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartmaterialcosttahun", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart tahunan: " + ex.Message);
            }
        }

        private void LoadChartConsumableCostHarian()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartconsumablecostharian", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Hari", tanggal.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggal.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggal.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart harian: " + ex.Message);
            }
        }
        private void LoadChartConsumableCostBulan()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartconsumablecostbulan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart: " + ex.Message);
            }
        }
        private void LoadChartConsumableCostTahun()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartconsumablecosttahun", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart tahunan: " + ex.Message);
            }
        }

        private void LoadChartSafetyCostHarian()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartsafetycostharian", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Hari", tanggal.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggal.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggal.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart harian: " + ex.Message);
            }
        }
        private void LoadChartSafetyCostBulan()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartsafetycostbulan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart: " + ex.Message);
            }
        }
        private void LoadChartSafetyCostTahun()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartsafetycosttahun", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();

                Series seriesBQ = new Series("TotalBQ");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("TotalPemakaian");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load chart tahunan: " + ex.Message);
            }
        }


        private void LoadChartStock()
        {
            chartUssageMaterial.Series.Clear();

            Series series = new Series("Stock Material");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Black;

            try
            {
                conn.Open();
                string query = "SELECT TOP 10 namaBarang, jumlahStok FROM stok_material ORDER BY jumlahStok ASC;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string namaBarang = dr["namaBarang"].ToString();
                        int jumlah = dr["jumlahStok"] != DBNull.Value ? Convert.ToInt32(dr["jumlahStok"]) : 0;

                        series.Points.AddXY(namaBarang, jumlah);
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            series.Color = Color.FromArgb(244, 67, 53);
            chartUssageMaterial.Series.Add(series);
            chartUssageMaterial.ChartAreas[0].AxisX.Interval = 1;
        }

        private void registerpenerimaans()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.penerimaan_s", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadPanel();
                            registerpenerimaans();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerperbaikans()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.perbaikan_s", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadPanel();
                            registerperbaikans();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerwelding()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.Rb_Stok", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadchartRB();
                            LoadNotifikasi();
                            registerwelding();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerstok()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.stok_material", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadChartStock();
                            LoadNotifikasi();
                            registerstok();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerSetminRb()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT kode, min_stok FROM dbo.setmin_Rb", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadNotifikasi();
                            registerSetminRb(); 
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());

            typeof(Panel).InvokeMember("DoubleBuffered",
               System.Reflection.BindingFlags.SetProperty |
               System.Reflection.BindingFlags.Instance |
               System.Reflection.BindingFlags.NonPublic,
               null, panelNotif, new object[] { true });
            Instance = this;
            if (MainForm.Instance != null && MainForm.Instance.role != null)
            {
                btntiga.Visible = (MainForm.Instance.role == "Operator Gudang");
                btntiga.Visible = (MainForm.Instance.role == "Manajer");
            }
            else
            {
                btntiga.Visible = false;
            }
            LoadNotifikasi();
            tanggal.Value = DateTime.Now;
            datebulan.Value = DateTime.Now;
            LoadPanel();
            containerrentang.Visible = true;
            containertanggal.Visible = true;
            //LoadChartStock();

            //registerpenerimaans();
            //registerperbaikans();
            //registerwelding();
            //registerstok();
            //registerSetminRb();
        }

        private void cmbpilihdata_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbpilihdata.SelectedIndex == 0) 
            //{
            //    registerstok();
            //    LoadChartStock();
            //    lbltitlechart.Text = "10 Material Dengan Stok Rendah";
            //}
            //else
            if (cmbpilihdata.SelectedIndex == 0)
            {   
                
                lbltitlechart.Text = "Stok Round Bar Dan Welding Piece";
                containerbulan.Visible = false;
                containertipe.Visible = false;
                containertipem.Visible = false;
                containertanggal.Visible = false;

                containerrentang.Visible = true;
                containertanggal.Visible = true;
                cmbrentang.SelectedIndex = 0;
                tanggal.Value = DateTime.Now;
            }
            else if (cmbpilihdata.SelectedIndex == 1)
            {
                lbltitlechart.Text = "Estimasi VS Actual";
                containerbulan.Visible = false;
                containertipe.Visible = false;
                containertanggal.Visible = false;

                containerrentang.Visible = true;
                containertipem.Visible = true;
                containertanggal.Visible = true;
                cmbrentang.SelectedIndex = 0;
                cmbtipem.SelectedIndex = 0;
                tanggal.Value = DateTime.Now;
            }
            else if (cmbpilihdata.SelectedIndex == 2)
            {
                lbltitlechart.Text = "Trend Jenis Kerusakan";
                containerbulan.Visible = false;
                containertipem.Visible = false;
                containertanggal.Visible = false;

                containerrentang.Visible = true;
                containertipe.Visible = true;
                containertanggal.Visible = true;
                cmbrentang.SelectedIndex = 0;
                cmbtipe.SelectedIndex = 0;
                tanggal.Value = DateTime.Now;
            }
        }

        private void btntiga_Click(object sender, EventArgs e)
        {
            Form setmin = new setmin_rb();
            setmin.Show();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void cmbrentang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbrentang.SelectedIndex == 0) 
            {
                containerbulan.Visible = false;
                containertanggal.Visible=true;
            }
            else if(cmbrentang.SelectedIndex == 1)
            {
                containertanggal.Visible = false;
                containerbulan.Visible=true;
            }
            else if(cmbrentang.SelectedIndex == 2)
            {
                containertanggal.Visible = false;
                containerbulan.Visible=true;
            }
        }

        private void btnsetfilter_Click(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedIndex == 0) 
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    LoadchartRB();
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    LoadchartRBByMonth();
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    LoadchartRBByYear();
                }
            }
            else if(cmbpilihdata.SelectedIndex == 1)
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    if(cmbtipem.SelectedIndex == 0)
                    {
                        //Material Cost hari
                        LoadChartMaterialCostHarian();

                    }
                    else if(cmbtipem.SelectedIndex == 1)
                    {
                        //Consumable Cost hari
                        LoadChartConsumableCostHarian();
                    }
                    else if(cmbtipem.SelectedIndex == 2)
                    {
                        //Safety Protector Cost hari
                        LoadChartSafetyCostHarian();
                    }
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    if (cmbtipem.SelectedIndex == 0)
                    {
                        //Material Cost bulan
                        LoadChartMaterialCostBulan();
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        //Consumable Cost bulan
                        LoadChartConsumableCostBulan();
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        //Safety Protector Cost bulan
                        LoadChartSafetyCostBulan();
                    }
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    if (cmbtipem.SelectedIndex == 0)
                    {
                        //Material Cost tahun
                        LoadChartMaterialCostTahun();
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        //Consumable Cost tahun
                        LoadChartConsumableCostTahun();
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        //Safety Protector Cost tahun
                        LoadChartSafetyCostTahun();
                    }
                }
            }
            else if (cmbpilihdata.SelectedIndex == 2)
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        //Penerimaan hari
                        LoadChartPenerimaanHarian();
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        //Perbaikan hari
                        loadChartPerbaikanHarian();
                    }
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        //Penerimaan bulan
                        LoadChartPenerimaanBulanan();
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        //Perbaikan bulan
                        LoadChartPerbaikanBulanan();
                    }                    
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        //Penerimaan tahun
                        LoadChartPenerimaanTahunan();
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        //Perbaikan tahun
                        LoadChartPerbaikanTahunan();
                    }
                }
            }
        }

        private void bulan_MouseDown(object sender, MouseEventArgs e)
        {
            if (cmbrentang.SelectedIndex == 1)
            {
                using (Form pickerForm = new Form())
                {
                    pickerForm.StartPosition = FormStartPosition.Manual;
                    pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    pickerForm.ControlBox = false;
                    pickerForm.Size = new Size(250, 200);
                    pickerForm.Text = "Pilih Bulan & Tahun";

                    var screenPos = datebulan.PointToScreen(DrawingPoint.Empty);
                    pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datebulan.Height);

                    var cmbBulan = new Guna2ComboBox
                    {
                        Font = new Font("Segoe UI", 11F),
                        Left = 10,
                        Top = 10,
                        Width = 200,
                        BorderRadius = 6,
                        ForeColor = Color.Black,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        BorderColor = Color.FromArgb(64, 64, 64),
                        BorderThickness = 2,
                    };
                    string[] bulan = {
                                    "01 - Januari", "02 - Februari", "03 - Maret", "04 - April", "05 - Mei", "06 - Juni",
                                    "07 - Juli", "08 - Agustus", "09 - September", "10 - Oktober", "11 - November", "12 - Desember"
                                };
                    cmbBulan.Items.AddRange(bulan);
                    cmbBulan.SelectedIndex = datebulan.Value.Month - 1;

                    var numTahun = new Guna2NumericUpDown
                    {
                        Font = new Font("Segoe UI", 11F),
                        Left = 10,
                        Top = 55,
                        Width = 200,
                        BorderRadius = 6,
                        Minimum = 1900,
                        Maximum = 2100,
                        ForeColor = Color.Black,
                        Value = datebulan.Value.Year,
                        BorderColor = Color.FromArgb(64, 64, 64),
                        BorderThickness = 2,
                    };

                    var btnOK = new Guna2Button
                    {
                        Text = "OK",
                        Font = new Font("Segoe UI", 10F),
                        Left = 10,
                        Top = 110,
                        Width = 80,
                        Height = 35,
                        BorderRadius = 6,
                        FillColor = Color.FromArgb(53, 53, 58)
                    };
                    btnOK.Click += (s, ev) =>
                    {
                        datebulan.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                        pickerForm.DialogResult = DialogResult.OK;
                    };

                    pickerForm.Controls.Add(cmbBulan);
                    pickerForm.Controls.Add(numTahun);
                    pickerForm.Controls.Add(btnOK);

                    pickerForm.ShowDialog();
                }

            }
            else if (cmbrentang.SelectedIndex == 2)
            {
                using (Form pickerForm = new Form())
                {
                    pickerForm.StartPosition = FormStartPosition.Manual;
                    pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    pickerForm.ControlBox = false;
                    pickerForm.Size = new Size(250, 150);
                    pickerForm.Text = "Pilih Tahun";

                    var screenPos = datebulan.PointToScreen(Point.Empty);
                    pickerForm.Location = new Point(screenPos.X, screenPos.Y + datebulan.Height);

                    var numTahun = new Guna2NumericUpDown
                    {
                        Font = new Font("Segoe UI", 11F),
                        Left = 10,
                        Top = 20,
                        Width = 200,
                        BorderRadius = 6,
                        Minimum = 1900,
                        Maximum = 2100,
                        ForeColor = Color.Black,
                        Value = datebulan.Value.Year,
                        BorderColor = Color.FromArgb(64, 64, 64),
                        BorderThickness = 2,
                    };

                    var btnOK = new Guna2Button
                    {
                        Text = "OK",
                        Font = new Font("Segoe UI", 10F),
                        Left = 10,
                        Top = 70,
                        Width = 80,
                        Height = 35,
                        BorderRadius = 6,
                        FillColor = Color.FromArgb(53, 53, 58)
                    };
                    btnOK.Click += (s, ev) =>
                    {
                        datebulan.Value = new DateTime((int)numTahun.Value, 1, 1);
                        pickerForm.DialogResult = DialogResult.OK;
                    };

                    pickerForm.Controls.Add(numTahun);
                    pickerForm.Controls.Add(btnOK);

                    pickerForm.ShowDialog();
                }
            }
        }
    }
}
