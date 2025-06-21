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

        private void formpenerimaan()
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

                var adapter = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.penerimaan_sTableAdapter();
                GOS_FxApps.DataSet.PenerimaanForm.penerimaan_sDataTable data = adapter.GetData(tanggal1, tanggal2, shift);

                frmrpt = new reportviewr();
                frmrpt.reportViewer1.Reset();
                frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "penerimaan.rdlc");

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

                frmrpt.Show();
        }

        private void formperbaikan()
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

            var adapter = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.perbaikan_pTableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.perbaikan_pDataTable data = adapter.GetData(tanggal1, tanggal2, shift);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Perbaikan.rdlc");

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();
            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSetPerbaikan", (DataTable)data));

            ReportParameter[] parameters = new ReportParameter[]
            {
            new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
            new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
            new ReportParameter("shift", shift),
            new ReportParameter("tim", tim)
            };
            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();

            frmrpt.Show();
        }

        private void formpengiriman()
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

            var adapter = new GOS_FxApps.DataSet.PengirimanFormTableAdapters.pengirimanTableAdapter();
            GOS_FxApps.DataSet.PengirimanForm.pengirimanDataTable data = adapter.GetData(tanggal1, tanggal2, shift);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i]["RowNumber"] = i + 1;
            }

            for (int i = data.Rows.Count; i < 120; i++)
            {
                var row = data.NewRow();             
                row["RowNumber"] = i + 1;
                row["nomor_rod"] = DBNull.Value;
                row["tanggal_pengiriman"] = DBNull.Value;
                row["shift"] = DBNull.Value;
                data.Rows.Add(row);           
            }

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Pengiriman.rdlc");

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();
            frmrpt.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetPengiriman", (DataTable)data));

            ReportParameter[] parameters = new ReportParameter[]
            {
            new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
            new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
            new ReportParameter("shift", shift),
            new ReportParameter("tim", tim)
            };
            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();

            frmrpt.Show();
        }

        private reportviewr frmrpt;

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            formperbaikan();
        }

        private void printpenerimaan_Load(object sender, EventArgs e)
        {
            datecari.Checked = false;
        }

        private void tampilpenerimaan()
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

        private void tampilperbaikan()
        {
            try
            {
                string query = "SELECT * FROM perbaikan_p";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
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

        private void tampilpengiriman()
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
                dataGridView1.Columns[24].HeaderText = "Tanggal Perbaikan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void HurufOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            else
            {
                e.Handled = true;
            }
        }

        private bool cari()
        {
            DateTime? tanggal1 = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            DateTime? tanggal2 = datecari.Checked ? (DateTime?)datecari.Value.AddDays(1).Date : null;
            string shift = cbShift.SelectedItem ?.ToString(); // ambil nilai teks shift

            if (!tanggal1.HasValue || string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan isi tanggal dan pilih shift untuk melakukan pencarian.");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM perbaikan_p WHERE tanggal_perbaikan >= @tanggal1 AND tanggal_perbaikan < @tanggal2 AND shift = @shift";

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
                tampilperbaikan();
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
            //labeljumlahbaris2.Text = "Jumlah data: " + total.ToString();
        }

        private void TambahCancelOption()
        {
            if (!cmbpilihdata.Items.Contains("Cancel"))
            {
                cmbpilihdata.Items.Add("Cancel");
            }
        }

        private void cmbpilihdata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedItem == null)
                return;

            string pilihan = cmbpilihdata.SelectedItem.ToString();

            if (pilihan == "Penerimaan")
            {
                paneldata.Visible = true;
                tampilpenerimaan();
                TambahCancelOption();
            }
            else if (pilihan == "Perbaikan")
            {
                paneldata.Visible = true;
                tampilperbaikan();
                TambahCancelOption();
            }
            else if (pilihan == "Pengiriman")
            {
                paneldata.Visible = true;
                tampilpengiriman();
                TambahCancelOption();
            }
            else if (pilihan == "Cancel")
            {
                paneldata.Visible = false;
                cmbpilihdata.SelectedIndex = -1;
                dataGridView1.DataSource = null;
            }

        }
    }
}

