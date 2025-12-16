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
using Guna.UI2.WinForms;
using static System.Net.Mime.MediaTypeNames;
using System.Management.Instrumentation;
using System.Threading;

namespace GOS_FxApps
{
    public partial class Pengiriman : Form
    {
        private List<Perbaikan> list = new List<Perbaikan>();

        Guna.UI2.WinForms.Guna2TextBox[] txtrods;

        int pageSize = 30;
        int currentPage = 1;
        int totalRecords = 0;
        int totalPages = 0;

        bool isSearching = false;
        string lastSearchWhere = "";
        SqlCommand lastSearchCmd;
        int searchTotalRecords = 0;

        public class Perbaikan
        {
            public int No { get; set; }                  
            public string NomorRod { get; set; }        

            public Perbaikan() { }

            public Perbaikan(int no, string nomorRod)
            {
                No = no;
                NomorRod = nomorRod;
            }
        }

        public Pengiriman()
        {
            InitializeComponent();
            txtrods = new Guna.UI2.WinForms.Guna2TextBox[]
            {
                txtrod1, txtrod2, txtrod3, txtrod4, txtrod5,
                txtrod6, txtrod7, txtrod8, txtrod9, txtrod10
            };
        }

        private async Task HitungTotalData()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM perbaikan_s";
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

        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                switch (table)
                {
                    case "perbaikan_s":
                        if (!isSearching)
                        {
                            await HitungTotalData();
                            currentPage = 1;
                            await tampilperbaikan();
                        }
                        else
                        {
                            int oldTotal = searchTotalRecords;
                            await HitungTotalDataPencarian();
                            if (searchTotalRecords > oldTotal)
                                await tampilperbaikan();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch { }
        }

        private async Task tampilperbaikan()
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
                    SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, bac, nba, ba, ba1, r, m, cr, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks, catatan
                    FROM perbaikan_s
                    ORDER BY tanggal_perbaikan DESC
                    OFFSET {offset} ROWS
                    FETCH NEXT {pageSize} ROWS ONLY";
                    }
                    else
                    {
                        query = $@"
                    SELECT no, tanggal_perbaikan, shift, nomor_rod, jenis, e1_ers, e1_est, e1_jumlah, e2_ers, e2_cst, e2_cstub, e2_jumlah, e3, e4, s, d, b, bac, nba, ba, ba1, r, m, cr, c, rl, jumlah, tanggal_penerimaan, updated_at, remaks, catatan
                    {lastSearchWhere}
                    ORDER BY tanggal_perbaikan DESC
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(213, 213, 214);
            dataGridView1.ReadOnly = true;

            if (dt.Columns.Count >= 31)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Tanggal Perbaikan";
                dataGridView1.Columns[2].HeaderText = "Shift";
                dataGridView1.Columns[3].HeaderText = "Nomor ROD";
                dataGridView1.Columns[4].HeaderText = "Jenis";
                dataGridView1.Columns[5].HeaderText = "E1 Ers";
                dataGridView1.Columns[6].HeaderText = "E1 Est";
                dataGridView1.Columns[7].HeaderText = "E1 Jumlah";
                dataGridView1.Columns[8].HeaderText = "E2 Ers";
                dataGridView1.Columns[9].HeaderText = "E2 Cst";
                dataGridView1.Columns[10].HeaderText = "E2 Cstub";
                dataGridView1.Columns[11].HeaderText = "E2 Jumlah";
                dataGridView1.Columns[12].HeaderText = "E3";
                dataGridView1.Columns[13].HeaderText = "E4";
                dataGridView1.Columns[14].HeaderText = "S";
                dataGridView1.Columns[15].HeaderText = "D";
                dataGridView1.Columns[16].HeaderText = "B";
                dataGridView1.Columns[17].HeaderText = "BAC";
                dataGridView1.Columns[18].HeaderText = "NBA";
                dataGridView1.Columns[19].HeaderText = "BA";
                dataGridView1.Columns[20].HeaderText = "BA-1";
                dataGridView1.Columns[21].HeaderText = "R";
                dataGridView1.Columns[22].HeaderText = "M";
                dataGridView1.Columns[23].HeaderText = "CR";
                dataGridView1.Columns[24].HeaderText = "C";
                dataGridView1.Columns[25].HeaderText = "RL";
                dataGridView1.Columns[26].HeaderText = "Jumlah";
                dataGridView1.Columns[27].HeaderText = "Tanggal Penerimaan";
                dataGridView1.Columns[28].HeaderText = "Diubah";
                dataGridView1.Columns[29].HeaderText = "Remaks";
                dataGridView1.Columns[30].HeaderText = "Catatan";
            }

            lblhalaman.Text = $"Halaman {currentPage} dari {totalPages}";

            btnleft.Enabled = currentPage > 1;
            btnright.Enabled = currentPage < totalPages;
        }

        private async void Pengiriman_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;

            txtrod1.Focus();

