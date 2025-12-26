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
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Drawing.Printing;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace GOS_FxApps
{
    public partial class historyPengiriman : Form
    {
        public static historyPengiriman instance;

        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;
        private bool isEditing = false;

        public historyPengiriman()
        {
            InitializeComponent();
            instance = this;
        }

        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                if (isEditing) return;

                switch (table)
                {
                    case "pengiriman":
                        if (!isSearching)
                        {
                            await HitungTotalData();
                            currentPage = 1;
                            await tampil();
                        }
                        else
                        {
                            int oldTotal = searchTotalRecords;
                            await HitungTotalDataPencarian();
                            if (searchTotalRecords > oldTotal)
                                await tampil();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal realtime");
                return;
            }
        }

        private async Task HitungTotalData()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM pengiriman";
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        totalRecords = (int)await cmd.ExecuteScalarAsync();
                    }
                }
                totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal hitungtotaldata");
                return;
            }
        }

        private async Task HitungTotalDataPencarian()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lastSearchWhere))
                {
                    searchTotalRecords = 0;
                    totalPages = 0;
                    return;
                }

                string countQuery = "SELECT COUNT(*) " + lastSearchWhere;
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (var cmd = new SqlCommand(countQuery, conn))
                    {
                        if (lastSearchCmd?.Parameters.Count > 0)
                        {
                            foreach (SqlParameter p in lastSearchCmd.Parameters)
                                cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                        }

                        searchTotalRecords = (int)await cmd.ExecuteScalarAsync();
                    }
                }

                totalPages = (int)Math.Ceiling(searchTotalRecords / (double)pageSize);
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal hitungtotaldatacari");
                return;
            }
        }

        private async Task tampil()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    string query;

                    if (!isSearching)
                    {
                        query = $@"
                SELECT *
                FROM pengiriman
                ORDER BY tanggal_pengiriman DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                    }
                    else
                    {
                        query = $@"
                SELECT *
                {lastSearchWhere}
                ORDER BY tanggal_pengiriman DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";

                        foreach (SqlParameter p in lastSearchCmd.Parameters)
                            cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                    }

                    cmd.CommandText = query;

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(dt);
                    }

                    if (dataGridView1.InvokeRequired)
                    {
                        dataGridView1.Invoke(new Action(() =>
                        {
                            UpdateGrid(dt);
                        }));
                    }
                    else
                    {
                        UpdateGrid(dt);
                    }
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal tampil");
                return;
            }
        }

        private void UpdateGrid(DataTable dt)
        {
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeRows = false;

            if (dt.Columns.Count >= 6)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Pengiriman";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Diubah";
                dataGridView1.Columns[5].HeaderText = "Remaks";
            }

            if (!isSearching)
            {
                lbljumlahdata.Text = "Jumlah data: " + totalRecords;
            }
            else
            {
                lbljumlahdata.Text = "Hasil pencarian: " + searchTotalRecords;
            }

            lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

            btnleft.Enabled = currentPage > 1;
            btnright.Enabled = currentPage < totalPages;
        }

        private async Task<bool> cari()
        {
            DateTime? date1Value = tanggal1.Checked ? (DateTime?)tanggal1.Value.Date : null;
            DateTime? date2Value = tanggal2.Checked ? (DateTime?)tanggal2.Value.Date : null;
            string inputRod = txtcari.Text.Trim();
            string inputnama = txtnama.Text.Trim();
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!date1Value.HasValue && !date2Value.HasValue && string.IsNullOrEmpty(inputRod) && string.IsNullOrEmpty(inputnama) && !shiftValid)
            {
                MessageBox.Show("Silakan isi Tanggal, Nomor ROD, Checker Tim Penginput atau Shift untuk melakukan pencarian.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM pengiriman WHERE 1=1 ";

                if (date1Value.HasValue && date2Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal_pengiriman AS DATE) BETWEEN @tgl1 AND @tgl2 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl1", date1Value.Value);
                    lastSearchCmd.Parameters.AddWithValue("@tgl2", date2Value.Value);
                }
                else if (date1Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal_pengiriman AS DATE) = @tgl1 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl1", date1Value.Value);
                }
                else if (date2Value.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal_pengiriman AS DATE) = @tgl2 ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl2", date2Value.Value);
                }

                if (!string.IsNullOrEmpty(inputRod))
                {
                    lastSearchWhere += " AND nomor_rod LIKE @rod ";
                    lastSearchCmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");
                }

                if (!string.IsNullOrEmpty(inputnama))
                {
                    lastSearchWhere += " AND remaks LIKE @remaks ";
                    lastSearchCmd.Parameters.AddWithValue("@remaks", "%" + inputnama + "%");
                }

                if (shiftValid)
                {
                    lastSearchWhere += " AND shift = @shift ";
                    lastSearchCmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem.ToString());
                }

                await HitungTotalDataPencarian();
                currentPage = 1;
                await tampil();

                btnreset.Enabled = true;
                return true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal cari");
                return false;
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

        private async Task<DataTable> AmbilSemuaDataPencarian()
        {
            DataTable dt = new DataTable();

            using (var conn = await Koneksi.GetConnectionAsync())
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                string fullQuery = $@"
            SELECT *
            {lastSearchWhere}
            ORDER BY tanggal_pengiriman DESC";

                cmd.CommandText = fullQuery;

                foreach (SqlParameter p in lastSearchCmd.Parameters)
                    cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));

                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dt);
                }
            }

            return dt;
        }

        private async void ExportPengirimanFromGrid()
        {
            if (!isSearching)
            {
                MessageBox.Show("Silakan lakukan pencarian dulu.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtExport = await AmbilSemuaDataPencarian();

            if (dtExport.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diexport.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> visibleCols = new List<int>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Visible)
                    visibleCols.Add(col.Index);
            }

            List<object[]> gridData = new List<object[]>();

            foreach (DataRow dr in dtExport.Rows)
            {
                object[] row = new object[visibleCols.Count];

                for (int i = 0; i < visibleCols.Count; i++)
                {
                    string colName = dataGridView1.Columns[visibleCols[i]].Name;
                    row[i] = dr[colName];
                }

                gridData.Add(row);
            }

            using (FormLoading loading = new FormLoading())
            {
                Form mainform = this.FindForm()?.ParentForm;
                mainform.Enabled = false;
                loading.Show(mainform);
                loading.Refresh();

                bool t1Checked = tanggal1.Checked;
                bool t2Checked = tanggal2.Checked;
                DateTime t1Value = tanggal1.Value;
                DateTime t2Value = tanggal2.Value;

                string shift = cbShift.Text;
                string rod = txtcari.Text;
                string checker = txtnama.Text;

                Excel.Application xlApp = null;
                Excel.Workbook xlWorkBook = null;
                Excel.Worksheet xlSheet = null;

                try
                {
                    await Task.Run(() =>
                    {

                        xlApp = new Excel.Application();
                        string templatePath = Path.Combine(Application.StartupPath, "Template_Pengiriman.xlsx");

                        xlWorkBook = xlApp.Workbooks.Open(templatePath);
                        xlSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                        Excel.Range title = xlSheet.Range["A1:F1"];
                        title.Merge();
                        title.Value = "LAPORAN PENGIRIMAN ROD";
                        title.Font.Bold = true;
                        title.Font.Size = 18;
                        title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        string tanggalFilter = "";
                        if (t1Checked && t2Checked)
                            tanggalFilter = $"Tanggal: {t1Value:yyyy-MM-dd} s/d {t2Value:yyyy-MM-dd}";
                        else if (t1Checked)
                            tanggalFilter = $"Tanggal: >= {t1Value:yyyy-MM-dd}";
                        else if (t2Checked)
                            tanggalFilter = $"Tanggal: <= {t2Value:yyyy-MM-dd}";
                        else
                            tanggalFilter = "Tanggal: (semua)";

                        string filterText =
                            $"Filter: {tanggalFilter} | Shift: {shift} | ROD: {rod} | Checker: {checker}";

                        Excel.Range filterRow = xlSheet.Range["A2:F2"];
                        filterRow.Merge();
                        filterRow.Value = filterText;
                        filterRow.Font.Bold = true;
                        filterRow.Font.Size = 12;
                        filterRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        int startRow = 5;

                        for (int r = 0; r < gridData.Count; r++)
                        {
                            xlSheet.Cells[startRow + r, 1] = r + 1; 

                            object[] row = gridData[r];
                            for (int c = 0; c < row.Length; c++)
                                xlSheet.Cells[startRow + r, c + 2] = row[c];
                        }

                        this.Invoke(new Action(() =>
                        {
                            loading.Close();
                            mainform.Enabled = true;

                            SaveFileDialog dlg = new SaveFileDialog
                            {
                                Filter = "Excel Files|*.xlsx",
                                FileName = $"Laporan_Pengiriman_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                            };

                            if (dlg.ShowDialog(mainform) == DialogResult.OK)
                            {
                                if (File.Exists(dlg.FileName))
                                    File.Delete(dlg.FileName);

                                xlWorkBook.SaveCopyAs(dlg.FileName);
                                MessageBox.Show("Export selesai!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }));
                    });
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

                    ReleaseCom(xlSheet);
                    ReleaseCom(xlWorkBook);
                    ReleaseCom(xlApp);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        private async void historyPengiriman_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            tanggal1.Value = DateTime.Now.Date;
            tanggal2.Value = DateTime.Now.Date;
            tanggal1.Checked = false;
            tanggal2.Checked = false;

            await HitungTotalData();
            await tampil();
        }

        private async void btncari_Click(object sender, EventArgs e)
        {
            await cari();
        }        

        private async void btnreset_Click_1(object sender, EventArgs e)
        {
            isSearching = false;

            txtcari.Clear();
            txtnama.Clear();
            cbShift.SelectedIndex = 0;
            tanggal1.Checked = false;
            tanggal2.Checked = false;

            btnreset.Enabled = false;

            await HitungTotalData();
            currentPage = 1;
            await tampil();
        }

        private async void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await tampil();
            }
        }

        private async void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                await tampil();
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void historyPengiriman_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            ExportPengirimanFromGrid();
        }
    }
}
