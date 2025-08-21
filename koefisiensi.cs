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

namespace GOS_FxApps
{
    public partial class koefisiensi : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        bool infocarimaterial = false;
        bool infocariquantity = false;

        int noprimarymaterial = 0;
        int noprimaryquantity = 0;

        public koefisiensi()
        {
            InitializeComponent();
            tampilmaterial();
            tampilqty();
            datematerial.Value = DateTime.Now;
            datequantity.Value = DateTime.Now;
            datecarimaterial.Value = DateTime.Now;
            datecariqty.Value = DateTime.Now;
            datecarimaterial.Checked = false;
            datecariqty.Checked = false;
            combonama();
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

        private void registertampilquantity()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.koefisiensi_quantity", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampilqty();
                            registertampilquantity();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
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
            finally 
            { 
                conn.Close();
            }
        }

        private void setdefaultmaterial()
        {
            datematerial.Value = DateTime.Now;
            cmbmaterial.SelectedIndex = -1;
            txtspesifikasi.Clear();
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

        private void setdefaultquantity()
        {
            datequantity.Value = DateTime.Now;
            txtqtye1.Clear();
            txtqtye2.Clear();
            txtqtye3.Clear();
            txtqtye4.Clear();
            txtqtys.Clear();
            txtqtyd.Clear();
            txtqtyb.Clear();
            txtqtyba.Clear();
            txtqtyba1.Clear();
            txtqtycr.Clear();
            txtqtym.Clear();
            txtqtyr.Clear();
            txtqtyc.Clear();
            txtqtyrl.Clear();
            noprimarymaterial = 0;
        }

        private void tampilqty()
        {
            try
            {
                string query = "SELECT * FROM koefisiensi_quantity ORDER BY tanggal DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView2.DataSource = dt;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView2.RowTemplate.Height = 35;
                dataGridView2.ReadOnly = true;

                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Tanggal";
                dataGridView2.Columns[2].HeaderText = "Qty E1";
                dataGridView2.Columns[3].HeaderText = "Qty E2";
                dataGridView2.Columns[4].HeaderText = "Qty E3";
                dataGridView2.Columns[5].HeaderText = "Qty E4";
                dataGridView2.Columns[6].HeaderText = "Qty S";
                dataGridView2.Columns[7].HeaderText = "Qty D";
                dataGridView2.Columns[8].HeaderText = "Qty B";
                dataGridView2.Columns[9].HeaderText = "Qty BA";
                dataGridView2.Columns[10].HeaderText = "Qty BA-1";
                dataGridView2.Columns[11].HeaderText = "Qty CR";
                dataGridView2.Columns[12].HeaderText = "Qty M";
                dataGridView2.Columns[13].HeaderText = "Qty R";
                dataGridView2.Columns[14].HeaderText = "Qty C";
                dataGridView2.Columns[15].HeaderText = "Qty RL";
                dataGridView2.Columns[16].HeaderText = "Diubah";

                dataGridView2.Columns[1].DefaultCellStyle.Format = "MM-yyyy";
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

        private void tampilmaterial()
        {
            try
            {
                string query = "SELECT * FROM koefisiensi_material ORDER BY tanggal DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal";
                dataGridView1.Columns[2].HeaderText = "Type";
                dataGridView1.Columns[3].HeaderText = "Kode Barang";
                dataGridView1.Columns[4].HeaderText = "Deskripsi";
                dataGridView1.Columns[5].HeaderText = "Spesifikasi";
                dataGridView1.Columns[6].HeaderText = "UoM";
                dataGridView1.Columns[7].HeaderText = "Koef E1";
                dataGridView1.Columns[8].HeaderText = "Koef E2";
                dataGridView1.Columns[9].HeaderText = "Koef E3";
                dataGridView1.Columns[10].HeaderText = "Koef E4";
                dataGridView1.Columns[11].HeaderText = "Koef S";
                dataGridView1.Columns[12].HeaderText = "Koef D";
                dataGridView1.Columns[13].HeaderText = "Koef B";
                dataGridView1.Columns[14].HeaderText = "Koef BA";
                dataGridView1.Columns[15].HeaderText = "Koef BA-1";
                dataGridView1.Columns[16].HeaderText = "Koef CR";
                dataGridView1.Columns[17].HeaderText = "Koef M";
                dataGridView1.Columns[18].HeaderText = "Koef R";
                dataGridView1.Columns[19].HeaderText = "Koef C";
                dataGridView1.Columns[20].HeaderText = "Koef RL";
                dataGridView1.Columns[21].HeaderText = "Diubah";

                dataGridView1.Columns[1].DefaultCellStyle.Format = "MM-yyyy";
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
            int bulan = datecarimaterial.Value.Month;
            int tahun = datecarimaterial.Value.Year;

            DataTable dt = new DataTable();

            string query = @"SELECT * 
                     FROM koefisiensi_material 
                     WHERE YEAR(tanggal) = @year 
                       AND MONTH(tanggal) = @bulan";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@year", tahun);
                cmd.Parameters.AddWithValue("@bulan", bulan);

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

        private bool cariquantity()
        {
            int bulan = datecariqty.Value.Month;
            int tahun = datecariqty.Value.Year;

            DataTable dt = new DataTable();

            string query = @"SELECT * 
                     FROM koefisiensi_quantity
                     WHERE YEAR(tanggal) = @year 
                       AND MONTH(tanggal) = @bulan";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@year", tahun);
                cmd.Parameters.AddWithValue("@bulan", bulan);

                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView2.DataSource = dt;
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
                    using (SqlCommand cmdcekkode = new SqlCommand(
                        "SELECT 1 FROM koefisiensi_material WHERE MONTH(tanggal) = @bulan AND YEAR(tanggal) = @tahun", conn))
                    {
                        cmdcekkode.Parameters.AddWithValue("@bulan", datematerial.Value.Month);
                        cmdcekkode.Parameters.AddWithValue("@tahun", datematerial.Value.Year);

                        using (SqlDataReader dr = cmdcekkode.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                MessageBox.Show("Data Koefisiensi Material di bulan dan tahun ini sudah ada",
                                                "Peringatan",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd1 = new SqlCommand(@"
                INSERT INTO koefisiensi_material 
                (tanggal,type,kodeBarang,deskripsi,spesifikasi,uom,
                 koef_e1,koef_e2,koef_e3,koef_e4,
                 koef_s,koef_d,koef_b,koef_ba,koef_ba1,
                 koef_cr,koef_m,koef_r,koef_c,koef_rl,
                 updated_at) 
                VALUES
                (@tgl,@type,@kodeBarang,@deskripsi,@spesifikasi,@uom,
                 @e1,@e2,@e3,@e4,
                 @s,@d,@b,@ba,@ba1,
                 @cr,@m,@r,@c,@rl,
                 @diubah)", conn))
                    {
                        cmd1.Parameters.AddWithValue("@tgl", datematerial.Value.Date);
                        cmd1.Parameters.AddWithValue("@type", txttipe.Text);
                        cmd1.Parameters.AddWithValue("@kodeBarang", cmbmaterial.SelectedValue.ToString());
                        cmd1.Parameters.AddWithValue("@deskripsi", cmbmaterial.Text);
                        cmd1.Parameters.AddWithValue("@spesifikasi", txtspesifikasi.Text);
                        cmd1.Parameters.AddWithValue("@uom", txtuom.Text);
                        cmd1.Parameters.AddWithValue("@e1", txtkoefe1.Text);
                        cmd1.Parameters.AddWithValue("@e2", txtkoefe2.Text);
                        cmd1.Parameters.AddWithValue("@e3", txtkoefe3.Text);
                        cmd1.Parameters.AddWithValue("@e4", txtkoefe4.Text);
                        cmd1.Parameters.AddWithValue("@s", txtkoefs.Text);
                        cmd1.Parameters.AddWithValue("@d", txtkoefd.Text);
                        cmd1.Parameters.AddWithValue("@b", txtkoefb.Text);
                        cmd1.Parameters.AddWithValue("@ba", txtkoefba.Text);
                        cmd1.Parameters.AddWithValue("@ba1", txtkoefba1.Text);
                        cmd1.Parameters.AddWithValue("@cr", txtkoefcr.Text);
                        cmd1.Parameters.AddWithValue("@m", txtkoefm.Text);
                        cmd1.Parameters.AddWithValue("@r", txtkoefr.Text);
                        cmd1.Parameters.AddWithValue("@c", txtkoefc.Text);
                        cmd1.Parameters.AddWithValue("@rl", txtkoefrl.Text);

                        cmd1.Parameters.AddWithValue("@diubah", DateTime.Now);

                        cmd1.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data Koefisiensi Material Berhasil Disimpan",
                                "Sukses",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                tampilmaterial();
                setdefaultmaterial();
                btnbatalmaterial.Enabled = false;
                btnsimpanmaterial.Enabled = false;
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

        private void simpandataquantity()
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Apakah Anda yakin dengan data Anda?",
                    "Konfirmasi",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (result != DialogResult.OK)
                    return;

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmdcekkode = new SqlCommand(@"
                SELECT 1 
                FROM koefisiensi_quantity 
                WHERE MONTH(tanggal) = @bulan AND YEAR(tanggal) = @tahun", conn))
                    {
                        cmdcekkode.Parameters.AddWithValue("@bulan", datequantity.Value.Month);
                        cmdcekkode.Parameters.AddWithValue("@tahun", datequantity.Value.Year);

                        object exists = cmdcekkode.ExecuteScalar();
                        if (exists != null)
                        {
                            MessageBox.Show("Data Koefisiensi Quantity di bulan dan tahun ini sudah ada",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd1 = new SqlCommand(@"
                INSERT INTO koefisiensi_quantity 
                (tanggal, qty_e1, qty_e2, qty_e3, qty_e4, qty_s, qty_d, qty_b, qty_ba, 
                 qty_ba1, qty_cr, qty_m, qty_r, qty_c, qty_rl, updated_at) 
                VALUES 
                (@tgl, @e1, @e2, @e3, @e4, @s, @d, @b, @ba, 
                 @ba1, @cr, @m, @r, @c, @rl, @diubah)", conn))
                    {
                        cmd1.Parameters.AddWithValue("@tgl", datequantity.Value.Date);
                        cmd1.Parameters.AddWithValue("@e1", txtqtye1.Text);
                        cmd1.Parameters.AddWithValue("@e2", txtqtye2.Text);
                        cmd1.Parameters.AddWithValue("@e3", txtqtye3.Text);
                        cmd1.Parameters.AddWithValue("@e4", txtqtye4.Text);
                        cmd1.Parameters.AddWithValue("@s", txtqtys.Text);
                        cmd1.Parameters.AddWithValue("@d", txtqtyd.Text);
                        cmd1.Parameters.AddWithValue("@b", txtqtyb.Text);
                        cmd1.Parameters.AddWithValue("@ba", txtqtyba.Text);
                        cmd1.Parameters.AddWithValue("@ba1", txtqtyba1.Text);
                        cmd1.Parameters.AddWithValue("@cr", txtqtycr.Text);
                        cmd1.Parameters.AddWithValue("@m", txtqtym.Text);
                        cmd1.Parameters.AddWithValue("@r", txtqtyr.Text);
                        cmd1.Parameters.AddWithValue("@c", txtqtyc.Text);
                        cmd1.Parameters.AddWithValue("@rl", txtqtyrl.Text);
                        cmd1.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);

                        cmd1.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data Koefisiensi Quantity Berhasil Disimpan", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                tampilqty();
                setdefaultquantity();
                btnsimpanquantity.Enabled = false;
                btnbatalquantity.Enabled = false;
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
                        cmd.Parameters.AddWithValue("@e1", txtkoefe1.Text);
                        cmd.Parameters.AddWithValue("@e2", txtkoefe2.Text);
                        cmd.Parameters.AddWithValue("@e3", txtkoefe3.Text);
                        cmd.Parameters.AddWithValue("@e4", txtkoefe4.Text);
                        cmd.Parameters.AddWithValue("@s", txtkoefs.Text);
                        cmd.Parameters.AddWithValue("@d", txtkoefd.Text);
                        cmd.Parameters.AddWithValue("@b", txtkoefb.Text);
                        cmd.Parameters.AddWithValue("@ba", txtkoefba.Text);
                        cmd.Parameters.AddWithValue("@ba1", txtkoefba1.Text);
                        cmd.Parameters.AddWithValue("@cr", txtkoefcr.Text);
                        cmd.Parameters.AddWithValue("@m", txtkoefm.Text);
                        cmd.Parameters.AddWithValue("@r", txtkoefr.Text);
                        cmd.Parameters.AddWithValue("@c", txtkoefc.Text);
                        cmd.Parameters.AddWithValue("@rl", txtkoefrl.Text);
                        cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Koefisiensi Material Berhasil Diedit", "Sukses.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        setdefaultmaterial();
                        tampilmaterial();
                        btnsimpanmaterial.Enabled = false;
                        btnbatalmaterial.Enabled = false;
                        cmbmaterial.Enabled = true;
                        datematerial.Enabled = true;
                        btnsimpanmaterial.Text = "Simpan Data";
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
        }

        private void editquantity()
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE koefisiensi_quantity SET qty_e1 = @e1, qty_e2 = @e2, qty_e3 = @e3, qty_e4 = @e4, qty_s = @s, qty_d = @d, qty_b = @b, qty_ba = @ba, qty_ba1 = @ba1, " +
                    "qty_cr = @cr, qty_m = @m, qty_r = @r, qty_c = @c, qty_rl = @rl, updated_at = @diubah WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", noprimaryquantity);
                    cmd.Parameters.AddWithValue("@e1", txtqtye1.Text);
                    cmd.Parameters.AddWithValue("@e2", txtqtye2.Text);
                    cmd.Parameters.AddWithValue("@e3", txtqtye3.Text);
                    cmd.Parameters.AddWithValue("@e4", txtqtye4.Text);
                    cmd.Parameters.AddWithValue("@s", txtqtys.Text);
                    cmd.Parameters.AddWithValue("@d", txtqtyd.Text);
                    cmd.Parameters.AddWithValue("@b", txtqtyb.Text);
                    cmd.Parameters.AddWithValue("@ba", txtqtyba.Text);
                    cmd.Parameters.AddWithValue("@ba1", txtqtyba1.Text);
                    cmd.Parameters.AddWithValue("@cr", txtqtycr.Text);
                    cmd.Parameters.AddWithValue("@m", txtqtym.Text);
                    cmd.Parameters.AddWithValue("@r", txtqtyr.Text);
                    cmd.Parameters.AddWithValue("@c", txtqtyc.Text);
                    cmd.Parameters.AddWithValue("@rl", txtqtyrl.Text);
                    cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Koefisiensi Quantity Berhasil Diedit", "Sukses.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefaultquantity();
                    tampilqty();
                    btnsimpanquantity.Enabled = false;
                    btnbatalquantity.Enabled = false;
                    datequantity.Enabled = true;
                    btnsimpanquantity.Text = "Simpan Data";
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
        }

        private void btnsimpanquantity_Click(object sender, EventArgs e)
        {
            if (btnsimpanquantity.Text == "Edit Data")
            {
                editquantity();
            }
            else
            {
                simpandataquantity();
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

        private void btnbatalquantity_Click(object sender, EventArgs e)
        {
            setdefaultquantity();
            btnbatalquantity.Enabled = false;
            btnsimpanquantity.Enabled = false;
        }

        private void btnbatalmaterial_Click(object sender, EventArgs e)
        {
            txtspesifikasi.Enabled = true;
            txtuom.Enabled = true;
            txttipe.Enabled = true;
            combonama();
            setdefaultmaterial();
            txtspesifikasi.Enabled = false;
            txtuom.Enabled = false;
            txttipe.Enabled = false;
            btnbatalmaterial.Enabled = false;
            btnsimpanmaterial.Enabled = false;
        }

        private void txtqtym_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtys_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtye3_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtye2_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtye1_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtycr_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtyb_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtyba_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtyba1_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtyd_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtyrl_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtyc_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtye4_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtqtyr_TextChanged(object sender, EventArgs e)
        {
            btnbatalquantity.Enabled = true;
            btnsimpanquantity.Enabled = true;
        }

        private void txtkoefm_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefs_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe3_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe2_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe1_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefcr_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefb_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefba_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefba1_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefd_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefrl_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefc_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe4_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefr_TextChanged(object sender, EventArgs e)
        {
            btnbatalmaterial.Enabled = true;
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
                    "SELECT spesifikasi,uom,type FROM stok_material WHERE kodeBarang = @kodeBarang", conn))
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
                            btnbatalmaterial.Enabled = true;
                            btnsimpanmaterial.Enabled = true;
                        }
                        else
                        {
                            setdefaultmaterial();
                            btnbatalmaterial.Enabled = false;
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

        private void datequantity_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 200);
                pickerForm.Text = "Pilih Bulan & Tahun";

                var screenPos = datequantity.PointToScreen(DrawingPoint.Empty);
                pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datequantity.Height);

                var cmbBulan = new Guna2ComboBox
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 10,
                    Width = 200,
                    BorderRadius = 6,
                    ForeColor = Color.Black,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };
                string[] bulan = {
                                    "01 - Januari", "02 - Februari", "03 - Maret", "04 - April", "05 - Mei", "06 - Juni",
                                    "07 - Juli", "08 - Agustus", "09 - September", "10 - Oktober", "11 - November", "12 - Desember"
                                };
                cmbBulan.Items.AddRange(bulan);
                cmbBulan.SelectedIndex = datequantity.Value.Month - 1;

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 55,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = datequantity.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 110,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    datequantity.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(cmbBulan);
                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private void btncarimaterial_Click(object sender, EventArgs e)
        {
            if (!infocarimaterial)
            {
                bool hasilCari = carimaterial();
                if (hasilCari)
                {
                    infocarimaterial = true;
                    btncarimaterial.Text = "Reset";
                }
                else
                {
                    infocarimaterial = true;
                    btncarimaterial.Text = "Reset";
                }
            }
            else
            {
                tampilmaterial();
                infocarimaterial = false;
                btncarimaterial.Text = "Cari";

                //txtcari.Text = "";
                datecarimaterial.Checked = false;
            }
        }

        private void btncariquantity_Click(object sender, EventArgs e)
        {
            if (!infocariquantity)
            {
                bool hasilCari = cariquantity();
                if (hasilCari)
                {
                    infocariquantity = true;
                    btncariquantity.Text = "Reset";
                }
                else
                {
                    infocariquantity = true;
                    btncariquantity.Text = "Reset";
                }
            }
            else
            {
                tampilqty();
                infocariquantity = false;
                btncariquantity.Text = "Cari";

                //txtcari.Text = "";
                datecariqty.Checked = false;
            }
        }

        private void datematerial_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 200);
                pickerForm.Text = "Pilih Bulan & Tahun";

                var screenPos = datematerial.PointToScreen(DrawingPoint.Empty);
                pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datematerial.Height);

                var cmbBulan = new Guna2ComboBox
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 10,
                    Width = 200,
                    BorderRadius = 6,
                    ForeColor = Color.Black,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };
                string[] bulan = {
                                    "01 - Januari", "02 - Februari", "03 - Maret", "04 - April", "05 - Mei", "06 - Juni",
                                    "07 - Juli", "08 - Agustus", "09 - September", "10 - Oktober", "11 - November", "12 - Desember"
                                };
                cmbBulan.Items.AddRange(bulan);
                cmbBulan.SelectedIndex = datematerial.Value.Month - 1;

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 55,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = datematerial.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 110,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    datematerial.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(cmbBulan);
                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private void datecariqty_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 200);
                pickerForm.Text = "Pilih Bulan & Tahun";

                var screenPos = datecariqty.PointToScreen(DrawingPoint.Empty);
                pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datecariqty.Height);

                var cmbBulan = new Guna2ComboBox
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 10,
                    Width = 200,
                    BorderRadius = 6,
                    ForeColor = Color.Black,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };
                string[] bulan = {
                                    "01 - Januari", "02 - Februari", "03 - Maret", "04 - April", "05 - Mei", "06 - Juni",
                                    "07 - Juli", "08 - Agustus", "09 - September", "10 - Oktober", "11 - November", "12 - Desember"
                                };
                cmbBulan.Items.AddRange(bulan);
                cmbBulan.SelectedIndex = datecariqty.Value.Month - 1;

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 55,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = datecariqty.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 110,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    datecariqty.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(cmbBulan);
                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private void datecarimaterial_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 200);
                pickerForm.Text = "Pilih Bulan & Tahun";

                var screenPos = datecarimaterial.PointToScreen(DrawingPoint.Empty);
                pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datecarimaterial.Height);

                var cmbBulan = new Guna2ComboBox
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 10,
                    Width = 200,
                    BorderRadius = 6,
                    ForeColor = Color.Black,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };
                string[] bulan = {
                                    "01 - Januari", "02 - Februari", "03 - Maret", "04 - April", "05 - Mei", "06 - Juni",
                                    "07 - Juli", "08 - Agustus", "09 - September", "10 - Oktober", "11 - November", "12 - Desember"
                                };
                cmbBulan.Items.AddRange(bulan);
                cmbBulan.SelectedIndex = datecarimaterial.Value.Month - 1;

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 55,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = datecarimaterial.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 110,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    datecarimaterial.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(cmbBulan);
                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private void koefisiensi_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            registertampilmaterial();
            registertampilquantity();
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
                datematerial.Value = Convert.ToDateTime(row.Cells["tanggal"].Value);
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

