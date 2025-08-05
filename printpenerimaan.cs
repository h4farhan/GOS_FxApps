using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GOS_FxApps.DataSet;
using Microsoft.Data.SqlClient;
using Microsoft.Reporting.Map.WebForms.BingMaps;
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
                    MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu", "Warning");
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

        private void formPemakaian()
        {
            int bulan = datecaripemakaian.Value.Month;     
            int tahun = datecaripemakaian.Value.Year;

            int jumlahHari = DateTime.DaysInMonth(tahun, bulan);

            string namaFileRDLC = null;
            switch (jumlahHari)
            {
                case 28:
                    namaFileRDLC = "laporanPemakaian_28.rdlc";
                    break;
                case 29:
                    namaFileRDLC = "laporanPemakaian_29.rdlc";
                    break;
                case 30:
                    namaFileRDLC = "laporanPemakaian_30.rdlc";
                    break;
                case 31:
                    namaFileRDLC = "laporanPemakaian_31.rdlc";
                    break;
            }

            var adapter = new GOS_FxApps.DataSet.laporanpemakaianTableAdapters.sp_LaporanPemakaianMaterialTableAdapter();
            GOS_FxApps.DataSet.laporanpemakaian.sp_LaporanPemakaianMaterialDataTable data = adapter.GetData(bulan, tahun);

            var adapterperbaikan = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.sp_LaporanPerbaikanTableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.sp_LaporanPerbaikanDataTable dataperbaikan = adapterperbaikan.GetData(bulan, tahun);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, namaFileRDLC);

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetlaporanpemakaian", (DataTable)data));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetjumlahperbaikan", (DataTable)dataperbaikan));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("bulan", bulan.ToString()),
        new ReportParameter("tahun", tahun.ToString())
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
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu", "Warning");
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
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu", "Warning");
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

        private void formwelding()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            var adapter = new GOS_FxApps.DataSet.rb_stokTableAdapters.sp_Rb_StokTableAdapter();
            GOS_FxApps.DataSet.rb_stok.sp_Rb_StokDataTable data = adapter.GetData(bulan, tahun);

            var adapterfirst = new GOS_FxApps.DataSet.rb_stokTableAdapters.Rb_StokTableAdapter();
            GOS_FxApps.DataSet.rb_stok.Rb_StokDataTable datafirst = adapterfirst.GetData(bulan, tahun);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Rb_Stok.rdlc");

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetrbstok", (DataTable)data));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetfirstrbstok", (DataTable)datafirst));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("bulan", bulan.ToString()),
        new ReportParameter("tahun", tahun.ToString())
            };

            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();
            frmrpt.Show();
        }

        private void formlaporanharian()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            var adapter = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.sp_Laporan_HarianTableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.sp_Laporan_HarianDataTable data = adapter.GetData(bulan, tahun);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "produksiharian.rdlc");

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetharian", (DataTable)data));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("bulan", bulan.ToString()),
        new ReportParameter("tahun", tahun.ToString())
            };

            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();
            frmrpt.Show();
        }

        private void formactual()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            int jumlahHari = DateTime.DaysInMonth(tahun, bulan);

            string namaFileRDLC = null;
            switch (jumlahHari)
            {
                case 28:
                    namaFileRDLC = "actual_28.rdlc";
                    break;
                case 29:
                    namaFileRDLC = "actual_29.rdlc";
                    break;
                case 30:
                    namaFileRDLC = "actual_30.rdlc";
                    break;
                case 31:
                    namaFileRDLC = "actual_31.rdlc";
                    break;
            }

            var adapteractual = new GOS_FxApps.DataSet.actualTableAdapters.sp_LaporanActualTableAdapter();
            GOS_FxApps.DataSet.actual.sp_LaporanActualDataTable dataactual = adapteractual.GetData(bulan, tahun);

            var adapterperbaikan = new GOS_FxApps.DataSet.actualTableAdapters.sp_LaporanShiftPerbaikanTableAdapter();
            GOS_FxApps.DataSet.actual.sp_LaporanShiftPerbaikanDataTable dataperbaikan = adapterperbaikan.GetData(bulan, tahun);

            var adapterpenerimaan = new GOS_FxApps.DataSet.actualTableAdapters.sp_LaporanShiftPenerimaanTableAdapter();
            GOS_FxApps.DataSet.actual.sp_LaporanShiftPenerimaanDataTable datapenerimaan = adapterpenerimaan.GetData(bulan, tahun);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, namaFileRDLC);

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetactual", (DataTable)dataactual));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetshiftperbaikan", (DataTable)dataperbaikan));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetshiftpenerimaan", (DataTable)datapenerimaan));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("bulan", bulan.ToString()),
        new ReportParameter("tahun", tahun.ToString())
            };

            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();
            frmrpt.Show();
        }

        private void formkondisi()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            int jumlahHari = DateTime.DaysInMonth(tahun, bulan);

            string namaFileRDLC = null;
            switch (jumlahHari)
            {
                case 28:
                    namaFileRDLC = "kondisi_28.rdlc";
                    break;
                case 29:
                    namaFileRDLC = "kondisi_29.rdlc";
                    break;
                case 30:
                    namaFileRDLC = "kondisi_30.rdlc";
                    break;
                case 31:
                    namaFileRDLC = "kondisi_31.rdlc";
                    break;
            }

            var adapterkondisi = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiPerbaikanTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiPerbaikanDataTable datakondisi = adapterkondisi.GetData(bulan, tahun);

            var adapterperbaikan = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanShiftPerbaikanTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanShiftPerbaikanDataTable dataperbaikan = adapterperbaikan.GetData(bulan, tahun);

            var adapterpenerimaan = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanShiftPenerimaanTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanShiftPenerimaanDataTable datapenerimaan = adapterpenerimaan.GetData(bulan, tahun);

            var adapterbutt = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiButtRatioTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiButtRatioDataTable databutt = adapterbutt.GetData(bulan, tahun);

            var adapterman = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiManPowerTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiManPowerDataTable dataman = adapterman.GetData(bulan, tahun);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, namaFileRDLC);

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisiperbaikan", (DataTable)datakondisi));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisishiftperbaikan", (DataTable)dataperbaikan));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisishiftpenerimaan", (DataTable)datapenerimaan));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisibuttratio", (DataTable)databutt));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisimanpower", (DataTable)dataman));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("bulan", bulan.ToString()),
        new ReportParameter("tahun", tahun.ToString())
            };

            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();
            frmrpt.Show();
        }

        private reportviewr frmrpt;

        private void guna2Button2_Click(object sender, EventArgs e) 
        {
            if (cmbpilihdata.SelectedItem == null)
                return;

            string pilihan = cmbpilihdata.SelectedItem.ToString();

            if (pilihan == "Penerimaan")
            {
                formpenerimaan();
            }
            else if (pilihan == "Perbaikan")
            {
                formperbaikan();
            }
            else if (pilihan == "Pengiriman")
            {
                formpengiriman();
            }
            else if (pilihan == "Welding Pieces")
            {
                formwelding();
            }
            else if (pilihan == "Hasil Produksi & Pemakaian Material")
            {
                formPemakaian();
            }
            else if (pilihan == "Summary Data for Anode ROD Repair")
            {
                formlaporanharian();
            }
            else if (pilihan == "Actual Quantity for Repaired ROD Assy")
            {
                formactual();
            }
            else if (pilihan == "Kondisi ROD Reject di Rod Repair Shop")
            {
                formkondisi();
            }
        }

        private void printpenerimaan_Load(object sender, EventArgs e)
        {
            datecari.Checked = false;
            datecari.Value = DateTime.Now.Date;
            datecaripemakaian.Checked = false;
            datecaripemakaian.Value = DateTime.Now.Date;
            datecaripemakaian.ShowUpDown = true;
            cmbpilihdata.SelectedIndex = 0;
            infocari = false;
            btncari.Text = "Cari";
            btnprint.Enabled = false;
            guna2Panel4.ResetText();
            datecari.Checked = false;
            paneldata2.Visible = false;

            paneldata2.Visible = true;
            btncari.Enabled = true;
            tampilpenerimaan();
            TambahCancelOption();
            jumlahdata();
        }

        private void tampilpenerimaan()
        {
            try
            {
                string query = "SELECT * FROM penerimaan_p ORDER BY tanggal_penerimaan DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

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
                dataGridView1.Columns["updated_at"].Visible = false;
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

        private void tampilperbaikan()
        {
            try
            {
                string query = "SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, ba, ba1, cr, m, r, c, rl, jumlah, tanggal_penerimaan, updated_at FROM perbaikan_p ORDER BY tanggal_perbaikan DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

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
                dataGridView1.Columns[13].HeaderText = "E4";
                dataGridView1.Columns[14].HeaderText = "S";
                dataGridView1.Columns[15].HeaderText = "D";
                dataGridView1.Columns[16].HeaderText = "B";
                dataGridView1.Columns[17].HeaderText = "BA";
                dataGridView1.Columns[18].HeaderText = "BA-1";
                dataGridView1.Columns[19].HeaderText = "CR";
                dataGridView1.Columns[20].HeaderText = "M";
                dataGridView1.Columns[21].HeaderText = "R";
                dataGridView1.Columns[22].HeaderText = "C";
                dataGridView1.Columns[23].HeaderText = "RL";
                dataGridView1.Columns[24].HeaderText = "Jumlah";
                dataGridView1.Columns[25].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[26].HeaderText = "Diubah";
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

        private void tampilpengiriman()
        {
            try
            {
                string query = "SELECT * FROM pengiriman ORDER BY tanggal_pengiriman DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Pengiriman";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Diubah";
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

        private void tampilwelding()
        {
            try
            {
                string query = "SELECT * FROM Rb_Stok ORDER BY tanggal DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

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
                dataGridView1.Columns["updated_at"].Visible = false;
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

        private void tampilpemakaianmaterial()
        {
            try
            {
                string query = "SELECT * FROM pemakaian_material ORDER BY tanggalPemakaian DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowTemplate.Height = 35;
                dataGridView1.ReadOnly = true;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Kode Barang";
                dataGridView1.Columns[2].HeaderText = "Nama Barang";
                dataGridView1.Columns[3].HeaderText = "Tanggal Pemakaian";
                dataGridView1.Columns[4].HeaderText = "Jumlah Pemakaian";
                dataGridView1.Columns["updated_at"].Visible = false;
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

        private bool caripenerimaan()
        {
            DateTime? tanggal1 = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            DateTime? tanggal2 = datecari.Checked ? (DateTime?)datecari.Value.AddDays(1).Date : null;
            string shift = cbShift.SelectedItem ?.ToString();

            if (!tanggal1.HasValue || string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan isi tanggal dan pilih shift untuk melakukan pencarian.", "Warning");
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

                return dt.Rows.Count > 0;
            }
        }

        private bool cariperbaikan()
        {
            DateTime? tanggal1 = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            DateTime? tanggal2 = datecari.Checked ? (DateTime?)datecari.Value.AddDays(1).Date : null;
            string shift = cbShift.SelectedItem?.ToString(); 

            if (!tanggal1.HasValue || string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan isi tanggal dan pilih shift untuk melakukan pencarian.", "Warning");
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

                return dt.Rows.Count > 0;
            }
        }

        private bool caripengiriman()
        {
            DateTime? tanggal1 = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            DateTime? tanggal2 = datecari.Checked ? (DateTime?)datecari.Value.AddDays(1).Date : null;
            string shift = cbShift.SelectedItem?.ToString();

            if (!tanggal1.HasValue || string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan isi tanggal dan pilih shift untuk melakukan pencarian.", "Warning");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM pengiriman WHERE tanggal_pengiriman >= @tanggal1 AND tanggal_pengiriman < @tanggal2 AND shift = @shift";

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

                return dt.Rows.Count > 0;
            }
        }

        private bool caripemakaian()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            if (datecaripemakaian.Checked == false)
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.", "Warning");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM pemakaian_material WHERE YEAR(tanggalPemakaian) = @year AND MONTH(tanggalPemakaian) = @bulan;";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@year", tahun);
                cmd.Parameters.AddWithValue("@bulan", bulan);

                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.DataSource = dt;
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

                return dt.Rows.Count > 0;
            }
        }

        private bool cariwelding()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            if (datecaripemakaian.Checked == false)
            {
                MessageBox.Show("Silakan isi tanggal atau nomor ROD untuk melakukan pencarian.", "Warning");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM Rb_Stok WHERE YEAR(tanggal) = @year AND MONTH(tanggal) = @bulan;";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@year", tahun);
                cmd.Parameters.AddWithValue("@bulan", bulan);

                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.DataSource = dt;
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
                return dt.Rows.Count > 0;
            }
        }

        private bool carilaporanharian()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            if (datecaripemakaian.Checked == false)
            {
                MessageBox.Show("Silakan isi tanggal untuk melakukan pencarian.", "Warning");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM perbaikan_p WHERE YEAR(tanggal_perbaikan) = @year AND MONTH(tanggal_perbaikan) = @bulan;";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@year", tahun);
                cmd.Parameters.AddWithValue("@bulan", bulan);

                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.DataSource = dt;
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
                return dt.Rows.Count > 0;
            }
        }

        private bool cariactual()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            if (datecaripemakaian.Checked == false)
            {
                MessageBox.Show("Silakan isi tanggal untuk melakukan pencarian.", "Warning");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM perbaikan_p WHERE YEAR(tanggal_perbaikan) = @year AND MONTH(tanggal_perbaikan) = @bulan;";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@year", tahun);
                cmd.Parameters.AddWithValue("@bulan", bulan);

                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.DataSource = dt;
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
                return dt.Rows.Count > 0;
            }
        }

        private bool carikondisi()
        {
            int bulan = datecaripemakaian.Value.Month;
            int tahun = datecaripemakaian.Value.Year;

            if (datecaripemakaian.Checked == false)
            {
                MessageBox.Show("Silakan isi tanggal untuk melakukan pencarian.", "Warning");
                return false;
            }

            DataTable dt = new DataTable();

            string query = "SELECT * FROM perbaikan_p WHERE YEAR(tanggal_perbaikan) = @year AND MONTH(tanggal_perbaikan) = @bulan;";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@year", tahun);
                cmd.Parameters.AddWithValue("@bulan", bulan);

                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.DataSource = dt;
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
                return dt.Rows.Count > 0;
            }
        }

        private void btncari_Click(object sender, EventArgs e)
        {

            if (cmbpilihdata.SelectedItem == null)
                return;

            string pilihan = cmbpilihdata.SelectedItem.ToString();

            if (pilihan == "Penerimaan")
            {
                if (!infocari)
                {
                    bool hasilCari = caripenerimaan();
                    if (hasilCari)
                    {
                        infocari = true;
                        btnprint.Enabled = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                    else
                    {
                        infocari = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                }
                else
                {
                    tampilpenerimaan();
                    infocari = false;
                    btncari.Text = "Cari";

                    btnprint.Enabled = false;

                    guna2Panel4.ResetText();
                    datecari.Checked = false;
                    jumlahdata();
                }
            } 
            else if (pilihan == "Perbaikan")
            {
                if (!infocari)
                {
                    bool hasilCari = cariperbaikan();
                    if (hasilCari)
                    {
                        infocari = true;
                        btnprint.Enabled = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                    else
                    {
                        infocari = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                }
                else
                {
                    tampilperbaikan();
                    infocari = false;
                    btncari.Text = "Cari";
                    jumlahdata();

                    btnprint.Enabled = false;

                    guna2Panel4.ResetText();
                    datecari.Checked = false;
                }
            }
            else if (pilihan == "Pengiriman")
            {
                if (!infocari)
                {
                    bool hasilCari = caripengiriman();
                    if (hasilCari)
                    {
                        infocari = true;
                        btnprint.Enabled = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                    else
                    {
                        infocari = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                }
                else
                {
                    tampilpengiriman();
                    infocari = false;
                    btncari.Text = "Cari";
                    jumlahdata();

                    btnprint.Enabled = false;

                    guna2Panel4.ResetText();
                    datecari.Checked = false;
                }
            }
            else if (pilihan == "Welding Pieces")
            {
                if (!infocari)
                {
                    bool hasilCari = cariwelding();
                    if (hasilCari)
                    {
                        infocari = true;
                        btnprint.Enabled = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                    else
                    {
                        infocari = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                }
                else
                {
                    tampilwelding();
                    infocari = false;
                    btncari.Text = "Cari";
                    jumlahdata();

                    btnprint.Enabled = false;

                    guna2Panel4.ResetText();
                    datecaripemakaian.Checked = false;
                }
            }
            else if (pilihan == "Hasil Produksi & Pemakaian Material")
            {
                if (!infocari)
                {
                    bool hasilCari = caripemakaian();
                    if (hasilCari)
                    {
                        infocari = true;
                        btnprint.Enabled = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                    else
                    {
                        infocari = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                }
                else
                {
                    tampilpemakaianmaterial();
                    infocari = false;
                    btncari.Text = "Cari";
                    jumlahdata();

                    btnprint.Enabled = false;

                    guna2Panel4.ResetText();
                    datecaripemakaian.Checked = false;
                }
            }
            else if (pilihan == "Summary Data for Anode ROD Repair")
            {
                if (!infocari)
                {
                    bool hasilCari = carilaporanharian();
                    if (hasilCari)
                    {
                        infocari = true;
                        btnprint.Enabled = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                    else
                    {
                        infocari = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                }
                else
                {
                    tampilperbaikan();
                    infocari = false;
                    btncari.Text = "Cari";
                    jumlahdata();

                    btnprint.Enabled = false;

                    guna2Panel4.ResetText();
                    datecari.Checked = false;
                }
            }
            else if (pilihan == "Actual Quantity for Repaired ROD Assy")
            {
                if (!infocari)
                {
                    bool hasilCari = cariactual();
                    if (hasilCari)
                    {
                        infocari = true;
                        btnprint.Enabled = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                    else
                    {
                        infocari = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                }
                else
                {
                    tampilperbaikan();
                    infocari = false;
                    btncari.Text = "Cari";
                    jumlahdata();

                    btnprint.Enabled = false;

                    guna2Panel4.ResetText();
                    datecari.Checked = false;
                }
            }
            else if (pilihan == "Kondisi ROD Reject di Rod Repair Shop")
            {
                if (!infocari)
                {
                    bool hasilCari = carikondisi();
                    if (hasilCari)
                    {
                        infocari = true;
                        btnprint.Enabled = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                    else
                    {
                        infocari = true;
                        btncari.Text = "Reset";
                        jumlahdata();
                    }
                }
                else
                {
                    tampilperbaikan();
                    infocari = false;
                    btncari.Text = "Cari";
                    jumlahdata();

                    btnprint.Enabled = false;

                    guna2Panel4.ResetText();
                    datecari.Checked = false;
                }
            }
        }

        private void jumlahdata()
        {
            int total = dataGridView1.Rows.Count;
            label4.Text = "Jumlah data: " + total.ToString();
            jlhpanel1.Text = "Jumlah data: " + total.ToString();
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
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecari.Checked = false;
                paneldata2.Visible = false;

                paneldata2.Visible = true;
                btncari.Enabled = true;
                tampilpenerimaan();
                TambahCancelOption();
                jumlahdata();
            }
            else if (pilihan == "Perbaikan")
            {
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecari.Checked = false;
                paneldata2.Visible = false;

                paneldata2.Visible = true;
                btncari.Enabled = true;
                tampilperbaikan();
                TambahCancelOption();
                jumlahdata();
            }
            else if (pilihan == "Pengiriman")
            {
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecari.Checked = false;
                paneldata2.Visible = false;

                paneldata2.Visible = true;
                btncari.Enabled = true;
                tampilpengiriman();
                TambahCancelOption();
                jumlahdata();
            }
            else if (pilihan == "Welding Pieces")
            {
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecaripemakaian.Checked = false;
                paneldata2.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                tampilwelding();
                TambahCancelOption();
                jumlahdata();
            }
            else if (pilihan == "Hasil Produksi & Pemakaian Material")
            {
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecaripemakaian.Checked = false;
                paneldata2.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                tampilpemakaianmaterial();
                TambahCancelOption();
                jumlahdata();
            }
            else if (pilihan == "Summary Data for Anode ROD Repair")
            {
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecaripemakaian.Checked = false;
                paneldata2.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                tampilperbaikan();
                TambahCancelOption();
                jumlahdata();
            }
            else if (pilihan == "Actual Quantity for Repaired ROD Assy")
            {
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecaripemakaian.Checked = false;
                paneldata2.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                tampilperbaikan();
                TambahCancelOption();
                jumlahdata();
            }
            else if (pilihan == "Kondisi ROD Reject di Rod Repair Shop")
            {
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecaripemakaian.Checked = false;
                paneldata2.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                tampilperbaikan();
                TambahCancelOption();
                jumlahdata();
            }
            else if (pilihan == "Cancel")
            {
                btncari.Enabled = false;
                paneldata1.Visible = false;
                paneldata2.Visible = false;
                cmbpilihdata.SelectedIndex = -1;
                dataGridView1.DataSource = null;
                cmbpilihdata.Items.Remove("Cancel");
            }

        }
        
        private void datecari_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

