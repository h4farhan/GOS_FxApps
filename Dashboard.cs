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
using System.Threading;

namespace GOS_FxApps
{
    public partial class Dashboard : Form
    {
        public static Dashboard Instance;

        bool bukatutupfilter = false;
        private Action shiftChangedHandler;

        public Dashboard()
        {
            InitializeComponent();
        }

        private async Task LoadPanel()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    string queryJumlah = @"
                SELECT 
                    (SELECT COUNT(*) FROM penerimaan_s) AS jPenerimaanS,
                    (SELECT COUNT(*) FROM perbaikan_s) AS jPerbaikanS,
                    (SELECT COUNT(*) FROM penerimaan_p WHERE shift = @shift AND CAST(tanggal_penerimaan AS DATE) = CAST(@tanggal AS DATE)) AS jPenerimaanShift,
                    (SELECT COUNT(*) FROM perbaikan_p WHERE shift = @shift AND CAST(tanggal_perbaikan AS DATE) = CAST(@tanggal AS DATE)) AS jPerbaikanShift,
                    (SELECT COUNT(*) FROM pengiriman WHERE shift = @shift AND CAST(tanggal_pengiriman AS DATE) = CAST(@tanggal AS DATE)) AS jPengirimanShift
            ";

                    using (var cmd = new SqlCommand(queryJumlah, conn))
                    {
                        cmd.Parameters.AddWithValue("@shift", MainForm.Instance.shift);
                        cmd.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int jumlah1 = reader.GetInt32(reader.GetOrdinal("jPenerimaanS"));
                                int jumlah2 = reader.GetInt32(reader.GetOrdinal("jPerbaikanS"));
                                int jumlah3 = jumlah1 + jumlah2;

                                int penerimaanshift = reader.GetInt32(reader.GetOrdinal("jPenerimaanShift"));
                                int perbaikanshift = reader.GetInt32(reader.GetOrdinal("jPerbaikanShift"));
                                int pengirimanshift = reader.GetInt32(reader.GetOrdinal("jPengirimanShift"));

                                lblubrepaired.Text = jumlah1.ToString();
                                label15.Text = jumlah2.ToString();
                                label3.Text = jumlah3.ToString();
                                lblpenerimaanshif.Text = penerimaanshift.ToString();
                                lblperbaikanshift.Text = perbaikanshift.ToString();
                                lblpengirimanshift.Text = pengirimanshift.ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private async Task LoadChartstokreject()
        {
            double e1 = 0, e2 = 0, e3 = 0, s = 0, d = 0, b = 0, ba = 0, r = 0, m = 0, cr = 0, c = 0, rl = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
            SELECT 
                SUM(e1) AS e1, SUM(e2) AS e2, SUM(e3) AS e3,
                SUM(s) AS s, SUM(d) AS d, SUM(b) AS b, SUM(ba) AS ba,
                SUM(r) AS r, SUM(m) AS m, SUM(cr) AS cr, SUM(c) AS c, SUM(rl) AS rl
            FROM penerimaan_s
        ", conn))
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        e1 = dr["e1"] != DBNull.Value ? Convert.ToDouble(dr["e1"]) : 0;
                        e2 = dr["e2"] != DBNull.Value ? Convert.ToDouble(dr["e2"]) : 0;
                        e3 = dr["e3"] != DBNull.Value ? Convert.ToDouble(dr["e3"]) : 0;
                        s = dr["s"] != DBNull.Value ? Convert.ToDouble(dr["s"]) : 0;
                        d = dr["d"] != DBNull.Value ? Convert.ToDouble(dr["d"]) : 0;
                        b = dr["b"] != DBNull.Value ? Convert.ToDouble(dr["b"]) : 0;
                        ba = dr["ba"] != DBNull.Value ? Convert.ToDouble(dr["ba"]) : 0;
                        r = dr["r"] != DBNull.Value ? Convert.ToDouble(dr["r"]) : 0;
                        m = dr["m"] != DBNull.Value ? Convert.ToDouble(dr["m"]) : 0;
                        cr = dr["cr"] != DBNull.Value ? Convert.ToDouble(dr["cr"]) : 0;
                        c = dr["c"] != DBNull.Value ? Convert.ToDouble(dr["c"]) : 0;
                        rl = dr["rl"] != DBNull.Value ? Convert.ToDouble(dr["rl"]) : 0;
                    }
                }
            }
            catch
            {
                return;
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
                Name = "Total Penerimaan",
                ChartType = SeriesChartType.Column,
                IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            series.Points.AddXY("E1", e1);
            series.Points.AddXY("E2", e2);
            series.Points.AddXY("E3", e3);
            series.Points.AddXY("S", s);
            series.Points.AddXY("D", d);
            series.Points.AddXY("B", b);
            series.Points.AddXY("BA", ba);
            series.Points.AddXY("R", r);
            series.Points.AddXY("M", m);
            series.Points.AddXY("CR", cr);
            series.Points.AddXY("C", c);
            series.Points.AddXY("RL", rl);

            chartUssageMaterial.Series.Add(series);
            chartUssageMaterial.Legends.Clear();
        }

