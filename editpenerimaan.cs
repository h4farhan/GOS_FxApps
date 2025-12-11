using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Guna.UI2.WinForms;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class editpenerimaan : Form
    {
        DateTime tanggalpenerimaan;
        int shift;
        int noprimary;

        bool infocari = false;
        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;
        string nomorrodsebelumnya = null;
        private bool isEditing = false;

        public editpenerimaan()
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
                    case "penerimaan_p":
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
                string query = "SELECT COUNT(*) FROM penerimaan_p";
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
                FROM penerimaan_p
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

        private async Task<bool> cari()
        {
            DateTime? tanggal = dateeditpenerimaan.Checked ? (DateTime?)dateeditpenerimaan.Value.Date : null;
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.", "Peringatan");
                return false; 
            }
            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM penerimaan_p WHERE 1=1 ";

                if (tanggal.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal_penerimaan AS DATE) = @tgl ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(inputRod))
                {
                    lastSearchWhere += " AND nomor_rod LIKE @rod ";
                    lastSearchCmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");
                }

                await HitungTotalDataPencarian();
                currentPage = 1;
                await tampilpenerimaan();

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

        private void clearedit()
        {
            txtnomorrod.Enabled = true; 
            txtnomorrod.Clear(); 
            txtnomorrod.Enabled = false;
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
            lbltotalsebelum.Text = "-";
            lbltotalupdate.Text = "-";
            txtcatatan.Clear();

            nomorrodsebelumnya = null;
        }

        private void resetsearchui()
        {
            txtcari.Clear();
            btncari.Text = "Cari";
        }


        private void settrue()
        {
            txtnomorrod.Enabled = true;
            txtjenis.Enabled = true;
            txtstasiun.Enabled = true;
            txte1.Enabled = true;
            txte2.Enabled = true;
            txte3.Enabled = true;
            txts.Enabled = true;
            txtd.Enabled = true;
            txtb.Enabled = true;
            txtba.Enabled = true;
            txtr.Enabled = true;
            txtm.Enabled = true;
            txtcr.Enabled = true;
            txtc.Enabled = true;
            txtrl.Enabled = true;
            txtcatatan.Enabled = true;
        }

        private void setfalse()
        {
            txtjenis.Enabled = false;
            txtstasiun.Enabled = false;
            txte1.Enabled = false;
            txte2.Enabled = false;
            txte3.Enabled = false;
            txts.Enabled = false;
            txtd.Enabled = false;
            txtb.Enabled = false;
            txtba.Enabled = false;
            txtr.Enabled = false;
            txtm.Enabled = false;
            txtcr.Enabled = false;
            txtc.Enabled = false;
            txtrl.Enabled = false;
            txtcatatan.Enabled = false;
        }

        private async void editpenerimaan_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            dateeditpenerimaan.Value = DateTime.Now.Date;
            dateeditpenerimaan.Checked = false;

            await HitungTotalData();
            await tampilpenerimaan();
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
                isSearching = false;

                txtcari.Text = "";

                btncari.Text = "Cari";

                await HitungTotalData();
                currentPage = 1;
                await tampilpenerimaan();

                infocari = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["no"].Value);
                tanggalpenerimaan = Convert.ToDateTime(row.Cells["tanggal_penerimaan"].Value);
                shift = Convert.ToInt32(row.Cells["shift"].Value);
                txtnomorrod.Text = row.Cells["nomor_rod"].Value.ToString();
                nomorrodsebelumnya = row.Cells["nomor_rod"].Value.ToString();
                txtjenis.Text = row.Cells["jenis"].Value.ToString();
                txtstasiun.Text = row.Cells["stasiun"].Value.ToString();
                txte1.Text = row.Cells["e1"].Value.ToString();
                txte2.Text = row.Cells["e2"].Value.ToString();
                txte3.Text = row.Cells["e3"].Value.ToString();
                txts.Text = row.Cells["s"].Value.ToString();
                txtd.Text = row.Cells["d"].Value.ToString();
                txtb.Text = row.Cells["b"].Value.ToString();
                txtba.Text = row.Cells["ba"].Value.ToString();
                txtcr.Text = row.Cells["cr"].Value.ToString();
                txtm.Text = row.Cells["m"].Value.ToString();
                txtr.Text = row.Cells["r"].Value.ToString();
                txtc.Text = row.Cells["c"].Value.ToString();
                txtrl.Text = row.Cells["rl"].Value.ToString();
                lbltotalsebelum.Text = row.Cells["jumlah"].Value.ToString();
                txtcatatan.Text = row.Cells["catatan"].Value.ToString();

                btnupdate.Enabled = true;
                btndelete.Enabled = true;
                btnclear.Enabled = true;
                settrue();
                txtjenis.Focus();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clearedit();
            btnclear.Enabled = false;
            btnupdate.Enabled = false;
            btndelete.Enabled = false;
            setfalse();
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
            if (angka4a > 0)
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
            lbltotalupdate.Text = hasil.ToString();
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
                btnupdate.PerformClick();
            }
        }

        private async void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "" || txtstasiun.Text == "")
            {
                MessageBox.Show("Nomor ROD, Jenis, dan Stasiun Tidak Boleh Kosong",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnupdate.Enabled = false;
            btnclear.Enabled = false;
            btndelete.Enabled = false;

            using (var conn = await Koneksi.GetConnectionAsync())
            using (var trans = conn.BeginTransaction())
            {
                isEditing = true;
                try
                {
                    string query = @"
                SELECT 'penerimaan_s' AS sumber, nomor_rod 
                FROM penerimaan_s 
                WHERE nomor_rod = @rod AND no <> @no
                UNION
                SELECT 'perbaikan_s' AS sumber, nomor_rod 
                FROM perbaikan_s
                WHERE nomor_rod = @rod AND no <> @no";

                    using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@rod", txtnomorrod.Text);
                        cmd.Parameters.AddWithValue("@no", noprimary);

                        string sumber = null;

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                                sumber = dr["sumber"].ToString();
                        }

                        if (sumber != null)
                        {
                            MessageBox.Show(
                                sumber == "penerimaan_s"
                                ? "Nomor ROD ini sudah ada di data penerimaan dan belum diperbaiki."
                                : "Nomor ROD ini sudah ada di data perbaikan dan belum dikirim.",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            trans.Rollback();

                            return;
                        }
                    }

                    if (!txtnomorrod.Text.Trim().Equals(nomorrodsebelumnya, StringComparison.OrdinalIgnoreCase))
                    {
                        using (SqlCommand cmdCekTanggal = new SqlCommand(@"
                    SELECT COUNT(*) 
                    FROM penerimaan_p
                    WHERE nomor_rod = @nomor_rod
                      AND CONVERT(date, tanggal_penerimaan) = CONVERT(date, @tgl)
                      AND no <> @no", conn, trans))
                        {
                            cmdCekTanggal.Parameters.AddWithValue("@nomor_rod", txtnomorrod.Text);
                            cmdCekTanggal.Parameters.AddWithValue("@tgl", MainForm.Instance.tanggal);
                            cmdCekTanggal.Parameters.AddWithValue("@no", noprimary);

                            int sudahAda = (int)await cmdCekTanggal.ExecuteScalarAsync();

                            if (sudahAda > 0)
                            {
                                MessageBox.Show(
                                    $"Nomor ROD {txtnomorrod.Text} sudah pernah diterima pada tanggal yang sama ({MainForm.Instance.tanggal:dd MMMM yyyy}).",
                                    "Tanggal Duplikat",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                trans.Rollback();

                                return;
                            }
                        }
                    }

                    int hariRentang = 24;

                    using (SqlCommand cmdHari = new SqlCommand("SELECT hari FROM perputaran_rod", conn, trans))
                    {
                        object val = await cmdHari.ExecuteScalarAsync();
                        if (val != null && val != DBNull.Value)
                            hariRentang = Convert.ToInt32(val);
                    }

                    using (SqlCommand cmdCekRentang = new SqlCommand(@"
                SELECT COUNT(*) 
                FROM penerimaan_p
                WHERE nomor_rod = @nomor_rod
                  AND tanggal_penerimaan BETWEEN DATEADD(DAY, -@rentang, @tgl) 
                                            AND DATEADD(DAY, @rentang, @tgl)
                  AND no <> @no", conn, trans))
                    {
                        cmdCekRentang.Parameters.AddWithValue("@nomor_rod", txtnomorrod.Text);
                        cmdCekRentang.Parameters.AddWithValue("@tgl", MainForm.Instance.tanggal);
                        cmdCekRentang.Parameters.AddWithValue("@rentang", hariRentang);
                        cmdCekRentang.Parameters.AddWithValue("@no", noprimary);

                        int terlaluDekat = (int)await cmdCekRentang.ExecuteScalarAsync();

                        if (terlaluDekat > 0)
                        {
                            if (MessageBox.Show(
                                $"Nomor ROD {txtnomorrod.Text} sudah pernah diterima dalam rentang ±{hariRentang} hari.\n" +
                                $"Tetap lanjutkan?",
                                "Peringatan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                            {
                                trans.Rollback();
                                return;
                            }
                        }
                    }

                    SqlCommand cmd1 = new SqlCommand(@"
                UPDATE penerimaan_p 
                SET nomor_rod=@nomorrod, jenis=@jenis, stasiun=@stasiun, 
                    e1=@e1, e2=@e2, e3=@e3, s=@s, d=@d, b=@b, ba=@ba, cr=@cr, 
                    m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah,
                    remaks=@remaks, updated_at=@diubah, catatan=@catatan
                WHERE no=@no", conn, trans);

                    cmd1.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd1.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd1.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                    cmd1.Parameters.AddWithValue("@e1", txte1.Text);
                    cmd1.Parameters.AddWithValue("@e2", txte2.Text);
                    cmd1.Parameters.AddWithValue("@e3", txte3.Text);
                    cmd1.Parameters.AddWithValue("@s", txts.Text);
                    cmd1.Parameters.AddWithValue("@d", txtd.Text);
                    cmd1.Parameters.AddWithValue("@b", txtb.Text);
                    cmd1.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd1.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd1.Parameters.AddWithValue("@m", txtm.Text);
                    cmd1.Parameters.AddWithValue("@r", txtr.Text);
                    cmd1.Parameters.AddWithValue("@c", txtc.Text);
                    cmd1.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd1.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                    cmd1.Parameters.AddWithValue("@remaks", loginform.login.name);
                    cmd1.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd1.Parameters.AddWithValue("@catatan", txtcatatan.Text);
                    cmd1.Parameters.AddWithValue("@no", noprimary);

                    await cmd1.ExecuteNonQueryAsync();

                    SqlCommand log1 = new SqlCommand("INSERT INTO penerimaan_e SELECT * FROM penerimaan_p WHERE no=@no", conn, trans);
                    log1.Parameters.AddWithValue("@no", noprimary);
                    await log1.ExecuteNonQueryAsync();

                    // 2. Update penerimaan_s (Hanya kalau ada)
                    SqlCommand cek2 = new SqlCommand("SELECT COUNT(*) FROM penerimaan_s WHERE no=@no", conn, trans);
                    cek2.Parameters.AddWithValue("@no", noprimary);
                    int ada2 = (int)await cek2.ExecuteScalarAsync();

                    if (ada2 > 0)
                    {
                        SqlCommand cmd2 = new SqlCommand(@"
            UPDATE penerimaan_s
            SET nomor_rod = @nomorrod, jenis=@jenis, stasiun=@stasiun, e1=@e1, e2=@e2, e3=@e3, s=@s, d=@d, 
                b=@b, ba=@ba, cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah,
                remaks=@remaks, updated_at=@diubah, catatan = @catatan
            WHERE no=@no", conn, trans);

                        cmd2.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        cmd2.Parameters.AddWithValue("@jenis", txtjenis.Text);
                        cmd2.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                        cmd2.Parameters.AddWithValue("@e1", txte1.Text);
                        cmd2.Parameters.AddWithValue("@e2", txte2.Text);
                        cmd2.Parameters.AddWithValue("@e3", txte3.Text);
                        cmd2.Parameters.AddWithValue("@s", txts.Text);
                        cmd2.Parameters.AddWithValue("@d", txtd.Text);
                        cmd2.Parameters.AddWithValue("@b", txtb.Text);
                        cmd2.Parameters.AddWithValue("@ba", txtba.Text);
                        cmd2.Parameters.AddWithValue("@cr", txtcr.Text);
                        cmd2.Parameters.AddWithValue("@m", txtm.Text);
                        cmd2.Parameters.AddWithValue("@r", txtr.Text);
                        cmd2.Parameters.AddWithValue("@c", txtc.Text);
                        cmd2.Parameters.AddWithValue("@rl", txtrl.Text);
                        cmd2.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                        cmd2.Parameters.AddWithValue("@remaks", loginform.login.name);
                        cmd2.Parameters.AddWithValue("@no", noprimary);
                        cmd2.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        cmd2.Parameters.AddWithValue("@catatan", txtcatatan.Text);
                        await cmd2.ExecuteNonQueryAsync();
                    }

                    // 3. Update perbaikan_p (Hanya kalau ada)
                    SqlCommand cek3 = new SqlCommand("SELECT COUNT(*) FROM perbaikan_p WHERE no=@no", conn, trans);
                    cek3.Parameters.AddWithValue("@no", noprimary);
                    int ada3 = (int)await cek3.ExecuteScalarAsync();

                    if (ada3 > 0)
                    {
                        SqlCommand cmd3 = new SqlCommand(@"
            UPDATE perbaikan_p 
            SET nomor_rod=@nomorrod, remaks=@remaks, updated_at=@diubah
            WHERE no=@no", conn, trans);
                        cmd3.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        cmd3.Parameters.AddWithValue("@remaks", loginform.login.name);
                        cmd3.Parameters.AddWithValue("@no", noprimary);
                        cmd3.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        await cmd3.ExecuteNonQueryAsync();

                        SqlCommand log3 = new SqlCommand("INSERT INTO perbaikan_e SELECT * FROM perbaikan_p WHERE no=@no", conn, trans);
                        log3.Parameters.AddWithValue("@no", noprimary);
                        await log3.ExecuteNonQueryAsync();
                    }

                    // 4. Update perbaikan_s (Hanya kalau ada)
                    SqlCommand cek4 = new SqlCommand("SELECT COUNT(*) FROM perbaikan_s WHERE no=@no", conn, trans);
                    cek4.Parameters.AddWithValue("@no", noprimary);
                    int ada4 = (int)await cek4.ExecuteScalarAsync();

                    if (ada4 > 0)
                    {
                        SqlCommand cmd4 = new SqlCommand(@"
            UPDATE perbaikan_s 
            SET nomor_rod=@nomorrod, remaks=@remaks, updated_at=@diubah
            WHERE no=@no", conn, trans);
                        cmd4.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        cmd4.Parameters.AddWithValue("@remaks", loginform.login.name);
                        cmd4.Parameters.AddWithValue("@no", noprimary);
                        cmd4.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        await cmd4.ExecuteNonQueryAsync();

                        SqlCommand log4 = new SqlCommand("INSERT INTO perbaikan_e SELECT * FROM perbaikan_s WHERE no=@no", conn, trans);
                        log4.Parameters.AddWithValue("@no", noprimary);
                        await log4.ExecuteNonQueryAsync();
                    }

                    // 5. Update pengiriman (Hanya kalau ada)
                    SqlCommand cek5 = new SqlCommand("SELECT COUNT(*) FROM pengiriman WHERE no=@no", conn, trans);
                    cek5.Parameters.AddWithValue("@no", noprimary);
                    int ada5 = (int)await cek5.ExecuteScalarAsync();

                    if (ada5 > 0)
                    {
                        SqlCommand cmd5 = new SqlCommand(@"
            UPDATE pengiriman 
            SET nomor_rod=@nomorrod, remaks=@remaks, updated_at=@diubah
            WHERE no=@no", conn, trans);
                        cmd5.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        cmd5.Parameters.AddWithValue("@remaks", loginform.login.name);
                        cmd5.Parameters.AddWithValue("@no", noprimary);
                        cmd5.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                        await cmd5.ExecuteNonQueryAsync();
                    }

                    trans.Commit();

                    MessageBox.Show("Data berhasil diperbarui.", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clearedit();
                    setfalse();

                    if (isSearching)
                    {
                        await HitungTotalDataPencarian();
                        await tampilpenerimaan();
                    }
                    else
                    {
                        await HitungTotalData();
                        await tampilpenerimaan();

                        resetsearchui();
                    }

                }
                catch (SqlException)
                {
                    if (trans.Connection != null)
                        trans.Rollback();
                    MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception)
                {
                    if (trans.Connection != null)
                        trans.Rollback();
                    MessageBox.Show("Gagal mengedit: ");
                    return;
                }
                finally
                {
                    isEditing = false;
                }
            }
        }

        private async Task hapusdata()
        {
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin menghapus data ini?\n\n" +
                "Semua data terkait (penerimaan, perbaikan, pengiriman) juga akan dihapus permanen.",
                "Konfirmasi Hapus Data",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (konfirmasi != DialogResult.Yes)
                return;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var trans = conn.BeginTransaction())
                {
                    isEditing = true;
                    try
                    {
                        string[] deleteOrder =
                        {
                    "pengiriman_m",
                    "pengiriman",

                    "perbaikan_m",
                    "perbaikan_e",
                    "perbaikan_s",
                    "perbaikan_p",

                    "penerimaan_m",
                    "penerimaan_e",
                    "penerimaan_s",
                    "penerimaan_p"
                };

                        foreach (var table in deleteOrder)
                        {
                            using (SqlCommand cmd = new SqlCommand(
                                $"DELETE FROM {table} WHERE no = @no", conn, trans))
                            {
                                cmd.Parameters.Add("@no", SqlDbType.NVarChar).Value = noprimary;
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        trans.Commit();

                        MessageBox.Show(
                            "Semua data penerimaan, perbaikan, dan pengiriman berhasil dihapus permanen.",
                            "Berhasil",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        clearedit();
                        setfalse();

                        if (isSearching)
                        {
                            await HitungTotalDataPencarian();
                            await tampilpenerimaan();
                        }
                        else
                        {
                            await HitungTotalData();
                            await tampilpenerimaan();

                            resetsearchui();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();

                        MessageBox.Show(
                            "Penghapusan dibatalkan karena terjadi kesalahan:\n\n" + ex.Message,
                            "Gagal",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch(SqlException)
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
            finally
            {
                isEditing = false;
            }
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

        private void txtba_TextChanged(object sender, EventArgs e)
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

        private void txtm_TextChanged(object sender, EventArgs e)
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

        private void editpenerimaan_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private async void btndelete_Click(object sender, EventArgs e)
        {
            btnupdate.Enabled = false;
            btndelete.Enabled = false;
            btnclear.Enabled = false;
            await hapusdata();
            clearedit();
            setfalse();
            noprimary = 0;
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

        private void txtr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtcr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }
    }
}
