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
using DrawingPoint = System.Drawing.Point;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Guna.UI2.WinForms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.NetworkInformation;

namespace GOS_FxApps
{
    public partial class EstimasiPemakaianMaterial : Form
    {

        public EstimasiPemakaianMaterial()
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
        private void AddStaticRow(DataTable dt, string text)
        {
            DataRow r = dt.NewRow();
            r["Deskripsi"] = text;
            dt.Rows.Add(r);
        }

        private async Task loadsp2()
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

                    var dt1Task = Task.Run(() => GetDataFromSPtanggalAsync("sp_koefisiensiMaterialCostbulan", tanggalMulai, tanggalAkhir));
                    var dt2Task = Task.Run(() => GetDataFromSPtanggalAsync("sp_koefisiensiConsumableCostbulan", tanggalMulai, tanggalAkhir));
                    var dt3Task = Task.Run(() => GetDataFromSPtanggalAsync("sp_koefisiensiSafetyCostbulan", tanggalMulai, tanggalAkhir));

                    await Task.WhenAll(dt1Task, dt2Task, dt3Task);

                    DataTable dt1 = dt1Task.Result;
                    DataTable dt2 = dt2Task.Result;
                    DataTable dt3 = dt3Task.Result;

                    DataTable finalDt = dt1.Clone();

                    AddStaticRow(finalDt, "-Material Cost-");
                    finalDt.Merge(dt1);

                    AddStaticRow(finalDt, "-Consumable Cost-");
                    finalDt.Merge(dt2);

                    AddStaticRow(finalDt, "-Safety Cost-");
                    finalDt.Merge(dt3);

                    finalDt.Columns.Add("No", typeof(int)).SetOrdinal(0);

                    int no = 1;
                    for (int i = 0; i < finalDt.Rows.Count; i++)
                    {
                        string desc = finalDt.Rows[i]["Deskripsi"]?.ToString() ?? "";
                        if (desc.StartsWith("-"))
                            finalDt.Rows[i]["No"] = DBNull.Value;
                        else
                        {
                            finalDt.Rows[i]["No"] = no;
                            no++;
                        }
                    }

                    dataGridView1.RowTemplate.Height = 34;
                    dataGridView1.DataSource = finalDt;
                    dataGridView1.ColumnHeadersVisible = false;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToResizeRows = false;

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                        col.Resizable = DataGridViewTriState.False;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        string desc = row.Cells["Deskripsi"].Value?.ToString() ?? "";
                        if (desc.StartsWith("-"))
                        {
                            row.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                            row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        }
                    }

                    string[] hiddenCols = { "Juli","Agustus","September","Oktober","November","Desember",
                                    "Januari","Februari","Maret","April","Mei","Juni" };
                    foreach (var colName in hiddenCols)
                    {
                        if (dataGridView1.Columns.Contains(colName))
                            dataGridView1.Columns[colName].Visible = false;
                    }

                    var colWidths = new Dictionary<string, int>
                    {
                        ["No"] = label5.Width,
                        ["Deskripsi"] = label8.Width,
                        ["Spesifikasi"] = label9.Width,
                        ["Satuan"] = label15.Width,
                        ["Koef E1"] = label23.Width,
                        ["Hasil E1"] = label24.Width,
                        ["Koef E2"] = label26.Width,
                        ["Hasil E2"] = label28.Width,
                        ["Koef E3"] = label30.Width,
                        ["Hasil E3"] = label32.Width,
                        ["Koef E4"] = label34.Width,
                        ["Hasil E4"] = label36.Width,
                        ["Koef S"] = label38.Width,
                        ["Hasil S"] = label25.Width,
                        ["Koef D"] = label40.Width,
                        ["Hasil D"] = label27.Width,
                        ["Koef B"] = label42.Width,
                        ["Hasil B"] = label29.Width,
                        ["Koef BA"] = label44.Width,
                        ["Hasil BA"] = label31.Width,
                        ["Koef BA1"] = label46.Width,
                        ["Hasil BA1"] = label33.Width,
                        ["Koef R"] = label48.Width,
                        ["Hasil R"] = label35.Width,
                        ["Koef M"] = label37.Width,
                        ["Hasil M"] = label39.Width,
                        ["Koef CR"] = label41.Width,
                        ["Hasil CR"] = label43.Width,
                        ["Koef C"] = label45.Width,
                        ["Hasil C"] = label47.Width,
                        ["Koef RL"] = label50.Width,
                        ["Hasil RL"] = label49.Width,
                        ["Total"] = label51.Width,
                        ["Rata-rata per Hari"] = label52.Width,
                        ["TotalPemakaian"] = label53.Width,
                        ["RataPerHariKumulatif"] = label54.Width,
                        ["UoM2"] = label55.Width,
                        ["bq"] = label57.Width,
                        ["aktual"] = label56.Width,
                        ["Persentase"] = label59.Width
                    };

