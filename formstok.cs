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
using System.IO;

namespace GOS_FxApps
{
    public partial class formstok : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        private byte[] imageBytes = null;

        public formstok()
        {
            InitializeComponent();
        }

        private void formstok_Load(object sender, EventArgs e)
        {
            tampil();
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

                dt.Columns["foto"].ColumnName = "Gambar";

                DataTable dtWithImage = new DataTable();
                dtWithImage.Columns.Add("Kode Barang", typeof(string));
                dtWithImage.Columns.Add("Nama Barang", typeof(string));
                dtWithImage.Columns.Add("Jumlah Stok", typeof(int));
                dtWithImage.Columns.Add("Gambar", typeof(Image));

                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtWithImage.NewRow();
                    newRow["Kode Barang"] = row["kodeBarang"];
                    newRow["Nama Barang"] = row["namaBarang"];
                    newRow["Jumlah Stok"] = row["jumlahStok"];

                    if (row["Gambar"] != DBNull.Value)
                    {
                        byte[] imageBytes = (byte[])row["Gambar"];
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            newRow["Gambar"] = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        newRow["Gambar"] = null;
                    }

                    dtWithImage.Rows.Add(newRow);
                }

                dataGridView1.DataSource = dtWithImage;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowTemplate.Height = 100;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Height = 100;
                }

                DataGridViewImageColumn imageCol = (DataGridViewImageColumn)dataGridView1.Columns["Gambar"];
                imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.DefaultCellStyle.Padding = new Padding(5);
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        private void cari()
        {
            string keyword = txtcari.Text;

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM stok_material WHERE kodeBarang LIKE @keyword OR namaBarang LIKE @keyword", conn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);

                    dt.Columns["foto"].ColumnName = "Gambar";

                    DataTable dtWithImage = new DataTable();
                    dtWithImage.Columns.Add("Kode Barang", typeof(string));
                    dtWithImage.Columns.Add("Nama Barang", typeof(string));
                    dtWithImage.Columns.Add("Jumlah Stok", typeof(int));
                    dtWithImage.Columns.Add("Gambar", typeof(Image));

                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow newRow = dtWithImage.NewRow();
                        newRow["Kode Barang"] = row["kodeBarang"];
                        newRow["Nama Barang"] = row["namaBarang"];
                        newRow["Jumlah Stok"] = row["jumlahStok"];

                        if (row["Gambar"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])row["Gambar"];
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                newRow["Gambar"] = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            newRow["Gambar"] = null;
                        }

                        dtWithImage.Rows.Add(newRow);
                    }

                    dataGridView1.DataSource = dtWithImage;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.RowTemplate.Height = 100;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Height = 100;
                    }

                    DataGridViewImageColumn imageCol = (DataGridViewImageColumn)dataGridView1.Columns["Gambar"];
                    imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.DefaultCellStyle.Padding = new Padding(5);
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
        }

        private void setdefault()
        {
            txtkodebarang.Enabled = true;
            txtkodebarang.Clear();
            txtstok.Clear();
            txtnamabarang.Clear();
            imageBytes = null;
            picturebox.Image = null;
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if (txtkodebarang.Text == "" || txtnamabarang.Text == "" || txtstok.Text == "" || imageBytes == null)
            {
                MessageBox.Show("Data Harus Diisi Dengan Lengkap.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (btnsimpan.Text == "Cancel")
                {
                    txtkodebarang.Enabled = true;
                    btnupdate.Enabled = false;
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

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO stok_material VALUES(@kodebarang,@namabarang,@stok,@foto)", conn))
                        {
                            cmd.Parameters.AddWithValue("@kodebarang", txtkodebarang.Text);
                            cmd.Parameters.AddWithValue("@namabarang", txtnamabarang.Text);
                            cmd.Parameters.AddWithValue("@stok", txtstok.Text);
                            cmd.Parameters.AddWithValue("@foto", imageBytes);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Data Berhasil Disimpan.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        setdefault();
                        tampil();
                        pemakaianMaterial.instance.combonama();
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
            }
            
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "UPDATE stok_material SET namaBarang = @namabarang, jumlahStok = @stok, foto = @foto WHERE kodeBarang = @kodebarang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kodebarang", txtkodebarang.Text);
                cmd.Parameters.AddWithValue("@namabarang", txtnamabarang.Text);
                cmd.Parameters.AddWithValue("@stok", txtstok.Text);
                cmd.Parameters.AddWithValue("@foto", imageBytes);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Diupdate", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setdefault();  
                tampil();
                pemakaianMaterial.instance.combonama();
                btnupdate.Enabled = false;
                btnsimpan.Text = "Simpan";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtkodebarang.Text = row.Cells["Kode Barang"].Value.ToString();
                txtnamabarang.Text = row.Cells["Nama Barang"].Value.ToString();
                txtstok.Text = row.Cells["Jumlah Stok"].Value.ToString();

                Image img = row.Cells["Gambar"].Value as Image;
                if (img != null)
                {
                    picturebox.Image = img;

                    using (Bitmap clone = new Bitmap(img))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            clone.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageBytes = ms.ToArray();
                        }
                    }
                }
                else
                {
                    picturebox.Image = null;
                    imageBytes = null;
                }


                txtkodebarang.Enabled = false;
                btnupdate.Enabled = true;
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

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            cari();
        }

        private void btnopenfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Pilih Foto Material";
                openFileDialog.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picturebox.Image = Image.FromFile(openFileDialog.FileName);
                    imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                }
            }
        }
    }
}