        private async Task LoadchartRB()
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
                using (var conn = await Koneksi.GetConnectionAsync())
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT TOP 1 bstok, bpe1, bpe2, bbe1, bbe2, wpe1, wpe2, wbe1, wbe2, tanggal, id_stok
                    FROM Rb_Stok
                    WHERE tanggal >= @tanggal AND tanggal < DATEADD(DAY, 1, @tanggal)
                    ORDER BY id_stok DESC", conn))
                {

                    cmd.CommandTimeout = 3;
                    cmd.Parameters.AddWithValue("@tanggal", tanggalcustom1.Value.Date);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
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
            catch
            {
                return;
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

        private async Task LoadchartRBByMonth()
        {
            double rbStock = 0, rbSawinge1 = 0, rbSawinge2 = 0,
                   rblathee1 = 0, rblathee2 = 0,
                   wpsawinge1 = 0, wpsawinge2 = 0,
                   wplathee1 = 0, wplathee2 = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {

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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
        private async Task LoadchartRBByYear()
        {
            double rbStock = 0, rbSawinge1 = 0, rbSawinge2 = 0,
                   rblathee1 = 0, rblathee2 = 0,
                   wpsawinge1 = 0, wpsawinge2 = 0,
                   wplathee1 = 0, wplathee2 = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                { 

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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
        private async Task LoadchartRBCustom()
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
                using (var conn = await Koneksi.GetConnectionAsync())
                {
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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if ( await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

        private async Task LoadChartPenerimaanHarian()
        {
            double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                   totalS = 0, totalD = 0, totalB = 0,
                   totalBA = 0, totalCR = 0, totalM = 0,
                   totalR = 0, totalC = 0, totalRL = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
        private async Task LoadChartPenerimaanBulanan()
        {

            double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                   totalS = 0, totalD = 0, totalB = 0,
                   totalBA = 0, totalCR = 0, totalM = 0,
                   totalR = 0, totalC = 0, totalRL = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
        private async Task LoadChartPenerimaanTahunan()
        {
            
                double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                       totalS = 0, totalD = 0, totalB = 0,
                       totalBA = 0, totalCR = 0, totalM = 0,
                       totalR = 0, totalC = 0, totalRL = 0;

                try
                {
                    using (var conn = await Koneksi.GetConnectionAsync())
                    {
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

                            using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                            {
                                if (await dr.ReadAsync())
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
                }
                catch (SqlException)
                {
                    MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                     "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
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
        private async Task LoadChartPenerimaanCustom()
        {
            double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                   totalS = 0, totalD = 0, totalB = 0,
                   totalBA = 0, totalCR = 0, totalM = 0,
                   totalR = 0, totalC = 0, totalRL = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

        private async Task loadChartPerbaikanHarian()
        {

            double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                   totalS = 0, totalD = 0, totalB = 0,
                   totalBA = 0, totalCR = 0, totalM = 0,
                   totalR = 0, totalC = 0, totalRL = 0,
                   totalBA1 = 0, totalE4 = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
        private async Task LoadChartPerbaikanBulanan()
        {
            double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                   totalS = 0, totalD = 0, totalB = 0,
                   totalBA = 0, totalCR = 0, totalM = 0,
                   totalR = 0, totalC = 0, totalRL = 0,
                   totalBA1 = 0, totalE4 = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
        private async Task LoadChartPerbaikanTahunan()
        {
            double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                   totalS = 0, totalD = 0, totalB = 0,
                   totalBA = 0, totalCR = 0, totalM = 0,
                   totalR = 0, totalC = 0, totalRL = 0,
                   totalBA1 = 0, totalE4 = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
        private async Task LoadChartPerbaikanCustom()
        {
            double totalE1 = 0, totalE2 = 0, totalE3 = 0,
                   totalE4 = 0, totalS = 0, totalD = 0,
                   totalB = 0, totalBA = 0, totalBA1 = 0,
                   totalCR = 0, totalM = 0, totalR = 0,
                   totalC = 0, totalRL = 0;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
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

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

        private async Task LoadChartMaterialCostHarian()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartmaterialcostharian", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Hari", tanggalcustom1.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggalcustom1.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggalcustom1.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader); 
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartMaterialCostBulan()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartmaterialcostbulan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartMaterialCostTahun()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartmaterialcosttahun", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartmaterialCostCustom()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartmaterialcostcustom", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async Task LoadChartConsumableCostHarian()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartconsumablecostharian", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Hari", tanggalcustom1.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggalcustom1.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggalcustom1.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartConsumableCostBulan()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartconsumablecostbulan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartConsumableCostTahun()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartconsumablecosttahun", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartConsumableCostCustom()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartconsumablecostcustom", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async Task LoadChartSafetyCostHarian()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartsafetycostharian", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Hari", tanggalcustom1.Value.Day);
                        cmd.Parameters.AddWithValue("@Bulan", tanggalcustom1.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", tanggalcustom1.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartSafetyCostBulan()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartsafetycostbulan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartSafetyCostTahun()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartsafetycosttahun", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Tahun", datebulan.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async Task LoadChartsafetyCostCustom()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chartsafetycostcustom", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
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
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                 "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                switch (table)
                {
                    case "penerimaan_s":
                        await LoadPanel();
                        break;

                    case "penerimaan_p":
                        await LoadPanel();
                        break;

                    case "perbaikan_s":
                        await LoadPanel();
                        break;

                    case "perbaikan_p":
                        await LoadPanel();
                        break;

                    case "pengiriman":
                        await LoadPanel();
                        break;

                    default:
                        break;
                }
            }
            catch { }
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            shiftChangedHandler = async () => await LoadPanel();
            MainForm.Instance.ShiftChanged += shiftChangedHandler;

            await LoadPanel();
            await LoadChartstokreject();

            tanggalcustom1.Value = DateTime.Today;
            tanggalcustom2.Value = DateTime.Today;
            datebulan.Value = DateTime.Now;

            lbltitlechart.Text =
                $"{cmbpilihdata.Text}";
        }

        private void Dashboard_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;

            if (shiftChangedHandler != null)
            {
                MainForm.Instance.ShiftChanged -= shiftChangedHandler;
                shiftChangedHandler = null;
            }
        }

        private void cmbpilihdata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedIndex == 0)
            {
                containerrentang.Visible = false;
                containertanggal1.Visible = false;
                containertanggal2.Visible = false;
                containerbulan.Visible = false;
                containertipe.Visible = false;
                containertipem.Visible = false;
            }
            else if (cmbpilihdata.SelectedIndex == 1)
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
            else if (cmbpilihdata.SelectedIndex == 2)
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
            else if (cmbpilihdata.SelectedIndex == 3)
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

        private async void btnsetfilter_Click(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedIndex == 0)
            {
                await LoadChartstokreject();
            }
            else if (cmbpilihdata.SelectedIndex == 1)
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    await LoadchartRB();
                    lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    await LoadchartRBByMonth();
                    lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    await LoadchartRBByYear();
                    lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                }
                else if (cmbrentang.SelectedIndex == 3)
                {
                    if (tanggalcustom1.Value.Date > tanggalcustom2.Value.Date)
                    {
                        MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    await LoadchartRBCustom();
                    lbltitlechart.Text = cmbpilihdata.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                }
            }
            else if (cmbpilihdata.SelectedIndex == 2)
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    if (cmbtipem.SelectedIndex == 0)
                    {
                        await LoadChartMaterialCostHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        await LoadChartConsumableCostHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        await LoadChartSafetyCostHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    if (cmbtipem.SelectedIndex == 0)
                    {
                        await LoadChartMaterialCostBulan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        await LoadChartConsumableCostBulan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        await LoadChartSafetyCostBulan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    if (cmbtipem.SelectedIndex == 0)
                    {
                        await LoadChartMaterialCostTahun();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        await LoadChartConsumableCostTahun();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        await LoadChartSafetyCostTahun();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                }
                else if (cmbrentang.SelectedIndex == 3)
                {
                    if (tanggalcustom1.Value.Date > tanggalcustom2.Value.Date)
                    {
                        MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (cmbtipem.SelectedIndex == 0)
                    {
                        await LoadChartmaterialCostCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipem.SelectedIndex == 1)
                    {
                        await LoadChartConsumableCostCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipem.SelectedIndex == 2)
                    {
                        await LoadChartsafetyCostCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipem.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                    }
                }
            }
            else if (cmbpilihdata.SelectedIndex == 3)
            {
                if (cmbrentang.SelectedIndex == 0)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        await LoadChartPenerimaanHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        await loadChartPerbaikanHarian();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy");
                    }
                }
                else if (cmbrentang.SelectedIndex == 1)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        await LoadChartPenerimaanBulanan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        await LoadChartPerbaikanBulanan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + datebulan.Value.Month + "/" + datebulan.Value.Year;
                    }
                }
                else if (cmbrentang.SelectedIndex == 2)
                {
                    if (cmbtipe.SelectedIndex == 0)
                    {
                        await LoadChartPenerimaanTahunan();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + datebulan.Value.Year;
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        await LoadChartPerbaikanTahunan();
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
                        await LoadChartPenerimaanCustom();
                        lbltitlechart.Text = cmbpilihdata.Text + " " + cmbtipe.Text + " " + cmbrentang.Text + " " + tanggalcustom1.Value.ToString("dd/MM/yyyy") + " s/d " + tanggalcustom2.Value.ToString("dd/MM/yyyy");
                    }
                    else if (cmbtipe.SelectedIndex == 1)
                    {
                        await LoadChartPerbaikanCustom();
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

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            await LoadChartstokreject();
        }
    }
}
