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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Runtime.InteropServices;

namespace GOS_FxApps
{
    public partial class EstimasiPemakaianMaterial : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public EstimasiPemakaianMaterial()
        {
            InitializeComponent();         
        }

        private DataTable GetDataFromSPHari(string spName, int hari, int bulan, int tahun)
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hari", hari);
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
            int hari = DateTime.Now.Day;
            int bulan = DateTime.Now.Month;
            int tahun = DateTime.Now.Year;

            DataTable dt1 = GetDataFromSPHari("sp_koefisiensiMaterialCosthari", hari, bulan, tahun);
            DataTable dt2 = GetDataFromSPHari("sp_koefisiensiConsumableCosthari", hari, bulan, tahun);
            DataTable dt3 = GetDataFromSPHari("sp_koefisiensiSafetyCosthari", hari, bulan, tahun);

            DataTable finalDt = dt1.Copy();

            finalDt.Merge(dt2);
            finalDt.Merge(dt3);

            finalDt.Columns.Add("No", typeof(int)).SetOrdinal(0);

            for (int i = 0; i < finalDt.Rows.Count; i++)
            {
                finalDt.Rows[i]["No"] = i + 1;
            }

            dataGridView1.DataSource = finalDt;
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

            dataGridView1.Columns["Juli"].Visible = false;
            dataGridView1.Columns["Agustus"].Visible = false;
            dataGridView1.Columns["September"].Visible = false;
            dataGridView1.Columns["Oktober"].Visible = false;
            dataGridView1.Columns["November"].Visible = false;
            dataGridView1.Columns["Desember"].Visible = false;
            dataGridView1.Columns["Januari"].Visible = false;
            dataGridView1.Columns["Februari"].Visible = false;
            dataGridView1.Columns["Maret"].Visible = false;
            dataGridView1.Columns["April"].Visible = false;
            dataGridView1.Columns["Mei"].Visible = false;
            dataGridView1.Columns["Juni"].Visible = false;

            dataGridView1.Columns["No"].Width = label5.Width;       
            dataGridView1.Columns["Deskripsi"].Width = label8.Width;
            dataGridView1.Columns["Spesifikasi"].Width = label9.Width;
            dataGridView1.Columns["Satuan"].Width = label15.Width;
            dataGridView1.Columns["Koef E1"].Width = label23.Width;
            dataGridView1.Columns["Hasil E1"].Width = label24.Width;
            dataGridView1.Columns["Koef E2"].Width = label26.Width;
            dataGridView1.Columns["Hasil E2"].Width = label28.Width;
            dataGridView1.Columns["Koef E3"].Width = label30.Width;
            dataGridView1.Columns["Hasil E3"].Width = label32.Width;

            dataGridView1.Columns["Koef E4"].Width = label34.Width;
            dataGridView1.Columns["Hasil E4"].Width = label36.Width;
            dataGridView1.Columns["Koef S"].Width = label38.Width;
            dataGridView1.Columns["Hasil S"].Width = label25.Width;
            dataGridView1.Columns["Koef D"].Width = label40.Width;
            dataGridView1.Columns["Hasil D"].Width = label27.Width;

            dataGridView1.Columns["Koef B"].Width = label42.Width;
            dataGridView1.Columns["Hasil B"].Width = label29.Width;
            dataGridView1.Columns["Koef BA"].Width = label44.Width;
            dataGridView1.Columns["Hasil BA"].Width = label31.Width;
            dataGridView1.Columns["Koef BA1"].Width = label46.Width;
            dataGridView1.Columns["Hasil BA1"].Width = label33.Width;

            dataGridView1.Columns["Koef R"].Width = label48.Width;
            dataGridView1.Columns["Hasil R"].Width = label35.Width;
            dataGridView1.Columns["Koef M"].Width = label37.Width;
            dataGridView1.Columns["Hasil M"].Width = label39.Width;
            dataGridView1.Columns["Koef CR"].Width = label41.Width;
            dataGridView1.Columns["Hasil CR"].Width = label43.Width;

            dataGridView1.Columns["Koef C"].Width = label45.Width;
            dataGridView1.Columns["Hasil C"].Width = label47.Width;
            dataGridView1.Columns["Koef RL"].Width = label50.Width;
            dataGridView1.Columns["Hasil RL"].Width = label49.Width;


