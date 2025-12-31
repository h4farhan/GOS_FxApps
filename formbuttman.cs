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
using System.Windows.Input;

namespace GOS_FxApps
{
    public partial class formbuttman : Form
    {
        int noprimary = 0;

        bool infocari = false;
        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;

        public formbuttman()
        {
            InitializeComponent();
        }

        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                switch (table)
                {
                    case "kondisiROD":
                        if (!isSearching)
                        {
                            await HitungTotalData();
                            currentPage = 1;
                            await tampil();
                        }
                        else
                        {
                            int oldTotal = searchTotalRecords;
                            await HitungTotalDataPencarian();
                            if (searchTotalRecords > oldTotal)
                                await tampil();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                return;
            }
        }

        private async Task HitungTotalData()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM kondisiROD";
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        totalRecords = (int)await cmd.ExecuteScalarAsync();
                    }
                }
                totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            }
            catch
            {
                return;
            }
        }

        private async Task HitungTotalDataPencarian()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lastSearchWhere))
                {
                    searchTotalRecords = 0;
                    totalPages = 0;
                    return;
                }

                string countQuery = "SELECT COUNT(*) " + lastSearchWhere;
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (var cmd = new SqlCommand(countQuery, conn))
                    {
                        if (lastSearchCmd?.Parameters.Count > 0)
                        {
                            foreach (SqlParameter p in lastSearchCmd.Parameters)
                                cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                        }

                        searchTotalRecords = (int)await cmd.ExecuteScalarAsync();
                    }
                }

                totalPages = (int)Math.Ceiling(searchTotalRecords / (double)pageSize);
            }
            catch
            {
                return;
            }
        }

        private async Task tampil()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;

                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    string query;

                    if (!isSearching)
                    {
                        query = $@"
                SELECT no,tanggal,shift,butt_ratio,man_power,updated_at,remaks
                FROM kondisiROD
                ORDER BY tanggal DESC, shift ASC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                    }
                    else
                    {
                        query = $@"
                SELECT no,tanggal,shift,butt_ratio,man_power,updated_at,remaks
                {lastSearchWhere}
                ORDER BY tanggal DESC, shift ASC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";

                        foreach (SqlParameter p in lastSearchCmd.Parameters)
                            cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                    }

                    cmd.CommandText = query;

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(dt);
                    }

                    if (dataGridView1.InvokeRequired)
                    {
                        dataGridView1.Invoke(new Action(() =>
                        {
                            UpdateGrid(dt);
                        }));
                    }
                    else
                    {
                        UpdateGrid(dt);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void UpdateGrid(DataTable dt)
        {
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.DataSource = dt;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeRows = false;

            if (dt.Columns.Count >= 7)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Butt Ratio";
                dataGridView1.Columns[4].HeaderText = "Man Power";
                dataGridView1.Columns[5].HeaderText = "Diubah";
                dataGridView1.Columns[6].HeaderText = "Remaks";
            }

            lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

            btnleft.Enabled = currentPage > 1;
            btnright.Enabled = currentPage < totalPages;
        }

        private async Task<bool> cari()
        {
            DateTime? tanggal = datecari.Checked ? (DateTime?)datecari.Value.Date : null;
            bool shiftValid = cbShift.SelectedIndex > 0;

            if (!tanggal.HasValue && !shiftValid)
            {
                MessageBox.Show("Silakan isi tanggal atau shift untuk melakukan pencarian.", "Peringatan");
                return false;
            }
            try
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM kondisiROD WHERE 1=1 ";

                if (tanggal.HasValue)
                {
                    lastSearchWhere += " AND CAST(tanggal AS DATE) = @tgl ";
                    lastSearchCmd.Parameters.AddWithValue("@tgl", tanggal.Value);
                }

                if (shiftValid)
                {
                    lastSearchWhere += " AND shift = @shift ";
                    lastSearchCmd.Parameters.AddWithValue("@shift", cbShift.SelectedItem.ToString());
                }

                await HitungTotalDataPencarian();
                currentPage = 1;
                await tampil();

                btncari.Text = "Reset";
                return true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int ToInt(string text)
        {
            return int.TryParse(text, out int val) ? val : 0;
        }

        private async Task editdata()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (var cmd = new SqlCommand(
                        @"UPDATE kondisiROD 
                  SET butt_ratio = @butt,
                      man_power = @man,
                      updated_at = GETDATE(),
                      remaks = @remaks
                  WHERE no = @no", conn))
                    {
                        cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                        cmd.Parameters.Add("@butt", SqlDbType.Int).Value = ToInt(txtbutt.Text);
                        cmd.Parameters.Add("@man", SqlDbType.Int).Value = ToInt(txtman.Text);
                        cmd.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await Task.Yield();

                MessageBox.Show("Data Berhasil Diedit.", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Gagal Edit Data.");
            }
        }

        private async Task simpandata()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {

                    using (var cekCmd = new SqlCommand(
                        @"SELECT COUNT(1) 
              FROM kondisiROD 
              WHERE tanggal = @tgl AND shift = @shift", conn))
                    {
                        cekCmd.Parameters.Add("@tgl", SqlDbType.Date).Value = date.Value.Date;
                        cekCmd.Parameters.Add("@shift", SqlDbType.VarChar).Value = cmbshift.SelectedItem;

                        int exists = (int)await cekCmd.ExecuteScalarAsync();
                        if (exists > 0)
                        {
                            MessageBox.Show("Data di Tanggal dan Shift ini sudah ada",
                                            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (var cmd = new SqlCommand(
                        @"INSERT INTO kondisiROD
              (tanggal, shift, butt_ratio, man_power, updated_at, remaks)
              VALUES (@tgl,@shift,@butt,@man,GETDATE(),@remaks)", conn))
                    {
                        cmd.Parameters.Add("@tgl", SqlDbType.Date).Value = date.Value.Date;
                        cmd.Parameters.Add("@shift", SqlDbType.VarChar).Value = cmbshift.SelectedItem;
                        cmd.Parameters.Add("@butt", SqlDbType.Int).Value = ToInt(txtbutt.Text);
                        cmd.Parameters.Add("@man", SqlDbType.Int).Value = ToInt(txtman.Text);
                        cmd.Parameters.Add("@remaks", SqlDbType.VarChar).Value = loginform.login.name;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await Task.Yield();

                MessageBox.Show("Data Berhasil Disimpan.", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                setdefault();
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Gagal Simpan Data.");
            }
        }

        private async Task hapusdata()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (var cmd = new SqlCommand("DELETE FROM kondisiROD WHERE no = @no", conn))
                    {
                        cmd.Parameters.Add("@no", SqlDbType.Int).Value = noprimary;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await Task.Yield();

                MessageBox.Show("Data berhasil dihapus.", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Gagal Hapus Data.");
            }
        }


        private void setdefault()
        {
            txtman.Clear();
            txtbutt.Clear();
            cmbshift.StartIndex = -1;
            date.Value = DateTime.Now.Date;
        }

        private void AngkaOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnsimpan.PerformClick();
            }
        }

        private async void btnsimpan_Click(object sender, EventArgs e)
        {
            if (cmbshift.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtbutt.Text) ||
                string.IsNullOrWhiteSpace(txtman.Text))
            {
                MessageBox.Show("Harap lengkapi data terlebih dahulu.",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            btnsimpan.Enabled = false;
            btndelete.Enabled = false;

            if (btnsimpan.Text == "Simpan Data")
            {
                await simpandata();
            }
            else if (btnsimpan.Text == "Edit Data")
            {
                await editdata();
                btnsimpan.Text = "Simpan Data";
                btndelete.Text = "Batal";
                btnbatal.Visible = false;
                noprimary = 0;
            }

            setdefault();
            date.Enabled = true;
            cmbshift.Enabled = true;

            btnsimpan.Enabled = false;
            btndelete.Enabled = false;
        }

        private async void formbuttman_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            date.Value = DateTime.Now.Date;
            datecari.Value = DateTime.Now.Date;
            datecari.Checked = false;

            await HitungTotalData();
            await tampil();
        }

        private async void btncari_Click(object sender, EventArgs e)
        {
            if (!infocari)
            {
                bool hasil = await cari();
                if (hasil)
                {
                    infocari = true;
                    btncari.Text = "Reset";
                }
            }
            else
            {
                infocari = false;
                isSearching = false;

                cbShift.StartIndex = 0;
                datecari.Checked = false;

                btncari.Text = "Cari";

                await HitungTotalData();
                currentPage = 1;
                await tampil();
            }
        }

        private void cmbshift_SelectedIndexChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpan.Enabled = true;
        }

        private void txtman_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpan.Enabled = true;
        }

        private void txtbutt_TextChanged(object sender, EventArgs e)
        {
            btndelete.Enabled = true;
            btnsimpan.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MainForm.Instance.role != "Manajer" && MainForm.Instance.role != "Developer") return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                noprimary = Convert.ToInt32(row.Cells["no"].Value);
                date.Value = Convert.ToDateTime(row.Cells["tanggal"].Value);
                cmbshift.SelectedItem = row.Cells["shift"].Value.ToString();
                txtbutt.Text = row.Cells["butt_ratio"].Value.ToString();
                txtman.Text = row.Cells["man_power"].Value.ToString();
                date.Enabled = false;
                cmbshift.Enabled = false;
                btnsimpan.Text = "Edit Data";
                btndelete.Enabled = true;
                btndelete.Text = "Hapus Data";
                btnbatal.Visible = true;
            }
        }

        private void btnbatal_Click_1(object sender, EventArgs e)
        {
            setdefault();
            btnsimpan.Text = "Simpan Data";
            btnsimpan.Enabled = false;
            btndelete.Text = "Batal";
            btndelete.Enabled = false;
            btnbatal.Visible = false;
            date.Enabled = true;
            cmbshift.Enabled = true;
            noprimary = 0;
        }

        private async void btndelete_Click(object sender, EventArgs e)
        {
            btnsimpan.Enabled = false;
            btndelete.Enabled = false;
            btnbatal.Visible = false;

            if (btndelete.Text == "Hapus Data")
            {
                await hapusdata();
            }

            setdefault();
            btnsimpan.Text = "Simpan Data";
            btndelete.Text = "Batal";
            date.Enabled = true;
            cmbshift.Enabled = true;
            noprimary = 0;

            btnsimpan.Enabled = false;
            btndelete.Enabled = false;
            btnbatal.Visible = false;
        }


        private void formbuttman_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private async void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await tampil();
            }
        }

        private async void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                await tampil();
            }
        }
    }
}
