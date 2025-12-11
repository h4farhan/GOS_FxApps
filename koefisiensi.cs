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
using Guna.UI2.WinForms;
using DrawingPoint = System.Drawing.Point;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace GOS_FxApps
{
    public partial class koefisiensi : Form
    {
        int noprimarymaterial = 0;
        private bool isLoading = false;
        private System.Windows.Forms.Timer searchTimer;
        private bool formSiap = false;

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

        public koefisiensi()
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
                    case "koefisiensi_material":
                        if (!isSearching)
                        {
                            await HitungTotalData();
                            currentPage = 1;
                            await tampilmaterial();
                        }
                        else
                        {
                            int oldTotal = searchTotalRecords;
                            await HitungTotalDataPencarian();
                            if (searchTotalRecords > oldTotal)
                                await tampilmaterial();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch { }
        }

        private async Task HitungTotalData()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM koefisiensi_material";
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

        private async Task tampilmaterial()
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
                FROM koefisiensi_material s
                ORDER BY 
                             CASE 
                                 WHEN s.kodeBarang LIKE 'MATR%' THEN 1
                                 WHEN s.kodeBarang LIKE 'CONS%' THEN 2
                                 WHEN s.kodeBarang LIKE 'APDS%' THEN 3
                                 WHEN s.kodeBarang LIKE 'MAIN%' THEN 4
                                 WHEN s.kodeBarang LIKE 'ATK%'  THEN 5
                                 WHEN s.kodeBarang LIKE 'LOGS%' THEN 6
                                 ELSE 7
                             END,
                             TRY_CAST(
                                 LEFT(
                                     SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)), 
                                     PATINDEX('%[^0-9]%', SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)) + 'X') - 1
                                 ) AS INT
                             ),
                             SUBSTRING(
                                 SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)), 
                                 PATINDEX('%[^0-9]%', SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)) + 'X'), 
                                 LEN(s.kodeBarang)
                             ),
                             s.kodeBarang, tanggal DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                }
                else
                {
                    query = $@"
                    SELECT *
                    {lastSearchWhere}
                    ORDER BY 
                        CASE 
                            WHEN s.kodeBarang LIKE 'MATR%' THEN 1
                            WHEN s.kodeBarang LIKE 'CONS%' THEN 2
                            WHEN s.kodeBarang LIKE 'APDS%' THEN 3
                            WHEN s.kodeBarang LIKE 'MAIN%' THEN 4
                            WHEN s.kodeBarang LIKE 'ATK%'  THEN 5
                            WHEN s.kodeBarang LIKE 'LOGS%' THEN 6
                            ELSE 7
                        END,
                        TRY_CAST(
                            LEFT(
                                SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)), 
                                PATINDEX('%[^0-9]%', SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)) + 'X') - 1
                            ) AS INT
                        ),
                        SUBSTRING(
                            SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)), 
                            PATINDEX('%[^0-9]%', SUBSTRING(s.kodeBarang, 5, LEN(s.kodeBarang)) + 'X'), 
                            LEN(s.kodeBarang)
                        ),
                        s.kodeBarang, tanggal DESC
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
            catch
            {
                return;
            }
        }

        private void UpdateGrid(DataTable dt)
        {
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["tanggal"].DefaultCellStyle.Format = "yyyy";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            if (dt.Columns.Count >= 23)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tahun";
                dataGridView1.Columns[2].HeaderText = "Type";
                dataGridView1.Columns[3].HeaderText = "Kode Barang";
                dataGridView1.Columns[4].HeaderText = "Deskripsi";
                dataGridView1.Columns[5].HeaderText = "Spesifikasi";
                dataGridView1.Columns[6].HeaderText = "UoM";
                dataGridView1.Columns[7].HeaderText = "Koef E1";
                dataGridView1.Columns[8].HeaderText = "Koef E2";
                dataGridView1.Columns[9].HeaderText = "Koef E3";
                dataGridView1.Columns[10].HeaderText = "Koef E4";
                dataGridView1.Columns[11].HeaderText = "Koef S";
                dataGridView1.Columns[12].HeaderText = "Koef D";
                dataGridView1.Columns[13].HeaderText = "Koef B";
                dataGridView1.Columns[14].HeaderText = "Koef BA";
                dataGridView1.Columns[15].HeaderText = "Koef BA-1";
                dataGridView1.Columns[16].HeaderText = "Koef CR";
                dataGridView1.Columns[17].HeaderText = "Koef M";
                dataGridView1.Columns[18].HeaderText = "Koef R";
                dataGridView1.Columns[19].HeaderText = "Koef C";
                dataGridView1.Columns[20].HeaderText = "Koef RL";
                dataGridView1.Columns[21].HeaderText = "Diubah";
                dataGridView1.Columns[22].HeaderText = "Remaks";
            }

            lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

            btnleft.Enabled = currentPage > 1;
            btnright.Enabled = currentPage < totalPages;
        }

        private async Task<bool> cari()
        {
            string keyword = txtcari.Text.Trim();
            int? tahun = tahuncari.Checked ? tahuncari.Value.Year : (int?)null;

            if (!tahun.HasValue && string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Silakan isi Tanggal atau Kode/Nama Barang untuk melakukan pencarian.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = " FROM koefisiensi_material s WHERE 1=1 ";

                if (tahun.HasValue)
                {
                    lastSearchWhere += " AND YEAR(tanggal) = @tahun ";
                    lastSearchCmd.Parameters.AddWithValue("@tahun", tahun.Value);
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    lastSearchWhere += " AND (kodeBarang LIKE @kode OR deskripsi LIKE @kode) ";
                    lastSearchCmd.Parameters.AddWithValue("@kode", "%" + keyword + "%");
                }

                await HitungTotalDataPencarian();
                currentPage = 1;
                await tampilmaterial();

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
                MessageBox.Show("Gagal Cari Data.");
                return false;
            }
        }


        private void textBoxx_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == ',')
            {
                if (tb.Text.Contains(",") || tb.SelectionStart == 0)
                    e.Handled = true; 
                return;
            }

            e.Handled = true;
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void clearedit()
        {
            cmbmaterial.SelectedIndex = -1;
            txtcarinamabarang.Clear();
            txtspesifikasi.Clear();
            txtuom.Clear();
            txttipe.Clear();
            txtkoefe1.Clear();
            txtkoefe2.Clear();
            txtkoefe3.Clear();
            txtkoefe4.Clear();
            txtkoefs.Clear();
            txtkoefd.Clear();
            txtkoefb.Clear();
            txtkoefba.Clear();
            txtkoefba1.Clear();
            txtkoefcr.Clear();
            txtkoefm.Clear();
            txtkoefr.Clear();
            txtkoefc.Clear();
            txtkoefrl.Clear();
            noprimarymaterial = 0;
            tahun.Value = new DateTime(DateTime.Now.Year, 1, 1);
        }

        private void resetsearchui()
        {
            txtcari.Clear();
            btncari.Text = "Cari";
        }

        private bool ValidasiInputKoefisiensi()
        {
            Guna2TextBox[] daftarTextBox = {
        txtkoefe1, txtkoefe2, txtkoefe3, txtkoefe4,
        txtkoefs, txtkoefd, txtkoefb, txtkoefba,
        txtkoefba1, txtkoefcr, txtkoefm, txtkoefr,
        txtkoefc, txtkoefrl
            };

            foreach (var tb in daftarTextBox)
            {
                string text = tb.Text.Trim();

                if (string.IsNullOrEmpty(text))
                    continue;

                if (!System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9,]+$"))
                {
                    MessageBox.Show($"Kolom '{tb.Name}' hanya boleh berisi angka dan koma!",
                                    "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tb.Focus();
                    return false;
                }

                if (text.Count(c => c == ',') > 1)
                {
                    MessageBox.Show($"Kolom '{tb.Name}' tidak boleh memiliki lebih dari satu koma!",
                                    "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tb.Focus();
                    return false;
                }
            }

            return true;
        }

        private async Task simpandatamaterial()
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?",
                                                      "Konfirmasi",
                                                      MessageBoxButtons.OKCancel,
                                                      MessageBoxIcon.Warning);

                if (result != DialogResult.OK)
                    return;

                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmdcekkode = new SqlCommand("SELECT 1 FROM koefisiensi_material WHERE kodeBarang = @kode AND YEAR(tanggal) = @tahun", conn))
                    {
                        cmdcekkode.Parameters.AddWithValue("@kode", cmbKodeBarang.SelectedValue.ToString());
                        cmdcekkode.Parameters.AddWithValue("@tahun", tahun.Value.Year);

                        object exists = await cmdcekkode.ExecuteScalarAsync();
                        if (exists != null)
                        {
                            MessageBox.Show($"Kode Barang ini sudah ada di tahun {tahun.Value.Year}",
                                            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (SqlCommand cmd1 = new SqlCommand(@"
                    INSERT INTO koefisiensi_material 
                    (tanggal,type,kodeBarang,deskripsi,spesifikasi,uom,
                     koef_e1,koef_e2,koef_e3,koef_e4,
                     koef_s,koef_d,koef_b,koef_ba,koef_ba1,
                     koef_cr,koef_m,koef_r,koef_c,koef_rl,
                     updated_at,remaks) 
                    VALUES
                    (@tanggal,@type,@kodeBarang,@deskripsi,@spesifikasi,@uom,
                     @e1,@e2,@e3,@e4,
                     @s,@d,@b,@ba,@ba1,
                     @cr,@m,@r,@c,@rl,
                     @diubah,@remaks)", conn))
                    {
                        cmd1.Parameters.AddWithValue("@tanggal", new DateTime(tahun.Value.Year, 1, 1));
                        cmd1.Parameters.AddWithValue("@type", txttipe.Text);
                        cmd1.Parameters.AddWithValue("@kodeBarang", cmbKodeBarang.SelectedValue.ToString());
                        cmd1.Parameters.AddWithValue("@deskripsi", cmbmaterial.Text);
                        cmd1.Parameters.AddWithValue("@spesifikasi", txtspesifikasi.Text);
                        cmd1.Parameters.AddWithValue("@uom", txtuom.Text);

                        cmd1.Parameters.AddWithValue("@e1", string.IsNullOrWhiteSpace(txtkoefe1.Text) ? 0 : Convert.ToDecimal(txtkoefe1.Text));
                        cmd1.Parameters.AddWithValue("@e2", string.IsNullOrWhiteSpace(txtkoefe2.Text) ? 0 : Convert.ToDecimal(txtkoefe2.Text));
                        cmd1.Parameters.AddWithValue("@e3", string.IsNullOrWhiteSpace(txtkoefe3.Text) ? 0 : Convert.ToDecimal(txtkoefe3.Text));
                        cmd1.Parameters.AddWithValue("@e4", string.IsNullOrWhiteSpace(txtkoefe4.Text) ? 0 : Convert.ToDecimal(txtkoefe4.Text));
                        cmd1.Parameters.AddWithValue("@s", string.IsNullOrWhiteSpace(txtkoefs.Text) ? 0 : Convert.ToDecimal(txtkoefs.Text));
                        cmd1.Parameters.AddWithValue("@d", string.IsNullOrWhiteSpace(txtkoefd.Text) ? 0 : Convert.ToDecimal(txtkoefd.Text));
                        cmd1.Parameters.AddWithValue("@b", string.IsNullOrWhiteSpace(txtkoefb.Text) ? 0 : Convert.ToDecimal(txtkoefb.Text));
                        cmd1.Parameters.AddWithValue("@ba", string.IsNullOrWhiteSpace(txtkoefba.Text) ? 0 : Convert.ToDecimal(txtkoefba.Text));
                        cmd1.Parameters.AddWithValue("@ba1", string.IsNullOrWhiteSpace(txtkoefba1.Text) ? 0 : Convert.ToDecimal(txtkoefba1.Text));
                        cmd1.Parameters.AddWithValue("@cr", string.IsNullOrWhiteSpace(txtkoefcr.Text) ? 0 : Convert.ToDecimal(txtkoefcr.Text));
                        cmd1.Parameters.AddWithValue("@m", string.IsNullOrWhiteSpace(txtkoefm.Text) ? 0 : Convert.ToDecimal(txtkoefm.Text));
                        cmd1.Parameters.AddWithValue("@r", string.IsNullOrWhiteSpace(txtkoefr.Text) ? 0 : Convert.ToDecimal(txtkoefr.Text));
                        cmd1.Parameters.AddWithValue("@c", string.IsNullOrWhiteSpace(txtkoefc.Text) ? 0 : Convert.ToDecimal(txtkoefc.Text));
                        cmd1.Parameters.AddWithValue("@rl", string.IsNullOrWhiteSpace(txtkoefrl.Text) ? 0 : Convert.ToDecimal(txtkoefrl.Text));

                        cmd1.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        cmd1.Parameters.AddWithValue("@remaks", loginform.login.name);

                        await cmd1.ExecuteNonQueryAsync();
                    }
                }

                MessageBox.Show("Data Koefisiensi Material Berhasil Disimpan",
                                "Sukses",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                clearedit();

                if (isSearching)
                {
                    await HitungTotalDataPencarian();
                    await tampilmaterial();
                }
                else
                {
                    await HitungTotalData();
                    await tampilmaterial();

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
                MessageBox.Show("Gagal Simpan Data.");
                return;
            }
        }

        private async Task editmaterial()
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                try
                {
                    using (var conn = await Koneksi.GetConnectionAsync())
                    {
                        string query = "UPDATE koefisiensi_material SET koef_e1 = @e1, koef_e2 = @e2, koef_e3 = @e3, koef_e4 = @e4, koef_s = @s, koef_d = @d, koef_b = @b, koef_ba = @ba, koef_ba1 = @ba1, " +
                    "koef_cr = @cr, koef_m = @m, koef_r = @r, koef_c = @c, koef_rl = @rl, updated_at = @diubah, remaks = @remaks WHERE no = @no";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@no", noprimarymaterial);
                        cmd.Parameters.AddWithValue("@e1", string.IsNullOrWhiteSpace(txtkoefe1.Text) ? 0 : Convert.ToDecimal(txtkoefe1.Text));
                        cmd.Parameters.AddWithValue("@e2", string.IsNullOrWhiteSpace(txtkoefe2.Text) ? 0 : Convert.ToDecimal(txtkoefe2.Text));
                        cmd.Parameters.AddWithValue("@e3", string.IsNullOrWhiteSpace(txtkoefe3.Text) ? 0 : Convert.ToDecimal(txtkoefe3.Text));
                        cmd.Parameters.AddWithValue("@e4", string.IsNullOrWhiteSpace(txtkoefe4.Text) ? 0 : Convert.ToDecimal(txtkoefe4.Text));
                        cmd.Parameters.AddWithValue("@s", string.IsNullOrWhiteSpace(txtkoefs.Text) ? 0 : Convert.ToDecimal(txtkoefs.Text));
                        cmd.Parameters.AddWithValue("@d", string.IsNullOrWhiteSpace(txtkoefd.Text) ? 0 : Convert.ToDecimal(txtkoefd.Text));
                        cmd.Parameters.AddWithValue("@b", string.IsNullOrWhiteSpace(txtkoefb.Text) ? 0 : Convert.ToDecimal(txtkoefb.Text));
                        cmd.Parameters.AddWithValue("@ba", string.IsNullOrWhiteSpace(txtkoefba.Text) ? 0 : Convert.ToDecimal(txtkoefba.Text));
                        cmd.Parameters.AddWithValue("@ba1", string.IsNullOrWhiteSpace(txtkoefba1.Text) ? 0 : Convert.ToDecimal(txtkoefba1.Text));
                        cmd.Parameters.AddWithValue("@cr", string.IsNullOrWhiteSpace(txtkoefcr.Text) ? 0 : Convert.ToDecimal(txtkoefcr.Text));
                        cmd.Parameters.AddWithValue("@m", string.IsNullOrWhiteSpace(txtkoefm.Text) ? 0 : Convert.ToDecimal(txtkoefm.Text));
                        cmd.Parameters.AddWithValue("@r", string.IsNullOrWhiteSpace(txtkoefr.Text) ? 0 : Convert.ToDecimal(txtkoefr.Text));
                        cmd.Parameters.AddWithValue("@c", string.IsNullOrWhiteSpace(txtkoefc.Text) ? 0 : Convert.ToDecimal(txtkoefc.Text));
                        cmd.Parameters.AddWithValue("@rl", string.IsNullOrWhiteSpace(txtkoefrl.Text) ? 0 : Convert.ToDecimal(txtkoefrl.Text));
                        cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        cmd.Parameters.AddWithValue("@remaks", loginform.login.name);
                        await cmd.ExecuteNonQueryAsync();

                        MessageBox.Show("Data Koefisiensi Material Berhasil Diedit", "Sukses.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearedit();

                        if (isSearching)
                        {
                            await HitungTotalDataPencarian();
                            await tampilmaterial();
                        }
                        else
                        {
                            await HitungTotalData();
                            await tampilmaterial();

                            resetsearchui();
                        }
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
                    MessageBox.Show("Gagal Edit Data.");
                    return;
                }
            }
        }

        private async Task hapusmaterial()
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin hapus data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                try
                {
                    using (var conn = await Koneksi.GetConnectionAsync())
                    {
                        string query = "DELETE FROM koefisiensi_material WHERE no = @no";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@no", noprimarymaterial);
                        await cmd.ExecuteNonQueryAsync();

                        MessageBox.Show("Data Koefisiensi Material Berhasil Dihapus", "Sukses.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearedit();

                        if (isSearching)
                        {
                            await HitungTotalDataPencarian();
                            await tampilmaterial();
                        }
                        else
                        {
                            await HitungTotalData();
                            await tampilmaterial();

                            resetsearchui();
                        }
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
                    MessageBox.Show("Gagal Hapus Data.");
                    return;
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnsimpanmaterial.PerformClick();
            }
        }

        private async void btnsimpanmaterial_Click(object sender, EventArgs e)
        {
            if (cmbmaterial.Text == "Pilih Material" || cmbKodeBarang.Text == "Pilih Kode Barang" || txtuom.Text == "" || txttipe.Text == "")
            {
                MessageBox.Show("Data Material Harus Diisi Dengan Lengkap.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!ValidasiInputKoefisiensi())
                return;

            if (btnsimpanmaterial.Text == "Edit Data")
            {
                await editmaterial();
                txtuom.Enabled = true;
                txttipe.Enabled = true;
                txtspesifikasi.Enabled = true;
                txtuom.Enabled = false;
                txttipe.Enabled = false;
                txtspesifikasi.Enabled = false;

                btnsimpanmaterial.Text = "Simpan Data";
                btndelete.Text = "Batal";
                btndelete.Enabled = false;
                btnsimpanmaterial.Enabled = false;
                btnbatalmaterial.Visible = false;

                cmbmaterial.Enabled = true;
                cmbKodeBarang.Enabled = true;
                tahun.Enabled = true;
                txtcarinamabarang.Enabled = true;

                if (cmbmaterial.Items.Count > 0)
                    cmbmaterial.SelectedIndex = 0;
                cmbmaterial.Enabled = true;

                cmbKodeBarang.DataSource = null;
                cmbKodeBarang.Items.Clear();
                cmbKodeBarang.Items.Add("Pilih Kode Barang");
                cmbKodeBarang.SelectedIndex = 0;
                cmbKodeBarang.Enabled = true;
            }
            else
            {
                await simpandatamaterial();
                txtuom.Enabled = true;
                txttipe.Enabled = true;
                txtspesifikasi.Enabled = true;
                txtuom.Enabled = false;
                txttipe.Enabled = false;
                txtspesifikasi.Enabled = false;

                btnbatalmaterial.Visible = false;
                btnsimpanmaterial.Enabled = false;
                btndelete.Enabled = false;

                if (cmbmaterial.Items.Count > 0)
                    cmbmaterial.SelectedIndex = 0;
                cmbmaterial.Enabled = true;

                cmbKodeBarang.DataSource = null;
                cmbKodeBarang.Items.Clear();
                cmbKodeBarang.Items.Add("Pilih Kode Barang");
                cmbKodeBarang.SelectedIndex = 0;
                cmbKodeBarang.Enabled = true;
            }             
        }

        private void btnbatalmaterial_Click(object sender, EventArgs e)
        {
            txtuom.Enabled = true;
            txttipe.Enabled = true;
            txtspesifikasi.Enabled = true;
            clearedit();
            txtuom.Enabled = false;
            txttipe.Enabled = false;
            txtspesifikasi.Enabled = false;

            btnsimpanmaterial.Text = "Simpan Data";
            btndelete.Text = "Batal";
            btndelete.Enabled = false;
            btnsimpanmaterial.Enabled = false;
            btnbatalmaterial.Visible = false;

            cmbmaterial.Enabled = true;
            cmbKodeBarang.Enabled = true;
            tahun.Enabled = true;
            txtcarinamabarang.Enabled = true;

            if (cmbmaterial.Items.Count > 0)
                cmbmaterial.SelectedIndex = 0;
            cmbmaterial.Enabled = true;

            cmbKodeBarang.DataSource = null;
            cmbKodeBarang.Items.Clear();
            cmbKodeBarang.Items.Add("Pilih Kode Barang");
            cmbKodeBarang.SelectedIndex = 0;
            cmbKodeBarang.Enabled = true;
        }

        private void txtkoefm_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefs_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe3_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe2_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe1_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefcr_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefb_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefba_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefba1_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefd_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefrl_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefc_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefe4_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
        }

        private void txtkoefr_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpanmaterial.Enabled = true;
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

        public async Task combonama()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
            SELECT DISTINCT namaBarang
            FROM stok_material
            WHERE type IN ('MATERIAL COST','CONSUMABLE COST','SAFETY PROTECTOR COST')
            ORDER BY namaBarang", conn))
                {
                    DataTable dt = await LoadDataTableAsync(cmd);

                    DataRow dr = dt.NewRow();
                    dr["namaBarang"] = "Pilih Material";
                    dt.Rows.InsertAt(dr, 0);

                    cmbmaterial.SelectedIndexChanged -= cmbmaterial_SelectedIndexChanged;

                    cmbmaterial.DataSource = dt;
                    cmbmaterial.DisplayMember = "namaBarang";
                    cmbmaterial.ValueMember = "namaBarang";
                    cmbmaterial.SelectedIndex = 0;

                    cmbmaterial.SelectedIndexChanged += cmbmaterial_SelectedIndexChanged;
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal combonama Data.");
                return;
            }
        }

        private async void cmbmaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (cmbmaterial.SelectedIndex <= 0)
            {
                cmbKodeBarang.DataSource = null;
                cmbKodeBarang.Items.Clear();
                cmbKodeBarang.Items.Add("Pilih Kode Barang");
                cmbKodeBarang.SelectedIndex = 0;
                return;
            }

            string namaBarang = cmbmaterial.SelectedValue.ToString();

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
            SELECT kodeBarang
            FROM stok_material
            WHERE namaBarang = @namaBarang
              AND type IN ('MATERIAL COST','CONSUMABLE COST','SAFETY PROTECTOR COST')
            ORDER BY kodeBarang ASC", conn))
                {
                    cmd.Parameters.AddWithValue("@namaBarang", namaBarang);

                    DataTable dt = await LoadDataTableAsync(cmd);

                    DataRow dr = dt.NewRow();
                    dr["kodeBarang"] = "Pilih Kode Barang";
                    dt.Rows.InsertAt(dr, 0);

                    cmbKodeBarang.SelectedIndexChanged -= cmbKodeBarang_SelectedIndexChanged;

                    cmbKodeBarang.DataSource = dt;
                    cmbKodeBarang.DisplayMember = "kodeBarang";
                    cmbKodeBarang.ValueMember = "kodeBarang";
                    cmbKodeBarang.SelectedIndex = 0;

                    cmbKodeBarang.SelectedIndexChanged += cmbKodeBarang_SelectedIndexChanged;
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal cmbmaterial Data.");
                return;
            }
        }

        private async void cmbKodeBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading || cmbKodeBarang.SelectedIndex <= 0) return;

            string kodeBarang = cmbKodeBarang.SelectedValue.ToString();

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(
                    "SELECT spesifikasi, uom, type FROM stok_material WHERE kodeBarang = @kode",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@kode", kodeBarang);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            txtuom.Text = reader["uom"]?.ToString();
                            txttipe.Text = reader["type"]?.ToString();
                            txtspesifikasi.Text = reader["spesifikasi"]?.ToString();
                            btndelete.Enabled = true;
                            btnsimpanmaterial.Enabled = true;
                        }
                        else
                        {
                            clearedit();
                            btndelete.Enabled = false;
                            btnsimpanmaterial.Enabled = false;
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
                MessageBox.Show("Gagal cmbKodeBarang Data.");
                return;
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            noprimarymaterial = Convert.ToInt32(row.Cells["no"].Value);
            string namaBarang = row.Cells["deskripsi"].Value.ToString();
            string kodeBarangGrid = row.Cells["kodeBarang"].Value.ToString();

            isLoading = true;

            cmbmaterial.SelectedIndexChanged -= cmbmaterial_SelectedIndexChanged;
            cmbKodeBarang.SelectedIndexChanged -= cmbKodeBarang_SelectedIndexChanged;

            cmbmaterial.SelectedValue = namaBarang;

            using (var conn = await Koneksi.GetConnectionAsync())
            using (var cmd = new SqlCommand(@"
        SELECT kodeBarang
        FROM stok_material
        WHERE namaBarang = @nama
          AND type IN ('MATERIAL COST','CONSUMABLE COST','SAFETY PROTECTOR COST')
        ORDER BY kodeBarang ASC", conn))
            {
                cmd.Parameters.AddWithValue("@nama", namaBarang.Trim());

                DataTable dt = await LoadDataTableAsync(cmd);

                DataRow dr = dt.NewRow();
                dr["kodeBarang"] = "Pilih Kode Barang";
                dt.Rows.InsertAt(dr, 0);

                cmbKodeBarang.DataSource = dt;
                cmbKodeBarang.DisplayMember = "kodeBarang";
                cmbKodeBarang.ValueMember = "kodeBarang";

                if (dt.AsEnumerable().Any(r => r["kodeBarang"].ToString() == kodeBarangGrid))
                    cmbKodeBarang.SelectedValue = kodeBarangGrid;
                else
                    cmbKodeBarang.SelectedIndex = 0;
            }

            tahun.Value = new DateTime(Convert.ToDateTime(row.Cells["tanggal"].Value).Year, 1, 1);
            txtspesifikasi.Text = row.Cells["spesifikasi"].Value.ToString();
            txtuom.Text = row.Cells["uom"].Value.ToString();
            txttipe.Text = row.Cells["type"].Value.ToString();

            txtkoefe1.Text = row.Cells["koef_e1"].Value.ToString();
            txtkoefe2.Text = row.Cells["koef_e2"].Value.ToString();
            txtkoefe3.Text = row.Cells["koef_e3"].Value.ToString();
            txtkoefe4.Text = row.Cells["koef_e4"].Value.ToString();
            txtkoefs.Text = row.Cells["koef_s"].Value.ToString();
            txtkoefd.Text = row.Cells["koef_d"].Value.ToString();
            txtkoefb.Text = row.Cells["koef_b"].Value.ToString();
            txtkoefba.Text = row.Cells["koef_ba"].Value.ToString();
            txtkoefba1.Text = row.Cells["koef_ba1"].Value.ToString();
            txtkoefcr.Text = row.Cells["koef_cr"].Value.ToString();
            txtkoefm.Text = row.Cells["koef_m"].Value.ToString();
            txtkoefr.Text = row.Cells["koef_r"].Value.ToString();
            txtkoefc.Text = row.Cells["koef_c"].Value.ToString();
            txtkoefrl.Text = row.Cells["koef_rl"].Value.ToString();

            cmbmaterial.SelectedIndexChanged += cmbmaterial_SelectedIndexChanged;
            cmbKodeBarang.SelectedIndexChanged += cmbKodeBarang_SelectedIndexChanged;

            isLoading = false;

            tahun.Enabled = false;
            cmbmaterial.Enabled = false;
            cmbKodeBarang.Enabled = false;
            txtcarinamabarang.Enabled = false;

            btnsimpanmaterial.Enabled = true;
            btnsimpanmaterial.Text = "Edit Data";
            btndelete.Enabled = true;
            btndelete.Text = "Hapus Data";
            btnbatalmaterial.Visible = true;
        }


        private async void koefisiensi_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300;
            searchTimer.Tick += SearchTimer_Tick;


            tahun.Value = new DateTime(DateTime.Now.Year, 1, 1);
            tahuncari.Value = new DateTime(DateTime.Now.Year, 1, 1);
            tahuncari.Checked = false;

            cmbmaterial.DropDownStyle = ComboBoxStyle.DropDown;
            cmbmaterial.MaxDropDownItems = 10;
            cmbmaterial.DropDownHeight = 500;

            await combonama();
            formSiap = true;

            await HitungTotalData();
            await tampilmaterial();
        }

        private void koefisiensi_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private async void btndelete_Click(object sender, EventArgs e)
        {
            if (btndelete.Text == "Hapus Data")
            {
                await hapusmaterial();

                txtuom.Enabled = true;
                txttipe.Enabled = true;
                txtspesifikasi.Enabled = true;
                txtuom.Enabled = false;
                txttipe.Enabled = false;
                txtspesifikasi.Enabled = false;

                btnsimpanmaterial.Text = "Simpan Data";
                btndelete.Text = "Batal";
                btndelete.Enabled = false;
                btnsimpanmaterial.Enabled = false;
                btnbatalmaterial.Visible = false;

                cmbmaterial.Enabled = true;
                cmbKodeBarang.Enabled = true;
                tahun.Enabled = true;
                txtcarinamabarang.Enabled = true;

                if (cmbmaterial.Items.Count > 0)
                    cmbmaterial.SelectedIndex = 0;
                cmbmaterial.Enabled = true;

                cmbKodeBarang.DataSource = null;
                cmbKodeBarang.Items.Clear();
                cmbKodeBarang.Items.Add("Pilih Kode Barang");
                cmbKodeBarang.SelectedIndex = 0;
                cmbKodeBarang.Enabled = true;
            }
            else
            {
                txtuom.Enabled = true;
                txttipe.Enabled = true;
                txtspesifikasi.Enabled = true;
                clearedit();
                txtuom.Enabled = false;
                txttipe.Enabled = false;
                txtspesifikasi.Enabled = false;

                btnbatalmaterial.Visible = false;
                btnsimpanmaterial.Enabled = false;
                btndelete.Enabled = false;

                if (cmbmaterial.Items.Count > 0)
                    cmbmaterial.SelectedIndex = 0;
                cmbmaterial.Enabled = true;

                cmbKodeBarang.DataSource = null;
                cmbKodeBarang.Items.Clear();
                cmbKodeBarang.Items.Add("Pilih Kode Barang");
                cmbKodeBarang.SelectedIndex = 0;
                cmbKodeBarang.Enabled = true;
            }
        }

        private void tahun_MouseDown(object sender, MouseEventArgs e)
        {

            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 150);
                pickerForm.Text = "Pilih Tahun";

                var screenPos = tahun.PointToScreen(Point.Empty);
                pickerForm.Location = new Point(screenPos.X, screenPos.Y + tahun.Height);

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 20,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = tahun.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 70,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    tahun.Value = new DateTime((int)numTahun.Value, 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private CancellationTokenSource searchCancelToken;

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            if (!formSiap || cmbmaterial == null || txtcarinamabarang == null)
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
            WHERE 
                (namaBarang LIKE @keyword OR kodeBarang LIKE @keyword)
                AND type IN ('MATERIAL COST', 'CONSUMABLE COST', 'SAFETY PROTECTOR COST')
            ORDER BY namaBarang ASC", conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    DataTable dt = await LoadDataTableAsync(cmd);

                    if (token.IsCancellationRequested)
                        return;

                    DataRow dr = dt.NewRow();
                    dr["namaBarang"] = "Pilih Material";
                    dt.Rows.InsertAt(dr, 0);

                    cmbmaterial.SelectedIndexChanged -= cmbmaterial_SelectedIndexChanged;

                    cmbmaterial.DataSource = dt;
                    cmbmaterial.DisplayMember = "namaBarang";
                    cmbmaterial.ValueMember = "namaBarang";
                    cmbmaterial.SelectedIndex = 0;

                    cmbmaterial.SelectedIndexChanged += cmbmaterial_SelectedIndexChanged;
                }

                if (!cmbmaterial.DroppedDown)
                {
                    if (cmbmaterial.Items.Count > 1)
                        cmbmaterial.DroppedDown = true;
                    else
                        cmbmaterial.DroppedDown = false;
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
                MessageBox.Show("Gagal Search Data.");
                return;
            }
        }

        private void txtcarinamabarang_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void tahuncari_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 150);
                pickerForm.Text = "Pilih Tahun";

                var screenPos = tahuncari.PointToScreen(Point.Empty);
                pickerForm.Location = new Point(screenPos.X, screenPos.Y + tahuncari.Height);

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 20,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = tahuncari.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 70,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    tahuncari.Value = new DateTime((int)numTahun.Value, 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                    tahuncari.Checked = true;
                };

                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
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
                tahuncari.Checked = false;

                btncari.Text = "Cari";

                await HitungTotalData();
                currentPage = 1;
                await tampilmaterial();
            }
        }

        private async void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await tampilmaterial();
            }
        }

        private async void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                await tampilmaterial();
            }
        }
    }
}