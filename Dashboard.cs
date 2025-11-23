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

        bool bukatutupfilter = false;
        private Action shiftChangedHandler;

        public Dashboard()
        {
            InitializeComponent();
        }

        private void LoadPanel()
        {
            try
            {
                conn.Open();
                int jumlah1 = 0;
                int jumlah2 = 0;
                int jumlah3 = 0;

                int penerimaanshift = 0;
                int perbaikanhift = 0;
                int pengirimanshift = 0;

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

                string query3 = "SELECT COUNT(*) FROM penerimaan_p WHERE shift = @shift AND CAST(tanggal_penerimaan AS DATE) = CAST(@tanggal AS DATE)";
                using (SqlCommand cmd3 = new SqlCommand(query3, conn))
                {
                    cmd3.Parameters.AddWithValue("@shift", MainForm.Instance.shift);
                    cmd3.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                    penerimaanshift = Convert.ToInt32(cmd3.ExecuteScalar());
                    lblpenerimaanshif.Text = penerimaanshift.ToString();
                }

                string query4 = "SELECT COUNT(*) FROM perbaikan_p WHERE shift = @shift AND CAST(tanggal_perbaikan AS DATE) = CAST(@tanggal AS DATE)";
                using (SqlCommand cmd4 = new SqlCommand(query4, conn))
                {
                    cmd4.Parameters.AddWithValue("@shift", MainForm.Instance.shift);
                    cmd4.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                    perbaikanhift = Convert.ToInt32(cmd4.ExecuteScalar());
                    lblperbaikanshift.Text = perbaikanhift.ToString();
                }

                string query5 = "SELECT COUNT(*) FROM pengiriman WHERE shift = @shift AND CAST(tanggal_pengiriman AS DATE) = CAST(@tanggal AS DATE)";
                using (SqlCommand cmd5 = new SqlCommand(query5, conn))
                {
                    cmd5.Parameters.AddWithValue("@shift", MainForm.Instance.shift);
                    cmd5.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                    pengirimanshift = Convert.ToInt32(cmd5.ExecuteScalar());
                    lblpengirimanshift.Text = pengirimanshift.ToString();
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
                    cmd.Parameters.AddWithValue("@tanggal", tanggalcustom1.Value.Date);

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
                Name = "Jumlah Harian",
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
            chartUssageMaterial.Legends.Clear();
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
                Name = "Jumlah Bulanan",
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
            chartUssageMaterial.Legends.Clear();
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
                Name = "Jumlah Tahunan",
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
            chartUssageMaterial.Legends.Clear();
        }
        private void LoadchartRBCustom()
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
    WHERE tanggal >= @TanggalMulai
      AND tanggal < DATEADD(DAY, 1, @TanggalAkhir);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                    cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);

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
                Name = "Jumlah Periode",
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
            chartUssageMaterial.Legends.Clear();
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
                        cmd.Parameters.AddWithValue("@tanggal", tanggalcustom1.Value.Date);

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

                Series series = new Series("Jumlah Penerimaan Harian")
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
                chartUssageMaterial.Legends.Clear();
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

                Series series = new Series("Jumlah Penerimaan Bulanan")
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
                chartUssageMaterial.Legends.Clear();
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

                Series series = new Series("Jumlah Penerimaan Tahunan")
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
                chartUssageMaterial.Legends.Clear();
            }
        }
        private void LoadChartPenerimaanCustom()
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
                WHERE CAST(tanggal_penerimaan AS DATE) >= @TanggalMulai
                  AND CAST(tanggal_penerimaan AS DATE) <= @TanggalAkhir";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);

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

                Series series = new Series("Jumlah Penerimaan Periode")
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
                chartUssageMaterial.Legends.Clear();
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
                        cmd.Parameters.AddWithValue("@tanggal", tanggalcustom1.Value.Date);

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
                chartUssageMaterial.Legends.Clear();
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
                chartUssageMaterial.Legends.Clear();
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
                chartUssageMaterial.Legends.Clear();
            }
        }
        private void LoadChartPerbaikanCustom()
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                       totalE4 = 0, totalS = 0, totalD = 0,
                       totalB = 0, totalBA = 0, totalBA1 = 0,
                       totalCR = 0, totalM = 0, totalR = 0,
                       totalC = 0, totalRL = 0;

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
                WHERE CAST(tanggal_perbaikan AS DATE) >= @TanggalMulai
                  AND CAST(tanggal_perbaikan AS DATE) <= @TanggalAkhir";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);

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

                Series series = new Series("Jumlah Perbaikan Periode")
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
                chartUssageMaterial.Legends.Clear();
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
                        cmd.Parameters.AddWithValue("@Hari", tanggalcustom1.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggalcustom1.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggalcustom1.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                // ===== Bersihkan chart lama =====
                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                // Reset ChartArea
                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                // ===== Tambahkan series baru =====
                Series seriesBQ = new Series("Estimasi")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "Deskripsi",
                    YValueMembers = "TotalBQ",
                    IsValueShownAsLabel = true
                };
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "Deskripsi",
                    YValueMembers = "TotalPemakaian",
                    IsValueShownAsLabel = true
                };
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

                // ===== Setup axis X =====
                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;
                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.ScaleView.Size = 5;
                axisX.ScrollBar.Enabled = true;
                axisX.ScrollBar.IsPositionedInside = true;

                // ===== Bind data =====
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
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
        private void LoadChartmaterialCostCustom()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartmaterialcostcustom", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
                        cmd.Parameters.AddWithValue("@Hari", tanggalcustom1.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggalcustom1.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggalcustom1.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);
                chartUssageMaterial.Legends.Clear();

                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
        private void LoadChartConsumableCostCustom()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartconsumablecostcustom", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
                        cmd.Parameters.AddWithValue("@Hari", tanggalcustom1.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggalcustom1.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggalcustom1.Value.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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
        private void LoadChartsafetyCostCustom()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartsafetycostcustom", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi");
                seriesBQ.ChartType = SeriesChartType.Column;
                seriesBQ.XValueMember = "Deskripsi";
                seriesBQ.YValueMembers = "TotalBQ";
                seriesBQ.IsValueShownAsLabel = true;
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual");
                seriesPemakaian.ChartType = SeriesChartType.Column;
                seriesPemakaian.XValueMember = "Deskripsi";
                seriesPemakaian.YValueMembers = "TotalPemakaian";
                seriesPemakaian.IsValueShownAsLabel = true;
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

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

        private void registerpenerimaanp()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.penerimaan_p", conn))
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
        private void registerperbaikanp()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.perbaikan_p", conn))
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
        private void registerpengiriman()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.pengiriman", conn))
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

        private void Dashboard_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());

            shiftChangedHandler = () => LoadPanel();
            MainForm.Instance.ShiftChanged += shiftChangedHandler;

            tanggalcustom1.Value = DateTime.Today;
            tanggalcustom2.Value = DateTime.Today;
            datebulan.Value = DateTime.Now;
            LoadPanel();
            LoadchartRB();
            containerrentang.Visible = true;
            containertanggal1.Visible = true;
            lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");

            registerpenerimaans();
            registerperbaikans();
            registerpenerimaanp();
            registerperbaikanp();
            registerpengiriman();
        }

        private void cmbpilihdata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedIndex == 0)
            {   
                containerbulan.Visible = false;
                containertipe.Visible = false;
                containertipem.Visible = false;
                containertanggal1.Visible = false;

                containerrentang.Visible = true;
                containertanggal1.Visible = true;
                cmbrentang.SelectedIndex = 0;
                tanggalcustom1.Value = DateTime.Now;
            }
            else if (cmbpilihdata.SelectedIndex == 1)
            {
                containerbulan.Visible = false;
                containertipe.Visible = false;
                containertanggal1.Visible = false;

                containerrentang.Visible = true;
                containertipem.Visible = true;
                containertanggal1.Visible = true;
                cmbrentang.SelectedIndex = 0;
                cmbtipem.SelectedIndex = 0;
                tanggalcustom1.Value = DateTime.Now;
            }
            else if (cmbpilihdata.SelectedIndex == 2)
            {
                containerbulan.Visible = false;
                containertipem.Visible = false;
                containertanggal1.Visible = false;

                containerrentang.Visible = true;
                containertipe.Visible = true;
                containertanggal1.Visible = true;
                cmbrentang.SelectedIndex = 0;
                cmbtipe.SelectedIndex = 0;
                tanggalcustom1.Value = DateTime.Now;
            }
        }

        private void cmbrentang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbrentang.SelectedIndex == 0) 
            {
                containerbulan.Visible = false;
                containertanggal2.Visible = false;
                containertanggal1.Visible=true;
            }
            else if(cmbrentang.SelectedIndex == 1)
            {
                containertanggal1.Visible = false;
                containertanggal2.Visible = false;
                containerbulan.Visible=true;
            }
            else if(cmbrentang.SelectedIndex == 2)
            {
                containertanggal1.Visible = false;
                containertanggal2.Visible = false;
                containerbulan.Visible=true;
            }
            else if (cmbrentang.SelectedIndex == 3)
            {
                containerbulan.Visible = false;
                containertanggal1.Visible=true;
                containertanggal2.Visible=true;
            }
        }

        private void btnsetfilter_Click(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedIndex == 0) 
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    LoadchartRB();
                    lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    LoadchartRBByMonth();
                    lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    LoadchartRBByYear();
                    lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                }
                else if (cmbrentang.SelectedIndex == 3)
                {
                    if (tanggalcustom1.Value.Date > tanggalcustom2.Value.Date)
                    {
                        MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    LoadchartRBCustom();
                    lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                }
            }
            else if(cmbpilihdata.SelectedIndex == 1)
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    if(cmbtipem.SelectedIndex == 0)
                    {
                        LoadChartMaterialCostHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                    else if(cmbtipem.SelectedIndex == 1)
                    {
                        LoadChartConsumableCostHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                    else if(cmbtipem.SelectedIndex == 2)
                    {
                        LoadChartSafetyCostHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    if (cmbtipem.SelectedIndex == 0)
                    {
                        LoadChartMaterialCostBulan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        LoadChartConsumableCostBulan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        LoadChartSafetyCostBulan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    if (cmbtipem.SelectedIndex == 0)
                    {
                        LoadChartMaterialCostTahun();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        LoadChartConsumableCostTahun();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        LoadChartSafetyCostTahun();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                }
                else if (cmbrentang.SelectedIndex == 3)
                {
                    if(tanggalcustom1.Value.Date > tanggalcustom2.Value.Date)
                    {
                        MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (cmbtipem.SelectedIndex == 0)
                    {
                        LoadChartmaterialCostCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        LoadChartConsumableCostCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        LoadChartsafetyCostCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                    }
                }
            }
            else if (cmbpilihdata.SelectedIndex == 2)
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        LoadChartPenerimaanHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        loadChartPerbaikanHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        LoadChartPenerimaanBulanan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        LoadChartPerbaikanBulanan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }                    
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        LoadChartPenerimaanTahunan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        LoadChartPerbaikanTahunan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                }
                else if (cmbrentang.SelectedIndex == 3)
                {
                    if (tanggalcustom1.Value.Date > tanggalcustom2.Value.Date)
                    {
                        MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        LoadChartPenerimaanCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        LoadChartPerbaikanCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
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

        private void HamburgerButton_Click(object sender, EventArgs e)
        {
            if (bukatutupfilter)
            {
                panelfilter.Visible = false;
                bukatutupfilter = false;
                HamburgerButton.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
            }
            else
            {
                panelfilter.Visible = true;
                bukatutupfilter = true;
                HamburgerButton.IconChar = FontAwesome.Sharp.IconChar.AngleRight;
            }
        }

        private void Dashboard_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (shiftChangedHandler != null)
            {
                MainForm.Instance.ShiftChanged -= shiftChangedHandler;
                shiftChangedHandler = null;
            }
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }
    }
}
