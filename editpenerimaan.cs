using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Guna.UI2.WinForms;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class editpenerimaan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();
        
        bool infocari = false;

        string tanggalpenerimaan;
        int shift;
        int noprimary;

        public editpenerimaan()
        {
            InitializeComponent();
        }

        private void registertampil()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.penerimaan_p", conn))
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
                string query = "SELECT * FROM penerimaan_p ORDER BY tanggal_penerimaan DESC";
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

        private bool cari()
        {
            DateTime? tanggal = dateeditpenerimaan.Checked ? (DateTime?)dateeditpenerimaan.Value.Date : null;
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.", "Peringatan");
                return false; 
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM penerimaan_p WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                     query += "AND CAST(tanggal_penerimaan AS DATE) = @tgl";
                     cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(inputRod))
                {
                     query += " AND nomor_rod = @rod";
                     cmd.Parameters.AddWithValue("@rod", Convert.ToInt32(inputRod));
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

        private void setdefault()
        {
            txtnomorrod.Enabled = true;
            txtnomorrod.Clear();
            txtnomorrod.Enabled = false;
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
            lbltotalsebelum.Text = "-";
            lbltotalupdate.Text = "-";
        }

        private void settrue()
        {
            txtnomorrod.Enabled = true;
            txtjenis.Enabled = true;
            txtstasiun.Enabled = true;
            txte1.Enabled = true;
            txte2.Enabled = true;
            txte3.Enabled = true;
            txts.Enabled = true;
            txtd.Enabled = true;
            txtb.Enabled = true;
            txtba.Enabled = true;
            txtcr.Enabled = true;
            txtm.Enabled = true;
            txtr.Enabled = true;
            txtc.Enabled = true;
            txtrl.Enabled = true;
        }

        private void setfalse()
        {
            txtjenis.Enabled = false;
            txtstasiun.Enabled = false;
            txte1.Enabled = false;
            txte2.Enabled = false;
            txte3.Enabled = false;
            txts.Enabled = false;
            txtd.Enabled = false;
            txtb.Enabled = false;
            txtba.Enabled = false;
            txtcr.Enabled = false;
            txtm.Enabled = false;
            txtr.Enabled = false;
            txtc.Enabled = false;
            txtrl.Enabled = false;
        }

        private void editpenerimaan_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            tampil();
            dateeditpenerimaan.Value = DateTime.Now.Date;
            dateeditpenerimaan.Checked = false;
            registertampil();
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
                dateeditpenerimaan.Checked = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["no"].Value);
                tanggalpenerimaan = row.Cells["tanggal_penerimaan"].ToString();
                shift = Convert.ToInt32(row.Cells["shift"].Value);
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
                lbltotalsebelum.Text = row.Cells["jumlah"].Value.ToString();
                settrue();
                btnclear.Enabled = true;
                txtjenis.Focus();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            setdefault();
            btnclear.Enabled = false;
            btnupdate.Enabled = false;
            setfalse();
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
            int angka4 = SafeParse(txts);
            int angka5 = SafeParse(txtd);
            int angka6 = SafeParse(txtb);
            int angka7 = SafeParse(txtba);
            int angka8 = SafeParse(txtcr);
            int angka9 = SafeParse(txtm);
            int angka10 = SafeParse(txtr);
            int angka11 = SafeParse(txtc);
            int angka12 = SafeParse(txtrl);

            int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12;
            lbltotalupdate.Text = hasil.ToString();
            btnupdate.Enabled = true;
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnupdate.PerformClick();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "" || txtstasiun.Text == "")
            {
                MessageBox.Show("Nomor ROD, Jenis, dan Stasiun Tidak Boleh Kosong",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlTransaction trans = null;

            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?",
                                                      "Konfirmasi",
                                                      MessageBoxButtons.OKCancel,
                                                      MessageBoxIcon.Warning);

                if (result != DialogResult.OK) return;

                conn.Open();
                trans = conn.BeginTransaction();

                SqlCommand cmd1 = new SqlCommand(@"
            UPDATE penerimaan_p 
            SET nomor_rod = @nomorrod, jenis=@jenis, stasiun=@stasiun, e1=@e1, e2=@e2, e3=@e3, s=@s, d=@d, 
                b=@b, ba=@ba, cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah,
                remaks=@remaks, updated_at=@diubah
            WHERE no=@no", conn, trans);

                cmd1.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                cmd1.Parameters.AddWithValue("@jenis", txtjenis.Text);
                cmd1.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                cmd1.Parameters.AddWithValue("@e1", txte1.Text);
                cmd1.Parameters.AddWithValue("@e2", txte2.Text);
                cmd1.Parameters.AddWithValue("@e3", txte3.Text);
                cmd1.Parameters.AddWithValue("@s", txts.Text);
                cmd1.Parameters.AddWithValue("@d", txtd.Text);
                cmd1.Parameters.AddWithValue("@b", txtb.Text);
                cmd1.Parameters.AddWithValue("@ba", txtba.Text);
                cmd1.Parameters.AddWithValue("@cr", txtcr.Text);
                cmd1.Parameters.AddWithValue("@m", txtm.Text);
                cmd1.Parameters.AddWithValue("@r", txtr.Text);
                cmd1.Parameters.AddWithValue("@c", txtc.Text);
                cmd1.Parameters.AddWithValue("@rl", txtrl.Text);
                cmd1.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                cmd1.Parameters.AddWithValue("@remaks", loginform.login.name);
                cmd1.Parameters.AddWithValue("@no", noprimary);
                cmd1.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmd1.ExecuteNonQuery();

                SqlCommand log1 = new SqlCommand("INSERT INTO penerimaan_e SELECT * FROM penerimaan_p WHERE no=@no", conn, trans);
                log1.Parameters.AddWithValue("@no", noprimary);
                log1.ExecuteNonQuery();

                // 2. Update penerimaan_s (Hanya kalau ada)
                SqlCommand cek2 = new SqlCommand("SELECT COUNT(*) FROM penerimaan_s WHERE no=@no", conn, trans);
                cek2.Parameters.AddWithValue("@no", noprimary);
                int ada2 = (int)cek2.ExecuteScalar();

                if (ada2 > 0)
                {
                    SqlCommand cmd2 = new SqlCommand(@"
            UPDATE penerimaan_s
            SET nomor_rod = @nomorrod, jenis=@jenis, stasiun=@stasiun, e1=@e1, e2=@e2, e3=@e3, s=@s, d=@d, 
                b=@b, ba=@ba, cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah,
                remaks=@remaks, updated_at=@diubah
            WHERE no=@no", conn, trans);

                    cmd2.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd2.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd2.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                    cmd2.Parameters.AddWithValue("@e1", txte1.Text);
                    cmd2.Parameters.AddWithValue("@e2", txte2.Text);
                    cmd2.Parameters.AddWithValue("@e3", txte3.Text);
                    cmd2.Parameters.AddWithValue("@s", txts.Text);
                    cmd2.Parameters.AddWithValue("@d", txtd.Text);
                    cmd2.Parameters.AddWithValue("@b", txtb.Text);
                    cmd2.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd2.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd2.Parameters.AddWithValue("@m", txtm.Text);
                    cmd2.Parameters.AddWithValue("@r", txtr.Text);
                    cmd2.Parameters.AddWithValue("@c", txtc.Text);
                    cmd2.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                    cmd2.Parameters.AddWithValue("@remaks", loginform.login.name);
                    cmd2.Parameters.AddWithValue("@no", noprimary);
                    cmd2.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Penerimaan_s gada");
                }

                // 3. Update perbaikan_p (Hanya kalau ada)
                SqlCommand cek3 = new SqlCommand("SELECT COUNT(*) FROM perbaikan_p WHERE no=@no", conn, trans);
                cek3.Parameters.AddWithValue("@no", noprimary);
                int ada3 = (int)cek3.ExecuteScalar();

                if (ada3 > 0)
                {
                    SqlCommand cmd3 = new SqlCommand(@"
            UPDATE perbaikan_p 
            SET nomor_rod=@nomorrod, jenis=@jenis, remaks=@remaks, updated_at=@diubah
            WHERE no=@no", conn, trans);
                    cmd3.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd3.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd3.Parameters.AddWithValue("@remaks", loginform.login.name);
                    cmd3.Parameters.AddWithValue("@no", noprimary);
                    cmd3.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd3.ExecuteNonQuery();

                    SqlCommand log3 = new SqlCommand("INSERT INTO perbaikan_e SELECT * FROM perbaikan_p WHERE no=@no", conn, trans);
                    log3.Parameters.AddWithValue("@no", noprimary);
                    log3.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Perbaikan_p gada");
                }

                // 4. Update perbaikan_s (Hanya kalau ada)
                SqlCommand cek4 = new SqlCommand("SELECT COUNT(*) FROM perbaikan_s WHERE no=@no", conn, trans);
                cek4.Parameters.AddWithValue("@no", noprimary);
                int ada4 = (int)cek4.ExecuteScalar();

                if (ada4 > 0)
                {
                    SqlCommand cmd4 = new SqlCommand(@"
            UPDATE perbaikan_s 
            SET nomor_rod=@nomorrod, jenis=@jenis, remaks=@remaks, updated_at=@diubah
            WHERE no=@no", conn, trans);
                    cmd4.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd4.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd4.Parameters.AddWithValue("@remaks", loginform.login.name);
                    cmd4.Parameters.AddWithValue("@no", noprimary);
                    cmd4.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd4.ExecuteNonQuery();

                    SqlCommand log4 = new SqlCommand("INSERT INTO perbaikan_e SELECT * FROM perbaikan_s WHERE no=@no", conn, trans);
                    log4.Parameters.AddWithValue("@no", noprimary);
                    log4.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Perbaikan_s gada");
                }

                // 5. Update pengiriman (Hanya kalau ada)
                SqlCommand cek5 = new SqlCommand("SELECT COUNT(*) FROM pengiriman WHERE no=@no", conn, trans);
                cek5.Parameters.AddWithValue("@no", noprimary);
                int ada5 = (int)cek5.ExecuteScalar();

                if (ada5 > 0)
                {
                    SqlCommand cmd5 = new SqlCommand(@"
            UPDATE pengiriman 
            SET nomor_rod=@nomorrod, remaks=@remaks, updated_at=@diubah
            WHERE no=@no", conn, trans);
                    cmd5.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd5.Parameters.AddWithValue("@remaks", loginform.login.name);
                    cmd5.Parameters.AddWithValue("@no", noprimary);
                    cmd5.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd5.ExecuteNonQuery();

                    SqlCommand log5 = new SqlCommand("INSERT INTO pengiriman_e SELECT * FROM pengiriman WHERE no=@no", conn, trans);
                    log5.Parameters.AddWithValue("@no", noprimary);
                    log5.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("pengiriman gada");
                }

                trans.Commit();

                MessageBox.Show("Data berhasil diperbarui.", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                setdefault();
                tampil();
                btnupdate.Enabled = false;
                btnclear.Enabled = false;
                setfalse();
            }
            catch (SqlException ex)
            {
                if (trans != null) trans.Rollback();
                MessageBox.Show("Koneksi ke database gagal atau terputus.\n\nDetail: " + ex.Message,
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                MessageBox.Show("Terjadi kesalahan sistem:\n\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
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

        private void txtba_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtcr_TextChanged(object sender, EventArgs e)
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

        private void editpenerimaan_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }
    }
}
