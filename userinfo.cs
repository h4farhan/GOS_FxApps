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
using System.Data.SqlClient;

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
            try
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
                        lbljabatan.Text = "[" + dr["lvl"].ToString() + "]";
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

        private void label2_Click(object sender, EventArgs e)
        {
            MainForm.Instance.loginstatus = false;
            MainForm.Instance.setvisiblefalse();
            MainForm.Instance.ResetButtonColors();
            MainForm.Instance.ResetColorContainer();

            MainForm.Instance.SwitchPanel(new Dashboard());
            MainForm.Instance.dashboardButton.FillColor = Color.FromArgb(52, 52, 57);
            MainForm.Instance.dashboardButton.ForeColor = Color.White;
            MainForm.Instance.lbluser.Text = "";
            MainForm.Instance.entryContainer.Size = MainForm.Instance.defaultentrycontainer;
            MainForm.Instance.EditContainer.Size = MainForm.Instance.defaulteditcontainer;
            MainForm.Instance.historycontainer.Size = MainForm.Instance.defaulhistorycontainer;
            MainForm.Instance.role = null;
            formnotifikasi.Instance.btntiga.Visible = false;
            this.Close();
        }
        private void userinfo_Load(object sender, EventArgs e)
        {
            profil(); 
            if (lbljabatan.Text == "[Manajer]")
            {
                lbltambahakun.Visible = true;
            }
            else
            {
                lbltambahakun.Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form regis = new registrasi();
            regis.Show();
            this.Close();
        }
    }
}
