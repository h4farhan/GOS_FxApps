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
            catch { }
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
                ORDER BY tanggal DESC
                OFFSET {offset} ROWS
                FETCH NEXT {pageSize} ROWS ONLY";
                    }
                    else
                    {
                        query = $@"
                SELECT no,tanggal,shift,butt_ratio,man_power,updated_at,remaks
                {lastSearchWhere}
                ORDER BY tanggal DESC
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
                MessageBox.Show("Gagal Cari Data.");
                return false;
            }
        }

        private async Task editdata()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE kondisiROD SET butt_ratio = @butt, man_power = @man, updated_at = GETDATE(), remaks = @remaks WHERE no = @no", conn);
                    cmd.Parameters.AddWithValue("@no", noprimary);
                    cmd.Parameters.AddWithValue("@butt", txtbutt.Text);
                    cmd.Parameters.AddWithValue("@man", txtman.Text);
                    cmd.Parameters.AddWithValue("@remaks", loginform.login.name);
                    await cmd.ExecuteNonQueryAsync();

                    MessageBox.Show("Data Berhasil Diedit.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnsimpan.Text = "Simpan Data";
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Edit Data.");
                return;
            }
        }

        private async Task simpandata()
        {

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmdcekkode = new SqlCommand("SELECT tanggal,shift FROM kondisiROD WHERE tanggal = @tgl AND shift = @shift", conn))
                    {
                        cmdcekkode.Parameters.AddWithValue("@tgl", date.Value);
                        cmdcekkode.Parameters.AddWithValue("@shift", cmbshift.SelectedItem);
                        using (SqlDataReader dr = await cmdcekkode.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                MessageBox.Show("Data di Tanggal dan di Shift ini sudah ada", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO kondisiROD (tanggal, shift, butt_ratio, man_power, updated_at, remaks) VALUES(@tgl,@shift,@butt,@man,GETDATE(),@remaks)", conn))
                    {
                        cmd.Parameters.AddWithValue("@tgl", date.Value);
                        cmd.Parameters.AddWithValue("@shift", cmbshift.SelectedItem);
                        cmd.Parameters.AddWithValue("@butt", txtbutt.Text);
                        cmd.Parameters.AddWithValue("@man", txtman.Text);
                        cmd.Parameters.AddWithValue("@remaks", loginform.login.name);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    MessageBox.Show("Data Berhasil Disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setdefault();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Simpan Data.");
                return;
            }
        }

        private async Task hapusdata()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM kondisiROD WHERE no = @no", conn);
                    cmd.Parameters.AddWithValue("@no", noprimary);
                    await cmd.ExecuteNonQueryAsync();

                    MessageBox.Show("Data berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi anda masih terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Hapus Data.");
                return;
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
            if (cmbshift.SelectedIndex == -1 || txtbutt.Text == "" || txtman.Text == "") 
            {
                MessageBox.Show("Harap lengkapi data terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(btnsimpan.Text == "Simpan Data")
            {
                btndelete.Enabled = false;
                btnsimpan.Enabled = false;
                await simpandata();
                setdefault();
                btndelete.Enabled = false;
                btnsimpan.Enabled = false;
                date.Enabled = true;
                cmbshift.Enabled = true;
            }
            else if (btnsimpan.Text == "Edit Data")
            {
                btndelete.Enabled = false;
                btnsimpan.Enabled = false;
                await editdata();
                setdefault();
                btndelete.Enabled = false;
                btnsimpan.Enabled = false;
                btnbatal.Visible = false;
                noprimary = 0;
                date.Enabled = true;
                cmbshift.Enabled = true;
            }

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
            if (btndelete.Text == "Hapus Data")
            {
                btnsimpan.Text = "Simpan Data";
                btnsimpan.Enabled = false;
                btndelete.Text = "Batal";
                btndelete.Enabled = false;
                btnbatal.Visible = false;
                await hapusdata();
                setdefault();
                btnsimpan.Enabled = false;
                btndelete.Enabled = false;
                btnbatal.Visible = false;
                date.Enabled = true;
                cmbshift.Enabled = true;
                noprimary = 0;
            }
            else
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
