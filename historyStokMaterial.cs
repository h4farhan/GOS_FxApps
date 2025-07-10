using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public partial class historyStokMaterial : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public historyStokMaterial()
        {
            InitializeComponent();
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
                dtWithImage.Columns.Add("No", typeof(int));
                dtWithImage.Columns.Add("Kode Barang", typeof(string));
                dtWithImage.Columns.Add("Nama Barang", typeof(string));
                dtWithImage.Columns.Add("Jumlah Stok", typeof(int));
                dtWithImage.Columns.Add("Gambar", typeof(Image));
                dtWithImage.Columns.Add("Disimpan", typeof(DateTime));
                dtWithImage.Columns.Add("Diubah", typeof(DateTime));

                int no = 1;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtWithImage.NewRow();
                    newRow["No"] = no++;
                    newRow["Kode Barang"] = row["kodeBarang"];
                    newRow["Nama Barang"] = row["namaBarang"];
                    newRow["Jumlah Stok"] = row["jumlahStok"];
                    newRow["Disimpan"] = row["created_at"];
                    newRow["Diubah"] = row["updated_at"];

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

                dataGridView1.Columns["No"].Width = 30;

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
                    dtWithImage.Columns.Add("No", typeof(int));
                    dtWithImage.Columns.Add("Kode Barang", typeof(string));
                    dtWithImage.Columns.Add("Nama Barang", typeof(string));
                    dtWithImage.Columns.Add("Jumlah Stok", typeof(int));
                    dtWithImage.Columns.Add("Gambar", typeof(Image));
                    dtWithImage.Columns.Add("Disimpan", typeof(DateTime));
                    dtWithImage.Columns.Add("Diubah", typeof(DateTime));

                    int no = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow newRow = dtWithImage.NewRow();
                        newRow["No"] = no++;
                        newRow["Kode Barang"] = row["kodeBarang"];
                        newRow["Nama Barang"] = row["namaBarang"];
                        newRow["Jumlah Stok"] = row["jumlahStok"];
                        newRow["Disimpan"] = row["created_at"];
                        newRow["Diubah"] = row["updated_at"];

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

                    dataGridView1.Columns["No"].Width = 30;

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

        private void historyStokMaterial_Load(object sender, EventArgs e)
        {
            tampil();
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            cari();
        }
    }
}
