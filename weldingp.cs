using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace GOS_FxApps
{
    public partial class weldingp : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        private int bstok;
        private int wpe1;
        private int wpe2;
        private int wbe1;
        private int wbe2;
        private double wastekg;
        private int ttle1e2mm;
        bool infocari = false;
        int idmulai;        

        public class datarows
        {
            public int id { get; set; }
            public double dsmasuk { get; set; }
            public double dskeluar { get; set; }
            public double dsstok { get; set; }
            public double dsmasukpotonge1 { get; set; }
            public double dsmasukpotonge2 { get; set; }
            public double dsmasukbubute1 { get; set; }
            public double dsmasukbubute2 { get; set; }
            public double dsrbkeluare1 { get; set; }
            public double dsrbkeluare2 { get; set; }
            public double dsstokpotonge1 { get; set; }
            public double dsstokpotonge2 { get; set; }
            public double dsstokbubute1 { get; set; }
            public double dsstokbubute2 { get; set; }
            public double dssisapotongkg { get; set; }
            public double dswastekg { get; set; }
        }

        public weldingp()
        {
            InitializeComponent();
        }


        private void registerperbaikan()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.perbaikan_s", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampil();
                            registerperbaikan();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        //Kode Kode Tampil Data
        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM Rb_Stok ORDER BY tanggal ASC, shift ASC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "RB Masuk";
                dataGridView1.Columns[4].HeaderText = "RB Keluar";
                dataGridView1.Columns[5].HeaderText = "Stock";
                dataGridView1.Columns[6].HeaderText = "Panjang RB (mm)";
                dataGridView1.Columns[7].HeaderText = "Sisa Potongan RB (mm)";
                dataGridView1.Columns[8].HeaderText = "RB Sawing E1-155 mm";
                dataGridView1.Columns[9].HeaderText = "RB Sawing E2-220 mm";
                dataGridView1.Columns[10].HeaderText = "RB Lathe E1-155 mm";
                dataGridView1.Columns[11].HeaderText = "RB Lathe E2-220 mm";
                dataGridView1.Columns[12].HeaderText = "Produksi RB E1-155 mm";
                dataGridView1.Columns[13].HeaderText = "Produksi RB E2-220 mm";
                dataGridView1.Columns[14].HeaderText = "Stock WPS E1-155 mm";
                dataGridView1.Columns[15].HeaderText = "Stock WPS E2-220 mm";
                dataGridView1.Columns[16].HeaderText = "Stock WPL E1-155 mm";
                dataGridView1.Columns[17].HeaderText = "Stock WPL E2-220 mm";
                dataGridView1.Columns[18].HeaderText = "Sisa Potongan RB (Kg)";
                dataGridView1.Columns[19].HeaderText = "Waste (Kg)";
                dataGridView1.Columns[20].HeaderText = "E1 (MM)";
                dataGridView1.Columns[21].HeaderText = "E2 (MM)";
                dataGridView1.Columns[22].HeaderText = "Total E1&E2";
                dataGridView1.Columns[23].HeaderText = "Waste";
                dataGridView1.Columns[24].HeaderText = "Keterangan";
                dataGridView1.Columns[25].HeaderText = "Diubah";
                dataGridView1.Columns[26].HeaderText = "Remaks";
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

        private void HitungDataSimpan()
        {
            DateTime tanggalinput = date.Value;
            int shiftinput = Convert.ToInt32(shift.SelectedItem);

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                conn.Open();

                AmbilDataTerakhir(conn, tanggalinput, shiftinput);
                HitungBstok();
                HitungWpeWbe();
                HitungWaste();
            }
        }
        private void AmbilDataTerakhir(SqlConnection conn, DateTime tanggal, int shift)
        {
            try
            {
                SqlCommand cmdLast = new SqlCommand(
                    @"SELECT TOP 1 bstok, wpe1, wpe2, wbe1, wbe2, wastekg
              FROM Rb_Stok
              WHERE (tanggal < @tgl) OR (tanggal = @tgl AND shift < @shift)
              ORDER BY tanggal DESC, shift DESC", conn);
                cmdLast.Parameters.AddWithValue("@tgl", tanggal);
                cmdLast.Parameters.AddWithValue("@shift", shift);

                using (SqlDataReader reader = cmdLast.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bstok = Convert.ToInt32(reader["bstok"]);
                        wpe1 = Convert.ToInt32(reader["wpe1"]);
                        wpe2 = Convert.ToInt32(reader["wpe2"]);
                        wbe1 = Convert.ToInt32(reader["wbe1"]);
                        wbe2 = Convert.ToInt32(reader["wbe2"]);
                        wastekg = Convert.ToDouble(reader["wastekg"]);
                    }
                    else
                    {
                        bstok = wpe1 = wpe2 = wbe1 = wbe2 = 0;
                        wastekg = 0;
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
        private void HitungBstok()
        {
            int masuk = SafeParse(txtmasuk.Text);
            int keluar = SafeParse(txtkeluar.Text);
            int totalrb = bstok + masuk - keluar;
            lblstoksekarang.Text = totalrb.ToString();
        }
        private void HitungWpeWbe()
        {
            int rbse1 = SafeParse(sawinge1.Text);
            int rble1 = SafeParse(lathee1.Text);
            int rbse2 = SafeParse(sawinge2.Text);
            int rble2 = SafeParse(lathee2.Text);

            int ttlsawinge1 = wpe1 + rbse1 - rble1;
            int ttlsawinge2 = wpe2 + rbse2 - rble2;

            int ttle1mm = rbse1 * 155;
            int ttle2mm = rbse2 * 220;
            ttle1e2mm = ttle1mm + ttle2mm;

            lble1mm.Text = ttle1mm.ToString();
            lble2mm.Text = ttle2mm.ToString();
            ttlstoksawinge1.Text = ttlsawinge1.ToString();
            ttlstoksawinge2.Text = ttlsawinge2.ToString();
            lblttle1e2.Text = ttle1e2mm.ToString();

            int ttllathee1 = wbe1 + rble1 - SafeParse(pkeluare1.Text);
            int ttllathee2 = wbe2 + rble2 - SafeParse(pkeluare2.Text);

            ttlstoklathee1.Text = ttllathee1.ToString();
            ttlstoklathee2.Text = ttllathee2.ToString();
        }
        private void HitungWaste()
        {
            int sisarbkg = SafeParse(txtsbarkg.Text);
            int panjangrbmm = SafeParse(txtpbar.Text);

            double ttlwastekg = wastekg + sisarbkg;
            int ttlwaste = panjangrbmm - ttle1e2mm;

            lblwastekg.Text = ttlwastekg.ToString();
            lblwaste.Text = ttlwaste.ToString();
        }

        private int SafeParse(string input)
        {
            int hasil;
            if (int.TryParse(input, out hasil))
                return hasil;
            else
                return 0;
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
            txtmasuk.Clear();
            txtkeluar.Clear();
            lblstoksekarang.Text = "-";
            txtpbar.Clear();
            txtsbarmm.Clear();
            sawinge1.Clear();
            sawinge2.Clear();
            lathee1.Clear();
            lathee2.Clear();
            pkeluare1.Clear();
            pkeluare2.Clear();
            ttlstoksawinge1.Text = "-";
            ttlstoksawinge2.Text = "-";
            ttlstoklathee1.Text = "-";
            ttlstoklathee2.Text = "-";
            txtsbarkg.Clear();
            lblwastekg.Text = "-";
            lble1mm.Text = "-";
            lble2mm.Text = "-";
            lblttle1e2.Text = "-";
            lblwaste.Text = "-";
            txtketerangan.Clear();
        }

        private void weldingp_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            btnsimpan.Enabled = false;
            tampil();
            date.Value = DateTime.Now.Date;
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            txtmasuk.Focus();
            registerperbaikan();
        }
        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!tanggal.HasValue && !shiftValid)
            {
                MessageBox.Show("Silakan isi tanggal atau shift untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DataTable dt = new DataTable();
            string query = "SELECT * FROM Rb_Stok WHERE 1=1 ";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += " AND CAST(tanggal AS DATE) = @tgl ";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (shiftValid)
                {
                    query += " AND shift = @shift ";
                    cmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem.ToString());
                }

                query += " ORDER BY tanggal ASC, id_stok ASC";

                cmd.CommandText = query;
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.DataSource = dt;
                }
                catch (Exception)
                {
                    MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                return dt.Rows.Count > 0;
            }
        }

        private void Simpan1(SqlConnection conn)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Apakah Anda yakin ingin menyimpan data baru?",
                    "Konfirmasi",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (result != DialogResult.OK) return;

                using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO Rb_Stok 
                (tanggal, shift, bmasuk, bkeluar, bstok, bpanjang, bsisamm, 
                 bpe1, bpe2, bbe1, bbe2, rbkeluare1, rbkeluare2,
                 wpe1, wpe2, wbe1, wbe2, bsisakg, wastekg, e1mm, e2mm, ttle1e2, waste, 
                 keterangan, updated_at, remaks)
                VALUES
                (@tanggal, @shift, @bmasuk, @bkeluar, @bstok, @bpanjang, @bsisamm, 
                 @bpe1, @bpe2, @bbe1, @bbe2, @rbkeluare1, @rbkeluare2, 
                 @wpe1, @wpe2, @wbe1, @wbe2, @bsisakg, @wastekg, @e1mm, @e2mm, @ttle1e2, @waste, 
                 @keterangan, @diubah, @remaks)", conn))
                {
                    // Input user
                    cmd.Parameters.AddWithValue("@tanggal", date.Value);
                    cmd.Parameters.AddWithValue("@shift", shift.SelectedItem);
                    cmd.Parameters.AddWithValue("@bmasuk", SafeParse(txtmasuk.Text));
                    cmd.Parameters.AddWithValue("@bkeluar", SafeParse(txtkeluar.Text));
                    cmd.Parameters.AddWithValue("@bpanjang", SafeParse(txtpbar.Text));
                    cmd.Parameters.AddWithValue("@bsisamm", SafeParse(txtsbarmm.Text));
                    cmd.Parameters.AddWithValue("@bpe1", SafeParse(sawinge1.Text));
                    cmd.Parameters.AddWithValue("@bpe2", SafeParse(sawinge2.Text));
                    cmd.Parameters.AddWithValue("@bbe1", SafeParse(lathee1.Text));
                    cmd.Parameters.AddWithValue("@bbe2", SafeParse(lathee2.Text));
                    cmd.Parameters.AddWithValue("@rbkeluare1", SafeParse(pkeluare1.Text));
                    cmd.Parameters.AddWithValue("@rbkeluare2", SafeParse(pkeluare2.Text));
                    cmd.Parameters.AddWithValue("@bsisakg", SafeParse(txtsbarkg.Text));
                    cmd.Parameters.AddWithValue("@keterangan", txtketerangan.Text);
                    cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd.Parameters.AddWithValue("@remaks", loginform.login.name);

                    // Hasil hitung
                    cmd.Parameters.AddWithValue("@bstok", Convert.ToInt32(lblstoksekarang.Text));
                    cmd.Parameters.AddWithValue("@wpe1", ttlstoksawinge1.Text);
                    cmd.Parameters.AddWithValue("@wpe2", ttlstoksawinge2.Text);
                    cmd.Parameters.AddWithValue("@wbe1", ttlstoklathee1.Text);
                    cmd.Parameters.AddWithValue("@wbe2", ttlstoklathee2.Text);

                    double wk = double.Parse(lblwastekg.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    cmd.Parameters.AddWithValue("@wastekg", wk);

                    cmd.Parameters.AddWithValue("@e1mm", Convert.ToInt32(lble1mm.Text));
                    cmd.Parameters.AddWithValue("@e2mm", Convert.ToInt32(lble2mm.Text));
                    cmd.Parameters.AddWithValue("@ttle1e2", Convert.ToInt32(lblttle1e2.Text));
                    cmd.Parameters.AddWithValue("@waste", Convert.ToInt32(lblwaste.Text));

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Data baru berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();
                tampil();

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
        private void Simpan2(SqlConnection conn, DateTime tanggalInput, int shiftInput)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Apakah Anda yakin ingin menambahkan data ke shift yang sama?",
                    "Konfirmasi",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (result != DialogResult.OK) return;

                // Ambil data shift saat ini
                SqlCommand cmdCheck = new SqlCommand(@"
            SELECT * FROM Rb_Stok
            WHERE tanggal = @tgl AND shift = @shift", conn);
                cmdCheck.Parameters.AddWithValue("@tgl", tanggalInput);
                cmdCheck.Parameters.AddWithValue("@shift", shiftInput);

                using (SqlDataReader dr = cmdCheck.ExecuteReader())
                {
                    if (!dr.Read())
                    {
                        MessageBox.Show("Data untuk tanggal dan shift ini tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int id = Convert.ToInt32(dr["id_stok"]);

                    // Ambil nilai lama
                    int bmasukLama = Convert.ToInt32(dr["bmasuk"]);
                    int bkeluarLama = Convert.ToInt32(dr["bkeluar"]);
                    int bpanjangLama = Convert.ToInt32(dr["bpanjang"]);
                    int bsisammLama = Convert.ToInt32(dr["bsisamm"]);
                    int bpe1Lama = Convert.ToInt32(dr["bpe1"]);
                    int bpe2Lama = Convert.ToInt32(dr["bpe2"]);
                    int bbe1Lama = Convert.ToInt32(dr["bbe1"]);
                    int bbe2Lama = Convert.ToInt32(dr["bbe2"]);
                    int rbkeluare1Lama = Convert.ToInt32(dr["rbkeluare1"]);
                    int rbkeluare2Lama = Convert.ToInt32(dr["rbkeluare2"]);
                    int bsisakgLama = Convert.ToInt32(dr["bsisakg"]);
                    double wpe1Lama = Convert.ToDouble(dr["wpe1"]);
                    double wpe2Lama = Convert.ToDouble(dr["wpe2"]);
                    double wbe1Lama = Convert.ToDouble(dr["wbe1"]);
                    double wbe2Lama = Convert.ToDouble(dr["wbe2"]);
                    double wastekgLama = Convert.ToDouble(dr["wastekg"]);

                    dr.Close();

                    // Hitung nilai baru untuk shift ini
                    int bmasukBaru = bmasukLama + SafeParse(txtmasuk.Text);
                    int bkeluarBaru = bkeluarLama + SafeParse(txtkeluar.Text);
                    int bpanjangBaru = bpanjangLama + SafeParse(txtpbar.Text);
                    int bsisammBaru = bsisammLama + SafeParse(txtsbarmm.Text);
                    int bpe1Baru = bpe1Lama + SafeParse(sawinge1.Text);
                    int bpe2Baru = bpe2Lama + SafeParse(sawinge2.Text);
                    int bbe1Baru = bbe1Lama + SafeParse(lathee1.Text);
                    int bbe2Baru = bbe2Lama + SafeParse(lathee2.Text);
                    int rbkeluare1Baru = rbkeluare1Lama + SafeParse(pkeluare1.Text);
                    int rbkeluare2Baru = rbkeluare2Lama + SafeParse(pkeluare2.Text);
                    int bsisakgBaru = bsisakgLama + SafeParse(txtsbarkg.Text);

                    // Ambil stok terakhir sebelum shift ini sebagai dasar
                    double bstokAwal = 0;
                    using (SqlCommand cmdLast = new SqlCommand(@"
                SELECT TOP 1 bstok, wpe1, wpe2, wbe1, wbe2, wastekg
                FROM Rb_Stok
                WHERE tanggal < @tgl OR (tanggal = @tgl AND shift < @shift)
                ORDER BY tanggal DESC, shift DESC, id_stok DESC", conn))
                    {
                        cmdLast.Parameters.AddWithValue("@tgl", tanggalInput);
                        cmdLast.Parameters.AddWithValue("@shift", shiftInput);

                        using (SqlDataReader drLast = cmdLast.ExecuteReader())
                        {
                            if (drLast.Read())
                            {
                                bstokAwal = Convert.ToDouble(drLast["bstok"]);
                                wpe1Lama = Convert.ToDouble(drLast["wpe1"]);
                                wpe2Lama = Convert.ToDouble(drLast["wpe2"]);
                                wbe1Lama = Convert.ToDouble(drLast["wbe1"]);
                                wbe2Lama = Convert.ToDouble(drLast["wbe2"]);
                                wastekgLama = Convert.ToDouble(drLast["wastekg"]);
                            }
                        }
                    }

                    // Hitung stok kumulatif shift ini
                    double bstokBaru = bstokAwal + bmasukBaru - bkeluarBaru;
                    double wpe1BaruC = wpe1Lama + bpe1Baru - bbe1Baru;
                    double wpe2BaruC = wpe2Lama + bpe2Baru - bbe2Baru;
                    double wbe1BaruC = wbe1Lama + bbe1Baru - rbkeluare1Baru;
                    double wbe2BaruC = wbe2Lama + bbe2Baru - rbkeluare2Baru;
                    double wastekgBaru = wastekgLama + bsisakgBaru;
                    int e1mmBaru = (bpe1Baru) * 155;
                    int e2mmBaru = (bpe2Baru) * 220;
                    int ttle1e2Baru = e1mmBaru + e2mmBaru;
                    double wasteBaru = bpanjangBaru - ttle1e2Baru;

                    // Update shift ini
                    using (SqlCommand cmdUpdate = new SqlCommand(@"
                UPDATE Rb_Stok SET
                    bmasuk=@bmasuk, bkeluar=@bkeluar, bpanjang=@bpanjang, bsisamm=@bsisamm,
                    bpe1=@bpe1, bpe2=@bpe2, bbe1=@bbe1, bbe2=@bbe2,
                    rbkeluare1=@rbkeluare1, rbkeluare2=@rbkeluare2, bsisakg=@bsisakg,
                    bstok=@bstok, wpe1=@wpe1, wpe2=@wpe2, wbe1=@wbe1, wbe2=@wbe2, wastekg=@wastekg,
                    e1mm=@e1mm, e2mm=@e2mm, ttle1e2=@ttle1e2, waste=@waste, remaks = @remaks
                WHERE id_stok=@id", conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@bmasuk", bmasukBaru);
                        cmdUpdate.Parameters.AddWithValue("@bkeluar", bkeluarBaru);
                        cmdUpdate.Parameters.AddWithValue("@bpanjang", bpanjangBaru);
                        cmdUpdate.Parameters.AddWithValue("@bsisamm", bsisammBaru);
                        cmdUpdate.Parameters.AddWithValue("@bpe1", bpe1Baru);
                        cmdUpdate.Parameters.AddWithValue("@bpe2", bpe2Baru);
                        cmdUpdate.Parameters.AddWithValue("@bbe1", bbe1Baru);
                        cmdUpdate.Parameters.AddWithValue("@bbe2", bbe2Baru);
                        cmdUpdate.Parameters.AddWithValue("@rbkeluare1", rbkeluare1Baru);
                        cmdUpdate.Parameters.AddWithValue("@rbkeluare2", rbkeluare2Baru);
                        cmdUpdate.Parameters.AddWithValue("@bsisakg", bsisakgBaru);
                        cmdUpdate.Parameters.AddWithValue("@bstok", bstokBaru);
                        cmdUpdate.Parameters.AddWithValue("@wpe1", wpe1BaruC);
                        cmdUpdate.Parameters.AddWithValue("@wpe2", wpe2BaruC);
                        cmdUpdate.Parameters.AddWithValue("@wbe1", wbe1BaruC);
                        cmdUpdate.Parameters.AddWithValue("@wbe2", wbe2BaruC);
                        cmdUpdate.Parameters.AddWithValue("@wastekg", wastekgBaru);
                        cmdUpdate.Parameters.AddWithValue("@e1mm", e1mmBaru);
                        cmdUpdate.Parameters.AddWithValue("@e2mm", e2mmBaru);
                        cmdUpdate.Parameters.AddWithValue("@ttle1e2", ttle1e2Baru);
                        cmdUpdate.Parameters.AddWithValue("@waste", wasteBaru);
                        cmdUpdate.Parameters.AddWithValue("@remaks", loginform.login.name);
                        cmdUpdate.Parameters.AddWithValue("@id", id);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    UpdateShiftSetelahnya(conn, tanggalInput, shiftInput);

                    MessageBox.Show("Data berhasil ditambahkan ke shift yang sama.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
                    tampil();
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
        private void Simpan3(SqlConnection conn, DateTime tanggalInput, int shiftInput)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Apakah Anda yakin ingin menambahkan data baru di pertengahan?",
                    "Konfirmasi",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (result != DialogResult.OK) return;

                double bstokAwal = 0, wpe1Awal = 0, wpe2Awal = 0, wbe1Awal = 0, wbe2Awal = 0, wastekgAwal = 0;

                using (SqlCommand cmdLast = new SqlCommand(@"
            SELECT TOP 1 bstok, wpe1, wpe2, wbe1, wbe2, wastekg
            FROM Rb_Stok
            WHERE tanggal < @tgl OR (tanggal = @tgl AND shift < @shift)
            ORDER BY tanggal DESC, shift DESC, id_stok DESC", conn))
                {
                    cmdLast.Parameters.AddWithValue("@tgl", tanggalInput);
                    cmdLast.Parameters.AddWithValue("@shift", shiftInput);

                    using (SqlDataReader drLast = cmdLast.ExecuteReader())
                    {
                        if (drLast.Read())
                        {
                            bstokAwal = Convert.ToDouble(drLast["bstok"]);
                            wpe1Awal = Convert.ToDouble(drLast["wpe1"]);
                            wpe2Awal = Convert.ToDouble(drLast["wpe2"]);
                            wbe1Awal = Convert.ToDouble(drLast["wbe1"]);
                            wbe2Awal = Convert.ToDouble(drLast["wbe2"]);
                            wastekgAwal = Convert.ToDouble(drLast["wastekg"]);
                        }
                    }
                }

                int bmasuk = SafeParse(txtmasuk.Text);
                int bkeluar = SafeParse(txtkeluar.Text);
                int bpanjang = SafeParse(txtpbar.Text);
                int bsisamm = SafeParse(txtsbarmm.Text);
                int bpe1 = SafeParse(sawinge1.Text);
                int bpe2 = SafeParse(sawinge2.Text);
                int bbe1 = SafeParse(lathee1.Text);
                int bbe2 = SafeParse(lathee2.Text);
                int rbkeluare1 = SafeParse(pkeluare1.Text);
                int rbkeluare2 = SafeParse(pkeluare2.Text);
                int bsisakg = SafeParse(txtsbarkg.Text);

                double wk = double.Parse(lblwastekg.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

                double bstok = bstokAwal + bmasuk - bkeluar;
                double wpe1 = wpe1Awal + bpe1 - bbe1;
                double wpe2 = wpe2Awal + bpe2 - bbe2;
                double wbe1 = wbe1Awal + bbe1 - rbkeluare1;
                double wbe2 = wbe2Awal + bbe2 - rbkeluare2;
                double wastekg = wastekgAwal + bsisakg;

                int e1mm = bpe1 * 155;
                int e2mm = bpe2 * 220;
                int ttle1e2 = e1mm + e2mm;
                int waste = bpanjang - ttle1e2;

                using (SqlCommand cmdInsert = new SqlCommand(@"
            INSERT INTO Rb_Stok 
            (tanggal, shift, bmasuk, bkeluar, bpanjang, bsisamm,
             bpe1, bpe2, bbe1, bbe2, rbkeluare1, rbkeluare2,
             bsisakg, bstok, wpe1, wpe2, wbe1, wbe2, wastekg,
             e1mm, e2mm, ttle1e2, waste, keterangan, updated_at, remaks)
            VALUES
            (@tanggal, @shift, @bmasuk, @bkeluar, @bpanjang, @bsisamm,
             @bpe1, @bpe2, @bbe1, @bbe2, @rbkeluare1, @rbkeluare2,
             @bsisakg, @bstok, @wpe1, @wpe2, @wbe1, @wbe2, @wastekg,
             @e1mm, @e2mm, @ttle1e2, @waste, @keterangan, @diubah, @remaks)", conn))
                {
                    cmdInsert.Parameters.AddWithValue("@tanggal", tanggalInput);
                    cmdInsert.Parameters.AddWithValue("@shift", shiftInput);
                    cmdInsert.Parameters.AddWithValue("@bmasuk", bmasuk);
                    cmdInsert.Parameters.AddWithValue("@bkeluar", bkeluar);
                    cmdInsert.Parameters.AddWithValue("@bpanjang", bpanjang);
                    cmdInsert.Parameters.AddWithValue("@bsisamm", bsisamm);
                    cmdInsert.Parameters.AddWithValue("@bpe1", bpe1);
                    cmdInsert.Parameters.AddWithValue("@bpe2", bpe2);
                    cmdInsert.Parameters.AddWithValue("@bbe1", bbe1);
                    cmdInsert.Parameters.AddWithValue("@bbe2", bbe2);
                    cmdInsert.Parameters.AddWithValue("@rbkeluare1", rbkeluare1);
                    cmdInsert.Parameters.AddWithValue("@rbkeluare2", rbkeluare2);
                    cmdInsert.Parameters.AddWithValue("@bsisakg", bsisakg);
                    cmdInsert.Parameters.AddWithValue("@bstok", bstok);
                    cmdInsert.Parameters.AddWithValue("@wpe1", wpe1);
                    cmdInsert.Parameters.AddWithValue("@wpe2", wpe2);
                    cmdInsert.Parameters.AddWithValue("@wbe1", wbe1);
                    cmdInsert.Parameters.AddWithValue("@wbe2", wbe2);
                    cmdInsert.Parameters.AddWithValue("@wastekg", wk);
                    cmdInsert.Parameters.AddWithValue("@e1mm", e1mm);
                    cmdInsert.Parameters.AddWithValue("@e2mm", e2mm);
                    cmdInsert.Parameters.AddWithValue("@ttle1e2", ttle1e2);
                    cmdInsert.Parameters.AddWithValue("@waste", waste);
                    cmdInsert.Parameters.AddWithValue("@keterangan", txtketerangan.Text);
                    cmdInsert.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmdInsert.Parameters.AddWithValue("@remaks", loginform.login.name);

                    cmdInsert.ExecuteNonQuery();
                }

                UpdateShiftSetelahnya(conn, tanggalInput, shiftInput);

                MessageBox.Show("Data baru berhasil disimpan di pertengahan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();
                tampil();
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
        private void EditData(SqlConnection conn, int id)
        {
            try
            {
                SqlCommand cmdOld = new SqlCommand("SELECT * FROM Rb_Stok WHERE id_stok=@id", conn);
                cmdOld.Parameters.AddWithValue("@id", id);

                int bmasuk, bkeluar, bpanjang, bsisamm, bpe1, bpe2, bbe1, bbe2, rbkeluare1, rbkeluare2, bsisakg;
                double wpe1, wpe2, wbe1, wbe2, wastekg;
                DateTime tanggal;
                int shiftInput;

                using (SqlDataReader dr = cmdOld.ExecuteReader())
                {
                    if (!dr.Read())
                    {
                        MessageBox.Show("Data tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bmasuk = SafeParse(txtmasuk.Text);
                    bkeluar = SafeParse(txtkeluar.Text);
                    bpanjang = SafeParse(txtpbar.Text);
                    bsisamm = SafeParse(txtsbarmm.Text);
                    bpe1 = SafeParse(sawinge1.Text);
                    bpe2 = SafeParse(sawinge2.Text);
                    bbe1 = SafeParse(lathee1.Text);
                    bbe2 = SafeParse(lathee2.Text);
                    rbkeluare1 = SafeParse(pkeluare1.Text);
                    rbkeluare2 = SafeParse(pkeluare2.Text);
                    bsisakg = SafeParse(txtsbarkg.Text);
                    wpe1 = double.Parse(ttlstoksawinge1.Text);
                    wpe2 = double.Parse(ttlstoksawinge2.Text);
                    wbe1 = double.Parse(ttlstoklathee1.Text);
                    wbe2 = double.Parse(ttlstoklathee2.Text);
                    wastekg = double.Parse(lblwastekg.Text.Replace(',', '.'));
                    tanggal = date.Value;
                    shiftInput = Convert.ToInt32(shift.SelectedItem);
                }

                // Hitung kolom per-shift
                int e1mm = bpe1 * 155;
                int e2mm = bpe2 * 220;
                int ttle1e2 = e1mm + e2mm;
                int waste = bpanjang - ttle1e2;

                // Ambil stok terakhir sebelum shift ini
                double bstokAwal = 0, wpe1Awal = 0, wpe2Awal = 0, wbe1Awal = 0, wbe2Awal = 0, wastekgAwal = 0;

                using (SqlCommand cmdLast = new SqlCommand(@"
            SELECT TOP 1 bstok, wpe1, wpe2, wbe1, wbe2, wastekg
            FROM Rb_Stok
            WHERE tanggal < @tgl OR (tanggal = @tgl AND shift < @shift)
            ORDER BY tanggal DESC, shift DESC, id_stok DESC", conn))
                {
                    cmdLast.Parameters.AddWithValue("@tgl", tanggal);
                    cmdLast.Parameters.AddWithValue("@shift", shiftInput);

                    using (SqlDataReader drLast = cmdLast.ExecuteReader())
                    {
                        if (drLast.Read())
                        {
                            bstokAwal = Convert.ToDouble(drLast["bstok"]);
                            wpe1Awal = Convert.ToDouble(drLast["wpe1"]);
                            wpe2Awal = Convert.ToDouble(drLast["wpe2"]);
                            wbe1Awal = Convert.ToDouble(drLast["wbe1"]);
                            wbe2Awal = Convert.ToDouble(drLast["wbe2"]);
                            wastekgAwal = Convert.ToDouble(drLast["wastekg"]);
                        }
                    }
                }

                // Hitung stok kumulatif shift ini
                double bstok = bstokAwal + bmasuk - bkeluar;
                double wpe1Cum = wpe1Awal + bpe1 - bbe1;
                double wpe2Cum = wpe2Awal + bpe2 - bbe2;
                double wbe1Cum = wbe1Awal + bbe1 - rbkeluare1;
                double wbe2Cum = wbe2Awal + bbe2 - rbkeluare2;
                double wastekgCum = wastekgAwal + bsisakg;

                // Update data shift ini
                using (SqlCommand cmdUpdate = new SqlCommand(@"
            UPDATE Rb_Stok SET
                bmasuk=@bmasuk, bkeluar=@bkeluar, bpanjang=@bpanjang, bsisamm=@bsisamm,
                bpe1=@bpe1, bpe2=@bpe2, bbe1=@bbe1, bbe2=@bbe2,
                rbkeluare1=@rbkeluare1, rbkeluare2=@rbkeluare2, bsisakg=@bsisakg,
                bstok=@bstok, wpe1=@wpe1, wpe2=@wpe2, wbe1=@wbe1, wbe2=@wbe2,
                wastekg=@wastekg, e1mm=@e1mm, e2mm=@e2mm, ttle1e2=@ttle1e2, waste=@waste,
                keterangan=@keterangan, updated_at=@diubah, remaks=@remaks
            WHERE id_stok=@id", conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@bmasuk", bmasuk);
                    cmdUpdate.Parameters.AddWithValue("@bkeluar", bkeluar);
                    cmdUpdate.Parameters.AddWithValue("@bpanjang", bpanjang);
                    cmdUpdate.Parameters.AddWithValue("@bsisamm", bsisamm);
                    cmdUpdate.Parameters.AddWithValue("@bpe1", bpe1);
                    cmdUpdate.Parameters.AddWithValue("@bpe2", bpe2);
                    cmdUpdate.Parameters.AddWithValue("@bbe1", bbe1);
                    cmdUpdate.Parameters.AddWithValue("@bbe2", bbe2);
                    cmdUpdate.Parameters.AddWithValue("@rbkeluare1", rbkeluare1);
                    cmdUpdate.Parameters.AddWithValue("@rbkeluare2", rbkeluare2);
                    cmdUpdate.Parameters.AddWithValue("@bsisakg", bsisakg);
                    cmdUpdate.Parameters.AddWithValue("@bstok", bstok);
                    cmdUpdate.Parameters.AddWithValue("@wpe1", wpe1Cum);
                    cmdUpdate.Parameters.AddWithValue("@wpe2", wpe2Cum);
                    cmdUpdate.Parameters.AddWithValue("@wbe1", wbe1Cum);
                    cmdUpdate.Parameters.AddWithValue("@wbe2", wbe2Cum);
                    cmdUpdate.Parameters.AddWithValue("@wastekg", wastekgCum);
                    cmdUpdate.Parameters.AddWithValue("@e1mm", e1mm);
                    cmdUpdate.Parameters.AddWithValue("@e2mm", e2mm);
                    cmdUpdate.Parameters.AddWithValue("@ttle1e2", ttle1e2);
                    cmdUpdate.Parameters.AddWithValue("@waste", waste);
                    cmdUpdate.Parameters.AddWithValue("@keterangan", txtketerangan.Text);
                    cmdUpdate.Parameters.AddWithValue("@diubah",MainForm.Instance.tanggal);
                    cmdUpdate.Parameters.AddWithValue("@remaks", loginform.login.name);
                    cmdUpdate.Parameters.AddWithValue("@id", id);
                    cmdUpdate.ExecuteNonQuery();
                }

                UpdateShiftSetelahnya(conn, tanggal, shiftInput);

                MessageBox.Show("Data berhasil diupdate.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();
                tampil();
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
        private void UpdateShiftSetelahnya(SqlConnection conn, DateTime tanggalInput, int shiftInput)
        {
            // Ambil semua shift setelah shift yang baru diupdate
            string query = @"
SELECT *
FROM Rb_Stok
WHERE tanggal > @tgl
   OR (tanggal = @tgl AND shift > @shift)
ORDER BY tanggal ASC, shift ASC, id_stok ASC";

            List<datarows> rows = new List<datarows>();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@tgl", tanggalInput);
                cmd.Parameters.AddWithValue("@shift", shiftInput);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rows.Add(new datarows
                        {
                            id = Convert.ToInt32(dr["id_stok"]),
                            dsmasuk = Convert.ToDouble(dr["bmasuk"]),
                            dskeluar = Convert.ToDouble(dr["bkeluar"]),
                            dsmasukpotonge1 = Convert.ToDouble(dr["bpe1"]),
                            dsmasukpotonge2 = Convert.ToDouble(dr["bpe2"]),
                            dsmasukbubute1 = Convert.ToDouble(dr["bbe1"]),
                            dsmasukbubute2 = Convert.ToDouble(dr["bbe2"]),
                            dsrbkeluare1 = Convert.ToDouble(dr["rbkeluare1"]),
                            dsrbkeluare2 = Convert.ToDouble(dr["rbkeluare2"]),
                            dssisapotongkg = Convert.ToDouble(dr["bsisakg"]),
                        });
                    }
                }
            }

            if (rows.Count == 0) return; // Tidak ada shift setelahnya

            // Ambil stok terbaru dari shift yang baru saja diupdate sebagai dasar
            double bstok = 0, wpe1 = 0, wpe2 = 0, wbe1 = 0, wbe2 = 0, wastekg = 0;
            using (SqlCommand cmdBase = new SqlCommand(@"
        SELECT TOP 1 bstok, wpe1, wpe2, wbe1, wbe2, wastekg
        FROM Rb_Stok
        WHERE tanggal = @tgl AND shift = @shift", conn))
            {
                cmdBase.Parameters.AddWithValue("@tgl", tanggalInput);
                cmdBase.Parameters.AddWithValue("@shift", shiftInput);

                using (SqlDataReader drBase = cmdBase.ExecuteReader())
                {
                    if (drBase.Read())
                    {
                        bstok = Convert.ToDouble(drBase["bstok"]);
                        wpe1 = Convert.ToDouble(drBase["wpe1"]);
                        wpe2 = Convert.ToDouble(drBase["wpe2"]);
                        wbe1 = Convert.ToDouble(drBase["wbe1"]);
                        wbe2 = Convert.ToDouble(drBase["wbe2"]);
                        wastekg = Convert.ToDouble(drBase["wastekg"]);
                    }
                }
            }

            // Hitung stok kumulatif untuk semua shift setelahnya
            foreach (var ds in rows)
            {
                double bstokNext = bstok + ds.dsmasuk - ds.dskeluar;
                double wpe1Next = wpe1 + ds.dsmasukpotonge1 - ds.dsmasukbubute1;
                double wpe2Next = wpe2 + ds.dsmasukpotonge2 - ds.dsmasukbubute2;
                double wbe1Next = wbe1 + ds.dsmasukbubute1 - ds.dsrbkeluare1;
                double wbe2Next = wbe2 + ds.dsmasukbubute2 - ds.dsrbkeluare2;
                double wasteNext = wastekg + ds.dssisapotongkg;

                using (SqlCommand cmdUpdate = new SqlCommand(@"
            UPDATE Rb_Stok SET
                bstok=@bstok, wpe1=@wpe1, wpe2=@wpe2,
                wbe1=@wbe1, wbe2=@wbe2, wastekg=@wastekg,
                updated_at=GETDATE()
            WHERE id_stok=@id", conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@bstok", bstokNext);
                    cmdUpdate.Parameters.AddWithValue("@wpe1", wpe1Next);
                    cmdUpdate.Parameters.AddWithValue("@wpe2", wpe2Next);
                    cmdUpdate.Parameters.AddWithValue("@wbe1", wbe1Next);
                    cmdUpdate.Parameters.AddWithValue("@wbe2", wbe2Next);
                    cmdUpdate.Parameters.AddWithValue("@wastekg", wasteNext);
                    cmdUpdate.Parameters.AddWithValue("@id", ds.id);

                    cmdUpdate.ExecuteNonQuery();
                }

                // Jadikan shift ini sebagai dasar untuk shift berikutnya
                bstok = bstokNext;
                wpe1 = wpe1Next;
                wpe2 = wpe2Next;
                wbe1 = wbe1Next;
                wbe2 = wbe2Next;
                wastekg = wasteNext;
            }
        }
        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if (shift.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih shift terlebih dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime tanggalInput = date.Value;
            int shiftInput = Convert.ToInt32(shift.SelectedItem);

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                conn.Open();

                if (btnsimpan.Text == "Edit Data")
                {
                    DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin mengedit data?",
                "Konfirmasi",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

                    if (result != DialogResult.OK) return;

                    EditData(conn, idmulai);
                    setdefault();
                    btnbatal.Enabled = false;
                    btnsimpan.Enabled = false;
                    date.Enabled = true;
                    shift.Enabled = true;
                    btnhitung.Text = "Hitung";
                    btnsimpan.Text = "Simpan Data";
                    date.Value = DateTime.Now.Date;
                    shift.SelectedIndex = -1;
                }
                else
                {
                    SqlCommand cmdCheck = new SqlCommand(@"
                                                        SELECT COUNT(*) 
                                                        FROM Rb_Stok 
                                                        WHERE tanggal = @tgl AND shift = @shift", conn);
                    cmdCheck.Parameters.AddWithValue("@tgl", tanggalInput);
                    cmdCheck.Parameters.AddWithValue("@shift", shiftInput);

                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        Simpan2(conn, tanggalInput, shiftInput);
                        setdefault();
                        btnbatal.Enabled = false;
                        btnsimpan.Enabled = false;
                        date.Enabled = true;
                        shift.Enabled = true;
                        btnhitung.Text = "Hitung";
                        btnsimpan.Text = "Simpan Data";
                        date.Value = DateTime.Now.Date;
                        shift.SelectedIndex = -1;
                    }
                    else
                    {
                        SqlCommand cmdAfter = new SqlCommand(@"
                                                        SELECT COUNT(*) 
                                                        FROM Rb_Stok 
                                                        WHERE tanggal > @tgl OR (tanggal = @tgl AND shift > @shift)", conn);
                        cmdAfter.Parameters.AddWithValue("@tgl", tanggalInput);
                        cmdAfter.Parameters.AddWithValue("@shift", shiftInput);

                        int countAfter = (int)cmdAfter.ExecuteScalar();

                        if (countAfter > 0)
                        {
                            Simpan3(conn, tanggalInput, shiftInput);
                            setdefault();
                            btnbatal.Enabled = false;
                            btnsimpan.Enabled = false;
                            date.Enabled = true;
                            shift.Enabled = true;
                            btnhitung.Text = "Hitung";
                            btnsimpan.Text = "Simpan Data";
                            date.Value = DateTime.Now.Date;
                            shift.SelectedIndex = -1;
                        }
                        else
                        {
                            Simpan1(conn);
                            setdefault();
                            btnbatal.Enabled = false;
                            btnsimpan.Enabled = false;
                            date.Enabled = true;
                            shift.Enabled = true;
                            btnhitung.Text = "Hitung";
                            btnsimpan.Text = "Simpan Data";
                            date.Value = DateTime.Now.Date;
                            shift.SelectedIndex = -1;
                        }
                    }
                }
            }
        }

        private void btnhitung_Click(object sender, EventArgs e)
        {
            if (shift.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih shift terlebih dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime tanggalInput = date.Value;
            int shiftInput = Convert.ToInt32(shift.SelectedItem);

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                conn.Open();

                SqlCommand cmdCheck = new SqlCommand(@"
            SELECT COUNT(*) FROM Rb_Stok 
            WHERE tanggal = @tgl AND shift = @shift", conn);
                cmdCheck.Parameters.AddWithValue("@tgl", tanggalInput);
                cmdCheck.Parameters.AddWithValue("@shift", shiftInput);

                int count = (int)cmdCheck.ExecuteScalar();

                if (count > 0)
                {
                    DialogResult result = MessageBox.Show(
                            "Data untuk tanggal dan shift ini sudah ada.\n" +
                            "Perhitungan ini hanya bersifat sementara dan bisa diabaikan.\n" +
                            "Nilai yang sebenarnya bisa dilihat di tabel atau setelah dicetak.",
                            "Informasi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }
                HitungDataSimpan();
                btnsimpan.Enabled = true;
                btnbatal.Enabled = true;
            }
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            setdefault();
            btnbatal.Enabled = false;
            btnsimpan.Enabled = false;
            date.Enabled = true;
            shift.Enabled = true;
            btnhitung.Text = "Hitung";
            btnsimpan.Text = "Simpan Data";
            date.Value = DateTime.Now.Date;
            shift.SelectedIndex = -1;
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
                tampil();
                infocari = false;
                btncari.Text = "Cari";

                cbShift.StartIndex = 0;
                datecari.Checked = false;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MainForm.Instance.role != "Manajer" && MainForm.Instance.role != "Developer") return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                idmulai = Convert.ToInt32(row.Cells["id_stok"].Value);

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MIN(id_stok) FROM Rb_Stok", conn);
                    int firstId = (int)cmd.ExecuteScalar();

                    if (idmulai == firstId)
                    {
                        MessageBox.Show("Data ini tidak bisa diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                date.Value = Convert.ToDateTime(row.Cells["tanggal"].Value);
                shift.SelectedItem = row.Cells["shift"].Value.ToString();
                txtmasuk.Text = row.Cells["bmasuk"].Value.ToString();
                txtkeluar.Text = row.Cells["bkeluar"].Value.ToString();
                lblstoksekarang.Text = row.Cells["bstok"].Value.ToString();
                txtpbar.Text = row.Cells["bpanjang"].Value.ToString();
                txtsbarmm.Text = row.Cells["bsisamm"].Value.ToString();
                sawinge1.Text = row.Cells["bpe1"].Value.ToString();
                sawinge2.Text = row.Cells["bpe2"].Value.ToString();
                lathee1.Text = row.Cells["bbe1"].Value.ToString();
                lathee2.Text = row.Cells["bbe2"].Value.ToString();
                pkeluare1.Text = row.Cells["rbkeluare1"].Value.ToString();
                pkeluare2.Text = row.Cells["rbkeluare2"].Value.ToString();
                ttlstoksawinge1.Text = row.Cells["wpe1"].Value.ToString();
                ttlstoksawinge2.Text = row.Cells["wpe2"].Value.ToString();
                ttlstoklathee1.Text = row.Cells["wbe1"].Value.ToString();
                ttlstoklathee2.Text = row.Cells["wbe2"].Value.ToString();
                txtsbarkg.Text = row.Cells["bsisakg"].Value.ToString();
                lblwastekg.Text = row.Cells["wastekg"].Value.ToString();
                lble1mm.Text = row.Cells["e1mm"].Value.ToString();
                lble2mm.Text = row.Cells["e2mm"].Value.ToString();
                lblttle1e2.Text = row.Cells["ttle1e2"].Value.ToString();
                lblwaste.Text = row.Cells["waste"].Value.ToString();
                txtketerangan.Text = row.Cells["keterangan"].Value.ToString();

                date.Enabled = false;
                shift.Enabled = false;

                btnsimpan.Text = "Edit Data";

                btnbatal.Enabled = true;
            }
        }

        private void weldingp_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }
    }
}
