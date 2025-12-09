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
                    SELECT 'penerimaan_s' AS sumber FROM penerimaan_s WHERE nomor_rod = @rod
                    UNION
                    SELECT 'perbaikan_s' AS sumber FROM perbaikan_s WHERE nomor_rod = @rod";

                        using (var cmd = new SqlCommand(queryCek, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@rod", nomorRodFix);
                            object sumber = await cmd.ExecuteScalarAsync();

                            if (sumber != null)
                            {
                                string pesan = (sumber.ToString() == "penerimaan_s")
                                    ? "Nomor ROD ini sudah ada di data penerimaan dan belum diperbaiki."
                                    : "Nomor ROD ini sudah ada di data perbaikan dan belum dikirim.";

                                MessageBox.Show(pesan, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            cmdCekTanggal.Parameters.AddWithValue("@nomor_rod", nomorRodFix);
                            cmdCekTanggal.Parameters.AddWithValue("@tgl", MainForm.Instance.tanggal);

                            if ((int)await cmdCekTanggal.ExecuteScalarAsync() > 0)
                            {
                                MessageBox.Show(
                                    $"Nomor ROD {nomorRodFix} sudah pernah diterima pada tanggal yang sama.",
                                    "Tanggal Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                trans.Rollback();
                                return;
                            }
                        }

                        int hariRentang = 24;
                        using (var cmdHari = new SqlCommand("SELECT hari FROM perputaran_rod", conn, trans))
                        {
                            object val = await cmdHari.ExecuteScalarAsync();
                            if (val != null && val != DBNull.Value)
                                hariRentang = Convert.ToInt32(val);
                        }

                        using (var cmdRentang = new SqlCommand(@"
                    SELECT COUNT(*) 
                    FROM penerimaan_p
                    WHERE nomor_rod = @nomor_rod
                    AND tanggal_penerimaan BETWEEN DATEADD(DAY, -@r, @tgl)
                                              AND DATEADD(DAY, @r, @tgl)", conn, trans))
                        {
                            cmdRentang.Parameters.AddWithValue("@nomor_rod", nomorRodFix);
                            cmdRentang.Parameters.AddWithValue("@tgl", MainForm.Instance.tanggal);
                            cmdRentang.Parameters.AddWithValue("@r", hariRentang);

                            if ((int)await cmdRentang.ExecuteScalarAsync() > 0)
                            {
                                DialogResult r = MessageBox.Show(
                                    $"Nomor ROD {nomorRodFix} sudah pernah diterima dalam rentang ±{hariRentang} hari.\nLanjutkan?",
                                    "Peringatan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                                if (r != DialogResult.OK)
                                {
                                    trans.Rollback();
                                    return;
                                }
                            }
                        }

                        string[] tabel = { "penerimaan_s", "penerimaan_p", "penerimaan_m" };

                        foreach (string t in tabel)
                        {
                            using (var cmd = new SqlCommand($@"
                        INSERT INTO {t}
                        (tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, e1, e2, e3, 
                         s, d, b, ba, cr, m, r, c, rl, jumlah, updated_at, remaks, catatan)
                        VALUES
                        (@tanggal_penerimaan, @shift, @nomorrod, @jenis, @stasiun, @e1, @e2, @e3,
                         @s, @d, @b, @ba, @cr, @m, @r, @c, @rl, @jumlah, @updated, @remaks, @catatan)", conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@tanggal_penerimaan", MainForm.Instance.tanggal);
                                cmd.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                                cmd.Parameters.AddWithValue("@nomorrod", nomorRodFix);
                                cmd.Parameters.AddWithValue("@jenis", txtjenis.Text);
                                cmd.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                                cmd.Parameters.AddWithValue("@e1", txte1.Text);
                                cmd.Parameters.AddWithValue("@e2", txte2.Text);
                                cmd.Parameters.AddWithValue("@e3", txte3.Text);
                                cmd.Parameters.AddWithValue("@s", txts.Text);
                                cmd.Parameters.AddWithValue("@d", txtd.Text);
                                cmd.Parameters.AddWithValue("@b", txtb.Text);
                                cmd.Parameters.AddWithValue("@ba", txtba.Text);
                                cmd.Parameters.AddWithValue("@cr", txtcr.Text);
                                cmd.Parameters.AddWithValue("@m", txtm.Text);
                                cmd.Parameters.AddWithValue("@r", txtr.Text);
                                cmd.Parameters.AddWithValue("@c", txtc.Text);
                                cmd.Parameters.AddWithValue("@rl", txtrl.Text);
                                cmd.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                                cmd.Parameters.AddWithValue("@updated", MainForm.Instance.tanggal);
                                cmd.Parameters.AddWithValue("@remaks", loginform.login.name);
                                cmd.Parameters.AddWithValue("@catatan", txtcatatan.Text);

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        trans.Commit();

                        MessageBox.Show("Data berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Simpan Data.");
                return;
            }
        }


        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "" || txtstasiun.Text == "")
            {
                MessageBox.Show("Nomor ROD, Jenis dan Stasiun Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnsimpan.Enabled = false;
            btncancel.Enabled = false;
            await simpandata();
            setdefault();
            btnsimpan.Enabled = false;
            btncancel.Enabled = false;
            txtnomorrod.Focus();
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
            catch { }
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

            await HitungTotalData();
            await tampilpenerimaan();
            await tampilperputaranrod();

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

        private int SafeParse(Guna2TextBox tb)
        {
            return int.TryParse(tb.Text, out int result) ? result : 0;
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

        private async void txtcari_TextChanged(object sender, EventArgs e)
        {
            string inputRod = txtcari.Text.Trim();

            ctsCari?.Cancel();
            ctsCari = new CancellationTokenSource();
            var token = ctsCari.Token;

            try
            {
                await Task.Delay(300, token);
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
