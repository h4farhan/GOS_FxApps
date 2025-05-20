using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

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
        private int wastekg;
        private int ttle1e2mm;

        public weldingp()
        {
            InitializeComponent();
        }

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
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void cari()
        {
            string keyword = txtcari.Text;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Rb_Stok WHERE tanggal LIKE @keyword OR shift LIKE @keyword", conn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat pencarian: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void getdatastok()
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
                MessageBox.Show("data null");
            }
            reader.Close();
            conn.Close();
        }

        private void getdata()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 bstok,wpe1,wpe2,wbe1,wbe2,wastekg from Rb_Stok order by id_stok DESC;", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) { 
                bstok = Convert.ToInt32(reader["bstok"]);
                wpe1 = Convert.ToInt32(reader["wpe1"]);
                wpe2 = Convert.ToInt32(reader["wpe2"]);
                wbe1 = Convert.ToInt32(reader["wbe1"]);
                wbe2 = Convert.ToInt32(reader["wbe2"]);
                wastekg = Convert.ToInt32(reader["wastekg"]);
                lblstcokakhir.Text = reader["bstok"].ToString();
            } else
            {
                MessageBox.Show("data null");
            }
            reader.Close();
            conn.Close();
        }

        private int SafeParse(string input)
        {
            int hasil;
            if (int.TryParse(input, out hasil))
                return hasil;
            else
                return 0;
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

            int rble2 = SafeParse (lathee2.Text);
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
    
            int ttlwastekg = wastekg + sisarbkg;
            int ttlwaste = panjangrbmm - ttle1e2mm;

            lblwastekg.Text = ttlwastekg.ToString();
            lblwaste.Text = ttlwaste.ToString();
        }

        private void weldingp_Load(object sender, EventArgs e)
        {
            btnsimpan.Enabled = false;
            getdatastok();
            tampil();
        }

        private void btnhitung_Click(object sender, EventArgs e)
        {
            getdata();
            hitungrb();
            hitungrbs();
            hitungrbl();
            hitungwaste();
            btnsimpan.Enabled=true;
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

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            DateTime tanggalinput = DateTime.Now.Date;
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT tanggal, shift FROM Rb_Stok WHERE CONVERT(date, tanggal) = @tglinput AND shift = @shift", conn);
            cmd.Parameters.AddWithValue("@tglinput", tanggalinput);
            cmd.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Data untuk shift dan tanggal ini sudah pernah dimasukkan.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();
                btnsimpan.Enabled = false;
                conn.Close();
            }
            else
            {
                conn.Close(); 
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
                        cmd1.Parameters.AddWithValue("@wastekg", lblwastekg.Text);
                        cmd1.Parameters.AddWithValue("@e1mm", lble1mm.Text);
                        cmd1.Parameters.AddWithValue("@e2mm", lble2mm.Text);
                        cmd1.Parameters.AddWithValue("@ttle1e2", lblttle1e2.Text);
                        cmd1.Parameters.AddWithValue("@waste", lblwaste.Text);
                        cmd1.Parameters.AddWithValue("@keterangan", txtketerangan.Text);

                        cmd1.ExecuteNonQuery();

                        MessageBox.Show("Data Berhasil Disimpan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        setdefault();
                        tampil();
                        btnsimpan.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Dimasukkan: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                    getdatastok();
                }
            }

        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            cari();
        }
    }
}
