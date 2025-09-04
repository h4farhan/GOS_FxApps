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
    public partial class datapengirimanedit : Form
    {
        public static datapengirimanedit Instance;

        public datapengirimanedit()
        {
            InitializeComponent();
            Instance = this;
        }

        //private void LoadData()
        //{
        //    try
        //    {
        //        using (SqlConnection conn = Koneksi.GetConnection())
        //        {
        //            string query = "SELECT * FROM pengiriman_e WHERE no = @no";
        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@no", historyPengiriman.instance.noprimary);

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);

        //            dataGridView1.DataSource = dt;
        //            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
        //            dataGridView1.RowTemplate.Height = 35;
        //            dataGridView1.Columns[0].Visible = false;
        //            dataGridView1.Columns[1].HeaderText = "Tanggal Pengiriman";
        //            dataGridView1.Columns[2].HeaderText = "Shift";
        //            dataGridView1.Columns[3].HeaderText = "Nomor ROD";
        //            dataGridView1.Columns[4].HeaderText = "Diubah";
        //            dataGridView1.Columns[5].HeaderText = "Remaks";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error load data: " + ex.Message);
        //    }
        //}

        private void datapengirimanedit_Load(object sender, EventArgs e)
        {
            //LoadData();
        }
    }
}
