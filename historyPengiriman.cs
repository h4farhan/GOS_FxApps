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
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Drawing.Printing;

namespace GOS_FxApps
{
    public partial class historyPengiriman : Form
    {

        SqlConnection conn = Koneksi.GetConnection();

        public static historyPengiriman instance;

        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;

        public historyPengiriman()
        {
            InitializeComponent();
            instance = this;
        }

        private void registertampilpengiriman()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (var cmd = new SqlCommand("SELECT updated_at FROM dbo.pengiriman", conn))
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

                            registertampilpengiriman();
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
            string query = "SELECT COUNT(*) FROM pengiriman";
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
                SELECT *
                FROM pengiriman
                ORDER BY tanggal_pengiriman DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                }
                else
                {
                    query = $@"
                SELECT *
                {lastSearchWhere}
                ORDER BY tanggal_pengiriman DESC
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
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Pengiriman";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Diubah";

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

        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string inputRod = txtcari.Text.Trim();
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod) && !shiftValid)
            {
                MessageBox.Show("Silakan isi tanggal, nomor ROD, atau shift untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnreset.Enabled = false;
                return false;
            }

            isSearching = true;
            lastSearchCmd = new SqlCommand();
            lastSearchWhere = "FROM pengiriman WHERE 1=1 ";

            if (tanggal.HasValue)
            {
                lastSearchWhere += " AND CAST(tanggal_pengiriman AS DATE) = @tgl ";
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

        private void btncari_Click(object sender, EventArgs e)
        {
            cari();
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void historyPengiriman_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            HitungTotalData();
            tampil();
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            registertampilpengiriman();
        }

        private void btnreset_Click_1(object sender, EventArgs e)
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

        private void historyPengiriman_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
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
