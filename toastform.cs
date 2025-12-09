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
    public partial class toastform : Form
    {
        public toastform()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true;
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.Size = new Size(280, 80);

            Label lbl = new Label()
            {
                Text = "⚠ Koneksi terputus...\nMenunggu jaringan pulih...",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White
            };

            this.Controls.Add(lbl);

            // posisi pojok kanan bawah
            this.Left = Screen.PrimaryScreen.WorkingArea.Right - this.Width - 20;
            this.Top = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height - 20;
        }
    }
}
