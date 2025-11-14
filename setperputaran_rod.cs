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
    public partial class setperputaran_rod : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public setperputaran_rod()
        {
            InitializeComponent();
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnedit.PerformClick();
            }
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT hari FROM perputaran_rod"; 

                using (SqlDataAdapter ad = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    if (dt.Rows.Count > 0) 
                    {
                        string hari = dt.Rows[0]["hari"].ToString();
                        lblhari.Text = hari + " Hari";
                    }
                    else
                    {
                        lblhari.Text = "Data tidak ditemukan";
                    }
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
        }

        private void editdata()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE perputaran_rod SET hari = @hari, updated_at = GETDATE() WHERE id = 1", conn);
                cmd.Parameters.AddWithValue("@hari", txthari.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data berhasil diperbarui.", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                tampil();
                txthari.Clear();
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

        private void setperputaran_rod_Load(object sender, EventArgs e)
        {
            tampil();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if(txthari.Text == "")
            {
                MessageBox.Show("Jumlah Hari Tidak Boleh Kosong",
                               "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                editdata();
            }
        }
    }
}
