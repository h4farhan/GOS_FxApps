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
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class loginform : Form
    {
        public string level;
        public string name;

        SqlConnection conn = Koneksi.GetConnection();

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
                MessageBox.Show("Harap Isi Data Terlebih Dahulu!!");
            }
            else
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
                            MainForm.Instance.setvisibletrue();
                            MainForm.Instance.lbluser.Text = name + " [Manajer]";
                            this.Close();
                        }
                        else if (level == "Operator")
                        { 
                            MainForm.Instance.setvisibletrue();
                            MainForm.Instance.lbluser.Text = name + " [Operator]";
                            this.Close();
                        }
                        else if (level == "Supervisor")
                        {    
                            MainForm.Instance.lbluser.Text = name + " [Supervisor]";
                            this.Close();
                        } 
                        else if (level == "spare")
                        {

                        }
                    MainForm.Instance.loginstatus = true;
                    } 
                    else
                    {
                        MessageBox.Show("Id Dan Password Anda Salah, SilahkaN Coba Kembali!!");
                        txtid.Clear();
                        txtpw.Clear();
                    }
             conn.Close();
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
