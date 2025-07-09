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
using Microsoft.Data.SqlClient;
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

        //Kode Kode Tampil Data
        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM Rb_Stok";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);

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
        private void getdatastok()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 bstok from Rb_Stok order by id_stok DESC;", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblstcokakhir.Text = reader["bstok"].ToString();
                }
                else
                {
                    MessageBox.Show("Data Kosong");
                }
                reader.Close();
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
        private void getdatastokedit()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 id_stok, bstok from Rb_Stok WHERE id_stok < @id ORDER BY id_stok DESC", conn);
                cmd.Parameters.AddWithValue("@id", idmulai);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblstcokakhir.Text = reader["bstok"].ToString();
                }
                else
                {
                    MessageBox.Show("data null");
                }
                reader.Close();
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
        private void getdata()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 bstok,wpe1,wpe2,wbe1,wbe2,wastekg from Rb_Stok order by id_stok DESC;", conn);
                SqlDataReader reader = cmd.ExecuteReader();
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
                    MessageBox.Show("data null");
                }
                reader.Close();
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
        private void getdataedit()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 bstok,wpe1,wpe2,wbe1,wbe2,wastekg from Rb_Stok WHERE id_stok < @id order by id_stok DESC", conn);
                cmd.Parameters.AddWithValue("@id", idmulai);
                SqlDataReader reader = cmd.ExecuteReader();
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
                    MessageBox.Show("data null");
                }
                reader.Close();
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
        private void tampildataedit()
        {
            DateTime tanggal = DateTime.Now.Date;
            string shift = MainForm.Instance.lblshift.Text;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Rb_Stok WHERE CONVERT(date, tanggal) = @tgl AND shift = @shift", conn);
                cmd.Parameters.AddWithValue("@tgl", tanggal);
                cmd.Parameters.AddWithValue("@shift", shift);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    idmulai = Convert.ToInt32(reader["id_stok"]);
                    txtmasuk.Text = reader["bmasuk"].ToString();
                    txtkeluar.Text = reader["bkeluar"].ToString();
                    lblstoksekarang.Text = reader["bstok"].ToString();
                    txtpbar.Text = reader["bpanjang"].ToString();
                    txtsbarmm.Text = reader["bsisamm"].ToString();
                    sawinge1.Text = reader["bpe1"].ToString();
                    sawinge2.Text = reader["bpe2"].ToString();
                    lathee1.Text = reader["bbe1"].ToString();
                    lathee2.Text = reader["bbe2"].ToString();
                    pkeluare1.Text = reader["rbkeluare1"].ToString();
                    pkeluare2.Text = reader["rbkeluare2"].ToString();
                    ttlstoksawinge1.Text = reader["wpe1"].ToString();
                    ttlstoksawinge2.Text = reader["wpe2"].ToString();
                    ttlstoklathee1.Text = reader["wbe1"].ToString();
                    ttlstoklathee2.Text = reader["wbe2"].ToString();
                    txtsbarkg.Text = reader["bsisakg"].ToString();
                    lblwastekg.Text = reader["wastekg"].ToString();
                    lble1mm.Text = reader["e1mm"].ToString();
                    lble2mm.Text = reader["e2mm"].ToString();
                    lblttle1e2.Text = reader["ttle1e2"].ToString();
                    lblwaste.Text = reader["waste"].ToString();
                    txtketerangan.Text = reader["keterangan"].ToString();
                }
                else
                {
                }
                reader.Close();
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
 

        //Kode Kode Kalkulasi
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
        private void hitungrb()
        {
            int masuk = SafeParse(txtmasuk.Text);
            int keluar = SafeParse(txtkeluar.Text);

            int totalrb = masuk + bstok - keluar;
            lblstoksekarang.Text = totalrb.ToString();
        }
        private void hitungrbs()
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
        }
        private void hitungrbl()
        {
            int rble1 = SafeParse(lathee1.Text);
            int rbke1 = SafeParse(pkeluare1.Text);

            int rble2 = SafeParse(lathee2.Text);
            int rbke2 = SafeParse(pkeluare2.Text);

            int ttllathee1 = wbe1 + rble1 - rbke1;
            int ttllathee2 = wbe2 + rble2 - rbke2;

            ttlstoklathee1.Text = ttllathee1.ToString();
            ttlstoklathee2.Text = ttllathee2.ToString();
        }
        private void hitungwaste()
        {
            int sisarbkg = SafeParse(txtsbarkg.Text);

            int panjangrbmm = SafeParse(txtpbar.Text);

            double ttlwastekg = wastekg + sisarbkg;
            int ttlwaste = panjangrbmm - ttle1e2mm;

            lblwastekg.Text = ttlwastekg.ToString();
            lblwaste.Text = ttlwaste.ToString();
        }
        private void update()
        {
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Rb_Stok WHERE id_stok > @id ORDER BY id_stok ASC";
                    SqlCommand cmd1 = new SqlCommand(query, conn);
                    cmd1.Parameters.AddWithValue("@id", idmulai);
                    SqlDataReader reader = cmd1.ExecuteReader();

                    List<datarows> rows = new List<datarows>();
                    while (reader.Read())
                    {
                        rows.Add(new datarows
                        {
                            id = Convert.ToInt32(reader["id_stok"]),
                            dsmasuk = Convert.ToInt32(reader["bmasuk"]),
                            dskeluar = Convert.ToDouble(reader["bkeluar"]),
                            dsstok = Convert.ToDouble(reader["bstok"]),
                            dsmasukpotonge1 = Convert.ToDouble(reader["bpe1"]),
                            dsmasukpotonge2 = Convert.ToDouble(reader["bpe2"]),
                            dsmasukbubute1 = Convert.ToDouble(reader["bbe1"]),
                            dsmasukbubute2 = Convert.ToDouble(reader["bbe2"]),
                            dsrbkeluare1 = Convert.ToDouble(reader["rbkeluare1"]),
                            dsrbkeluare2 = Convert.ToDouble(reader["rbkeluare2"]),
                            dsstokpotonge1 = Convert.ToDouble(reader["wpe1"]),
                            dsstokpotonge2 = Convert.ToDouble(reader["wpe2"]),
                            dsstokbubute1 = Convert.ToDouble(reader["wbe1"]),
                            dsstokbubute2 = Convert.ToDouble(reader["wbe2"]),
                            dssisapotongkg = Convert.ToDouble(reader["bsisakg"]),
                            dswastekg = Convert.ToDouble(reader["wastekg"]),
                        });
                    }
                    reader.Close();

                    SqlCommand cmddataedit = new SqlCommand("SELECT TOP 1 * FROM Rb_Stok WHERE id_stok = @id", conn);
                    cmddataedit.Parameters.AddWithValue("@id", idmulai);
                    SqlDataReader dr = cmddataedit.ExecuteReader();

                    double dstok = 0,
                           dmasukpotonge1 = 0,
                           dmasukpotonge2 = 0,
                           dkeluarbubute1 = 0,
                           dkeluarbubute2 = 0,
                           drbkeluare1 = 0,
                           drbkeluare2 = 0,
                           dstokpotonge1 = 0,
                           dstokpotonge2 = 0,
                           dstokbubute1 = 0,
                           dstokbubute2 = 0,
                           dsisapotongkg = 0,
                           dwastekg = 0;

                    if (dr.Read())
                    {
                        dstok = Convert.ToDouble(dr["bstok"]);
                        dmasukpotonge1 = Convert.ToDouble(dr["bpe1"]);
                        dmasukpotonge2 = Convert.ToDouble(dr["bpe2"]);
                        dkeluarbubute1 = Convert.ToDouble(dr["bbe1"]);
                        dkeluarbubute2 = Convert.ToDouble(dr["bbe2"]);
                        drbkeluare1 = Convert.ToDouble(dr["rbkeluare1"]);
                        drbkeluare2 = Convert.ToDouble(dr["rbkeluare2"]);
                        dstokpotonge1 = Convert.ToDouble(dr["wpe1"]);
                        dstokpotonge2 = Convert.ToDouble(dr["wpe2"]);
                        dstokbubute1 = Convert.ToDouble(dr["wbe1"]);
                        dstokbubute2 = Convert.ToDouble(dr["wbe2"]);
                        dsisapotongkg = Convert.ToDouble(dr["bsisakg"]);
                        dwastekg = Convert.ToDouble(dr["wastekg"]);
                    }
                    dr.Close();

                    foreach (var ds in rows)
                    {
                        double destok = dstok + ds.dsmasuk - ds.dskeluar;
                        double destokpotonge1 = dstokpotonge1 + ds.dsmasukpotonge1 - ds.dsmasukbubute1;
                        double destokpotonge2 = dstokpotonge2 + ds.dsmasukpotonge2 - ds.dsmasukbubute2;
                        double destokbubute1 = dstokbubute1 + ds.dsmasukbubute1 - ds.dsrbkeluare1;
                        double destokbubute2 = dstokbubute2 + ds.dsmasukbubute2 - ds.dsrbkeluare2;
                        double dewastekg = dwastekg + ds.dssisapotongkg;

                        SqlCommand updateCmd = new SqlCommand(@"
                                                            UPDATE Rb_Stok SET 
                                                                bstok = @bstok, 
                                                                wpe1 = @wpe1, 
                                                                wpe2 = @wpe2, 
                                                                wbe1 = @wbe1, 
                                                                wbe2 = @wbe2, 
                                                                wastekg = @wastekg 
                                                            WHERE id_stok = @id", conn);

                        updateCmd.Parameters.AddWithValue("@bstok", destok);
                        updateCmd.Parameters.AddWithValue("@wpe1", destokpotonge1);
                        updateCmd.Parameters.AddWithValue("@wpe2", destokpotonge2);
                        updateCmd.Parameters.AddWithValue("@wbe1", destokbubute1);
                        updateCmd.Parameters.AddWithValue("@wbe2", destokbubute2);
                        updateCmd.Parameters.AddWithValue("@wastekg", dewastekg);
                        updateCmd.Parameters.AddWithValue("@id", ds.id);

                        updateCmd.ExecuteNonQuery();

                        dstok = destok;
                        dstokpotonge1 = destokpotonge1;
                        dstokpotonge2 = destokpotonge2;
                        dstokbubute1 = destokbubute1;
                        dstokbubute2 = destokbubute2;
                        dwastekg = dewastekg;
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


        //Kode Kode Fungsi
        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string shift = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM Rb_Stok WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += "AND CAST(tanggal AS DATE) = @tgl";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(shift))
                {
                    query += " AND shift = @shift";
                    cmd.Parameters.AddWithValue("@shift", Convert.ToInt32(shift));
                }

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
        private void simpandata()
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO Rb_Stok (tanggal,shift,bmasuk,bkeluar,bstok,bpanjang,bsisamm,bpe1,bpe2,bbe1,bbe2,rbkeluare1,rbkeluare2," +
                        "wpe1,wpe2,wbe1,wbe2,bsisakg,wastekg,e1mm,e2mm,ttle1e2,waste,keterangan) VALUES(GETDATE(),@shift,@bmasuk,@bkeluar,@bstok,@bpanjang,@bsisamm,@bpe1,@bpe2," +
                        "@bbe1,@bbe2,@rbkeluare1,@rbkeluare2,@wpe1,@wpe2,@wbe1,@wbe2,@bsisakg,@wastekg,@e1mm,@e2mm,@ttle1e2,@waste,@keterangan)", conn);

                    cmd1.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                    cmd1.Parameters.AddWithValue("@bmasuk", txtmasuk.Text);
                    cmd1.Parameters.AddWithValue("@bkeluar", txtkeluar.Text);
                    cmd1.Parameters.AddWithValue("@bstok", lblstoksekarang.Text);
                    cmd1.Parameters.AddWithValue("@bpanjang", txtpbar.Text);
                    cmd1.Parameters.AddWithValue("@bsisamm", txtsbarmm.Text);
                    cmd1.Parameters.AddWithValue("@bpe1", sawinge1.Text);
                    cmd1.Parameters.AddWithValue("@bpe2", sawinge2.Text);
                    cmd1.Parameters.AddWithValue("@bbe1", lathee1.Text);
                    cmd1.Parameters.AddWithValue("@bbe2", lathee2.Text);
                    cmd1.Parameters.AddWithValue("@rbkeluare1", pkeluare1.Text);
                    cmd1.Parameters.AddWithValue("@rbkeluare2", pkeluare2.Text);
                    cmd1.Parameters.AddWithValue("@wpe1", ttlstoksawinge1.Text);
                    cmd1.Parameters.AddWithValue("@wpe2", ttlstoksawinge2.Text);
                    cmd1.Parameters.AddWithValue("@wbe1", ttlstoklathee1.Text);
                    cmd1.Parameters.AddWithValue("@wbe2", ttlstoklathee2.Text);
                    cmd1.Parameters.AddWithValue("@bsisakg", txtsbarkg.Text);

                    wastekg = double.Parse(lblwastekg.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    cmd1.Parameters.AddWithValue("@wastekg", wastekg);

                    cmd1.Parameters.AddWithValue("@e1mm", lble1mm.Text);
                    cmd1.Parameters.AddWithValue("@e2mm", lble2mm.Text);
                    cmd1.Parameters.AddWithValue("@ttle1e2", lblttle1e2.Text);
                    cmd1.Parameters.AddWithValue("@waste", lblwaste.Text);
                    cmd1.Parameters.AddWithValue("@keterangan", txtketerangan.Text);

                    cmd1.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Disimpan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
                    tampil();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                getdatastok();
            }
        }
        private void editdata()
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Rb_Stok SET bmasuk = @bmasuk, bkeluar = @bkeluar, bstok = @bstok, bpanjang = @bpanjang, bsisamm = @bsisamm, bpe1 = @bpe1, bpe2 = @bpe2, bbe1 = @bbe1," +
                        "bbe2 = @bbe2, rbkeluare1 = @rbkeluare1, rbkeluare2 = @rbkeluare2, wpe1 = @wpe1, wpe2 = @wpe2, wbe1 = @wbe1, wbe2 = @wbe2, bsisakg = @bsisakg, wastekg = @wastekg, e1mm = @e1mm, e2mm = @e2mm," +
                        "ttle1e2 = @ttle1e2, waste = @waste, keterangan = @keterangan WHERE id_stok = @id", conn);

                    cmd.Parameters.AddWithValue("@bmasuk", txtmasuk.Text);
                    cmd.Parameters.AddWithValue("@bkeluar", txtkeluar.Text);
                    cmd.Parameters.AddWithValue("@bstok", lblstoksekarang.Text);
                    cmd.Parameters.AddWithValue("@bpanjang", txtpbar.Text);
                    cmd.Parameters.AddWithValue("@bsisamm", txtsbarmm.Text);
                    cmd.Parameters.AddWithValue("@bpe1", sawinge1.Text);
                    cmd.Parameters.AddWithValue("@bpe2", sawinge2.Text);
                    cmd.Parameters.AddWithValue("@bbe1", lathee1.Text);
                    cmd.Parameters.AddWithValue("@bbe2", lathee2.Text);
                    cmd.Parameters.AddWithValue("@rbkeluare1", pkeluare1.Text);
                    cmd.Parameters.AddWithValue("@rbkeluare2", pkeluare2.Text);
                    cmd.Parameters.AddWithValue("@wpe1", ttlstoksawinge1.Text);
                    cmd.Parameters.AddWithValue("@wpe2", ttlstoksawinge2.Text);
                    cmd.Parameters.AddWithValue("@wbe1", ttlstoklathee1.Text);
                    cmd.Parameters.AddWithValue("@wbe2", ttlstoklathee2.Text);
                    cmd.Parameters.AddWithValue("@bsisakg", txtsbarkg.Text);

                    wastekg = double.Parse(lblwastekg.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    cmd.Parameters.AddWithValue("@wastekg", wastekg);

                    cmd.Parameters.AddWithValue("@e1mm", lble1mm.Text);
                    cmd.Parameters.AddWithValue("@e2mm", lble2mm.Text);
                    cmd.Parameters.AddWithValue("@ttle1e2", lblttle1e2.Text);
                    cmd.Parameters.AddWithValue("@waste", lblwaste.Text);
                    cmd.Parameters.AddWithValue("@keterangan", txtketerangan.Text);
                    cmd.Parameters.AddWithValue("@id", idmulai);

                    cmd.ExecuteNonQuery();

                    update();

                    MessageBox.Show("Data Berhasil Diupdate", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
                    tampil();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                getdatastok();
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
            bstok = 0;
            wpe1 = 0;
            wpe2 = 0;
            wbe1 = 0;
            wbe2 = 0;
            wastekg = 0;
            ttle1e2mm = 0;
            idmulai = 0;
        }


        //Kode Load Form
        private void weldingp_Load(object sender, EventArgs e)
        {
            btnsimpan.Enabled = false;
            getdatastok();
            tampil();
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
        }


        //Kode Kode Button
        private void btnhitung_Click(object sender, EventArgs e)
        {
            if (btnhitung.Text == "Hitung")
            {
                getdata();
                hitungrb();
                hitungrbs();
                hitungrbl();
                hitungwaste();

                btnsimpan.Enabled = true;
                btnbatal.Enabled = true;
            }
            else
            {
                getdataedit();
                hitungrb();
                hitungrbs();
                hitungrbl();
                hitungwaste();
                btnsimpan.Enabled = true;
                btnbatal.Enabled = true;
            }
        }
        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if (btnsimpan.Text == "Simpan Data")
            {
                DateTime tanggalinput = DateTime.Now.Date;
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT tanggal, shift, id_stok FROM Rb_Stok WHERE CONVERT(date, tanggal) = @tglinput AND shift = @shift", conn);
                    cmd.Parameters.AddWithValue("@tglinput", tanggalinput);
                    cmd.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        DialogResult result = MessageBox.Show("Data untuk shift dan tanggal ini sudah pernah dimasukkan, Apakah anda ingin edit datanya?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            setdefault();
                            tampildataedit();
                            btnsimpan.Text = "Edit Data";
                            btnsimpan.Enabled = false;
                            btnhitung.Text = "Hitung Ulang";
                            btnbatal.Enabled = true;
                            idmulai = Convert.ToInt32(dr["id_stok"]);
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        simpandata();
                        btnsimpan.Enabled = false;
                        btnbatal.Enabled = false;
                    }
                }
            }
            else if (btnsimpan.Text == "Edit Data")
            {
                editdata();
                setdefault();
                btnsimpan.Text = "Simpan Data";
                btnsimpan.Enabled = false;
                btnhitung.Text = "Hitung";
                btnbatal.Enabled = false;
            }
        }
        private void btnbatal_Click(object sender, EventArgs e)
        {
            setdefault();
            getdatastok();
            btnbatal.Enabled = false;
            btnsimpan.Enabled = false;
            btnhitung.Text = "Hitung";
            btnsimpan.Text = "Simpan Data";
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

                txtcari.Text = "";
                datecari.Checked = false;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                idmulai = Convert.ToInt32(row.Cells["id_stok"].Value);
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

                getdatastokedit();
                btnhitung.Text = "Hitung Ulang";
                btnsimpan.Text = "Edit Data";

                btnbatal.Enabled = true;
            }
        }
    }
}
