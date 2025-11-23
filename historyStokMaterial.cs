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

        public historyStokMaterial()
        {
            InitializeComponent();
        }

        private void registertampilstokmaterial()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (var cmd = new SqlCommand("SELECT updated_at FROM dbo.stok_material", conn))
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

                            registertampilstokmaterial();
                        }));
                    }
                };

                conn.Open();
                cmd.ExecuteReader();
            }
        }
        private void HitungTotalData()
        {
            string query = "SELECT COUNT(*) FROM stok_material";
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

        private void LoadDataToGrid(DataTable dt)
        {
            dt.Columns["foto"].ColumnName = "Gambar";

            DataTable dtWithImage = new DataTable();
            dtWithImage.Columns.Add("No", typeof(int));
            dtWithImage.Columns.Add("Kode Barang", typeof(string));
            dtWithImage.Columns.Add("Nama Barang", typeof(string));
            dtWithImage.Columns.Add("Spesifikasi", typeof(string));
            dtWithImage.Columns.Add("UoM", typeof(string));
            dtWithImage.Columns.Add("Tipe", typeof(string));
            dtWithImage.Columns.Add("Jumlah Stok", typeof(int));
            dtWithImage.Columns.Add("Min Stok", typeof(int));
            dtWithImage.Columns.Add("Gambar", typeof(Image));
            dtWithImage.Columns.Add("Disimpan", typeof(DateTime));
            dtWithImage.Columns.Add("Diubah", typeof(DateTime));

            int no = 1;
            foreach (DataRow row in dt.Rows)
            {
                DataRow newRow = dtWithImage.NewRow();
                newRow["No"] = no++;
                newRow["Kode Barang"] = row["kodeBarang"];
                newRow["Nama Barang"] = row["namaBarang"];
                newRow["Spesifikasi"] = row["spesifikasi"];
                newRow["UoM"] = row["uom"];
                newRow["Tipe"] = row["type"];
                newRow["Jumlah Stok"] = row["jumlahStok"];
                newRow["Min Stok"] = row["min_stok"];
                newRow["Disimpan"] = row["created_at"];
                newRow["Diubah"] = row["updated_at"];

                if (row["Gambar"] != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])row["Gambar"];
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        newRow["Gambar"] = Image.FromStream(ms);
                    }
                }

                dtWithImage.Rows.Add(newRow);
            }

            dataGridView1.DataSource = dtWithImage;

            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewRow r in dataGridView1.Rows)
                r.Height = 100;

            DataGridViewImageColumn imageCol =
                (DataGridViewImageColumn)dataGridView1.Columns["Gambar"];
            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(213, 213, 214);
            dataGridView1.RowHeadersVisible = false;
        }
        private void tampil()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;

                string query = @"
            SELECT * FROM stok_material
            ORDER BY updated_at DESC
            OFFSET @offset ROWS 
            FETCH NEXT @pageSize ROWS ONLY";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                LoadDataToGrid(dt);
                if (!isSearching)
                    lbljumlahdata.Text = "Jumlah data: " + totalRecords;
                else
                    lbljumlahdata.Text = "Hasil pencarian: " + searchTotalRecords;

                lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

                btnleft.Enabled = currentPage > 1;
                btnright.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message);
            }
        }
        private void cari()
        {
            string keyword = "%" + txtcari.Text + "%";
            currentPage = Math.Max(1, currentPage);

            isSearching = true;

            lastSearchWhere = "FROM stok_material WHERE kodeBarang LIKE @key OR namaBarang LIKE @key";

            lastSearchCmd = new SqlCommand();
            lastSearchCmd.Parameters.AddWithValue("@key", keyword);

            int offset = (currentPage - 1) * pageSize;

            string query = @"
        SELECT * FROM stok_material
        WHERE kodeBarang LIKE @key OR namaBarang LIKE @key
        ORDER BY updated_at DESC
        OFFSET @offset ROWS 
        FETCH NEXT @pageSize ROWS ONLY";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@key", keyword);
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                ad.Fill(dt);
                conn.Close();

                HitungTotalDataPencarian();

                LoadDataToGrid(dt);

                lbljumlahdata.Text = "Hasil pencarian: " + searchTotalRecords;
                lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

                btnleft.Enabled = currentPage > 1;
                btnright.Enabled = currentPage < totalPages;
                btnreset.Enabled = true;
            }
        }

        private void historyStokMaterial_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            HitungTotalData();
            tampil();
            registertampilstokmaterial();
        }

        private void historyStokMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
        }

        private void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                if (isSearching) cari();
                else tampil();
            }
        }

        private void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                if (isSearching) cari();
                else tampil();
            }
        }

        private void btncari_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcari.Text))
            {
                MessageBox.Show("Masukkan Kode Barang atau Nama Barang terlebih dahulu.","Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentPage = 1;
            cari();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtcari.Text = "";     
            isSearching = false;  
            currentPage = 1;      
            lastSearchCmd = null;  
            lastSearchWhere = ""; 
            searchTotalRecords = 0;

            btnreset.Enabled = false;

            HitungTotalData();   
            tampil();
        }
    }
}
