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
                MessageBox.Show("isi woi");
            }
            else
            {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [user] WHERE id = @id AND password = @pw", conn);
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
                            this.Hide();
                            MainForm.Instance.lbluser.Text = name + " [Manajer]";

                        }
                        else if (level == "Operator")
                        {
                            this.Hide();
                            MainForm.Instance.setvisibletrue();
                            MainForm.Instance.lbluser.Text = name + " [Operator]";
                            MainForm.Instance.iconButton6.Visible = false;
                            
                        }
                        else if (level == "Supervisor")
                        {
                            this.Hide();
                            MainForm.Instance.lbluser.Text = name + " [Supervisor]";
                        } 
                        else if (level == "spare")
                        {

                        }
                    MainForm.Instance.loginstatus = true;
                    } 
                    else
                    {
                        MessageBox.Show("Salah Woi");
                        txtid.Clear();
                        txtpw.Clear();
                    }
             conn.Close();
            }
        }

    }
}
