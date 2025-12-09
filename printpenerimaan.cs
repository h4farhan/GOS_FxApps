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

        private async Task formpenerimaan()
        {
            DateTime tanggal1 = datecari.Value.Date;
            DateTime tanggal2 = datecari.Value.AddDays(1).Date;
            string shift = cbShift.SelectedItem?.ToString();
            string tim = txttim.Text;

            if (string.IsNullOrEmpty(tim))
            {
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();

            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();

                await Task.Delay(150);

                DataTable data = null;
                DataTable data1 = null;
                DataTable data2 = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        var a0 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters
                            .penerimaan_sTableAdapter();
                        data = a0.GetData(tanggal1, tanggal2, shift);

                        var a1 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters
                            .jumlahpenerimaan1TableAdapter();
                        data1 = a1.GetData(tanggal1, tanggal2);

                        var a2 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters
                            .jumlahpenerimaan2TableAdapter();
                        data2 = a2.GetData(tanggal1, tanggal2);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (data != null)
                    {
                        int total = data.AsEnumerable()
                            .Count(row =>
                                row["nomor_rod"] != DBNull.Value &&
                                !row["nomor_rod"].ToString()
                                    .Trim()
                                    .Equals("Total", StringComparison.OrdinalIgnoreCase)
                            );

                        label4.Text = "Jumlah data: " + total;
                    }

                    reportViewer1.Reset();
                    reportViewer1.LocalReport.ReportPath =
                        System.IO.Path.Combine(Application.StartupPath, "penerimaan.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("DataSetPenerimaan", data));

                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("datasetjumlahpenerimaan1", data1));

                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("datasetjumlahpenerimaan2", data2));

                    reportViewer1.LocalReport.SetParameters(new[]
                    {
                    new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
                    new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
                    new ReportParameter("shift", shift),
                    new ReportParameter("tim", tim)
                    });

                    reportViewer1.RefreshReport();
                    Show();
                }
                catch (Exception)
                {
                    loading.Hide();
                    MessageBox.Show(
                        mainform,
                        "Koneksi anda masih terputus, periksa jaringan.",
                        "Kesalahan Jaringan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formperbaikan()
        {
            DateTime tanggal1 = datecari.Value.Date;
            DateTime tanggal2 = datecari.Value.AddDays(1).Date;
            string shift = cbShift.SelectedItem?.ToString();
            string tim = txttim.Text;

            if (string.IsNullOrEmpty(tim))
            {
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            DataTable data = null;
            DataTable data1 = null;
            DataTable data2 = null;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                try
                {
                    try
                    {
                        await Task.Yield(); 

                        data = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters
                            .perbaikan_pTableAdapter()
                            .GetData(tanggal1, tanggal2, shift);

                        data1 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters
                            .jumlahperbaikan1TableAdapter()
                            .GetData(tanggal1, tanggal2);

                        data2 = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters
                            .jumlahperbaikan2TableAdapter()
                            .GetData(tanggal1, tanggal2);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (data != null)
                    {
                        int total = data.AsEnumerable()
                            .Count(row =>
                                row["nomor_rod"] != DBNull.Value &&
                                !row["nomor_rod"].ToString()
                                    .Trim()
                                    .Equals("Total", StringComparison.OrdinalIgnoreCase)
                            );

                        label4.Text = "Jumlah data: " + total;
                    }

                    reportViewer1.Reset();
                    reportViewer1.LocalReport.ReportPath =
                        System.IO.Path.Combine(Application.StartupPath, "Perbaikan.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetPerbaikan", data));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetjumlahperbaikan1", data1));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetjumlahperbaikan2", data2));

                    reportViewer1.LocalReport.SetParameters(new[]
                    {
                new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
                new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
                new ReportParameter("shift", shift),
                new ReportParameter("tim", tim)
            });

                    reportViewer1.RefreshReport();
                    Show();
                }
                catch (Exception)
                {
                    loading.Hide();
                    MessageBox.Show(
                        mainform,
                        "Koneksi anda masih terputus, periksa jaringan.",
                        "Kesalahan Jaringan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formpengiriman()
        {
            DateTime tanggal1 = datecari.Value.Date;
            DateTime tanggal2 = datecari.Value.AddDays(1).Date;
            string shift = cbShift.SelectedItem?.ToString();
            string tim = txttim.Text;

            if (string.IsNullOrEmpty(tim))
            {
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            DataTable data = null;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                try
                {
                    try
                    {
                        await Task.Yield();

                        data = new GOS_FxApps.DataSet.PengirimanFormTableAdapters
                            .pengirimanTableAdapter()
                            .GetData(tanggal1, tanggal2, shift);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

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
                    reportViewer1.LocalReport.ReportPath =
                        System.IO.Path.Combine(Application.StartupPath, "Pengiriman.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetPengiriman", data));

                    reportViewer1.LocalReport.SetParameters(new[]
                    {
                new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
                new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
                new ReportParameter("shift", shift),
                new ReportParameter("tim", tim)
            });

                    reportViewer1.RefreshReport();
                    Show();
                }
                catch (Exception)
                {
                    loading.Hide();
                    MessageBox.Show(
                        mainform,
                        "Koneksi anda masih terputus, periksa jaringan.",
                        "Kesalahan Jaringan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formbukti()
        {
            DateTime tanggal = datecaribukti.Value.Date;
            string shift = shiftbukti.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(shift))
            {
                MessageBox.Show("Silahkan Masukkan Tim Terlebih Dahulu",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                DataTable data = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        data = new GOS_FxApps.DataSet.buktiTableAdapters
                            .sp_buktiperubahanTableAdapter()
                            .GetData(tanggal, shift);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (data != null)
                    {
                        if (data.Columns.Contains("nomor_rod"))
                        {
                            int total = data.AsEnumerable()
                                .Count(row =>
                                    !row.IsNull("nomor_rod") &&
                                    !row["nomor_rod"].ToString()
                                        .Trim()
                                        .Equals("Total", StringComparison.OrdinalIgnoreCase)
                                );

                            lbljumlahbukti.Text = "Jumlah data: " + total;
                        }
                        else
                        {
                            lbljumlahbukti.Text = "Kolom nomor_rod tidak ditemukan!";
                        }

                        reportViewer1.Reset();
                        reportViewer1.LocalReport.ReportPath =
                            System.IO.Path.Combine(Application.StartupPath, "buktiperubahan.rdlc");

                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetbukti", data));

                        reportViewer1.LocalReport.SetParameters(new[]
                        {
                            new ReportParameter("TanggalAwal", tanggal.ToString("yyyy-MM-dd")),
                            new ReportParameter("Shift", shift)
                        });

                        reportViewer1.RefreshReport();
                        Show();
                    }
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formPemakaian()
        {
            DateTime tanggalmulai = tanggalMulai.Value.Date;
            DateTime tanggalakhir = tanggalAkhir.Value.Date;

            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();

                await Task.Delay(150);

                DataTable dataPemakaian = null;
                DataTable dataPerbaikan = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        var adapter1 = new GOS_FxApps.DataSet.laporanpemakaianTableAdapters
                            .sp_LaporanPemakaianMaterialTableAdapter();
                        dataPemakaian = adapter1.GetData(tanggalmulai, tanggalakhir);

                        var adapter2 = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters
                            .sp_LaporanPerbaikanTableAdapter();
                        dataPerbaikan = adapter2.GetData(tanggalmulai, tanggalakhir);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    reportViewer1.Reset();
                    reportViewer1.LocalReport.ReportPath =
                        System.IO.Path.Combine(Application.StartupPath, "laporanPemakaian_31.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("datasetlaporanpemakaian", dataPemakaian));
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("datasetjumlahperbaikan", dataPerbaikan));

                    reportViewer1.LocalReport.SetParameters(new[]
                    {
                new ReportParameter("tanggalMulai", tanggalmulai.ToString("yyyy-MM-dd")),
                new ReportParameter("tanggalAkhir", tanggalakhir.ToString("yyyy-MM-dd"))
            });

                    reportViewer1.RefreshReport();
                    Show();
                }
                catch (Exception)
                {
                    loading.Hide();
                    MessageBox.Show(
                        mainform,
                        "Koneksi anda masih terputus, periksa jaringan.",
                        "Kesalahan Jaringan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formwelding()
        {
            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                DataTable data = null;
                DataTable datafirst = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        DateTime tanggalmulai = tanggalMulai.Value.Date;
                        DateTime tanggalakhir = tanggalAkhir.Value.Date;

                        data = new GOS_FxApps.DataSet.rb_stokTableAdapters
                            .sp_Rb_StokTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datafirst = new GOS_FxApps.DataSet.rb_stokTableAdapters
                            .Rb_StokTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (data != null && datafirst != null)
                    {
                        reportViewer1.Reset();
                        reportViewer1.LocalReport.ReportPath =
                            System.IO.Path.Combine(Application.StartupPath, "Rb_Stok.rdlc");

                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetrbstok", data));
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetfirstrbstok", datafirst));

                        reportViewer1.LocalReport.SetParameters(new[]
                        {
                    new ReportParameter("tanggalMulai", tanggalMulai.Value.ToString("yyyy-MM-dd")),
                    new ReportParameter("tanggalAkhir", tanggalAkhir.Value.ToString("yyyy-MM-dd"))
                });

                        reportViewer1.RefreshReport();
                    }
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formweldinghari()
        {
            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                DataTable data = null;
                DataTable datafirst = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        DateTime tanggalmulai = tanggalMulai.Value.Date;
                        DateTime tanggalakhir = tanggalAkhir.Value.Date;

                        data = new GOS_FxApps.DataSet.rb_stokTableAdapters
                            .sp_Rb_StokhariTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datafirst = new GOS_FxApps.DataSet.rb_stokTableAdapters
                            .Rb_StokTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (data != null && datafirst != null)
                    {
                        reportViewer1.Reset();
                        reportViewer1.LocalReport.ReportPath =
                            System.IO.Path.Combine(Application.StartupPath, "Rb_Stokhari.rdlc");

                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetrbstokhari", data));
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetstokawal", datafirst));

                        reportViewer1.LocalReport.SetParameters(new[]
                        {
                    new ReportParameter("tanggalMulai", tanggalMulai.Value.ToString("yyyy-MM-dd")),
                    new ReportParameter("tanggalAkhir", tanggalAkhir.Value.ToString("yyyy-MM-dd"))
                });

                        reportViewer1.RefreshReport();
                    }
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formlaporanharian()
        {
            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                DataTable data = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        DateTime tanggalmulai = tanggalMulai.Value.Date;
                        DateTime tanggalakhir = tanggalAkhir.Value.Date;

                        data = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters
                            .sp_Laporan_HarianTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (data != null)
                    {
                        if (data.Columns.Contains("nomor_rod"))
                        {
                            int total = data.AsEnumerable()
                                .Count(row =>
                                    !row.IsNull("nomor_rod") &&
                                    !row["nomor_rod"].ToString()
                                        .Trim()
                                        .Equals("Total", StringComparison.OrdinalIgnoreCase)
                                );

                            lbljumlahsummary.Text = "Jumlah data: " + total;
                        }
                        else
                        {
                            lbljumlahsummary.Text = "Kolom nomor_rod tidak ditemukan!";
                        }

                        reportViewer1.Reset();
                        reportViewer1.LocalReport.ReportPath =
                            System.IO.Path.Combine(Application.StartupPath, "produksiharian.rdlc");

                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetharian", data));

                        reportViewer1.LocalReport.SetParameters(new[]
                        {
                    new ReportParameter("tanggalMulai", tanggalMulai.Value.ToString("yyyy-MM-dd")),
                    new ReportParameter("tanggalAkhir", tanggalAkhir.Value.ToString("yyyy-MM-dd"))
                });

                        reportViewer1.RefreshReport();
                    }
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formactual()
        {
            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                DataTable dataactual = null;
                DataTable dataperbaikan = null;
                DataTable datapenerimaan = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        DateTime tanggalmulai = tanggalMulai.Value.Date;
                        DateTime tanggalakhir = tanggalAkhir.Value.Date;

                        dataactual = new GOS_FxApps.DataSet.actualTableAdapters
                            .sp_LaporanActualTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        dataperbaikan = new GOS_FxApps.DataSet.actualTableAdapters
                            .sp_LaporanShiftPerbaikanTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datapenerimaan = new GOS_FxApps.DataSet.actualTableAdapters
                            .sp_LaporanShiftPenerimaanTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    reportViewer1.Reset();
                    reportViewer1.LocalReport.ReportPath =
                        System.IO.Path.Combine(Application.StartupPath, "actual_31.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetactual", dataactual));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetshiftperbaikan", dataperbaikan));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetshiftpenerimaan", datapenerimaan));

                    reportViewer1.LocalReport.SetParameters(new[]
                    {
                new ReportParameter("tanggalMulai", tanggalMulai.Value.ToString("yyyy-MM-dd")),
                new ReportParameter("tanggalAkhir", tanggalAkhir.Value.ToString("yyyy-MM-dd"))
            });

                    reportViewer1.RefreshReport();
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formmaterial()
        {
            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                DataTable data = null;
                DataTable data2 = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        DateTime tanggalmulai = tanggalMulaimaterial.Value.Date;
                        DateTime tanggalakhir = tanggalAkhirmaterial.Value.Date;
                        string kode = cmbnamamaterial.SelectedValue.ToString();

                        data = new GOS_FxApps.DataSet.materialTableAdapters
                            .cardMaterialTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir, kode);

                        data2 = new GOS_FxApps.DataSet.materialTableAdapters
                            .sp_dataCardMaterialTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir, kode);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    reportViewer1.Reset();
                    reportViewer1.LocalReport.ReportPath =
                        System.IO.Path.Combine(Application.StartupPath, "cardmaterial.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("cardmaterial", data));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dataCardMaterial", data2));

                    reportViewer1.LocalReport.SetParameters(new[]
                    {
                new ReportParameter("tanggalMulai", tanggalMulaimaterial.Value.ToString("yyyy-MM-dd")),
                new ReportParameter("tanggalAkhir", tanggalAkhirmaterial.Value.ToString("yyyy-MM-dd")),
                new ReportParameter("kode", cmbnamamaterial.SelectedValue.ToString())
            });

                    reportViewer1.RefreshReport();
                    Show();
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formconsumption()
        {
            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                DataTable datamaterial = null;
                DataTable dataconsumable = null;
                DataTable datasafety = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        DateTime tanggalmulai = tanggalMulai.Value.Date;
                        DateTime tanggalakhir = tanggalAkhir.Value.Date;

                        datamaterial = new GOS_FxApps.DataSet.buktiTableAdapters
                            .sp_consumptionmaterialcostTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        dataconsumable = new GOS_FxApps.DataSet.buktiTableAdapters
                            .sp_consumptionconsumablecostTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datasafety = new GOS_FxApps.DataSet.buktiTableAdapters
                            .sp_consumptionsafetycostTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    reportViewer1.Reset();
                    reportViewer1.LocalReport.ReportPath =
                        System.IO.Path.Combine(Application.StartupPath, "consumption31.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("consumptionmaterial", datamaterial));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("consumptionconsumable", dataconsumable));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("consumptionsafety", datasafety));

                    reportViewer1.LocalReport.SetParameters(new[]
                    {
                new ReportParameter("tanggalMulai", tanggalMulai.Value.ToString("yyyy-MM-dd")),
                new ReportParameter("tanggalAkhir", tanggalAkhir.Value.ToString("yyyy-MM-dd"))
            });

                    reportViewer1.RefreshReport();
                    Show();
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async Task formkondisi()
        {
            Form mainform = this.FindForm()?.ParentForm ?? this.FindForm();
            mainform.Enabled = false;

            using (FormLoading loading = new FormLoading())
            {
                loading.Show(mainform);
                loading.Refresh();
                await Task.Delay(150);

                DataTable datakondisi = null;
                DataTable dataperbaikan = null;
                DataTable datapenerimaan = null;
                DataTable databutt = null;
                DataTable dataman = null;
                DataTable datareject = null;
                DataTable datastokreguler = null;
                DataTable datastok = null;
                DataTable datastokrepair = null;

                try
                {
                    try
                    {
                        await Task.Yield();

                        DateTime tanggalmulai = tanggalMulai.Value.Date;
                        DateTime tanggalakhir = tanggalAkhir.Value.Date;

                        datakondisi = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanKondisiPerbaikanTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        dataperbaikan = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanShiftPerbaikanTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datapenerimaan = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanShiftPenerimaanTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        databutt = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanKondisiButtRatioTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        dataman = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanKondisiManPowerTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datareject = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanRejectBATableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datastokreguler = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanStokRegulerTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datastok = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanKondisiStokTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);

                        datastokrepair = new GOS_FxApps.DataSet.kondisiTableAdapters
                            .sp_LaporanKondisiStokRepairTableAdapter()
                            .GetData(tanggalmulai, tanggalakhir);
                    }
                    catch (SqlException)
                    {
                        loading.Hide();
                        MessageBox.Show(
                            mainform,
                            "Koneksi anda masih terputus, periksa jaringan.",
                            "Kesalahan Jaringan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    reportViewer1.Reset();
                    reportViewer1.LocalReport.ReportPath =
                        System.IO.Path.Combine(Application.StartupPath, "kondisi_31.rdlc");

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetkondisiperbaikan", datakondisi));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetkondisishiftperbaikan", dataperbaikan));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetkondisishiftpenerimaan", datapenerimaan));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetkondisibuttratio", databutt));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetkondisimanpower", dataman));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetkondisirejectba", datareject));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dastasetkondisistokreguler", datastokreguler));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetkondisistok", datastok));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetkondisistokrepair", datastokrepair));

                    reportViewer1.LocalReport.SetParameters(new[]
                    {
                new ReportParameter("tanggalMulai", tanggalMulai.Value.ToString("yyyy-MM-dd")),
                new ReportParameter("tanggalAkhir", tanggalAkhir.Value.ToString("yyyy-MM-dd"))
            });

                    reportViewer1.RefreshReport();
                }
                finally
                {
                    loading.Close();
                    mainform.Enabled = true;
                    mainform.Activate();
                }
            }
        }

        private async void guna2Button2_Click(object sender, EventArgs e) 
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

                await formpenerimaan();
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

                await formperbaikan();
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

                await formpengiriman();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Welding Pieces (Detail Shift)")
            {
                if (tanggalMulai.Value.Date > tanggalAkhir.Value.Date)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await formwelding();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Welding Pieces (Rekap Harian)")
            {
                if (tanggalMulai.Value.Date > tanggalAkhir.Value.Date)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await formweldinghari();
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

                await formPemakaian();
                btnreset.Enabled = true;
            }
            else if (pilihan == "Summary Data for Anode ROD Repair")
            {
                if (tanggalMulai.Value.Date > tanggalAkhir.Value.Date)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await formlaporanharian();
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

                await formactual();
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

                await formkondisi();
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

                await formmaterial();
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

                await formconsumption();
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

                await formbukti();
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
                lbljumlahsummary.Visible = false;
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
                lbljumlahsummary.Visible = false;
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
                lbljumlahsummary.Visible = false;
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
                lbljumlahsummary.Visible = true;
                lbljumlahsummary.Text = "Jumlah data: 0";
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
                lbljumlahsummary.Visible = false;
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
                lbljumlahsummary.Visible = false;
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
                lbljumlahsummary.Visible = false;
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
                lbljumlahsummary.Visible = false;
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
            lbljumlahbukti.Text = "Jumlah data: 0";
        }
    }
}

