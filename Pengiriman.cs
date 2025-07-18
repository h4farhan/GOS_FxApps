﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Guna.UI2.WinForms;
using static System.Net.Mime.MediaTypeNames;

namespace GOS_FxApps
{
    public partial class Pengiriman : Form
    {
        private List<Perbaikan> list = new List<Perbaikan>();
        SqlConnection conn = Koneksi.GetConnection();

        bool infocari = false;

        public class Perbaikan
        {
            public DateTime? TanggalPerbaikan { get; set; }
            public string Shift { get; set; }
            public int? NomorRod { get; set; }
            public string Jenis { get; set; }
            public int? E1_Ers { get; set; }
            public int? E1_Est { get; set; }
            public int? E1_Jumlah { get; set; }
            public int? E2_Ers { get; set; }
            public int? E2_Cst { get; set; }
            public int? E2_Cstub { get; set; }
            public int? E2_Jumlah { get; set; }
            public int? E3 { get; set; }
            public int? S { get; set; }
            public int? D { get; set; }
            public int? B { get; set; }
            public int? Ba { get; set; }
            public int? Cr { get; set; }
            public int? M { get; set; }
            public int? R { get; set; }
            public int? C { get; set; }
            public int? Rl { get; set; }
            public int? Jumlah { get; set; }
            public DateTime? TanggalPenerimaan { get; set; }
        }

        public Pengiriman()
        {
            InitializeComponent();            
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM pengiriman ORDER BY tanggal_pengiriman DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Pengiriman";
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
                dataGridView1.Columns[13].HeaderText = "S";
                dataGridView1.Columns[14].HeaderText = "D";
                dataGridView1.Columns[15].HeaderText = "B";
                dataGridView1.Columns[16].HeaderText = "BA";
                dataGridView1.Columns[17].HeaderText = "CR";
                dataGridView1.Columns[18].HeaderText = "M";
                dataGridView1.Columns[19].HeaderText = "R";
                dataGridView1.Columns[20].HeaderText = "C";
                dataGridView1.Columns[21].HeaderText = "RL";
                dataGridView1.Columns[22].HeaderText = "Jumlah";
                dataGridView1.Columns[23].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[24].HeaderText = "Tanggal Perbaikan";
                dataGridView1.Columns[25].HeaderText = "Diubah";
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

            string query = "SELECT * FROM pengiriman WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += "AND CAST(tanggal_pengiriman AS DATE) = @tgl";
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
            txtrod1.Clear();
            txtrod2.Clear();
            txtrod3.Clear();
            txtrod4.Clear();
            txtrod5.Clear();
            txtrod6.Clear();
            txtrod7.Clear();
            txtrod8.Clear();
            txtrod9.Clear();
            txtrod10.Clear();

            txtrod1.PlaceholderText = "4xxxx";
            txtrod2.PlaceholderText = "4xxxx";
            txtrod3.PlaceholderText = "4xxxx";
            txtrod4.PlaceholderText = "4xxxx";
            txtrod5.PlaceholderText = "4xxxx";
            txtrod6.PlaceholderText = "4xxxx";
            txtrod7.PlaceholderText = "4xxxx";
            txtrod8.PlaceholderText = "4xxxx";
            txtrod9.PlaceholderText = "4xxxx";
            txtrod10.PlaceholderText = "4xxxx";

            txtrod1.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod2.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod3.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod4.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod5.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod6.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod7.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod8.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod9.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod10.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            btnclear.Enabled = false;
            guna2Button2.Enabled = false;
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Clear();
                txt.PlaceholderText = "4xxxx";
                txt.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
                return;
            }

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM perbaikan_s WHERE nomor_rod = @A", conn);
                cmd.Parameters.AddWithValue("@A", txt.Text);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        
                    }
                    else
                    {
                        txt.Clear();
                        txt.PlaceholderText = "Data tidak ditemukan di Perbaikan";
                        txt.PlaceholderForeColor = Color.Red;
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
        }

        private void insertdata(List<Perbaikan> data)
        {
            string query = @"INSERT INTO pengiriman (tanggal_pengiriman, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah,
                                e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, s, d, b, ba, cr, m, r, c, rl, jumlah, tanggal_penerimaan,
                                tanggal_perbaikan,updated_at)
                                VALUES (getdate(), @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20,
                                @21, @22, @23, @24, getdate())";

            SqlConnection conn = Koneksi.GetConnection();

