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

namespace GOS_FxApps
{
    public partial class setperputaran_rod : Form
    {
        public setperputaran_rod()
        {
            InitializeComponent();
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnedit.PerformClick();
            }
        }

        private async void setperputaran_rod_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;
            await TampilAsync();
        }

        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                if (table == "perputaran_rod")
                {
                    await TampilAsync();
                }
            }
            catch
            {                
            }
        }

        private async Task TampilAsync()
        {
            try
            {
                string query = "SELECT hari FROM perputaran_rod";

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var ad = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    lblhari.Text = dt.Rows.Count > 0
                        ? dt.Rows[0]["hari"].ToString() + " Hari"
                        : "Data tidak ditemukan";
                }
            }
            catch
            {
                return;
            }
        }

        private async Task EditDataAsync()
        {
            try
            {
                if (!int.TryParse(txthari.Text, out int jumlahHari))
                {
                    MessageBox.Show("Jumlah Hari harus berupa angka",
                                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (jumlahHari <= 0)
                {
                    MessageBox.Show("Jumlah Hari Tidak Boleh 0",
                                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand("UPDATE perputaran_rod SET hari = @hari, updated_at = GETDATE() WHERE id = 1", conn))
                {
                    cmd.Parameters.AddWithValue("@hari", jumlahHari);
                    await cmd.ExecuteNonQueryAsync();
                }

                MessageBox.Show("Data berhasil diperbarui.", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txthari.Clear();
            }
            catch
            {
                return;
            }
        }

        private async void btnedit_Click(object sender, EventArgs e)
        {
            await EditDataAsync();
        }
    }
}
