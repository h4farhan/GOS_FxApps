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
using System.Web.UI.WebControls.WebParts;

namespace GOS_FxApps
{
    public partial class printpenerimaan : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        private bool isBinding = false;
        bool isProgrammaticChange = false;

        public printpenerimaan()
        {
            InitializeComponent();
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

                var adapter1 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.jumlahpenerimaan1TableAdapter();
                GOS_FxApps.DataSet.PenerimaanForm.jumlahpenerimaan1DataTable data1 = adapter1.GetData(tanggal1, tanggal2);

                var adapter2 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.jumlahpenerimaan2TableAdapter();
                GOS_FxApps.DataSet.PenerimaanForm.jumlahpenerimaan2DataTable data2 = adapter2.GetData(tanggal1, tanggal2);

            int total = data.AsEnumerable()
        .Count(row =>
            row["nomor_rod"] != DBNull.Value &&
            !row["nomor_rod"].ToString()
                .Equals("Total", StringComparison.OrdinalIgnoreCase)
        );
            label4.Text = "Jumlah data: " + total;

            reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "penerimaan.rdlc");

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSetPenerimaan", (DataTable)data));
            reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("datasetjumlahpenerimaan1", (DataTable)data1));
            reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("datasetjumlahpenerimaan2", (DataTable)data2));

            ReportParameter[] parameters = new ReportParameter[]
                {
            new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
            new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
            new ReportParameter("shift", shift),
            new ReportParameter("tim", tim)
                };
                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
        }

        private void formPemakaian()
        {
            DateTime tanggalmulai = tanggalMulai.Value.Date;
            DateTime tanggalakhir = tanggalAkhir.Value.Date;

            string namaFileRDLC = "laporanPemakaian_31.rdlc"; ;

            var adapter = new GOS_FxApps.DataSet.laporanpemakaianTableAdapters.sp_LaporanPemakaianMaterialTableAdapter();
            GOS_FxApps.DataSet.laporanpemakaian.sp_LaporanPemakaianMaterialDataTable data = adapter.GetData(tanggalmulai, tanggalakhir);

            var adapterperbaikan = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.sp_LaporanPerbaikanTableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.sp_LaporanPerbaikanDataTable dataperbaikan = adapterperbaikan.GetData(tanggalmulai, tanggalakhir);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, namaFileRDLC);

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetlaporanpemakaian", (DataTable)data));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetjumlahperbaikan", (DataTable)dataperbaikan));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd"))
            };

            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
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

            var adapter1 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.jumlahperbaikan1TableAdapter();
            GOS_FxApps.DataSet.PenerimaanForm.jumlahperbaikan1DataTable data1 = adapter1.GetData(tanggal1, tanggal2);

            var adapter2 = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.jumlahperbaikan2TableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.jumlahperbaikan2DataTable data2 = adapter2.GetData(tanggal1, tanggal2);

            int total = data.AsEnumerable()
            .Count(row =>
                row["nomor_rod"] != DBNull.Value &&
                !row["nomor_rod"].ToString()
                    .Equals("Total", StringComparison.OrdinalIgnoreCase)
            );
            label4.Text = "Jumlah data: " + total;

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Perbaikan.rdlc");

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSetPerbaikan", (DataTable)data));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetjumlahperbaikan1", (DataTable)data1));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetjumlahperbaikan2", (DataTable)data2));

            ReportParameter[] parameters = new ReportParameter[]
            {
            new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
            new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
            new ReportParameter("shift", shift),
            new ReportParameter("tim", tim)
            };
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
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

            int jumlahAsli = data.AsEnumerable()
                     .Count(r => !r.IsNull("nomor_rod") &&
                                 !string.IsNullOrWhiteSpace(r["nomor_rod"].ToString()));

            label4.Text = "Jumlah data: " + jumlahAsli;

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Pengiriman.rdlc");

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetPengiriman", (DataTable)data));

            ReportParameter[] parameters = new ReportParameter[]
            {
            new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
            new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
            new ReportParameter("shift", shift),
            new ReportParameter("tim", tim)
            };
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
        }

        private void formwelding()
        {
            DateTime tanggalmulai = tanggalMulai.Value.Date;
            DateTime tanggalakhir = tanggalAkhir.Value.Date;

            var adapter = new GOS_FxApps.DataSet.rb_stokTableAdapters.sp_Rb_StokTableAdapter();
            GOS_FxApps.DataSet.rb_stok.sp_Rb_StokDataTable data = adapter.GetData(tanggalmulai, tanggalakhir);

            var adapterfirst = new GOS_FxApps.DataSet.rb_stokTableAdapters.Rb_StokTableAdapter();
            GOS_FxApps.DataSet.rb_stok.Rb_StokDataTable datafirst = adapterfirst.GetData(tanggalmulai, tanggalakhir);
            
            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Rb_Stok.rdlc");

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetrbstok", (DataTable)data));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetfirstrbstok", (DataTable)datafirst));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd"))
            };

            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
        }

        private void formweldinghari()
        {
            DateTime tanggalmulai = tanggalMulai.Value.Date;
            DateTime tanggalakhir = tanggalAkhir.Value.Date;

            var adapter = new GOS_FxApps.DataSet.rb_stokTableAdapters.sp_Rb_StokhariTableAdapter();
            GOS_FxApps.DataSet.rb_stok.sp_Rb_StokhariDataTable data = adapter.GetData(tanggalmulai, tanggalakhir);

            var adapterfirst = new GOS_FxApps.DataSet.rb_stokTableAdapters.Rb_StokTableAdapter();
            GOS_FxApps.DataSet.rb_stok.Rb_StokDataTable datafirst = adapterfirst.GetData(tanggalmulai, tanggalakhir);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Rb_Stokhari.rdlc");

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetrbstokhari", (DataTable)data));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetstokawal", (DataTable)datafirst));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd"))
            };

            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
        }

        private void formlaporanharian()
        {
            DateTime tanggalmulai = tanggalMulai.Value.Date;
            DateTime tanggalakhir = tanggalAkhir.Value.Date;

            var adapter = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.sp_Laporan_HarianTableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.sp_Laporan_HarianDataTable data = adapter.GetData(tanggalmulai, tanggalakhir);

            int total = data.Rows.Count;
            lbljumlahsummary.Text = "Jumlah data: " + total;

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "produksiharian.rdlc");

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetharian", (DataTable)data));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd"))
            };

            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
        }

        private void formactual()
        {
            DateTime tanggalmulai = tanggalMulai.Value.Date;
            DateTime tanggalakhir = tanggalAkhir.Value.Date;

            string namaFileRDLC = "actual_31.rdlc";

            var adapteractual = new GOS_FxApps.DataSet.actualTableAdapters.sp_LaporanActualTableAdapter();
            GOS_FxApps.DataSet.actual.sp_LaporanActualDataTable dataactual = adapteractual.GetData(tanggalmulai, tanggalakhir);

            var adapterperbaikan = new GOS_FxApps.DataSet.actualTableAdapters.sp_LaporanShiftPerbaikanTableAdapter();
            GOS_FxApps.DataSet.actual.sp_LaporanShiftPerbaikanDataTable dataperbaikan = adapterperbaikan.GetData(tanggalmulai, tanggalakhir);

            var adapterpenerimaan = new GOS_FxApps.DataSet.actualTableAdapters.sp_LaporanShiftPenerimaanTableAdapter();
            GOS_FxApps.DataSet.actual.sp_LaporanShiftPenerimaanDataTable datapenerimaan = adapterpenerimaan.GetData(tanggalmulai, tanggalakhir);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, namaFileRDLC);

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetactual", (DataTable)dataactual));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetshiftperbaikan", (DataTable)dataperbaikan));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetshiftpenerimaan", (DataTable)datapenerimaan));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd"))
            };

            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
        }

        private void formkondisi()
        {
            DateTime tanggalmulai = tanggalMulai.Value.Date;
            DateTime tanggalakhir = tanggalAkhir.Value.Date;

            string namaFileRDLC = "kondisi_31.rdlc";

            var adapterkondisi = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiPerbaikanTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiPerbaikanDataTable datakondisi = adapterkondisi.GetData(tanggalmulai, tanggalakhir);

            var adapterperbaikan = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanShiftPerbaikanTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanShiftPerbaikanDataTable dataperbaikan = adapterperbaikan.GetData(tanggalmulai, tanggalakhir);

            var adapterpenerimaan = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanShiftPenerimaanTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanShiftPenerimaanDataTable datapenerimaan = adapterpenerimaan.GetData(tanggalmulai, tanggalakhir);

            var adapterbutt = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiButtRatioTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiButtRatioDataTable databutt = adapterbutt.GetData(tanggalmulai, tanggalakhir);

            var adapterman = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiManPowerTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiManPowerDataTable dataman = adapterman.GetData(tanggalmulai, tanggalakhir);

            var adapterreject = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanRejectBATableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanRejectBADataTable datareject = adapterreject.GetData(tanggalmulai, tanggalakhir);

            var adapterstokreguler = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanStokRegulerTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanStokRegulerDataTable datastokreguler = adapterstokreguler.GetData(tanggalmulai, tanggalakhir);

            var adapterstok = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiStokTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiStokDataTable datastok = adapterstok.GetData(tanggalmulai, tanggalakhir);

            var adapterstokrepair = new GOS_FxApps.DataSet.kondisiTableAdapters.sp_LaporanKondisiStokRepairTableAdapter();
            GOS_FxApps.DataSet.kondisi.sp_LaporanKondisiStokRepairDataTable datastokrepair = adapterstokrepair.GetData(tanggalmulai, tanggalakhir);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, namaFileRDLC);

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisiperbaikan", (DataTable)datakondisi));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisishiftperbaikan", (DataTable)dataperbaikan));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisishiftpenerimaan", (DataTable)datapenerimaan));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisibuttratio", (DataTable)databutt));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisimanpower", (DataTable)dataman));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisirejectba", (DataTable)datareject));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("dastasetkondisistokreguler", (DataTable)datastokreguler));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisistok", (DataTable)datastok));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetkondisistokrepair", (DataTable)datastokrepair));

            ReportParameter[] parameters = new ReportParameter[]
            {
         new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd"))
            };

            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
            Show();
        }

        private void formmaterial()
        {
            DateTime tanggalmulai = tanggalMulaimaterial.Value.Date;
            DateTime tanggalakhir = tanggalAkhirmaterial.Value.Date;
            string kode = cmbnamamaterial.SelectedValue.ToString();

            var adapter = new GOS_FxApps.DataSet.materialTableAdapters.cardMaterialTableAdapter();
            GOS_FxApps.DataSet.material.cardMaterialDataTable data = adapter.GetData(tanggalmulai, tanggalakhir, kode);

            var adapter2 = new GOS_FxApps.DataSet.materialTableAdapters.sp_dataCardMaterialTableAdapter();
            GOS_FxApps.DataSet.material.sp_dataCardMaterialDataTable data2 = adapter2.GetData(tanggalmulai, tanggalakhir, kode);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "cardmaterial.rdlc");

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("cardmaterial", (DataTable)data));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("dataCardMaterial", (DataTable)data2));

            ReportParameter[] parameters = new ReportParameter[]
            {
            new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd")),
            new ReportParameter("kode", kode)
            };
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();

            Show();
        }

        private void formconsumption()
        {
            DateTime tanggalmulai = tanggalMulai.Value.Date;
            DateTime tanggalakhir = tanggalAkhir.Value.Date;

            string namaFileRDLC = "consumption31.rdlc";

            var adaptermaterial = new GOS_FxApps.DataSet.buktiTableAdapters.sp_consumptionmaterialcostTableAdapter();
            GOS_FxApps.DataSet.bukti.sp_consumptionmaterialcostDataTable datamaterial = adaptermaterial.GetData(tanggalmulai, tanggalakhir);

            var adapterconsumable = new GOS_FxApps.DataSet.buktiTableAdapters.sp_consumptionconsumablecostTableAdapter();
            GOS_FxApps.DataSet.bukti.sp_consumptionconsumablecostDataTable dataconsumable = adapterconsumable.GetData(tanggalmulai, tanggalakhir);

            var adaptersafety = new GOS_FxApps.DataSet.buktiTableAdapters.sp_consumptionsafetycostTableAdapter();
            GOS_FxApps.DataSet.bukti.sp_consumptionsafetycostDataTable datasafety = adaptersafety.GetData(tanggalmulai, tanggalakhir);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, namaFileRDLC);

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("consumptionmaterial", (DataTable)datamaterial));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("consumptionconsumable", (DataTable)dataconsumable));

            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("consumptionsafety", (DataTable)datasafety));

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
        new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd"))
            };

            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
            Show();
        }

        private void formbukti()
        {
            DateTime tanggal = datecaribukti.Value.Date;
            string shift = shiftbukti.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var adapter = new GOS_FxApps.DataSet.buktiTableAdapters.sp_buktiperubahanTableAdapter();
            GOS_FxApps.DataSet.bukti.sp_buktiperubahanDataTable data = adapter.GetData(tanggal, shift);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "buktiperubahan.rdlc");

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetbukti", (DataTable)data));

            ReportParameter[] parameters = new ReportParameter[]
            {
            new ReportParameter("TanggalAwal", tanggal.ToString("yyyy-MM-dd")),
            new ReportParameter("Shift", shift),
            };
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();

            Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e) 
        {
            if (cmbpilihdata.SelectedItem == null)
                return;

            string pilihan = cmbpilihdata.SelectedItem.ToString();

            if (pilihan == "Penerimaan")
            {
                string shift = cbShift.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(shift) || txttim.Text == "")
                {
                    MessageBox.Show("Silakan Masukkan Tanggal, Shift dan Tim untuk melakukan Print Data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formpenerimaan();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Perbaikan")
            {
                string shift = cbShift.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(shift) || txttim.Text == "")
                {
                    MessageBox.Show("Silakan Masukkan Tanggal, Shift dan Tim untuk melakukan Print Data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formperbaikan();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Pengiriman")
            {
                string shift = cbShift.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(shift) || txttim.Text == "")
                {
                    MessageBox.Show("Silakan Masukkan Tanggal, Shift dan Tim untuk melakukan Print Data.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formpengiriman();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Welding Pieces (Detail Shift)")
            {
                if (tanggalMulai.Value.Date > tanggalAkhir.Value.Date)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formwelding();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Welding Pieces (Rekap Harian)")
            {
                if (tanggalMulai.Value.Date > tanggalAkhir.Value.Date)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formweldinghari();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Hasil Produksi & Pemakaian Material")
            {
                DateTime mulai = tanggalMulai.Value.Date;
                DateTime akhir = tanggalAkhir.Value.Date;

                int selisih = (akhir - mulai).Days + 1;

                if (selisih > 31)
                {
                    MessageBox.Show("Rentang pencarian maksimal 31 hari!",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                if (mulai > akhir)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                formPemakaian();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Summary Data for Anode ROD Repair")
            {
                if (tanggalMulai.Value.Date > tanggalAkhir.Value.Date)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formlaporanharian();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Actual Quantity for Repaired ROD Assy")
            {
                DateTime mulai = tanggalMulai.Value.Date;
                DateTime akhir = tanggalAkhir.Value.Date;

                int selisih = (akhir - mulai).Days + 1;

                if (selisih > 31)
                {
                    MessageBox.Show("Rentang pencarian maksimal 31 hari!",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                if (mulai > akhir)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                formactual();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Kondisi ROD Reject di Rod Repair Shop")
            {
                DateTime mulai = tanggalMulai.Value.Date;
                DateTime akhir = tanggalAkhir.Value.Date;

                int selisih = (akhir - mulai).Days + 1;

                if (selisih > 31)
                {
                    MessageBox.Show("Rentang pencarian maksimal 31 hari!",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                if (mulai > akhir)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                formkondisi();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Kartu Stock Material")
            {
                DateTime mulai = tanggalMulaimaterial.Value.Date;
                DateTime akhir = tanggalAkhirmaterial.Value.Date;

                if (cmbnamamaterial.SelectedValue == null)
                {
                    MessageBox.Show("Silakan pilih Material untuk melakukan pencarian.",
                                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (mulai > akhir)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                formmaterial();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Actual Consumption Of Material & Part")
            {
                DateTime mulai = tanggalMulai.Value.Date;
                DateTime akhir = tanggalAkhir.Value.Date;

                int selisih = (akhir - mulai).Days + 1;

                if (selisih > 31)
                {
                    MessageBox.Show("Rentang pencarian maksimal 31 hari!",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                if (mulai > akhir)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                formconsumption();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Bukti Perubahan")
            {
                string shift = shiftbukti.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(shift))
                {
                    MessageBox.Show("Silakan pilih shift untuk melakukan pencarian.",
                                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formbukti();
                btnreset.Enabled = true;
            }
        }

        private void printpenerimaan_Load(object sender, EventArgs e)
        {
            datecari.Value = DateTime.Now.Date;
            tanggalMulai.Value = DateTime.Now.Date;
            tanggalAkhir.Value = DateTime.Now.Date;
            tanggalMulaimaterial.Value = DateTime.Now.Date;
            tanggalAkhirmaterial.Value = DateTime.Now.Date;
            datecaribukti.Value = DateTime.Now.Date;
            cmbpilihdata.SelectedIndex = 0;

            btnreset.Enabled = false;
            guna2Panel4.ResetText();

            paneldata2.Visible = true;
            this.reportViewer1.RefreshReport();
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

        private void cmbpilihdata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedItem == null)
                return;

            string pilihan = cmbpilihdata.SelectedItem.ToString();

            if (pilihan == "Penerimaan")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                panelsummary.Visible = false;
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                paneldata2.Visible = true;
                
            }
            else if (pilihan == "Perbaikan")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                panelsummary.Visible = false;
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                paneldata2.Visible = true;
                
            }
            else if (pilihan == "Pengiriman")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                panelsummary.Visible = false;
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                paneldata2.Visible = true;
                
            }
            else if (pilihan == "Welding Pieces (Detail Shift)")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
    
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                panelsummary.Visible = true;

            }
            else if (pilihan == "Welding Pieces (Rekap Harian)")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                panelsummary.Visible = true;

            }
            else if (pilihan == "Hasil Produksi & Pemakaian Material")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                panelsummary.Visible = true;

            }
            else if (pilihan == "Summary Data for Anode ROD Repair")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                panelsummary.Visible = true;
            }
            else if (pilihan == "Actual Quantity for Repaired ROD Assy")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                panelsummary.Visible = true;

            }
            else if (pilihan == "Kondisi ROD Reject di Rod Repair Shop")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                panelsummary.Visible = true;

            }
            else if (pilihan == "Kartu Stock Material")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                panelsummary.Visible = false;
                paneldata2.Visible = false;
                
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                paneldata3.Visible = true;
                combonama();
                cmbnamamaterial.DropDownStyle = ComboBoxStyle.DropDown;
                cmbnamamaterial.MaxDropDownItems = 20;
                cmbnamamaterial.DropDownHeight = 400;
                
            }
            else if (pilihan == "Actual Consumption Of Material & Part")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                
                paneldata2.Visible = false;
                paneldata3.Visible = false;
                panelbukti.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                panelsummary.Visible = true;

            }
            else if (pilihan == "Bukti Perubahan")
            {
                //reset dulu
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();

                btnreset.Enabled = false;
                guna2Panel4.ResetText();
                panelsummary.Visible = false;
                paneldata2.Visible = false;
                
                paneldata3.Visible = false;
                label4.Text = "Jumlah data: 0";
                lbljumlahsummary.Text = "Jumlah data: 0";
                lbljumlahdatamaterial.Text = "Jumlah data: 0";
                lbljumlahbukti.Text = "Jumlah data: 0";

                panelbukti.Visible = true;
                
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

        private void btnreset_Click(object sender, EventArgs e)
        {
            reportViewer1.Reset();
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.RefreshReport();
            btnreset.Enabled = false;
            txtcarimaterial.Text = "";
            label4.Text = "Jumlah data: 0";
            lbljumlahsummary.Text = "Jumlah data: 0";
            lbljumlahdatamaterial.Text = "Jumlah data: 0";
            lbljumlahbukti.Text = "Jumlah data: 0";
        }
    }
}

