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
using static System.Net.Mime.MediaTypeNames;
using System.Management.Instrumentation;

namespace GOS_FxApps
{
    public partial class Pengiriman : Form
    {
        private List<Perbaikan> list = new List<Perbaikan>();
        SqlConnection conn = Koneksi.GetConnection();

        Guna.UI2.WinForms.Guna2TextBox[] txtrods;
        int noprimary;
        public class Perbaikan
        {
            public int? NomorRod { get; set; }

            public Perbaikan() { }

            public Perbaikan(int? nomorRod)
            {
                NomorRod = nomorRod;
            }
        }

        public Pengiriman()
        {
            InitializeComponent();
            txtrods = new Guna.UI2.WinForms.Guna2TextBox[]
            {
                txtrod1, txtrod2, txtrod3, txtrod4, txtrod5,
                txtrod6, txtrod7, txtrod8, txtrod9, txtrod10
            };
        }

        private void registertampil()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.pengiriman", conn))
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
                string query = "SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, bac, nba, ba, ba1, cr, m, r, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks FROM perbaikan_s ORDER BY tanggal_perbaikan DESC";
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
                dataGridView1.Columns[21].HeaderText = "CR";
                dataGridView1.Columns[22].HeaderText = "M";
                dataGridView1.Columns[23].HeaderText = "R";
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
            string inputRod = txtcari.Text.Trim();
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

        private void insertdata()
        {
            List<Perbaikan> list = new List<Perbaikan>();

            SqlConnection conn = Koneksi.GetConnection();

            SqlCommand cmd1 = new SqlCommand(
                "SELECT nomor_rod FROM perbaikan_s WHERE nomor_rod IN (@A,@B,@C,@D,@E,@F,@G,@H,@I,@J)", conn);

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
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Perbaikan
                        {
                            NomorRod = reader["nomor_rod"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["nomor_rod"])
                        });
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                conn.Close();
            }

            if (list.Count == 0)
            {
                MessageBox.Show("Tidak ada data yang cocok dengan nomor ROD.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string queryInsertp = @"INSERT INTO pengiriman (no, tanggal_pengiriman, shift, nomor_rod, updated_at, remaks) 
                           VALUES (@no, @tanggal, @shift, @nomor_rod, @diubah, @remaks)";

            string queryInsertm = @"INSERT INTO pengiriman_m (no, tanggal_pengiriman, shift, nomor_rod, updated_at, remaks) 
                           VALUES (@no, @tanggal, @shift, @nomor_rod, @diubah, @remaks)";

            try
            {
                conn.Open();

                foreach (var item in list)
                {
                    using (SqlCommand cmdp = new SqlCommand(queryInsertp, conn))
                    {
                        cmdp.Parameters.AddWithValue("@no", noprimary);
                        cmdp.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                        cmdp.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                        cmdp.Parameters.AddWithValue("@nomor_rod", item.NomorRod ?? (object)DBNull.Value);
                        cmdp.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        cmdp.Parameters.AddWithValue("@remaks", loginform.login.name);

                        cmdp.ExecuteNonQuery();
                    }
                    using (SqlCommand cmdm = new SqlCommand(queryInsertm, conn))
                    {
                        cmdm.Parameters.AddWithValue("@no", noprimary);
                        cmdm.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                        cmdm.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                        cmdm.Parameters.AddWithValue("@nomor_rod", item.NomorRod ?? (object)DBNull.Value);
                        cmdm.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        cmdm.Parameters.AddWithValue("@remaks", loginform.login.name);

                        cmdm.ExecuteNonQuery();
                    }
                }

                using (SqlCommand cmd3 = new SqlCommand(
                    "DELETE FROM perbaikan_s WHERE nomor_rod IN (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j)", conn))
                {
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
                }

                MessageBox.Show("Data Berhasil Dikirim!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tampil();
                setdefault();
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.\n",
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                guna2Button2.PerformClick();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Guna2TextBox[] txtRods = {
        txtrod1, txtrod2, txtrod3, txtrod4, txtrod5,
        txtrod6, txtrod7, txtrod8, txtrod9, txtrod10
    };

            bool adaIsi = txtRods.Any(t => !string.IsNullOrWhiteSpace(t.Text));

            if (!adaIsi)
            {
                MessageBox.Show("Isilah salah satu Nomor ROD yang ingin dikirim terlebih dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin dengan data Anda?",
                "Konfirmasi",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.OK)
            {
                insertdata();
                btnclear.Enabled = false;
                guna2Button2.Enabled = false;
            }
        }

        private void Pengiriman_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            tampil();
            txtrod1.Focus();
            txtrod1.Focus();
            registertampil();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            setdefault();
            btnclear.Enabled = false;
            guna2Button2.Enabled = false;
            dataGridView1.ClearSelection();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string value = row.Cells["nomor_rod"].Value.ToString();
                noprimary = Convert.ToInt32(row.Cells["no"].Value);

                foreach (var txt in txtrods)
                {
                    if (txt.Text == value)
                    {
                        MessageBox.Show("Nomor ROD ini sudah dimasukkan sebelumnya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                for (int i = 0; i < txtrods.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(txtrods[i].Text))
                    {
                        txtrods[i].Text = value;
                        return;
                    }
                }
                MessageBox.Show("Maksimal pengiriman hanya 10 ROD. Kirim terlebih dahulu dan coba lagi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Pengiriman_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            cari();
        }
    }
}
