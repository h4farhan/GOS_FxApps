using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class infoalluser : Form
    {
        string iduser = null;
        string jabatan = null;

        SqlConnection conn = Koneksi.GetConnection();

        public infoalluser()
        {
            InitializeComponent();
            tampil();
        }

        private void registertampil()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.users", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                tampil();
                                registertampil();
                            }));
                        }
                        else
                        {
                            this.HandleCreated += (s2, e2) =>
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    tampil();
                                    registertampil();
                                }));
                            };
                        }
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
                string query = "SELECT * FROM users ORDER BY updated_at DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(34, 34, 36);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Id User";
                dataGridView1.Columns[2].HeaderText = "Nama Lengkap";
                dataGridView1.Columns[3].HeaderText = "Password";
                dataGridView1.Columns[4].HeaderText = "Jabatan";
                dataGridView1.Columns[5].HeaderText = "Terdaftar";
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
                conn.Open();
                string query = "SELECT * FROM users " +
                               "WHERE username LIKE @cari OR lvl LIKE @cari " +
                               "ORDER BY created_at DESC";

                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                ad.SelectCommand.Parameters.AddWithValue("@cari", "%" + txtcari.Text + "%");

                DataTable dt = new DataTable();
                ad.Fill(dt);
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

        }

        private void setdefault()
        {
            iduser = null;
            btnbatal.Enabled = false;
            btnhapus.Enabled = false;
            dataGridView1.ClearSelection();
        }

        private bool getdatano()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 lvl FROM users WHERE lvl = @lvl", conn);
                cmd.Parameters.AddWithValue("@lvl", jabatan);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string level = reader["lvl"].ToString();
                        if (level.Equals("Manajer", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Data manajer tidak bisa diedit.",
                                            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
                return true; 
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        private void infoalluser_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            registertampil();
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            cari();
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            setdefault();
        }

        private void btnhapus_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin hapus permanen data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                if (iduser == null)
                {
                    MessageBox.Show("Harap pilih data terlebih dahulu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("id", iduser);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Dihapus!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                jabatan = row.Cells["lvl"].Value.ToString();

                if (!getdatano()) return;
                iduser = row.Cells["id"].Value.ToString();
                btnbatal.Enabled = true;
                btnhapus.Enabled = true;
            }
        }

        private void infoalluser_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }
    }
}
