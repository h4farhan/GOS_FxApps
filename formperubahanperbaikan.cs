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
using System.IO;
using System.Threading;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace GOS_FxApps
{
    public partial class formperubahanperbaikan : Form
    {
        private DateTime tanggalpenerimaan;
        int noprimary;

        private Image fotoSementara;

        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;

        public formperubahanperbaikan()
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

        private void btnambilfoto_Click(object sender, EventArgs e)
        {
            if (fotoSementara != null)
            {
                var result = MessageBox.Show("Foto sudah ada, ambil ulang?",
                                             "Konfirmasi",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
                fotoSementara = null;
            }

            using (var frm = new formkamera())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.HasilFoto != null)
                    {
                        fotoSementara = new Bitmap(frm.HasilFoto);
                        MessageBox.Show("Foto berhasil diambil, siap disimpan!",
                                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        MessageBox.Show("Foto gagal diambil. Silakan ulangi.",
                                        "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
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
            fotoSementara = null;
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
            btnambilfoto.Enabled = true;
            iconButton1.Enabled = true;
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
            btnambilfoto.Enabled= false;
            iconButton1.Enabled = false;
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
            lbltotal.Text = hasil.ToString();
            btnsimpan.Enabled = true;
        }

        private async void formperubahanperbaikan_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            setdefault();

            await HitungTotalData();
            await tampilpenerimaan();
        }

        private async void btnsimpan_Click(object sender, EventArgs e)
        {
            if (txtjenis.Text == "")
            {
                MessageBox.Show("Jenis Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (fotoSementara == null)
            {
                MessageBox.Show("Foto bukti Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            btnsimpan.Enabled = false;
            dataGridView2.ClearSelection();
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
                btnsimpan.Enabled = true;
                txtjenis.Focus();
            }
        }

        private void formperubahanperbaikan_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private byte[] ImageToByteArray(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
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
                        using (var cmdInsert = conn.CreateCommand())
                        using (var cmdUbahP = conn.CreateCommand())
                        using (var cmdUbahS = conn.CreateCommand())
                        {
                            cmdInsert.Transaction = trans;
                            cmdUbahP.Transaction = trans;
                            cmdUbahS.Transaction = trans;

                            cmdInsert.CommandText = @"
                            INSERT INTO buktiperubahan
                            (nounikpenerimaan, tanggal_penerimaan, shift, nomor_rod, jenis,
                             e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah,
                             e3, e4, s, d, b, bac, nba, ba, ba1,
                             cr, m, r, c, rl, jumlah,
                             tanggal_perbaikan, updated_at, remaks, catatan, foto)
                            VALUES
                            (@no, @tglTerima, @shift, @rod, @jenis,
                             @e1ers, @e1est, @e1jml, @e2ers, @e2cst, @e2cstub, @e2jml,
                             @e3, @e4, @s, @d, @b, @bac, @nba, @ba, @ba1,
                             @cr, @m, @r, @c, @rl, @jumlah,
                             @tgl, GETDATE(), @remaks, @catatan, @foto)";

                            cmdInsert.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                            cmdInsert.Parameters.Add("@tglTerima", SqlDbType.DateTime).Value = tanggalpenerimaan;
                            cmdInsert.Parameters.Add("@shift", SqlDbType.VarChar).Value = MainForm.Instance.lblshift.Text;
                            cmdInsert.Parameters.Add("@rod", SqlDbType.VarChar).Value = txtnomorrod.Text;
                            cmdInsert.Parameters.Add("@jenis", SqlDbType.VarChar).Value = txtjenis.Text;

                            cmdInsert.Parameters.Add("@e1ers", SqlDbType.Int).Value = ToInt(txte1ers.Text);
                            cmdInsert.Parameters.Add("@e1est", SqlDbType.Int).Value = ToInt(txte1est.Text);
                            cmdInsert.Parameters.Add("@e1jml", SqlDbType.Int).Value = ToInt(lbltotale1.Text);

                            cmdInsert.Parameters.Add("@e2ers", SqlDbType.Int).Value = ToInt(txte2ers.Text);
                            cmdInsert.Parameters.Add("@e2cst", SqlDbType.Int).Value = ToInt(txte2cst.Text);
                            cmdInsert.Parameters.Add("@e2cstub", SqlDbType.Int).Value = ToInt(txte2cstub.Text);
                            cmdInsert.Parameters.Add("@e2jml", SqlDbType.Int).Value = ToInt(lbltotale2.Text);

                            cmdInsert.Parameters.Add("@e3", SqlDbType.Int).Value = ToInt(txte3.Text);
                            cmdInsert.Parameters.Add("@e4", SqlDbType.Int).Value = ToInt(txte4.Text);
                            cmdInsert.Parameters.Add("@s", SqlDbType.Int).Value = ToInt(txts.Text);
                            cmdInsert.Parameters.Add("@d", SqlDbType.Int).Value = ToInt(txtd.Text);
                            cmdInsert.Parameters.Add("@b", SqlDbType.Int).Value = ToInt(txtb.Text);

                            cmdInsert.Parameters.Add("@bac", SqlDbType.Int).Value = ToInt(txtbac.Text);
                            cmdInsert.Parameters.Add("@nba", SqlDbType.Int).Value = ToInt(txtnba.Text);
                            cmdInsert.Parameters.Add("@ba", SqlDbType.Int).Value = ToInt(lbltotalba.Text);
                            cmdInsert.Parameters.Add("@ba1", SqlDbType.Int).Value = ToInt(txtba1.Text);

                            cmdInsert.Parameters.Add("@cr", SqlDbType.Int).Value = ToInt(txtcr.Text);
                            cmdInsert.Parameters.Add("@m", SqlDbType.Int).Value = ToInt(txtm.Text);
                            cmdInsert.Parameters.Add("@r", SqlDbType.Int).Value = ToInt(txtr.Text);
                            cmdInsert.Parameters.Add("@c", SqlDbType.Int).Value = ToInt(txtc.Text);
                            cmdInsert.Parameters.Add("@rl", SqlDbType.Int).Value = ToInt(txtrl.Text);

                            cmdInsert.Parameters.Add("@jumlah", SqlDbType.Int).Value = ToInt(lbltotal.Text);
                            cmdInsert.Parameters.Add("@tgl", SqlDbType.DateTime).Value = MainForm.Instance.tanggal;
                            cmdInsert.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;
                            cmdInsert.Parameters.Add("@catatan", SqlDbType.VarChar).Value = txtcatatan.Text;

                            cmdInsert.Parameters.Add("@foto", SqlDbType.VarBinary).Value =
                                fotoSementara != null ? ImageToByteArray(fotoSementara) : (object)DBNull.Value;

                            string updateSql = @"
                            UPDATE {0}
                            SET jenis=@jenis, e1=@e1, e2=@e2, e3=@e3, s=@s, d=@d,
                                b=@b, ba=@ba, cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah,
                                remaks=@remaks, updated_at=@tgl, catatan=@catatan
                            WHERE no=@no
                            AND CAST(tanggal_penerimaan AS DATE)=CAST(@tgl AS DATE)
                            AND shift=@shift";

                            cmdUbahP.CommandText = string.Format(updateSql, "penerimaan_p");
                            cmdUbahS.CommandText = string.Format(updateSql, "penerimaan_s");

                            foreach (var cmdx in new[] { cmdUbahP, cmdUbahS })
                            {
                                cmdx.Parameters.Add("@jenis", SqlDbType.VarChar).Value = txtjenis.Text;
                                cmdx.Parameters.Add("@e1", SqlDbType.Int).Value = ToInt(lbltotale1.Text);
                                cmdx.Parameters.Add("@e2", SqlDbType.Int).Value = ToInt(lbltotale2.Text);
                                cmdx.Parameters.Add("@e3", SqlDbType.Int).Value = ToInt(txte3.Text);
                                cmdx.Parameters.Add("@s", SqlDbType.Int).Value = ToInt(txts.Text);
                                cmdx.Parameters.Add("@d", SqlDbType.Int).Value = ToInt(txtd.Text);
                                cmdx.Parameters.Add("@b", SqlDbType.Int).Value = ToInt(txtb.Text);
                                cmdx.Parameters.Add("@ba", SqlDbType.Int).Value = ToInt(lbltotalba.Text);
                                cmdx.Parameters.Add("@cr", SqlDbType.Int).Value = ToInt(txtcr.Text);
                                cmdx.Parameters.Add("@m", SqlDbType.Int).Value = ToInt(txtm.Text);
                                cmdx.Parameters.Add("@r", SqlDbType.Int).Value = ToInt(txtr.Text);
                                cmdx.Parameters.Add("@c", SqlDbType.Int).Value = ToInt(txtc.Text);
                                cmdx.Parameters.Add("@rl", SqlDbType.Int).Value = ToInt(txtrl.Text);
                                cmdx.Parameters.Add("@jumlah", SqlDbType.Int).Value = ToInt(lbltotal.Text);
                                cmdx.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;
                                cmdx.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                                cmdx.Parameters.Add("@tgl", SqlDbType.DateTime).Value = MainForm.Instance.tanggal;
                                cmdx.Parameters.Add("@catatan", SqlDbType.VarChar).Value = txtcatatan.Text;
                                cmdx.Parameters.Add("@shift", SqlDbType.VarChar).Value = MainForm.Instance.lblshift.Text;
                            }

                            await cmdInsert.ExecuteNonQueryAsync();
                            await cmdUbahP.ExecuteNonQueryAsync();
                            await cmdUbahS.ExecuteNonQueryAsync();
                        }

                        trans.Commit();

                        await Task.Yield();

                        MessageBox.Show("Data Berhasil Disimpan", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        MessageBox.Show("Gagal menyimpan: ");
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

        private void txte2cstub_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte1ers_TextChanged(object sender, EventArgs e)
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

        private void txte3_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte4_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txts_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtba1_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtb_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtd_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte2cst_TextChanged(object sender, EventArgs e)
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

        private void txtrl_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtc_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (fotoSementara != null)
            {
                var result = MessageBox.Show("Foto sudah ada, ambil ulang?",
                                             "Konfirmasi",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                fotoSementara = null;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih Foto";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        Image img = Image.FromStream(fs);

                        using (formdialog frm = new formdialog((Image)img.Clone()))
                        {
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                if (frm.HasilFoto != null)
                                {
                                    fotoSementara = new Bitmap(frm.HasilFoto);
                                    MessageBox.Show("Foto berhasil dipilih, siap disimpan!",
                                                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Foto gagal dipilih. Silakan ulangi.",
                                                    "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }
            }
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
