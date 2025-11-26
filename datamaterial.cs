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

namespace GOS_FxApps
{
    public partial class datamaterial : Form
    {
        public datamaterial()
        {
            InitializeComponent();
        }

        private DataTable GetDataFromSPtanggal(string spName, DateTime tanggalMulai, DateTime tanggalAkhir)
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tanggalMulai", tanggalMulai);
                cmd.Parameters.AddWithValue("@tanggalAkhir", tanggalAkhir);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void loadsp1()
        {

            dataGridView1.AutoGenerateColumns = true;

            DateTime tanggalMulai = datejadwalMulai.Value.Date;
            DateTime tanggalAkhir = datejadwalAkhir.Value.Date;

            DataTable dt1 = GetDataFromSPtanggal("sp_LaporanDataMaterial", tanggalMulai, tanggalAkhir);

            dt1.Columns.Add("No", typeof(int)).SetOrdinal(0);

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i]["No"] = i + 1;
            }

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView1.RowTemplate.Height = 70;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.DataSource = dt1;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns["No"].Width = label5.Width;
            dataGridView1.Columns["Periode"].Width = label12.Width;
            dataGridView1.Columns["kodeBarang"].Width = label4.Width;
            dataGridView1.Columns["namaBarang"].Width = label8.Width;
            dataGridView1.Columns["spesifikasi"].Width = label9.Width;
            dataGridView1.Columns["uom"].Width = label15.Width;
            dataGridView1.Columns["StokAwal"].Width = label7.Width;

            dataGridView1.Columns["Masuk1"].Width = label6.Width;
            dataGridView1.Columns["Masuk2"].Width = label10.Width;
            dataGridView1.Columns["Masuk3"].Width = label14.Width;
            dataGridView1.Columns["Masuk4"].Width = label13.Width;
            dataGridView1.Columns["Masuk5"].Width = label11.Width;
            dataGridView1.Columns["Masuk6"].Width = label20.Width;
            dataGridView1.Columns["Masuk7"].Width = label19.Width;
            dataGridView1.Columns["Masuk8"].Width = label16.Width;
            dataGridView1.Columns["Masuk9"].Width = label18.Width;
            dataGridView1.Columns["Masuk10"].Width = label17.Width;
            dataGridView1.Columns["Masuk11"].Width = label30.Width;
            dataGridView1.Columns["Masuk12"].Width = label29.Width;
            dataGridView1.Columns["Masuk13"].Width = label26.Width;
            dataGridView1.Columns["Masuk14"].Width = label28.Width;
            dataGridView1.Columns["Masuk15"].Width = label27.Width;
            dataGridView1.Columns["Masuk16"].Width = label25.Width;
            dataGridView1.Columns["Masuk17"].Width = label24.Width;
            dataGridView1.Columns["Masuk18"].Width = label21.Width;
            dataGridView1.Columns["Masuk19"].Width = label23.Width;
            dataGridView1.Columns["Masuk20"].Width = label22.Width;
            dataGridView1.Columns["Masuk21"].Width = label40.Width;
            dataGridView1.Columns["Masuk22"].Width = label39.Width;
            dataGridView1.Columns["Masuk23"].Width = label36.Width;
            dataGridView1.Columns["Masuk24"].Width = label38.Width;
            dataGridView1.Columns["Masuk25"].Width = label37.Width;
            dataGridView1.Columns["Masuk26"].Width = label35.Width;
            dataGridView1.Columns["Masuk27"].Width = label34.Width;
            dataGridView1.Columns["Masuk28"].Width = label31.Width;
            dataGridView1.Columns["Masuk29"].Width = label33.Width;
            dataGridView1.Columns["Masuk30"].Width = label32.Width;
            dataGridView1.Columns["Masuk31"].Width = label41.Width;

