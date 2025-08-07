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
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class Dashboard : Form
    {
        SqlConnection conn = Koneksi.GetConnection();
        public Dashboard()
        {
            InitializeComponent();
            LoadchartRB();
            LoadPanel();
            LoadChartStock();
        }

        private void LoadPanel()
        {
            try
            {
                int jumlah1 = 0;
                int jumlah2 = 0;
                int jumlah3 = 0;
                conn.Open();

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

    }
}
