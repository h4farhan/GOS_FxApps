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
    public partial class aturjam : Form
    {
        public aturjam()
        {
            InitializeComponent();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aturjam_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 24; i++)
                cbjam.Items.Add(i.ToString("D2"));

            // isi combo menit (00–59)
            for (int i = 0; i < 60; i++)
                cbmenit.Items.Add(i.ToString("D2"));

            cbjam.SelectedIndex = 0;
            cbmenit.SelectedIndex = 0;

            date.Value = DateTime.Now;
            cbjam.DropDownStyle = ComboBoxStyle.DropDown;
            cbjam.MaxDropDownItems = 10;
            cbjam.DropDownHeight = 300;
            cbmenit.DropDownStyle = ComboBoxStyle.DropDown;
            cbmenit.MaxDropDownItems = 10;
            cbmenit.DropDownHeight = 300;
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            MainForm.Instance.isManual = false;
            MainForm.Instance.tanggal = DateTime.Now;
            MainForm.Instance.lbldate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy  [HH:mm:ss]");
            MainForm.Instance.shiftcontrol();
            MainForm.Instance.StopAlert();

            // timer tetap jalan, tapi kembali pakai waktu sistem
            MainForm.Instance.jam.Start();

            this.Close();
        }


        private void btnset_Click(object sender, EventArgs e)
        {
            DateTime tanggalManual = date.Value.Date;
            int jam = int.Parse(cbjam.SelectedItem.ToString());
            int menit = int.Parse(cbmenit.SelectedItem.ToString());

            DateTime waktuManual = tanggalManual.AddHours(jam).AddMinutes(menit);

            MainForm.Instance.isManual = true;
            MainForm.Instance.tanggal = waktuManual;
            MainForm.Instance.lbldate.Text = waktuManual.ToString("dddd, dd MMMM yyyy  [HH:mm:ss]");
            MainForm.Instance.shiftcontrol();

            if (this.Owner is MainForm main)
            {
                main.StartAlert();
            }

            // timer tetap jalan supaya jam manual ikut maju
            MainForm.Instance.jam.Start();

            this.Close();
        }

    }
}
