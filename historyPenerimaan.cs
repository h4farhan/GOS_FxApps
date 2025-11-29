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
using System.Drawing.Printing;

namespace GOS_FxApps
{
    public partial class historyPenerimaan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public int noprimary;
        public string nomorrod;

        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;


        public static historyPenerimaan instance;

        public historyPenerimaan()
        {
            InitializeComponent();
            instance = this;
        }

        private void registertampilpenerimaan()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (var cmd = new SqlCommand("SELECT updated_at FROM dbo.penerimaan_p", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!isSearching)
                            {
                                HitungTotalData();
                                currentPage = 1;
                                tampil();
                            }
                            else
                            {
                                int oldTotal = searchTotalRecords;
                                HitungTotalDataPencarian();
                                if (searchTotalRecords > oldTotal)
                                {
                                    tampil();
                                }
                            }

                            registertampilpenerimaan();
                        }));
                    }
                };

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) { }
                }
            }
        }

        private void HitungTotalData()
        {
            string query = "SELECT COUNT(*) FROM penerimaan_p";
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            totalRecords = (int)cmd.ExecuteScalar();
            conn.Close();

            totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }

        private void HitungTotalDataPencarian()
        {
            string countQuery = "SELECT COUNT(*) " + lastSearchWhere;

            lastSearchCmd.CommandText = countQuery;
            lastSearchCmd.Connection = conn;

            conn.Open();
            searchTotalRecords = (int)lastSearchCmd.ExecuteScalar();
            conn.Close();

            totalPages = (int)Math.Ceiling(searchTotalRecords / (double)pageSize);
        }


        private void tampil()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                string query;

                if (!isSearching)
                {
                    query = $@"
                SELECT no, tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, e1, e2, e3, s, d, b, ba, r, m, cr, c, rl, jumlah, updated_at, remaks, catatan
                FROM penerimaan_p
                ORDER BY tanggal_penerimaan DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                }
                else
                {
                    query = $@"
                SELECT no, tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, e1, e2, e3, s, d, b, ba, r, m, cr, c, rl, jumlah, updated_at, remaks, catatan
                {lastSearchWhere}
                ORDER BY tanggal_penerimaan DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";

                    foreach (SqlParameter p in lastSearchCmd.Parameters)
                        cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                }

                cmd.CommandText = query;

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

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
                dataGridView1.Columns[13].HeaderText = "R";
                dataGridView1.Columns[14].HeaderText = "M";
                dataGridView1.Columns[15].HeaderText = "CR";
                dataGridView1.Columns[16].HeaderText = "C";
                dataGridView1.Columns[17].HeaderText = "RL";
                dataGridView1.Columns[18].HeaderText = "Jumlah";
                dataGridView1.Columns[19].HeaderText = "Diubah";
                dataGridView1.Columns[20].HeaderText = "Remaks";
                dataGridView1.Columns[21].HeaderText = "Catatan";

                if (!isSearching)
                {
                    lbljumlahdata.Text = "Jumlah data: " + totalRecords;
                }
                else
                {
                    lbljumlahdata.Text = "Hasil pencarian: " + searchTotalRecords;
                }

                lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

                btnleft.Enabled = currentPage > 1;
                btnright.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string inputRod = txtcari.Text.Trim();
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod) && !shiftValid)
            {
                MessageBox.Show("Silakan isi tanggal, nomor ROD, atau shift untuk melakukan pencarian.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            isSearching = true;
            lastSearchCmd = new SqlCommand();
            lastSearchWhere = "FROM penerimaan_p WHERE 1=1 ";

            if (tanggal.HasValue)
            {
                lastSearchWhere += " AND CAST(tanggal_penerimaan AS DATE) = @tgl ";
                lastSearchCmd.Parameters.AddWithValue("@tgl", tanggal.Value);
            }

            if (!string.IsNullOrEmpty(inputRod))
            {
                lastSearchWhere += " AND nomor_rod LIKE @rod ";
                lastSearchCmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");
            }

            if (shiftValid)
            {
                lastSearchWhere += " AND shift = @shift ";
                lastSearchCmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem.ToString());
            }

            HitungTotalDataPencarian();   
            currentPage = 1;             
            tampil();                    

            btnreset.Enabled = true;
            return true;
        }

        private void btncari_Click_1(object sender, EventArgs e)
        {
            cari();
        }

        private void historyPenerimaan_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            HitungTotalData();
            tampil();
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            registertampilpenerimaan();
        }


        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            isSearching = false;

            txtcari.Text = "";
            cbShift.SelectedIndex = 0;
            datecari.Checked = false;

            btnreset.Enabled = false;

            HitungTotalData();
            currentPage = 1;
            tampil();
        }

        private void historyPenerimaan_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (dataGridView1.Columns.Contains("no") && dataGridView1.Columns.Contains("nomor_rod"))
                {
                    noprimary = Convert.ToInt32(row.Cells["no"].Value);
                    nomorrod = row.Cells["nomor_rod"].Value.ToString();

                    datapenerimaanedit data = new datapenerimaanedit();
                    data.lbljudul.Text = "Nomor ROD = " + nomorrod;
                    data.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Kolom 'no' atau 'nomor_rod' tidak ditemukan di DataGridView.",
                                    "Kesalahan Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                tampil();
            }
        }

        private void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                tampil();
            }
        }
    }
}
