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
    public partial class dataperbaikanedit : Form
    {
        public static dataperbaikanedit Instance;

        public dataperbaikanedit()
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
                    string query = "SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, ba, ba1, cr, m, r, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks FROM perbaikan_e WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", historyPerbaikan.instance.noprimary);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView1.RowTemplate.Height = 35;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Tanggal Perbaikan";
                    dataGridView1.Columns[2].HeaderText = "Shift";
                    dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                    dataGridView1.Columns[4].HeaderText = "Jenis";
                    dataGridView1.Columns[5].HeaderText = "E1 Ers";
                    dataGridView1.Columns[6].HeaderText = "E1 Est";
                    dataGridView1.Columns[7].HeaderText = "E1 Jumlah";
                    dataGridView1.Columns[8].HeaderText = "E2 Ers";
                    dataGridView1.Columns[9].HeaderText = "E2 Cst";
                    dataGridView1.Columns[10].HeaderText = "E2 Cstub";
                    dataGridView1.Columns[11].HeaderText = "E2 Jumlah";
                    dataGridView1.Columns[12].HeaderText = "E3";
                    dataGridView1.Columns[13].HeaderText = "E4";
                    dataGridView1.Columns[14].HeaderText = "S";
                    dataGridView1.Columns[15].HeaderText = "D";
                    dataGridView1.Columns[16].HeaderText = "B";
                    dataGridView1.Columns[17].HeaderText = "BA";
                    dataGridView1.Columns[18].HeaderText = "BA-1";
                    dataGridView1.Columns[19].HeaderText = "CR";
                    dataGridView1.Columns[20].HeaderText = "M";
                    dataGridView1.Columns[21].HeaderText = "R";
                    dataGridView1.Columns[22].HeaderText = "C";
                    dataGridView1.Columns[23].HeaderText = "RL";
                    dataGridView1.Columns[24].HeaderText = "Jumlah";
                    dataGridView1.Columns[25].HeaderText = "Tanggal Penerimaan";
                    dataGridView1.Columns[26].HeaderText = "Diubah";
                    dataGridView1.Columns[27].HeaderText = "Remaks";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load data: " + ex.Message);
            }
        }

        private void dataperbaikanedit_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
