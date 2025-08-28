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
        SqlConnection conn = Koneksi.GetConnection();

        public laporanpersediaan()
        {
            InitializeComponent();
        }

        private DataTable GetDataFromSPbulan(string spName, int bulan, int tahun)
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Bulan", bulan);
                cmd.Parameters.AddWithValue("@Tahun", tahun);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        private void loadsp1()
        {
            int bulan = datejadwal.Value.Month;
            int tahun = datejadwal.Value.Year;

            DataTable dt1 = GetDataFromSPbulan("sp_LaporanPersediaanMaterial", bulan, tahun);

            dt1.Columns.Add("No", typeof(int)).SetOrdinal(0);

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i]["No"] = i + 1;
            }

            dataGridView1.DataSource = dt1;
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
            dataGridView1.RowTemplate.Height = 34;
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

        private void ExportToExcelBulan()
        {
            try
            {
                int bulan = datejadwal.Value.Month;
                int tahun = datejadwal.Value.Year;
                string namaBulan = new DateTime(tahun, bulan, 1).ToString("MMMM");

                DataTable dtMaterial = GetDataFromSPbulan("sp_LaporanPersediaanMaterial", bulan, tahun);

                Excel.Application xlApp = new Excel.Application();

                string templatePath = Path.Combine(Application.StartupPath, "Stock Barang Template.xlsx");
                Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(templatePath);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                xlWorkSheet.Cells[1, 1] = "LAPORAN PERSEDIAAN MATERIAL DI KUALA TANJUNG";
                xlWorkSheet.Cells[2, 1] = "PT.GENTANUSA GEMILANG";
                xlWorkSheet.Cells[3, 1] = $"Per Tanggal {namaBulan} {tahun}";

                int rowStart = 6;
                int colStart = 1;
                for (int i = 0; i < dtMaterial.Rows.Count; i++)
                {
                    xlWorkSheet.Cells[rowStart + i, colStart] = (i + 1).ToString();

                    for (int j = 0; j < dtMaterial.Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[rowStart + i, colStart + j + 1] =
                            dtMaterial.Rows[i][j].ToString();
                    }
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Simpan File Excel";
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.FileName = $"STOCK BARANG KTJ PER {namaBulan} {tahun}.xlsx";

                try
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = saveFileDialog.FileName;

                        if (File.Exists(savePath))
                        {
                            File.Delete(savePath);
                        }

                        xlWorkBook.SaveCopyAs(savePath);

                        MessageBox.Show("Export selesai ke: " + savePath, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                finally
                {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void datejadwal_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 200);
                pickerForm.Text = "Pilih Bulan & Tahun";

                var screenPos = datejadwal.PointToScreen(DrawingPoint.Empty);
                pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datejadwal.Height);

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
                cmbBulan.SelectedIndex = datejadwal.Value.Month - 1;

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
                    Value = datejadwal.Value.Year,
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
                    datejadwal.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(cmbBulan);
                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private void btncari_Click(object sender, EventArgs e)
        {
            loadsp1();
            btnreset.Enabled = true;
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            datejadwal.Value = DateTime.Now;
            loadsp1();
            btnreset.Enabled = false;
        }

        private void laporanpersediaan_Load(object sender, EventArgs e)
        {
            datejadwal.Value = DateTime.Now;
            loadsp1();
            lbltanggal.Text = "Per " + DateTime.Now.ToString("MMMM yyyy");
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            ExportToExcelBulan();
        }
    }
}
