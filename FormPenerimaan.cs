using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace GOS_FxApps
{
    public partial class FormPenerimaan : Form
    {
         
        public FormPenerimaan()
        {
            InitializeComponent();
        }

        private void FormPenerimaan_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            DateTime tanggal1 = dtp1.Value.Date;
            DateTime tanggal2 = dtp1.Value.AddDays(1).Date;
            string shift = cbShift.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(shift)) 
            {
                MessageBox.Show("Silahkan Pilih Shift Terlebih Dahulu");
                return;
            }

            //ambil data
            var adapter = new GOS_FxApps.DataSet.PenerimaanFormTableAdapters.penerimaan_sTableAdapter();
            GOS_FxApps.DataSet.PenerimaanForm.penerimaan_sDataTable data =  adapter.GetData(tanggal1, tanggal2, shift);

            string reportPath = Path.Combine(Application.StartupPath, "FormPenerimaan.rdlc");

            //Prepare Repoertviewer
            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
            new ReportDataSource("DataSetPenerimaan", (DataTable)data));

            ReportParameter[] parameters = new ReportParameter[]
            {
                new ReportParameter("tanggal1", tanggal1.ToString("yyyy-MM-dd")),
                new ReportParameter("tanggal2", tanggal2.ToString("yyyy-MM-dd")),
                new ReportParameter("shift", shift)
            };
            reportViewer1.LocalReport.SetParameters(parameters);

            reportViewer1.RefreshReport();

        }
    }
}
