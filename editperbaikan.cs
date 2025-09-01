using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class editperbaikan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        bool infocari = false;
        string tanggalpenerimaan;
        string tanggalperbaikan;
        int shift;
        int noprimary;

        public editperbaikan()
        {
            InitializeComponent();
        }

        private void registertampil()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.perbaikan_p", conn))
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

        private void editperbaikan_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            tampil();
            dateeditperbaikan.Value = DateTime.Now.Date;
            dateeditperbaikan.Checked = false;
            registertampil();
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, bac, nba, ba, ba1, r, m, cr, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks FROM perbaikan_p ORDER BY tanggal_perbaikan DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Perbaikan";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Jenis";
                dataGridView1.Columns[5].HeaderText = "E1 Ers";
                dataGridView1.Columns[6].HeaderText = "E1 Est";
                dataGridView1.Columns[7].HeaderText = "E1 Jumlah";
                dataGridView1.Columns[8].HeaderText = "E2 Ers";
                dataGridView1.Columns[9].HeaderText = "E2 Cst";
                dataGridView1.Columns[10].HeaderText = "E2 Cstub";
                dataGridView1.Columns[11].HeaderText = "E2 Jumlah";
                dataGridView1.Columns[12].HeaderText = "E3";
                dataGridView1.Columns[13].HeaderText = "E4";
                dataGridView1.Columns[14].HeaderText = "S";
                dataGridView1.Columns[15].HeaderText = "D";
                dataGridView1.Columns[16].HeaderText = "B";
                dataGridView1.Columns[17].HeaderText = "BAC";
                dataGridView1.Columns[18].HeaderText = "NBA";
                dataGridView1.Columns[19].HeaderText = "BA";
                dataGridView1.Columns[20].HeaderText = "BA-1";
                dataGridView1.Columns[21].HeaderText = "R";
                dataGridView1.Columns[22].HeaderText = "M";
                dataGridView1.Columns[23].HeaderText = "CR";
                dataGridView1.Columns[24].HeaderText = "C";
                dataGridView1.Columns[25].HeaderText = "RL";
                dataGridView1.Columns[26].HeaderText = "Jumlah";
                dataGridView1.Columns[27].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[28].HeaderText = "Diubah";
                dataGridView1.Columns[29].HeaderText = "Remaks";
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
            DateTime? tanggal = dateeditperbaikan.Checked ? (DateTime?)dateeditperbaikan.Value.Date : null;
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM perbaikan_p WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += "AND CAST(tanggal_perbaikan AS DATE) = @tgl";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(inputRod))
                {
                    query += " AND nomor_rod = @rod";
                    cmd.Parameters.AddWithValue("@rod", Convert.ToInt32(inputRod));
                }

                query += " ORDER BY tanggal_perbaikan DESC";

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
            txte1ers.Clear();
            txte1est.Clear();
            txte2ers.Clear();
            txte2cst.Clear();
            txte2cstub.Clear();
            txte3.Clear();
            txte4.Clear();
            txts.Clear();
            txtd.Clear();
            txtb.Clear();
            txtbac.Clear();
            txtnba.Clear();
            txtba1.Clear();
            txtcr.Clear();
            txtm.Clear();
            txtr.Clear();
            txtc.Clear();
            txtrl.Clear();
            lbltotalsebelum.Text = "-";
            lbltotalupdate.Text = "-";
            lbltotale1.Text = "-";
            lbltotale2.Text = "-";
            lbltotalba.Text = "-";
        }

        private void settrue()
        {
            txtnomorrod.Enabled = true;
            txtjenis.Enabled = true;
            txte1ers.Enabled = true;
            txte1est.Enabled = true;
            txte2ers.Enabled = true;
            txte2cst.Enabled = true;
            txte2cstub.Enabled = true;
            txte3.Enabled = true;
            txte4.Enabled = true;
            txts.Enabled = true;
            txtd.Enabled = true;
            txtb.Enabled = true;
            txtbac.Enabled = true;
            txtnba.Enabled = true;
            txtba1.Enabled = true;
            txtcr.Enabled = true;
            txtm.Enabled = true;
            txtr.Enabled = true;
            txtc.Enabled = true;
            txtrl.Enabled = true;
        }

        private void setfalse()
        {
            txtjenis.Enabled = false;
            txte1ers.Enabled = false;
            txte1est.Enabled = false;
            txte2ers.Enabled = false;
            txte2cst.Enabled = false;
            txte2cstub.Enabled = false;
            txte3.Enabled = false;
            txte4.Enabled = false;
            txts.Enabled = false;
            txtd.Enabled = false;
            txtb.Enabled = false;
            txtbac.Enabled = false;
            txtnba.Enabled = false;
            txtba1.Enabled = false;
            txtcr.Enabled = false;
            txtm.Enabled = false;
            txtr.Enabled = false;
            txtc.Enabled = false;
            txtrl.Enabled = false;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnupdate.PerformClick();
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtjenis.Text == "")
            {
                MessageBox.Show("Jenis Tidak Boleh Kosong", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                // === UPDATE perbaikan_p ===
                SqlCommand cmd1 = new SqlCommand(@"
            UPDATE perbaikan_p 
            SET nomor_rod = @nomorrod, jenis=@jenis, e1_ers=@e1ers, e1_est=@e1est, e1_jumlah=@e1jumlah,
                e2_ers=@e2ers, e2_cst=@e2cst, e2_cstub=@e2cstub, e2_jumlah=@e2jumlah,
                e3=@e3, e4=@e4, s=@s, d=@d, b=@b, bac=@bac, nba=@nba, ba=@ba, ba1=@ba1,
                cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah, updated_at=@diubah
            WHERE no=@no", conn, trans);

                cmd1.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                cmd1.Parameters.AddWithValue("@jenis", txtjenis.Text);
                cmd1.Parameters.AddWithValue("@e1ers", txte1ers.Text);
                cmd1.Parameters.AddWithValue("@e1est", txte1est.Text);
                cmd1.Parameters.AddWithValue("@e1jumlah", lbltotale1.Text);
                cmd1.Parameters.AddWithValue("@e2ers", txte2ers.Text);
                cmd1.Parameters.AddWithValue("@e2cst", txte2cst.Text);
                cmd1.Parameters.AddWithValue("@e2cstub", txte2cstub.Text);
                cmd1.Parameters.AddWithValue("@e2jumlah", lbltotale2.Text);
                cmd1.Parameters.AddWithValue("@e3", txte3.Text);
                cmd1.Parameters.AddWithValue("@e4", txte4.Text);
                cmd1.Parameters.AddWithValue("@s", txts.Text);
                cmd1.Parameters.AddWithValue("@d", txtd.Text);
                cmd1.Parameters.AddWithValue("@b", txtb.Text);
                cmd1.Parameters.AddWithValue("@bac", txtbac.Text);
                cmd1.Parameters.AddWithValue("@nba", txtnba.Text);
                cmd1.Parameters.AddWithValue("@ba", lbltotalba.Text);
                cmd1.Parameters.AddWithValue("@ba1", txtba1.Text);
                cmd1.Parameters.AddWithValue("@cr", txtcr.Text);
                cmd1.Parameters.AddWithValue("@m", txtm.Text);
                cmd1.Parameters.AddWithValue("@r", txtr.Text);
                cmd1.Parameters.AddWithValue("@c", txtc.Text);
                cmd1.Parameters.AddWithValue("@rl", txtrl.Text);
                cmd1.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                cmd1.Parameters.AddWithValue("@no", noprimary);
                cmd1.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmd1.ExecuteNonQuery();

                SqlCommand log1 = new SqlCommand(
                    "INSERT INTO perbaikan_e SELECT * FROM perbaikan_p WHERE no=@no",
                    conn, trans);
                log1.Parameters.AddWithValue("@no", noprimary);
                log1.ExecuteNonQuery();

                // === UPDATE perbaikan_s jika ada ===
                SqlCommand cek2 = new SqlCommand(
                    "SELECT COUNT(*) FROM perbaikan_s WHERE no=@no", conn, trans);
                cek2.Parameters.AddWithValue("@no", noprimary);
                int ada2 = (int)cek2.ExecuteScalar();

                if (ada2 > 0)
                {
                    SqlCommand cmd2 = new SqlCommand(@"
                UPDATE perbaikan_s 
                SET nomor_rod = @nomorrod, jenis=@jenis, e1_ers=@e1ers, e1_est=@e1est, e1_jumlah=@e1jumlah,
                    e2_ers=@e2ers, e2_cst=@e2cst, e2_cstub=@e2cstub, e2_jumlah=@e2jumlah,
                    e3=@e3, e4=@e4, s=@s, d=@d, b=@b, bac=@bac, nba=@nba, ba=@ba, ba1=@ba1,
                    cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah, updated_at=@diubah
                WHERE no=@no", conn, trans);

                    cmd2.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd2.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd2.Parameters.AddWithValue("@e1ers", txte1ers.Text);
                    cmd2.Parameters.AddWithValue("@e1est", txte1est.Text);
                    cmd2.Parameters.AddWithValue("@e1jumlah", lbltotale1.Text);
                    cmd2.Parameters.AddWithValue("@e2ers", txte2ers.Text);
                    cmd2.Parameters.AddWithValue("@e2cst", txte2cst.Text);
                    cmd2.Parameters.AddWithValue("@e2cstub", txte2cstub.Text);
                    cmd2.Parameters.AddWithValue("@e2jumlah", lbltotale2.Text);
                    cmd2.Parameters.AddWithValue("@e3", txte3.Text);
                    cmd2.Parameters.AddWithValue("@e4", txte4.Text);
                    cmd2.Parameters.AddWithValue("@s", txts.Text);
                    cmd2.Parameters.AddWithValue("@d", txtd.Text);
                    cmd2.Parameters.AddWithValue("@b", txtb.Text);
                    cmd2.Parameters.AddWithValue("@bac", txtbac.Text);
                    cmd2.Parameters.AddWithValue("@nba", txtnba.Text);
                    cmd2.Parameters.AddWithValue("@ba", lbltotalba.Text);
                    cmd2.Parameters.AddWithValue("@ba1", txtba1.Text);
                    cmd2.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd2.Parameters.AddWithValue("@m", txtm.Text);
                    cmd2.Parameters.AddWithValue("@r", txtr.Text);
                    cmd2.Parameters.AddWithValue("@c", txtc.Text);
                    cmd2.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                    cmd2.Parameters.AddWithValue("@no", noprimary);
                    cmd2.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);

                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Perbaikan_s tidak ditemukan");
                }

                //UPDATE penerimaan_p jika ada
                SqlCommand cek3 = new SqlCommand(
                    "SELECT COUNT(*) FROM penerimaan_p WHERE no=@no", conn, trans);
                cek3.Parameters.AddWithValue("@no", noprimary);
                int ada3 = (int)cek3.ExecuteScalar();

                if (ada3 > 0)
                {
                    SqlCommand cmd3 = new SqlCommand(@"
                UPDATE penerimaan_p
                SET nomor_rod=@nomorrod, jenis=@jenis, remaks=@remaks, updated_at=@diubah
                WHERE no=@no", conn, trans);

                    cmd3.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd3.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd3.Parameters.AddWithValue("@remaks", loginform.login.name);
                    cmd3.Parameters.AddWithValue("@no", noprimary);
                    cmd3.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd3.ExecuteNonQuery();

                    SqlCommand log3 = new SqlCommand(
                        "INSERT INTO penerimaan_e SELECT * FROM penerimaan_p WHERE no=@no",
                        conn, trans);
                    log3.Parameters.AddWithValue("@no", noprimary);
                    log3.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Penerimaan_p tidak ditemukan");
                }

                //UPDATE pengiriman jika ada
                SqlCommand cek5 = new SqlCommand(
                    "SELECT COUNT(*) FROM pengiriman WHERE no=@no", conn, trans);
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

                    SqlCommand log5 = new SqlCommand(
                        "INSERT INTO pengiriman_e SELECT * FROM pengiriman WHERE no=@no",
                        conn, trans);
                    log5.Parameters.AddWithValue("@no", noprimary);
                    log5.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Pengiriman tidak ditemukan");
                }

                //Commit transaksi
                trans.Commit();

                MessageBox.Show("Data berhasil diperbarui.",
                                "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                setdefault();
                tampil();
                btnupdate.Enabled = false;
                btnclear.Enabled = false;
                setfalse();
            }
            catch (SqlException ex)
            {
                if (trans != null) trans.Rollback();
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif." + ex.Message,
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {   
            setdefault();
            btnclear.Enabled = false;   
            btnupdate.Enabled = false;
            setfalse();   
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
                dateeditperbaikan.Checked = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["no"].Value);
                tanggalperbaikan = row.Cells["tanggal_perbaikan"].ToString();
                shift = Convert.ToInt32(row.Cells["shift"].Value);        
                txtnomorrod.Text = row.Cells["nomor_rod"].Value.ToString();           
                txtjenis.Text = row.Cells["jenis"].Value.ToString();           
                txte1ers.Text = row.Cells["e1_ers"].Value.ToString();              
                txte1est.Text = row.Cells["e1_est"].Value.ToString();              
                lbltotale1.Text = row.Cells["e1_jumlah"].Value.ToString();
                txte2ers.Text = row.Cells["e2_ers"].Value.ToString();              
                txte2cst.Text = row.Cells["e2_cst"].Value.ToString();             
                txte2cstub.Text = row.Cells["e2_cstub"].Value.ToString();             
                lbltotale2.Text = row.Cells["e2_jumlah"].Value.ToString();
                txte3.Text = row.Cells["e3"].Value.ToString();          
                txte4.Text = row.Cells["e4"].Value.ToString();          
                txts.Text = row.Cells["s"].Value.ToString();              
                txtd.Text = row.Cells["d"].Value.ToString();               
                txtb.Text = row.Cells["b"].Value.ToString();            
                txtbac.Text = row.Cells["bac"].Value.ToString();            
                txtnba.Text = row.Cells["nba"].Value.ToString();            
                lbltotalba.Text = row.Cells["ba"].Value.ToString();            
                txtba1.Text = row.Cells["ba1"].Value.ToString();              
                txtcr.Text = row.Cells["cr"].Value.ToString();               
                txtm.Text = row.Cells["m"].Value.ToString();               
                txtr.Text = row.Cells["r"].Value.ToString();               
                txtc.Text = row.Cells["c"].Value.ToString();                
                txtrl.Text = row.Cells["rl"].Value.ToString();                
                lbltotalsebelum.Text = row.Cells["jumlah"].Value.ToString();
                tanggalpenerimaan = row.Cells["tanggal_penerimaan"].Value.ToString();

                settrue();

                btnclear.Enabled = true;
                txtjenis.Focus();
            }
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
            int angka1 = SafeParse(txte1ers);
            int angka2 = SafeParse(txte1est);
            int hasile1 = angka1 + angka2;
            lbltotale1.Text = hasile1.ToString();

            int angka3 = SafeParse(txte2ers);
            int angka4 = SafeParse(txte2cst);
            int angka5 = SafeParse(txte2cstub);
            int hasile2 = angka3 + angka4 + angka5;
            lbltotale2.Text = hasile2.ToString();

            int angka6 = SafeParse(txte3);
            int angka7 = SafeParse(txts);
            int angka8 = SafeParse(txtd);
            int angka9 = SafeParse(txtb);
            int angka10 = SafeParse(txtba1);
            int angka11 = SafeParse(txtcr);
            int angka12 = SafeParse(txtm);
            int angka13 = SafeParse(txtr);
            int angka14 = SafeParse(txtc);
            int angka15 = SafeParse(txtrl);
            int angka16 = SafeParse(txte4);

            int angka17 = SafeParse(txtbac);
            int angka18 = SafeParse(txtnba);
            int hasilba = angka17 + angka18;
            lbltotalba.Text = hasilba.ToString();

            int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12 + angka13 + angka14 + angka15 + angka16 + angka17 + angka18;
            lbltotalupdate.Text = hasil.ToString();
            btnupdate.Enabled = true;
        }

        private void txte1ers_TextChanged_1(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtrl_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtnba_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte1est_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2ers_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2cst_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2cstub_TextChanged(object sender, EventArgs e)
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

        private void txte4_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtba1_TextChanged(object sender, EventArgs e)
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

        private void txtcr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtc_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtbac_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void editperbaikan_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
        }

    }
}
