using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOS_FxApps
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Koneksi.CekKoneksi())
            {
                MessageBox.Show("Jaringan anda tidak tersedia!!.",
                                "Koneksi Terputus",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            Application.Run(new MainForm());
        }
    }
}
