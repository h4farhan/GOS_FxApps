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

        string type;

        private bool isBinding = false;
        bool isProgrammaticChange = false;

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
                string query = "SELECT idPemakaian, kodeBarang, namaBarang, spesifikasi, type, tanggalPemakaian, jumlahPemakaian, updated_at, remaks FROM pemakaian_material ORDER BY tanggalPemakaian DESC";
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

                query += " ORDER BY tanggalPemakaian DESC";

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

                SqlCommand cmdPakai = new SqlCommand("UPDATE pemakaian_material SET tanggalPemakaian = @tgl, jumlahPemakaian = @jumlah, updated_at = @diubah, remaks = @remaks WHERE idPemakaian = @id", conn);
                cmdPakai.Parameters.AddWithValue("@id", noprimary);
                cmdPakai.Parameters.AddWithValue("@tgl", datepemakaian.Value);
                cmdPakai.Parameters.AddWithValue("@jumlah", jumlah);
                cmdPakai.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmdPakai.Parameters.AddWithValue("@remaks", loginform.login.name);
                cmdPakai.ExecuteNonQuery();

                SqlCommand cmdUpdateStok = new SqlCommand("UPDATE stok_material SET jumlahStok = jumlahStok - @pakai, updated_at = @diubah WHERE kodeBarang = @kode", conn);
                cmdUpdateStok.Parameters.AddWithValue("@pakai", txtjumlah.Text);
                cmdUpdateStok.Parameters.AddWithValue("@kode", kodeBarang);
                cmdUpdateStok.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmdUpdateStok.ExecuteNonQuery();


                MessageBox.Show("Data Pemakaian Material Berhasil Diedit", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtjumlah.Clear();
                lblstoksaatini.Text = "Stok Saat Ini : -";
                tampil();
                btnsimpan.Text = "Simpan Data";
                cmbnama.Enabled = true;
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
                    MessageBox.Show("Stok Material tidak cukup.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtjumlah.Clear();
                    return;
                }
                SqlCommand cmdPakai = new SqlCommand("INSERT INTO pemakaian_material (kodeBarang, namaBarang, type, tanggalPemakaian, jumlahPemakaian, updated_at, remaks, spesifikasi) VALUES (@kode, @nama, @type, @tgl, @jumlah, @diubah, @remaks, @spesifikasi)", conn);
                cmdPakai.Parameters.AddWithValue("@kode", kodeBarang);
                cmdPakai.Parameters.AddWithValue("@nama", namaBarang);
                cmdPakai.Parameters.AddWithValue("@spesifikasi", txttipe.Text);
                cmdPakai.Parameters.AddWithValue("@type", type);
                cmdPakai.Parameters.AddWithValue("@tgl", datepemakaian.Value);
                cmdPakai.Parameters.AddWithValue("@jumlah", jumlahPakai);
                cmdPakai.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmdPakai.Parameters.AddWithValue("@remaks", loginform.login.name);
                cmdPakai.ExecuteNonQuery();

                SqlCommand cmdUpdateStok = new SqlCommand("UPDATE stok_material SET jumlahStok = jumlahStok - @pakai, updated_at = @diubah WHERE kodeBarang = @kode", conn);
                cmdUpdateStok.Parameters.AddWithValue("@pakai", jumlahPakai);
                cmdUpdateStok.Parameters.AddWithValue("@kode", kodeBarang);
                cmdUpdateStok.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmdUpdateStok.ExecuteNonQuery();

                MessageBox.Show("Data Pemakaian Material berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtjumlah.Clear();
                lblstoksaatini.Text = "Stok Saat Ini: -";
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

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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
            registertampil();
            cmbnama.DropDownStyle = ComboBoxStyle.DropDown;
            cmbnama.MaxDropDownItems = 10;
            cmbnama.DropDownHeight = 200;
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
                txttipe.Text = row.Cells["spesifikasi"].Value.ToString();
                type = row.Cells["type"].Value.ToString();
                jumlahlama = Convert.ToInt32(row.Cells["jumlahPemakaian"].Value);

                cmbnama.Enabled = false;
                btnsimpan.Text = "Edit Data";
                btnbatal.Enabled = true;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnsimpan.PerformClick();
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if(btnsimpan.Text == "Simpan Data")
            {
                if (txtjumlah.Text == "" || cmbnama.SelectedIndex == -1) 
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
                        lblstoksaatini.Text = "Stok Saat Ini: -";
                        txttipe.Enabled = true;
                        txttipe.Clear();
                        txttipe.Enabled = false;
                        picture1.Image = null;
                        btnbatal.Enabled = false;
                        btnsimpan.Enabled = false;
                        txtcarimaterial.Clear();
                        type = null;
                        
                    }
                }
            }
            else
            {
                if (txtjumlah.Text == "" || cmbnama.SelectedIndex == -1 )
                {
                    MessageBox.Show("Lengkapi data terlebih dahulu dengan benar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        editdata();
                        lblstoksaatini.Text = "Stok Saat Ini: -";
                        txttipe.Enabled = true;
                        txttipe.Clear();
                        txttipe.Enabled = false;
                        picture1.Image = null;
                        btnbatal.Enabled = false;
                        btnsimpan.Enabled = false;
                        cmbnama.Enabled = true;
                        txtcarimaterial.Enabled = true;
                        txtcarimaterial.Clear();
                        type = null;
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
            lblstoksaatini.Text = "Stok Saat Ini: -";
            txttipe.Enabled = true;
            txttipe.Clear();
            txttipe.Enabled = false;
            picture1.Image = null;
            btnsimpan.Enabled = false;
            btnbatal.Enabled = false;
            cmbnama.Enabled = true;
            txtcarimaterial.Enabled = true;
            txtcarimaterial.Clear();
            type = null ;
        }

        private void pemakaianMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void txtjumlah_TextChanged(object sender, EventArgs e)
        {
            btnbatal.Enabled = true;
            btnsimpan.Enabled = true;
        }

        public void combonama(string keyword = "")
        {
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT * FROM stok_material WHERE namaBarang LIKE @keyword ORDER BY namaBarang ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    isBinding = true;
                    cmbnama.DataSource = dt;
                    cmbnama.DisplayMember = "namaBarang";
                    cmbnama.ValueMember = "kodeBarang";
                    cmbnama.SelectedIndex = -1;
                    isBinding = false;

                    if (dt.Rows.Count > 0)
                    {
                        cmbnama.DroppedDown = true;
                        txtcarimaterial.Focus();
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
        private void txtcarimaterial_TextChanged_1(object sender, EventArgs e)
        {
            if (isProgrammaticChange)
                return; 

            string keyword = txtcarimaterial.Text.Trim();
            combonama(keyword);
            if (!string.IsNullOrEmpty(keyword))
            {
                cmbnama.DroppedDown = true;

                Cursor = Cursors.Default;

                txtcarimaterial.Focus();
                txtcarimaterial.SelectionStart = txtcarimaterial.Text.Length;
            }
            else
            {
                cmbnama.DroppedDown = false;
            }
        }
        private void cmbnama_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBinding) return;

            if (cmbnama.SelectedIndex == -1 || cmbnama.SelectedValue == null || cmbnama.SelectedValue == DBNull.Value)
            {
                picture1.Image = null;
                lblstoksaatini.Text = "Stok Saat Ini : -";
                return;
            }

            string kodeBarang = cmbnama.SelectedValue.ToString();

            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT foto, jumlahStok, type, namaBarang, spesifikasi FROM stok_material WHERE kodeBarang = @kodeBarang", conn))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblstoksaatini.Text = "Stok Saat Ini: " + reader["jumlahStok"]?.ToString();
                            txttipe.Text = reader["spesifikasi"]?.ToString();
                            type = reader["type"]?.ToString();

                            isProgrammaticChange = true;
                            txtcarimaterial.Text = reader["namaBarang"]?.ToString();
                            isProgrammaticChange = false;

                            if (reader["foto"] != DBNull.Value)
                            {
                                byte[] imgData = (byte[])reader["foto"];
                                using (MemoryStream ms = new MemoryStream(imgData))
                                {
                                    picture1.Image = Image.FromStream(ms);
                                }
                                btnbatal.Enabled = true;
                                btnsimpan.Enabled = true;
                            }
                            else
                            {
                                picture1.Image = null;
                                btnbatal.Enabled = true;
                                btnsimpan.Enabled = true;
                            }
                        }
                        else
                        {
                            lblstoksaatini.Text = "Stok Saat Ini : -";
                            picture1.Image = null;
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
        }

    }
}
