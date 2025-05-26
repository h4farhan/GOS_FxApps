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

namespace GOS_FxApps
{
    public partial class reportviewr : Form
    {
        public reportviewr instace;
        public reportviewr()
        {
            InitializeComponent();
            instace = this;
        }

        public void SetReportParameters(string tgl, string shift, string tim, string nmrrod, string jenis, string stasiun, string e1, 
            string e2, string e3, string s, string d, string b, string ba, string cr, string m, string r, string c, string rl, string jumlah)
        {
            if (string.IsNullOrEmpty(reportViewer1.LocalReport.ReportPath))
            {
                string reportpath = @"E:\GitHub\GOS_FxApps\Penerimaan.rdlc";
                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = reportpath;
            }

            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
        new Microsoft.Reporting.WinForms.ReportParameter("tgl", tgl),
        new Microsoft.Reporting.WinForms.ReportParameter("shift", shift),
        new Microsoft.Reporting.WinForms.ReportParameter("tim", tim),
        new Microsoft.Reporting.WinForms.ReportParameter("nmrrod", nmrrod),
        new Microsoft.Reporting.WinForms.ReportParameter("jenis", jenis),
        new Microsoft.Reporting.WinForms.ReportParameter("stasiun", stasiun),
        new Microsoft.Reporting.WinForms.ReportParameter("e1", e1),
        new Microsoft.Reporting.WinForms.ReportParameter("e2", e2),
        new Microsoft.Reporting.WinForms.ReportParameter("e3", e3),
        new Microsoft.Reporting.WinForms.ReportParameter("s", s),
        new Microsoft.Reporting.WinForms.ReportParameter("d", d),
        new Microsoft.Reporting.WinForms.ReportParameter("b", b),
        new Microsoft.Reporting.WinForms.ReportParameter("ba", ba),
        new Microsoft.Reporting.WinForms.ReportParameter("cr", cr),
        new Microsoft.Reporting.WinForms.ReportParameter("m", m),
        new Microsoft.Reporting.WinForms.ReportParameter("r", r),
        new Microsoft.Reporting.WinForms.ReportParameter("c", c),
        new Microsoft.Reporting.WinForms.ReportParameter("rl", rl),
        new Microsoft.Reporting.WinForms.ReportParameter("jumlah", jumlah)
            };

            this.reportViewer1.LocalReport.SetParameters(p);
            this.reportViewer1.RefreshReport();
        }

        private void reportviewr_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
