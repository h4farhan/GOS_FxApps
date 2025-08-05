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
    public partial class historyPengiriman : Form
    {

        SqlConnection conn = Koneksi.GetConnection();

        public historyPengiriman()
        {
            InitializeComponent();
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM pengiriman ORDER BY tanggal_pengiriman DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Pengiriman";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Diubah";
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
        }

        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string inputRod = txtcari.Text.Trim();
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod) && !shiftValid)
            {
                MessageBox.Show("Silakan isi tanggal, nomor ROD, atau shift untuk melakukan pencarian.", "Warning");
                btnreset.Enabled = false;
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM pengiriman WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += "AND CAST(tanggal_pengiriman AS DATE) = @tgl";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(inputRod))
                {
                    query += " AND nomor_rod = @rod";
                    cmd.Parameters.AddWithValue("@rod", Convert.ToInt32(inputRod));
                }

                if (shiftValid)
                {
                    query += " AND shift = @shift";
                    cmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem.ToString());
                }

                cmd.CommandText = query;
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                        btnreset.Enabled=true;
                    }

                    dataGridView1.DataSource = dt;
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
                return dt.Rows.Count > 0;
            }
        }

        private void btncari_Click(object sender, EventArgs e)
        {
            cari();
        }

        private void historyPengiriman_Load(object sender, EventArgs e)
        {
            tampil();
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
        }

        private void btnreset_Click_1(object sender, EventArgs e)
        {
            tampil();
            cbShift.SelectedIndex = 0;
            txtcari.Text = "";
            datecari.Checked = false;
            btnreset.Enabled = false;
        }

    }
}
