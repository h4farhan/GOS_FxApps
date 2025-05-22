using System;
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
            tampil();
            datecari.Checked = false;
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM pengiriman";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.");
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
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat pencarian: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return dt.Rows.Count > 0;
            }
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender; 

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
                        txt.PlaceholderText = "Data Tidak Ditemukan";
                        txt.PlaceholderForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                                tanggal_perbaikan)
                                VALUES (getdate(), @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20,
                                @21, @22, @23, @24)";

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
                MessageBox.Show("Data ditemukan");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                tampil();
                MessageBox.Show("Data Berhasil Dikirim");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            insertdata(list);
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
                    infocari = false;
                    btncari.Text = "Cari";
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
    }
}
