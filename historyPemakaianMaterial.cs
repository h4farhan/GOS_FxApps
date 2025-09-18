using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class historyPemakaianMaterial : Form
    {

        SqlConnection conn = Koneksi.GetConnection();

        public historyPemakaianMaterial()
        {
            InitializeComponent();
        }

        private void registertampil()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.pemakaian_material", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampil();
                            registertampil();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT idPemakaian, kodeBarang, namaBarang, spesifikasi, type, tanggalPemakaian, jumlahPemakaian, updated_at, remaks FROM pemakaian_material ORDER BY tanggalPemakaian DESC, updated_at DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Kode Barang";
                dataGridView1.Columns[2].HeaderText = "Nama Barang";
                dataGridView1.Columns[3].HeaderText = "Spesifikasi";
                dataGridView1.Columns[4].HeaderText = "Tipe";
                dataGridView1.Columns[5].HeaderText = "Tanggal Pemakaian";
                dataGridView1.Columns[6].HeaderText = "Jumlah Pemakaian";
                dataGridView1.Columns[7].HeaderText = "Diubah";
                dataGridView1.Columns[8].HeaderText = "Remaks";
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
            string keyword = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Silakan isi Tanggal atau Kode/Nama Barang untuk melakukan pencarian.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            idPemakaian, 
            kodeBarang, 
            namaBarang, 
            spesifikasi, 
            type, 
            tanggalPemakaian, 
            jumlahPemakaian, 
            updated_at, 
            remaks
        FROM pemakaian_material
        WHERE 1=1 ";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += "AND CAST(tanggalPemakaian AS DATE) = @tgl ";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    query += "AND (kodeBarang LIKE @kode OR namaBarang LIKE @kode) ";
                    cmd.Parameters.AddWithValue("@kode", "%" + keyword + "%");
                }

                query += "ORDER BY tanggalPemakaian DESC, updated_at DESC";

                cmd.CommandText = query;
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                        btnreset.Enabled = true;
                    }

                    dataGridView1.DataSource = dt;

                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView1.RowTemplate.Height = 35;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dataGridView1.Columns.Contains("idPemakaian"))
                        dataGridView1.Columns["idPemakaian"].Visible = false;

                    dataGridView1.Columns["kodeBarang"].HeaderText = "Kode Barang";
                    dataGridView1.Columns["namaBarang"].HeaderText = "Nama Barang";
                    dataGridView1.Columns["spesifikasi"].HeaderText = "Spesifikasi";
                    dataGridView1.Columns["type"].HeaderText = "Tipe";
                    dataGridView1.Columns["tanggalPemakaian"].HeaderText = "Tanggal Pemakaian";
                    dataGridView1.Columns["jumlahPemakaian"].HeaderText = "Jumlah Pemakaian";
                    dataGridView1.Columns["updated_at"].HeaderText = "Diubah";
                    dataGridView1.Columns["remaks"].HeaderText = "Remaks";
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

            return dt.Rows.Count > 0;
        }

        private void jumlahdata()
        {
            int total = dataGridView1.Rows.Count;
            lbljumlahdata.Text = "Jumlah data: " + total.ToString();
        }

        private void btncari_Click(object sender, EventArgs e)
        {
            cari();
            jumlahdata();
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void historyPemakaianMaterial_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            tampil();
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            registertampil();
            jumlahdata();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            tampil();
            txtcari.Text = "";
            datecari.Checked = false;
            btnreset.Enabled = false;
            jumlahdata();
        }

        private void historyPemakaianMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
        }
    }
}
