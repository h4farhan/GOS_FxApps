using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Guna.UI2.WinForms;
using DrawingPoint = System.Drawing.Point;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.NetworkInformation;

namespace GOS_FxApps
{
    public partial class laporanpersediaan : Form
    {

        public laporanpersediaan()
        {
            InitializeComponent();
        }

        private async Task StartNetworkMonitorAsync(CancellationTokenSource cts)
        {
            int failCount = 0;

            while (!cts.IsCancellationRequested)
            {
                bool ok = await IsNetworkOk();

                if (!ok)
                {
                    failCount++;
                    if (failCount >= 3)
                    {
                        cts.Cancel(); 
                        return;
                    }
                }
                else
                {
                    failCount = 0;
                }

                await Task.Delay(2000);
            }
        }

        private async Task<bool> IsNetworkOk()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Koneksi.GetConnectionString()))
                {
                    var timeoutTask = Task.Delay(3000);
                    var openTask = conn.OpenAsync();

                    var finished = await Task.WhenAny(openTask, timeoutTask);

                    if (finished == timeoutTask)
                        return false;

                    return conn.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task<DataTable> GetDataFromSPtanggalAsync(string spName, DateTime tanggalMulai, DateTime tanggalAkhir)
        {
            try
            {
                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tanggalMulai", tanggalMulai);
                    cmd.Parameters.AddWithValue("@tanggalAkhir", tanggalAkhir);

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    return dt;
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal ambil data:");
                throw;
            }
        }

        private async Task loadsp1()
        {
            using (FormLoading loading = new FormLoading())
            {
                loading.TopMost = true;
                loading.Show();
                loading.Refresh();

                try
                {
                    DateTime tanggalMulai = datejadwalMulai.Value.Date;
                    DateTime tanggalAkhir = datejadwalAkhir.Value.Date;

                    DataTable dt1 = await Task.Run(() => GetDataFromSPtanggalAsync("sp_LaporanPersediaanMaterial", tanggalMulai, tanggalAkhir));

                    dt1.Columns.Add("No", typeof(int)).SetOrdinal(0);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                        dt1.Rows[i]["No"] = i + 1;

                    dataGridView1.RowTemplate.Height = 34;
                    dataGridView1.DataSource = dt1;
                    dataGridView1.AutoGenerateColumns = true;

                    dataGridView1.ColumnHeadersVisible = false;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView1.AllowUserToAddRows = false;

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                        col.Resizable = DataGridViewTriState.False;

                    string[] hiddenCols = { "saldodex", "totalsaldo", "status", "tglrequest", "keterangan" };
                    foreach (var colName in hiddenCols)
                    {
                        if (dataGridView1.Columns.Contains(colName))
                            dataGridView1.Columns[colName].Visible = false;
                    }

                    var colWidths = new Dictionary<string, int>
                    {
                        ["No"] = label5.Width,
                        ["kodebarang"] = label4.Width,
                        ["namabarang"] = label8.Width,
                        ["spesifikasi"] = label9.Width,
                        ["stokawalktj"] = label15.Width,
                        ["uom"] = label7.Width,
                        ["masuk"] = label6.Width,
                        ["keluar"] = label10.Width,
                        ["saldoktj"] = label14.Width,
                        ["ratarata"] = label11.Width,
                        ["limit"] = label13.Width
                    };

                    foreach (var kvp in colWidths)
                    {
                        if (dataGridView1.Columns.Contains(kvp.Key))
                            dataGridView1.Columns[kvp.Key].Width = kvp.Value;
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                        "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Gagal cari");
                }
                finally
                {
                    loading.Close();
                }
            }
        }


        private void ReleaseCom(object obj)
        {
            try
            {
                if (obj != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch { }
        }

        private async void ExportToExcelBulan()
        {
            using (FormLoading loading = new FormLoading())
            {
                Form mainform = this.FindForm()?.ParentForm;
                mainform.Enabled = false;
                loading.Show(mainform);
                loading.Refresh();

                CancellationTokenSource cts = new CancellationTokenSource();
                var monitoringTask = StartNetworkMonitorAsync(cts);

                Excel.Application xlApp = null;
                Excel.Workbook xlWorkBook = null;
                Excel.Worksheet xlWorkSheet = null;

                try
                {
                    await Task.Run(async () =>
                    {
                        DateTime t1 = datejadwalMulai.Value.Date;
                        DateTime t2 = datejadwalAkhir.Value.Date;

                        if (cts.IsCancellationRequested)
                            throw new OperationCanceledException();

                        DataTable dtMaterial = await GetDataFromSPtanggalAsync(
                            "sp_LaporanPersediaanMaterial", t1, t2);

                        if (cts.IsCancellationRequested)
                            throw new OperationCanceledException();

                        xlApp = new Excel.Application();
                        string templatePath = Path.Combine(Application.StartupPath, "Stock Barang Template.xlsx");

                        xlWorkBook = xlApp.Workbooks.Open(templatePath);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                        xlWorkSheet.Cells[1, 1] = "LAPORAN PERSEDIAAN MATERIAL DI KUALA TANJUNG";
                        xlWorkSheet.Cells[2, 1] = "PT.GENTANUSA GEMILANG";
                        xlWorkSheet.Cells[3, 1] = $"Per Tanggal {t1:dd MMMM yyyy} s/d {t2:dd MMMM yyyy}";

                        int rowStart = 6;

                        for (int i = 0; i < dtMaterial.Rows.Count; i++)
                        {
                            if (cts.IsCancellationRequested)
                                throw new OperationCanceledException();

                            xlWorkSheet.Cells[rowStart + i, 1] = (i + 1).ToString();

                            for (int j = 0; j < dtMaterial.Columns.Count; j++)
                            {
                                xlWorkSheet.Cells[rowStart + i, 2 + j] = dtMaterial.Rows[i][j];
                            }
                        }

                        this.Invoke(new Action(() =>
                        {
                            loading.Close();
                            mainform.Enabled = true;

                            SaveFileDialog dlg = new SaveFileDialog
                            {
                                Filter = "Excel Files|*.xlsx",
                                FileName = $"STOCK BARANG KTJ PER {t1:ddMMMyyyy} - {t2:ddMMMyyyy}.xlsx"
                            };

                            if (dlg.ShowDialog(mainform) == DialogResult.OK)
                            {
                                if (File.Exists(dlg.FileName))
                                    File.Delete(dlg.FileName);

                                xlWorkBook.SaveCopyAs(dlg.FileName);
                                MessageBox.Show("Export selesai!", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }));
                    }, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        loading.Close();
                        mainform.Enabled = true;
                        MessageBox.Show("Proses dibatalkan karena jaringan terputus.", "Jaringan Terputus",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                }
                catch (Exception)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        loading.Close();
                        mainform.Enabled = true;
                        MessageBox.Show("Proses gagal. Periksa jaringan.", "Jaringan Terputus",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                }
                finally
                {
                    try
                    {
                        if (xlWorkBook != null)
                            xlWorkBook.Close(false);
                    }
                    catch { }

                    try
                    {
                        if (xlApp != null)
                            xlApp.Quit();
                    }
                    catch { }

                    ReleaseCom(xlWorkSheet);
                    ReleaseCom(xlWorkBook);
                    ReleaseCom(xlApp);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    cts.Cancel();
                }
            }
        }

        private async void btncari_Click(object sender, EventArgs e)
        {
            if (datejadwalMulai.Value.Date > datejadwalAkhir.Value.Date)
            {
                MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await loadsp1();
            lbltanggal.Text = "Per " + datejadwalMulai.Value.ToString("dd MMM yyyy") + " - " + datejadwalAkhir.Value.ToString("dd MMM yyyy");
            btnreset.Enabled = true;
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            datejadwalMulai.Value = DateTime.Now;
            datejadwalAkhir.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            lbltanggal.Text = "";
            btnreset.Enabled = false;
        }

        private void laporanpersediaan_Load(object sender, EventArgs e)
        {
            datejadwalMulai.Value = DateTime.Now;
            datejadwalAkhir.Value = DateTime.Now;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Lakukan Pencarian Terlebih Dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (datejadwalMulai.Value.Date > datejadwalAkhir.Value.Date)
            {
                MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ExportToExcelBulan();
        }
    }
}
