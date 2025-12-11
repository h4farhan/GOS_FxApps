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
using System.Windows.Input;
using System.Security.AccessControl;

namespace GOS_FxApps
{
    public partial class historyWelding : Form
    {
        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;
        private bool isEditing = false;

        public historyWelding()
        {
            InitializeComponent();
        }

        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                if (isEditing) return;

                switch (table)
                {
                    case "Rb_Stok":
                        if (!isSearching)
                        {
                            await HitungTotalData();
                            currentPage = 1;
                            await tampil();
                        }
                        else
                        {
                            int oldTotal = searchTotalRecords;
                            await HitungTotalDataPencarian();
                            if (searchTotalRecords > oldTotal)
                                await tampil();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal realtime");
                return;
            }
        }

        private async Task HitungTotalData()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Rb_Stok";
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        totalRecords = (int)await cmd.ExecuteScalarAsync();
                    }
                }
                totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal hitungtotaldata");
                return;
            }
        }

        private async Task HitungTotalDataPencarian()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lastSearchWhere))
                {
                    searchTotalRecords = 0;
                    totalPages = 0;
                    return;
                }

                string countQuery = "SELECT COUNT(*) " + lastSearchWhere;
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (var cmd = new SqlCommand(countQuery, conn))
                    {
                        if (lastSearchCmd?.Parameters.Count > 0)
                        {
                            foreach (SqlParameter p in lastSearchCmd.Parameters)
                                cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                        }

                        searchTotalRecords = (int)await cmd.ExecuteScalarAsync();
                    }
                }

                totalPages = (int)Math.Ceiling(searchTotalRecords / (double)pageSize);
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal hitungtotaldatacari");
                return;
            }
        }

        private async Task tampil()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    string query;

                    if (!isSearching)
                {
                    query = $@"
                SELECT *
                FROM Rb_Stok
                ORDER BY tanggal DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                }
                else
                {
                    query = $@"
                SELECT *
                {lastSearchWhere}
                ORDER BY tanggal DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";

                    foreach (SqlParameter p in lastSearchCmd.Parameters)
                        cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                }

                    cmd.CommandText = query;

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(dt);
                    }

                    if (dataGridView1.InvokeRequired)
                    {
                        dataGridView1.Invoke(new Action(() =>
                        {
                            UpdateGrid(dt);
                        }));
                    }
                    else
                    {
                        UpdateGrid(dt);
                    }
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal tampil");
                return;
            }
        }

        private void UpdateGrid(DataTable dt)
        {
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);

            dataGridView1.ReadOnly = true;

            if (dt.Columns.Count >= 9)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "RB Masuk";
                dataGridView1.Columns[4].HeaderText = "RB Keluar";
                dataGridView1.Columns[5].HeaderText = "Stock";
                dataGridView1.Columns[6].HeaderText = "Panjang RB (mm)";
                dataGridView1.Columns[7].HeaderText = "Sisa Potongan RB (mm)";
                dataGridView1.Columns[8].HeaderText = "RB Sawing E1-155 mm";
                dataGridView1.Columns[9].HeaderText = "RB Sawing E2-220 mm";
                dataGridView1.Columns[10].HeaderText = "RB Lathe E1-155 mm";
                dataGridView1.Columns[11].HeaderText = "RB Lathe E2-220 mm";
                dataGridView1.Columns[12].HeaderText = "Produksi RB E1-155 mm";
                dataGridView1.Columns[13].HeaderText = "Produksi RB E2-220 mm";
                dataGridView1.Columns[14].HeaderText = "Stock WPS E1-155 mm";
                dataGridView1.Columns[15].HeaderText = "Stock WPS E2-220 mm";
                dataGridView1.Columns[16].HeaderText = "Stock WPL E1-155 mm";
                dataGridView1.Columns[17].HeaderText = "Stock WPL E2-220 mm";
                dataGridView1.Columns[18].HeaderText = "Sisa Potongan RB (Kg)";
                dataGridView1.Columns[19].HeaderText = "Waste (Kg)";
                dataGridView1.Columns[20].HeaderText = "E1 (MM)";
                dataGridView1.Columns[21].HeaderText = "E2 (MM)";
                dataGridView1.Columns[22].HeaderText = "Total E1&E2";
                dataGridView1.Columns[23].HeaderText = "Waste";
                dataGridView1.Columns[24].HeaderText = "Keterangan";
                dataGridView1.Columns[25].HeaderText = "Diubah";
            }

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

        private async Task<bool> cari()
        {
            DateTime? date1Value = tanggal1.Checked ? (DateTime?)tanggal1.Value.Date : null;
            DateTime? date2Value = tanggal2.Checked ? (DateTime?)tanggal2.Value.Date : null;
            string inputname = txtnama.Text.Trim();
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!date1Value.HasValue && !date2Value.HasValue && string.IsNullOrEmpty(inputname) && !shiftValid)
            {
                MessageBox.Show("Silakan isi Tanggal, Nama Penginput atau Shift untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM Rb_Stok WHERE 1=1 ";

                if (date1Value.HasValue && date2Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal AS DATE) BETWEEN @tgl1 AND @tgl2 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl1", date1Value.Value);
                    lastSearchCmd.Parameters.AddWithValue("@tgl2", date2Value.Value);
                }
                else if (date1Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal AS DATE) = @tgl1 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl1", date1Value.Value);
                }
                else if (date2Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal AS DATE) = @tgl2 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl2", date2Value.Value);
                }

                if (shiftValid)
                {
                    lastSearchWhere += " AND shift = @shift ";
                    lastSearchCmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem.ToString());
                }

                if (!string.IsNullOrEmpty(inputname))
                {
                    lastSearchWhere += " AND remaks LIKE @remaks ";
                    lastSearchCmd.Parameters.AddWithValue("@remaks", "%" + inputname + "%");
                }

                await HitungTotalDataPencarian();
                currentPage = 1;
                await tampil();

                btnreset.Enabled = true;
                return true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal cari");
                return false;
            }
        }

        private async void historyWelding_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;
            await HitungTotalData();
            await tampil();
            tanggal1.Value = DateTime.Now.Date;
            tanggal2.Value = DateTime.Now.Date;
            tanggal1.Checked = false;
            tanggal2.Checked = false;
        }

        private async void btncari_Click(object sender, EventArgs e)
        {
            await cari();
        }

        private async void btnreset_Click(object sender, EventArgs e)
        {
            isSearching = false;

            cbShift.SelectedIndex = 0;
            txtnama.Clear();
            tanggal1.Checked = false;
            tanggal2.Checked = false;

            btnreset.Enabled = false;

            await HitungTotalData();
            currentPage = 1;
            await tampil();
        }

        private async void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await tampil();
            }
        }

        private async void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                await tampil();
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void historyWelding_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }
    }
}
