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
using System.Data.SqlClient;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WinForms;
using Guna.UI2.WinForms;
using DrawingPoint = System.Drawing.Point;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web.Security;
using System.Windows.Data;

namespace GOS_FxApps
{
    public partial class printpenerimaan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        bool infocari = false;
        private bool isBinding = false;
        bool isProgrammaticChange = false;

        public printpenerimaan()
        {
            InitializeComponent();
            dataGridView1.ClearSelection();
        }

        private void registerpenerimaan()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.penerimaan_p", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampilpenerimaan();
                            jumlahdata();
                            registerpenerimaan();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerperbaikan()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.perbaikan_p", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampilperbaikan();
                            jumlahdata();
                            registerperbaikan();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerpengiriman()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.pengiriman", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampilpengiriman();
                            jumlahdata();
                            registerpengiriman();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerpemakaian()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.pemakaian_material", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampilpemakaianmaterial();
                            jumlahdata();
                            registerpemakaian();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerwelding()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.Rb_Stok", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        this.Invoke(new Action(() =>
                        {
                            tampilwelding();
                            jumlahdata();
                            registerwelding();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void formpenerimaan()
        {
                DateTime tanggal1 = datecari.Value.Date;
                DateTime tanggal2 = datecari.Value.AddDays(1).Date;
                string shift = cbShift.SelectedItem?.ToString();
                string tim = txttim.Text;

                if (string.IsNullOrEmpty(tim))
                {
                    MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var adapter = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.penerimaan_sTableAdapter();
                GOS_FxApps.DataSet.PenerimaanForm.penerimaan_sDataTable data = adapter.GetData(tanggal1, tanggal2, shift);

                var adapter2 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.jumlahpenerimaan2TableAdapter();
                GOS_FxApps.DataSet.PenerimaanForm.jumlahpenerimaan2DataTable data2 = adapter2.GetData(tanggal1, tanggal2, shift);

                frmrpt = new reportviewr();
                frmrpt.reportViewer1.Reset();
                frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "penerimaan.rdlc");

                frmrpt.reportViewer1.LocalReport.DataSources.Clear();
                frmrpt.reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSetPenerimaan", (DataTable)data));
                frmrpt.reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("datasetjumlahpenerimaan2", (DataTable)data2));

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
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var adapter = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.perbaikan_pTableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.perbaikan_pDataTable data = adapter.GetData(tanggal1, tanggal2, shift);

            var adapter2 = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.jumlahperbaikan2TableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.jumlahperbaikan2DataTable data2 = adapter2.GetData(tanggal1, tanggal2, shift);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Perbaikan.rdlc");

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();
            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSetPerbaikan", (DataTable)data));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetjumlahperbaikan2", (DataTable)data2));

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
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            var adapterreject = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanRejectBATableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanRejectBADataTable datareject = adapterreject.GetData(bulan, tahun);

            var adapterstokreguler = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanStokRegulerTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanStokRegulerDataTable datastokreguler = adapterstokreguler.GetData(bulan, tahun);

            var adapterstok = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiStokTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiStokDataTable datastok = adapterstok.GetData(bulan, tahun);

            var adapterstokrepair = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiStokRepairTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiStokRepairDataTable datastokrepair = adapterstokrepair.GetData(bulan, tahun);

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

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisirejectba", (DataTable)datareject));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("dastasetkondisistokreguler", (DataTable)datastokreguler));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisistok", (DataTable)datastok));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisistokrepair", (DataTable)datastokrepair));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("bulan", bulan.ToString()),
        new ReportParameter("tahun", tahun.ToString())
            };

            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();
            frmrpt.Show();
        }

        private void formmaterial()
        {
            int bulan = datematerial.Value.Month;
            int tahun = datematerial.Value.Year;
            string kode = cmbnamamaterial.SelectedValue.ToString();

            var adapter = new GOS_FxApps.DataSet.materialTableAdapters.cardMaterialTableAdapter();
            GOS_FxApps.DataSet.material.cardMaterialDataTable data = adapter.GetData(tahun, bulan, kode);

            var adapter2 = new GOS_FxApps.DataSet.materialTableAdapters.sp_dataCardMaterialTableAdapter();
            GOS_FxApps.DataSet.material.sp_dataCardMaterialDataTable data2 = adapter2.GetData(tahun, bulan, kode);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "cardmaterial.rdlc");

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();
            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("cardmaterial", (DataTable)data));

            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("dataCardMaterial", (DataTable)data2));

            ReportParameter[] parameters = new ReportParameter[]
            {
            new ReportParameter("bulan", bulan.ToString()),
            new ReportParameter("tahun", tahun.ToString()),
            new ReportParameter("kode", kode)
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
            else if (pilihan == "Kartu Stock Material")
            {
                formmaterial();
            }
        }

        private void printpenerimaan_Load(object sender, EventArgs e)
        {
            if (!(MainForm.Instance.role == "Operator Gudang"
               || MainForm.Instance.role == "Manajer"
               || MainForm.Instance.role == "Admin"))
            {
                if (cmbpilihdata.Items.Contains("Kartu Stock Material"))
                {
                    cmbpilihdata.Items.Remove("Kartu Stock Material");
                }
            }
            datecari.Value = DateTime.Now.Date;
            datecaripemakaian.Value = DateTime.Now.Date;
            datematerial.Value = DateTime.Now.Date;
            cmbpilihdata.SelectedIndex = 0;
            infocari = false;

            btncari.Text = "Cari";
            btnprint.Enabled = false;
            guna2Panel4.ResetText();

            paneldata2.Visible = true;
            btncari.Enabled = true;
            tampilpenerimaan();
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
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
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
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
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
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
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
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
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
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
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

        private void tampilmaterial()
        {
            try
            {
                string query = "SELECT * FROM stok_material ORDER BY updated_at DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                dt.Columns["foto"].ColumnName = "Gambar";

                DataTable dtWithImage = new DataTable();
                dtWithImage.Columns.Add("No", typeof(int));
                dtWithImage.Columns.Add("Kode Barang", typeof(string));
                dtWithImage.Columns.Add("Nama Barang", typeof(string));
                dtWithImage.Columns.Add("Spesifikasi", typeof(string));
                dtWithImage.Columns.Add("UoM", typeof(string));
                dtWithImage.Columns.Add("Tipe", typeof(string));
                dtWithImage.Columns.Add("Jumlah Stok", typeof(int));
                dtWithImage.Columns.Add("Min Stok", typeof(int));
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
                    newRow["Spesifikasi"] = row["spesifikasi"];
                    newRow["UoM"] = row["uom"];
                    newRow["Tipe"] = row["type"];
                    newRow["Jumlah Stok"] = row["jumlahStok"];
                    newRow["Min Stok"] = row["min_stok"];
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
                dataGridView1.Columns["No"].FillWeight = 50;
                dataGridView1.RowTemplate.Height = 130;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                dataGridView1.ReadOnly = true;

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
            DateTime tanggal1 = datecari.Value.Date;
            DateTime tanggal2 = datecari.Value.AddDays(1).Date;
            string shift = cbShift.SelectedItem ?.ToString();

            if (string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan pilih shift untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                catch (SqlException ex)
                {
                    MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif." + ex.Message,
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
            DateTime tanggal1 = datecari.Value.Date;
            DateTime tanggal2 = datecari.Value.AddDays(1).Date;
            string shift = cbShift.SelectedItem?.ToString(); 

            if (string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan pilih shift untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            DateTime tanggal1 = datecari.Value.Date;
            DateTime tanggal2 = datecari.Value.AddDays(1).Date;
            string shift = cbShift.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silakan pilih shift untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public void combonama(string keyword = "")
        {
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT * FROM stok_material WHERE namaBarang LIKE @keyword ORDER BY namaBarang ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    isBinding = true;
                    cmbnamamaterial.DataSource = dt;
                    cmbnamamaterial.DisplayMember = "namaBarang";
                    cmbnamamaterial.ValueMember = "kodeBarang";
                    cmbnamamaterial.SelectedIndex = -1;
                    isBinding = false;

                    if (dt.Rows.Count > 0)
                    {
                        cmbnamamaterial.DroppedDown = true;
                        txtcarimaterial.Focus();
                    }
                }
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

        private bool carimaterial()
        {
            if (cmbnamamaterial.SelectedValue == null)
            {
                MessageBox.Show("Silakan pilih Material untuk melakukan pencarian.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string keyword = cmbnamamaterial.SelectedValue.ToString();
            bool found = false;

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM stok_material WHERE kodeBarang = @keyword", conn))
            {
                cmd.Parameters.AddWithValue("@keyword", keyword);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);

                    DataTable dtWithImage = new DataTable();
                    dtWithImage.Columns.Add("No", typeof(int));
                    dtWithImage.Columns.Add("Kode Barang", typeof(string));
                    dtWithImage.Columns.Add("Nama Barang", typeof(string));
                    dtWithImage.Columns.Add("Spesifikasi", typeof(string));
                    dtWithImage.Columns.Add("UoM", typeof(string));
                    dtWithImage.Columns.Add("Tipe", typeof(string));
                    dtWithImage.Columns.Add("Jumlah Stok", typeof(int));
                    dtWithImage.Columns.Add("Min Stok", typeof(int));
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
                        newRow["Spesifikasi"] = row["spesifikasi"];
                        newRow["UoM"] = row["uom"];
                        newRow["Tipe"] = row["type"];
                        newRow["Jumlah Stok"] = row["jumlahStok"];
                        newRow["Min Stok"] = row["min_stok"];
                        newRow["Disimpan"] = row["created_at"];
                        newRow["Diubah"] = row["updated_at"];

                        if (row["foto"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])row["foto"];
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
                        found = true;
                    }

                    dataGridView1.DataSource = dtWithImage;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.Columns["No"].FillWeight = 50;
                    dataGridView1.RowTemplate.Height = 130;
                    dataGridView1.ReadOnly = true;

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

            return found;
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
                }
            }
            else if (pilihan == "Kartu Stock Material")
            {
                if (!infocari)
                {
                    bool hasilCari = carimaterial();
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
                    tampilmaterial();
                    infocari = false;
                    btncari.Text = "Cari";
                    jumlahdata();

                    btnprint.Enabled = false;
                    txtcarimaterial.Clear();
                    cmbnamamaterial.SelectedIndex = -1;
                    cmbnamamaterial.DroppedDown = false;
                    guna2Panel4.ResetText();
                }
            }
        }

        private void jumlahdata()
        {
            int total = dataGridView1.Rows.Count;
            label4.Text = "Jumlah data: " + total.ToString();
            jlhpanel1.Text = "Jumlah data: " + total.ToString();
            lbljumlahdatamaterial.Text = "Jumlah data: " + total.ToString();

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
                paneldata3.Visible = false;

                paneldata2.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilpenerimaan();
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
                paneldata3.Visible = false;

                paneldata2.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilperbaikan();
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
                paneldata3.Visible = false;

                paneldata2.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilpengiriman();
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
                paneldata3.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilwelding();
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
                paneldata3.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilpemakaianmaterial();
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
                paneldata3.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilperbaikan();
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
                paneldata3.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilperbaikan();
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
                paneldata3.Visible = false;

                paneldata1.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilperbaikan();
                jumlahdata();
            }
            else if (pilihan == "Kartu Stock Material")
            {
                //reset dulu
                infocari = false;
                btncari.Text = "Cari";
                btnprint.Enabled = false;
                guna2Panel4.ResetText();
                datecaripemakaian.Checked = false;
                paneldata2.Visible = false;
                paneldata1.Visible = false;

                paneldata3.Visible = true;
                btncari.Enabled = true;
                btnprint.Text = "Print Data";
                tampilmaterial();
                combonama();
                cmbnamamaterial.DropDownStyle = ComboBoxStyle.DropDown;
                cmbnamamaterial.MaxDropDownItems = 20;
                cmbnamamaterial.DropDownHeight = 400;
                jumlahdata();
            }
        }

        private void datecaripemakaian_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 200);
                pickerForm.Text = "Pilih Bulan & Tahun";

                var screenPos = datecaripemakaian.PointToScreen(DrawingPoint.Empty);
                pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datecaripemakaian.Height);

                var cmbBulan = new Guna2ComboBox
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 10,
                    Width = 200,
                    BorderRadius = 6,
                    ForeColor = Color.Black,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };
                string[] bulan = {
                                    "01 - Januari", "02 - Februari", "03 - Maret", "04 - April", "05 - Mei", "06 - Juni",
                                    "07 - Juli", "08 - Agustus", "09 - September", "10 - Oktober", "11 - November", "12 - Desember"
                                };
                cmbBulan.Items.AddRange(bulan);
                cmbBulan.SelectedIndex = datecaripemakaian.Value.Month - 1;

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 55,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = datecaripemakaian.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 110,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    datecaripemakaian.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(cmbBulan);
                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private void datematerial_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 200);
                pickerForm.Text = "Pilih Bulan & Tahun";

                var screenPos = datematerial.PointToScreen(DrawingPoint.Empty);
                pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datematerial.Height);

                var cmbBulan = new Guna2ComboBox
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 10,
                    Width = 200,
                    BorderRadius = 6,
                    ForeColor = Color.Black,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };
                string[] bulan = {
                                    "01 - Januari", "02 - Februari", "03 - Maret", "04 - April", "05 - Mei", "06 - Juni",
                                    "07 - Juli", "08 - Agustus", "09 - September", "10 - Oktober", "11 - November", "12 - Desember"
                                };
                cmbBulan.Items.AddRange(bulan);
                cmbBulan.SelectedIndex = datematerial.Value.Month - 1;

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 55,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = datematerial.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 110,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    datematerial.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(cmbBulan);
                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private void cmbnamamaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBinding) return;

            if (cmbnamamaterial.SelectedIndex == -1 || cmbnamamaterial.SelectedValue == null || cmbnamamaterial.SelectedValue == DBNull.Value)
            {
                return;
            }

            string kodeBarang = cmbnamamaterial.SelectedValue.ToString();

            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT foto, jumlahStok, type, namaBarang FROM stok_material WHERE kodeBarang = @kodeBarang", conn))
                {
                    cmd.Parameters.AddWithValue("@kodeBarang", kodeBarang);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            isProgrammaticChange = true;
                            txtcarimaterial.Text = reader["namaBarang"]?.ToString();
                            isProgrammaticChange = false;
                        }
                        else
                        {
                            
                        }
                    }
                }
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

        private void txtcarimaterial_TextChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange)
                return;

            string keyword = txtcarimaterial.Text.Trim();
            combonama(keyword);
            if (!string.IsNullOrEmpty(keyword))
            {
                cmbnamamaterial.DroppedDown = true;

                Cursor = Cursors.Default;

                txtcarimaterial.Focus();
                txtcarimaterial.SelectionStart = txtcarimaterial.Text.Length;
            }
            else
            {
                cmbnamamaterial.DroppedDown = false;
            }
        }
    }
}

