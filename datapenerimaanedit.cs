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
    public partial class datapenerimaanedit : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public static datapenerimaanedit Instance;

        public datapenerimaanedit()
        {
            InitializeComponent();
            Instance = this;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT * FROM penerimaan_e WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", historyPenerimaan.instance.noprimary);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Tanggal Penerimaan";
                    dataGridView1.Columns[2].HeaderText = "Shift";
                    dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                    dataGridView1.Columns[4].HeaderText = "Jenis";
                    dataGridView1.Columns[5].HeaderText = "Stasiun";
                    dataGridView1.Columns[6].HeaderText = "E1";
                    dataGridView1.Columns[7].HeaderText = "E2";
                    dataGridView1.Columns[8].HeaderText = "E3";
                    dataGridView1.Columns[9].HeaderText = "S";
                    dataGridView1.Columns[10].HeaderText = "D";
                    dataGridView1.Columns[11].HeaderText = "B";
                    dataGridView1.Columns[12].HeaderText = "BA";
                    dataGridView1.Columns[13].HeaderText = "CR";
                    dataGridView1.Columns[14].HeaderText = "M";
                    dataGridView1.Columns[15].HeaderText = "R";
                    dataGridView1.Columns[16].HeaderText = "C";
                    dataGridView1.Columns[17].HeaderText = "RL";
                    dataGridView1.Columns[18].HeaderText = "Jumlah";
                    dataGridView1.Columns[19].HeaderText = "Diubah";
                    dataGridView1.Columns[20].HeaderText = "Remaks";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load data: " + ex.Message);
            }
        }

        private void datapenerimaanedit_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
