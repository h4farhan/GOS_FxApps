using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class registrasi : Form
    {
        public registrasi()
        {
            InitializeComponent();
        }

        private void setdefault()
        {
            txtid.Clear();
            txtpass.Clear();
            txtusername.Clear();
            cmblevel.StartIndex = -1;
        }

        private void btnregis_Click(object sender, EventArgs e)
        {

            if (cmblevel.SelectedIndex == -1 || txtid.Text == "" || txtpass.Text == "" | txtusername.Text == "")
            { 
                MessageBox.Show("Harap lengkapi data terlebih dahulu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection conn = Koneksi.GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmdcheck = new SqlCommand("SELECT id FROM users WHERE id = @id", conn);
                        cmdcheck.Parameters.AddWithValue("@id", txtid.Text);
                        SqlDataReader dr = cmdcheck.ExecuteReader();
                        if (dr.Read())
                        {
                            MessageBox.Show("Id User sudah terdaftar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        dr.Close();

                        SqlCommand cmd = new SqlCommand("INSERT INTO users (id, username, password, lvl, updated_at) VALUES (@id, @username, @password, @lvl, getdate())", conn);
                        cmd.Parameters.AddWithValue("@id", txtid.Text);
                        cmd.Parameters.AddWithValue("@username", txtusername.Text);
                        cmd.Parameters.AddWithValue("@password", txtpass.Text);
                        cmd.Parameters.AddWithValue("@lvl", cmblevel.SelectedItem);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Akun Berhasil Ditambahkan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Form info = new infoalluser();
            info.ShowDialog();
            this.Close();   
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpass.PasswordChar = '\0';
            }
            else
            {
                txtpass.PasswordChar = '*';
            }
        }
    }
}
