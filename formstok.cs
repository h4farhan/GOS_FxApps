using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOS_FxApps
{
    public partial class formstok : Form
    {
        SqlConnection conn = Koneksi.GetConnection();
        public formstok()
        {
            InitializeComponent();
        }

        private void kodebarang()
        {
            long hitung;
            string urutan;
            SqlDataReader dr;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select kode_barang from tblmaterial where kode_barang in(select max(kode_barang) from tblmaterial order by desc", conn);
            dr = cmd.ExecuteReader();   
            dr.Read();
            if (dr.HasRows)
            {
                hitung = Convert.ToInt64(dr[0].ToString().Substring(dr["kode_barang"].ToString().Length - 3, 3)) + 1;
                string kodeurutan = "171101" + hitung;
                urutan = "MART" + kodeurutan.Substring(kodeurutan.Length - 3, 3);
            }
            else 
            {
                urutan = "MART171101001";
            }
            dr.Close();
            txtkodebarang.Text = urutan;
            conn.Close();
        }

        private void btnhitung_Click(object sender, EventArgs e)
        {

        }

        private void formstok_Load(object sender, EventArgs e)
        {
            kodebarang();
        }
    }
}
