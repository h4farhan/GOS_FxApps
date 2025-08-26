using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class loginform : Form
    {
        public string level;
        public string name;

        public static loginform login;
        public loginform()
        {
            InitializeComponent();
            login = this;
        }

        private void btnlogin_Click_1(object sender, EventArgs e)
        {
            if (txtid.Text == "" || txtpw.Text == "")
            {
                MessageBox.Show("Harap Isi Data Terlebih Dahulu!!", "Warning");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = Koneksi.GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @id AND password = @pw", conn);
                        cmd.Parameters.AddWithValue("@id", txtid.Text);
                        cmd.Parameters.AddWithValue("@pw", txtpw.Text);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            level = dr["lvl"].ToString();
                            name = dr["username"].ToString();

                            if (level == "Manajer")
                            {
                                MainForm.Instance.truemanajer();
                                MainForm.Instance.lbluser.Text = name + " [Manajer]";
                                MainForm.Instance.role = "Manajer";
                                this.Close();
                            }
                            else if (level == "Admin")
                            {
                                MainForm.Instance.trueadmin();
                                MainForm.Instance.lbluser.Text = name + " [Admin]";
                                MainForm.Instance.role = "Admin";
                                this.Close();
                            }
                            else if (level == "Operator Gudang")
                            {
                                MainForm.Instance.trueoperatorgudang();
                                MainForm.Instance.lbluser.Text = name + " [Operator Gudang]";
                                MainForm.Instance.role = "Operator Gudang";
                                this.Close();
                            }
                            else if (level == "Operator Perbaikan")
                            {
                                MainForm.Instance.trueoperatorperbaikan();
                                MainForm.Instance.lbluser.Text = name + " [Operator Perbaikan]";
                                MainForm.Instance.role = "Operator Perbaikan";
                                this.Close();
                            }
                            else if (level == "Operator Penerimaan/Pengiriman")
                            {
                                MainForm.Instance.trueoperatorpenerimaan();
                                MainForm.Instance.lbluser.Text = name + " [Operator Penerimaan/Pengiriman]";
                                MainForm.Instance.role = "Operator Penerimaan/Pengiriman";
                                this.Close();
                            }
                            MainForm.Instance.loginstatus = true;
                        }
                        else
                        {
                            MessageBox.Show("Id Dan Password Anda Salah!!", "Warning");
                            txtid.Clear();
                            txtpw.Clear();
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
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpw.PasswordChar = '\0';
            }
            else
            {
                txtpw.PasswordChar = '*';
            }
        }
    }
}
