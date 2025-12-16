using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SqlClient;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace GOS_FxApps
{
    public partial class editperbaikan : Form
    {
        bool infocari = false;
        DateTime tanggalpenerimaan;
        DateTime tanggalperbaikan;
        int shift;
        int noprimary;

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

        public editperbaikan()
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
                    case "perbaikan_p":
                        if (!isSearching)
                        {
                            await HitungTotalData();
                            currentPage = 1;
                            await tampilperbaikan();
                        }
                        else
                        {
                            int oldTotal = searchTotalRecords;
                            await HitungTotalDataPencarian();
                            if (searchTotalRecords > oldTotal)
                                await tampilperbaikan();
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
                string query = "SELECT COUNT(*) FROM perbaikan_p";
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

        private async void editperbaikan_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            dateeditperbaikan.Value = DateTime.Now.Date;
            dateeditperbaikan.Checked = false;

            await HitungTotalData();
            await tampilperbaikan();
        }

        private async Task tampilperbaikan()
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
                    SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, bac, nba, ba, ba1, r, m, cr, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks, catatan
                    FROM perbaikan_p
                    ORDER BY tanggal_perbaikan DESC
                    OFFSET {offset} ROWS
                    FETCH NEXT {pageSize} ROWS ONLY";
                    }
                    else
                    {
                        query = $@"
                    SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, bac, nba, ba, ba1, r, m, cr, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks, catatan
                    {lastSearchWhere}
                    ORDER BY tanggal_perbaikan DESC
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

            if (dt.Columns.Count >= 31)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Perbaikan";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Jenis";
                dataGridView1.Columns[5].HeaderText = "E1 Ers";
                dataGridView1.Columns[6].HeaderText = "E1 Est";
                dataGridView1.Columns[7].HeaderText = "E1 Jumlah";
                dataGridView1.Columns[8].HeaderText = "E2 Ers";
                dataGridView1.Columns[9].HeaderText = "E2 Cst";
                dataGridView1.Columns[10].HeaderText = "E2 Cstub";
                dataGridView1.Columns[11].HeaderText = "E2 Jumlah";
                dataGridView1.Columns[12].HeaderText = "E3";
                dataGridView1.Columns[13].HeaderText = "E4";
                dataGridView1.Columns[14].HeaderText = "S";
                dataGridView1.Columns[15].HeaderText = "D";
                dataGridView1.Columns[16].HeaderText = "B";
                dataGridView1.Columns[17].HeaderText = "BAC";
                dataGridView1.Columns[18].HeaderText = "NBA";
                dataGridView1.Columns[19].HeaderText = "BA";
                dataGridView1.Columns[20].HeaderText = "BA-1";
                dataGridView1.Columns[21].HeaderText = "R";
                dataGridView1.Columns[22].HeaderText = "M";
                dataGridView1.Columns[23].HeaderText = "CR";
                dataGridView1.Columns[24].HeaderText = "C";
                dataGridView1.Columns[25].HeaderText = "RL";
                dataGridView1.Columns[26].HeaderText = "Jumlah";
                dataGridView1.Columns[27].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[28].HeaderText = "Diubah";
                dataGridView1.Columns[29].HeaderText = "Remaks";
                dataGridView1.Columns[30].HeaderText = "Catatan";
            }

            lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

            btnleft.Enabled = currentPage > 1;
            btnright.Enabled = currentPage < totalPages;
        }

        private async Task<bool> cari()
        {
            DateTime? tanggal = dateeditperbaikan.Checked ? (DateTime?)dateeditperbaikan.Value.Date : null;
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM perbaikan_p WHERE 1=1 ";

                if (tanggal.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal_perbaikan AS DATE) = @tgl ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(inputRod))
                {
                    lastSearchWhere += " AND nomor_rod LIKE @rod ";
                    lastSearchCmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");
                }

                await HitungTotalDataPencarian();
                currentPage = 1;
                await tampilperbaikan();

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
            txte1ers.Clear();
            txte1est.Clear();
            txte2ers.Clear();
            txte2cst.Clear();
            txte2cstub.Clear();
            txte3.Clear();
            txte4.Clear();
            txts.Clear();
            txtd.Clear();
            txtb.Clear();
            txtbac.Clear();
            txtnba.Clear();
            txtba1.Clear();
            txtr.Clear();
            txtm.Clear();
            txtcr.Clear();
            txtc.Clear();
            txtrl.Clear();
            txtcatatan.Clear();
            lbltotalsebelum.Text = "-";
            lbltotalupdate.Text = "-";
            lbltotale1.Text = "-";
            lbltotale2.Text = "-";
            lbltotalba.Text = "-";
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
            txte1ers.Enabled = true;
            txte1est.Enabled = true;
            txte2ers.Enabled = true;
            txte2cst.Enabled = true;
            txte2cstub.Enabled = true;
            txte3.Enabled = true;
            txte4.Enabled = true;
            txts.Enabled = true;
            txtd.Enabled = true;
            txtb.Enabled = true;
            txtbac.Enabled = true;
            txtnba.Enabled = true;
            txtba1.Enabled = true;
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
            txte1ers.Enabled = false;
            txte1est.Enabled = false;
            txte2ers.Enabled = false;
            txte2cst.Enabled = false;
            txte2cstub.Enabled = false;
            txte3.Enabled = false;
            txte4.Enabled = false;
            txts.Enabled = false;
            txtd.Enabled = false;
            txtb.Enabled = false;
            txtbac.Enabled = false;
            txtnba.Enabled = false;
            txtba1.Enabled = false;
            txtr.Enabled = false;
            txtm.Enabled = false;
            txtcr.Enabled = false;
            txtc.Enabled = false;
            txtrl.Enabled = false;
            txtcatatan.Enabled = false;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnupdate.PerformClick();
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private async void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtjenis.Text == "")
            {
                MessageBox.Show("Jenis Tidak Boleh Kosong", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnupdate.Enabled = false;
            btnclear.Enabled = false;

            using (var conn = await Koneksi.GetConnectionAsync())
            using (var trans = conn.BeginTransaction())
            {
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
                    UPDATE perbaikan_p 
                    SET nomor_rod = @nomorrod, jenis=@jenis, e1_ers=@e1ers, e1_est=@e1est, e1_jumlah=@e1jumlah,
                        e2_ers=@e2ers, e2_cst=@e2cst, e2_cstub=@e2cstub, e2_jumlah=@e2jumlah,
                        e3=@e3, e4=@e4, s=@s, d=@d, b=@b, bac=@bac, nba=@nba, ba=@ba, ba1=@ba1,
                        cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah, updated_at=GETDATE(), remaks = @remaks, catatan = @catatan
                    WHERE no=@no", conn, trans);

                    cmd1.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    cmd1.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd1.Parameters.AddWithValue("@e1ers", txte1ers.Text);
                    cmd1.Parameters.AddWithValue("@e1est", txte1est.Text);
                    cmd1.Parameters.AddWithValue("@e1jumlah", lbltotale1.Text);
                    cmd1.Parameters.AddWithValue("@e2ers", txte2ers.Text);
                    cmd1.Parameters.AddWithValue("@e2cst", txte2cst.Text);
                    cmd1.Parameters.AddWithValue("@e2cstub", txte2cstub.Text);
                    cmd1.Parameters.AddWithValue("@e2jumlah", lbltotale2.Text);
                    cmd1.Parameters.AddWithValue("@e3", txte3.Text);
                    cmd1.Parameters.AddWithValue("@e4", txte4.Text);
                    cmd1.Parameters.AddWithValue("@s", txts.Text);
                    cmd1.Parameters.AddWithValue("@d", txtd.Text);
                    cmd1.Parameters.AddWithValue("@b", txtb.Text);
                    cmd1.Parameters.AddWithValue("@bac", txtbac.Text);
                    cmd1.Parameters.AddWithValue("@nba", txtnba.Text);
                    cmd1.Parameters.AddWithValue("@ba", lbltotalba.Text);
                    cmd1.Parameters.AddWithValue("@ba1", txtba1.Text);
                    cmd1.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd1.Parameters.AddWithValue("@m", txtm.Text);
                    cmd1.Parameters.AddWithValue("@r", txtr.Text);
                    cmd1.Parameters.AddWithValue("@c", txtc.Text);
                    cmd1.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd1.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                    cmd1.Parameters.AddWithValue("@no", noprimary);
                    cmd1.Parameters.AddWithValue("@remaks", loginform.login.name);
                    cmd1.Parameters.AddWithValue("@catatan", txtcatatan.Text);
                    await cmd1.ExecuteNonQueryAsync();

                    SqlCommand log1 = new SqlCommand(
                        "INSERT INTO perbaikan_e SELECT * FROM perbaikan_p WHERE no=@no",
                        conn, trans);
                    log1.Parameters.AddWithValue("@no", noprimary);
                    await log1.ExecuteNonQueryAsync();

                    SqlCommand cek2 = new SqlCommand(
                        "SELECT COUNT(*) FROM perbaikan_s WHERE no=@no", conn, trans);
                    cek2.Parameters.AddWithValue("@no", noprimary);
                    int ada2 = (int)await cek2.ExecuteScalarAsync();

                    if (ada2 > 0)
                    {
                        SqlCommand cmd2 = new SqlCommand(@"
                UPDATE perbaikan_s 
                SET nomor_rod = @nomorrod, jenis=@jenis, e1_ers=@e1ers, e1_est=@e1est, e1_jumlah=@e1jumlah,
                    e2_ers=@e2ers, e2_cst=@e2cst, e2_cstub=@e2cstub, e2_jumlah=@e2jumlah,
                    e3=@e3, e4=@e4, s=@s, d=@d, b=@b, bac=@bac, nba=@nba, ba=@ba, ba1=@ba1,
                    cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah, updated_at=GETDATE(), remaks = @remaks, catatan = @catatan
                WHERE no=@no", conn, trans);

                        cmd2.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        cmd2.Parameters.AddWithValue("@jenis", txtjenis.Text);
                        cmd2.Parameters.AddWithValue("@e1ers", txte1ers.Text);
                        cmd2.Parameters.AddWithValue("@e1est", txte1est.Text);
                        cmd2.Parameters.AddWithValue("@e1jumlah", lbltotale1.Text);
                        cmd2.Parameters.AddWithValue("@e2ers", txte2ers.Text);
                        cmd2.Parameters.AddWithValue("@e2cst", txte2cst.Text);
                        cmd2.Parameters.AddWithValue("@e2cstub", txte2cstub.Text);
                        cmd2.Parameters.AddWithValue("@e2jumlah", lbltotale2.Text);
                        cmd2.Parameters.AddWithValue("@e3", txte3.Text);
                        cmd2.Parameters.AddWithValue("@e4", txte4.Text);
                        cmd2.Parameters.AddWithValue("@s", txts.Text);
                        cmd2.Parameters.AddWithValue("@d", txtd.Text);
                        cmd2.Parameters.AddWithValue("@b", txtb.Text);
                        cmd2.Parameters.AddWithValue("@bac", txtbac.Text);
                        cmd2.Parameters.AddWithValue("@nba", txtnba.Text);
                        cmd2.Parameters.AddWithValue("@ba", lbltotalba.Text);
                        cmd2.Parameters.AddWithValue("@ba1", txtba1.Text);
                        cmd2.Parameters.AddWithValue("@cr", txtcr.Text);
                        cmd2.Parameters.AddWithValue("@m", txtm.Text);
                        cmd2.Parameters.AddWithValue("@r", txtr.Text);
                        cmd2.Parameters.AddWithValue("@c", txtc.Text);
                        cmd2.Parameters.AddWithValue("@rl", txtrl.Text);
                        cmd2.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                        cmd2.Parameters.AddWithValue("@no", noprimary);
                        cmd2.Parameters.AddWithValue("@remaks", loginform.login.name);
                        cmd2.Parameters.AddWithValue("@catatan", txtcatatan.Text);

                        await cmd2.ExecuteNonQueryAsync();
                    }

                    SqlCommand cek3 = new SqlCommand(
                        "SELECT COUNT(*) FROM penerimaan_p WHERE no=@no", conn, trans);
                    cek3.Parameters.AddWithValue("@no", noprimary);
                    int ada3 = (int)await cek3.ExecuteScalarAsync();

                    if (ada3 > 0)
                    {
                        SqlCommand cmd3 = new SqlCommand(@"
                UPDATE penerimaan_p
                SET nomor_rod=@nomorrod, remaks=@remaks, updated_at=GETDATE()
                WHERE no=@no", conn, trans);

                        cmd3.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        cmd3.Parameters.AddWithValue("@remaks", loginform.login.name);
                        cmd3.Parameters.AddWithValue("@no", noprimary);
                        await cmd3.ExecuteNonQueryAsync();

                        SqlCommand log3 = new SqlCommand(
                            "INSERT INTO penerimaan_e SELECT * FROM penerimaan_p WHERE no=@no",
                            conn, trans);
                        log3.Parameters.AddWithValue("@no", noprimary);
                        await log3.ExecuteNonQueryAsync();
                    }

                    //UPDATE pengiriman jika ada
                    SqlCommand cek5 = new SqlCommand(
                        "SELECT COUNT(*) FROM pengiriman WHERE no=@no", conn, trans);
                    cek5.Parameters.AddWithValue("@no", noprimary);
                    int ada5 = (int)await cek5.ExecuteScalarAsync();

                    if (ada5 > 0)
                    {
                        SqlCommand cmd5 = new SqlCommand(@"
                UPDATE pengiriman 
                SET nomor_rod=@nomorrod, remaks=@remaks, updated_at=GETDATE()
                WHERE no=@no", conn, trans);

                        cmd5.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                        cmd5.Parameters.AddWithValue("@remaks", loginform.login.name);
                        cmd5.Parameters.AddWithValue("@no", noprimary);
                        await cmd5.ExecuteNonQueryAsync();
                    }

                    trans.Commit();

                    MessageBox.Show("Data berhasil diperbarui.",
                                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clearedit();
                    setfalse();

                    if (isSearching)
                    {
                        await HitungTotalDataPencarian();
                        await tampilperbaikan();
                    }
                    else
                    {
                        await HitungTotalData();
                        await tampilperbaikan();

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

        private void btnclear_Click(object sender, EventArgs e)
        {   
            clearedit();
            btnclear.Enabled = false;   
            btnupdate.Enabled = false;
            setfalse();   
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
                await tampilperbaikan();

                infocari = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["no"].Value);
                tanggalperbaikan = Convert.ToDateTime(row.Cells["tanggal_perbaikan"].Value);
                shift = Convert.ToInt32(row.Cells["shift"].Value);        
                txtnomorrod.Text = row.Cells["nomor_rod"].Value.ToString();
                nomorrodsebelumnya = row.Cells["nomor_rod"].Value.ToString();           
                txtjenis.Text = row.Cells["jenis"].Value.ToString();           
                txte1ers.Text = row.Cells["e1_ers"].Value.ToString();              
                txte1est.Text = row.Cells["e1_est"].Value.ToString();              
                lbltotale1.Text = row.Cells["e1_jumlah"].Value.ToString();
                txte2ers.Text = row.Cells["e2_ers"].Value.ToString();              
                txte2cst.Text = row.Cells["e2_cst"].Value.ToString();             
                txte2cstub.Text = row.Cells["e2_cstub"].Value.ToString();             
                lbltotale2.Text = row.Cells["e2_jumlah"].Value.ToString();
                txte3.Text = row.Cells["e3"].Value.ToString();          
                txte4.Text = row.Cells["e4"].Value.ToString();          
                txts.Text = row.Cells["s"].Value.ToString();              
                txtd.Text = row.Cells["d"].Value.ToString();               
                txtb.Text = row.Cells["b"].Value.ToString();            
                txtbac.Text = row.Cells["bac"].Value.ToString();            
                txtnba.Text = row.Cells["nba"].Value.ToString();            
                lbltotalba.Text = row.Cells["ba"].Value.ToString();            
                txtba1.Text = row.Cells["ba1"].Value.ToString();              
                txtcr.Text = row.Cells["cr"].Value.ToString();               
                txtm.Text = row.Cells["m"].Value.ToString();               
                txtr.Text = row.Cells["r"].Value.ToString();               
                txtc.Text = row.Cells["c"].Value.ToString();                
                txtrl.Text = row.Cells["rl"].Value.ToString();                
                lbltotalsebelum.Text = row.Cells["jumlah"].Value.ToString();
                tanggalpenerimaan = Convert.ToDateTime(row.Cells["tanggal_penerimaan"].Value);
                txtcatatan.Text = row.Cells["catatan"].Value.ToString();

                btnupdate.Enabled = true;
                btnclear.Enabled = true;
                settrue();
                txtjenis.Focus();
            }
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
            int angka1 = SafeParse(txte1ers);
            int angka2 = SafeParse(txte1est);
            int hasile1 = angka1 + angka2;
            lbltotale1.Text = hasile1.ToString();

            int angka3 = SafeParse(txte2ers);
            int angka4 = SafeParse(txte2cst);
            int angka5 = SafeParse(txte2cstub);
            int hasile2 = angka3 + angka4 + angka5;
            lbltotale2.Text = hasile2.ToString();

            int angka6 = SafeParse(txte3);
            int angka7 = 0;
            int angka7a = SafeParse(txts);
            if (angka7a > 0)
            {
                angka7 = 1;
            }
            else
            {
                angka7 = 0;
            }
            int angka8 = SafeParse(txtd);
            int angka9 = SafeParse(txtb);
            int angka10 = SafeParse(txtba1);
            int angka11 = 0;
            int angka11a = SafeParse(txtcr);
            if (angka11a > 0)
            {
                angka11 = 1;
            }
            else
            {
                angka11 = 0;
            }
            int angka12 = SafeParse(txtm);
            int angka13 = SafeParse(txtr);
            int angka14 = 0;
            int angka14a = SafeParse(txtc);
            if (angka14a > 0)
            {
                angka14 = 1;
            }
            else
            {
                angka14 = 0;
            }
            int angka15 = SafeParse(txtrl);
            int angka16 = SafeParse(txte4);

            int angka17 = SafeParse(txtbac);
            int angka18 = SafeParse(txtnba);
            int hasilba = angka17 + angka18;
            lbltotalba.Text = hasilba.ToString();

            int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12 + angka13 + angka14 + angka15 + angka16 + angka17 + angka18;
            lbltotalupdate.Text = hasil.ToString();
        }

        private void txte1ers_TextChanged_1(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtrl_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtnba_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte1est_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2ers_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2cst_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2cstub_TextChanged(object sender, EventArgs e)
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

        private void txte4_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtba1_TextChanged(object sender, EventArgs e)
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

        private void txtbac_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void editperbaikan_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private async void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await tampilperbaikan();
            }
        }

        private async void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                await tampilperbaikan();
            }
        }

        private void txtcr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }
    }
}
