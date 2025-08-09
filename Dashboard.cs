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

namespace GOS_FxApps
{
    public partial class Dashboard : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public static Dashboard Instance;

        public Dashboard()
        {
            InitializeComponent();
            LoadchartRB();
            LoadPanel();
            LoadChartStock();
            Instance = this;
            if (MainForm.Instance != null && MainForm.Instance.role != null)
            {
                btntiga.Visible = (MainForm.Instance.role == "Operator Gudang");
            }
            else
            {
                btntiga.Visible = false; 
            }

        }

        private void LoadNotifikasi()
        {
            panelNotif.Controls.Clear();
            panelNotif.AutoScroll = true;

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                conn.Open();

                string query1 = @"
                                SELECT namaBarang, jumlahStok, min_stok
                                FROM stok_material
                                WHERE jumlahStok < min_stok";

                SqlCommand cmd1 = new SqlCommand(query1, conn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    string nama = reader1["namaBarang"].ToString();
                    int stok = Convert.ToInt32(reader1["jumlahStok"]);
                    int minStok = Convert.ToInt32(reader1["min_stok"]);
                    AddNotifPanel($"Barang {nama} stok rendah ({stok}/{minStok})", Color.Red);
                }
                reader1.Close();

                string query2 = @"
                                SELECT TOP 1 bstok, bpe1, bpe2, bbe1, bbe2, wpe1, wpe2, wbe1, wbe2
                                FROM Rb_Stok
                                ORDER BY id_stok DESC";

                SqlCommand cmd2 = new SqlCommand(query2, conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();

                Dictionary<string, int> stokData = new Dictionary<string, int>();

                if (reader2.Read())
                {
                    string[] kolomList = { "bstok", "bpe1", "bpe2", "bbe1", "bbe2", "wpe1", "wpe2", "wbe1", "wbe2" };
                    foreach (string kolom in kolomList)
                    {
                        stokData[kolom] = Convert.ToInt32(reader2[kolom]);
                    }
                }
                reader2.Close(); 

                foreach (var item in stokData)
                {
                    using (SqlCommand cmdMin = new SqlCommand("SELECT namaTampilan, min_stok FROM setmin_Rb WHERE kode = @kode", conn))
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
                                    AddNotifPanel($"{nama} stok rendah ({item.Value}/{minStok})", Color.DarkOrange);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AddNotifPanel(string text, Color warna)
        {
            Panel itemPanel = new Panel();
            itemPanel.Width = panelNotif.Width - 25;
            itemPanel.Height = 70;
            itemPanel.BackColor = Color.White;
            itemPanel.Margin = new Padding(3);
            itemPanel.Padding = new Padding(10);

            Label icon = new Label();
            icon.Text = "🔔";
            icon.Font = new Font("Segoe UI Emoji", 16);
            icon.Dock = DockStyle.Left;
            icon.Width = 40;
            icon.TextAlign = ContentAlignment.MiddleCenter;

            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.ForeColor = warna;

            itemPanel.Controls.Add(lbl);
            itemPanel.Controls.Add(icon);

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
                string query = "SELECT TOP 1 bstok, bpe1, bpe2, bbe1, bbe2, wpe1, wpe2, wbe1, wbe2" +   
                    " FROM Rb_Stok ORDER BY id_stok DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
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
            
            chartRoundbar.Series.Clear();
            Series series = new Series();
            series.Name = "DataSeries";
            series.ChartType = SeriesChartType.Column;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Black;

            series.Points.AddXY("Roundbar Stock", rbStock);
            series.Points.AddXY("Rounbar Sawing E1", rbSawinge1);
            series.Points.AddXY("Rounbar Sawing E2", rbSawinge2);
            series.Points.AddXY("Rounbar Lathe E1", rblathee1);
            series.Points.AddXY("Rounbar Lathe E2", rblathee2);
            series.Points.AddXY("Welding Pieces Sawing E1", wpsawinge1);
            series.Points.AddXY("Welding Pieces Sawing E2", wpsawinge2);
            series.Points.AddXY("Welding Pieces Lathe E1", wplathee1);
            series.Points.AddXY("Welding Pieces Lathe E2", wplathee2);

            series.Points[0].Color = Color.SeaGreen;
            series.Points[1].Color = Color.SeaGreen;
            series.Points[2].Color = Color.SeaGreen;
            series.Points[3].Color = Color.SeaGreen;
            series.Points[4].Color = Color.SeaGreen;
            series.Points[5].Color = Color.SeaGreen;
            series.Points[6].Color = Color.SeaGreen;
            series.Points[7].Color = Color.SeaGreen;
            series.Points[8].Color = Color.SeaGreen;

            chartRoundbar.Series.Add(series);

            chartRoundbar.ChartAreas[0].AxisX.Interval = 1;   
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

        private void Dashboard_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());

            typeof(Panel).InvokeMember("DoubleBuffered",
               System.Reflection.BindingFlags.SetProperty |
               System.Reflection.BindingFlags.Instance |
               System.Reflection.BindingFlags.NonPublic,
               null, panelNotif, new object[] { true });
            LoadNotifikasi();

            registerpenerimaans();
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

    }
}
