using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace GOS_FxApps
{
    public partial class editpmaterial : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        bool infocari = false;
        int noprimary;

        public editpmaterial()
        {
            InitializeComponent();
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM pemakaian_material";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Kode Barang";
                dataGridView1.Columns[2].HeaderText = "Nama Barang";
                dataGridView1.Columns[3].HeaderText = "Tanggal Pemakaian";
                dataGridView1.Columns[4].HeaderText = "Jumlah Pemakaian";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string kodeBarang = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(kodeBarang))
            {
                MessageBox.Show("Silakan isi Tanggal atau Kode Barang untuk melakukan pencarian.");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM pemakaian_material WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += "AND CAST(tanggalPemakaian AS DATE) = @tgl";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(kodeBarang))
                {
                    query += " AND kodeBarang = @kode";
                    cmd.Parameters.AddWithValue("@kode", kodeBarang);
                }

                cmd.CommandText = query;
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat pencarian: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return dt.Rows.Count > 0;
            }
        }


        private void editpmaterial_Load(object sender, EventArgs e)
        {
            tampil();
            datecari.Checked = false;
        }

        private void btncari_Click(object sender, EventArgs e)
        {
            if (!infocari)
            {
                bool hasilCari = cari();
                if (hasilCari)
                {
                    infocari = true;
                    btncari.Text = "Reset";
                }
                else
                {
                    infocari = false;
                    btncari.Text = "Cari";
                }
            }
            else
            {
                tampil();
                infocari = false;
                btncari.Text = "Cari";

                txtcari.Text = "";
                datecari.Checked = false;
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtjumlah.Clear();
            btnupdate.Enabled = false;
            btnclear.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["idPemakaian"].Value);
                datepemakaian.Value = Convert.ToDateTime(row.Cells["tanggalPemakaian"].Value);
                txtjumlah.Text = row.Cells["jumlahPemakaian"].Value.ToString();

                btnclear.Enabled = true;
                btnupdate.Enabled = true;
            }
        }
    }
}
