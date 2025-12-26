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
    public partial class datapenerimaanedit : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public static datapenerimaanedit Instance;

        public datapenerimaanedit()
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
                    string query = "SELECT no,tanggal_penerimaan,shift,nomor_rod,jenis,stasiun,e1,e2,e3,s,d,b,ba,r,m,cr,c,rl,jumlah,updated_at,remaks,catatan FROM penerimaan_m WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", historyPenerimaan.instance.noprimary);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView2.RowTemplate.Height = 35;
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView2.AllowUserToResizeRows = false;
                    dataGridView2.ReadOnly = true;

                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].HeaderText = "Tanggal Penerimaan";
                    dataGridView2.Columns[2].HeaderText = "Shift";
                    dataGridView2.Columns[3].HeaderText = "Nomor ROD";
                    dataGridView2.Columns[4].HeaderText = "Jenis";
                    dataGridView2.Columns[5].HeaderText = "Stasiun";
                    dataGridView2.Columns[6].HeaderText = "E1";
                    dataGridView2.Columns[7].HeaderText = "E2";
                    dataGridView2.Columns[8].HeaderText = "E3";
                    dataGridView2.Columns[9].HeaderText = "S";
                    dataGridView2.Columns[10].HeaderText = "D";
                    dataGridView2.Columns[11].HeaderText = "B";
                    dataGridView2.Columns[12].HeaderText = "BA";
                    dataGridView2.Columns[13].HeaderText = "R";
                    dataGridView2.Columns[14].HeaderText = "M";
                    dataGridView2.Columns[15].HeaderText = "CR";
                    dataGridView2.Columns[16].HeaderText = "C";
                    dataGridView2.Columns[17].HeaderText = "RL";
                    dataGridView2.Columns[18].HeaderText = "Jumlah";
                    dataGridView2.Columns[19].HeaderText = "Diubah";
                    dataGridView2.Columns[20].HeaderText = "Remaks";
                    dataGridView2.Columns[21].HeaderText = "Catatan";
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
                    string query = "SELECT no,tanggal_penerimaan,shift,nomor_rod,jenis,stasiun,e1,e2,e3,s,d,b,ba,r,m,cr,c,rl,jumlah,updated_at,remaks,catatan FROM penerimaan_e WHERE no = @no";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@no", historyPenerimaan.instance.noprimary);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.RowTemplate.Height = 35;
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
                    dataGridView1.AllowUserToResizeRows = false;
                    dataGridView1.ReadOnly = true;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Tanggal Penerimaan";
                    dataGridView1.Columns[2].HeaderText = "Shift";
                    dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                    dataGridView1.Columns[4].HeaderText = "Jenis";
                    dataGridView1.Columns[5].HeaderText = "Stasiun";
                    dataGridView1.Columns[6].HeaderText = "E1";
                    dataGridView1.Columns[7].HeaderText = "E2";
                    dataGridView1.Columns[8].HeaderText = "E3";
                    dataGridView1.Columns[9].HeaderText = "S";
                    dataGridView1.Columns[10].HeaderText = "D";
                    dataGridView1.Columns[11].HeaderText = "B";
                    dataGridView1.Columns[12].HeaderText = "BA";
                    dataGridView1.Columns[13].HeaderText = "R";
                    dataGridView1.Columns[14].HeaderText = "M";
                    dataGridView1.Columns[15].HeaderText = "CR";
                    dataGridView1.Columns[16].HeaderText = "C";
                    dataGridView1.Columns[17].HeaderText = "RL";
                    dataGridView1.Columns[18].HeaderText = "Jumlah";
                    dataGridView1.Columns[19].HeaderText = "Diubah";
                    dataGridView1.Columns[20].HeaderText = "Remaks";
                    dataGridView1.Columns[21].HeaderText = "Catatan";
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
        private void formpenerimaan()
        {
            int? no = historyPenerimaan.instance.noprimary;
            string nomorrod = historyPenerimaan.instance.nomorrod;

            if (no == null)
            {
                MessageBox.Show("Nomor ROD tidak terdapat.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var adapter = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.penerimaan_mTableAdapter();
            GOS_FxApps.DataSet.PenerimaanForm.penerimaan_mDataTable data = adapter.GetData(no.Value);

            var adapter2 = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.penerimaan_eTableAdapter();
            GOS_FxApps.DataSet.PenerimaanForm.penerimaan_eDataTable data2 = adapter2.GetData(no.Value);

            frmrpt = new reportviewr();
            frmrpt.reportViewer1.Reset();
            frmrpt.reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "RiwayatPenerimaan.rdlc");

            frmrpt.reportViewer1.LocalReport.DataSources.Clear();
            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("datasetpenerimanmati", (DataTable)data));
            frmrpt.reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("datasetpenerimaanedit", (DataTable)data2));

            ReportParameter[] parameters = new ReportParameter[]
                {
            new ReportParameter("no", no.ToString()),
            new ReportParameter("nomorrod", nomorrod.ToString())
                };
            frmrpt.reportViewer1.LocalReport.SetParameters(parameters);
            frmrpt.reportViewer1.RefreshReport();

            frmrpt.Show();
        }

        private void datapenerimaanedit_Load(object sender, EventArgs e)
        {
            LoadData1();
            LoadData2();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            formpenerimaan();
        }
    }
}
