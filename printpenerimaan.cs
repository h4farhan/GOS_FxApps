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
            dataGridView1.ClearSelection();
        }

        private reportviewr frmrpt;

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DateTime tanggal1 = datecari.Value.Date;
            DateTime tanggal2 = datecari.Value.AddDays(1).Date;
            string shift = cbShift.SelectedItem?.ToString();
            string tim = txttim.Text;

            if (string.IsNullOrEmpty(tim))
            {
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu");
                return;
            }

            // Ambil data dari adapter
            var adapter = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.penerimaan_sTableAdapter();
            GOS_FxApps.DataSet.PenerimaanForm.penerimaan_sDataTable data = adapter.GetData(tanggal1, tanggal2, shift);

            // Inisialisasi form reportviewr
            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "penerimaan.rdlc");

            // Siapkan reportviewer

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();
            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSetPenerimaan", (DataTable)data));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
        new ReportParameter("shift", shift),
        new ReportParameter("tim", tim)
            };
            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();

            frmrpt.Show(); // Tampilkan form report
        }


        private void printpenerimaan_Load(object sender, EventArgs e)
        {
            tampil();
            datecari.Checked = false;
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
            DateTime? tanggal1 = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            DateTime? tanggal2 = datecari.Checked ? (DateTime?)datecari.Value.AddDays(1).Date : null;
            string shift = cbShift.SelectedItem ?.ToString(); // ambil nilai teks shift

            // Validasi input
            if (!tanggal1.HasValue || string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan isi tanggal dan pilih shift untuk melakukan pencarian.");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM penerimaan_p WHERE tanggal_penerimaan >= @tanggal1 AND tanggal_penerimaan < @tanggal2 AND shift = @shift";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@tanggal1", tanggal1);
                cmd.Parameters.AddWithValue("@tanggal2", tanggal2);
                cmd.Parameters.AddWithValue("@shift", shift);

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

                return dt.Rows.Count > 0; // true jika ada data ditemukan
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
                    btnprint.Enabled = true;
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

                btnprint.Enabled = false;

                guna2Panel4.ResetText();
                datecari.Checked = false;
            }
        }

        private void jumlahdata()
        {
            int total = dataGridView1.Rows.Count;
            labeljumlahbaris2.Text = "Jumlah data: " + total.ToString();
        }
    }
}

