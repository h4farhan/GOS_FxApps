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
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Threading;
using Azure.Core;

namespace GOS_FxApps
{
    public partial class Perbaikan : Form
    {
        private DateTime tanggalpenerimaan;
        int noprimary;

        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;

        public Perbaikan()
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

                    default:
                        break;
                }
            }
            catch
            {
                return;
            }
        }

        private async void Perbaikan_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            setdefault();
            btnsimpan.Enabled = false;
            txtnomorrod.Focus();

            await HitungTotalData();
            await tampilpenerimaan();
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

                    if (dataGridView2.InvokeRequired)
                    {
                        dataGridView2.Invoke(new Action(() =>
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
            dataGridView2.RowTemplate.Height = 35;
            dataGridView2.DataSource = dt;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);

            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToResizeRows = false;

            if (dt.Columns.Count >= 22)
            {
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Tanggal Penerimaan";
                dataGridView2.Columns[2].HeaderText = "Shift";
                dataGridView2.Columns[3].HeaderText = "Nomor ROD";
                dataGridView2.Columns[4].HeaderText = "Jenis";
                dataGridView2.Columns[5].HeaderText = "Stasiun";
                dataGridView2.Columns[6].HeaderText = "E1";
                dataGridView2.Columns[7].HeaderText = "E2";
                dataGridView2.Columns[8].HeaderText = "E3";
                dataGridView2.Columns[9].HeaderText = "S";
                dataGridView2.Columns[10].HeaderText = "D";
                dataGridView2.Columns[11].HeaderText = "B";
                dataGridView2.Columns[12].HeaderText = "BA";
                dataGridView2.Columns[13].HeaderText = "R";
                dataGridView2.Columns[14].HeaderText = "M";
                dataGridView2.Columns[15].HeaderText = "CR";
                dataGridView2.Columns[16].HeaderText = "C";
                dataGridView2.Columns[17].HeaderText = "RL";
                dataGridView2.Columns[18].HeaderText = "Jumlah";
                dataGridView2.Columns[19].HeaderText = "Diubah";
                dataGridView2.Columns[20].HeaderText = "Remaks";
                dataGridView2.Columns[21].HeaderText = "Catatan";
            }

            lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

            btnleft.Enabled = currentPage > 1;
            btnright.Enabled = currentPage < totalPages;
        }

        private void setdefault()
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
            txtcaripenerimaan.Clear();
            lbltotal.Text = "-";
            lbltotale1.Text = "-";
            lbltotale2.Text = "-";
            lbltotalba.Text = "-";
        }

        private void settrue()
        {
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

        private int ToInt(string text)
        {
            return int.TryParse(text, out int val) ? val : 0;
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
                        string insertQuery = @"
                INSERT INTO {0}
                (no, tanggal_perbaikan, shift, nomor_rod, jenis,
                 e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah,
                 e3, e4, s, d, b, bac, nba, ba, ba1, cr, m, r, c, rl,
                 jumlah, tanggal_penerimaan, updated_at, remaks, catatan)
                VALUES
                (@no, @tanggal, @shift, @nomorrod, @jenis,
                 @e1ers, @e1est, @e1jumlah, @e2ers, @e2cst, @e2cstub, @e2jumlah,
                 @e3, @e4, @s, @d, @b, @bac, @nba, @ba, @ba1, @cr, @m, @r, @c, @rl,
                 @jumlah, @tanggalpenerimaan, GETDATE(), @remaks, @catatan)";

                        string[] tabel = { "perbaikan_s", "perbaikan_p", "perbaikan_m" };

                        foreach (string t in tabel)
                        {
                            using (SqlCommand cmd = new SqlCommand(string.Format(insertQuery, t), conn, trans))
                            {
                                cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                                cmd.Parameters.Add("@tanggal", SqlDbType.DateTime).Value = MainForm.Instance.tanggal;
                                cmd.Parameters.Add("@shift", SqlDbType.VarChar).Value = MainForm.Instance.lblshift.Text;
                                cmd.Parameters.Add("@nomorrod", SqlDbType.VarChar).Value = txtnomorrod.Text;
                                cmd.Parameters.Add("@jenis", SqlDbType.VarChar).Value = txtjenis.Text;

                                cmd.Parameters.Add("@e1ers", SqlDbType.Int).Value = ToInt(txte1ers.Text);
                                cmd.Parameters.Add("@e1est", SqlDbType.Int).Value = ToInt(txte1est.Text);
                                cmd.Parameters.Add("@e1jumlah", SqlDbType.Int).Value = ToInt(lbltotale1.Text);

                                cmd.Parameters.Add("@e2ers", SqlDbType.Int).Value = ToInt(txte2ers.Text);
                                cmd.Parameters.Add("@e2cst", SqlDbType.Int).Value = ToInt(txte2cst.Text);
                                cmd.Parameters.Add("@e2cstub", SqlDbType.Int).Value = ToInt(txte2cstub.Text);
                                cmd.Parameters.Add("@e2jumlah", SqlDbType.Int).Value = ToInt(lbltotale2.Text);

                                cmd.Parameters.Add("@e3", SqlDbType.Int).Value = ToInt(txte3.Text);
                                cmd.Parameters.Add("@e4", SqlDbType.Int).Value = ToInt(txte4.Text);
                                cmd.Parameters.Add("@s", SqlDbType.Int).Value = ToInt(txts.Text);
                                cmd.Parameters.Add("@d", SqlDbType.Int).Value = ToInt(txtd.Text);
                                cmd.Parameters.Add("@b", SqlDbType.Int).Value = ToInt(txtb.Text);

                                cmd.Parameters.Add("@bac", SqlDbType.Int).Value = ToInt(txtbac.Text);
                                cmd.Parameters.Add("@nba", SqlDbType.Int).Value = ToInt(txtnba.Text);
                                cmd.Parameters.Add("@ba", SqlDbType.Int).Value = ToInt(lbltotalba.Text);
                                cmd.Parameters.Add("@ba1", SqlDbType.Int).Value = ToInt(txtba1.Text);

                                cmd.Parameters.Add("@cr", SqlDbType.Int).Value = ToInt(txtcr.Text);
                                cmd.Parameters.Add("@m", SqlDbType.Int).Value = ToInt(txtm.Text);
                                cmd.Parameters.Add("@r", SqlDbType.Int).Value = ToInt(txtr.Text);
                                cmd.Parameters.Add("@c", SqlDbType.Int).Value = ToInt(txtc.Text);
                                cmd.Parameters.Add("@rl", SqlDbType.Int).Value = ToInt(txtrl.Text);

                                cmd.Parameters.Add("@jumlah", SqlDbType.Int).Value = ToInt(lbltotal.Text);
                                cmd.Parameters.Add("@tanggalpenerimaan", SqlDbType.DateTime).Value = tanggalpenerimaan;
                                cmd.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;
                                cmd.Parameters.Add("@catatan", SqlDbType.VarChar).Value = txtcatatan.Text;

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        using (SqlCommand cmdDel = new SqlCommand(
                            "DELETE FROM penerimaan_s WHERE nomor_rod=@nomorrod", conn, trans))
                        {
                            cmdDel.Parameters.Add("@nomorrod", SqlDbType.VarChar).Value = txtnomorrod.Text;
                            await cmdDel.ExecuteNonQueryAsync();
                        }

                        using (SqlCommand cmdUbah = new SqlCommand(@"
                UPDATE penerimaan_p SET
                e1=@e1, e2=@e2, e3=@e3, s=@s, d=@d, b=@b, ba=@ba,
                cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah,
                remaks=@remaks, updated_at=@diubah, catatan=@catatan
                WHERE no=@no
                AND CAST(tanggal_penerimaan AS DATE)=CAST(@tanggal AS DATE)
                AND shift=@shift", conn, trans))
                        {
                            cmdUbah.Parameters.Add("@e1", SqlDbType.Int).Value = ToInt(lbltotale1.Text);
                            cmdUbah.Parameters.Add("@e2", SqlDbType.Int).Value = ToInt(lbltotale2.Text);
                            cmdUbah.Parameters.Add("@e3", SqlDbType.Int).Value = ToInt(txte3.Text);
                            cmdUbah.Parameters.Add("@s", SqlDbType.Int).Value = ToInt(txts.Text);
                            cmdUbah.Parameters.Add("@d", SqlDbType.Int).Value = ToInt(txtd.Text);
                            cmdUbah.Parameters.Add("@b", SqlDbType.Int).Value = ToInt(txtb.Text);
                            cmdUbah.Parameters.Add("@ba", SqlDbType.Int).Value = ToInt(lbltotalba.Text);
                            cmdUbah.Parameters.Add("@cr", SqlDbType.Int).Value = ToInt(txtcr.Text);
                            cmdUbah.Parameters.Add("@m", SqlDbType.Int).Value = ToInt(txtm.Text);
                            cmdUbah.Parameters.Add("@r", SqlDbType.Int).Value = ToInt(txtr.Text);
                            cmdUbah.Parameters.Add("@c", SqlDbType.Int).Value = ToInt(txtc.Text);
                            cmdUbah.Parameters.Add("@rl", SqlDbType.Int).Value = ToInt(txtrl.Text);
                            cmdUbah.Parameters.Add("@jumlah", SqlDbType.Int).Value = ToInt(lbltotal.Text);

                            cmdUbah.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;
                            cmdUbah.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                            cmdUbah.Parameters.Add("@diubah", SqlDbType.DateTime).Value = MainForm.Instance.tanggal;
                            cmdUbah.Parameters.Add("@catatan", SqlDbType.VarChar).Value = txtcatatan.Text;
                            cmdUbah.Parameters.Add("@tanggal", SqlDbType.DateTime).Value = MainForm.Instance.tanggal;
                            cmdUbah.Parameters.Add("@shift", SqlDbType.VarChar).Value = MainForm.Instance.lblshift.Text;

                            await cmdUbah.ExecuteNonQueryAsync();
                        }

                        trans.Commit();

                        await Task.Yield();

                        MessageBox.Show("Data Berhasil Disimpan", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Simpan Data: ");
            }
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
                angka11 =1;
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
            lbltotal.Text = hasil.ToString();
            btnsimpan.Enabled = true;
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

        private async void btnsimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtnomorrod.Text) ||
                string.IsNullOrWhiteSpace(txtjenis.Text))
            {
                MessageBox.Show("Jenis Tidak Boleh Kosong",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnsimpan.Enabled = false;
            btncancel.Enabled = false;

            await simpandata();

            delay = 0;

            this.SuspendLayout();
            setdefault();
            setfalse();
            this.ResumeLayout();

            btnsimpan.Enabled = false;
            btncancel.Enabled = false;

            delay = 300;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            txtnomorrod.Enabled = true;
            setdefault();
            txtnomorrod.Enabled = false;
            setfalse();
            btncancel.Enabled = false;
            btnsimpan.Text = "Simpan Data";
            btnsimpan.Enabled = false;
            dataGridView2.ClearSelection();
        }

        private void txtrl_TextChanged(object sender, EventArgs e)
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

        private void txtd_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtb_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtnba_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtbac_TextChanged(object sender, EventArgs e)
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

        private void txte1ers_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtba1_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte4_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                tanggalpenerimaan = Convert.ToDateTime(row.Cells["tanggal_penerimaan"].Value);
                noprimary = Convert.ToInt32(row.Cells["no"].Value);
                txtnomorrod.Text = row.Cells["nomor_rod"].Value.ToString();
                txtjenis.Text = row.Cells["jenis"].Value.ToString();
                txte1ers.Text = row.Cells["e1"].Value.ToString();
                txte2ers.Text = row.Cells["e2"].Value.ToString();
                txte3.Text = row.Cells["e3"].Value.ToString();
                txts.Text = row.Cells["s"].Value.ToString();
                txtd.Text = row.Cells["d"].Value.ToString();
                txtb.Text = row.Cells["b"].Value.ToString();
                txtbac.Text = row.Cells["ba"].Value.ToString();
                txtcr.Text = row.Cells["cr"].Value.ToString();
                txtm.Text = row.Cells["m"].Value.ToString();
                txtr.Text = row.Cells["r"].Value.ToString();
                txtc.Text = row.Cells["c"].Value.ToString();
                txtrl.Text = row.Cells["rl"].Value.ToString();
                lbltotal.Text = row.Cells["jumlah"].Value.ToString();
                txtcatatan.Text = row.Cells["catatan"].Value.ToString();
                settrue();
                btncancel.Enabled = true;
                txtjenis.Focus();
            }
        }

        private void Perbaikan_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private CancellationTokenSource ctsCari = null;
        int delay = 300;
        private async void txtcaripenerimaan_TextChanged(object sender, EventArgs e)
        {
            string inputRod = txtcaripenerimaan.Text.Trim();

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
