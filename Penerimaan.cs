using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class Penerimaan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        int noprimary;

        public Penerimaan()
        {
            InitializeComponent();
            setdefault();
            tampil();
            btnsimpan.Enabled = false;
        }

        private void registertampil()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.penerimaan_s", conn))
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

        private void simpandata()
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Apakah Anda yakin dengan data Anda?",
                    "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
                );

                if (result != DialogResult.OK) return;

                conn.Open();

                string query = @"
                                SELECT 'penerimaan_s' AS sumber, nomor_rod 
                                FROM penerimaan_s 
                                WHERE nomor_rod = @rod
                                UNION
                                SELECT 'perbaikan_s' AS sumber, nomor_rod 
                                FROM perbaikan_s
                                WHERE nomor_rod = @rod";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@rod", txtnomorrod.Text);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string sumber = dr["sumber"].ToString();
                            string pesan = (sumber == "penerimaan_s")
                                ? "Nomor ROD ini sudah ada di data penerimaan dan belum diperbaiki."
                                : "Nomor ROD ini sudah ada di data perbaikan dan belum dikirim.";

                            MessageBox.Show(pesan, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand(@"
                                                    SELECT COUNT(*) 
                                                    FROM penerimaan_p
                                                    WHERE nomor_rod = @nomor_rod
                                                      AND @tgl BETWEEN tanggal_penerimaan AND DATEADD(DAY, 30, tanggal_penerimaan)", conn))
                {
                    cmd.Parameters.AddWithValue("@nomor_rod", txtnomorrod.Text);
                    cmd.Parameters.AddWithValue("@tgl", MainForm.Instance.tanggal);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        DialogResult result1 = MessageBox.Show(
                            $"Nomor ROD {txtnomorrod.Text} sudah ada dalam 30 hari dari penerimaan sebelumnya.\n" +
                            $"Apakah Anda Ingin Melanjutkan Simpan?",
                            "Peringatan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
                        );

                        if (result1 != DialogResult.OK) return;
                    }

                    SqlCommand cmd1 = new SqlCommand(@"
                                                    INSERT INTO penerimaan_s 
                                                    (tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, e1, e2, e3,
                                                     s, d, b, ba, cr, m, r, c, rl, jumlah, updated_at) 
                                                    VALUES
                                                    (@tanggal_penerimaan, @shift, @nomorrod, @jenis, @stasiun, @e1, @e2, @e3,
                                                     @s, @d, @b, @ba, @cr, @m, @r, @c, @rl, @jumlah, @diubah)", conn);

                    SqlCommand cmd2 = new SqlCommand(@"
                                                    INSERT INTO penerimaan_p 
                                                    (tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, e1, e2, e3,
                                                     s, d, b, ba, cr, m, r, c, rl, jumlah, updated_at) 
                                                    VALUES
                                                    (@tanggal_penerimaan, @shift, @nomorrod, @jenis, @stasiun, @e1, @e2, @e3,
                                                     @s, @d, @b, @ba, @cr, @m, @r, @c, @rl, @jumlah, @diubah)", conn);

                    foreach (var command in new[] { cmd1, cmd2 })
                    {
                        command.Parameters.AddWithValue("@tanggal_penerimaan", MainForm.Instance.tanggal);
                        command.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                        command.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        command.Parameters.AddWithValue("@jenis", txtjenis.Text);
                        command.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                        command.Parameters.AddWithValue("@e1", txte1.Text);
                        command.Parameters.AddWithValue("@e2", txte2.Text);
                        command.Parameters.AddWithValue("@e3", txte3.Text);
                        command.Parameters.AddWithValue("@s", txts.Text);
                        command.Parameters.AddWithValue("@d", txtd.Text);
                        command.Parameters.AddWithValue("@b", txtb.Text);
                        command.Parameters.AddWithValue("@ba", txtba.Text);
                        command.Parameters.AddWithValue("@cr", txtcr.Text);
                        command.Parameters.AddWithValue("@m", txtm.Text);
                        command.Parameters.AddWithValue("@r", txtr.Text);
                        command.Parameters.AddWithValue("@c", txtc.Text);
                        command.Parameters.AddWithValue("@rl", txtrl.Text);
                        command.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                        command.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    }

                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
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

        private void editdata()
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Apakah Anda yakin dengan data Anda?",
                    "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
                );

                if (result != DialogResult.OK) return;

                conn.Open();

                string query = @"
                                SELECT 'penerimaan_s' AS sumber, nomor_rod 
                                FROM penerimaan_s 
                                WHERE nomor_rod = @rod
                                UNION
                                SELECT 'perbaikan_s' AS sumber, nomor_rod 
                                FROM perbaikan_s
                                WHERE nomor_rod = @rod";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@rod", txtnomorrod.Text);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string sumber = dr["sumber"].ToString();
                            string pesan = (sumber == "penerimaan_s")
                                ? "Nomor ROD ini sudah ada di data penerimaan dan belum diperbaiki."
                                : "Nomor ROD ini sudah ada di data perbaikan dan belum dikirim.";

                            MessageBox.Show(pesan, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand(@"
                                                    SELECT COUNT(*) 
                                                    FROM penerimaan_p
                                                    WHERE nomor_rod = @nomor_rod
                                                      AND @tgl BETWEEN tanggal_penerimaan AND DATEADD(DAY, 30, tanggal_penerimaan)", conn))
                {
                    cmd.Parameters.AddWithValue("@nomor_rod", txtnomorrod.Text);
                    cmd.Parameters.AddWithValue("@tgl", MainForm.Instance.tanggal);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        DialogResult result1 = MessageBox.Show(
                            $"Nomor ROD {txtnomorrod.Text} sudah ada dalam 30 hari dari penerimaan sebelumnya.\n" +
                            $"Apakah Anda Ingin Melanjutkan Edit Data?",
                            "Peringatan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
                        );

                        if (result1 != DialogResult.OK) return;
                    }

                    SqlCommand cmd1 = new SqlCommand("UPDATE penerimaan_s SET nomor_rod = @nomorrod, jenis = @jenis, stasiun = @stasiun, e1 = @e1, e2 = @e2, e3 = @e3, s = @s, d = @d," +
                                    "b = @b, ba = @ba, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah, updated_at = @diubah WHERE no = @no ", conn);
                    SqlCommand cmd2 = new SqlCommand("UPDATE penerimaan_p SET nomor_rod = @nomorrod, jenis = @jenis, stasiun = @stasiun, e1 = @e1, e2 = @e2, e3 = @e3, s = @s, d = @d," +
                        "b = @b, ba = @ba, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah, updated_at = @diubah WHERE no = @no ", conn);

                    foreach (var command in new[] { cmd1, cmd2 })
                    {
                        command.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        command.Parameters.AddWithValue("@jenis", txtjenis.Text);
                        command.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                        command.Parameters.AddWithValue("@e1", txte1.Text);
                        command.Parameters.AddWithValue("@e2", txte2.Text);
                        command.Parameters.AddWithValue("@e3", txte3.Text);
                        command.Parameters.AddWithValue("@s", txts.Text);
                        command.Parameters.AddWithValue("@d", txtd.Text);
                        command.Parameters.AddWithValue("@b", txtb.Text);
                        command.Parameters.AddWithValue("@ba", txtba.Text);
                        command.Parameters.AddWithValue("@cr", txtcr.Text);
                        command.Parameters.AddWithValue("@m", txtm.Text);
                        command.Parameters.AddWithValue("@r", txtr.Text);
                        command.Parameters.AddWithValue("@c", txtc.Text);
                        command.Parameters.AddWithValue("@rl", txtrl.Text);
                        command.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                        command.Parameters.AddWithValue("@no", noprimary);
                        command.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    }

                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Diedit", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
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

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM penerimaan_s ORDER BY tanggal_penerimaan DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Jenis";
                dataGridView1.Columns[5].HeaderText = "Stasiun";
                dataGridView1.Columns[6].HeaderText = "E1";
                dataGridView1.Columns[7].HeaderText = "E2";
                dataGridView1.Columns[8].HeaderText = "E3";
                dataGridView1.Columns[9].HeaderText = "S";
                dataGridView1.Columns[10].HeaderText = "D";
                dataGridView1.Columns[11].HeaderText = "B";
                dataGridView1.Columns[12].HeaderText = "BA";
                dataGridView1.Columns[13].HeaderText = "CR";
                dataGridView1.Columns[14].HeaderText = "M";
                dataGridView1.Columns[15].HeaderText = "R";
                dataGridView1.Columns[16].HeaderText = "C";
                dataGridView1.Columns[17].HeaderText = "RL";
                dataGridView1.Columns[18].HeaderText = "Jumlah";
                dataGridView1.Columns[19].HeaderText = "Diubah";
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnsimpan.PerformClick();
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "" || txtstasiun.Text == "")
            {
                MessageBox.Show("Nomor ROD, Jenis dan Stasiun Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnsimpan.Text == "Edit Data")
            {
                editdata();
                setdefault();
                btnsimpan.Text = "Simpan Data";
                btncancel.Enabled = false;
                btnsimpan.Enabled = false;
                txtnomorrod.Focus();
            }
            else
            {
                simpandata();
                tampil();
                btnsimpan.Enabled = false;
                btncancel.Enabled= false;
                txtnomorrod.Focus();
            }
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void setdefault()
        {
            txtnomorrod.Clear();
            txtjenis.Clear();
            txtstasiun.Clear();
            txte1.Clear();
            txte2.Clear();
            txte3.Clear();
            txts.Clear();
            txtd.Clear();
            txtb.Clear();
            txtba.Clear();
            txtcr.Clear();
            txtm.Clear();
            txtr.Clear();
            txtc.Clear();
            txtrl.Clear();
            lbltotal.Text = "-";
        }

        private int SafeParse(Guna2TextBox tb)
        {
            return int.TryParse(tb.Text, out int result) ? result : 0;
        }

        private void hitung()
        {
            int angka1 = SafeParse(txte1);
            int angka2 = SafeParse(txte2);
            int angka3 = SafeParse(txte3);
            int angka4 = 0;
            int angka4a = SafeParse(txts);
            if(angka4a > 0)
            {
                angka4 = 1;
            }
            else
            {
                angka4 = 0;
            }
            int angka5 = SafeParse(txtd);
            int angka6 = SafeParse(txtb);
            int angka7 = SafeParse(txtba);
            int angka8 = 0;
            int angka8a = SafeParse(txtcr);
            if (angka8a > 0) 
            { 
                angka8 = 1;
            }
            else
            {
                angka8 = 0;
            }
            int angka9 = SafeParse(txtm);
            int angka10 = SafeParse(txtr);
            int angka11 = 0;
            int angka11a = SafeParse(txtc);
            if (angka11a > 0)
            {
                angka11 = 1;
            }
            else
            {
                angka11 = 0;
            }
            int angka12 = SafeParse(txtrl);

            int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12;
            lbltotal.Text = hasil.ToString();
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private bool cari()
        {
            string inputRod = txtcari.Text.Trim();
            DataTable dt = new DataTable();

            string query = "SELECT * FROM penerimaan_s WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (!string.IsNullOrEmpty(inputRod))
                {
                    query += " AND CAST(nomor_rod AS VARCHAR) LIKE @rod";
                    cmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");
                }

                query += " ORDER BY tanggal_penerimaan DESC";

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MainForm.Instance.role != "Manajer") return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["no"].Value);
                txtnomorrod.Text = row.Cells["nomor_rod"].Value.ToString();
                txtjenis.Text = row.Cells["jenis"].Value.ToString();
                txtstasiun.Text = row.Cells["stasiun"].Value.ToString();
                txte1.Text = row.Cells["e1"].Value.ToString();
                txte2.Text = row.Cells["e2"].Value.ToString();
                txte3.Text = row.Cells["e3"].Value.ToString();
                txts.Text = row.Cells["s"].Value.ToString();
                txtd.Text = row.Cells["d"].Value.ToString();
                txtb.Text = row.Cells["b"].Value.ToString();
                txtba.Text = row.Cells["ba"].Value.ToString();
                txtcr.Text = row.Cells["cr"].Value.ToString();
                txtm.Text = row.Cells["m"].Value.ToString();
                txtr.Text = row.Cells["r"].Value.ToString();
                txtc.Text = row.Cells["c"].Value.ToString();
                txtrl.Text = row.Cells["rl"].Value.ToString();
                lbltotal.Text = row.Cells["jumlah"].Value.ToString();
                btncancel.Enabled = true;
                btnsimpan.Text = "Edit Data";

            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            setdefault();
            btnsimpan.Text = "Simpan Data";
            btncancel.Enabled = false;
            btnsimpan.Enabled = false;
            dataGridView1.ClearSelection();
        }

        private void Penerimaan_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            txtnomorrod.Focus();
            registertampil();
        }

        private void txtrl_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte3_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txts_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtd_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtb_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtba_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtcr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtm_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtc_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte1_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtrl_TextChanged_1(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtnomorrod_TextChanged(object sender, EventArgs e)
        {
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void txtjenis_TextChanged(object sender, EventArgs e)
        {
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void txtstasiun_TextChanged(object sender, EventArgs e)
        {
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void Penerimaan_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            cari();
        }
    }
}
