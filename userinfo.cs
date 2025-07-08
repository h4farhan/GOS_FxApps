using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class userinfo : Form
    {
        public static userinfo Instance;

        public userinfo()
        {
            InitializeComponent();
            Instance = this;
        }

        private void profil()
        {
            string iduser = loginform.login.txtid.Text.Trim();

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                string query = "SELECT * FROM users WHERE id = @iduser";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@iduser", iduser);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lbluser.Text = "Halo, " + dr["username"].ToString();
                    lbljabatan.Text = dr["lvl"].ToString();
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            MainForm.Instance.loginstatus = false;
            MainForm.Instance.setvisiblefalse();
            MainForm.Instance.SwitchPanel(new Dashboard());
            MainForm.Instance.lbluser.Text = "";
            this.Close();
        }
        private void userinfo_Load(object sender, EventArgs e)
        {
            profil(); 
        }
    }
}
