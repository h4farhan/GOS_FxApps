using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class Penerimaan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();
        public Penerimaan()
        {
            InitializeComponent();
            setdefault();
            tampil();
        }

        private void Penerimaan_Load(object sender, EventArgs e)
        {
            btnsimpan.Enabled = false;
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM penerimaan_s";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Jenis";
                dataGridView1.Columns[5].HeaderText = "Stasiun";
                dataGridView1.Columns[6].HeaderText = "E1";
                dataGridView1.Columns[7].HeaderText = "E2";
                dataGridView1.Columns[8].HeaderText = "E3";
                dataGridView1.Columns[9].HeaderText = "S";
                dataGridView1.Columns[10].HeaderText = "D";
                dataGridView1.Columns[11].HeaderText = "B";
                dataGridView1.Columns[12].HeaderText = "BA";
                dataGridView1.Columns[13].HeaderText = "CR";
                dataGridView1.Columns[14].HeaderText = "M";
                dataGridView1.Columns[15].HeaderText = "R";
                dataGridView1.Columns[16].HeaderText = "C";
                dataGridView1.Columns[17].HeaderText = "RL";
                dataGridView1.Columns[18].HeaderText = "Jumlah";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            

            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("INSERT INTO penerimaan_s (tanggal_penerimaan,shift,nomor_rod,jenis,stasiun,e1,e2,e3," +
                    "s,d,b,ba,cr,m,r,c,rl,jumlah) VALUES(getdate(),@shift,@nomorrod,@jenis,@stasiun,@e1,@e2,@e3,@s,@d,@b,@ba,@cr,@m,@r,@c,@rl,@jumlah)", conn);
                SqlCommand cmd2 = new SqlCommand("INSERT INTO penerimaan_p (tanggal_penerimaan,shift,nomor_rod,jenis,stasiun,e1,e2,e3," +
                    "s,d,b,ba,cr,m,r,c,rl,jumlah) VALUES(getdate(),@shift,@nomorrod,@jenis,@stasiun,@e1,@e2,@e3,@s,@d,@b,@ba,@cr,@m,@r,@c,@rl,@jumlah)", conn);

                cmd1.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                cmd1.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                cmd1.Parameters.AddWithValue("@jenis", txtjenis.Text);
                cmd1.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                cmd1.Parameters.AddWithValue("@e1", txte1.Text);
                cmd1.Parameters.AddWithValue("@e2", txte2.Text);
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

                cmd2.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                cmd2.Parameters.AddWithValue("@nomorrod", txtnomorrod.Text);
                cmd2.Parameters.AddWithValue("@jenis", txtjenis.Text);
                cmd2.Parameters.AddWithValue("@stasiun", txtstasiun.Text);
                cmd2.Parameters.AddWithValue("@e1", txte1.Text);
                cmd2.Parameters.AddWithValue("@e2", txte2.Text);
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

                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                MessageBox.Show("Data Berhasil Disimpan");
                setdefault();
                tampil();
                btnsimpan.Enabled = false;

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

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void setdefault()
        {
            txtnomorrod.Clear();
            txtjenis.Clear();
            txtstasiun.Clear();
            txte1.Clear();
            txte2.Clear();
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
        }

        private int SafeParse(Guna2TextBox tb)
        {
            return int.TryParse(tb.Text, out int result) ? result : 0;
        }

        private void btnhitung_Click(object sender, EventArgs e)
        {
            if (txtnomorrod.Text == "" || txtjenis.Text == "" || txtstasiun.Text == "") 
            {
                MessageBox.Show("Data Tidak Boleh Kosong");
            }
            else
            {
                int angka1 = SafeParse(txte1);
                int angka2 = SafeParse(txte2);
                int angka3 = SafeParse(txte3);
                int angka4 = SafeParse(txts);
                int angka5 = SafeParse(txtd);
                int angka6 = SafeParse(txtb);
                int angka7 = SafeParse(txtba);
                int angka8 = SafeParse(txtcr);
                int angka9 = SafeParse(txtm);
                int angka10 = SafeParse(txtr);
                int angka11 = SafeParse(txtc);
                int angka12 = SafeParse(txtrl);

                int hasil = angka1 + angka2 + angka3 + angka4 + angka5 + angka6 + angka7 + angka8 + angka9 + angka10 + angka11 + angka12;
                lbltotal.Text = hasil.ToString();
                btnsimpan.Enabled = true;
            }  
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