            dataGridView1.Columns["Keluar1"].Width = label72.Width;
            dataGridView1.Columns["Keluar2"].Width = label71.Width;
            dataGridView1.Columns["Keluar3"].Width = label68.Width;
            dataGridView1.Columns["Keluar4"].Width = label70.Width;
            dataGridView1.Columns["Keluar5"].Width = label69.Width;
            dataGridView1.Columns["Keluar6"].Width = label67.Width;
            dataGridView1.Columns["Keluar7"].Width = label66.Width;
            dataGridView1.Columns["Keluar8"].Width = label63.Width;
            dataGridView1.Columns["Keluar9"].Width = label65.Width;
            dataGridView1.Columns["Keluar10"].Width = label64.Width;
            dataGridView1.Columns["Keluar11"].Width = label62.Width;
            dataGridView1.Columns["Keluar12"].Width = label61.Width;
            dataGridView1.Columns["Keluar13"].Width = label58.Width;
            dataGridView1.Columns["Keluar14"].Width = label60.Width;
            dataGridView1.Columns["Keluar15"].Width = label59.Width;
            dataGridView1.Columns["Keluar16"].Width = label57.Width;
            dataGridView1.Columns["Keluar17"].Width = label56.Width;
            dataGridView1.Columns["Keluar18"].Width = label53.Width;
            dataGridView1.Columns["Keluar19"].Width = label55.Width;
            dataGridView1.Columns["Keluar20"].Width = label54.Width;
            dataGridView1.Columns["Keluar21"].Width = label52.Width;
            dataGridView1.Columns["Keluar22"].Width = label51.Width;
            dataGridView1.Columns["Keluar23"].Width = label48.Width;
            dataGridView1.Columns["Keluar24"].Width = label50.Width;
            dataGridView1.Columns["Keluar25"].Width = label49.Width;
            dataGridView1.Columns["Keluar26"].Width = label47.Width;
            dataGridView1.Columns["Keluar27"].Width = label46.Width;
            dataGridView1.Columns["Keluar28"].Width = label43.Width;
            dataGridView1.Columns["Keluar29"].Width = label45.Width;
            dataGridView1.Columns["Keluar30"].Width = label44.Width;
            dataGridView1.Columns["Keluar31"].Width = label42.Width;

            if (dataGridView1.Columns.Contains("foto"))
            {
                int fotoIndex = dataGridView1.Columns["foto"].Index;

                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "foto";
                imgCol.HeaderText = "Foto";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imgCol.DataPropertyName = "foto";

                dataGridView1.Columns.Remove("foto");
                dataGridView1.Columns.Insert(fotoIndex, imgCol);

                dataGridView1.Columns["foto"].Width = label73.Width;
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

                await Task.Run(() =>
                {
                    try
                    {
                        DateTime tanggalMulai = datejadwalMulai.Value.Date;
                        DateTime tanggalAkhir = datejadwalAkhir.Value.Date;
                        
                        string namaBulan = $"{tanggalMulai:dd MMMM yyyy} s/d {tanggalAkhir:dd MMMM yyyy}";

                        DataTable dtMaterial = GetDataFromSPtanggal("sp_LaporanDataMaterial", tanggalMulai, tanggalAkhir);

                        Excel.Application xlApp = new Excel.Application();
                        string templatePath = Path.Combine(Application.StartupPath, "Data Barang Template.xlsx");
                        Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(templatePath);
                        Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                        int rowStart = 3;
                        int colStart = 1;
                        for (int i = 0; i < dtMaterial.Rows.Count; i++)
                        {
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

                                            float cmToPoint = 28.35f;
                                            pic.Height = 3.5f * cmToPoint;

                                            pic.Left = (float)cell.Left + ((float)cell.Width - pic.Width) / 2;
                                            pic.Top = (float)cell.Top + ((float)cell.Height - pic.Height) / 2;
                                        }
                                    }
                                }
                                else
                                {
                                    xlWorkSheet.Cells[rowStart + i, colStart + j + 1] = dtMaterial.Rows[i][j].ToString();
                                }
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
                                FileName = $"DATA BARANG KTJ PER {tanggalMulai:ddMMMyyyy} - {tanggalAkhir:ddMMMyyyy}.xlsx"
                            };

                            if (saveFileDialog.ShowDialog(mainform) == DialogResult.OK)
                            {
                                string savePath = saveFileDialog.FileName;
                                if (File.Exists(savePath)) File.Delete(savePath);

                                xlWorkBook.SaveCopyAs(savePath);
                                MessageBox.Show(mainform, "Export selesai ke: " + savePath,
                                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

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
                        }));
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

        private void btncari_Click(object sender, EventArgs e)
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

            loadsp1();
            lbltanggal.Text = "Per " + datejadwalMulai.Value.ToString("dd MMM yyyy") + " - " + datejadwalAkhir.Value.ToString("dd MMM yyyy");
            btnreset.Enabled = true;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
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
