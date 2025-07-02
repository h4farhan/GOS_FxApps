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
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class editpengiriman : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        bool infocari = false;
        string tanggalpenerimaan;
        string tanggalperbaikan;
        string tanggalpengiriman;
        int shift;
        int noprimary;

        public editpengiriman()
        {
            InitializeComponent();
        }
        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM pengiriman";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Pengiriman";
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
                dataGridView1.Columns[13].HeaderText = "S";
                dataGridView1.Columns[14].HeaderText = "D";
                dataGridView1.Columns[15].HeaderText = "B";
                dataGridView1.Columns[16].HeaderText = "BA";
                dataGridView1.Columns[17].HeaderText = "CR";
                dataGridView1.Columns[18].HeaderText = "M";
                dataGridView1.Columns[19].HeaderText = "R";
                dataGridView1.Columns[20].HeaderText = "C";
                dataGridView1.Columns[21].HeaderText = "RL";
                dataGridView1.Columns[22].HeaderText = "Jumlah";
                dataGridView1.Columns[23].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[23].HeaderText = "Tanggal Perbaikan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private bool cari()
        {
            DateTime? tanggal = dateeditpengiriman.Checked ? (DateTime?)dateeditpengiriman.Value.Date : null;
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM pengiriman WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += "AND CAST(tanggal_pengiriman AS DATE) = @tgl";
                    cmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (!string.IsNullOrEmpty(inputRod))
                {
                    query += " AND nomor_rod = @rod";
                    cmd.Parameters.AddWithValue("@rod", Convert.ToInt32(inputRod));
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
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat pencarian: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return dt.Rows.Count > 0;
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
            txts.Clear();
            txtd.Clear();
            txtb.Clear();
            txtba.Clear();
            txtcr.Clear();
            txtm.Clear();
            txtr.Clear();
            txtc.Clear();
            txtrl.Clear();
            lbltotalsebelum.Text = "-";
            lbltotalupdate.Text = "-";
            lbltotale1.Text = "-";
            lbltotale2.Text = "-";
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
            txts.Enabled = true;
            txtd.Enabled = true;
            txtb.Enabled = true;
            txtba.Enabled = true;
            txtcr.Enabled = true;
            txtm.Enabled = true;
            txtr.Enabled = true;
            txtc.Enabled = true;
            txtrl.Enabled = true;
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
            txts.Enabled = false;
            txtd.Enabled = false;
            txtb.Enabled = false;
            txtba.Enabled = false;
            txtcr.Enabled = false;
            txtm.Enabled = false;
            txtr.Enabled = false;
            txtc.Enabled = false;
            txtrl.Enabled = false;
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
                    infocari = false;
                    btncari.Text = "Cari";
                }
            }
            else
            {
                tampil();
                infocari = false;
                btncari.Text = "Cari";

                txtcari.Text = "";
                dateeditpengiriman.Checked = false;
            }
        }

        private void editpengiriman_Load(object sender, EventArgs e)
        {
            tampil();
            dateeditpengiriman.Checked = false;
            dateeditpengiriman.Value = DateTime.Now.Date;
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            setdefault();
            btnclear.Enabled = false;
            btnupdate.Enabled = false;
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

        private void btnhitung_Click(object sender, EventArgs e)
        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "")
            {
                MessageBox.Show("Data Tidak Boleh Kosong");
            }
            else
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
                int angka7 = SafeParse(txts);
                int angka8 = SafeParse(txtd);
                int angka9 = SafeParse(txtb);
                int angka10 = SafeParse(txtba);
                int angka11 = SafeParse(txtcr);
                int angka12 = SafeParse(txtm);
                int angka13 = SafeParse(txtr);
                int angka14 = SafeParse(txtc);
                int angka15 = SafeParse(txtrl);

                int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12 + angka13 + angka14 + angka15;
                lbltotalupdate.Text = hasil.ToString();
                btnupdate.Enabled = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["no"].Value);
                tanggalpengiriman = row.Cells["tanggal_pengiriman"].ToString();
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
                tanggalpenerimaan = row.Cells["tanggal_penerimaan"].Value.ToString();
                tanggalperbaikan = row.Cells["tanggal_perbaikan"].Value.ToString();

                settrue();

                btnclear.Enabled = true;
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE pengiriman SET jenis = @jenis, e1_ers = @e1ers, e1_est = @e1est, e1_jumlah = @e1jumlah, e2_ers = @e2ers, e2_cst = @e2cst, e2_cstub = @e2cstub, e2_jumlah = @e2jumlah," +
                        "e3 = @e3, s = @s, d = @d, b = @b, ba = @ba, cr = @cr, m = @m, r = @r, c = @c, rl = @rl, jumlah = @jumlah WHERE no = @no ", conn);

                    //SqlCommand cmd2 = new SqlCommand("INSERT INTO histori_perbaikan (shift,nomor_rod,jenis,e1_ers,e1_est,e1_jumlah,e2_ers,e2_cst,e2_cstub,e2_jumlah," +
                    //    "e3,s,d,b,ba,cr,m,r,c,rl,jumlah,tanggal_perbaikan,tanggal_penerimaan) VALUES(@shift,@nomorrod,@jenis,@e1ers,@e1est,@e1jumlah,@e2ers,@e2cst,@e2cstub,@e2jumlah,@e3,@s,@d,@b,@ba,@cr,@m,@r,@c,@rl,@jumlah,@tanggalpenerimaan,@tanggalperbaikan)", conn);

                    cmd.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    cmd.Parameters.AddWithValue("@e1ers", txte1ers.Text);
                    cmd.Parameters.AddWithValue("@e1est", txte1est.Text);
                    cmd.Parameters.AddWithValue("@e1jumlah", lbltotale1.Text);
                    cmd.Parameters.AddWithValue("@e2ers", txte2ers.Text);
                    cmd.Parameters.AddWithValue("@e2cst", txte2cst.Text);
                    cmd.Parameters.AddWithValue("@e2cstub", txte2cstub.Text);
                    cmd.Parameters.AddWithValue("@e2jumlah", lbltotale2.Text);
                    cmd.Parameters.AddWithValue("@e3", txte3.Text);
                    cmd.Parameters.AddWithValue("@s", txts.Text);
                    cmd.Parameters.AddWithValue("@d", txtd.Text);
                    cmd.Parameters.AddWithValue("@b", txtb.Text);
                    cmd.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd.Parameters.AddWithValue("@m", txtm.Text);
                    cmd.Parameters.AddWithValue("@r", txtr.Text);
                    cmd.Parameters.AddWithValue("@c", txtc.Text);
                    cmd.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                    cmd.Parameters.AddWithValue("@no", noprimary);

                    //cmd2.Parameters.AddWithValue("@shift", shift);
                    //cmd2.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                    //cmd2.Parameters.AddWithValue("@jenis", txtjenis.Text);
                    //cmd2.Parameters.AddWithValue("@e1ers", txte1ers.Text);
                    //cmd2.Parameters.AddWithValue("@e1est", txte1est.Text);
                    //cmd2.Parameters.AddWithValue("@e1jumlah", lbltotale1.Text);
                    //cmd2.Parameters.AddWithValue("@e2ers", txte2ers.Text);
                    //cmd2.Parameters.AddWithValue("@e2cst", txte2cst.Text);
                    //cmd2.Parameters.AddWithValue("@e2cstub", txte2cstub.Text);
                    //cmd2.Parameters.AddWithValue("@e2jumlah", lbltotale2.Text);
                    //cmd2.Parameters.AddWithValue("@e3", txte3.Text);
                    //cmd2.Parameters.AddWithValue("@s", txts.Text);
                    //cmd2.Parameters.AddWithValue("@d", txtd.Text);
                    //cmd2.Parameters.AddWithValue("@b", txtb.Text);
                    //cmd2.Parameters.AddWithValue("@ba", txtba.Text);
                    //cmd2.Parameters.AddWithValue("@cr", txtcr.Text);
                    //cmd2.Parameters.AddWithValue("@m", txtm.Text);
                    //cmd2.Parameters.AddWithValue("@r", txtr.Text);
                    //cmd2.Parameters.AddWithValue("@c", txtc.Text);
                    //cmd2.Parameters.AddWithValue("@rl", txtrl.Text);
                    //cmd2.Parameters.AddWithValue("@jumlah", lbltotalupdate.Text);
                    //cmd2.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);
                    //cmd2.Parameters.AddWithValue("@tanggalperbaikan", tanggalperbaikan);

                    cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diupdate", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
                    tampil();
                    btnupdate.Enabled = false;
                    btnclear.Enabled = false;
                    setfalse();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi Kesalahan Update Data " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
