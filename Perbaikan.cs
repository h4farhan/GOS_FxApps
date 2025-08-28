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
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace GOS_FxApps
{
    public partial class Perbaikan : Form
    {
        private DateTime tanggalpenerimaan;
        SqlConnection conn = Koneksi.GetConnection();
        int noprimary;

        public Perbaikan()
        {
            InitializeComponent();
            setdefault();
            tampilperbaikan();
            tampilpenerimaan();
        }

        private void registertampilpenerimaan()
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
                            tampilpenerimaan();
                            registertampilpenerimaan();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registertampilperbaikan()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.perbaikan_s", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampilperbaikan();
                            registertampilperbaikan();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void Perbaikan_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            btnsimpan.Enabled = false;
            txtnomorrod.Focus();
            registertampilpenerimaan();
            registertampilperbaikan();
        }

        private void tampilpenerimaan()
        {
            try
            {
                string query = "SELECT * FROM penerimaan_s ORDER BY tanggal_penerimaan DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView2.DataSource = dt;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView2.RowTemplate.Height = 35;
                dataGridView2.ReadOnly = true;

                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Tanggal Penerimaan";
                dataGridView2.Columns[2].HeaderText = "Shift";
                dataGridView2.Columns[3].HeaderText = "Nomor ROD";
                dataGridView2.Columns[4].HeaderText = "Jenis";
                dataGridView2.Columns[5].HeaderText = "Stasiun";
                dataGridView2.Columns[6].HeaderText = "E1";
                dataGridView2.Columns[7].HeaderText = "E2";
                dataGridView2.Columns[8].HeaderText = "E3";
                dataGridView2.Columns[9].HeaderText = "S";
                dataGridView2.Columns[10].HeaderText = "D";
                dataGridView2.Columns[11].HeaderText = "B";
                dataGridView2.Columns[12].HeaderText = "BA";
                dataGridView2.Columns[13].HeaderText = "CR";
                dataGridView2.Columns[14].HeaderText = "M";
                dataGridView2.Columns[15].HeaderText = "R";
                dataGridView2.Columns[16].HeaderText = "C";
                dataGridView2.Columns[17].HeaderText = "RL";
                dataGridView2.Columns[18].HeaderText = "Jumlah";
                dataGridView2.Columns[19].HeaderText = "Diubah";
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

        private bool caripenerimaan()
        {
            string inputRod = txtcaripenerimaan.Text.Trim();
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

        private void tampilperbaikan()
        {
            try
            {
                string query = "SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, bac, nba, ba, ba1, cr, m, r, c, rl, jumlah, tanggal_penerimaan, updated_at FROM perbaikan_s ORDER BY tanggal_perbaikan DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query,conn);
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
                dataGridView1.Columns[21].HeaderText = "CR";
                dataGridView1.Columns[22].HeaderText = "M";
                dataGridView1.Columns[23].HeaderText = "R";
                dataGridView1.Columns[24].HeaderText = "C";
                dataGridView1.Columns[25].HeaderText = "RL";
                dataGridView1.Columns[26].HeaderText = "Jumlah";
                dataGridView1.Columns[27].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[28].HeaderText = "Diubah"; 
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

        private bool cariperbaikan()
        {
            string inputRod = txtcariperbaikan.Text.Trim();
            DataTable dt = new DataTable();

            string query = "SELECT * FROM perbaikan_s WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (!string.IsNullOrEmpty(inputRod))
                {
                    query += " AND CAST(nomor_rod AS VARCHAR) LIKE @rod";
                    cmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");
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
            txtnomorrod.Clear();
            txtnomorrod.Clear();
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
            lbltotal.Text = "-";
            lbltotale1.Text = "-";
            lbltotale2.Text = "-";
            lbltotalba.Text = "-";
        }

        private void settrue()
        {
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

        private void simpandata()
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO perbaikan_s (tanggal_perbaikan,shift,nomor_rod,jenis,e1_ers,e1_est,e1_jumlah,e2_ers,e2_cst,e2_cstub,e2_jumlah," +
                        "e3,e4,s,d,b,bac,nba,ba,ba1,cr,m,r,c,rl,jumlah,tanggal_penerimaan,updated_at) VALUES(@tanggal,@shift,@nomorrod,@jenis,@e1ers,@e1est,@e1jumlah,@e2ers,@e2cst,@e2cstub,@e2jumlah,@e3,@e4,@s,@d,@b,@bac,@nba,@ba,@ba1,@cr,@m,@r,@c,@rl,@jumlah,@tanggalpenerimaan,@diubah)", conn);

                    SqlCommand cmd2 = new SqlCommand("INSERT INTO perbaikan_p (tanggal_perbaikan,shift,nomor_rod,jenis,e1_ers,e1_est,e1_jumlah,e2_ers,e2_cst,e2_cstub,e2_jumlah," +
                        "e3,e4,s,d,b,bac,nba,ba,ba1,cr,m,r,c,rl,jumlah,tanggal_penerimaan,updated_at) VALUES(@tanggal,@shift,@nomorrod,@jenis,@e1ers,@e1est,@e1jumlah,@e2ers,@e2cst,@e2cstub,@e2jumlah,@e3,@e4,@s,@d,@b,@bac,@nba,@ba,@ba1,@cr,@m,@r,@c,@rl,@jumlah,@tanggalpenerimaan,@diubah)", conn);

                    cmd1.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                    cmd1.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
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
                    cmd1.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd1.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);
                    cmd1.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);

                    cmd2.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                    cmd2.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
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
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd2.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);
                    cmd2.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);

                    SqlCommand cmd3 = new SqlCommand("DELETE FROM penerimaan_s WHERE nomor_rod = @nomorrod", conn);
                    cmd3.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);

                    cmd3.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilperbaikan();
                    tampilpenerimaan();
                }
                else
                {

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
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE perbaikan_s SET jenis = @jenis, e1_ers = @e1ers, e1_est = @e1est, e1_jumlah = @e1jumlah, e2_ers = @e2ers, e2_cst = @e2cst, e2_cstub = @e2cstub, e2_jumlah = @e2jumlah," +
                        "e3 = @e3, e4 = @e4, s = @s, d = @d, bac = @bac, nba = @nba, b = @b, ba = @ba, ba1 = @ba1, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah, updated_at = @diubah WHERE no = @no ", conn);

                    SqlCommand cmd2 = new SqlCommand("UPDATE perbaikan_p SET jenis = @jenis, e1_ers = @e1ers, e1_est = @e1est, e1_jumlah = @e1jumlah, e2_ers = @e2ers, e2_cst = @e2cst, e2_cstub = @e2cstub, e2_jumlah = @e2jumlah," +
                        "e3 = @e3, e4 = @e4, s = @s, d = @d, bac = @bac, nba = @nba, b = @b, ba = @ba, ba1 = @ba1, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah, updated_at = @diubah WHERE no = @no ", conn);


                    cmd.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd.Parameters.AddWithValue("@e1ers", txte1ers.Text);
                    cmd.Parameters.AddWithValue("@e1est", txte1est.Text);
                    cmd.Parameters.AddWithValue("@e1jumlah", lbltotale1.Text);
                    cmd.Parameters.AddWithValue("@e2ers", txte2ers.Text);
                    cmd.Parameters.AddWithValue("@e2cst", txte2cst.Text);
                    cmd.Parameters.AddWithValue("@e2cstub", txte2cstub.Text);
                    cmd.Parameters.AddWithValue("@e2jumlah", lbltotale2.Text);
                    cmd.Parameters.AddWithValue("@e3", txte3.Text);
                    cmd.Parameters.AddWithValue("@e4", txte4.Text);
                    cmd.Parameters.AddWithValue("@s", txts.Text);
                    cmd.Parameters.AddWithValue("@d", txtd.Text);
                    cmd.Parameters.AddWithValue("@b", txtb.Text);
                    cmd.Parameters.AddWithValue("@bac", txtbac.Text);
                    cmd.Parameters.AddWithValue("@nba", txtnba.Text);
                    cmd.Parameters.AddWithValue("@ba", lbltotalba.Text);
                    cmd.Parameters.AddWithValue("@ba1", txtba1.Text);
                    cmd.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd.Parameters.AddWithValue("@m", txtm.Text);
                    cmd.Parameters.AddWithValue("@r", txtr.Text);
                    cmd.Parameters.AddWithValue("@c", txtc.Text);
                    cmd.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd.Parameters.AddWithValue("@no", noprimary);
                    cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);

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
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd2.Parameters.AddWithValue("@no", noprimary);
                    cmd2.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diedit", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilperbaikan();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif. " + ex.Message,
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
            int angka7 = 0;
            int angka7a = SafeParse(txts);
            if (angka7a > 0)
            {
                angka7 = 1;
            }
            else
            {
                angka7 = 0;
            }

            int angka8 = SafeParse(txtd);
            int angka9 = SafeParse(txtb);
            int angka10 = SafeParse(txtba1);
            int angka11 = 0;
            int angka11a = SafeParse(txtcr);
            if (angka11a > 0) 
            { 
                angka11 =1;
            }
            else
            {
                angka11 = 0;
            }
            int angka12 = SafeParse(txtm);
            int angka13 = SafeParse(txtr);
            int angka14 = 0;
            int angka14a = SafeParse(txtc);
            if (angka14a > 0)
            {
                angka14 = 1;
            }
            else
            {
                angka14 = 0;
            }
            int angka15 = SafeParse(txtrl);
            int angka16 = SafeParse(txte4);

            int angka17 = SafeParse(txtbac);
            int angka18 = SafeParse(txtnba);
            int hasilba = angka17 + angka18;
            lbltotalba.Text = hasilba.ToString();

            int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12 + angka13 + angka14 + angka15 + angka16 + angka17 + angka18;
            lbltotal.Text = hasil.ToString();
            btnsimpan.Enabled = true;
        }

        private void btnsimpan_Click(object sender, EventArgs e)

        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "")
            {
                MessageBox.Show("Jenis Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnsimpan.Text == "Edit Data")
            {
                editdata();
                txtnomorrod.Enabled = true;
                setdefault();
                txtnomorrod.Enabled = false;
                setfalse();
                btncancel.Enabled = false;
                btnsimpan.Enabled = false;
                btnsimpan.Text = "Simpan Data";
            }
            else
            {
                simpandata();
                txtnomorrod.Enabled = true;
                setdefault();
                txtnomorrod.Enabled = false;
                setfalse();
                btnsimpan.Enabled = false;
                btncancel.Enabled = false;
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
                lbltotal.Text = row.Cells["jumlah"].Value.ToString();
                settrue();
                btncancel.Enabled = true;
                btnsimpan.Text = "Edit Data";
                txtnomorrod.Enabled = false;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            txtnomorrod.Enabled = true;
            setdefault();
            txtnomorrod.Enabled = false;
            setfalse();
            btncancel.Enabled = false;
            btnsimpan.Text = "Simpan Data";
            btnsimpan.Enabled = false;
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }

        private void txtrl_TextChanged(object sender, EventArgs e)
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

        private void txtd_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtb_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtnba_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtbac_TextChanged(object sender, EventArgs e)
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

        private void txte1ers_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtba1_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte4_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                tanggalpenerimaan = Convert.ToDateTime(row.Cells["tanggal_penerimaan"].Value);
                txtnomorrod.Text = row.Cells["nomor_rod"].Value.ToString();
                txtjenis.Text = row.Cells["jenis"].Value.ToString();
                txte1ers.Text = row.Cells["e1"].Value.ToString();
                txte2ers.Text = row.Cells["e2"].Value.ToString();
                txte3.Text = row.Cells["e3"].Value.ToString();
                txts.Text = row.Cells["s"].Value.ToString();
                txtd.Text = row.Cells["d"].Value.ToString();
                txtb.Text = row.Cells["b"].Value.ToString();
                txtbac.Text = row.Cells["ba"].Value.ToString();
                txtcr.Text = row.Cells["cr"].Value.ToString();
                txtm.Text = row.Cells["m"].Value.ToString();
                txtr.Text = row.Cells["r"].Value.ToString();
                txtc.Text = row.Cells["c"].Value.ToString();
                txtrl.Text = row.Cells["rl"].Value.ToString();
                lbltotal.Text = row.Cells["jumlah"].Value.ToString();
                settrue();
                btncancel.Enabled = true;

            }
        }

        private void Perbaikan_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void txtcaripenerimaan_TextChanged(object sender, EventArgs e)
        {
            caripenerimaan();
        }

        private void txtcariperbaikan_TextChanged(object sender, EventArgs e)
        {
            cariperbaikan();
        }
    }
}
