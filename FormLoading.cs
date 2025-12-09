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
        private Label lbl;
        private ProgressBar spinner;

        public FormLoading()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Loading...";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            spinner = new ProgressBar()
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 40,
                Width = 250,
                Height = 20,
                Dock = DockStyle.Top,
                Margin = new Padding(20)
            };

            lbl = new Label()
            {
                Text = "Sedang memproses data, mohon tunggu...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };

            var panel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(spinner);

            this.Controls.Add(panel);

            this.Width = 350;
            this.Height = 130;
        }
    }
}

