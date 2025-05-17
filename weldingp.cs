using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class weldingp : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        
        public weldingp()
        {
            InitializeComponent();
        }

        private void getdata()
        {
            SqlCommand cmd = new SqlCommand("SELECT Stok,");
        }

        private int SafeParse(string input)
        {
            int hasil;
            if (int.TryParse(input, out hasil))
                return hasil;
            else
                return 0;
        }

        private void hitungrb()
        {
            int masuk = SafeParse(txtmasuk.Text);
            int keluar = SafeParse(txtkeluar.Text);
            int stok = SafeParse(lblstcokakhir.Text);

            int totalrb = masuk + stok - keluar;
            lblstoksekarang.Text = totalrb.ToString();
        }


        private void weldingp_Load(object sender, EventArgs e)
        {
            btnsimpan.Enabled = false;
        }

        private void btnhitung_Click(object sender, EventArgs e)
        {

        }
    }
}
