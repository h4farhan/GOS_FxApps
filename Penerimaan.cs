using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Threading;

namespace GOS_FxApps
{
    public partial class Penerimaan : Form
    {
        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;

        public Penerimaan()
        {
            InitializeComponent();
        }

        private async Task HitungTotalData()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM penerimaan_s";
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

        private int ToInt(string text)
        {
            return int.TryParse(text, out int val) ? val : 0;
        }

        private int SafeParse(Guna2TextBox tb)
        {
            return int.TryParse(tb.Text, out int result) ? result : 0;
        }

        private async Task simpandata()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        string nomorRodFix = txtnomorrod.Text.Trim();

                        string queryCek = @"
                    SELECT 'penerimaan_s' 
                    FROM penerimaan_s WHERE nomor_rod = @rod
                    UNION
                    SELECT 'perbaikan_s'
                    FROM perbaikan_s WHERE nomor_rod = @rod";

                        using (var cmd = new SqlCommand(queryCek, conn, trans))
                        {
                            cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = nomorRodFix;

                            object sumber = await cmd.ExecuteScalarAsync();
                            if (sumber != null)
                            {
                                string pesan = sumber.ToString() == "penerimaan_s"
                                    ? "Nomor ROD ini sudah ada di data penerimaan dan belum diperbaiki."
                                    : "Nomor ROD ini sudah ada di data perbaikan dan belum dikirim.";

                                MessageBox.Show(pesan, "Peringatan",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                trans.Rollback();
                                return;
                            }
                        }

                        using (var cmdCekTanggal = new SqlCommand(@"
                    SELECT COUNT(*) 
                    FROM penerimaan_p
                    WHERE nomor_rod = @nomor_rod
                    AND CONVERT(date, tanggal_penerimaan) = CONVERT(date, @tgl)", conn, trans))
                        {
                            cmdCekTanggal.Parameters.Add("@nomor_rod", SqlDbType.VarChar).Value = nomorRodFix;
                            cmdCekTanggal.Parameters.Add("@tgl", SqlDbType.DateTime).Value = MainForm.Instance.tanggal;

                            if ((int)await cmdCekTanggal.ExecuteScalarAsync() > 0)
                            {
                                MessageBox.Show(
                                    $"Nomor ROD {nomorRodFix} sudah pernah diterima pada tanggal yang sama.",
                                    "Tanggal Duplikat",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                trans.Rollback();
                                return;
                            }
                        }

                        int hariRentang = 24;
                        using (var cmd = new SqlCommand("SELECT hari FROM perputaran_rod", conn, trans))
                        {
                            object v = await cmd.ExecuteScalarAsync();
                            if (v != null && v != DBNull.Value)
                                hariRentang = Convert.ToInt32(v);
                        }

                        DateTime? tanggalBentrok = null;

                        using (var cmd = new SqlCommand(@"
                    SELECT TOP 1 tanggal_penerimaan
                    FROM penerimaan_p
                    WHERE nomor_rod = @rod
                    AND tanggal_penerimaan BETWEEN DATEADD(DAY,-@h,@tgl)
                                                 AND DATEADD(DAY,@h,@tgl)", conn, trans))
                        {
                            cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = nomorRodFix;
                            cmd.Parameters.Add("@tgl", SqlDbType.DateTime).Value = MainForm.Instance.tanggal;
                            cmd.Parameters.Add("@h", SqlDbType.Int).Value = hariRentang;

                            object v = await cmd.ExecuteScalarAsync();
                            if (v != null && v != DBNull.Value)
                                tanggalBentrok = Convert.ToDateTime(v);
                        }

                        if (tanggalBentrok.HasValue)
                        {
                            string tglBentrok = tanggalBentrok.Value.ToString("dd-MM-yyyy HH:mm:ss");

                            if (MessageBox.Show(
                                $"Nomor ROD {nomorRodFix} sudah pernah diterima pada tanggal {tglBentrok}\n" +
                                $"(dalam rentang ±{hariRentang} hari).\n\n" +
                                $"Tetap lanjutkan?",
                                "Peringatan Duplikat ROD",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning) != DialogResult.OK)
                            {
                                trans.Rollback();
                                return;
                            }
                        }

                        string[] tabel = { "penerimaan_s", "penerimaan_p", "penerimaan_m" };

                        foreach (string t in tabel)
                        {
                            using (var cmd = new SqlCommand($@"
                        INSERT INTO {t}
                        (tanggal_penerimaan, shift, nomor_rod, jenis, stasiun,
                         e1, e2, e3, s, d, b, ba, cr, m, r, c, rl,
                         jumlah, updated_at, remaks, catatan)
                        VALUES
                        (@tanggal, @shift, @nomorrod, @jenis, @stasiun,
                         @e1, @e2, @e3, @s, @d, @b, @ba, @cr, @m, @r, @c, @rl,
                         @jumlah, GETDATE(), @remaks, @catatan)", conn, trans))
                            {
                                cmd.Parameters.Add("@tanggal", SqlDbType.DateTime).Value = MainForm.Instance.tanggal;
                                cmd.Parameters.Add("@shift", SqlDbType.VarChar).Value = MainForm.Instance.lblshift.Text;
                                cmd.Parameters.Add("@nomorrod", SqlDbType.VarChar).Value = nomorRodFix;
                                cmd.Parameters.Add("@jenis", SqlDbType.VarChar).Value = txtjenis.Text;
                                cmd.Parameters.Add("@stasiun", SqlDbType.VarChar).Value = txtstasiun.Text;

                                cmd.Parameters.Add("@e1", SqlDbType.Int).Value = ToInt(txte1.Text);
                                cmd.Parameters.Add("@e2", SqlDbType.Int).Value = ToInt(txte2.Text);
                                cmd.Parameters.Add("@e3", SqlDbType.Int).Value = ToInt(txte3.Text);
                                cmd.Parameters.Add("@s", SqlDbType.Int).Value = ToInt(txts.Text);
                                cmd.Parameters.Add("@d", SqlDbType.Int).Value = ToInt(txtd.Text);
                                cmd.Parameters.Add("@b", SqlDbType.Int).Value = ToInt(txtb.Text);
                                cmd.Parameters.Add("@ba", SqlDbType.Int).Value = ToInt(txtba.Text);
                                cmd.Parameters.Add("@cr", SqlDbType.Int).Value = ToInt(txtcr.Text);
                                cmd.Parameters.Add("@m", SqlDbType.Int).Value = ToInt(txtm.Text);
                                cmd.Parameters.Add("@r", SqlDbType.Int).Value = ToInt(txtr.Text);
                                cmd.Parameters.Add("@c", SqlDbType.Int).Value = ToInt(txtc.Text);
                                cmd.Parameters.Add("@rl", SqlDbType.Int).Value = ToInt(txtrl.Text);
                                cmd.Parameters.Add("@jumlah", SqlDbType.Int).Value = ToInt(lbltotal.Text);

                                cmd.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;
                                cmd.Parameters.Add("@catatan", SqlDbType.VarChar).Value = txtcatatan.Text;

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        trans.Commit();

                        await Task.Yield();

                        MessageBox.Show("Data berhasil disimpan.",
                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Gagal menyimpan: " + ex.Message);
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Gagal Simpan Data.");
            }
        }


        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtnomorrod.Text) ||
                string.IsNullOrWhiteSpace(txtjenis.Text) ||
                string.IsNullOrWhiteSpace(txtstasiun.Text))
            {
                MessageBox.Show(
                    "Nomor ROD, Jenis dan Stasiun Tidak Boleh Kosong",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            btnsimpan.Enabled = false;
            btncancel.Enabled = false;

            await simpandata();

            delay = 0;

            this.SuspendLayout();
            setdefault();
            this.ResumeLayout();

            btnsimpan.Enabled = false;
            btncancel.Enabled = false;
            txtnomorrod.Focus();

            delay = 300;
        }


        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                switch (table)
                {
                    case "penerimaan_s":
                        if (!isSearching)
                        {
                            await HitungTotalData();
                            currentPage = 1;
                            await tampilpenerimaan();
                        }
                        else
                        {
                            int oldTotal = searchTotalRecords;
                            await HitungTotalDataPencarian();
                            if (searchTotalRecords > oldTotal)
                                await tampilpenerimaan();
                        }
                        break;

                    case "perputaran_rod":
                        await tampilperputaranrod();
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

        private async Task tampilperputaranrod()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    string query = "SELECT hari FROM perputaran_rod";
                    SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    lblhari.Text = dt.Rows.Count > 0
                        ? "Perputaran ROD " + dt.Rows[0]["hari"].ToString() + " Hari"
                        : "Data tidak ditemukan";
                }
            }
            catch
            {

            }
        }

        private async Task tampilpenerimaan()
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
                SELECT no, tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, 
                       e1, e2, e3, s, d, b, ba, r, m, cr, c, rl, jumlah, updated_at, remaks, catatan
                FROM penerimaan_s
                ORDER BY tanggal_penerimaan DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                    }
                    else
                    {
                        query = $@"
                SELECT no, tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, 
                       e1, e2, e3, s, d, b, ba, r, m, cr, c, rl, jumlah, updated_at, remaks, catatan
                {lastSearchWhere}
                ORDER BY tanggal_penerimaan DESC
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeRows = false;

            if (dt.Columns.Count >= 22)
            {
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
            }

            lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

            btnleft.Enabled = currentPage > 1;
            btnright.Enabled = currentPage < totalPages;
        }

        private async void Penerimaan_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            txtnomorrod.Focus();
            setdefault();

            btnsimpan.Enabled = false;
            if (MainForm.Instance != null && MainForm.Instance.role != null)
            {
                btnaturjam.Visible = (MainForm.Instance.role == "Manajer" || MainForm.Instance.role == "Developer");
            }
            else
            {
                btnaturjam.Visible = false;
            }

            await HitungTotalData();
            await tampilpenerimaan();
            await tampilperputaranrod();
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void setdefault()
        {
            txtnomorrod.Clear();
            txtjenis.Clear();
            txtstasiun.Clear();
            txte1.Clear();
            txte2.Clear();
            txte3.Clear();
            txts.Clear();
            txtd.Clear();
            txtb.Clear();
            txtba.Clear();
            txtr.Clear();
            txtm.Clear();
            txtcr.Clear();
            txtc.Clear();
            txtrl.Clear();
            txtcatatan.Clear();
            txtcari.Clear();
            lbltotal.Text = "-";
        }

        private void hitung()
        {
            int angka1 = SafeParse(txte1);
            int angka2 = SafeParse(txte2);
            int angka3 = SafeParse(txte3);
            int angka4 = 0;
            int angka4a = SafeParse(txts);
            if(angka4a > 0)
            {
                angka4 = 1;
            }
            else
            {
                angka4 = 0;
            }
            int angka5 = SafeParse(txtd);
            int angka6 = SafeParse(txtb);
            int angka7 = SafeParse(txtba);
            int angka8 = 0;
            int angka8a = SafeParse(txtcr);
            if (angka8a > 0) 
            { 
                angka8 = 1;
            }
            else
            {
                angka8 = 0;
            }
            int angka9 = SafeParse(txtm);
            int angka10 = SafeParse(txtr);
            int angka11 = 0;
            int angka11a = SafeParse(txtc);
            if (angka11a > 0)
            {
                angka11 = 1;
            }
            else
            {
                angka11 = 0;
            }
            int angka12 = SafeParse(txtrl);

            int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12;
            lbltotal.Text = hasil.ToString();
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            setdefault();
            btnsimpan.Text = "Simpan Data";
            btncancel.Enabled = false;
            btnsimpan.Enabled = false;
            dataGridView1.ClearSelection();
        }

        private void txtrl_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte3_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txts_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtd_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtb_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtba_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtm_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }
        private void txtcr_TextChanged_1(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtc_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte1_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtrl_TextChanged_1(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtnomorrod_TextChanged(object sender, EventArgs e)
        {
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void txtjenis_TextChanged(object sender, EventArgs e)
        {
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void txtstasiun_TextChanged(object sender, EventArgs e)
        {
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void Penerimaan_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private CancellationTokenSource ctsCari = null;
        int delay = 300;

        private async void txtcari_TextChanged(object sender, EventArgs e)
        {
            string inputRod = txtcari.Text.Trim();

            ctsCari?.Cancel();
            ctsCari = new CancellationTokenSource();
            var token = ctsCari.Token;

            try
            {
                await Task.Delay(delay, token);
            }
            catch (TaskCanceledException)
            {
                return; 
            }

            currentPage = 1;

            if (string.IsNullOrEmpty(inputRod))
            {
                isSearching = false;
                await HitungTotalData();
            }
            else
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM penerimaan_s WHERE nomor_rod LIKE @rod";
                lastSearchCmd.Parameters.Clear();
                lastSearchCmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");
                await HitungTotalDataPencarian();
            }

            await tampilpenerimaan();
        }

        private async void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await tampilpenerimaan();
            }
        }

        private async void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                await tampilpenerimaan();
            }
        }

        private void txtcatatan_TextChanged(object sender, EventArgs e)
        {
            btnsimpan.Enabled = true;
            btncancel.Enabled = true;
        }

        private void btnaturjam_Click(object sender, EventArgs e)
        {
            Form setperputaran = new setperputaran_rod();
            setperputaran.ShowDialog();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnsimpan.PerformClick();
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }
    }
}