                datematerial.Enabled = false;
                cmbmaterial.Enabled = false;
                btnsimpanmaterial.Enabled = true;
                btnsimpanmaterial.Text = "Edit Data";
                btnbatalmaterial.Enabled = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                noprimaryquantity = Convert.ToInt32(row.Cells["no"].Value);                
                datequantity.Value = Convert.ToDateTime(row.Cells["tanggal"].Value);
                txtqtye1.Text = row.Cells["qty_e1"].Value.ToString();
                txtqtye2.Text = row.Cells["qty_e2"].Value.ToString();
                txtqtye3.Text = row.Cells["qty_e3"].Value.ToString();
                txtqtye4.Text = row.Cells["qty_e4"].Value.ToString();
                txtqtys.Text = row.Cells["qty_s"].Value.ToString();
                txtqtyd.Text = row.Cells["qty_d"].Value.ToString();
                txtqtyb.Text = row.Cells["qty_b"].Value.ToString();
                txtqtyba.Text = row.Cells["qty_ba"].Value.ToString();
                txtqtyba1.Text = row.Cells["qty_ba1"].Value.ToString();
                txtqtycr.Text = row.Cells["qty_cr"].Value.ToString();
                txtqtym.Text = row.Cells["qty_m"].Value.ToString();
                txtqtyr.Text = row.Cells["qty_r"].Value.ToString();
                txtqtyc.Text = row.Cells["qty_c"].Value.ToString();
                txtqtyrl.Text = row.Cells["qty_rl"].Value.ToString();

                datequantity.Enabled = false;
                btnsimpanquantity.Enabled = true;
                btnsimpanquantity.Text = "Edit Data";
                btnbatalquantity.Enabled = true;
            }
        }
    }
}