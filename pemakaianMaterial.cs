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

namespace GOS_FxApps
{
    public partial class pemakaianMaterial : Form
    {

        SqlConnection conn = Koneksi.GetConnection();

        public static pemakaianMaterial instance;

        public pemakaianMaterial()
        {
            InitializeComponent();
            instance = this;
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

        private void cari()
        {
            string keyword = txtcari.Text;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM pemakaian_material WHERE kodeBarang LIKE @keyword OR namaBarang LIKE @keyword OR " +
                "tanggalPemakaian LIKE @keyword", conn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
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
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form frmstok = new formstok();
            frmstok.ShowDialog();
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void combonama()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM stok_material", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbnama.DataSource = dt;
                cmbnama.DisplayMember = "namaBarang";
                cmbnama.ValueMember = "kodeBarang";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror Combobox " + ex.Message);
            }
            finally
            { 
                conn.Close();
            }
        }

        private void pemakaianMaterial_Load(object sender, EventArgs e)
        {
            combonama();
            tampil();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string kodeBarang = cmbnama.SelectedValue.ToString();
            string namaBarang = cmbnama.Text;
            if (!int.TryParse(txtjumlah.Text, out int jumlahPakai))
            {
                MessageBox.Show("Masukkan jumlah yang valid.");
                return;
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT jumlahPemakaian FROM pemakaian_material WHERE kodeBarang = @kode AND tanggalPemakaian = @tgl", conn);
                cmd.Parameters.AddWithValue("@kode", kodeBarang);
                cmd.Parameters.AddWithValue("@tgl", datepemakaian.Value.Date);

                bool sudahAda = false;
                int jumlahSebelumnya = 0;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        sudahAda = true;
                        jumlahSebelumnya = Convert.ToInt32(dr["jumlahPemakaian"]);
                    }
                }

                SqlCommand cmdCek = new SqlCommand("SELECT jumlahStok FROM stok_material WHERE kodeBarang = @kode", conn);
                cmdCek.Parameters.AddWithValue("@kode", kodeBarang);
                int stokSekarang = Convert.ToInt32(cmdCek.ExecuteScalar());

                if (jumlahPakai > stokSekarang)
                {
                    MessageBox.Show("Stok tidak cukup.");
                    return;
                }

                if (sudahAda)
                {
                    SqlCommand cmdUpdatePemakaian = new SqlCommand("UPDATE pemakaian_material SET jumlahPemakaian = jumlahPemakaian + @jumlahBaru WHERE kodeBarang = @kode AND tanggalPemakaian = @tgl", conn);
                    cmdUpdatePemakaian.Parameters.AddWithValue("@jumlahBaru", jumlahPakai);
                    cmdUpdatePemakaian.Parameters.AddWithValue("@kode", kodeBarang);
                    cmdUpdatePemakaian.Parameters.AddWithValue("@tgl", datepemakaian.Value.Date);
                    cmdUpdatePemakaian.ExecuteNonQuery();

                    MessageBox.Show("Data pemakaian berhasil diperbarui.");
                }
                else
                {
                    SqlCommand cmdPakai = new SqlCommand("INSERT INTO pemakaian_material (kodeBarang, namaBarang, tanggalPemakaian, jumlahPemakaian) VALUES (@kode, @nama, @tgl, @jumlah)", conn);
                    cmdPakai.Parameters.AddWithValue("@kode", kodeBarang);
                    cmdPakai.Parameters.AddWithValue("@nama", namaBarang);
                    cmdPakai.Parameters.AddWithValue("@tgl", datepemakaian.Value);
                    cmdPakai.Parameters.AddWithValue("@jumlah", jumlahPakai);
                    cmdPakai.ExecuteNonQuery();

                    MessageBox.Show("Data pemakaian berhasil ditambahkan.");
                }

                SqlCommand cmdUpdateStok = new SqlCommand("UPDATE stok_material SET jumlahStok = jumlahStok - @pakai WHERE kodeBarang = @kode", conn);
                cmdUpdateStok.Parameters.AddWithValue("@pakai", jumlahPakai);
                cmdUpdateStok.Parameters.AddWithValue("@kode", kodeBarang);
                cmdUpdateStok.ExecuteNonQuery();

                txtjumlah.Clear();
                tampil();
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

        private void guna2TextBox11_TextChanged(object sender, EventArgs e)
        {
            cari();
        }
    }
}
