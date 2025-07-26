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

namespace GOS_FxApps
{
    public partial class formbuttman : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        bool infocari = false;
        int noprimary;

        public formbuttman()
        {
            InitializeComponent();
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT no,tanggal,shift,butt_ratio,man_power,updated_at FROM kondisiROD ORDER BY tanggal DESC, updated_at DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Butt Ratio";
                dataGridView1.Columns[4].HeaderText = "Man Power";
                dataGridView1.Columns[5].HeaderText = "Diubah";
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
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!tanggal.HasValue && !shiftValid)
            {
                MessageBox.Show("Silakan isi tanggal atau shift untuk melakukan pencarian.", "Warning");
                return false;
            }

            DataTable dt = new DataTable();
            string query = "SELECT * FROM kondisiROD WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += " AND CAST(tanggal AS DATE) = @tgl";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (shiftValid)
                {
                    query += " AND shift = @shift";
                    cmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem.ToString());
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
                catch (Exception)
                {
                    MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                return dt.Rows.Count > 0;
            }
        }

        private void editdata()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE kondisiROD SET tanggal = @tgl, shift = @shift, butt_ratio = @butt, man_power = @man, updated_at = GETDATE() WHERE no = @no", conn);
                cmd.Parameters.AddWithValue("@no", noprimary);
                cmd.Parameters.AddWithValue("@tgl", date.Value);
                cmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem);
                cmd.Parameters.AddWithValue("@butt", txtbutt.Text);
                cmd.Parameters.AddWithValue("@man", txtman.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data pemakaian berhasil diedit.", "Warning");
                tampil();
                btnsimpan.Text = "Simpan Data";
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

        private void simpandata()
        {

            try
            {
                conn.Open();
                using (SqlCommand cmdcekkode = new SqlCommand("SELECT tanggal,shift FROM kondisiROD WHERE tanggal = @tgl AND shift = @shift", conn))
                {
                    cmdcekkode.Parameters.AddWithValue("@tgl", date.Value);
                    cmdcekkode.Parameters.AddWithValue("@shift", cmbshift.SelectedItem);
                    using (SqlDataReader dr = cmdcekkode.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            MessageBox.Show("Data di Tanggal dan di Shift ini sudah ada", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand("INSERT INTO kondisiROD (tanggal, shift, butt_ratio, man_power, updated_at) VALUES(@tgl,@shift,@butt,@man,GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@tgl", date.Value);
                    cmd.Parameters.AddWithValue("@shift", cmbshift.SelectedItem);
                    cmd.Parameters.AddWithValue("@butt", txtbutt.Text);
                    cmd.Parameters.AddWithValue("@man", txtman.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Data Berhasil Disimpan.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();
                tampil();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif." + ex.Message,
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

        private void setdefault()
        {
            txtman.Clear();
            txtbutt.Clear();
            cmbshift.StartIndex = -1;
            date.Value = DateTime.Now.Date;
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if (cmbshift.SelectedIndex == -1 || txtbutt.Text == "" || txtman.Text == "") 
            {
                MessageBox.Show("Harap lengkapi data terlebih dahulu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(btnsimpan.Text == "Simpan Data")
            {
                simpandata();
                setdefault();
                btnbatal.Enabled = false;
                btnsimpan.Enabled = false;
            }
            else if (btnsimpan.Text == "Edit Data")
            {
                editdata();
                setdefault();
                btnbatal.Enabled = false;
                btnsimpan.Enabled = false;
                noprimary = 0;
            }

        }

        private void formbuttman_Load(object sender, EventArgs e)
        {
            tampil();
            date.Value = DateTime.Now.Date;
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
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

                cbShift.StartIndex = 0;
                datecari.Checked = false;
            }
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            setdefault();
            btnbatal.Enabled=false;
            btnsimpan.Enabled=false;
        }

        private void cmbshift_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnbatal.Enabled = true;
            btnsimpan.Enabled = true;
        }

        private void txtman_TextChanged(object sender, EventArgs e)
        {
            btnbatal.Enabled = true;
            btnsimpan.Enabled = true;
        }

        private void txtbutt_TextChanged(object sender, EventArgs e)
        {
            btnbatal.Enabled = true;
            btnsimpan.Enabled = true;
        }
    }
}
