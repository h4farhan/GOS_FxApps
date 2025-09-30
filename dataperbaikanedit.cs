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
using Microsoft.Reporting.WinForms;

namespace GOS_FxApps
{
    public partial class dataperbaikanedit : Form
    {
        public static dataperbaikanedit Instance;

        public dataperbaikanedit()
        {
            InitializeComponent();
            Instance = this;
        }

        private void LoadData1()
        {
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, ba, ba1, cr, m, r, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks, catatan FROM perbaikan_m WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", historyPerbaikan.instance.noprimary);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView2.DataSource = dt;
                    dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView2.RowTemplate.Height = 35;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].HeaderText = "Tanggal Perbaikan";
                    dataGridView2.Columns[2].HeaderText = "Shift";
                    dataGridView2.Columns[3].HeaderText = "Nomor ROD";
                    dataGridView2.Columns[4].HeaderText = "Jenis";
                    dataGridView2.Columns[5].HeaderText = "E1 Ers";
                    dataGridView2.Columns[6].HeaderText = "E1 Est";
                    dataGridView2.Columns[7].HeaderText = "E1 Jumlah";
                    dataGridView2.Columns[8].HeaderText = "E2 Ers";
                    dataGridView2.Columns[9].HeaderText = "E2 Cst";
                    dataGridView2.Columns[10].HeaderText = "E2 Cstub";
                    dataGridView2.Columns[11].HeaderText = "E2 Jumlah";
                    dataGridView2.Columns[12].HeaderText = "E3";
                    dataGridView2.Columns[13].HeaderText = "E4";
                    dataGridView2.Columns[14].HeaderText = "S";
                    dataGridView2.Columns[15].HeaderText = "D";
                    dataGridView2.Columns[16].HeaderText = "B";
                    dataGridView2.Columns[17].HeaderText = "BA";
                    dataGridView2.Columns[18].HeaderText = "BA-1";
                    dataGridView2.Columns[19].HeaderText = "CR";
                    dataGridView2.Columns[20].HeaderText = "M";
                    dataGridView2.Columns[21].HeaderText = "R";
                    dataGridView2.Columns[22].HeaderText = "C";
                    dataGridView2.Columns[23].HeaderText = "RL";
                    dataGridView2.Columns[24].HeaderText = "Jumlah";
                    dataGridView2.Columns[25].HeaderText = "Tanggal Penerimaan";
                    dataGridView2.Columns[26].HeaderText = "Diubah";
                    dataGridView2.Columns[27].HeaderText = "Remaks";
                    dataGridView2.Columns[28].HeaderText = "Catatan";
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

        private void LoadData2()
        {
            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    string query = "SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, ba, ba1, cr, m, r, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks, catatan FROM perbaikan_e WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", historyPerbaikan.instance.noprimary);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView1.RowTemplate.Height = 35;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Tanggal Perbaikan";
                    dataGridView1.Columns[2].HeaderText = "Shift";
                    dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                    dataGridView1.Columns[4].HeaderText = "Jenis";
                    dataGridView1.Columns[5].HeaderText = "E1 Ers";
                    dataGridView1.Columns[6].HeaderText = "E1 Est";
                    dataGridView1.Columns[7].HeaderText = "E1 Jumlah";
                    dataGridView1.Columns[8].HeaderText = "E2 Ers";
                    dataGridView1.Columns[9].HeaderText = "E2 Cst";
                    dataGridView1.Columns[10].HeaderText = "E2 Cstub";
                    dataGridView1.Columns[11].HeaderText = "E2 Jumlah";
                    dataGridView1.Columns[12].HeaderText = "E3";
                    dataGridView1.Columns[13].HeaderText = "E4";
                    dataGridView1.Columns[14].HeaderText = "S";
                    dataGridView1.Columns[15].HeaderText = "D";
                    dataGridView1.Columns[16].HeaderText = "B";
                    dataGridView1.Columns[17].HeaderText = "BA";
                    dataGridView1.Columns[18].HeaderText = "BA-1";
                    dataGridView1.Columns[19].HeaderText = "CR";
                    dataGridView1.Columns[20].HeaderText = "M";
                    dataGridView1.Columns[21].HeaderText = "R";
                    dataGridView1.Columns[22].HeaderText = "C";
                    dataGridView1.Columns[23].HeaderText = "RL";
                    dataGridView1.Columns[24].HeaderText = "Jumlah";
                    dataGridView1.Columns[25].HeaderText = "Tanggal Penerimaan";
                    dataGridView1.Columns[26].HeaderText = "Diubah";
                    dataGridView1.Columns[27].HeaderText = "Remaks";
                    dataGridView1.Columns[28].HeaderText = "Catatan";
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

        private reportviewr frmrpt;
        private void formperbaikan()
        {
            int? no = historyPerbaikan.instance.noprimary;
            string nomorrod = historyPerbaikan.instance.nomorrod;

            if (no == null)
            {
                MessageBox.Show("Nomor ROD tidak terdapat.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var adapter = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.perbaikan_mTableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.perbaikan_mDataTable data = adapter.GetData(no.Value);

            var adapter2 = new GOS_FxApps.DataSet.PerbaikanFormTableAdapters.perbaikan_eTableAdapter();
            GOS_FxApps.DataSet.PerbaikanForm.perbaikan_eDataTable data2 = adapter2.GetData(no.Value);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "RiwayatPerbaikan.rdlc");

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();
            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetperbaikanmati", (DataTable)data));
            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("datasetperbaikanedit", (DataTable)data2));

            ReportParameter[] parameters = new ReportParameter[]
                {
            new ReportParameter("no", no.ToString()),
            new ReportParameter("nomorrod", nomorrod.ToString())
                };
            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();

            frmrpt.Show();
        }

        private void dataperbaikanedit_Load(object sender, EventArgs e)
        {
            LoadData1();
            LoadData2();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            formperbaikan();
        }
    }
}
