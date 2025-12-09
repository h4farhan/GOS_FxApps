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
namespace GOS_FxApps
{
    public partial class setmin_rb : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        string kodeprimary = null; 

        public setmin_rb()
        {
            InitializeComponent();
        }

        private void setdefault()
        {
            txtnamatampilan.Clear();
            txtminstok.Clear();
            kodeprimary = null ;
            btnbatal.Enabled = false;
            btnedit.Enabled = false;
            txtnamatampilan.Enabled = false;
            txtminstok.Enabled = false;
            dataGridView1.ClearSelection();
        }

        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                switch (table)
                {
                    case "setmin_Rb":
                        await Tampil();
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Gagal Realtime");
            }
        }

        private void StyleDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
            dataGridView1.ReadOnly = true;

            if (dataGridView1.Columns.Count >= 4)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Nama Tampilan";
                dataGridView1.Columns[2].HeaderText = "Min Stok";
                dataGridView1.Columns[3].HeaderText = "Diubah";
            }
        }

        private async Task Tampil()
        {
            try
            {
                string query = "SELECT * FROM setmin_Rb ORDER BY updated_at DESC";
                DataTable dt = await Task.Run(() =>
                {
                    DataTable temp = new DataTable();
                    using (SqlDataAdapter ad = new SqlDataAdapter(query, conn))
                    {
                        ad.Fill(temp);
                    }
                    return temp;
                });

                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.DataSource = dt;
                StyleDataGridView();
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi database gagal.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal tampil data: {ex.Message}");
            }
        }

        private async Task cari(string keyword)
        {
            try
            {
                string query = "SELECT * FROM setmin_Rb WHERE namaTampilan LIKE @keyword ORDER BY updated_at DESC";
                DataTable dt = await Task.Run(() =>
                {
                    DataTable temp = new DataTable();
                    using (SqlDataAdapter ad = new SqlDataAdapter(query, conn))
                    {
                        ad.SelectCommand.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                        ad.Fill(temp);
                    }
                    return temp;
                });

                dataGridView1.DataSource = dt;
                StyleDataGridView();
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal cari data: {ex.Message}");
            }
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void txtcari_TextChanged(object sender, EventArgs e)
        {
            await cari(txtcari.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                kodeprimary = row.Cells["kode"].Value.ToString();
                txtnamatampilan.Text = row.Cells["namaTampilan"].Value.ToString();
                txtminstok.Text = row.Cells["min_stok"].Value.ToString();
                txtnamatampilan.Enabled = true;
                txtminstok.Enabled = true;
                txtnamatampilan.Focus();
                btnedit.Enabled = true;
                btnbatal.Enabled = true;
            }
        }

        private async void btnedit_Click(object sender, EventArgs e)
        {
            if (txtnamatampilan.Text == "" || txtminstok.Text == "")
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE setmin_Rb SET namaTampilan = @nama, min_stok = @min_stok, updated_at = @diubah WHERE kode = @kode", conn);
                    cmd.Parameters.AddWithValue("@kode", kodeprimary);
                    cmd.Parameters.AddWithValue("@nama", txtnamatampilan.Text);
                    cmd.Parameters.AddWithValue("@min_stok", txtminstok.Text);
                    cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diedit", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await Tampil();
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

        private void btnbatal_Click(object sender, EventArgs e)
        {
            setdefault();
        }

        private async void setmin_rb_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;
            await Tampil();
        }
        
        private void setmin_rb_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }
    }
}
