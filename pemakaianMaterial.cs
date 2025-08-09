using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class pemakaianMaterial : Form
    {

        SqlConnection conn = Koneksi.GetConnection();

        public static pemakaianMaterial instance;

        bool infocari = false;
        int noprimary = 0;
        int jumlahlama;

        public pemakaianMaterial()
        {
            InitializeComponent();
            instance = this;
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
                string query = "SELECT * FROM pemakaian_material ORDER BY tanggalPemakaian DESC, updated_at DESC";
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
                dataGridView1.Columns[3].HeaderText = "Tanggal Pemakaian";
                dataGridView1.Columns[4].HeaderText = "Jumlah Pemakaian";
                dataGridView1.Columns[5].HeaderText = "Diubah";
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
            string kodeBarang = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(kodeBarang))
            {
                MessageBox.Show("Silakan isi Tanggal atau Kode Barang untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void editdata()
        {
            string kodeBarang = cmbnama.SelectedValue.ToString();
            int jumlah = int.Parse(txtjumlah.Text);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE stok_material SET jumlahStok = jumlahStok + @pakai WHERE kodeBarang = @kode", conn);
                cmd.Parameters.AddWithValue("@pakai", jumlahlama);
                cmd.Parameters.AddWithValue("@kode", kodeBarang);
                cmd.ExecuteNonQuery();

                SqlCommand cmdPakai = new SqlCommand("UPDATE pemakaian_material SET tanggalPemakaian = @tgl, jumlahPemakaian = @jumlah, updated_at = @diubah WHERE idPemakaian = @id", conn);
                cmdPakai.Parameters.AddWithValue("@id", noprimary);
                cmdPakai.Parameters.AddWithValue("@tgl", datepemakaian.Value);
                cmdPakai.Parameters.AddWithValue("@jumlah", jumlah);
                cmdPakai.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmdPakai.ExecuteNonQuery();

                SqlCommand cmdUpdateStok = new SqlCommand("UPDATE stok_material SET jumlahStok = jumlahStok - @pakai, updated_at = @diubah WHERE kodeBarang = @kode", conn);
                cmdUpdateStok.Parameters.AddWithValue("@pakai", txtjumlah.Text);
                cmdUpdateStok.Parameters.AddWithValue("@kode", kodeBarang);
                cmdUpdateStok.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmdUpdateStok.ExecuteNonQuery();


                MessageBox.Show("Data Berhasil Diedit", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtjumlah.Clear();
                tampil();
                btnsimpan.Text = "Simpan Data";
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
                conn.Close ();
            }
        }

        private void simpandata()
        {
            string kodeBarang = cmbnama.SelectedValue.ToString();
            string namaBarang = cmbnama.Text;
            if (!int.TryParse(txtjumlah.Text, out int jumlahPakai))
            {
                MessageBox.Show("Masukkan jumlah yang valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmdCek = new SqlCommand("SELECT jumlahStok FROM stok_material WHERE kodeBarang = @kode", conn);
                cmdCek.Parameters.AddWithValue("@kode", kodeBarang);
                int stokSekarang = Convert.ToInt32(cmdCek.ExecuteScalar());

                if (jumlahPakai > stokSekarang)
                {
                    MessageBox.Show("Stok tidak cukup.");
                    return;
                }
                SqlCommand cmdPakai = new SqlCommand("INSERT INTO pemakaian_material (kodeBarang, namaBarang, tanggalPemakaian, jumlahPemakaian, updated_at) VALUES (@kode, @nama, @tgl, @jumlah, @diubah)", conn);
                cmdPakai.Parameters.AddWithValue("@kode", kodeBarang);
                cmdPakai.Parameters.AddWithValue("@nama", namaBarang);
                cmdPakai.Parameters.AddWithValue("@tgl", datepemakaian.Value);
                cmdPakai.Parameters.AddWithValue("@jumlah", jumlahPakai);
                cmdPakai.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmdPakai.ExecuteNonQuery();

                SqlCommand cmdUpdateStok = new SqlCommand("UPDATE stok_material SET jumlahStok = jumlahStok - @pakai, updated_at = @diubah WHERE kodeBarang = @kode", conn);
                cmdUpdateStok.Parameters.AddWithValue("@pakai", jumlahPakai);
                cmdUpdateStok.Parameters.AddWithValue("@kode", kodeBarang);
                cmdUpdateStok.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmdUpdateStok.ExecuteNonQuery();

                MessageBox.Show("Data pemakaian berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtjumlah.Clear();
                tampil();
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
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT * FROM stok_material";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbnama.DataSource = dt;
                    cmbnama.DisplayMember = "namaBarang"; 
                    cmbnama.ValueMember = "kodeBarang";

                    cmbnama.SelectedIndexChanged -= cmbnama_SelectedIndexChanged;
                    cmbnama.SelectedIndex = -1;
                    cmbnama.SelectedIndexChanged += cmbnama_SelectedIndexChanged;
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
        }

        private void pemakaianMaterial_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            combonama();
            cmbnama.SelectedIndex = -1;
            tampil();
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            datepemakaian.Value = DateTime.Now.Date;
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
                    infocari = true;
                    btncari.Text = "Reset";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MainForm.Instance.role != "Manajer") return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["idPemakaian"].Value);
                cmbnama.SelectedValue = row.Cells["kodeBarang"].Value.ToString();
                datepemakaian.Value = Convert.ToDateTime(row.Cells["tanggalPemakaian"].Value);
                txtjumlah.Text = row.Cells["jumlahPemakaian"].Value.ToString();
                jumlahlama = Convert.ToInt32(row.Cells["jumlahPemakaian"].Value);

                btnsimpan.Text = "Edit Data";
                btnbatal.Enabled = true;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if(btnsimpan.Text == "Simpan Data")
            {
                if (txtjumlah.Text == "") 
                {
                    MessageBox.Show("Lengkapi data terlebih dahulu dengan benar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        simpandata();
                        combonama(); 
                        picture1.Image = null;
                        btnbatal.Enabled = false;
                        btnsimpan.Enabled = false;
                    }
                }
            }
            else
            {
                if (txtjumlah.Text == "")
                {
                    MessageBox.Show("Lengkapi data terlebih dahulu dengan benar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        editdata();
                        btnbatal.Enabled = false;
                        combonama(); 
                        picture1.Image = null;
                        btnbatal.Enabled = false;
                        btnsimpan.Enabled = false;
                    }
                }
            }
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            datepemakaian.Value = DateTime.Now.Date;
            txtjumlah.Clear();
            jumlahlama = 0;
            noprimary = 0;  
            btnsimpan.Text = "Simpan Data";
            combonama();
            picture1.Image = null;
            btnsimpan.Enabled = false;
            btnbatal.Enabled = false;
        }

        private void cmbnama_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbnama.SelectedValue == null || cmbnama.SelectedValue == DBNull.Value)
            {
                picture1.Image = null;
                return;
            }

            string kodeBarang = cmbnama.SelectedValue.ToString();

            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT foto FROM stok_material WHERE kodeBarang = @kodeBarang";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        byte[] imgData = (byte[])result;
                        using (MemoryStream ms = new MemoryStream(imgData))
                        {
                            picture1.Image = Image.FromStream(ms);
                            btnbatal.Enabled = true;
                            btnsimpan.Enabled = true;
                        }
                    }
                    else
                    {
                        picture1.Image = null; 
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
        }

        private void pemakaianMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }
    }
}
