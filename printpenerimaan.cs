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
using Microsoft.Reporting.WinForms;

namespace GOS_FxApps
{
    public partial class printpenerimaan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        bool infocari = false;

        public printpenerimaan()
        {
            InitializeComponent();
            table2();
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int jumlah = dataGridView1.SelectedRows.Count;
            labeljumlahbaris.Text = "Jumlah baris dipilih: " + jumlah.ToString();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView2.Rows.Count >= 30)
                {
                    MessageBox.Show("Data sudah maksimal (30 baris). Tidak bisa tambah lagi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    if (!selectedRow.IsNewRow)
                    {
                        string selectedId = selectedRow.Cells[1].Value?.ToString();

                        bool exists = false;
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (row.Cells[1].Value?.ToString() == selectedId)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (exists)
                        {
                            MessageBox.Show("Data di Tanggal " + selectedId + " sudah ada di tabel 2.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            object[] rowData = new object[selectedRow.Cells.Count];
                            for (int i = 0; i < selectedRow.Cells.Count; i++)
                            {
                                rowData[i] = selectedRow.Cells[i].Value;
                            }

                            dataGridView2.Rows.Add(rowData);
                            jumlahdata();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih minimal satu baris terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void printpenerimaan_Load(object sender, EventArgs e)
        {
            tampil();
            datecari.Checked = false;
            dateprint.Checked = false;
        }

        private void tampil()
        {
            try
            {
                string query = "SELECT * FROM penerimaan_p";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.AutoResizeColumnHeadersHeight();
                dataGridView1.Columns[1].FillWeight = 250;

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

        private void table2()
        {
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add("id", "Id");
            dataGridView2.Columns.Add("tgl", "Tanggal Penerimaan");
            dataGridView2.Columns.Add("shift", "Shift");
            dataGridView2.Columns.Add("nomorrod", "Nomor ROD");
            dataGridView2.Columns.Add("jenis", "Jenis");
            dataGridView2.Columns.Add("stasiun", "Stasiun");
            dataGridView2.Columns.Add("e1", "E1");
            dataGridView2.Columns.Add("e2", "E2");
            dataGridView2.Columns.Add("e3", "E3");
            dataGridView2.Columns.Add("s", "S");
            dataGridView2.Columns.Add("d", "D");
            dataGridView2.Columns.Add("b", "B");
            dataGridView2.Columns.Add("ba", "BA");
            dataGridView2.Columns.Add("cr", "CR");
            dataGridView2.Columns.Add("m", "M");
            dataGridView2.Columns.Add("r", "R");
            dataGridView2.Columns.Add("c", "C");
            dataGridView2.Columns.Add("rl", "RL");
            dataGridView2.Columns.Add("jumlah", "Jumlah");

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.AutoResizeColumnHeadersHeight();
            dataGridView2.Columns["tgl"].FillWeight = 250;
            dataGridView2.Columns["id"].Visible = false;
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void HurufOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            string inputRod = txtcari.Text.Trim();

            if (!tanggal.HasValue && string.IsNullOrEmpty(inputRod))
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM penerimaan_p WHERE 1=1";

            using (SqlCommand cmd = new SqlCommand())
            {
                if (tanggal.HasValue)
                {
                    query += "AND CAST(tanggal_penerimaan AS DATE) = @tgl";
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
                datecari.Checked = false;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridView2.Rows.Remove(row);
                        jumlahdata();
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih baris yang ingin dihapus terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void jumlahdata()
        {
            int total = dataGridView2.Rows.Count;
            labeljumlahbaris2.Text = "Jumlah data: " + total.ToString();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0 || dataGridView2.Rows.Cast<DataGridViewRow>().All(r => r.IsNewRow))
            {
                MessageBox.Show("Tabel sudah kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Yakin ingin menghapus semua data?", "Konfirmasi",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dataGridView2.Rows.Clear();
                jumlahdata();
            }
        }

        private reportviewr frmReport;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tgl = dateprint.Value.Date.ToString();
            string shift = cbShift.SelectedValue.ToString();
            string tim = txttim.Text;
            string nmrrod = dataGridView2.Rows[0].Cells["nomorrod"].Value.ToString();
            string jenis = dataGridView2.Rows[0].Cells["jenis"].Value.ToString();
            string stasiun = dataGridView2.Rows[0].Cells["stasiun"].Value.ToString();
            string e1 = dataGridView2.Rows[0].Cells["e1"].Value.ToString();
            string e2 = dataGridView2.Rows[0].Cells["e2"].Value.ToString();
            string e3 = dataGridView2.Rows[0].Cells["e3"].Value.ToString();
            string s = dataGridView2.Rows[0].Cells["s"].Value.ToString();
            string d = dataGridView2.Rows[0].Cells["d"].Value.ToString();
            string b = dataGridView2.Rows[0].Cells["b"].Value.ToString();
            string ba = dataGridView2.Rows[0].Cells["ba"].Value.ToString();
            string cr = dataGridView2.Rows[0].Cells["cr"].Value.ToString();
            string m = dataGridView2.Rows[0].Cells["m"].Value.ToString();
            string r = dataGridView2.Rows[0].Cells["r"].Value.ToString();
            string c = dataGridView2.Rows[0].Cells["c"].Value.ToString();
            string rl = dataGridView2.Rows[0].Cells["rl"].Value.ToString();
            string jumlah = dataGridView2.Rows[0].Cells["jumlah"].Value.ToString();

            if (frmReport == null || frmReport.IsDisposed)
                frmReport = new reportviewr();

            frmReport.SetReportParameters(tgl, shift, tim, nmrrod, jenis, stasiun, e1, e2, e3, s, d, b, ba, cr, m, r, c, rl, jumlah);

            frmReport.Show();
            frmReport.BringToFront();
        }
    }
}

