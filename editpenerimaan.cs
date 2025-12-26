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
            catch
            {
                return;
            }
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
            btnupdate.Enabled = true;
            btnclear.Enabled = true;
            btndelete.Enabled = true;
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

        private int ToInt(string text)
        {
            return int.TryParse(text, out int val) ? val : 0;
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
                    string cekRodQuery = @"
                SELECT sumber FROM (
                    SELECT 'penerimaan_s' AS sumber FROM penerimaan_s WHERE nomor_rod=@rod AND no<>@no
                    UNION ALL
                    SELECT 'perbaikan_s' FROM perbaikan_s WHERE nomor_rod=@rod AND no<>@no
                ) x";

                    using (var cmd = new SqlCommand(cekRodQuery, conn, trans))
                    {
                        cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text;
                        cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;

                        object hasil = await cmd.ExecuteScalarAsync();
                        if (hasil != null)
                        {
                            MessageBox.Show(
                                hasil.ToString() == "penerimaan_s"
                                ? "Nomor ROD ini sudah ada di data penerimaan dan belum diperbaiki."
                                : "Nomor ROD ini sudah ada di data perbaikan dan belum dikirim.",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            trans.Rollback();
                            return;
                        }
                    }

                    if (!txtnomorrod.Text.Equals(nomorrodsebelumnya, StringComparison.OrdinalIgnoreCase))
                    {
                        using (var cmd = new SqlCommand(@"
                    SELECT COUNT(*) FROM penerimaan_p
                    WHERE nomor_rod=@rod
                      AND CONVERT(date,tanggal_penerimaan)=CONVERT(date,@tgl)
                      AND no<>@no", conn, trans))
                        {
                            cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text;
                            cmd.Parameters.Add("@tgl", SqlDbType.DateTime).Value = tanggalpenerimaan;
                            cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;

                            if ((int)await cmd.ExecuteScalarAsync() > 0)
                            {
                                MessageBox.Show("Nomor ROD sudah diterima di tanggal yang sama.",
                                                "Duplikat",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                trans.Rollback();
                                return;
                            }
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
                                                 AND DATEADD(DAY,@h,@tgl)
                      AND no <> @no
                    ORDER BY tanggal_penerimaan", conn, trans))
                    {
                        cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text.Trim();
                        cmd.Parameters.Add("@tgl", SqlDbType.DateTime).Value = tanggalpenerimaan;
                        cmd.Parameters.Add("@h", SqlDbType.Int).Value = hariRentang;
                        cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;

                        object v = await cmd.ExecuteScalarAsync();
                        if (v != null && v != DBNull.Value)
                            tanggalBentrok = Convert.ToDateTime(v);
                    }

                    if (tanggalBentrok.HasValue)
                    {
                        string tglBentrok = tanggalBentrok.Value.ToString("dd-MM-yyyy HH:mm:ss");

                        if (MessageBox.Show(
                            $"Nomor ROD {txtnomorrod.Text} sudah pernah diterima pada tanggal {tglBentrok}\n" +
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

                    using (var cmd = new SqlCommand(@"
                UPDATE penerimaan_p SET
                    nomor_rod=@rod, jenis=@jenis, stasiun=@stasiun,
                    e1=@e1,e2=@e2,e3=@e3,s=@s,d=@d,b=@b,ba=@ba,cr=@cr,
                    m=@m,r=@r,c=@c,rl=@rl,jumlah=@jumlah,
                    remaks=@remaks, catatan=@catatan, updated_at=GETDATE()
                WHERE no=@no", conn, trans))
                    {
                        cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text;
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
                        cmd.Parameters.Add("@jumlah", SqlDbType.Int).Value = ToInt(lbltotalupdate.Text);

                        cmd.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;
                        cmd.Parameters.Add("@catatan", SqlDbType.VarChar).Value = txtcatatan.Text;
                        cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;

                        await cmd.ExecuteNonQueryAsync();
                    }

                    SqlCommand log1 = new SqlCommand("INSERT INTO penerimaan_e SELECT * FROM penerimaan_p WHERE no=@no", conn, trans);
                    log1.Parameters.AddWithValue("@no", noprimary);
                    await log1.ExecuteNonQueryAsync();

                    int adaPS = 0, adaPP = 0, adaPB = 0, adaPG = 0;

                    using (var cmd = new SqlCommand(@"
                SELECT
                    (SELECT COUNT(*) FROM penerimaan_s WHERE no=@no),
                    (SELECT COUNT(*) FROM perbaikan_p WHERE no=@no),
                    (SELECT COUNT(*) FROM perbaikan_s WHERE no=@no),
                    (SELECT COUNT(*) FROM pengiriman WHERE no=@no)", conn, trans))
                    {
                        cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                        using (var r = await cmd.ExecuteReaderAsync())
                        {
                            if (await r.ReadAsync())
                            {
                                adaPS = r.GetInt32(0);
                                adaPP = r.GetInt32(1);
                                adaPB = r.GetInt32(2);
                                adaPG = r.GetInt32(3);
                            }
                        }
                    }

                    if (adaPS > 0)
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE penerimaan_s SET nomor_rod=@rod, jenis=@jenis, stasiun=@stasiun,e1=@e1,e2=@e2,e3=@e3,s=@s,d=@d,b=@b,ba=@ba,cr=@cr," +
                            "m=@m,r=@r,c=@c,rl=@rl,jumlah=@jumlah,remaks=@remaks, catatan=@catatan, updated_at=GETDATE() WHERE no=@no",
                            conn, trans))
                        {
                            cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text;
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
                            cmd.Parameters.Add("@jumlah", SqlDbType.Int).Value = ToInt(lbltotalupdate.Text);

                            cmd.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;
                            cmd.Parameters.Add("@catatan", SqlDbType.VarChar).Value = txtcatatan.Text;
                            cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }

                    if (adaPP > 0)
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE perbaikan_p SET nomor_rod=@rod, remaks=@r, updated_at=GETDATE() WHERE no=@no",
                            conn, trans))
                        {
                            cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text;
                            cmd.Parameters.Add("@r", SqlDbType.VarChar).Value = loginform.login.name;
                            cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        SqlCommand log3 = new SqlCommand("INSERT INTO perbaikan_e SELECT * FROM perbaikan_p WHERE no=@no", conn, trans);
                        log3.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                        await log3.ExecuteNonQueryAsync();
                    }

                    if (adaPB > 0)
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE perbaikan_s SET nomor_rod=@rod, remaks=@r, updated_at=GETDATE() WHERE no=@no",
                            conn, trans))
                        {
                            cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text;
                            cmd.Parameters.Add("@r", SqlDbType.VarChar).Value = loginform.login.name;
                            cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }

                    if (adaPG > 0)
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE pengiriman SET nomor_rod=@rod, remaks=@r, updated_at=GETDATE() WHERE no=@no",
                            conn, trans))
                        {
                            cmd.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text;
                            cmd.Parameters.Add("@r", SqlDbType.VarChar).Value = loginform.login.name;
                            cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }

                    trans.Commit();

                    await Task.Yield();

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
                    btnupdate.Enabled = false;
                    btnclear.Enabled = false;
                    btndelete.Enabled = false;
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
                                cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
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

            btnupdate.Enabled = false;
            btnclear.Enabled = false;
            btndelete.Enabled = false;
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

        private void txtjenis_TextChanged(object sender, EventArgs e)
        {
            btnupdate.Enabled = true;
            btnclear.Enabled = true;
            btndelete.Enabled = true;
        }

        private void txtnomorrod_TextChanged(object sender, EventArgs e)
        {
            btnupdate.Enabled = true;
            btnclear.Enabled = true;
            btndelete.Enabled = true;
        }

        private void txtstasiun_TextChanged(object sender, EventArgs e)
        {
            btnupdate.Enabled = true;
            btnclear.Enabled = true;
            btndelete.Enabled = true;
        }
    }
}
