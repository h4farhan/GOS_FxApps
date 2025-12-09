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

namespace GOS_FxApps
{
    public partial class laporanpersediaan : Form
    {

        public laporanpersediaan()
        {
            InitializeComponent();
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
            try
            {
                DateTime tanggalMulai = datejadwalMulai.Value.Date;
                DateTime tanggalAkhir = datejadwalAkhir.Value.Date;

                DataTable dt1 = await GetDataFromSPtanggalAsync("sp_LaporanPersediaanMaterial", tanggalMulai, tanggalAkhir);

                dt1.Columns.Add("No", typeof(int)).SetOrdinal(0);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    dt1.Rows[i]["No"] = i + 1;
                }

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
                {
                    col.Resizable = DataGridViewTriState.False;
                }

                dataGridView1.Columns["saldodex"].Visible = false;
                dataGridView1.Columns["totalsaldo"].Visible = false;
                dataGridView1.Columns["status"].Visible = false;
                dataGridView1.Columns["tglrequest"].Visible = false;
                dataGridView1.Columns["keterangan"].Visible = false;

                dataGridView1.Columns["No"].Width = label5.Width;
                dataGridView1.Columns["kodebarang"].Width = label4.Width;
                dataGridView1.Columns["namabarang"].Width = label8.Width;
                dataGridView1.Columns["spesifikasi"].Width = label9.Width;
                dataGridView1.Columns["stokawalktj"].Width = label15.Width;
                dataGridView1.Columns["uom"].Width = label7.Width;
                dataGridView1.Columns["masuk"].Width = label6.Width;
                dataGridView1.Columns["keluar"].Width = label10.Width;
                dataGridView1.Columns["saldoktj"].Width = label14.Width;

                dataGridView1.Columns["ratarata"].Width = label11.Width;
                dataGridView1.Columns["limit"].Width = label13.Width;
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                    "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal cari");
                return;
            }
        }

        private async void ExportToExcelBulan()
        {
            using (FormLoading loading = new FormLoading())
            {
                Form mainform = this.FindForm()?.ParentForm;
                mainform.Enabled = false;
                loading.Show(mainform);
                loading.Refresh();

                await Task.Run(async () =>
                {
                    try
                    {
                        DateTime tanggalMulai = datejadwalMulai.Value.Date;
                        DateTime tanggalAkhir = datejadwalAkhir.Value.Date;

                        string judulRange = $"{tanggalMulai:dd MMMM yyyy} s/d {tanggalAkhir:dd MMMM yyyy}";

                        DataTable dtMaterial = await GetDataFromSPtanggalAsync("sp_LaporanPersediaanMaterial",tanggalMulai,tanggalAkhir);

                        Excel.Application xlApp = new Excel.Application();
                        string templatePath = Path.Combine(Application.StartupPath, "Stock Barang Template.xlsx");
                        Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(templatePath);
                        Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                        xlWorkSheet.Cells[1, 1] = "LAPORAN PERSEDIAAN MATERIAL DI KUALA TANJUNG";
                        xlWorkSheet.Cells[2, 1] = "PT.GENTANUSA GEMILANG";
                        xlWorkSheet.Cells[3, 1] = $"Per Tanggal {judulRange}";

                        int rowStart = 6;
                        int colStart = 1;
                        for (int i = 0; i < dtMaterial.Rows.Count; i++)
                        {
                            xlWorkSheet.Cells[rowStart + i, colStart] = (i + 1).ToString();
                            for (int j = 0; j < dtMaterial.Columns.Count; j++)
                            {
                                xlWorkSheet.Cells[rowStart + i, colStart + j + 1] = dtMaterial.Rows[i][j].ToString();
                            }
                        }

                        this.Invoke(new Action(() =>
                        {
                            loading.Close();
                            mainform.Enabled = true;

                            SaveFileDialog saveFileDialog = new SaveFileDialog
                            {
                                Title = "Simpan File Excel",
                                Filter = "Excel Files|*.xlsx",
                                FileName = $"STOCK BARANG KTJ PER {tanggalMulai:ddMMMyyyy} - {tanggalAkhir:ddMMMyyyy}.xlsx"
                            };

                            if (saveFileDialog.ShowDialog(mainform) == DialogResult.OK)
                            {
                                string savePath = saveFileDialog.FileName;
                                if (File.Exists(savePath))
                                {
                                    File.Delete(savePath);
                                }
                                xlWorkBook.SaveCopyAs(savePath);
                                MessageBox.Show(mainform,
                                                        "Export selesai ke: " + savePath,
                                                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }));

                        xlWorkBook.Close(false);
                        xlApp.Quit();

                        Marshal.ReleaseComObject(xlWorkSheet);
                        Marshal.ReleaseComObject(xlWorkBook);
                        Marshal.ReleaseComObject(xlApp);

                        xlWorkSheet = null;
                        xlWorkBook = null;
                        xlApp = null;

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            loading.Close();
                            mainform.Enabled = true;
                            MessageBox.Show("Error: " + ex.Message);
                        }));
                    }
                });
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
            ExportToExcelBulan();
        }
    }
}
