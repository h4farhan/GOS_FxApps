using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOS_FxApps
{
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Loading...";
            Label lbl = new Label()
            {
                Text = "Sedang memproses data, mohon tunggu...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lbl);
            this.Width = 300;
            this.Height = 100;
        }

        private void FormLoading_Load(object sender, EventArgs e)
        {

        }
    }
}
