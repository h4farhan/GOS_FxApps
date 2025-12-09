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
    public partial class datamaterial : Form
    {

        public datamaterial()
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
            string sqlServerIP = "192.168.1.25";

            try
            {
                using (Ping p = new Ping())
                {
                    PingReply reply = await p.SendPingAsync(sqlServerIP, 1200);
                    if (reply.Status != IPStatus.Success)
                        return false;
                }
            }
            catch
            {
                return false;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Koneksi.GetConnectionString()))
                {
                    var timeoutTask = Task.Delay(2000);
                    var openTask = conn.OpenAsync();

                    var finished = await Task.WhenAny(openTask, timeoutTask);

                    if (finished == timeoutTask)
                        return false;

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        return true;
                    }

                    return false;
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
                    dataGridView1.AutoGenerateColumns = true;

                    DateTime tanggalMulai = datejadwalMulai.Value.Date;
                    DateTime tanggalAkhir = datejadwalAkhir.Value.Date;

                    DataTable dt1 = await Task.Run(() => GetDataFromSPtanggalAsync("sp_LaporanDataMaterial", tanggalMulai, tanggalAkhir));

                    dt1.Columns.Add("No", typeof(int)).SetOrdinal(0);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                        dt1.Rows[i]["No"] = i + 1;

                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                    dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                    dataGridView1.RowTemplate.Height = 70;
                    dataGridView1.AllowUserToResizeRows = false;
                    dataGridView1.DataSource = dt1;

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                        col.Resizable = DataGridViewTriState.False;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dataGridView1.ColumnHeadersVisible = false;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView1.AllowUserToAddRows = false;

                    var colWidths = new Dictionary<string, int>
                    {
                        ["No"] = label5.Width,
                        ["Periode"] = label12.Width,
                        ["kodeBarang"] = label4.Width,
                        ["namaBarang"] = label8.Width,
                        ["spesifikasi"] = label9.Width,
                        ["uom"] = label15.Width,
                        ["StokAwal"] = label7.Width,
                        ["Masuk1"] = label6.Width,
                        ["Masuk2"] = label10.Width,
                        ["Masuk3"] = label14.Width,
                        ["Masuk4"] = label13.Width,
                        ["Masuk5"] = label11.Width,
                        ["Masuk6"] = label20.Width,
                        ["Masuk7"] = label19.Width,
                        ["Masuk8"] = label16.Width,
                        ["Masuk9"] = label18.Width,
                        ["Masuk10"] = label17.Width,
                        ["Masuk11"] = label30.Width,
                        ["Masuk12"] = label29.Width,
                        ["Masuk13"] = label26.Width,
                        ["Masuk14"] = label28.Width,
                        ["Masuk15"] = label27.Width,
                        ["Masuk16"] = label25.Width,
                        ["Masuk17"] = label24.Width,
                        ["Masuk18"] = label21.Width,
                        ["Masuk19"] = label23.Width,
                        ["Masuk20"] = label22.Width,
                        ["Masuk21"] = label40.Width,
                        ["Masuk22"] = label39.Width,
                        ["Masuk23"] = label36.Width,
                        ["Masuk24"] = label38.Width,
                        ["Masuk25"] = label37.Width,
                        ["Masuk26"] = label35.Width,
                        ["Masuk27"] = label34.Width,
                        ["Masuk28"] = label31.Width,
                        ["Masuk29"] = label33.Width,
                        ["Masuk30"] = label32.Width,
                        ["Masuk31"] = label41.Width,
                        ["Keluar1"] = label72.Width,
                        ["Keluar2"] = label71.Width,
                        ["Keluar3"] = label68.Width,
                        ["Keluar4"] = label70.Width,
                        ["Keluar5"] = label69.Width,
                        ["Keluar6"] = label67.Width,
                        ["Keluar7"] = label66.Width,
                        ["Keluar8"] = label63.Width,
                        ["Keluar9"] = label65.Width,
                        ["Keluar10"] = label64.Width,
                        ["Keluar11"] = label62.Width,
                        ["Keluar12"] = label61.Width,
                        ["Keluar13"] = label58.Width,
                        ["Keluar14"] = label60.Width,
                        ["Keluar15"] = label59.Width,
                        ["Keluar16"] = label57.Width,
                        ["Keluar17"] = label56.Width,
                        ["Keluar18"] = label53.Width,
                        ["Keluar19"] = label55.Width,
                        ["Keluar20"] = label54.Width,
                        ["Keluar21"] = label52.Width,
                        ["Keluar22"] = label51.Width,
                        ["Keluar23"] = label48.Width,
                        ["Keluar24"] = label50.Width,
                        ["Keluar25"] = label49.Width,
                        ["Keluar26"] = label47.Width,
                        ["Keluar27"] = label46.Width,
                        ["Keluar28"] = label43.Width,
                        ["Keluar29"] = label45.Width,
                        ["Keluar30"] = label44.Width,
                        ["Keluar31"] = label42.Width
                    };

                    foreach (var kvp in colWidths)
                    {
                        if (dataGridView1.Columns.Contains(kvp.Key))
                            dataGridView1.Columns[kvp.Key].Width = kvp.Value;
                    }

                    if (dataGridView1.Columns.Contains("foto"))
                    {
                        int fotoIndex = dataGridView1.Columns["foto"].Index;

                        DataGridViewImageColumn imgCol = new DataGridViewImageColumn
                        {
                            Name = "foto",
                            HeaderText = "Foto",
                            ImageLayout = DataGridViewImageCellLayout.Zoom,
                            DataPropertyName = "foto"
                        };

                        dataGridView1.Columns.Remove("foto");
                        dataGridView1.Columns.Insert(fotoIndex, imgCol);
                        dataGridView1.Columns["foto"].Width = label73.Width;
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

        private CancellationTokenSource exportCts;

        private async void ExportToExcelBulan()
        {
            exportCts = new CancellationTokenSource();
            CancellationToken ct = exportCts.Token;

            using (FormLoading loading = new FormLoading())
            {
                Form mainform = this.FindForm()?.ParentForm;

                mainform.Enabled = false;
                loading.Show(mainform);
                loading.Refresh();

                Excel.Application xlApp = null;
                Excel.Workbook xlWorkBook = null;
                Excel.Worksheet xlWorkSheet = null;

                var monitorNetwork = StartNetworkMonitorAsync(exportCts);

                try
                {
                    await Task.Run(async () =>
                    {
                        if (!await IsNetworkOk())
                            throw new Exception("NETWORK_DOWN");

                        DateTime tanggalMulai = datejadwalMulai.Value.Date;
                        DateTime tanggalAkhir = datejadwalAkhir.Value.Date;

                        DataTable dtMaterial = await GetDataFromSPtanggalAsync(
                            "sp_LaporanDataMaterial", tanggalMulai, tanggalAkhir);

                        if (ct.IsCancellationRequested || !await IsNetworkOk())
                            throw new Exception("NETWORK_LOST_MID");

                        xlApp = new Excel.Application();
                        string templatePath = Path.Combine(Application.StartupPath, "Data Barang Template.xlsx");
                        xlWorkBook = xlApp.Workbooks.Open(templatePath);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                        int rowStart = 3;
                        int colStart = 1;

                        for (int i = 0; i < dtMaterial.Rows.Count; i++)
                        {
                            if (ct.IsCancellationRequested)
                                throw new Exception("CANCELLED");

                            if (!await IsNetworkOk())
                                throw new Exception("NETWORK_LOST_MID");

                            xlWorkSheet.Cells[rowStart + i, colStart] = (i + 1).ToString();

                            for (int j = 0; j < dtMaterial.Columns.Count; j++)
                            {
                                string columnName = dtMaterial.Columns[j].ColumnName.ToLower();

                                if (columnName == "foto")
                                {
                                    if (dtMaterial.Rows[i][j] != DBNull.Value)
                                    {
                                        byte[] imgBytes = (byte[])dtMaterial.Rows[i][j];
                                        using (MemoryStream ms = new MemoryStream(imgBytes))
                                        {
                                            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                                            string tempPath = Path.Combine(Path.GetTempPath(), $"img_{i}_{j}.png");
                                            img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);

                                            Excel.Range cell = (Excel.Range)xlWorkSheet.Cells[rowStart + i, colStart + j + 1];
                                            Excel.Pictures pics = (Excel.Pictures)xlWorkSheet.Pictures(Type.Missing);
                                            Excel.Picture pic = pics.Insert(tempPath, Type.Missing);

                                            float cm = 28.35f;
                                            pic.Height = 3.5f * cm;
                                            pic.Left = (float)cell.Left + ((float)cell.Width - pic.Width) / 2;
                                            pic.Top = (float)cell.Top + ((float)cell.Height - pic.Height) / 2;
                                        }
                                    }
                                }
                                else
                                {
                                    xlWorkSheet.Cells[rowStart + i, colStart + j + 1] =
                                        dtMaterial.Rows[i][j]?.ToString();
                                }
                            }
                        }

                        this.Invoke(new Action(() =>
                        {
                            loading.Close();
                            mainform.Enabled = true;

                            SaveFileDialog saveDialog = new SaveFileDialog
                            {
                                Title = "Simpan File Excel",
                                Filter = "Excel Files|*.xlsx",
                                FileName = $"DATA BARANG KTJ PER {tanggalMulai:ddMMMyyyy} - {tanggalAkhir:ddMMMyyyy}.xlsx"
                            };

                            if (saveDialog.ShowDialog(mainform) == DialogResult.OK)
                            {
                                if (File.Exists(saveDialog.FileName))
                                    File.Delete(saveDialog.FileName);

                                xlWorkBook.SaveCopyAs(saveDialog.FileName);
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

                    exportCts.Cancel();
                }
            }
        }

        private async void btncari_Click(object sender, EventArgs e)
        {
            DateTime mulai = datejadwalMulai.Value.Date;
            DateTime akhir = datejadwalAkhir.Value.Date;

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
                MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal akhir!",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            await loadsp1();
            lbltanggal.Text = "Per " + datejadwalMulai.Value.ToString("dd MMM yyyy") + " - " + datejadwalAkhir.Value.ToString("dd MMM yyyy");
            btnreset.Enabled = true;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            DateTime mulai = datejadwalMulai.Value.Date;
            DateTime akhir = datejadwalAkhir.Value.Date;

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
                MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal akhir!",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            ExportToExcelBulan();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            datejadwalMulai.Value = DateTime.Now;
            datejadwalAkhir.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            lbltanggal.Text = "";
            btnreset.Enabled = false;
        }

        private void datamaterial_Load(object sender, EventArgs e)
        {
            datejadwalMulai.Value = DateTime.Now;
            datejadwalAkhir.Value = DateTime.Now;
        }

    }
}
