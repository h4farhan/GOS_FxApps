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
using System.Threading;

namespace GOS_FxApps
{
    public partial class materialmasuk : Form
    {

        int noprimary = 0;
        int jumlahlama;

        private System.Windows.Forms.Timer searchTimer;
        private bool formSiap = false;

        string type;

        bool infocari = false;
        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;
        private bool isEditing = false;

        public materialmasuk()
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

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnsimpan.PerformClick();
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
                ORDER BY tanggalMasuk DESC, updated_at DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                    }
                    else
                    {
                        query = $@"
                SELECT idMasuk, kodeBarang, namaBarang, spesifikasi, type, tanggalMasuk, jumlahMasuk, updated_at, remaks
                {lastSearchWhere}
                ORDER BY tanggalMasuk DESC, updated_at DESC
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
                dataGridView1.Columns[1].HeaderText = "Kode Barang";
                dataGridView1.Columns[2].HeaderText = "Nama Barang";
                dataGridView1.Columns[3].HeaderText = "Spesifikasi";
                dataGridView1.Columns[4].HeaderText = "Tipe";
                dataGridView1.Columns[5].HeaderText = "Tanggal Masuk";
                dataGridView1.Columns[6].HeaderText = "Jumlah Masuk";
                dataGridView1.Columns[7].HeaderText = "Diubah";
                dataGridView1.Columns[8].HeaderText = "Remaks";
            }

            lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

