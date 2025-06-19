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

        private void reportviewr_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

    }
}