            SqlCommand cmd1 = new SqlCommand("SELECT tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, " +
                "e2_ers, e2_cst, e2_cstub, e2_jumlah,e3, s, d, b, ba, cr, m, r, c, rl,jumlah, tanggal_penerimaan  " +
                "FROM perbaikan_s WHERE nomor_rod IN (@A, @B, @C, @D, @E, @F, @G, @H, @I, @J)", conn);
            cmd1.Parameters.AddWithValue("@A", txtrod1.Text);
            cmd1.Parameters.AddWithValue("@B", txtrod2.Text);
            cmd1.Parameters.AddWithValue("@C", txtrod3.Text);
            cmd1.Parameters.AddWithValue("@D", txtrod4.Text);
            cmd1.Parameters.AddWithValue("@E", txtrod5.Text);
            cmd1.Parameters.AddWithValue("@F", txtrod6.Text);
            cmd1.Parameters.AddWithValue("@G", txtrod7.Text);
            cmd1.Parameters.AddWithValue("@H", txtrod8.Text);
            cmd1.Parameters.AddWithValue("@I", txtrod9.Text);
            cmd1.Parameters.AddWithValue("@J", txtrod10.Text);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Perbaikan
                    {
                        TanggalPerbaikan = reader["tanggal_perbaikan"] as DateTime?,
                        Shift = reader["shift"].ToString().Trim(),
                        NomorRod = reader["nomor_rod"] as int?,
                        Jenis = reader["jenis"].ToString().Trim(),
                        E1_Ers = reader["e1_ers"] as int?,
                        E1_Est = reader["e1_est"] as int?,
                        E1_Jumlah = reader["e1_jumlah"] as int?,
                        E2_Ers = reader["e2_ers"] as int?,
                        E2_Cst = reader["e2_cst"] as int?,
                        E2_Cstub = reader["e2_cstub"] as int?,
                        E2_Jumlah = reader["e2_jumlah"] as int?,
                        E3 = reader["e3"] as int?,
                        S = reader["s"] as int?,
                        D = reader["d"] as int?,
                        B = reader["b"] as int?,
                        Ba = reader["ba"] as int?,
                        Cr = reader["cr"] as int?,
                        M = reader["m"] as int?,
                        R = reader["r"] as int?,
                        C = reader["c"] as int?,
                        Rl = reader["rl"] as int?,
                        Jumlah = reader["jumlah"] as int?,
                        TanggalPenerimaan = reader["tanggal_penerimaan"] as DateTime?
                    });
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
            try
            {

                conn.Open();

                foreach (var item in data)
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@2", MainForm.Instance.lblshift.Text);
                        cmd.Parameters.AddWithValue("@3", item.NomorRod ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@4", item.Jenis ?? "");
                        cmd.Parameters.AddWithValue("@5", item.E1_Ers ?? 0);
                        cmd.Parameters.AddWithValue("@6", item.E1_Est ?? 0);
                        cmd.Parameters.AddWithValue("@7", item.E1_Jumlah ?? 0);
                        cmd.Parameters.AddWithValue("@8", item.E2_Ers ?? 0);
                        cmd.Parameters.AddWithValue("@9", item.E2_Cst ?? 0);
                        cmd.Parameters.AddWithValue("@10", item.E2_Cstub ?? 0);
                        cmd.Parameters.AddWithValue("@11", item.E2_Jumlah ?? 0);
                        cmd.Parameters.AddWithValue("@12", item.E3 ?? 0);
                        cmd.Parameters.AddWithValue("@13", item.S ?? 0);
                        cmd.Parameters.AddWithValue("@14", item.D ?? 0);
                        cmd.Parameters.AddWithValue("@15", item.B ?? 0);
                        cmd.Parameters.AddWithValue("@16", item.Ba ?? 0);
                        cmd.Parameters.AddWithValue("@17", item.Cr ?? 0);
                        cmd.Parameters.AddWithValue("@18", item.M ?? 0);
                        cmd.Parameters.AddWithValue("@19", item.R ?? 0);
                        cmd.Parameters.AddWithValue("@20", item.C ?? 0);
                        cmd.Parameters.AddWithValue("@21", item.Rl ?? 0);
                        cmd.Parameters.AddWithValue("@22", item.Jumlah ?? 0);
                        cmd.Parameters.AddWithValue("@23", item.TanggalPenerimaan);
                        cmd.Parameters.AddWithValue("@24", item.TanggalPerbaikan);

                        cmd.ExecuteNonQuery();
                    }
                    
                }
                SqlCommand cmd3 = new SqlCommand("DELETE FROM perbaikan_s WHERE nomor_rod IN (@a, @b, @c, @d, @e, @f, " +
                    "@g, @h, @i, @j)", conn);
                cmd3.Parameters.AddWithValue("@a", txtrod1.Text);
                cmd3.Parameters.AddWithValue("@b", txtrod2.Text);
                cmd3.Parameters.AddWithValue("@c", txtrod3.Text);
                cmd3.Parameters.AddWithValue("@d", txtrod4.Text);
                cmd3.Parameters.AddWithValue("@e", txtrod5.Text);
                cmd3.Parameters.AddWithValue("@f", txtrod6.Text);
                cmd3.Parameters.AddWithValue("@g", txtrod7.Text);
                cmd3.Parameters.AddWithValue("@h", txtrod8.Text);
                cmd3.Parameters.AddWithValue("@i", txtrod9.Text);
                cmd3.Parameters.AddWithValue("@j", txtrod10.Text);
                
                cmd3.ExecuteNonQuery();
                
                MessageBox.Show("Data Berhasil Dikirim!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tampil();
                setdefault();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Guna2TextBox[] txtRods = { txtrod1, txtrod2, txtrod3, txtrod4, txtrod5,
                           txtrod6, txtrod7, txtrod8, txtrod9, txtrod10 };

            bool adaIsi = txtRods.Any(t => !string.IsNullOrWhiteSpace(t.Text));

            if (!adaIsi)
            {
                MessageBox.Show("Isilah salah satu Nomor ROD yang ingin dikirim terlebih dahulu", "Warning");
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK) 
            { 
                insertdata(list);
                return;
            }
            else
            {

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

        private void Pengiriman_Load(object sender, EventArgs e)
        {
            tampil();
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            txtrod1.Focus();
            txtrod1.Focus();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            setdefault();
            btnclear.Enabled = false;
            guna2Button2.Enabled = false;
        }

        private void txtrod1_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod9_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod8_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod7_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod6_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod5_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod4_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod3_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod2_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod10_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }
    }
}
