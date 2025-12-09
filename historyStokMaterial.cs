using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace GOS_FxApps
{
    public partial class historyStokMaterial : Form
    {
        SqlConnection conn = Koneksi.GetConnection();


        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;
        private bool isEditing = false;

        public historyStokMaterial()
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
                    case "stok_material":
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
                string query = "SELECT COUNT(*) FROM stok_material";
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

                string query;

                if (!isSearching)
                {
                    query = $@"
            SELECT kodeBarang,namaBarang,spesifikasi,uom,type,jumlahStok,min_stok,foto,created_at,updated_at,remaks
            FROM stok_material
            ORDER BY created_at DESC
            OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
                }
                else
                {
                    query = $@"
            SELECT kodeBarang,namaBarang,spesifikasi,uom,type,jumlahStok,min_stok,foto,created_at,updated_at,remaks
            {lastSearchWhere}
            ORDER BY created_at DESC
            OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
                }

                DataTable dt = new DataTable();

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (isSearching && lastSearchCmd != null)
                    {
                        foreach (SqlParameter p in lastSearchCmd.Parameters)
                            cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                    }

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }

                dt.Columns.Add("fotoImage", typeof(Image));

                dataGridView1.Invoke(new Action(() =>
                {
                    dataGridView1.RowTemplate.Height = 80;
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["kodeBarang"].HeaderText = "Kode Barang";

                    dataGridView1.Columns["namaBarang"].HeaderText = "Nama Barang";
                    dataGridView1.Columns["spesifikasi"].HeaderText = "Spesifikasi";
                    dataGridView1.Columns["uom"].HeaderText = "UoM";
                    dataGridView1.Columns["type"].HeaderText = "Type";

                    dataGridView1.Columns["jumlahStok"].HeaderText = "Jumlah Stok";
                    dataGridView1.Columns["min_stok"].HeaderText = "Min Stok";

                    if (dataGridView1.Columns.Contains("foto"))
                        dataGridView1.Columns["foto"].Visible = false;

                    if (dataGridView1.Columns.Contains("fotoImage"))
                    {
                        var imgCol = (DataGridViewImageColumn)dataGridView1.Columns["fotoImage"];
                        imgCol.HeaderText = "Foto";
                        imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    }

                    dataGridView1.Columns["created_at"].HeaderText = "Disimpan";
                    dataGridView1.Columns["updated_at"].HeaderText = "Diubah";
                    dataGridView1.Columns["remaks"].HeaderText = "Remaks";

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);

                }));

                _ = Task.Run(() =>
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["foto"] != DBNull.Value)
                        {
                            try
                            {
                                byte[] bytes = (byte[])row["foto"];
                                using (MemoryStream ms = new MemoryStream(bytes))
                                {
                                    row["fotoImage"] = Image.FromStream(ms);
                                }
                            }
                            catch { }
                        }
                    }
                });

                dataGridView1.Invoke(new Action(() =>
                {
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
                }));
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

        private async Task<bool> cari()
        {
            string nomorrod = txtcari.Text.Trim();

            if (string.IsNullOrEmpty(nomorrod))
            {
                MessageBox.Show("Silakan isi Tanggal atau Nomor ROD untuk melakukan pencarian.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM stok_material WHERE 1=1 ";

                if (!string.IsNullOrEmpty(nomorrod))
                {
                    lastSearchWhere += " AND kodeBarang LIKE @key OR namaBarang LIKE @key";
                    lastSearchCmd.Parameters.AddWithValue("@key", "%" + nomorrod + "%");
                }

                await HitungTotalDataPencarian();
                currentPage = 1;
                await tampil();

                btncari.Text = "Reset";
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

        private async void historyStokMaterial_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;
            await HitungTotalData();
            await tampil();
        }

        private async void btncari_Click(object sender, EventArgs e)
        {
            await cari();
            btnreset.Enabled = true;
        }

        private async void btnreset_Click(object sender, EventArgs e)
        {

            isSearching = false;

            txtcari.Text = "";

            btncari.Text = "Cari";

            await HitungTotalData();
            currentPage = 1;
            await tampil();

            btnreset.Enabled = false;
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

        private void historyStokMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }
    }
}
