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

                            switch (level)
                            {
                                case "Developer":
                                    MainForm.Instance.truemanajer();
                                    break;
                                case "Manajer":
                                    MainForm.Instance.truemanajer();
                                    break;
                                case "Admin":
                                    MainForm.Instance.trueadmin();
                                    break;
                                case "Operator Gudang":
                                    MainForm.Instance.trueoperatorgudang();
                                    break;
                                case "Operator":
                                    MainForm.Instance.trueoperator();
                                    break;
                                case "Foreman":
                                    MainForm.Instance.trueforeman();
                                    break;
                                case "Asisten Foreman":
                                    MainForm.Instance.trueforeman();
                                    break;
                                default:
                                    MessageBox.Show("Level tidak dikenali!", "Warning");
                                    txtid.Clear();
                                    txtpw.Clear();
                                    return;
                            }

                            MainForm.Instance.lbluser.Text = name + " [" + level + "]";
                            MainForm.Instance.role = level;
                            MainForm.Instance.loginstatus = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Id Dan Password Anda Salah!!", "Warning");
                            txtid.Clear();
                            txtpw.Clear();
                        }

                        //if (dr.Read())
                        //{
                        //    string level = dr["lvl"].ToString();
                        //    string name = dr["username"].ToString();

                        //    string allowedRole = ""; //Belum ditentukan

                        //    if (level == allowedRole)
                        //    {
                        //        switch (allowedRole)
                        //        {
                        //            case "Manajer":
                        //                MainForm.Instance.truemanajer();
                        //                break;
                        //            case "Admin":
                        //                MainForm.Instance.trueadmin();
                        //                break;
                        //            case "Operator Gudang":
                        //                MainForm.Instance.trueoperatorgudang();
                        //                break;
                        //            case "Operator Perbaikan":
                        //                MainForm.Instance.trueoperatorperbaikan();
                        //                break;
                        //            case "Operator Penerimaan/Pengiriman":
                        //                MainForm.Instance.trueoperatorpenerimaan();
                        //                break;
                        //        }

                        //        MainForm.Instance.lbluser.Text = name + " [" + allowedRole + "]";
                        //        MainForm.Instance.role = allowedRole;
                        //        MainForm.Instance.loginstatus = true;
                        //        this.Close();
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Akses hanya untuk " + allowedRole + "!", "Warning");
                        //        txtid.Clear();
                        //        txtpw.Clear();
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Id Dan Password Anda Salah!!", "Warning");
                        //    txtid.Clear();
                        //    txtpw.Clear();
                        //}
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
