﻿using System;
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
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class Penerimaan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        bool infocari = false;
        int noprimary;

        public Penerimaan()
        {
            InitializeComponent();
            setdefault();
            tampil();
            btnsimpan.Enabled = false;
        }

        private void simpandata()
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT nomor_rod FROM penerimaan_s WHERE nomor_rod = @rod", conn);
                    cmd.Parameters.AddWithValue("@rod", txtnomorrod.Text);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            MessageBox.Show("Nomor ROD ditable ini sudah ada dan belum diperbaiki.", "Warning");
                            return;
                        }
                    }

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO penerimaan_s (tanggal_penerimaan,shift,nomor_rod,jenis,stasiun,e1,e2,e3," +
                        "s,d,b,ba,cr,m,r,c,rl,jumlah,updated_at) VALUES(getdate(),@shift,@nomorrod,@jenis,@stasiun,@e1,@e2,@e3,@s,@d,@b,@ba,@cr,@m,@r,@c,@rl,@jumlah,getdate())", conn);
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO penerimaan_p (tanggal_penerimaan,shift,nomor_rod,jenis,stasiun,e1,e2,e3," +
                        "s,d,b,ba,cr,m,r,c,rl,jumlah,updated_at) VALUES(getdate(),@shift,@nomorrod,@jenis,@stasiun,@e1,@e2,@e3,@s,@d,@b,@ba,@cr,@m,@r,@c,@rl,@jumlah,getdate())", conn);

                    cmd1.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
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
                    cmd1.Parameters.AddWithValue("@jumlah", lbltotal.Text);

                    cmd2.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
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
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotal.Text);

                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Disimpan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
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
                    SqlCommand cmd = new SqlCommand("UPDATE penerimaan_s SET nomor_rod = @nomorrod, jenis = @jenis, stasiun = @stasiun, e1 = @e1, e2 = @e2, e3 = @e3, s = @s, d = @d," +
                        "b = @b, ba = @ba, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah, updated_at = getdate() WHERE no = @no ", conn);
                    SqlCommand cmd2 = new SqlCommand("UPDATE penerimaan_p SET nomor_rod = @nomorrod, jenis = @jenis, stasiun = @stasiun, e1 = @e1, e2 = @e2, e3 = @e3, s = @s, d = @d," +
                        "b = @b, ba = @ba, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah, updated_at = getdate() WHERE no = @no ", conn);

                    cmd.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                    cmd.Parameters.AddWithValue("@e1", txte1.Text);
                    cmd.Parameters.AddWithValue("@e2", txte2.Text);
                    cmd.Parameters.AddWithValue("@e3", txte3.Text);
                    cmd.Parameters.AddWithValue("@s", txts.Text);
                    cmd.Parameters.AddWithValue("@d", txtd.Text);
                    cmd.Parameters.AddWithValue("@b", txtb.Text);
                    cmd.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd.Parameters.AddWithValue("@m", txtm.Text);
                    cmd.Parameters.AddWithValue("@r", txtr.Text);
                    cmd.Parameters.AddWithValue("@c", txtc.Text);
                    cmd.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd.Parameters.AddWithValue("@no", noprimary);

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
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd2.Parameters.AddWithValue("@no", noprimary);

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diupdate", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampil();
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
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "" || txtstasiun.Text == "")
            {
                MessageBox.Show("Nomor ROD, Jenis dan Stasiun Tidak Boleh Kosong", "Warning");
                return;
            }

            if (btnsimpan.Text == "Update Data")
            {
                editdata();
                setdefault();
                btnsimpan.Text = "Simpan Data";
                btncancel.Enabled = false;
                btnsimpan.Enabled = false;
                btnhitung.Text = "Hitung";
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
            lbltotal.Text = hasil.ToString();
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void btnhitung_Click(object sender, EventArgs e)
        {
               
        }

        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.","Warning");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM penerimaan_s WHERE 1=1";

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
                btnsimpan.Text = "Update Data";
                btnhitung.Text = "Hitung Ulang";

            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            setdefault();
            btnsimpan.Text = "Simpan Data";
            btncancel.Enabled = false;
            btnsimpan.Enabled = false;
            btnhitung.Text = "Hitung";
        }

        private void Penerimaan_Load(object sender, EventArgs e)
        {
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            txtnomorrod.Focus();
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
    }
}
