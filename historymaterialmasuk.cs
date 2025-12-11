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
using System.Drawing.Printing;

namespace GOS_FxApps
{
    public partial class historymaterialmasuk : Form
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

        public historymaterialmasuk()
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
                    case "material_masuk":
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
                string query = "SELECT COUNT(*) FROM material_masuk";
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
                        SELECT idMasuk, kodeBarang, namaBarang, spesifikasi, type, tanggalMasuk, jumlahMasuk, updated_at, remaks
                        FROM material_masuk
                        ORDER BY tanggalMasuk DESC
                        OFFSET {offset} ROWS
                        FETCH NEXT {pageSize} ROWS ONLY";
                    }
                    else
                    {
                        query = $@"
                        SELECT idMasuk, kodeBarang, namaBarang, spesifikasi, type, tanggalMasuk, jumlahMasuk, updated_at, remaks
                        {lastSearchWhere}
                        ORDER BY tanggalMasuk DESC
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);

            dataGridView1.ReadOnly = true;

            if (dt.Columns.Count >= 9)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Kode Barang";
                dataGridView1.Columns[2].HeaderText = "Nama Barang";
                dataGridView1.Columns[3].HeaderText = "Spesifikasi";
                dataGridView1.Columns[4].HeaderText = "Tipe";
                dataGridView1.Columns[5].HeaderText = "Tanggal Masuk";
                dataGridView1.Columns[6].HeaderText = "Jumlah Masuk";
                dataGridView1.Columns[7].HeaderText = "Diubah";
                dataGridView1.Columns[8].HeaderText = "Remaks";
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
            string keyword = txtcari.Text.Trim();
            string inputname = txtnama.Text.Trim();

            if (!date1Value.HasValue && !date2Value.HasValue && string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(inputname))
            {
                MessageBox.Show("Silakan isi Tanggal, Kode Barang atau Nama Penginput untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM material_masuk WHERE 1=1 ";

                if (date1Value.HasValue && date2Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggalMasuk AS DATE) BETWEEN @tgl1 AND @tgl2 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl1", date1Value.Value);
                    lastSearchCmd.Parameters.AddWithValue("@tgl2", date2Value.Value);
                }
                else if (date1Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggalMasuk AS DATE) = @tgl1 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl1", date1Value.Value);
                }
                else if (date2Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggalMasuk AS DATE) = @tgl2 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl2", date2Value.Value);
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    lastSearchWhere += " AND (kodeBarang LIKE @kode OR namaBarang LIKE @kode) ";
                    lastSearchCmd.Parameters.AddWithValue("@kode", "%" + keyword + "%");
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

        private async void historymaterialmasuk_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            tanggal1.Value = DateTime.Now.Date;
            tanggal2.Value = DateTime.Now.Date;
            tanggal1.Checked = false;
            tanggal2.Checked = false;

            await HitungTotalData();
            await tampil();
        }

        private async void btncari_Click(object sender, EventArgs e)
        {
            await cari();
        }

        private async void btnreset_Click(object sender, EventArgs e)
        {
            isSearching = false;

            txtcari.Clear();
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

        private void historymaterialmasuk_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }
    }
}