            dataGridView1.Columns["Total"].Width = label51.Width;
            dataGridView1.Columns["Rata-rata per Hari"].Width = label52.Width;
            dataGridView1.Columns["TotalPemakaian"].Width = label53.Width;
            dataGridView1.Columns["RataPerHariKumulatif"].Width = label54.Width;
            dataGridView1.Columns["UoM2"].Width = label55.Width;
            dataGridView1.Columns["bq"].Width = label57.Width;
            dataGridView1.Columns["aktual"].Width = label56.Width;
            dataGridView1.Columns["Persentase"].Width = label59.Width;
        }
        private void ExportToExcelHari()
        {
            try
            {
                int hari = DateTime.Now.Day;
                int bulan = DateTime.Now.Month;
                int tahun = DateTime.Now.Year;

                DataTable dtMaterial = GetDataFromSPHari("sp_koefisiensiMaterialCosthari", hari, bulan, tahun);
                DataTable dtConsumable = GetDataFromSPHari("sp_koefisiensiConsumableCosthari", hari, bulan, tahun);
                DataTable dtsafety = GetDataFromSPHari("sp_koefisiensiSafetyCosthari", hari, bulan, tahun);
                DataTable dtQty = GetDataFromSPHari("koefisiensiqtyhari", hari, bulan, tahun);

                Excel.Application xlApp = new Excel.Application();

                string templatePath = Path.Combine(Application.StartupPath, "Koefisien Material.xlsx");
                Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(templatePath);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                xlWorkSheet.Cells[1, 1] = "BQ PEKERJAAN ROD REPAIR SHOP PERIODE TAHUN " + tahun + "/" + (tahun + 1);
                xlWorkSheet.Cells[2, 1] = "UoM = Unit of Measure,     U /Price = Unit Price,     Coeff. = Coefficient,     E1, E2, E3 = Erotion,     " +
                    "S = Sticking,     D= Deformation,     B = Bending,     BA = BA Clade Change,     R = Spark,     CR = Crack York,     M = Crack MIG,     C = End Cut,     RL = Rod Long";

                if (dtMaterial.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblMaterial = xlWorkSheet.ListObjects["Table6"];
                    foreach (DataRow dr in dtMaterial.Rows)
                    {
                        Excel.ListRow newRow = tblMaterial.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtMaterial.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtMaterial.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
                        }
                    }
                }

                if (dtConsumable.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblConsumable = xlWorkSheet.ListObjects["Table1"];
                    foreach (DataRow dr in dtConsumable.Rows)
                    {
                        Excel.ListRow newRow = tblConsumable.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtConsumable.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtConsumable.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
                        }
                    }
                }

                if (dtsafety.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblSafety = xlWorkSheet.ListObjects["Table2"];
                    foreach (DataRow dr in dtsafety.Rows)
                    {
                        Excel.ListRow newRow = tblSafety.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtsafety.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtsafety.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
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

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Simpan File Excel";
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.FileName = "Koefisien Material Tanggal " + hari + " " + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

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


        private DataTable GetDataFromSPBulan(string spName, int bulan, int tahun)
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
        private void loadsp2()
        {
            int bulan = DateTime.Now.Month;
            int tahun = DateTime.Now.Year;

            DataTable dt1 = GetDataFromSPBulan("sp_koefisiensiMaterialCostbulan", bulan, tahun);
            DataTable dt2 = GetDataFromSPBulan("sp_koefisiensiConsumableCostbulan", bulan, tahun);
            DataTable dt3 = GetDataFromSPBulan("sp_koefisiensiSafetyCostbulan", bulan, tahun);

            DataTable finalDt = dt1.Copy();

            finalDt.Merge(dt2);
            finalDt.Merge(dt3);

            finalDt.Columns.Add("No", typeof(int)).SetOrdinal(0);

            for (int i = 0; i < finalDt.Rows.Count; i++)
            {
                finalDt.Rows[i]["No"] = i + 1;
            }

            dataGridView1.DataSource = finalDt;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;


            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            dataGridView1.Columns["Juli"].Visible = false;
            dataGridView1.Columns["Agustus"].Visible = false;
            dataGridView1.Columns["September"].Visible = false;
            dataGridView1.Columns["Oktober"].Visible = false;
            dataGridView1.Columns["November"].Visible = false;
            dataGridView1.Columns["Desember"].Visible = false;
            dataGridView1.Columns["Januari"].Visible = false;
            dataGridView1.Columns["Februari"].Visible = false;
            dataGridView1.Columns["Maret"].Visible = false;
            dataGridView1.Columns["April"].Visible = false;
            dataGridView1.Columns["Mei"].Visible = false;
            dataGridView1.Columns["Juni"].Visible = false;

            dataGridView1.Columns["No"].Width = label5.Width;
            dataGridView1.Columns["Deskripsi"].Width = label8.Width;
            dataGridView1.Columns["Spesifikasi"].Width = label9.Width;
            dataGridView1.Columns["Satuan"].Width = label15.Width;
            dataGridView1.Columns["Koef E1"].Width = label23.Width;
            dataGridView1.Columns["Hasil E1"].Width = label24.Width;
            dataGridView1.Columns["Koef E2"].Width = label26.Width;
            dataGridView1.Columns["Hasil E2"].Width = label28.Width;
            dataGridView1.Columns["Koef E3"].Width = label30.Width;
            dataGridView1.Columns["Hasil E3"].Width = label32.Width;

            dataGridView1.Columns["Koef E4"].Width = label34.Width;
            dataGridView1.Columns["Hasil E4"].Width = label36.Width;
            dataGridView1.Columns["Koef S"].Width = label38.Width;
            dataGridView1.Columns["Hasil S"].Width = label25.Width;
            dataGridView1.Columns["Koef D"].Width = label40.Width;
            dataGridView1.Columns["Hasil D"].Width = label27.Width;

            dataGridView1.Columns["Koef B"].Width = label42.Width;
            dataGridView1.Columns["Hasil B"].Width = label29.Width;
            dataGridView1.Columns["Koef BA"].Width = label44.Width;
            dataGridView1.Columns["Hasil BA"].Width = label31.Width;
            dataGridView1.Columns["Koef BA1"].Width = label46.Width;
            dataGridView1.Columns["Hasil BA1"].Width = label33.Width;

            dataGridView1.Columns["Koef R"].Width = label48.Width;
            dataGridView1.Columns["Hasil R"].Width = label35.Width;
            dataGridView1.Columns["Koef M"].Width = label37.Width;
            dataGridView1.Columns["Hasil M"].Width = label39.Width;
            dataGridView1.Columns["Koef CR"].Width = label41.Width;
            dataGridView1.Columns["Hasil CR"].Width = label43.Width;

            dataGridView1.Columns["Koef C"].Width = label45.Width;
            dataGridView1.Columns["Hasil C"].Width = label47.Width;
            dataGridView1.Columns["Koef RL"].Width = label50.Width;
            dataGridView1.Columns["Hasil RL"].Width = label49.Width;


            dataGridView1.Columns["Total"].Width = label51.Width;
            dataGridView1.Columns["Rata-rata per Hari"].Width = label52.Width;
            dataGridView1.Columns["TotalPemakaian"].Width = label53.Width;
            dataGridView1.Columns["RataPerHariKumulatif"].Width = label54.Width;
            dataGridView1.Columns["UoM2"].Width = label55.Width;
            dataGridView1.Columns["bq"].Width = label57.Width;
            dataGridView1.Columns["aktual"].Width = label56.Width;
            dataGridView1.Columns["Persentase"].Width = label59.Width;
        }
        private void loadsp3()
        {
            int bulan = DateTime.Now.Month;
            int tahun = DateTime.Now.Year;

            DataTable dt1 = GetDataFromSPBulan("sp_koefisiensiMaterialCosttahun", bulan, tahun);
            DataTable dt2 = GetDataFromSPBulan("sp_koefisiensiConsumableCosttahun", bulan, tahun);
            DataTable dt3 = GetDataFromSPBulan("sp_koefisiensiSafetyCosttahun", bulan, tahun);

            DataTable finalDt = dt1.Copy();

            finalDt.Merge(dt2);
            finalDt.Merge(dt3);

            finalDt.Columns.Add("No", typeof(int)).SetOrdinal(0);

            for (int i = 0; i < finalDt.Rows.Count; i++)
            {
                finalDt.Rows[i]["No"] = i + 1;
            }

            dataGridView1.DataSource = finalDt;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            dataGridView1.Columns["Juli"].Visible = false;
            dataGridView1.Columns["Agustus"].Visible = false;
            dataGridView1.Columns["September"].Visible = false;
            dataGridView1.Columns["Oktober"].Visible = false;
            dataGridView1.Columns["November"].Visible = false;
            dataGridView1.Columns["Desember"].Visible = false;
            dataGridView1.Columns["Januari"].Visible = false;
            dataGridView1.Columns["Februari"].Visible = false;
            dataGridView1.Columns["Maret"].Visible = false;
            dataGridView1.Columns["April"].Visible = false;
            dataGridView1.Columns["Mei"].Visible = false;
            dataGridView1.Columns["Juni"].Visible = false;

            dataGridView1.Columns["No"].Width = label5.Width;
            dataGridView1.Columns["Deskripsi"].Width = label8.Width;
            dataGridView1.Columns["Spesifikasi"].Width = label9.Width;
            dataGridView1.Columns["Satuan"].Width = label15.Width;
            dataGridView1.Columns["Koef E1"].Width = label23.Width;
            dataGridView1.Columns["Hasil E1"].Width = label24.Width;
            dataGridView1.Columns["Koef E2"].Width = label26.Width;
            dataGridView1.Columns["Hasil E2"].Width = label28.Width;
            dataGridView1.Columns["Koef E3"].Width = label30.Width;
            dataGridView1.Columns["Hasil E3"].Width = label32.Width;

            dataGridView1.Columns["Koef E4"].Width = label34.Width;
            dataGridView1.Columns["Hasil E4"].Width = label36.Width;
            dataGridView1.Columns["Koef S"].Width = label38.Width;
            dataGridView1.Columns["Hasil S"].Width = label25.Width;
            dataGridView1.Columns["Koef D"].Width = label40.Width;
            dataGridView1.Columns["Hasil D"].Width = label27.Width;

            dataGridView1.Columns["Koef B"].Width = label42.Width;
            dataGridView1.Columns["Hasil B"].Width = label29.Width;
            dataGridView1.Columns["Koef BA"].Width = label44.Width;
            dataGridView1.Columns["Hasil BA"].Width = label31.Width;
            dataGridView1.Columns["Koef BA1"].Width = label46.Width;
            dataGridView1.Columns["Hasil BA1"].Width = label33.Width;

            dataGridView1.Columns["Koef R"].Width = label48.Width;
            dataGridView1.Columns["Hasil R"].Width = label35.Width;
            dataGridView1.Columns["Koef M"].Width = label37.Width;
            dataGridView1.Columns["Hasil M"].Width = label39.Width;
            dataGridView1.Columns["Koef CR"].Width = label41.Width;
            dataGridView1.Columns["Hasil CR"].Width = label43.Width;

            dataGridView1.Columns["Koef C"].Width = label45.Width;
            dataGridView1.Columns["Hasil C"].Width = label47.Width;
            dataGridView1.Columns["Koef RL"].Width = label50.Width;
            dataGridView1.Columns["Hasil RL"].Width = label49.Width;


            dataGridView1.Columns["Total"].Width = label51.Width;
            dataGridView1.Columns["Rata-rata per Hari"].Width = label52.Width;
            dataGridView1.Columns["TotalPemakaian"].Width = label53.Width;
            dataGridView1.Columns["RataPerHariKumulatif"].Width = label54.Width;
            dataGridView1.Columns["UoM2"].Width = label55.Width;
            dataGridView1.Columns["bq"].Width = label57.Width;
            dataGridView1.Columns["aktual"].Width = label56.Width;
            dataGridView1.Columns["Persentase"].Width = label59.Width;
        }
        private void ExportToExcelBulan()
        {
            try
            {
                int bulan = DateTime.Now.Month;
                int tahun = DateTime.Now.Year;

                DataTable dtMaterial = GetDataFromSPBulan("sp_koefisiensiMaterialCostbulan", bulan, tahun);
                DataTable dtConsumable = GetDataFromSPBulan("sp_koefisiensiConsumableCostbulan", bulan, tahun);
                DataTable dtsafety = GetDataFromSPBulan("sp_koefisiensiSafetyCostbulan", bulan, tahun);
                DataTable dtQty = GetDataFromSPBulan("koefisiensiqtybulan", bulan, tahun);

                Excel.Application xlApp = new Excel.Application();

                string templatePath = Path.Combine(Application.StartupPath, "Koefisien Material.xlsx");
                Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(templatePath);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                xlWorkSheet.Cells[1, 1] = "BQ PEKERJAAN ROD REPAIR SHOP PERIODE TAHUN " + tahun + "/" + (tahun + 1);
                xlWorkSheet.Cells[2, 1] = "UoM = Unit of Measure,     U /Price = Unit Price,     Coeff. = Coefficient,     E1, E2, E3 = Erotion,     " +
                    "S = Sticking,     D= Deformation,     B = Bending,     BA = BA Clade Change,     R = Spark,     CR = Crack York,     M = Crack MIG,     C = End Cut,     RL = Rod Long";

                if (dtMaterial.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblMaterial = xlWorkSheet.ListObjects["Table6"];
                    foreach (DataRow dr in dtMaterial.Rows)
                    {
                        Excel.ListRow newRow = tblMaterial.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtMaterial.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtMaterial.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
                        }
                    }
                }

                if (dtConsumable.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblConsumable = xlWorkSheet.ListObjects["Table1"];
                    foreach (DataRow dr in dtConsumable.Rows)
                    {
                        Excel.ListRow newRow = tblConsumable.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtConsumable.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtConsumable.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
                        }
                    }
                }

                if (dtsafety.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblSafety = xlWorkSheet.ListObjects["Table2"];
                    foreach (DataRow dr in dtsafety.Rows)
                    {
                        Excel.ListRow newRow = tblSafety.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtsafety.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtsafety.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
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

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Simpan File Excel";
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.FileName = "Koefisien Material Bulan " + bulan + " " + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

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
        private DataTable GetDataFromSPTahun(string spName, int tahun)
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tahun", tahun);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        private void ExportToExcelTahun()
        {
            try
            {
                int bulan = DateTime.Now.Month;
                int tahun = DateTime.Now.Year;

                DataTable dtMaterial = GetDataFromSPBulan("sp_koefisiensiMaterialCosttahun", bulan, tahun);
                DataTable dtConsumable = GetDataFromSPBulan("sp_koefisiensiConsumableCosttahun", bulan, tahun);
                DataTable dtsafety = GetDataFromSPBulan("sp_koefisiensiSafetyCosttahun", bulan, tahun);
                DataTable dtQty = GetDataFromSPTahun("koefisiensiqtytahun", tahun);

                Excel.Application xlApp = new Excel.Application();

                string templatePath = Path.Combine(Application.StartupPath, "Koefisien Material.xlsx");
                Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(templatePath);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];

                xlWorkSheet.Cells[1, 1] = "BQ PEKERJAAN ROD REPAIR SHOP PERIODE TAHUN " + tahun + "/" + (tahun + 1);
                xlWorkSheet.Cells[2, 1] = "UoM = Unit of Measure,     U /Price = Unit Price,     Coeff. = Coefficient,     E1, E2, E3 = Erotion,     " +
                    "S = Sticking,     D= Deformation,     B = Bending,     BA = BA Clade Change,     R = Spark,     CR = Crack York,     M = Crack MIG,     C = End Cut,     RL = Rod Long";

                if (dtMaterial.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblMaterial = xlWorkSheet.ListObjects["Table6"];
                    foreach (DataRow dr in dtMaterial.Rows)
                    {
                        Excel.ListRow newRow = tblMaterial.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtMaterial.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtMaterial.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
                        }
                    }
                }

                if (dtConsumable.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblConsumable = xlWorkSheet.ListObjects["Table1"];
                    foreach (DataRow dr in dtConsumable.Rows)
                    {
                        Excel.ListRow newRow = tblConsumable.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtConsumable.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtConsumable.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
                        }
                    }
                }

                if (dtsafety.Rows.Count > 0)
                {
                    int nomor = 1;
                    Excel.ListObject tblSafety = xlWorkSheet.ListObjects["Table2"];
                    foreach (DataRow dr in dtsafety.Rows)
                    {
                        Excel.ListRow newRow = tblSafety.ListRows.Add();
                        newRow.Range[1, 1].Value2 = nomor++;
                        for (int j = 0; j < dtsafety.Columns.Count; j++)
                        {
                            int targetCol = j + 2;
                            if (dtsafety.Columns[j].ColumnName == "Persentase")
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString() + " %";
                            else
                                newRow.Range[1, targetCol].Value2 = dr[j].ToString();
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

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Simpan File Excel";
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.FileName = "Koefisien Material Tahun " + tahun + " " + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

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

        private void cmbpilihdata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedItem.ToString().Equals("Hari Ini"))
            {
                loadsp1();
            }
            else if(cmbpilihdata.SelectedItem.ToString().Equals("Bulan Ini"))
            {
                loadsp2();
            }
            else if(cmbpilihdata.SelectedItem.ToString().Equals("Tahun Ini"))
            {
                loadsp3();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (cmbpilihdata.SelectedItem.ToString().Equals("Hari Ini"))
            {
                ExportToExcelHari();
            }
            else if (cmbpilihdata.SelectedItem.ToString().Equals("Bulan Ini"))
            {
                ExportToExcelBulan();
            }
            else if (cmbpilihdata.SelectedItem.ToString().Equals("Tahun Ini"))
            {
                ExportToExcelTahun();
            }
        }

        private void EstimasiPemakaianMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void EstimasiPemakaianMaterial_Load(object sender, EventArgs e)
        {
            cmbpilihdata.SelectedIndex = 0;
            loadsp1();
            lbltanggal.Text = DateTime.Now.ToString("MMMM yyyy");
        }
    }
}
