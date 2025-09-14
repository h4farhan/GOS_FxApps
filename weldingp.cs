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

        private int masukedit;
        private int keluaredit;
        private int bpanjangedit;
        private int sawing1edit;
        private int sawing2edit;
        private int lathe1edit;
        private int lathe2edit;
        private int pkeluare1edit;
        private int pkeluare2edit;
        private int bsisakgedit;
        

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
                string query = "SELECT * FROM Rb_Stok ORDER BY tanggal DESC, shift DESC";
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
        private void getdatastok()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 bstok FROM Rb_Stok ORDER BY tanggal DESC, shift DESC", conn);
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
        private bool getdatastoksimpan()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 tanggal, bstok from Rb_Stok WHERE (tanggal < @tanggal) OR (tanggal = @tanggal AND shift < @shift) ORDER BY tanggal DESC, shift DESC", conn);
                cmd.Parameters.AddWithValue("@tanggal", date.Value);
                cmd.Parameters.AddWithValue("@shift", shift.SelectedItem);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblstcokakhir.Text = reader["bstok"].ToString();
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Data Pertama Tidak Bisa Diedit!!", "Warning");
                    return false;
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        private bool getdatastokedit()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 1 tanggal, bstok 
            FROM Rb_Stok 
            WHERE id_stok < @id 
            ORDER BY id_stok DESC", conn);
                cmd.Parameters.AddWithValue("@id", idmulai);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblstcokakhir.Text = reader["bstok"].ToString();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Data Pertama Tidak Bisa Diedit!!", "Warning");
                        return false;
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message,
                                "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 bstok,wpe1,wpe2,wbe1,wbe2,wastekg from Rb_Stok ORDER BY tanggal DESC, shift DESC;", conn);
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
        private void getdatasimpan()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 bstok,wpe1,wpe2,wbe1,wbe2,wastekg from Rb_Stok WHERE (tanggal < @tanggal) OR (tanggal = @tanggal AND shift < @shift) ORDER BY tanggal DESC, shift DESC", conn);
                cmd.Parameters.AddWithValue("@tanggal", date.Value);
                cmd.Parameters.AddWithValue("@shift", shift.SelectedItem);
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
                    MessageBox.Show("Erorr Data Simpan", "Warning");
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
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 bstok,wpe1,wpe2,wbe1,wbe2,wastekg from Rb_Stok WHERE (tanggal < @tanggal) OR (tanggal = @tanggal AND shift < @shift) ORDER BY tanggal DESC, shift DESC", conn);
                cmd.Parameters.AddWithValue("@tanggal", date.Value);
                cmd.Parameters.AddWithValue("@shift", shift.SelectedItem);
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
                    MessageBox.Show("Data Pertama Tidak Bisa Diedit!!", "Warning");
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

        private void hitungrbedit()
        {
            int masuk = masukedit + SafeParse(txtmasuk.Text);
            int keluar = keluaredit + SafeParse(txtkeluar.Text);

            int totalrb = masuk + bstok - keluar;
            lblstoksekarang.Text = totalrb.ToString();
        }
        private void hitungrbsedit()
        {
            int rbse1 = sawing1edit + SafeParse(sawinge1.Text);
            int rble1 = lathe1edit + SafeParse(lathee1.Text);

            int rbse2 = sawing2edit + SafeParse(sawinge2.Text);
            int rble2 = lathe2edit + SafeParse(lathee2.Text);

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
        private void hitungrbledit()
        {
            int rble1 = lathe1edit + SafeParse(lathee1.Text);
            int rbke1 = pkeluare1edit + SafeParse(pkeluare1.Text);

            int rble2 = lathe2edit + SafeParse(lathee2.Text);
            int rbke2 = pkeluare2edit + SafeParse(pkeluare2.Text);

            int ttllathee1 = wbe1 + rble1 - rbke1;
            int ttllathee2 = wbe2 + rble2 - rbke2;

            ttlstoklathee1.Text = ttllathee1.ToString();
            ttlstoklathee2.Text = ttllathee2.ToString();
        }
        private void hitungwasteedit()
        {
            int sisarbkg = bsisakgedit + SafeParse(txtsbarkg.Text);

            int panjangrbmm = bpanjangedit + SafeParse(txtpbar.Text);

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

                    string query = @"
                SELECT *
                FROM Rb_Stok
                WHERE (tanggal > @tanggalMulai)
                   OR (tanggal = @tanggalMulai AND shift > @shiftMulai)
                ORDER BY tanggal ASC, shift ASC, id_stok ASC";

                    SqlCommand cmd1 = new SqlCommand(query, conn);
                    cmd1.Parameters.AddWithValue("@tanggalMulai", date.Value);
                    cmd1.Parameters.AddWithValue("@shiftMulai", shift.SelectedItem);    

                    SqlDataReader reader = cmd1.ExecuteReader();

                    List<datarows> rows = new List<datarows>();
                    while (reader.Read())
                    {
                        rows.Add(new datarows
                        {
                            id = Convert.ToInt32(reader["id_stok"]),
                            dsmasuk = Convert.ToInt32(reader["bmasuk"]),
                            dskeluar = Convert.ToDouble(reader["bkeluar"]),
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

                    string queryDataEdit = @"
                SELECT TOP 1 *
                FROM Rb_Stok
                WHERE (tanggal < @tanggalMulai)
                   OR (tanggal = @tanggalMulai AND shift <= @shiftMulai)
                ORDER BY tanggal DESC, shift DESC, id_stok DESC";

                    SqlCommand cmddataedit = new SqlCommand(queryDataEdit, conn);
                    cmddataedit.Parameters.AddWithValue("@tanggalMulai", date.Value);
                    cmddataedit.Parameters.AddWithValue("@shiftMulai", shift.SelectedItem);

                    SqlDataReader dr = cmddataedit.ExecuteReader();

                    double dstok = 0,
                           dstokpotonge1 = 0,
                           dstokpotonge2 = 0,
                           dstokbubute1 = 0,
                           dstokbubute2 = 0,
                           dwastekg = 0;

                    if (dr.Read())
                    {
                        dstok = Convert.ToDouble(dr["bstok"]);
                        dstokpotonge1 = Convert.ToDouble(dr["wpe1"]);
                        dstokpotonge2 = Convert.ToDouble(dr["wpe2"]);
                        dstokbubute1 = Convert.ToDouble(dr["wbe1"]);
                        dstokbubute2 = Convert.ToDouble(dr["wbe2"]);
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
                        wastekg = @wastekg,
                        updated_at = GETDATE()
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
        private void update2()
        {
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT * 
                FROM Rb_Stok 
                WHERE (tanggal > @tanggal) 
                   OR (tanggal = @tanggal AND shift > @shift)
                ORDER BY tanggal, shift, id_stok ASC";

                    SqlCommand cmd1 = new SqlCommand(query, conn);
                    cmd1.Parameters.AddWithValue("@tanggal", date.Value.Date);
                    cmd1.Parameters.AddWithValue("@shift", Convert.ToInt32(shift.SelectedItem));

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

                    string qEdit = @"SELECT TOP 1 * 
                             FROM Rb_Stok 
                             WHERE tanggal = @tanggal AND shift = @shift 
                             ORDER BY id_stok ASC";
                    SqlCommand cmddataedit = new SqlCommand(qEdit, conn);
                    cmddataedit.Parameters.AddWithValue("@tanggal", date.Value.Date);
                    cmddataedit.Parameters.AddWithValue("@shift", Convert.ToInt32(shift.SelectedItem));
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
                        wastekg = @wastekg,
                        updated_at = GETDATE()
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
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!tanggal.HasValue && !shiftValid)
            {
                MessageBox.Show("Silakan isi tanggal atau shift untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DataTable dt = new DataTable();
            string query = "SELECT * FROM Rb_Stok WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += " AND CAST(tanggal AS DATE) = @tgl";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (shiftValid)
                {
                    query += " AND shift = @shift";
                    cmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem.ToString());
                }

                query += " ORDER BY tanggal DESC";

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
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO Rb_Stok (tanggal,shift,bmasuk,bkeluar,bstok,bpanjang,bsisamm,bpe1,bpe2,bbe1,bbe2,rbkeluare1,rbkeluare2," +
                        "wpe1,wpe2,wbe1,wbe2,bsisakg,wastekg,e1mm,e2mm,ttle1e2,waste,keterangan,updated_at,remaks) VALUES(@tanggal,@shift,@bmasuk,@bkeluar,@bstok,@bpanjang,@bsisamm,@bpe1,@bpe2," +
                        "@bbe1,@bbe2,@rbkeluare1,@rbkeluare2,@wpe1,@wpe2,@wbe1,@wbe2,@bsisakg,@wastekg,@e1mm,@e2mm,@ttle1e2,@waste,@keterangan,@diubah,@remaks)", conn);

                    cmd1.Parameters.AddWithValue("@tanggal", date.Value);
                    cmd1.Parameters.AddWithValue("@shift", shift.SelectedItem);
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
                    cmd1.Parameters.AddWithValue("@remaks", txtsbarkg.Text);

                    wastekg = double.Parse(lblwastekg.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    cmd1.Parameters.AddWithValue("@wastekg", wastekg);

                    cmd1.Parameters.AddWithValue("@e1mm", lble1mm.Text);
                    cmd1.Parameters.AddWithValue("@e2mm", lble2mm.Text);
                    cmd1.Parameters.AddWithValue("@ttle1e2", lblttle1e2.Text);
                    cmd1.Parameters.AddWithValue("@waste", lblwaste.Text);
                    cmd1.Parameters.AddWithValue("@keterangan", txtketerangan.Text);
                    cmd1.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);

                    cmd1.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void simpandata2()
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO Rb_Stok (tanggal,shift,bmasuk,bkeluar,bstok,bpanjang,bsisamm,bpe1,bpe2,bbe1,bbe2,rbkeluare1,rbkeluare2," +
                        "wpe1,wpe2,wbe1,wbe2,bsisakg,wastekg,e1mm,e2mm,ttle1e2,waste,keterangan,updated_at,remaks) VALUES(@tanggal,@shift,@bmasuk,@bkeluar,@bstok,@bpanjang,@bsisamm,@bpe1,@bpe2," +
                        "@bbe1,@bbe2,@rbkeluare1,@rbkeluare2,@wpe1,@wpe2,@wbe1,@wbe2,@bsisakg,@wastekg,@e1mm,@e2mm,@ttle1e2,@waste,@keterangan,@diubah,@remaks)", conn);

                    cmd1.Parameters.AddWithValue("@tanggal", date.Value);
                    cmd1.Parameters.AddWithValue("@shift", shift.SelectedItem);
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
                    cmd1.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd1.Parameters.AddWithValue("@remaks", txtsbarkg.Text);

                    cmd1.ExecuteNonQuery();

                    update2();

                    MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Rb_Stok SET bmasuk = bmasuk + @bmasuk, bkeluar = bkeluar + @bkeluar, bstok = @bstok, bpanjang = bpanjang + @bpanjang, bsisamm = bsisamm + @bsisamm, bpe1 = bpe1 + @bpe1, bpe2 = bpe2 +@bpe2, bbe1 = bbe1 + @bbe1," +
                        "bbe2 = bbe2 + @bbe2, rbkeluare1 = rbkeluare1 + @rbkeluare1, rbkeluare2 = rbkeluare2 + @rbkeluare2, wpe1 = @wpe1, wpe2 = @wpe2, wbe1 = @wbe1, wbe2 = @wbe2, bsisakg = bsisakg + @bsisakg, wastekg = @wastekg, e1mm = @e1mm, e2mm = @e2mm," +
                        "ttle1e2 = @ttle1e2, waste = @waste, keterangan = @keterangan, updated_at = @diubah, remaks = @remaks WHERE id_stok = @id", conn);

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
                    cmd.Parameters.AddWithValue("@diubah", MainForm.Instance.tanggal);
                    cmd.Parameters.AddWithValue("@remaks", txtsbarkg.Text);

                    cmd.ExecuteNonQuery();

                    update();

                    MessageBox.Show("Data Berhasil Diedit", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            masukedit = 0;
            keluaredit = 0;
            bpanjangedit = 0;
            sawing1edit = 0;
            sawing2edit = 0;
            lathe1edit = 0;
            lathe2edit = 0;
            pkeluare1edit = 0;
            pkeluare2edit = 0;
            bsisakgedit = 0;
        }

        //Kode Load Form
        private void weldingp_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            btnsimpan.Enabled = false;
            getdatastok();
            tampil();
            date.Value = DateTime.Now.Date;
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;
            txtmasuk.Focus();
            registerperbaikan();
        }

        //Kode Kode Button
        private void btnhitung_Click(object sender, EventArgs e)
        {
            if (btnhitung.Text == "Hitung")
            {
                if (shift.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih shift terlebih dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime tanggalinput = date.Value;
                int shiftinput = Convert.ToInt32(shift.SelectedItem);

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open(); 

                    using (SqlCommand cmdcheck = new SqlCommand("SELECT COUNT(1) FROM Rb_Stok WHERE tanggal = @tgl AND shift = @shift", conn))
                    {
                        cmdcheck.Parameters.AddWithValue("@tgl", tanggalinput);
                        cmdcheck.Parameters.AddWithValue("@shift", shiftinput);

                        int ada = Convert.ToInt32(cmdcheck.ExecuteScalar());

                        if (ada > 0)
                        {
                                using (SqlCommand cmdget = new SqlCommand(@"
                            SELECT id_stok,bmasuk,bkeluar,bpanjang,bpe1,bpe2,bbe1,bbe2,
                                   rbkeluare1,rbkeluare2,bsisakg 
                            FROM Rb_Stok 
                            WHERE tanggal = @tgl AND shift = @shift", conn))
                                {
                                    cmdget.Parameters.AddWithValue("@tgl", tanggalinput);
                                    cmdget.Parameters.AddWithValue("@shift", shiftinput);

                                    using (SqlDataReader dr = cmdget.ExecuteReader())
                                    {
                                        if (dr.Read())
                                        {
                                            idmulai = Convert.ToInt32(dr["id_stok"]);
                                            masukedit = dr["bmasuk"] == DBNull.Value ? 0 : Convert.ToInt32(dr["bmasuk"]);
                                            keluaredit = dr["bkeluar"] == DBNull.Value ? 0 : Convert.ToInt32(dr["bkeluar"]);
                                            bpanjangedit = dr["bpanjang"] == DBNull.Value ? 0 : Convert.ToInt32(dr["bpanjang"]);
                                            sawing1edit = dr["bpe1"] == DBNull.Value ? 0 : Convert.ToInt32(dr["bpe1"]);
                                            sawing2edit = dr["bpe2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["bpe2"]);
                                            lathe1edit = dr["bbe1"] == DBNull.Value ? 0 : Convert.ToInt32(dr["bbe1"]);
                                            lathe2edit = dr["bbe2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["bbe2"]);
                                            pkeluare1edit = dr["rbkeluare1"] == DBNull.Value ? 0 : Convert.ToInt32(dr["rbkeluare1"]);
                                            pkeluare2edit = dr["rbkeluare2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["rbkeluare2"]);
                                            bsisakgedit = dr["bsisakg"] == DBNull.Value ? 0 : Convert.ToInt32(dr["bsisakg"]);

                                            getdataedit();
                                            hitungrbedit();
                                            hitungrbsedit();
                                            hitungrbledit();
                                            hitungwasteedit();

                                            btnsimpan.Enabled = true;
                                            btnbatal.Enabled = true;
                                        }
                                    }
                                }
                        }
                        else
                        {

                                using (SqlCommand cmd = new SqlCommand(@"
                            SELECT 
                                SUM(CASE WHEN (tanggal < @tglinput OR (tanggal = @tglinput AND shift < @shift)) THEN 1 ELSE 0 END) AS sebelum,
                                SUM(CASE WHEN (tanggal > @tglinput OR (tanggal = @tglinput AND shift > @shift)) THEN 1 ELSE 0 END) AS sesudah
                            FROM Rb_Stok", conn))
                                {
                                    cmd.Parameters.AddWithValue("@tglinput", tanggalinput);
                                    cmd.Parameters.AddWithValue("@shift", shiftinput);

                                    using (SqlDataReader dr = cmd.ExecuteReader())
                                    {
                                        if (dr.Read())
                                        {
                                            int sebelum = dr["sebelum"] == DBNull.Value ? 0 : Convert.ToInt32(dr["sebelum"]);
                                            int sesudah = dr["sesudah"] == DBNull.Value ? 0 : Convert.ToInt32(dr["sesudah"]);

                                            if (sebelum > 0 && sesudah > 0)
                                            {
                                                MessageBox.Show("Data ini akan berada di pertengahan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                getdatastoksimpan();
                                                getdatasimpan();
                                                hitungrb();
                                                hitungrbs();
                                                hitungrbl();
                                                hitungwaste();
                                            }
                                            else
                                            {
                                                getdata();
                                                hitungrb();
                                                hitungrbs();
                                                hitungrbl();
                                                hitungwaste();
                                            }

                                            btnsimpan.Enabled = true;
                                            btnbatal.Enabled = true;
                                        }
                                    }
                                }
                        }
                    }
                }
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
                if (shift.SelectedIndex == -1)
                {
                    MessageBox.Show("Pilih shift terlebih dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DateTime tanggalinput = date.Value;
                int shiftinput = Convert.ToInt32(shift.SelectedItem);

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT 1 FROM Rb_Stok WHERE tanggal = @tglinput AND shift = @shift", conn);
                    cmd.Parameters.AddWithValue("@tglinput", tanggalinput);
                    cmd.Parameters.AddWithValue("@shift", shiftinput);

                    object sudahAda = cmd.ExecuteScalar();
                    if (sudahAda != null)
                    {
                        editdata();
                        return;
                    }

                    SqlCommand cmd1 = new SqlCommand(@"
                                                    SELECT 
                                                        SUM(CASE WHEN (tanggal < @tglinput OR (tanggal = @tglinput AND shift < @shift)) THEN 1 ELSE 0 END) AS sebelum,
                                                        SUM(CASE WHEN (tanggal > @tglinput OR (tanggal = @tglinput AND shift > @shift)) THEN 1 ELSE 0 END) AS sesudah
                                                    FROM Rb_Stok", conn);

                    cmd1.Parameters.AddWithValue("@tglinput", tanggalinput);
                    cmd1.Parameters.AddWithValue("@shift", shiftinput);

                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                    {
                        if (dr1.Read())
                        {
                            int sebelum = dr1.GetInt32(0);
                            int sesudah = dr1.GetInt32(1);

                            if (sebelum > 0 && sesudah > 0)
                            {
                                MessageBox.Show("Data ini akan berada di pertengahan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                simpandata2();
                            }
                            else
                            {
                                simpandata();
                            }
                        }
                    }

                    btnsimpan.Enabled = false;
                    btnbatal.Enabled = false;
                    date.Enabled = true;
                    shift.Enabled = true;
                    shift.SelectedIndex = -1;
                    txtmasuk.Focus();
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
                date.Enabled = true;
                shift.Enabled = true;
                shift.SelectedItem = -1;
            }
        }
        private void btnbatal_Click(object sender, EventArgs e)
        {
            setdefault();
            getdatastok();
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

                if (!getdatastokedit()) return;

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

                btnhitung.Text = "Hitung Ulang";
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
