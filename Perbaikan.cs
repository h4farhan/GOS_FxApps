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
    public partial class Perbaikan : Form
    {
        private DateTime tanggalpenerimaan;
        SqlConnection conn = Koneksi.GetConnection();
        private bool infocheck = false;
        public Perbaikan()
        {
            InitializeComponent();
            setdefault();
            tampil();
        }

        private void Perbaikan_Load(object sender, EventArgs e)
        {
            btnsimpan.Enabled = false;
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM perbaikan_s";
                SqlDataAdapter ad = new SqlDataAdapter(query,conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
               
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void setdefault()
        {
            txtnomorrod.Clear();
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
            lbltotal.Text = "-";
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
            btnhitung.Enabled = true;
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
            btnhitung.Enabled = false;
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
                lbltotal.Text = hasil.ToString();
                btnsimpan.Enabled = true;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin dengan data Anda?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO perbaikan_s (tanggal_perbaikan,shift,nomor_rod,jenis,e1_ers,e1_est,e1_jumlah,e2_ers,e2_cst,e2_cstub,e2_jumlah," +
                        "e3,s,d,b,ba,cr,m,r,c,rl,jumlah,tanggal_penerimaan) VALUES(getdate(),@shift,@nomorrod,@jenis,@e1ers,@e1est,@e1jumlah,@e2ers,@e2cst,@e2cstub,@e2jumlah,@e3,@s,@d,@b,@ba,@cr,@m,@r,@c,@rl,@jumlah,@tanggalpenerimaan)", conn);

                    SqlCommand cmd2 = new SqlCommand("INSERT INTO perbaikan_p (tanggal_perbaikan,shift,nomor_rod,jenis,e1_ers,e1_est,e1_jumlah,e2_ers,e2_cst,e2_cstub,e2_jumlah," +
                        "e3,s,d,b,ba,cr,m,r,c,rl,jumlah,tanggal_penerimaan) VALUES(getdate(),@shift,@nomorrod,@jenis,@e1ers,@e1est,@e1jumlah,@e2ers,@e2cst,@e2cstub,@e2jumlah,@e3,@s,@d,@b,@ba,@cr,@m,@r,@c,@rl,@jumlah,@tanggalpenerimaan)", conn);

                    cmd1.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
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
                    cmd1.Parameters.AddWithValue("@s", txts.Text);
                    cmd1.Parameters.AddWithValue("@d", txtd.Text);
                    cmd1.Parameters.AddWithValue("@b", txtb.Text);
                    cmd1.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd1.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd1.Parameters.AddWithValue("@m", txtm.Text);
                    cmd1.Parameters.AddWithValue("@r", txtr.Text);
                    cmd1.Parameters.AddWithValue("@c", txtc.Text);
                    cmd1.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd1.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd1.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);

                    cmd2.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
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
                    cmd2.Parameters.AddWithValue("@s", txts.Text);
                    cmd2.Parameters.AddWithValue("@d", txtd.Text);
                    cmd2.Parameters.AddWithValue("@b", txtb.Text);
                    cmd2.Parameters.AddWithValue("@ba", txtba.Text);
                    cmd2.Parameters.AddWithValue("@cr", txtcr.Text);
                    cmd2.Parameters.AddWithValue("@m", txtm.Text);
                    cmd2.Parameters.AddWithValue("@r", txtr.Text);
                    cmd2.Parameters.AddWithValue("@c", txtc.Text);
                    cmd2.Parameters.AddWithValue("@rl", txtrl.Text);
                    cmd2.Parameters.AddWithValue("@jumlah", lbltotal.Text);
                    cmd2.Parameters.AddWithValue("@tanggalpenerimaan", tanggalpenerimaan);

                    SqlCommand cmd3 = new SqlCommand("DELETE FROM penerimaan_s WHERE nomor_rod = @nomorrod", conn);
                    cmd3.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                
                    cmd3.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Data Berhasil Disimpan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
                    setfalse();
                    tampil();
                    btnsimpan.Enabled = false;
                }
                else 
                {

                }      

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Gagal Dimasukkan" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            if (infocheck == true)
            {
                setdefault();
                setfalse();
                txtnomorrod.Enabled = true;
                btncheck.Text = "Check Data";
                btncheck.FillColor = Color.FromArgb (94, 148, 255);
                infocheck = false;
            }
            else
            {

                string nomorRod = txtnomorrod.Text;

                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT * FROM penerimaan_s WHERE nomor_rod = @nomor_rod";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nomor_rod", nomorRod);

                        try
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                txtjenis.Text = reader["jenis"].ToString();
                                lbltotale1.Text = reader["e1"].ToString();
                                lbltotale2.Text = reader["e2"].ToString();
                                txte3.Text = reader["e3"].ToString();
                                txts.Text = reader["s"].ToString();
                                txtd.Text = reader["d"].ToString();
                                txtb.Text = reader["b"].ToString();
                                txtba.Text = reader["ba"].ToString();
                                txtcr.Text = reader["cr"].ToString();
                                txtm.Text = reader["m"].ToString();
                                txtr.Text = reader["r"].ToString();
                                txtc.Text = reader["c"].ToString();
                                txtrl.Text = reader["rl"].ToString();
                                tanggalpenerimaan = Convert.ToDateTime(reader["tanggal_penerimaan"]);
                                settrue();
                                btncheck.Text = "Batal";
                                btncheck.FillColor = Color.Red;
                                infocheck = true;
                                txtnomorrod.Enabled = false;

                            }
                            else
                            {
                                MessageBox.Show("Data tidak ditemukan.");
                                setdefault();
                                setfalse();
                            }

                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                }
                
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
