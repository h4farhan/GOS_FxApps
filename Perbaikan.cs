﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace GOS_FxApps
{
    public partial class Perbaikan : Form
    {
        private DateTime tanggalpenerimaan;
        SqlConnection conn = Koneksi.GetConnection();
        bool infocari = false;
        int noprimary;

        public Perbaikan()
        {
            InitializeComponent();
            setdefault();
            tampil();
        }

        private void Perbaikan_Load(object sender, EventArgs e)
        {
            btnsimpan.Enabled = false;
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            txtnomorrod.Focus();
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, ba, ba1, cr, m, r, c, rl, jumlah, tanggal_penerimaan, updated_at FROM perbaikan_s ORDER BY tanggal_perbaikan DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query,conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
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
                dataGridView1.Columns[17].HeaderText = "BA";
                dataGridView1.Columns[18].HeaderText = "BA-1";
                dataGridView1.Columns[19].HeaderText = "CR";
                dataGridView1.Columns[20].HeaderText = "M";
                dataGridView1.Columns[21].HeaderText = "R";
                dataGridView1.Columns[22].HeaderText = "C";
                dataGridView1.Columns[23].HeaderText = "RL";
                dataGridView1.Columns[24].HeaderText = "Jumlah";
                dataGridView1.Columns[25].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[26].HeaderText = "Diubah"; 
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
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.", "Warning");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM perbaikan_s WHERE 1=1";

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
            txtba.Clear();
            txtba1.Clear();
            txtcr.Clear();
            txtm.Clear();
            txtr.Clear();
            txtc.Clear();
            txtrl.Clear();
            lbltotal.Text = "-";
            lbltotale1.Text = "-";
            lbltotale2.Text = "-";
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
            txtba.Enabled = true;
            txtba1.Enabled = true;
            txtcr.Enabled = true;
            txtm.Enabled = true;
            txtr.Enabled = true;
            txtc.Enabled = true;
            txtrl.Enabled = true;
            btnhitung.Enabled = true;
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
            txtba.Enabled = false;
            txtba1.Enabled = false;
            txtcr.Enabled = false;
            txtm.Enabled = false;
            txtr.Enabled = false;
            txtc.Enabled = false;
            txtrl.Enabled = false;
            btnhitung.Enabled = false;
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
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO perbaikan_s (tanggal_perbaikan,shift,nomor_rod,jenis,e1_ers,e1_est,e1_jumlah,e2_ers,e2_cst,e2_cstub,e2_jumlah," +
                        "e3,e4,s,d,b,ba,ba1,cr,m,r,c,rl,jumlah,tanggal_penerimaan,updated_at) VALUES(getdate(),@shift,@nomorrod,@jenis,@e1ers,@e1est,@e1jumlah,@e2ers,@e2cst,@e2cstub,@e2jumlah,@e3,@e4,@s,@d,@b,@ba,@ba1,@cr,@m,@r,@c,@rl,@jumlah,@tanggalpenerimaan,getdate())", conn);

                    SqlCommand cmd2 = new SqlCommand("INSERT INTO perbaikan_p (tanggal_perbaikan,shift,nomor_rod,jenis,e1_ers,e1_est,e1_jumlah,e2_ers,e2_cst,e2_cstub,e2_jumlah," +
                        "e3,e4,s,d,b,ba,ba1,cr,m,r,c,rl,jumlah,tanggal_penerimaan,updated_at) VALUES(getdate(),@shift,@nomorrod,@jenis,@e1ers,@e1est,@e1jumlah,@e2ers,@e2cst,@e2cstub,@e2jumlah,@e3,@e4,@s,@d,@b,@ba,@ba1,@cr,@m,@r,@c,@rl,@jumlah,@tanggalpenerimaan,getdate())", conn);

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
                    cmd1.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd1.Parameters.AddWithValue("@ba1", txtba1.Text);
                    cmd1.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd1.Parameters.AddWithValue("@m", txtm.Text);
                    cmd1.Parameters.AddWithValue("@r", txtr.Text);
                    cmd1.Parameters.AddWithValue("@c", txtc.Text);
                    cmd1.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd1.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd1.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);

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
                    cmd2.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd2.Parameters.AddWithValue("@ba1", txtba1.Text);
                    cmd2.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd2.Parameters.AddWithValue("@m", txtm.Text);
                    cmd2.Parameters.AddWithValue("@r", txtr.Text);
                    cmd2.Parameters.AddWithValue("@c", txtc.Text);
                    cmd2.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd2.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);

                    SqlCommand cmd3 = new SqlCommand("DELETE FROM penerimaan_s WHERE nomor_rod = @nomorrod", conn);
                    cmd3.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);

                    cmd3.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Disimpan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampil();
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
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE perbaikan_s SET jenis = @jenis, e1_ers = @e1ers, e1_est = @e1est, e1_jumlah = @e1jumlah, e2_ers = @e2ers, e2_cst = @e2cst, e2_cstub = @e2cstub, e2_jumlah = @e2jumlah," +
                        "e3 = @e3, e4 = @e4, s = @s, d = @d, b = @b, ba = @ba, ba1 = @ba1, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah, updated_at = getdate() WHERE no = @no ", conn);

                    SqlCommand cmd2 = new SqlCommand("UPDATE perbaikan_p SET jenis = @jenis, e1_ers = @e1ers, e1_est = @e1est, e1_jumlah = @e1jumlah, e2_ers = @e2ers, e2_cst = @e2cst, e2_cstub = @e2cstub, e2_jumlah = @e2jumlah," +
                        "e3 = @e3, e4 = @e4, s = @s, d = @d, b = @b, ba = @ba, ba1 = @ba1, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah, updated_at = getdate() WHERE no = @no ", conn);


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
                    cmd.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd.Parameters.AddWithValue("@ba1", txtba1.Text);
                    cmd.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd.Parameters.AddWithValue("@m", txtm.Text);
                    cmd.Parameters.AddWithValue("@r", txtr.Text);
                    cmd.Parameters.AddWithValue("@c", txtc.Text);
                    cmd.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd.Parameters.AddWithValue("@no", noprimary);

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
                    cmd2.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd2.Parameters.AddWithValue("@ba1", txtba1.Text);
                    cmd2.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd2.Parameters.AddWithValue("@m", txtm.Text);
                    cmd2.Parameters.AddWithValue("@r", txtr.Text);
                    cmd2.Parameters.AddWithValue("@c", txtc.Text);
                    cmd2.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd2.Parameters.AddWithValue("@no", noprimary);

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diupdate", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampil();
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
            int angka7 = SafeParse(txts);
            int angka8 = SafeParse(txtd);
            int angka9 = SafeParse(txtb);
            int angka10 = SafeParse(txtba);
            int angka11 = SafeParse(txtba1);
            int angka12 = SafeParse(txtcr);
            int angka13 = SafeParse(txtm);
            int angka14 = SafeParse(txtr);
            int angka15 = SafeParse(txtc);
            int angka16 = SafeParse(txtrl);
            int angka17 = SafeParse(txte4);

            int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12 + angka13 + angka14 + angka15 + angka16 + angka17;
            lbltotal.Text = hasil.ToString();
            btnsimpan.Enabled = true;
        }

        private void btnhitung_Click(object sender, EventArgs e)
        {
            
        }

        private void btnsimpan_Click(object sender, EventArgs e)

        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "")
            {
                MessageBox.Show("Nomro ROD Dan Jenis Tidak Boleh Kosong");
                return;
            }

            if (btnsimpan.Text == "Update Data")
            {
                editdata();
                setdefault();
                setfalse();
                btncancel.Enabled = false;
                btnsimpan.Enabled = false;
                btnsimpan.Text = "Simpan Data";
                btncheck.Enabled = true;
                txtnomorrod.Enabled = true;
                txtnomorrod.Focus();
            }
            else
            {
                simpandata();
                setdefault();
                setfalse();
                txtnomorrod.Enabled = true;
                txtnomorrod.Focus();
                btncheck.FillColor = Color.FromArgb(94, 148, 255);
                btnsimpan.Enabled = false;
                btncancel.Enabled = false;
                btncheck.Enabled= true;
            }
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
                string nomorRod = txtnomorrod.Text;

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT * FROM penerimaan_s WHERE nomor_rod = @nomor_rod";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nomor_rod", nomorRod);

                        try
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                txtjenis.Text = reader["jenis"].ToString();
                                lbltotale1.Text = reader["e1"].ToString();
                                lbltotale2.Text = reader["e2"].ToString();
                                txte3.Text = reader["e3"].ToString();
                                txts.Text = reader["s"].ToString();
                                txtd.Text = reader["d"].ToString();
                                txtb.Text = reader["b"].ToString();
                                txtba.Text = reader["ba"].ToString();
                                txtcr.Text = reader["cr"].ToString();
                                txtm.Text = reader["m"].ToString();
                                txtr.Text = reader["r"].ToString();
                                txtc.Text = reader["c"].ToString();
                                txtrl.Text = reader["rl"].ToString();
                                tanggalpenerimaan = Convert.ToDateTime(reader["tanggal_penerimaan"]);
                                settrue();
                                txtjenis.Focus();
                                txtnomorrod.Enabled = false;
                                btncancel.Enabled = true;
                                btncheck.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show("Nomor ROD tidak ditemukan.", "Warning");
                                setdefault();
                                setfalse();
                            }

                            reader.Close();
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
                txtba.Text = row.Cells["ba"].Value.ToString();
                txtba1.Text = row.Cells["ba1"].Value.ToString();
                txtcr.Text = row.Cells["cr"].Value.ToString();
                txtm.Text = row.Cells["m"].Value.ToString();
                txtr.Text = row.Cells["r"].Value.ToString();
                txtc.Text = row.Cells["c"].Value.ToString();
                txtrl.Text = row.Cells["rl"].Value.ToString();
                lbltotal.Text = row.Cells["jumlah"].Value.ToString();
                settrue();
                btncancel.Enabled = true;
                btnsimpan.Text = "Update Data";
                txtnomorrod.Enabled = false;
                btncheck.Enabled = false;
                btnhitung.Text = "Hitung Ulang";
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            setdefault();
            setfalse();
            btncancel.Enabled = false;
            btnsimpan.Text = "Simpan Data";
            txtnomorrod.Enabled = true;
            btncheck .Enabled = true;
            btnsimpan.Enabled = false;
            btncheck.Enabled= true;
            txtnomorrod.Focus();
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

        private void txtnomorrod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                btncheck.PerformClick();   
            
        }

    }
}
}