            await HitungTotalData();
            await tampilperbaikan();
        }

        private void setdefault()
        {
            txtrod1.Clear();
            txtrod2.Clear();
            txtrod3.Clear();
            txtrod4.Clear();
            txtrod5.Clear();
            txtrod6.Clear();
            txtrod7.Clear();
            txtrod8.Clear();
            txtrod9.Clear();
            txtrod10.Clear();
            txtcari.Clear();

            txtrod1.PlaceholderText = "4xxxx";
            txtrod2.PlaceholderText = "4xxxx";
            txtrod3.PlaceholderText = "4xxxx";
            txtrod4.PlaceholderText = "4xxxx";
            txtrod5.PlaceholderText = "4xxxx";
            txtrod6.PlaceholderText = "4xxxx";
            txtrod7.PlaceholderText = "4xxxx";
            txtrod8.PlaceholderText = "4xxxx";
            txtrod9.PlaceholderText = "4xxxx";
            txtrod10.PlaceholderText = "4xxxx";

            txtrod1.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod2.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod3.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod4.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod5.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod6.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod7.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod8.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod9.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
            txtrod10.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Clear();
                txt.PlaceholderText = "4xxxx";
                txt.PlaceholderForeColor = Color.FromArgb(193, 200, 207);
                return;
            }

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM perbaikan_s WHERE nomor_rod = @A", conn);
                cmd.Parameters.AddWithValue("@A", txt.Text);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        
                    }
                    else
                    {
                        txt.Clear();
                        txt.PlaceholderText = "Data tidak ditemukan di Perbaikan";
                        txt.PlaceholderForeColor = Color.Red;
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                        "Kesalahan Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan sistem:\n" + ex.Message,
                                    "Kesalahan Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private async Task insertdata()
        {
            List<Perbaikan> list = new List<Perbaikan>();

            using (var conn = await Koneksi.GetConnectionAsync())
            {
                SqlCommand cmd1 = new SqlCommand(
                    "SELECT no, nomor_rod FROM perbaikan_s WHERE nomor_rod IN (@A,@B,@C,@D,@E,@F,@G,@H,@I,@J)", conn);

                cmd1.Parameters.AddWithValue("@A", txtrod1.Text);
                cmd1.Parameters.AddWithValue("@B", txtrod2.Text);
                cmd1.Parameters.AddWithValue("@C", txtrod3.Text);
                cmd1.Parameters.AddWithValue("@D", txtrod4.Text);
                cmd1.Parameters.AddWithValue("@E", txtrod5.Text);
                cmd1.Parameters.AddWithValue("@F", txtrod6.Text);
                cmd1.Parameters.AddWithValue("@G", txtrod7.Text);
                cmd1.Parameters.AddWithValue("@H", txtrod8.Text);
                cmd1.Parameters.AddWithValue("@I", txtrod9.Text);
                cmd1.Parameters.AddWithValue("@J", txtrod10.Text);

                try
                {
                    using (SqlDataReader reader = await cmd1.ExecuteReaderAsync())
                    {
                        HashSet<int> seenNo = new HashSet<int>();

                        while (await reader.ReadAsync())
                        {
                            int no = Convert.ToInt32(reader["no"]);
                            string nomorRod = reader["nomor_rod"]?.ToString();

                            if (!seenNo.Add(no))
                            {
                                MessageBox.Show(
                                    $"Nomor 'no' {no} terdeteksi duplikat!\nProses dibatalkan.",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                                return;
                            }

                            list.Add(new Perbaikan
                            {
                                No = no,
                                NomorRod = nomorRod
                            });
                        }
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

                if (list.Count == 0)
                {
                    MessageBox.Show("Tidak ada data yang cocok dengan nomor ROD.",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                string queryInsertp = @"INSERT INTO pengiriman 
            (no, tanggal_pengiriman, shift, nomor_rod, updated_at, remaks) 
            VALUES (@no, @tanggal, @shift, @nomor_rod, GETDATE(), @remaks)";

                string queryInsertm = @"INSERT INTO pengiriman_m 
            (no, tanggal_pengiriman, shift, nomor_rod, updated_at, remaks) 
            VALUES (@no, @tanggal, @shift, @nomor_rod, GETDATE(), @remaks)";

                try
                {
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            DateTime baseTime = MainForm.Instance.tanggal;
                            int index = 0;

                            foreach (var item in list)
                            {
                                DateTime waktuPengiriman = baseTime.AddSeconds(index);

                                using (SqlCommand cmdp = new SqlCommand(queryInsertp, conn, trans))
                                {
                                    cmdp.Parameters.AddWithValue("@no", item.No);
                                    cmdp.Parameters.AddWithValue("@tanggal", waktuPengiriman);
                                    cmdp.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                                    cmdp.Parameters.AddWithValue("@nomor_rod", item.NomorRod ?? (object)DBNull.Value);
                                    cmdp.Parameters.AddWithValue("@remaks", loginform.login.name);

                                    await cmdp.ExecuteNonQueryAsync();
                                }

                                using (SqlCommand cmdm = new SqlCommand(queryInsertm, conn, trans))
                                {
                                    cmdm.Parameters.AddWithValue("@no", item.No);
                                    cmdm.Parameters.AddWithValue("@tanggal", waktuPengiriman);
                                    cmdm.Parameters.AddWithValue("@shift", MainForm.Instance.lblshift.Text);
                                    cmdm.Parameters.AddWithValue("@nomor_rod", item.NomorRod ?? (object)DBNull.Value);
                                    cmdm.Parameters.AddWithValue("@remaks", loginform.login.name);

                                    await cmdm.ExecuteNonQueryAsync();
                                }

                                index++;
                            }

                            using (SqlCommand cmd3 = new SqlCommand(
                                "DELETE FROM perbaikan_s WHERE nomor_rod IN (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j)", conn, trans))
                            {
                                cmd3.Parameters.AddWithValue("@a", txtrod1.Text);
                                cmd3.Parameters.AddWithValue("@b", txtrod2.Text);
                                cmd3.Parameters.AddWithValue("@c", txtrod3.Text);
                                cmd3.Parameters.AddWithValue("@d", txtrod4.Text);
                                cmd3.Parameters.AddWithValue("@e", txtrod5.Text);
                                cmd3.Parameters.AddWithValue("@f", txtrod6.Text);
                                cmd3.Parameters.AddWithValue("@g", txtrod7.Text);
                                cmd3.Parameters.AddWithValue("@h", txtrod8.Text);
                                cmd3.Parameters.AddWithValue("@i", txtrod9.Text);
                                cmd3.Parameters.AddWithValue("@j", txtrod10.Text);

                                await cmd3.ExecuteNonQueryAsync();
                            }

                            trans.Commit();

                            MessageBox.Show("Data Berhasil Dikirim!",
                                            "Sukses",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            delay = 0;
                            setdefault();
                        }
                        catch (Exception exTrans)
                        {
                            trans.Rollback();
                            MessageBox.Show("Gagal menyimpan: " + exTrans.Message);
                        }
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
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                guna2Button2.PerformClick();
            }
        }

        private void hurufbesar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            Guna2TextBox[] txtRods = {
                txtrod1, txtrod2, txtrod3, txtrod4, txtrod5,
                txtrod6, txtrod7, txtrod8, txtrod9, txtrod10
            };

            bool adaIsi = txtRods.Any(t => !string.IsNullOrWhiteSpace(t.Text));

            if (!adaIsi)
            {
                MessageBox.Show("Isilah salah satu Nomor ROD yang ingin dikirim terlebih dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnclear.Enabled = false;
            guna2Button2.Enabled = false;
            await insertdata();
            delay = 0;
            setdefault();
            btnclear.Enabled = false;
            guna2Button2.Enabled = false;
            delay = 300;
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            setdefault();
            btnclear.Enabled = false;
            guna2Button2.Enabled = false;
            dataGridView1.ClearSelection();
        }

        private void txtrod1_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod9_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod8_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod7_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod6_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod5_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod4_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod3_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod2_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void txtrod10_TextChanged(object sender, EventArgs e)
        {
            btnclear.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string value = row.Cells["nomor_rod"].Value.ToString();

                foreach (var txt in txtrods)
                {
                    if (txt.Text == value)
                    {
                        MessageBox.Show("Nomor ROD ini sudah dimasukkan sebelumnya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                for (int i = 0; i < txtrods.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(txtrods[i].Text))
                    {
                        txtrods[i].Text = value;
                        return;
                    }
                }
                MessageBox.Show("Maksimal pengiriman hanya 10 ROD. Kirim terlebih dahulu dan coba lagi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Pengiriman_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        private CancellationTokenSource ctsCari = null;
        int delay = 300;

        private async void txtcari_TextChanged(object sender, EventArgs e)
        {
            string inputRod = txtcari.Text.Trim();

            ctsCari?.Cancel();
            ctsCari = new CancellationTokenSource();
            var token = ctsCari.Token;

            try
            {
                await Task.Delay(delay, token);
            }
            catch (TaskCanceledException)
            {
                return;
            }

            currentPage = 1;

            if (string.IsNullOrEmpty(inputRod))
            {
                isSearching = false;
                await HitungTotalData();
            }
            else
            {
                isSearching = true;
                lastSearchCmd = new SqlCommand();
                lastSearchWhere = "FROM perbaikan_s WHERE nomor_rod LIKE @rod";
                lastSearchCmd.Parameters.Clear();
                lastSearchCmd.Parameters.AddWithValue("@rod", "%" + inputRod + "%");
                await HitungTotalDataPencarian();
            }

            await tampilperbaikan();
        }

        private async void btnleft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await tampilperbaikan();
            }
        }

        private async void btnright_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                await tampilperbaikan();
            }
        }
    }
}
