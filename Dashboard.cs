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

                // Query 1: penerimaan_s
                string query1 = "SELECT COUNT(*) FROM penerimaan_s";
                using (SqlCommand cmd1 = new SqlCommand(query1, conn))
                {
                    jumlah1 = Convert.ToInt32(cmd1.ExecuteScalar());
                    lblubrepaired.Text = jumlah1.ToString();
                }

                // Query 2: pengiriman_s
                string query2 = "SELECT COUNT(*) FROM perbaikan_s";
                using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                {
                    jumlah2 = Convert.ToInt32(cmd2.ExecuteScalar());
                    label15.Text = jumlah2.ToString();
                }
                jumlah3 = jumlah1 + jumlah2;
                label3.Text = jumlah3.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
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
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi Kesalahan: " + ex.Message);
            }
            finally 
            { 
                conn.Close(); 
            }
            
            // Buat series untuk data kolom
            chartRoundbar.Series.Clear();
            Series series = new Series();
            series.Name = "DataSeries";
            series.ChartType = SeriesChartType.Column;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Gainsboro;

            

            // Tambahkan data ke series
            series.Points.AddXY("Roundbar Stock", rbStock);
            series.Points.AddXY("Rounbar Sawing E1", rbSawinge1);
            series.Points.AddXY("Rounbar Sawing E2", rbSawinge2);
            series.Points.AddXY("Rounbar Lathe E1", rblathee1);
            series.Points.AddXY("Rounbar Lathe E2", rblathee2);
            series.Points.AddXY("Welding Pieces Sawing E1", wpsawinge1);
            series.Points.AddXY("Welding Pieces Sawing E2", wpsawinge2);
            series.Points.AddXY("Welding Pieces Lathe E1", wplathee1);
            series.Points.AddXY("Welding Pieces Lathe E2", wplathee2);

            //Styling
            series.Points[0].Color = Color.Red;
            series.Points[1].Color = Color.Lime;
            series.Points[2].Color = Color.Lime;
            series.Points[3].Color = Color.Aqua;
            series.Points[4].Color = Color.Aqua;
            series.Points[5].Color = Color.Orange;
            series.Points[6].Color = Color.Orange;
            series.Points[7].Color = Color.Violet;
            series.Points[8].Color = Color.Violet;

            // Tambahkan series ke chart
            chartRoundbar.Series.Add(series);

            chartRoundbar.ChartAreas[0].AxisX.Interval = 1;   
        }

        private void LoadChartStock()
        {
            chartUssageMaterial.Series.Clear();

            Series series = new Series("Stock Material");
            series.ChartType = SeriesChartType.Column;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Gainsboro;

            try
            {
                conn.Open();
                string query = "SELECT namaBarang, jumlahStok FROM stok_material";

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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            // Styling Chart
            series.Color = Color.SteelBlue;
            chartUssageMaterial.Series.Add(series);
            chartUssageMaterial.ChartAreas[0].AxisX.Interval = 1;
            chartUssageMaterial.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // miringkan label agar tidak tabrakan
            chartUssageMaterial.ChartAreas[0].AxisX.Title = "Nama Barang";
            chartUssageMaterial.ChartAreas[0].AxisY.Title = "Jumlah Stok";
        }

    }
}