            btnleft.Enabled = currentPage > 1;
            btnright.Enabled = currentPage < totalPages;
        }

        private async Task<bool> cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string keyword = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Silakan isi Tanggal atau Kode/Nama Barang untuk melakukan pencarian.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM material_masuk WHERE 1=1 ";

                if (tanggal.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggalMasuk AS DATE) = @tgl ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }


                if (!string.IsNullOrEmpty(keyword))
                {
                    lastSearchWhere += " AND (kodeBarang LIKE @kode OR namaBarang LIKE @kode) ";
                    lastSearchCmd.Parameters.AddWithValue("@kode", "%" + keyword + "%");
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

        private void resetsearchui()
        {
            txtcari.Clear();
            btncari.Text = "Cari";
        }

        private async Task editdata()
        {
            string kodeBarang = cmbSpesifikasi.SelectedValue.ToString();
            int jumlahBaru = int.Parse(txtjumlah.Text);

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE stok_material SET jumlahStok = jumlahStok - @jumlahLama WHERE kodeBarang = @kode",
                            conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@jumlahLama", jumlahlama);
                            cmd.Parameters.AddWithValue("@kode", kodeBarang);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        using (var cmdPakai = new SqlCommand(@"
                UPDATE material_masuk 
                SET tanggalMasuk = @tgl, 
                    jumlahMasuk = @jumlahBaru, 
                    updated_at = GETDATE(), 
                    remaks = @remaks 
                WHERE idMasuk = @id", conn, tran))
                        {
                            cmdPakai.Parameters.AddWithValue("@id", noprimary);
                            cmdPakai.Parameters.AddWithValue("@tgl", datemasuk.Value);
                            cmdPakai.Parameters.AddWithValue("@jumlahBaru", jumlahBaru);
                            cmdPakai.Parameters.AddWithValue("@remaks", loginform.login.name);
                            await cmdPakai.ExecuteNonQueryAsync();
                        }

                        using (var cmdUpdateStok = new SqlCommand(
                            "UPDATE stok_material SET jumlahStok = jumlahStok + @jumlahBaru, updated_at = GETDATE() WHERE kodeBarang = @kode",
                            conn, tran))
                        {
                            cmdUpdateStok.Parameters.AddWithValue("@jumlahBaru", jumlahBaru);
                            cmdUpdateStok.Parameters.AddWithValue("@kode", kodeBarang);
                            await cmdUpdateStok.ExecuteNonQueryAsync();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }

                MessageBox.Show("Data Material Masuk Berhasil Diedit",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtjumlah.Clear();
                lblstoksaatini.Text = "Stok Saat Ini : -";
                btnsimpan.Text = "Simpan Data";
                cmbnama.Enabled = true;

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

        private async Task simpandata()
        {
            if (!int.TryParse(txtjumlah.Text, out int jumlahMasuk))
            {
                MessageBox.Show("Masukkan jumlah yang valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kodeBarang = cmbSpesifikasi.SelectedValue.ToString();
            string namaBarang = cmbnama.Text;
            string spesifikasi = cmbSpesifikasi.Text;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmdPakai = new SqlCommand(@"
                INSERT INTO material_masuk 
                (kodeBarang, namaBarang, type, tanggalMasuk, jumlahMasuk, updated_at, remaks, spesifikasi) 
                VALUES (@kode, @nama, @type, @tgl, @jumlah, GETDATE(), @remaks, @spesifikasi)",
                            conn, tran))
                        {
                            cmdPakai.Parameters.AddWithValue("@kode", kodeBarang);
                            cmdPakai.Parameters.AddWithValue("@nama", namaBarang);
                            cmdPakai.Parameters.AddWithValue("@type", type);
                            cmdPakai.Parameters.AddWithValue("@tgl", datemasuk.Value);
                            cmdPakai.Parameters.AddWithValue("@jumlah", jumlahMasuk);
                            cmdPakai.Parameters.AddWithValue("@spesifikasi", spesifikasi);
                            cmdPakai.Parameters.AddWithValue("@remaks", loginform.login.name);

                            await cmdPakai.ExecuteNonQueryAsync();
                        }

                        using (var cmdUpdateStok = new SqlCommand(
                            "UPDATE stok_material SET jumlahStok = jumlahStok + @masuk, updated_at = GETDATE() WHERE kodeBarang = @kode",
                            conn, tran))
                        {
                            cmdUpdateStok.Parameters.AddWithValue("@masuk", jumlahMasuk);
                            cmdUpdateStok.Parameters.AddWithValue("@kode", kodeBarang);

                            await cmdUpdateStok.ExecuteNonQueryAsync();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                       tran.Rollback();
                        throw;
                    }
                }

                MessageBox.Show("Data Masuk Material berhasil ditambahkan.",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private async void btncari_Click(object sender, EventArgs e)
        {
            if (!infocari)
            {
                bool hasil = await cari();
                if (hasil)
                {
                    infocari = true;
                    btncari.Text = "Reset";
                }
            }
            else
            {
                infocari = false;
                isSearching = false;

                txtcari.Text = "";
                datecari.Checked = false;

                btncari.Text = "Cari";

                await HitungTotalData();
                currentPage = 1;
                await tampil();
            }
        }

        private async void btnsimpan_Click(object sender, EventArgs e)
        {
            if (btnsimpan.Text == "Simpan Data")
            {
                if (txtjumlah.Text == "" || cmbnama.Text == "Pilih Material" || cmbSpesifikasi.Text == "Pilih Spesifikasi")
                {
                    MessageBox.Show("Lengkapi data terlebih dahulu dengan benar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        await simpandata();
                        await combonama();
                        txtjumlah.Clear();
                        txtcarinamabarang.Clear();
                        lblstoksaatini.Text = "Stok Saat Ini: -";
                        picture1.Image = null;
                        btnbatal.Enabled = false;
                        btnsimpan.Enabled = false;
                        type = null;
                    }
                }
            }
            else
            {
                if (txtjumlah.Text == "" || cmbnama.SelectedIndex == -1)
                {
                    MessageBox.Show("Lengkapi data terlebih dahulu dengan benar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        await editdata();
                        datemasuk.Value = DateTime.Now.Date;
                        txtcarinamabarang.Clear();
                        txtjumlah.Clear();
                        jumlahlama = 0;
                        noprimary = 0;
                        btnsimpan.Text = "Simpan Data";
                        lblstoksaatini.Text = "Stok Saat Ini: -";
                        picture1.Image = null;
                        btnsimpan.Enabled = false;
                        btnbatal.Enabled = false;
                        type = null;

                        if (cmbnama.Items.Count > 0)
                            cmbnama.SelectedIndex = 0;
                        cmbnama.Enabled = true;
                        txtcarinamabarang.Enabled = true;
                        cmbSpesifikasi.DataSource = null;
                        cmbSpesifikasi.Items.Clear();
                        cmbSpesifikasi.Items.Add("Pilih Spesifikasi");
                        cmbSpesifikasi.SelectedIndex = 0;
                        cmbSpesifikasi.Enabled = true;
                    }
                }
            }
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            datemasuk.Value = DateTime.Now.Date;
            txtcarinamabarang.Clear();
            txtjumlah.Clear();
            jumlahlama = 0;
            noprimary = 0;
            btnsimpan.Text = "Simpan Data";
            lblstoksaatini.Text = "Stok Saat Ini: -";
            picture1.Image = null;
            btnsimpan.Enabled = false;
            btnbatal.Enabled = false;
            type = null;

            if (cmbnama.Items.Count > 0)
                cmbnama.SelectedIndex = 0;
            cmbnama.Enabled = true;
            txtcarinamabarang.Enabled = true;
            cmbSpesifikasi.DataSource = null;
            cmbSpesifikasi.Items.Clear();
            cmbSpesifikasi.Items.Add("Pilih Spesifikasi");
            cmbSpesifikasi.SelectedIndex = 0;
            cmbSpesifikasi.Enabled = true;
        }

        private async void materialmasuk_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300;
            searchTimer.Tick += SearchTimer_Tick;

            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            datemasuk.Value = DateTime.Now.Date;
            cmbnama.DropDownStyle = ComboBoxStyle.DropDown;
            cmbnama.MaxDropDownItems = 10;
            cmbnama.DropDownHeight = 300;
            cmbSpesifikasi.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSpesifikasi.MaxDropDownItems = 10;
            cmbSpesifikasi.DropDownHeight = 300;

            await combonama();
            formSiap = true;

            await HitungTotalData();
            await tampil();
        }

        private async Task<DataTable> LoadDataTableAsync(SqlCommand cmd)
        {
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }

        private bool suppressEvents = false;

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (MainForm.Instance.role != "Manajer" && MainForm.Instance.role != "Developer") return;
                if (e.RowIndex < 0) return;

                suppressEvents = true;

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["idMasuk"].Value);
                string namaBarang = row.Cells["namaBarang"].Value.ToString();
                string spesifikasi = row.Cells["spesifikasi"].Value.ToString();

                cmbnama.SelectedValue = namaBarang;

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
                SELECT kodeBarang, spesifikasi, jumlahStok, type, foto 
                FROM stok_material 
                WHERE namaBarang = @nama
                ORDER BY spesifikasi ASC", conn))
                {
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.AddWithValue("@nama", namaBarang.Trim());

                    DataTable dt = await LoadDataTableAsync(cmd);

                    DataRow dr = dt.NewRow();
                    dr["kodeBarang"] = "";
                    dr["spesifikasi"] = "Pilih Spesifikasi";
                    dt.Rows.InsertAt(dr, 0);

                    cmbSpesifikasi.DataSource = dt;
                    cmbSpesifikasi.DisplayMember = "spesifikasi";
                    cmbSpesifikasi.ValueMember = "kodeBarang";

                    int index = cmbSpesifikasi.FindStringExact(spesifikasi);
                    cmbSpesifikasi.SelectedIndex = index >= 0 ? index : 0;

                    if (index >= 0)
                    {
                        DataRow selectedRow = dt.Rows[index];
                        lblstoksaatini.Text = "Stok Saat Ini: " + selectedRow["jumlahStok"]?.ToString();
                        type = selectedRow["type"]?.ToString();

                        if (picture1.Image != null)
                        {
                            var old = picture1.Image;
                            picture1.Image = null;
                            old.Dispose();
                        }

                        if (selectedRow["foto"] != DBNull.Value)
                        {
                            byte[] imgData = (byte[])selectedRow["foto"];
                            using (MemoryStream ms = new MemoryStream(imgData))
                            {
                                picture1.Image = await Task.Run(() => Image.FromStream(ms));
                            }
                        }
                    }
                    else
                    {
                        lblstoksaatini.Text = "";
                        picture1.Image = null;
                    }
                }

                datemasuk.Value = Convert.ToDateTime(row.Cells["tanggalMasuk"].Value);
                txtjumlah.Text = row.Cells["jumlahMasuk"].Value.ToString();
                type = row.Cells["type"].Value.ToString();
                jumlahlama = Convert.ToInt32(row.Cells["jumlahMasuk"].Value);


                txtcarinamabarang.Enabled = false;
                cmbnama.Enabled = false;
                cmbSpesifikasi.Enabled = false;
                btnsimpan.Text = "Edit Data";
                btnbatal.Enabled = true;
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal datagrid");
                return;
            }
            finally
            {
                suppressEvents = false; 
            }
        }

        public async Task combonama()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
                SELECT DISTINCT namaBarang 
                FROM stok_material 
                ORDER BY namaBarang ASC", conn))
                {

                    DataTable dt = await LoadDataTableAsync(cmd);

                    DataRow dr = dt.NewRow();
                    dr["namaBarang"] = "Pilih Material";
                    dt.Rows.InsertAt(dr, 0);

                    cmbnama.SelectedIndexChanged -= cmbnama_SelectedIndexChanged;

                    cmbnama.DataSource = dt;
                    cmbnama.DisplayMember = "namaBarang";
                    cmbnama.ValueMember = "namaBarang";
                    cmbnama.SelectedIndex = 0;

                    cmbnama.SelectedIndexChanged += cmbnama_SelectedIndexChanged;
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal combonama");
                return;
            }
        }

        private async void cmbnama_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressEvents) return;
            if (cmbnama.SelectedIndex <= 0)
            {
                cmbSpesifikasi.DataSource = null;
                cmbSpesifikasi.Items.Clear();
                cmbSpesifikasi.Items.Add("Pilih Spesifikasi");
                cmbSpesifikasi.SelectedIndex = 0;
                lblstoksaatini.Text = "Stok Saat Ini: -";
                return;
            }

            try
            {
                suppressEvents = true;

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
                SELECT kodeBarang, spesifikasi
                FROM stok_material
                WHERE namaBarang = @namaBarang
                ORDER BY spesifikasi ASC", conn))
                {
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.AddWithValue("@namaBarang", cmbnama.SelectedValue.ToString());

                    DataTable dt = await LoadDataTableAsync(cmd);

                    DataRow dr = dt.NewRow();
                    dr["kodeBarang"] = "";
                    dr["spesifikasi"] = "Pilih Spesifikasi";
                    dt.Rows.InsertAt(dr, 0);

                    cmbSpesifikasi.DataSource = dt;
                    cmbSpesifikasi.DisplayMember = "spesifikasi";
                    cmbSpesifikasi.ValueMember = "kodeBarang";
                    cmbSpesifikasi.SelectedIndex = 0;
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal cmbnama");
                return;
            }
            finally
            {
                suppressEvents = false;
            }
        }

        private async void cmbSpesifikasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressEvents) return;
            if (cmbSpesifikasi.SelectedIndex <= 0)
            {
                lblstoksaatini.Text = "Stok Saat Ini: -";

                if (picture1.Image != null)
                {
                    var old = picture1.Image;
                    picture1.Image = null;
                    old.Dispose();
                }

                return;
            }

            try
            {
                suppressEvents = true;

                string kodeBarang = cmbSpesifikasi.SelectedValue.ToString();

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(
                    "SELECT foto, jumlahStok, type FROM stok_material WHERE kodeBarang = @kodeBarang",
                    conn))
                {
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            lblstoksaatini.Text = "Stok Saat Ini: " + reader["jumlahStok"].ToString();
                            type = reader["type"].ToString();

                            if (picture1.Image != null)
                            {
                                var old = picture1.Image;
                                picture1.Image = null;
                                old.Dispose();
                            }

                            if (reader["foto"] != DBNull.Value)
                            {
                                byte[] imgData = (byte[])reader["foto"];
                                using (MemoryStream ms = new MemoryStream(imgData))
                                {
                                    picture1.Image = await Task.Run(() => Image.FromStream(ms));
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal sepesifikasi");
                return;
            }
            finally
            {
                suppressEvents = false;
            }
        }

        private void txtjumlah_TextChanged(object sender, EventArgs e)
        {
            btnbatal.Enabled = true;
            btnsimpan.Enabled = true;
        }

        private CancellationTokenSource searchCancelToken;

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            if (!formSiap || cmbnama == null || txtcarinamabarang == null)
                return;

            string keyword = txtcarinamabarang.Text?.Trim() ?? "";

            searchCancelToken?.Cancel();
            searchCancelToken = new CancellationTokenSource();
            var token = searchCancelToken.Token;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
            SELECT DISTINCT namaBarang
            FROM stok_material
            WHERE namaBarang LIKE @keyword
               OR kodeBarang LIKE @keyword
            ORDER BY namaBarang ASC", conn))
                {
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    DataTable dt = await LoadDataTableAsync(cmd);

                    if (token.IsCancellationRequested) return;

                    DataRow dr = dt.NewRow();
                    dr["namaBarang"] = "Pilih Material";
                    dt.Rows.InsertAt(dr, 0);

                    suppressEvents = true; 

                    cmbnama.SelectedIndexChanged -= cmbnama_SelectedIndexChanged;

                    cmbnama.DataSource = dt;
                    cmbnama.DisplayMember = "namaBarang";
                    cmbnama.ValueMember = "namaBarang";

                    cmbnama.SelectedIndex = 0; 

                    cmbnama.SelectedIndexChanged += cmbnama_SelectedIndexChanged;

                    suppressEvents = false; 
                }

                if (cmbnama.Items.Count > 1)
                {
                    cmbnama.DroppedDown = true;
                }
                else
                {
                    cmbnama.DroppedDown = false;
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal searchtimer");
                return;
            }
        }

        private void txtcarinamabarang_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
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

        private void materialmasuk_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }
    }
}
