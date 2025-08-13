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
            tampil();
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

        private void registertampil()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.setmin_Rb", conn))
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
                string query = "SELECT * FROM setmin_Rb ORDER BY updated_at DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Nama Tampilan";
                dataGridView1.Columns[2].HeaderText = "Min Stok";
                dataGridView1.Columns[3].HeaderText = "Diubah";
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

        private void cari()
        {
            try
            {
                string query = "SELECT * FROM setmin_Rb " +
                               "WHERE namaTampilan LIKE @keyword " +
                               "ORDER BY updated_at DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                ad.SelectCommand.Parameters.AddWithValue("@keyword", "%" + txtcari.Text + "%");

                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Nama Tampilan";
                dataGridView1.Columns[2].HeaderText = "Min Stok";
                dataGridView1.Columns[3].HeaderText = "Diubah";
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
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            cari();
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

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtnamatampilan.Text == "" || txtminstok.Text == "")
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE setmin_Rb SET namaTampilan = @nama, min_stok = @min_stok, updated_at = GETDATE() WHERE kode = @kode", conn);
                cmd.Parameters.AddWithValue("@kode", kodeprimary);
                cmd.Parameters.AddWithValue("@nama", txtnamatampilan.Text);
                cmd.Parameters.AddWithValue("@min_stok", txtminstok.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Diedit", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnbatal_Click(object sender, EventArgs e)
        {
            setdefault();
        }

        private void setmin_rb_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            registertampil();
        }
        
        private void setmin_rb_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }
    }
}