                    foreach (var kvp in colWidths)
                    {
                        if (dataGridView1.Columns.Contains(kvp.Key))
                            dataGridView1.Columns[kvp.Key].Width = kvp.Value;
                    }

                    string[] hasilCols = { "Hasil E1","Hasil E2","Hasil E3","Hasil E4","Hasil S","Hasil D",
                                   "Hasil B","Hasil BA","Hasil BA1","Hasil R","Hasil M","Hasil CR",
                                   "Hasil C","Hasil RL" };
                    foreach (var colName in hasilCols)
                    {
                        if (dataGridView1.Columns.Contains(colName))
                            dataGridView1.Columns[colName].DefaultCellStyle.Format = "N0";
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
                        if (!await IsNetworkOk())
                            throw new OperationCanceledException("NETWORK_DOWN");

                        DateTime tanggalMulai = datejadwalMulai.Value.Date;
                        DateTime tanggalAkhir = datejadwalAkhir.Value.Date;

                        DataTable dtMaterial = await GetDataFromSPtanggalAsync("sp_koefisiensiMaterialCostbulan", tanggalMulai, tanggalAkhir);
                        if (cts.IsCancellationRequested) throw new OperationCanceledException("NETWORK_LOST_MID");

                        DataTable dtConsumable = await GetDataFromSPtanggalAsync("sp_koefisiensiConsumableCostbulan", tanggalMulai, tanggalAkhir);
                        if (cts.IsCancellationRequested) throw new OperationCanceledException("NETWORK_LOST_MID");

                        DataTable dtsafety = await GetDataFromSPtanggalAsync("sp_koefisiensiSafetyCostbulan", tanggalMulai, tanggalAkhir);
                        if (cts.IsCancellationRequested) throw new OperationCanceledException("NETWORK_LOST_MID");

                        DataTable dtQty = await GetDataFromSPtanggalAsync("koefisiensiqtybulan", tanggalMulai, tanggalAkhir);
                        if (cts.IsCancellationRequested) throw new OperationCanceledException("NETWORK_LOST_MID");


                        xlApp = new Excel.Application();
                        string templatePath = Path.Combine(Application.StartupPath, "Koefisien Material.xlsx");

                        xlWorkBook = xlApp.Workbooks.Open(templatePath);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                        xlWorkSheet.Cells[1, 1] =
                            $"BQ PEKERJAAN ROD REPAIR SHOP PERIODE {tanggalMulai:dd MMM yyyy} s/d {tanggalAkhir:dd MMM yyyy}".ToUpper();

                        xlWorkSheet.Cells[2, 1] =
                            "UoM = Unit of Measure, U/Price = Unit Price, Coeff. = Coefficient, " +
                            "E1,E2,E3 = Erotion, S=Sticking, D=Deformation, B=Bending, BA=BA Clade Change, R=Spark, CR=Crack York, M=Crack MIG, C=End Cut, RL=Rod Long";


                        if (dtMaterial.Rows.Count > 0)
                        {
                            Excel.ListObject tbl = xlWorkSheet.ListObjects["Table6"];
                            int nomor = 1;

                            foreach (DataRow dr in dtMaterial.Rows)
                            {
                                if (cts.IsCancellationRequested)
                                    throw new OperationCanceledException("NETWORK_LOST_MID");

                                Excel.ListRow row = tbl.ListRows.Add();
                                row.Range[1, 1].Value2 = nomor++;

                                for (int j = 0; j < dtMaterial.Columns.Count; j++)
                                {
                                    int colTarget = j + 2;
                                    if (dtMaterial.Columns[j].ColumnName == "Persentase")
                                        row.Range[1, colTarget].Value2 = dr[j] + " %";
                                    else
                                        row.Range[1, colTarget].Value2 = dr[j];
                                }
                            }
                        }

                        if (dtConsumable.Rows.Count > 0)
                        {
                            Excel.ListObject tbl = xlWorkSheet.ListObjects["Table1"];
                            int nomor = 1;

                            foreach (DataRow dr in dtConsumable.Rows)
                            {
                                if (cts.IsCancellationRequested)
                                    throw new OperationCanceledException("NETWORK_LOST_MID");

                                Excel.ListRow row = tbl.ListRows.Add();
                                row.Range[1, 1].Value2 = nomor++;

                                for (int j = 0; j < dtConsumable.Columns.Count; j++)
                                {
                                    int colTarget = j + 2;
                                    if (dtConsumable.Columns[j].ColumnName == "Persentase")
                                        row.Range[1, colTarget].Value2 = dr[j] + " %";
                                    else
                                        row.Range[1, colTarget].Value2 = dr[j];
                                }
                            }
                        }

                        if (dtsafety.Rows.Count > 0)
                        {
                            Excel.ListObject tbl = xlWorkSheet.ListObjects["Table2"];
                            int nomor = 1;

                            foreach (DataRow dr in dtsafety.Rows)
                            {
                                if (cts.IsCancellationRequested)
                                    throw new OperationCanceledException("NETWORK_LOST_MID");

                                Excel.ListRow row = tbl.ListRows.Add();
                                row.Range[1, 1].Value2 = nomor++;

                                for (int j = 0; j < dtsafety.Columns.Count; j++)
                                {
                                    int colTarget = j + 2;
                                    if (dtsafety.Columns[j].ColumnName == "Persentase")
                                        row.Range[1, colTarget].Value2 = dr[j] + " %";
                                    else
                                        row.Range[1, colTarget].Value2 = dr[j];
                                }
                            }
                        }

                        if (dtQty.Rows.Count > 0)
                        {
                            DataRow r = dtQty.Rows[0];
                            xlWorkSheet.Cells[5, 6] = r["Total_E1"];
                            xlWorkSheet.Cells[5, 8] = r["Total_E2"];
                            xlWorkSheet.Cells[5, 10] = r["Total_E3"];
                            xlWorkSheet.Cells[5, 12] = r["Total_E4"];
                            xlWorkSheet.Cells[5, 14] = r["Total_S"];
                            xlWorkSheet.Cells[5, 16] = r["Total_D"];
                            xlWorkSheet.Cells[5, 18] = r["Total_B"];
                            xlWorkSheet.Cells[5, 20] = r["Total_BA"];
                            xlWorkSheet.Cells[5, 22] = r["Total_BA1"];
                            xlWorkSheet.Cells[5, 24] = r["Total_R"];
                            xlWorkSheet.Cells[5, 26] = r["Total_M"];
                            xlWorkSheet.Cells[5, 28] = r["Total_CR"];
                            xlWorkSheet.Cells[5, 30] = r["Total_C"];
                            xlWorkSheet.Cells[5, 32] = r["Total_RL"];
                        }

                        this.Invoke(new Action(() =>
                        {
                            loading.Close();
                            mainform.Enabled = true;

                            SaveFileDialog dlg = new SaveFileDialog
                            {
                                Title = "Simpan File Excel",
                                Filter = "Excel Files|*.xlsx",
                                FileName = $"Koefisien Material Bulan {tanggalMulai:ddMMMyyyy}-{tanggalAkhir:ddMMMyyyy}.xlsx"
                            };

                            if (dlg.ShowDialog(mainform) == DialogResult.OK)
                            {
                                if (File.Exists(dlg.FileName)) File.Delete(dlg.FileName);
                                xlWorkBook.SaveCopyAs(dlg.FileName);

                                MessageBox.Show(mainform, "Export selesai!", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }));
                    });
                }
                catch (OperationCanceledException)
                {
                    this.Invoke(new Action(() =>
                    {
                        loading.Close();
                        mainform.Enabled = true;
                        MessageBox.Show(mainform, "Proses dibatalkan karena jaringan terputus.", "Jaringan Terputus",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                }
                catch (Exception)
                {
                    this.Invoke(new Action(() =>
                    {
                        loading.Close();
                        mainform.Enabled = true;
                        MessageBox.Show(mainform, "Proses gagal. Periksa jaringan.", "Jaringan Terputus",
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            DateTime mulai = datejadwalMulai.Value.Date;
            DateTime akhir = datejadwalAkhir.Value.Date;

            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Lakukan Pencarian Terlebih Dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            TimeSpan span = akhir - mulai;

            int selisihBulan = (akhir.Year - mulai.Year) * 12 + (akhir.Month - mulai.Month) + 1;

            if (selisihBulan > 12)
            {
                MessageBox.Show("Rentang tanggal tidak boleh melebihi 12 bulan.",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            ExportToExcelBulan();
        }

        private void EstimasiPemakaianMaterial_Load(object sender, EventArgs e)
        {
            datejadwalMulai.Value = DateTime.Now;
            datejadwalAkhir.Value = DateTime.Now;
            lbltanggal.Text = "";
        }

        private async void btncari_Click(object sender, EventArgs e)
        {
            DateTime mulai = datejadwalMulai.Value.Date;
            DateTime akhir = datejadwalAkhir.Value.Date;

            if (mulai > akhir)
            {
                MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            TimeSpan span = akhir - mulai;

            int selisihBulan = (akhir.Year - mulai.Year) * 12 + (akhir.Month - mulai.Month) + 1;

            if (selisihBulan > 12)
            {
                MessageBox.Show("Rentang tanggal tidak boleh melebihi 12 bulan.",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }


            await loadsp2();
            lbltanggal.Text = "Per " + mulai.ToString("dd/MM/yyyy") + " - " + akhir.ToString("dd/MM/yyyy");
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
    }
}
