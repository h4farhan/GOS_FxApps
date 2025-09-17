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
using Guna.UI2.WinForms;
using DrawingPoint = System.Drawing.Point;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;

namespace GOS_FxApps
{
    public partial class koefisiensi : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        int noprimarymaterial = 0;

        public koefisiensi()
        {
            InitializeComponent();
        }

        private void registertampilmaterial()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.koefisiensi_material", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampilmaterial();
                            registertampilmaterial();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }
        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox txt)
            {
                string text = txt.Text;

                string valid = "";
                bool komaSudahAda = false;

                foreach (char c in text)
                {
                    if (char.IsDigit(c))
                        valid += c;
                    else if (c == ',' && !komaSudahAda)
                    {
                        valid += c;
                        komaSudahAda = true;
                    }
                }

                if (txt.Text != valid)
                {
                    int pos = txt.SelectionStart;
                    txt.Text = valid;
                    txt.SelectionStart = Math.Min(pos, txt.Text.Length);
                }
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }
        public void combonama()
        {
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = @"
                SELECT *
                FROM stok_material
                WHERE type IN ('MATERIAL COST', 'CONSUMABLE COST', 'SAFETY PROTECTOR COST')
                ORDER BY namaBarang";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbmaterial.DataSource = dt;
                    cmbmaterial.DisplayMember = "namaBarang";
                    cmbmaterial.ValueMember = "kodeBarang";

                    cmbmaterial.SelectedIndexChanged -= cmbmaterial_SelectedIndexChanged;
                    cmbmaterial.SelectedIndex = -1;
                    cmbmaterial.SelectedIndexChanged += cmbmaterial_SelectedIndexChanged;
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

        private void setdefaultmaterial()
        {
            cmbmaterial.SelectedIndex = -1;
            txtspesifikasi.Clear();
            txtkodebarang.Clear();
            txtuom.Clear();
            txttipe.Clear();
            txtkoefe1.Clear();
            txtkoefe2.Clear();
            txtkoefe3.Clear();
            txtkoefe4.Clear();
            txtkoefs.Clear();
            txtkoefd.Clear();
            txtkoefb.Clear();
            txtkoefba.Clear();
            txtkoefba1.Clear();
            txtkoefcr.Clear();
            txtkoefm.Clear();
            txtkoefr.Clear();
            txtkoefc.Clear();
            txtkoefrl.Clear();
            noprimarymaterial = 0;
        }

        private void tampilmaterial()
        {
            try
            {
                string query = @"SELECT *
                         FROM koefisiensi_material s
                         ORDER BY 
                             CASE 
                                 WHEN s.kodeBarang LIKE 'MATR%' THEN 1
                                 WHEN s.kodeBarang LIKE 'CONS%' THEN 2
                                 WHEN s.kodeBarang LIKE 'APDS%' THEN 3
                                 WHEN s.kodeBarang LIKE 'MAIN%' THEN 4
                                 WHEN s.kodeBarang LIKE 'ATK%'  THEN 5
                                 WHEN s.kodeBarang LIKE 'LOGS%' THEN 6
                                 ELSE 7
                             END,
                             TRY_CAST(
                                 LEFT(
                                     SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)), 
                                     PATINDEX('%[^0-9]%', SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)) + 'X') - 1
                                 ) AS INT
                             ),
                             SUBSTRING(
                                 SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)), 
                                 PATINDEX('%[^0-9]%', SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)) + 'X'), 
                                 LEN(s.kodeBarang)
                             ),
                             s.kodeBarang;";

                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;

                // Styling
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Type";
                dataGridView1.Columns[2].HeaderText = "Kode Barang";
                dataGridView1.Columns[3].HeaderText = "Deskripsi";
                dataGridView1.Columns[4].HeaderText = "Spesifikasi";
                dataGridView1.Columns[5].HeaderText = "UoM";
                dataGridView1.Columns[6].HeaderText = "Koef E1";
                dataGridView1.Columns[7].HeaderText = "Koef E2";
                dataGridView1.Columns[8].HeaderText = "Koef E3";
                dataGridView1.Columns[9].HeaderText = "Koef E4";
                dataGridView1.Columns[10].HeaderText = "Koef S";
                dataGridView1.Columns[11].HeaderText = "Koef D";
                dataGridView1.Columns[12].HeaderText = "Koef B";
                dataGridView1.Columns[13].HeaderText = "Koef BA";
                dataGridView1.Columns[14].HeaderText = "Koef BA-1";
                dataGridView1.Columns[15].HeaderText = "Koef CR";
                dataGridView1.Columns[16].HeaderText = "Koef M";
                dataGridView1.Columns[17].HeaderText = "Koef R";
                dataGridView1.Columns[18].HeaderText = "Koef C";
                dataGridView1.Columns[19].HeaderText = "Koef RL";
                dataGridView1.Columns[20].HeaderText = "Diubah";
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


        private bool carimaterial()
        {
            string keyword = txtcari.Text.Trim();

            DataTable dt = new DataTable();

            string query = @"SELECT *
                     FROM koefisiensi_material
                     WHERE kodeBarang LIKE @keyword
                     ORDER BY updated_at DESC;";

            using (SqlConnection conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.DataSource = dt;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.\n" + ex.Message,
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                    "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt.Rows.Count > 0;
        }

        private void simpandatamaterial()
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?",
                                                      "Konfirmasi",
                                                      MessageBoxButtons.OKCancel,
                                                      MessageBoxIcon.Warning);

                if (result != DialogResult.OK)
                    return;

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();

                    using (SqlCommand cmdcekkode = new SqlCommand("SELECT 1 FROM koefisiensi_material WHERE kodeBarang = @kode", conn))
                    {
                        cmdcekkode.Parameters.AddWithValue("@kode", cmbmaterial.SelectedValue.ToString());
                        object exists = cmdcekkode.ExecuteScalar();
                        if (exists != null)
                        {
                            MessageBox.Show("Kode Barang ini sudah ada!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (SqlCommand cmd1 = new SqlCommand(@"
                    INSERT INTO koefisiensi_material 
                    (type,kodeBarang,deskripsi,spesifikasi,uom,
                     koef_e1,koef_e2,koef_e3,koef_e4,
                     koef_s,koef_d,koef_b,koef_ba,koef_ba1,
                     koef_cr,koef_m,koef_r,koef_c,koef_rl,
                     updated_at) 
                    VALUES
                    (@type,@kodeBarang,@deskripsi,@spesifikasi,@uom,
                     @e1,@e2,@e3,@e4,
                     @s,@d,@b,@ba,@ba1,
                     @cr,@m,@r,@c,@rl,
                     @diubah)", conn))
                    {
                        cmd1.Parameters.AddWithValue("@type", txttipe.Text);
                        cmd1.Parameters.AddWithValue("@kodeBarang", cmbmaterial.SelectedValue.ToString());
                        cmd1.Parameters.AddWithValue("@deskripsi", cmbmaterial.Text);
                        cmd1.Parameters.AddWithValue("@spesifikasi", txtspesifikasi.Text);
                        cmd1.Parameters.AddWithValue("@uom", txtuom.Text);

                        cmd1.Parameters.AddWithValue("@e1", string.IsNullOrWhiteSpace(txtkoefe1.Text) ? 0 : Convert.ToDecimal(txtkoefe1.Text));
                        cmd1.Parameters.AddWithValue("@e2", string.IsNullOrWhiteSpace(txtkoefe2.Text) ? 0 : Convert.ToDecimal(txtkoefe2.Text));
                        cmd1.Parameters.AddWithValue("@e3", string.IsNullOrWhiteSpace(txtkoefe3.Text) ? 0 : Convert.ToDecimal(txtkoefe3.Text));
                        cmd1.Parameters.AddWithValue("@e4", string.IsNullOrWhiteSpace(txtkoefe4.Text) ? 0 : Convert.ToDecimal(txtkoefe4.Text));
                        cmd1.Parameters.AddWithValue("@s", string.IsNullOrWhiteSpace(txtkoefs.Text) ? 0 : Convert.ToDecimal(txtkoefs.Text));
                        cmd1.Parameters.AddWithValue("@d", string.IsNullOrWhiteSpace(txtkoefd.Text) ? 0 : Convert.ToDecimal(txtkoefd.Text));
                        cmd1.Parameters.AddWithValue("@b", string.IsNullOrWhiteSpace(txtkoefb.Text) ? 0 : Convert.ToDecimal(txtkoefb.Text));
                        cmd1.Parameters.AddWithValue("@ba", string.IsNullOrWhiteSpace(txtkoefba.Text) ? 0 : Convert.ToDecimal(txtkoefba.Text));
                        cmd1.Parameters.AddWithValue("@ba1", string.IsNullOrWhiteSpace(txtkoefba1.Text) ? 0 : Convert.ToDecimal(txtkoefba1.Text));
                        cmd1.Parameters.AddWithValue("@cr", string.IsNullOrWhiteSpace(txtkoefcr.Text) ? 0 : Convert.ToDecimal(txtkoefcr.Text));
                        cmd1.Parameters.AddWithValue("@m", string.IsNullOrWhiteSpace(txtkoefm.Text) ? 0 : Convert.ToDecimal(txtkoefm.Text));
                        cmd1.Parameters.AddWithValue("@r", string.IsNullOrWhiteSpace(txtkoefr.Text) ? 0 : Convert.ToDecimal(txtkoefr.Text));
                        cmd1.Parameters.AddWithValue("@c", string.IsNullOrWhiteSpace(txtkoefc.Text) ? 0 : Convert.ToDecimal(txtkoefc.Text));
                        cmd1.Parameters.AddWithValue("@rl", string.IsNullOrWhiteSpace(txtkoefrl.Text) ? 0 : Convert.ToDecimal(txtkoefrl.Text));

                        cmd1.Parameters.AddWithValue("@diubah", DateTime.Now);

                        cmd1.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data Koefisiensi Material Berhasil Disimpan",
                                "Sukses",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                tampilmaterial();
                txtspesifikasi.Enabled = true;
                txtuom.Enabled = true;
                txttipe.Enabled = true;
                txtkodebarang.Enabled = true;
                combonama();
                setdefaultmaterial();
                txtspesifikasi.Enabled = false;
                txtuom.Enabled = false;
                txttipe.Enabled = false;
                txtkodebarang.Enabled = false;
                btndelete.Enabled = false;
                btnsimpanmaterial.Enabled = false;
                cmbmaterial.Enabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.\n" + ex.Message,
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editmaterial()
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE koefisiensi_material SET koef_e1 = @e1, koef_e2 = @e2, koef_e3 = @e3, koef_e4 = @e4, koef_s = @s, koef_d = @d, koef_b = @b, koef_ba = @ba, koef_ba1 = @ba1, " +
                    "koef_cr = @cr, koef_m = @m, koef_r = @r, koef_c = @c, koef_rl = @rl, updated_at = @diubah WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", noprimarymaterial);
                    cmd.Parameters.AddWithValue("@e1", string.IsNullOrWhiteSpace(txtkoefe1.Text) ? 0 : Convert.ToDecimal(txtkoefe1.Text));
                    cmd.Parameters.AddWithValue("@e2", string.IsNullOrWhiteSpace(txtkoefe2.Text) ? 0 : Convert.ToDecimal(txtkoefe2.Text));
                    cmd.Parameters.AddWithValue("@e3", string.IsNullOrWhiteSpace(txtkoefe3.Text) ? 0 : Convert.ToDecimal(txtkoefe3.Text));
                    cmd.Parameters.AddWithValue("@e4", string.IsNullOrWhiteSpace(txtkoefe4.Text) ? 0 : Convert.ToDecimal(txtkoefe4.Text));
                    cmd.Parameters.AddWithValue("@s", string.IsNullOrWhiteSpace(txtkoefs.Text) ? 0 : Convert.ToDecimal(txtkoefs.Text));
                    cmd.Parameters.AddWithValue("@d", string.IsNullOrWhiteSpace(txtkoefd.Text) ? 0 : Convert.ToDecimal(txtkoefd.Text));
                    cmd.Parameters.AddWithValue("@b", string.IsNullOrWhiteSpace(txtkoefb.Text) ? 0 : Convert.ToDecimal(txtkoefb.Text));
                    cmd.Parameters.AddWithValue("@ba", string.IsNullOrWhiteSpace(txtkoefba.Text) ? 0 : Convert.ToDecimal(txtkoefba.Text));
                    cmd.Parameters.AddWithValue("@ba1", string.IsNullOrWhiteSpace(txtkoefba1.Text) ? 0 : Convert.ToDecimal(txtkoefba1.Text));
                    cmd.Parameters.AddWithValue("@cr", string.IsNullOrWhiteSpace(txtkoefcr.Text) ? 0 : Convert.ToDecimal(txtkoefcr.Text));
                    cmd.Parameters.AddWithValue("@m", string.IsNullOrWhiteSpace(txtkoefm.Text) ? 0 : Convert.ToDecimal(txtkoefm.Text));
                    cmd.Parameters.AddWithValue("@r", string.IsNullOrWhiteSpace(txtkoefr.Text) ? 0 : Convert.ToDecimal(txtkoefr.Text));
                    cmd.Parameters.AddWithValue("@c", string.IsNullOrWhiteSpace(txtkoefc.Text) ? 0 : Convert.ToDecimal(txtkoefc.Text));
                    cmd.Parameters.AddWithValue("@rl", string.IsNullOrWhiteSpace(txtkoefrl.Text) ? 0 : Convert.ToDecimal(txtkoefrl.Text));
                    cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Koefisiensi Material Berhasil Diedit", "Sukses.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilmaterial();

                    txtspesifikasi.Enabled = true;
                    txtuom.Enabled = true;
                    txttipe.Enabled = true;
                    txtkodebarang.Enabled = true;
                    combonama();
                    setdefaultmaterial();
                    txtspesifikasi.Enabled = false;
                    txtuom.Enabled = false;
                    txttipe.Enabled = false;
                    txtkodebarang.Enabled = false;
                    btnsimpanmaterial.Text = "Simpan Data";
                    btndelete.Text = "Batal";
                    btndelete.Enabled = false;
                    btnsimpanmaterial.Enabled = false;
                    btnbatalmaterial.Visible = false;
                    cmbmaterial.Enabled = true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif." + ex.Message,
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
        }

        private void hapusmaterial()
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin hapus data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM koefisiensi_material WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", noprimarymaterial);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Koefisiensi Material Berhasil Dihapus", "Sukses.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilmaterial();

                    txtspesifikasi.Enabled = true;
                    txtuom.Enabled = true;
                    txttipe.Enabled = true;
                    txtkodebarang.Enabled = true;
                    combonama();
                    setdefaultmaterial();
                    txtspesifikasi.Enabled = false;
                    txtuom.Enabled = false;
                    txttipe.Enabled = false;
                    txtkodebarang.Enabled = false;
                    btnsimpanmaterial.Text = "Simpan Data";
                    btndelete.Text = "Batal";
                    btndelete.Enabled = false;
                    btnsimpanmaterial.Enabled = false;
                    btnbatalmaterial.Visible = false;
                    cmbmaterial.Enabled = true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif." + ex.Message,
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
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnsimpanmaterial.PerformClick();
            }
        }

        private void btnsimpanmaterial_Click(object sender, EventArgs e)
        {
            if (cmbmaterial.SelectedIndex == -1 || txtspesifikasi.Text == "" || txtuom.Text == "" || txttipe.Text == "")
            {
                MessageBox.Show("Data Material Harus Diisi Dengan Lengkap.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(btnsimpanmaterial.Text == "Edit Data")
            {
                editmaterial();
            }
            else
            {
                simpandatamaterial();
            }             
        }

        private void btnbatalmaterial_Click(object sender, EventArgs e)
        {
            txtspesifikasi.Enabled = true;
            txtuom.Enabled = true;
            txttipe.Enabled = true;
            txtkodebarang.Enabled = true;
            combonama();
            setdefaultmaterial();
            txtspesifikasi.Enabled = false;
            txtuom.Enabled = false;
            txttipe.Enabled = false;
            txtkodebarang.Enabled = false;
            btnsimpanmaterial.Text = "Simpan Data";
            btndelete.Text = "Batal";
            btndelete.Enabled = false;
            btnsimpanmaterial.Enabled = false;
            btnbatalmaterial.Visible = false;
            cmbmaterial.Enabled = true;
        }

        private void txtkoefm_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefs_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe3_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe2_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe1_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefcr_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefb_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefba_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefba1_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefd_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefrl_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefc_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe4_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefr_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void cmbmaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbmaterial.SelectedIndex == -1 || cmbmaterial.SelectedValue == null || cmbmaterial.SelectedValue is DataRowView)
                return;

            string kodeBarang = cmbmaterial.SelectedValue.ToString();
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT kodeBarang, spesifikasi,uom,type FROM stok_material WHERE kodeBarang = @kodeBarang", conn))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtspesifikasi.Text = reader["spesifikasi"]?.ToString();
                            txtuom.Text = reader["uom"]?.ToString();
                            txttipe.Text = reader["type"]?.ToString();
                            txtkodebarang.Text = reader["kodeBarang"].ToString();
                            btndelete.Enabled = true;
                            btnsimpanmaterial.Enabled = true;
                        }
                        else
                        {
                            setdefaultmaterial();
                            btndelete.Enabled = false;
                            btnsimpanmaterial.Enabled = false;
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
            finally 
            { 
                conn.Close();
            }
        }

        private void koefisiensi_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            registertampilmaterial();
            tampilmaterial();
            combonama();
        }

        private void koefisiensi_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimarymaterial = Convert.ToInt32(row.Cells["no"].Value);
                cmbmaterial.SelectedValue = row.Cells["kodeBarang"].Value.ToString();
                txtkodebarang.Text = row.Cells["kodeBarang"].Value.ToString();
                txtspesifikasi.Text = row.Cells["spesifikasi"].Value.ToString();
                txtuom.Text = row.Cells["uom"].Value.ToString();
                txttipe.Text = row.Cells["type"].Value.ToString();
                txtkoefe1.Text = row.Cells["koef_e1"].Value.ToString();
                txtkoefe2.Text = row.Cells["koef_e2"].Value.ToString();
                txtkoefe3.Text = row.Cells["koef_e3"].Value.ToString();
                txtkoefe4.Text = row.Cells["koef_e4"].Value.ToString();
                txtkoefs.Text = row.Cells["koef_s"].Value.ToString();
                txtkoefd.Text = row.Cells["koef_d"].Value.ToString();
                txtkoefb.Text = row.Cells["koef_b"].Value.ToString();
                txtkoefba.Text = row.Cells["koef_ba"].Value.ToString();
                txtkoefba1.Text = row.Cells["koef_ba1"].Value.ToString();
                txtkoefcr.Text = row.Cells["koef_cr"].Value.ToString();
                txtkoefm.Text = row.Cells["koef_m"].Value.ToString();
                txtkoefr.Text = row.Cells["koef_r"].Value.ToString();
                txtkoefc.Text = row.Cells["koef_c"].Value.ToString();
                txtkoefrl.Text = row.Cells["koef_rl"].Value.ToString();

                cmbmaterial.Enabled = false;
                btnsimpanmaterial.Enabled = true;
                btnsimpanmaterial.Text = "Edit Data";
                btndelete.Enabled = true;
                btndelete.Text = "Hapus Data";
                btnbatalmaterial.Visible = true;
            }
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            carimaterial();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (btndelete.Text == "Hapus Data")
            {
                hapusmaterial();
            }
            else
            {
                txtspesifikasi.Enabled = true;
                txtuom.Enabled = true;
                txttipe.Enabled = true;
                txtkodebarang.Enabled = true;
                combonama();
                setdefaultmaterial();
                txtspesifikasi.Enabled = false;
                txtuom.Enabled = false;
                txttipe.Enabled = false;
                txtkodebarang.Enabled = false;
                btnbatalmaterial.Enabled = false;
                btnsimpanmaterial.Enabled = false;
                cmbmaterial.Enabled = true;
                btndelete.Enabled = false;
            }
        }
    }
}