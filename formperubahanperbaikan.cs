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

namespace GOS_FxApps
{
    public partial class formperubahanperbaikan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();
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

        private void registertampilpenerimaan()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (var cmd = new SqlCommand("SELECT updated_at FROM dbo.penerimaan_s", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!isSearching)
                            {
                                HitungTotalData();
                                currentPage = 1;
                                tampilpenerimaan();
                            }
                            else
                            {
                                int oldTotal = searchTotalRecords;
                                HitungTotalDataPencarian();
                                if (searchTotalRecords > oldTotal)
                                {
                                    tampilpenerimaan();
                                }
                            }

                            registertampilpenerimaan();
                        }));
                    }
                };

                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void HitungTotalData()
        {
            string query = "SELECT COUNT(*) FROM penerimaan_s";
            using (var connLocal = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connLocal))
            {
                connLocal.Open();
                totalRecords = (int)cmd.ExecuteScalar();
                connLocal.Close();
            }

            totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }

        private void HitungTotalDataPencarian()
        {
            if (string.IsNullOrWhiteSpace(lastSearchWhere))
            {
                searchTotalRecords = 0;
                totalPages = 0;
                return;
            }

            string countQuery = "SELECT COUNT(*) " + lastSearchWhere;

            using (var connLocal = new SqlConnection(Koneksi.GetConnectionString()))
            using (var cmd = new SqlCommand(countQuery, connLocal))
            {
                if (lastSearchCmd?.Parameters.Count > 0)
                {
                    foreach (SqlParameter p in lastSearchCmd.Parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                    }
                }

                connLocal.Open();
                searchTotalRecords = (int)cmd.ExecuteScalar();
            }

            totalPages = (int)Math.Ceiling(searchTotalRecords / (double)pageSize);
        }

        private void tampilpenerimaan()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                string query;

                if (!isSearching)
                {
                    query = $@"
                SELECT no, tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, e1, e2, e3, s, d, b, ba, r, m, cr, c, rl, jumlah, updated_at, remaks, catatan
                FROM penerimaan_s
                ORDER BY tanggal_penerimaan DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                }
                else
                {
                    query = $@"
                SELECT no, tanggal_penerimaan, shift, nomor_rod, jenis, stasiun, e1, e2, e3, s, d, b, ba, r, m, cr, c, rl, jumlah, updated_at, remaks, catatan
                {lastSearchWhere}
                ORDER BY tanggal_penerimaan DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";

                    foreach (SqlParameter p in lastSearchCmd.Parameters)
                        cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                }

                cmd.CommandText = query;

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView2.DataSource = dt;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView2.RowTemplate.Height = 35;
                dataGridView2.ReadOnly = true;

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

                lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

                btnleft.Enabled = currentPage > 1;
                btnright.Enabled = currentPage < totalPages;
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            txtnomorrod.Clear();
            txtnomorrod.Clear();
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

        private void formperubahanperbaikan_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            setdefault();
            HitungTotalData();
            tampilpenerimaan();
            registertampilpenerimaan();
        }

        private void btnsimpan_Click(object sender, EventArgs e)
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

            simpandata();
            txtnomorrod.Enabled = true;
            setdefault();
            txtnomorrod.Enabled = false;
            setfalse();
            btnsimpan.Enabled = false;
            btncancel.Enabled = false;
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
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void txtcaripenerimaan_TextChanged(object sender, EventArgs e)
        {
            string inputRod = txtcaripenerimaan.Text.Trim();
            if (string.IsNullOrEmpty(inputRod))
            {
                isSearching = false;
                currentPage = 1;
                HitungTotalData();
                tampilpenerimaan();
            }
            else
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM penerimaan_s WHERE nomor_rod LIKE @rod";
                lastSearchCmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");

                HitungTotalDataPencarian();
                currentPage = 1;
                tampilpenerimaan();
            }
        }

        private byte[] ImageToByteArray(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void simpandata()
        {
            try
            {
                DialogResult result1 = MessageBox.Show(
                    "Apakah Anda yakin dengan data Anda?",
                    "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
                );

                if (result1 != DialogResult.OK) return;

                using (SqlConnection conn = new SqlConnection(Koneksi.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            using (SqlCommand cmdubah1 = conn.CreateCommand())
                            using (SqlCommand cmdubah2 = conn.CreateCommand())
                            {
                                cmd.Transaction = trans;
                                cmdubah1.Transaction = trans;
                                cmdubah2.Transaction = trans;

                                cmd.CommandText = @"INSERT INTO buktiperubahan 
                            (nounikpenerimaan, tanggal_penerimaan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, 
                             e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, bac, nba, ba, ba1, 
                             cr, m, r, c, rl, jumlah, tanggal_perbaikan, updated_at, remaks, catatan, foto) 
                            VALUES
                            (@nounikpenerimaan, @tanggalpenerimaan, @shift, @nomorrod, @jenis, @e1ers, @e1est, @e1jumlah, 
                             @e2ers, @e2cst, @e2cstub, @e2jumlah, @e3, @e4, @s, @d, @b, @bac, @nba, @ba, @ba1, 
                             @cr, @m, @r, @c, @rl, @jumlah, @tanggal, @diubah, @remaks, @catatan, @foto)";

                                cmd.Parameters.AddWithValue("@nounikpenerimaan", noprimary);
                                cmd.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);
                                cmd.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                                cmd.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                                cmd.Parameters.AddWithValue("@jenis", txtjenis.Text);
                                cmd.Parameters.AddWithValue("@e1ers", txte1ers.Text);
                                cmd.Parameters.AddWithValue("@e1est", txte1est.Text);
                                cmd.Parameters.AddWithValue("@e1jumlah", lbltotale1.Text);
                                cmd.Parameters.AddWithValue("@e2ers", txte2ers.Text);
                                cmd.Parameters.AddWithValue("@e2cst", txte2cst.Text);
                                cmd.Parameters.AddWithValue("@e2cstub", txte2cstub.Text);
                                cmd.Parameters.AddWithValue("@e2jumlah", lbltotale2.Text);
                                cmd.Parameters.AddWithValue("@e3", txte3.Text);
                                cmd.Parameters.AddWithValue("@e4", txte4.Text);
                                cmd.Parameters.AddWithValue("@s", txts.Text);
                                cmd.Parameters.AddWithValue("@d", txtd.Text);
                                cmd.Parameters.AddWithValue("@b", txtb.Text);
                                cmd.Parameters.AddWithValue("@bac", txtbac.Text);
                                cmd.Parameters.AddWithValue("@nba", txtnba.Text);
                                cmd.Parameters.AddWithValue("@ba", lbltotalba.Text);
                                cmd.Parameters.AddWithValue("@ba1", txtba1.Text);
                                cmd.Parameters.AddWithValue("@cr", txtcr.Text);
                                cmd.Parameters.AddWithValue("@m", txtm.Text);
                                cmd.Parameters.AddWithValue("@r", txtr.Text);
                                cmd.Parameters.AddWithValue("@c", txtc.Text);
                                cmd.Parameters.AddWithValue("@rl", txtrl.Text);
                                cmd.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                                cmd.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                                cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                                cmd.Parameters.AddWithValue("@remaks", loginform.login.name);
                                cmd.Parameters.AddWithValue("@catatan", txtcatatan.Text);

                                if (fotoSementara != null)
                                    cmd.Parameters.Add("@foto", SqlDbType.VarBinary).Value = ImageToByteArray(fotoSementara);
                                else
                                    cmd.Parameters.Add("@foto", SqlDbType.VarBinary).Value = DBNull.Value;

                                cmdubah1.CommandText = @"
                            UPDATE penerimaan_p 
                            SET jenis = @jenis, e1=@e1, e2=@e2, e3=@e3, s=@s, d=@d, 
                                b=@b, ba=@ba, cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah,
                                remaks=@remaks, updated_at=@diubah, catatan=@catatan
                            WHERE no=@no 
                              AND CAST(tanggal_penerimaan AS DATE) = CAST(@tanggal AS DATE) 
                              AND shift = @shift";

                                cmdubah2.CommandText = @"
                            UPDATE penerimaan_s 
                            SET jenis = @jenis, e1=@e1, e2=@e2, e3=@e3, s=@s, d=@d, 
                                b=@b, ba=@ba, cr=@cr, m=@m, r=@r, c=@c, rl=@rl, jumlah=@jumlah,
                                remaks=@remaks, updated_at=@diubah, catatan=@catatan
                            WHERE no=@no 
                              AND CAST(tanggal_penerimaan AS DATE) = CAST(@tanggal AS DATE) 
                              AND shift = @shift";

                                foreach (var cmdx in new[] { cmdubah1, cmdubah2 })
                                {
                                    cmdx.Parameters.AddWithValue("@jenis", txtjenis.Text);
                                    cmdx.Parameters.AddWithValue("@e1", lbltotale1.Text);
                                    cmdx.Parameters.AddWithValue("@e2", lbltotale2.Text);
                                    cmdx.Parameters.AddWithValue("@e3", txte3.Text);
                                    cmdx.Parameters.AddWithValue("@s", txts.Text);
                                    cmdx.Parameters.AddWithValue("@d", txtd.Text);
                                    cmdx.Parameters.AddWithValue("@b", txtb.Text);
                                    cmdx.Parameters.AddWithValue("@ba", lbltotalba.Text);
                                    cmdx.Parameters.AddWithValue("@cr", txtcr.Text);
                                    cmdx.Parameters.AddWithValue("@m", txtm.Text);
                                    cmdx.Parameters.AddWithValue("@r", txtr.Text);
                                    cmdx.Parameters.AddWithValue("@c", txtc.Text);
                                    cmdx.Parameters.AddWithValue("@rl", txtrl.Text);
                                    cmdx.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                                    cmdx.Parameters.AddWithValue("@remaks", loginform.login.name);
                                    cmdx.Parameters.AddWithValue("@no", noprimary);
                                    cmdx.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                                    cmdx.Parameters.AddWithValue("@catatan", txtcatatan.Text);
                                    cmdx.Parameters.AddWithValue("@tanggal", MainForm.Instance.tanggal);
                                    cmdx.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                                }

                                cmd.ExecuteNonQuery();
                                cmdubah1.ExecuteNonQuery();
                                cmdubah2.ExecuteNonQuery();
                            }

                            trans.Commit();
                            MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tampilpenerimaan();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            MessageBox.Show("Transaksi dibatalkan:\n" + ex.Message,
                                "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                    "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtcatatan_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                tampilpenerimaan();
            }
        }

        private void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                tampilpenerimaan();
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
