using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOS_FxApps
{
    public partial class formstok : Form
    {
        SqlConnection conn = Koneksi.GetConnection();
        public formstok()
        {
            InitializeComponent();
        }

        private void formstok_Load(object sender, EventArgs e)
        {
            tampil();
            btndelete.Enabled = false;
            btnupdate.Enabled = false;
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM stok_material";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns[0].HeaderText = "Kode Barang";
                dataGridView1.Columns[1].HeaderText = "Nama Barang";
                dataGridView1.Columns[2].HeaderText = "Jumlah Stok";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void setdefault()
        {
            txtkodebarang.Enabled = true;
            txtkodebarang.Clear();
            txtstok.Clear();
            txtnamabarang.Clear();
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if (txtkodebarang.Text == "" || txtnamabarang.Text == "" || txtstok.Text == "")
            {
                MessageBox.Show("Data Harus Diisi Dengan Lengkap.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (btnsimpan.Text == "Cancel")
                {
                    txtkodebarang.Enabled = true;
                    btnupdate.Enabled = false;
                    btndelete.Enabled = false;
                    btnsimpan.Text = "Simpan";
                    setdefault();
                }
                else
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmdcekkode = new SqlCommand("SELECT kodeBarang FROM stok_material WHERE kodeBarang = @kode", conn))
                        {
                            cmdcekkode.Parameters.AddWithValue("@kode", txtkodebarang.Text);
                            using (SqlDataReader dr = cmdcekkode.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    MessageBox.Show("Kode Sudah Dipakai Material Lain", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO stok_material VALUES(@kodebarang,@namabarang,@stok)", conn))
                        {
                            cmd.Parameters.AddWithValue("@kodebarang", txtkodebarang.Text);
                            cmd.Parameters.AddWithValue("@namabarang", txtnamabarang.Text);
                            cmd.Parameters.AddWithValue("@stok", txtstok.Text);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Data Berhasil Disimpan.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        setdefault();
                        tampil();
                        pemakaianMaterial.instance.combonama();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data Gagal Dimasukkan " + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE stok_material WHERE kodeBarang = @kodebarang", conn);
                cmd.Parameters.AddWithValue("@kodebarang", txtkodebarang.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Dihapus", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();
                tampil();
                pemakaianMaterial.instance.combonama();
                btnupdate.Enabled = false;
                btndelete.Enabled = false;
                btnsimpan.Text = "Simpan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Gagal Dihapus " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "UPDATE stok_material SET namaBarang = @namabarang, jumlahStok = @stok WHERE kodeBarang = @kodebarang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kodebarang", txtkodebarang.Text);
                cmd.Parameters.AddWithValue("@namabarang", txtnamabarang.Text);
                cmd.Parameters.AddWithValue("@stok", txtstok.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Diupdate", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();  
                tampil();
                pemakaianMaterial.instance.combonama();
                btnupdate.Enabled = false;
                btndelete.Enabled = false;
                btnsimpan.Text = "Simpan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengupdate data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtkodebarang.Text = row.Cells["kodeBarang"].Value.ToString();
                txtnamabarang.Text = row.Cells["namaBarang"].Value.ToString();
                txtstok.Text = row.Cells["jumlahStok"].Value.ToString();

                txtkodebarang.Enabled = false;
                btnupdate.Enabled = true;
                btndelete.Enabled = true;
                btnsimpan.Text = "Cancel";
            }
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
