using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GOS_FxApps
{
    public partial class formstok : Form
    {
        private byte[] imageBytes = null;

        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;
        private bool isEditing = false;

        public formstok()
        {
            InitializeComponent();
        }

        private async void formstok_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;
            await HitungTotalData();
            await tampil();
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
            catch
            {
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
            catch
            {
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
            catch
            {
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
                        imgCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        imgCol.Width = 250;
                    }

                    dataGridView1.Columns["created_at"].HeaderText = "Disimpan";
                    dataGridView1.Columns["updated_at"].HeaderText = "Diubah";
                    dataGridView1.Columns["remaks"].HeaderText = "Remaks";

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToResizeRows = false;

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
                    lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";
                    btnleft.Enabled = currentPage > 1;
                    btnright.Enabled = currentPage < totalPages;
                }));
            }
            catch
            {
                return;
            }
        }

        private async Task<bool> cari()
        {
            string nomorrod = txtcari.Text.Trim();

            if (string.IsNullOrEmpty(nomorrod))
            {
                MessageBox.Show("Silakan isi Kode Barang atau Nama Barang untuk melakukan pencarian.",
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
                return false;
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void setdefault()
        {
            txtkodebarang.Enabled = true;
            txtkodebarang.Clear();
            txtminstok.Clear();
            txtnamabarang.Clear();
            txtspesifikasi.Clear();
            txtuom.Clear();
            cmbtipematerial.SelectedIndex = -1;
            imageBytes = null;
            picturebox.Image = null;
            btnsimpan.Text = "Simpan";
            btnsimpan.FillColor = Color.FromArgb(76, 175, 80);
        }

        private void resetsearchui()
        {
            txtcari.Clear();
            btncari.Text = "Cari";
        }

        private async void btnsimpan_Click(object sender, EventArgs e)
        {
            if (btnsimpan.Text == "Batal")
            {
                txtkodebarang.Enabled = true;
                btnupdate.Enabled = false;
                setdefault();
                return;
            }

            if (txtkodebarang.Text == "" || txtnamabarang.Text == "" || cmbtipematerial.SelectedIndex == -1)
            {
                MessageBox.Show("Kode Barang, Nama Barang Dan Tipe Material Harus Diisi Dengan Lengkap.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?",
                                                  "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result != DialogResult.OK) return;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdcekkode = new SqlCommand(
                        "SELECT 1 FROM stok_material WHERE kodeBarang = @kode", conn, trans))
                        {
                            cmdcekkode.Parameters.AddWithValue("@kode", txtkodebarang.Text);

                            object exists = await cmdcekkode.ExecuteScalarAsync();
                            if (exists != null)
                            {
                                MessageBox.Show("Kode Sudah Dipakai Material Lain", "Peringatan",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                trans.Rollback();
                                return;
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO stok_material 
                (kodeBarang, namaBarang, spesifikasi, uom, type, jumlahStok, min_stok, foto, created_at, updated_at, remaks)
                VALUES(@kodebarang,@namabarang,@spesifikasi,@uom,@type,@jumlahStok,@min_stok,@foto,@tanggal,GETDATE(),@remaks)",
                            conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@kodebarang", txtkodebarang.Text);
                            cmd.Parameters.AddWithValue("@namabarang", txtnamabarang.Text);
                            cmd.Parameters.AddWithValue("@jumlahStok", 0);
                            cmd.Parameters.AddWithValue("@spesifikasi", txtspesifikasi.Text);
                            cmd.Parameters.AddWithValue("@uom", txtuom.Text);
                            cmd.Parameters.AddWithValue("@type", cmbtipematerial.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@remaks", loginform.login.name);

                            if (string.IsNullOrWhiteSpace(txtminstok.Text))
                                cmd.Parameters.AddWithValue("@min_stok", 0);
                            else
                                cmd.Parameters.AddWithValue("@min_stok", int.Parse(txtminstok.Text));

                            cmd.Parameters.Add("@foto", SqlDbType.VarBinary).Value =
                                (imageBytes == null ? DBNull.Value : (object)imageBytes);

                            cmd.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);

                            await cmd.ExecuteNonQueryAsync();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                MessageBox.Show("Data Berhasil Disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();

                if (isSearching)
                {
                    await HitungTotalDataPencarian();
                    await tampil();
                }
                else
                {
                    await HitungTotalData();
                    await tampil();
                    resetsearchui();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal simpan");
                return;
            }
        }

        private async void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtnamabarang.Text == "" || cmbtipematerial.SelectedIndex == -1)
            {
                MessageBox.Show("Nama Barang Dan Tipe Material Harus Diisi Dengan Lengkap.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?",
                                                  "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result != DialogResult.OK) return;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        string query = @"
                UPDATE stok_material 
                SET namaBarang = @namabarang, spesifikasi = @spesifikasi,
                    uom = @uom, type = @type, min_stok = @min_stok,
                    foto = @foto, updated_at = GETDATE(), remaks = @remaks
                WHERE kodeBarang = @kodebarang";

                        SqlCommand cmd = new SqlCommand(query, conn, trans);

                        cmd.Parameters.AddWithValue("@kodebarang", txtkodebarang.Text);
                        cmd.Parameters.AddWithValue("@namabarang", txtnamabarang.Text);
                        cmd.Parameters.AddWithValue("@spesifikasi", txtspesifikasi.Text);
                        cmd.Parameters.AddWithValue("@uom", txtuom.Text);
                        cmd.Parameters.AddWithValue("@type", cmbtipematerial.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@remaks", loginform.login.name);

                        if (string.IsNullOrWhiteSpace(txtminstok.Text))
                            cmd.Parameters.AddWithValue("@min_stok", 0);
                        else
                            cmd.Parameters.AddWithValue("@min_stok", int.Parse(txtminstok.Text));

                        cmd.Parameters.Add("@foto", SqlDbType.VarBinary).Value =
                            (imageBytes == null ? DBNull.Value : (object)imageBytes);


                        await cmd.ExecuteNonQueryAsync();

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }

                }

                MessageBox.Show("Data Berhasil Diedit", "Sukses.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                setdefault();
                btnupdate.Enabled = false;
                btnsimpan.Text = "Simpan";

                if (isSearching)
                {
                    await HitungTotalDataPencarian();
                    await tampil();
                }
                else
                {
                    await HitungTotalData();
                    await tampil();
                    resetsearchui();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal edit");
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MainForm.Instance.role != "Manajer" && MainForm.Instance.role != "Developer") return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtkodebarang.Text = row.Cells["kodeBarang"].Value.ToString();
                txtnamabarang.Text = row.Cells["namaBarang"].Value.ToString();
                txtspesifikasi.Text = row.Cells["spesifikasi"].Value.ToString();
                txtuom.Text = row.Cells["uom"].Value.ToString();
                cmbtipematerial.SelectedItem = row.Cells["type"].Value.ToString();
                txtminstok.Text = row.Cells["min_stok"].Value.ToString();

                Image img = row.Cells["fotoImage"].Value as Image;
                if (img != null)
                {
                    picturebox.Image = img;

                    using (Bitmap clone = new Bitmap(img))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            clone.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageBytes = ms.ToArray();
                        }
                    }
                }
                else
                {
                    picturebox.Image = null;
                    imageBytes = null;
                }


                txtkodebarang.Enabled = false;
                btnupdate.Enabled = true;
                btnsimpan.Text = "Batal";
                btnsimpan.FillColor = Color.Red;
            }
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnopenfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Pilih Foto Material";
                openFileDialog.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picturebox.Image = Image.FromFile(openFileDialog.FileName);
                    imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                }
            }
        }

        private void formstok_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
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

        private async void btncari_Click(object sender, EventArgs e)
        {
            await cari();
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
    }
}
