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
    public partial class formeditperubahanperbaikan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();
        DateTime tanggalpenerimaan;
        DateTime tanggalperbaikan;
        int shift;
        int noprimary;

        private bool fotoDiganti = false;
        private Image fotoSementara;

        bool infocari = false;

        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;

        public formeditperubahanperbaikan()
        {
            InitializeComponent();
        }

        private void registertampil()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.buktiperubahan", conn))
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
                                tampil();
                            }
                            else
                            {
                                int oldTotal = searchTotalRecords;
                                HitungTotalDataPencarian();

                                if (searchTotalRecords > oldTotal)
                                {
                                    tampil();
                                }
                            }

                            registertampil();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void HitungTotalData()
        {
            string query = "SELECT COUNT(*) FROM buktiperubahan";
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

        private void tampil()
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
                SELECT no, tanggal_penerimaan, shift, nomor_rod, jenis,
                       e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah,
                       e3, e4, s, d, b, bac, nba, ba, ba1, cr, m, r, c, rl,
                       jumlah, tanggal_perbaikan, updated_at, remaks, catatan, foto
                FROM buktiperubahan
                ORDER BY tanggal_perbaikan DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                }
                else
                {
                    query = $@"
                SELECT no, tanggal_penerimaan, shift, nomor_rod, jenis,
                       e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah,
                       e3, e4, s, d, b, bac, nba, ba, ba1, cr, m, r, c, rl,
                       jumlah, tanggal_perbaikan, updated_at, remaks, catatan, foto
                {lastSearchWhere}
                ORDER BY tanggal_perbaikan DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";

                    foreach (SqlParameter p in lastSearchCmd.Parameters)
                        cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                }

                cmd.CommandText = query;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                if (!dt.Columns.Contains("fotoImage"))
                    dt.Columns.Add("fotoImage", typeof(Image));

                foreach (DataRow row in dt.Rows)
                {
                    if (row["foto"] != DBNull.Value)
                    {
                        byte[] bytes = (byte[])row["foto"];
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            row["fotoImage"] = Image.FromStream(ms);
                        }
                    }
                }

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 80;
                dataGridView1.ReadOnly = true;

                if (dataGridView1.Columns.Contains("foto"))
                    dataGridView1.Columns["foto"].Visible = false;

                if (dataGridView1.Columns.Contains("fotoImage"))
                {
                    DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["fotoImage"];
                    imgCol.HeaderText = "Foto";
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

                if (dataGridView1.Columns.Count >= 30)
                {
                    dataGridView1.Columns["no"].Visible = false;
                    dataGridView1.Columns["tanggal_penerimaan"].HeaderText = "Tanggal Penerimaan";
                    dataGridView1.Columns["shift"].HeaderText = "Shift";
                    dataGridView1.Columns["nomor_rod"].HeaderText = "Nomor ROD";
                    dataGridView1.Columns["jenis"].HeaderText = "Jenis";

                    dataGridView1.Columns["e1_ers"].HeaderText = "E1 Ers";
                    dataGridView1.Columns["e1_est"].HeaderText = "E1 Est";
                    dataGridView1.Columns["e1_jumlah"].HeaderText = "E1 Jumlah";

                    dataGridView1.Columns["e2_ers"].HeaderText = "E2 Ers";
                    dataGridView1.Columns["e2_cst"].HeaderText = "E2 Cst";
                    dataGridView1.Columns["e2_cstub"].HeaderText = "E2 Cstub";
                    dataGridView1.Columns["e2_jumlah"].HeaderText = "E2 Jumlah";

                    dataGridView1.Columns["e3"].HeaderText = "E3";
                    dataGridView1.Columns["e4"].HeaderText = "E4";
                    dataGridView1.Columns["s"].HeaderText = "S";
                    dataGridView1.Columns["d"].HeaderText = "D";
                    dataGridView1.Columns["b"].HeaderText = "B";
                    dataGridView1.Columns["bac"].HeaderText = "BAC";
                    dataGridView1.Columns["nba"].HeaderText = "NBA";
                    dataGridView1.Columns["ba"].HeaderText = "BA";
                    dataGridView1.Columns["ba1"].HeaderText = "BA-1";

                    dataGridView1.Columns["cr"].HeaderText = "CR";
                    dataGridView1.Columns["m"].HeaderText = "M";
                    dataGridView1.Columns["r"].HeaderText = "R";
                    dataGridView1.Columns["c"].HeaderText = "C";
                    dataGridView1.Columns["rl"].HeaderText = "RL";

                    dataGridView1.Columns["jumlah"].HeaderText = "Jumlah";
                    dataGridView1.Columns["tanggal_perbaikan"].HeaderText = "Tanggal Perbaikan";
                    dataGridView1.Columns["updated_at"].HeaderText = "Diubah";
                    dataGridView1.Columns["remaks"].HeaderText = "Remaks";
                    dataGridView1.Columns["catatan"].HeaderText = "Catatan";
                }

                lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";
                btnleft.Enabled = currentPage > 1;
                btnright.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string nomorrod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(nomorrod))
            {
                MessageBox.Show("Silakan isi Tanggal atau Nomor ROD untuk melakukan pencarian.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            isSearching = true;
            lastSearchCmd = new SqlCommand();
            lastSearchWhere = "FROM buktiperubahan WHERE 1=1 ";

            if (tanggal.HasValue)
            {
                lastSearchWhere += " AND CAST(tanggal_penerimaan AS DATE) = @tgl ";
                lastSearchCmd.Parameters.AddWithValue("@tgl", tanggal.Value);
            }

            if (!string.IsNullOrEmpty(nomorrod))
            {
                lastSearchWhere += " AND nomor_rod LIKE @rod ";
                lastSearchCmd.Parameters.AddWithValue("@rod", "%" + nomorrod + "%");
            }

            HitungTotalDataPencarian(); 
            currentPage = 1;            
            tampil();                  

            btncari.Text = "Reset";
            return true;
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
            txtcr.Clear();
            txtm.Clear();
            txtr.Clear();
            txtc.Clear();
            txtrl.Clear();
            txtcatatan.Clear();
            lbltotal.Text = "-";
            lbltotale1.Text = "-";
            lbltotale2.Text = "-";
            lbltotalba.Text = "-";
            fotoSementara = null;
            txtcari.Clear();
            btncari.Text = "Cari";
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
            txtcr.Enabled = true;
            txtm.Enabled = true;
            txtr.Enabled = true;
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
            txtcr.Enabled = false;
            txtm.Enabled = false;
            txtr.Enabled = false;
            txtc.Enabled = false;
            txtrl.Enabled = false;
            txtcatatan.Enabled = false;
            btnambilfoto.Enabled = false;
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
                btnedit.PerformClick();
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
            btnedit.Enabled = true;
        }

        private void btnedit_Click(object sender, EventArgs e)
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

            editdata();
            txtnomorrod.Enabled = true;
            setdefault();
            txtnomorrod.Enabled = false;
            setfalse();
            btnedit.Enabled = false;
            btncancel.Enabled = false;
        }

        private void formeditperubahanperbaikan_Load(object sender, EventArgs e)
        {
            HitungTotalData();
            tampil();
            datecari.Value = DateTime.Now;
            datecari.Checked = false;
        }

        private void txtc_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txte1ers_TextChanged(object sender, EventArgs e)
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

        private void txte4_TextChanged(object sender, EventArgs e)
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

        private void txtba1_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtcr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtm_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtr_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtbac_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtnba_TextChanged(object sender, EventArgs e)
        {
            hitung();
        }

        private void txtrl_TextChanged(object sender, EventArgs e)
        {
            hitung();
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
                lbltotal.Text = row.Cells["jumlah"].Value.ToString();
                tanggalpenerimaan = Convert.ToDateTime(row.Cells["tanggal_penerimaan"].Value);
                txtcatatan.Text = row.Cells["catatan"].Value.ToString();

                if (row.Cells["foto"].Value != DBNull.Value && row.Cells["foto"].Value is byte[])
                {
                    byte[] fotoBytes = (byte[])row.Cells["foto"].Value;
                    using (MemoryStream ms = new MemoryStream(fotoBytes))
                    {
                        fotoSementara = new Bitmap(ms);
                    }
                }
                else
                {
                    fotoSementara = null;
                }


                settrue();

                btncancel.Enabled = true;
                txtjenis.Focus();
            }
        }

        private void editdata()
        {
            try
            {
                DialogResult result1 = MessageBox.Show(
                    "Apakah Anda yakin ingin memperbarui data ini?",
                    "Konfirmasi",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                );

                if (result1 != DialogResult.OK) return;

                conn.Open();

                // Base query tanpa foto
                string query = @"
        UPDATE buktiperubahan SET
            tanggal_perbaikan = @tanggal,
            shift = @shift,
            nomor_rod = @nomorrod,
            jenis = @jenis,
            e1_ers = @e1ers,
            e1_est = @e1est,
            e1_jumlah = @e1jumlah,
            e2_ers = @e2ers,
            e2_cst = @e2cst,
            e2_cstub = @e2cstub,
            e2_jumlah = @e2jumlah,
            e3 = @e3,
            e4 = @e4,
            s = @s,
            d = @d,
            b = @b,
            bac = @bac,
            nba = @nba,
            ba = @ba,
            ba1 = @ba1,
            cr = @cr,
            m = @m,
            r = @r,
            c = @c,
            rl = @rl,
            jumlah = @jumlah,
            tanggal_penerimaan = @tanggalpenerimaan,
            updated_at = @diubah,
            remaks = @remaks,
            catatan = @catatan
        WHERE no = @no";

                if (fotoDiganti && fotoSementara != null)
                {
                    query = query.Replace("WHERE no = @no", ", foto = @foto WHERE no = @no");
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@no", noprimary);
                cmd.Parameters.AddWithValue("@tanggal", tanggalperbaikan);
                cmd.Parameters.AddWithValue("@shift", shift);
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
                cmd.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);
                cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                cmd.Parameters.AddWithValue("@remaks", loginform.login.name);
                cmd.Parameters.AddWithValue("@catatan", txtcatatan.Text);

                if (fotoDiganti && fotoSementara != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fotoSementara.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        cmd.Parameters.AddWithValue("@foto", ms.ToArray());
                    }
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tampil();
                fotoDiganti = false;
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
            finally
            {
                conn.Close();
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
            }

            using (var frm = new formkamera())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.HasilFoto != null)
                    {
                        fotoSementara = new Bitmap(frm.HasilFoto);

                        fotoDiganti = true;

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


        private void btncari_Click(object sender, EventArgs e)
        {
            if (!infocari)
            {
                bool hasilCari = cari();
                if (hasilCari)
                {
                    infocari = true;
                    btncari.Text = "Reset";
                }
                else
                {
                    infocari = true;
                    btncari.Text = "Reset";
                }
            }
            else
            {
                isSearching = false;
                infocari = false;

                txtcari.Text = "";
                datecari.Checked = false;

                btncari.Text = "Cari";

                HitungTotalData();
                currentPage = 1;
                tampil();
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            txtnomorrod.Enabled = true;
            setdefault();
            txtnomorrod.Enabled = false;
            setfalse();
            btncancel.Enabled = false;
            btnedit.Enabled = false;
            dataGridView1.ClearSelection();
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
                                    fotoDiganti = true;
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
                tampil();
            }
        }

        private void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                tampil();
            }
        }
    }
}
