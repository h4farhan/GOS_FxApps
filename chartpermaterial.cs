using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;
using Guna.UI2.WinForms;
using DrawingPoint = System.Drawing.Point;
using System.Threading;

namespace GOS_FxApps
{
    public partial class chartpermaterial : Form
    {

        bool bukatutupfilter = false;
        private bool isLoading = false;
        private System.Windows.Forms.Timer searchTimer;
        private bool formSiap = false;

        public chartpermaterial()
        {
            InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, EventArgs e)
        {
            if (bukatutupfilter)
            {
                containerpanelfilter.Visible = false;
                bukatutupfilter = false;
                HamburgerButton.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
            }
            else
            {
                containerpanelfilter.Visible = true;
                bukatutupfilter = true;
                HamburgerButton.IconChar = FontAwesome.Sharp.IconChar.AngleRight;
            }
        }

        private async Task LoadChartEstimasiMaterialBulanan()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chart_estimasi_per_material_bulanan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KodeBarang", cmbkodebarang.Text);
                        cmd.Parameters.AddWithValue("@Bulan", datebulan.Value.Month);
                        cmd.Parameters.AddWithValue("@Tahun", datetahun.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
                    }
                }

                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "NamaTanggal",
                    YValueMembers = "TotalBQ",
                    IsValueShownAsLabel = true
                };
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "NamaTanggal",
                    YValueMembers = "TotalPemakaian",
                    IsValueShownAsLabel = true
                };
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;

                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.LabelStyle.Angle = -45;
                axisX.IsMarginVisible = true;
                axisX.LabelStyle.IsEndLabelVisible = true;
                axisX.LabelStyle.TruncatedLabels = false;

                int totalData = dt.Rows.Count;

                axisX.Minimum = 0;
                axisX.Maximum = totalData + 1;

                if (totalData > 10)
                {
                    axisX.ScaleView.Zoomable = false;
                    axisX.ScaleView.Size = 10;
                    axisX.ScaleView.Position = 0;

                    axisX.ScrollBar.Enabled = true;
                    axisX.ScrollBar.IsPositionedInside = true;
                    axisX.ScrollBar.Size = 12;
                }
                else
                {
                    axisX.ScaleView.ZoomReset();
                    axisX.ScrollBar.Enabled = false;
                }

                axisX.MajorGrid.Enabled = false;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private async Task LoadChartEstimasiMaterialTahunan()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chart_estimasi_per_material_tahunan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KodeBarang", cmbkodebarang.Text);
                        cmd.Parameters.AddWithValue("@Tahun", datetahun.Value.Year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
                    }
                }

                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "NamaBulan",        
                    YValueMembers = "TotalBQ",
                    IsValueShownAsLabel = true
                };
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "NamaBulan",       
                    YValueMembers = "TotalPemakaian",
                    IsValueShownAsLabel = true
                };
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;

                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.LabelStyle.Angle = -45;
                axisX.IsMarginVisible = true;
                axisX.LabelStyle.IsEndLabelVisible = true;
                axisX.LabelStyle.TruncatedLabels = false;

                int totalData = dt.Rows.Count;

                axisX.Minimum = 0;
                axisX.Maximum = totalData + 1;

                if (totalData > 10)
                {
                    axisX.ScaleView.Zoomable = false;
                    axisX.ScaleView.Size = 10;
                    axisX.ScaleView.Position = 0;

                    axisX.ScrollBar.Enabled = true;
                    axisX.ScrollBar.IsPositionedInside = true;
                    axisX.ScrollBar.Size = 12;
                }
                else
                {
                    axisX.ScaleView.ZoomReset();
                    axisX.ScrollBar.Enabled = false;
                }

                axisX.MajorGrid.Enabled = false;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private async Task LoadChartEstimasiMaterialCustom()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = await Koneksi.GetConnectionAsync())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_chart_estimasi_per_material_custom", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KodeBarang", cmbkodebarang.Text);
                        cmd.Parameters.AddWithValue("@TanggalMulai", tanggalcustom1.Value.Date);
                        cmd.Parameters.AddWithValue("@TanggalAkhir", tanggalcustom2.Value.Date);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dt.Load(reader);
                        }
                    }
                }

                chartUssageMaterial.Series.Clear();
                chartUssageMaterial.DataSource = null;

                var area = chartUssageMaterial.ChartAreas[0];
                area.AxisX.ScaleView.ZoomReset();
                area.RecalculateAxesScale();

                Series seriesBQ = new Series("Estimasi")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "NamaTanggal",
                    YValueMembers = "TotalBQ",
                    IsValueShownAsLabel = true
                };
                seriesBQ["PointWidth"] = "0.4";

                Series seriesPemakaian = new Series("Actual")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "NamaTanggal",
                    YValueMembers = "TotalPemakaian",
                    IsValueShownAsLabel = true
                };
                seriesPemakaian["PointWidth"] = "0.4";

                chartUssageMaterial.Series.Add(seriesBQ);
                chartUssageMaterial.Series.Add(seriesPemakaian);

                chartUssageMaterial.Legends.Clear();
                Legend legend = new Legend("LegendMaterial");
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                legend.LegendStyle = LegendStyle.Row;
                chartUssageMaterial.Legends.Add(legend);

                var axisX = chartUssageMaterial.ChartAreas[0].AxisX;

                axisX.Interval = 1;
                axisX.IsLabelAutoFit = false;
                axisX.LabelStyle.Angle = -45;
                axisX.IsMarginVisible = true;
                axisX.LabelStyle.IsEndLabelVisible = true;
                axisX.LabelStyle.TruncatedLabels = false;

                int totalData = dt.Rows.Count;

                axisX.Minimum = 0;
                axisX.Maximum = totalData + 1;

                if (totalData > 10)
                {
                    axisX.ScaleView.Zoomable = false;
                    axisX.ScaleView.Size = 10;       
                    axisX.ScaleView.Position = 0;

                    axisX.ScrollBar.Enabled = true;
                    axisX.ScrollBar.IsPositionedInside = true;
                    axisX.ScrollBar.Size = 12;
                }
                else
                {
                    axisX.ScaleView.ZoomReset();
                    axisX.ScrollBar.Enabled = false;
                }

                axisX.MajorGrid.Enabled = false;

                chartUssageMaterial.DataSource = dt;
                chartUssageMaterial.DataBind();
            }
            catch (SqlException)
            {
                MessageBox.Show("Koneksi terputus. Pastikan jaringan aktif.",
                                "Kesalahan Jaringan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var ax = chart.ChartAreas[0].AxisX;

            if (!ax.ScrollBar.Enabled) return;

            double newPos = ax.ScaleView.Position;

            if (e.Delta > 0)
                newPos--;
            else
                newPos++;

            if (newPos < ax.Minimum)
                newPos = ax.Minimum;

            if (newPos + ax.ScaleView.Size > ax.Maximum)
                newPos = ax.Maximum - ax.ScaleView.Size;

            ax.ScaleView.Position = newPos;
        }

        private async void btnsetfilter_Click(object sender, EventArgs e)
        {
            if (cmbnamamaterial.Text == "Pilih Material" || cmbkodebarang.Text == "Pilih Kode Barang")
            {
                MessageBox.Show("Data Material Harus Diisi Dengan Lengkap.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbrentang.SelectedIndex == 0)
            {
                await LoadChartEstimasiMaterialBulanan();
                lbltitlechart.Text = $"Estimasi VS Actual {cmbnamamaterial.Text} {cmbrentang.Text} {datebulan.Value.Month} {datebulan.Value.Year}";
            }
            else if (cmbrentang.SelectedIndex == 1)
            {
                await LoadChartEstimasiMaterialTahunan();
                lbltitlechart.Text = $"Estimasi VS Actual {cmbnamamaterial.Text} {cmbrentang.Text} {datetahun.Value.Year}";
            }
            else if (cmbrentang.SelectedIndex == 2)
            {
                if (tanggalcustom1.Value.Date > tanggalcustom2.Value.Date)
                {
                    MessageBox.Show("Tanggal Mulai harus kurang dari atau sama dengan Tanggal Akhir agar valid", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await LoadChartEstimasiMaterialCustom();
                lbltitlechart.Text = $"Estimasi VS Actual {cmbnamamaterial.Text} {cmbrentang.Text} {tanggalcustom1.Value.ToString("dd/MM/yyyy")} s/d {tanggalcustom2.Value.ToString("dd/MM/yyyy")}";
            }
        }

        private async Task<DataTable> LoadDataTableAsync(SqlCommand cmd)
        {
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }

        public async Task combonama()
        {
            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
            SELECT DISTINCT deskripsi
            FROM koefisiensi_material", conn))
                {
                    DataTable dt = await LoadDataTableAsync(cmd);

                    DataRow dr = dt.NewRow();
                    dr["deskripsi"] = "Pilih Material";
                    dt.Rows.InsertAt(dr, 0);

                    cmbnamamaterial.SelectedIndexChanged -= cmbnamamaterial_SelectedIndexChanged;

                    cmbnamamaterial.DataSource = dt;
                    cmbnamamaterial.DisplayMember = "deskripsi";
                    cmbnamamaterial.ValueMember = "deskripsi";
                    cmbnamamaterial.SelectedIndex = 0;

                    cmbnamamaterial.SelectedIndexChanged += cmbnamamaterial_SelectedIndexChanged;
                }
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal combonama Data. " + ex.Message) ;
                return;
            }
        }

        private async void cmbnamamaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cmbnamamaterial.SelectedIndex <= 0)
            {
                cmbkodebarang.DataSource = null;
                cmbkodebarang.Items.Clear();
                cmbkodebarang.Items.Add("Pilih Kode Barang");
                cmbkodebarang.SelectedIndex = 0;
                return;
            }

            string namaBarang = cmbnamamaterial.SelectedValue.ToString();

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
            SELECT DISTINCT kodeBarang
            FROM koefisiensi_material
            WHERE deskripsi = @deskripsi", conn))
                {
                    cmd.Parameters.AddWithValue("@deskripsi", namaBarang);

                    DataTable dt = await LoadDataTableAsync(cmd);

                    DataRow dr = dt.NewRow();
                    dr["kodeBarang"] = "Pilih Kode Barang";
                    dt.Rows.InsertAt(dr, 0);

                    cmbkodebarang.SelectedIndexChanged -= cmbkodebarang_SelectedIndexChanged;

                    cmbkodebarang.DataSource = dt;
                    cmbkodebarang.DisplayMember = "kodeBarang";
                    cmbkodebarang.ValueMember = "kodeBarang";
                    cmbkodebarang.SelectedIndex = 0;

                    cmbkodebarang.SelectedIndexChanged += cmbkodebarang_SelectedIndexChanged;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal memuat kode barang.");
            }
        }

        private void cmbkodebarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private CancellationTokenSource searchCancelToken;
        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            if (!formSiap || cmbnamamaterial == null || txtcarinamabarang == null)
                return;

            string keyword = txtcarinamabarang.Text?.Trim() ?? "";

            searchCancelToken?.Cancel();
            searchCancelToken = new CancellationTokenSource();
            var token = searchCancelToken.Token;

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                using (var cmd = new SqlCommand(@"
            SELECT DISTINCT deskripsi
            FROM koefisiensi_material
            WHERE 
                (deskripsi LIKE @keyword OR kodeBarang LIKE @keyword) 
            ORDER BY deskripsi ASC", conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    DataTable dt = await LoadDataTableAsync(cmd);

                    if (token.IsCancellationRequested)
                        return;

                    DataRow dr = dt.NewRow();
                    dr["deskripsi"] = "Pilih Material";
                    dt.Rows.InsertAt(dr, 0);

                    cmbnamamaterial.SelectedIndexChanged -= cmbnamamaterial_SelectedIndexChanged;

                    cmbnamamaterial.DataSource = dt;
                    cmbnamamaterial.DisplayMember = "deskripsi";
                    cmbnamamaterial.ValueMember = "deskripsi";
                    cmbnamamaterial.SelectedIndex = 0;

                    cmbnamamaterial.SelectedIndexChanged += cmbnamamaterial_SelectedIndexChanged;
                }

                if (cmbnamamaterial.Items.Count > 1)
                {
                    cmbnamamaterial.DroppedDown = true;
                }
                else
                {
                    cmbnamamaterial.DroppedDown = false;
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (SqlException)
            {
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Search Data.");
                return;
            }
        }

        private void txtcarinamabarang_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void datetahun_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 150);
                pickerForm.Text = "Pilih Tahun";

                var screenPos = datetahun.PointToScreen(Point.Empty);
                pickerForm.Location = new Point(screenPos.X, screenPos.Y + datetahun.Height);

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 20,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = datetahun.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 70,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    datetahun.Value = new DateTime((int)numTahun.Value, 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private async void chartpermaterial_Load(object sender, EventArgs e)
        {
            tanggalcustom1.Value = DateTime.Today;
            tanggalcustom2.Value = DateTime.Today;
            datebulan.Value = DateTime.Now;
            datetahun.Value = DateTime.Now;

            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300;
            searchTimer.Tick += SearchTimer_Tick;

            chartUssageMaterial.MouseWheel -= Chart_MouseWheel;
            chartUssageMaterial.MouseWheel += Chart_MouseWheel;

            cmbnamamaterial.DropDownStyle = ComboBoxStyle.DropDown;
            cmbnamamaterial.MaxDropDownItems = 10;
            cmbnamamaterial.DropDownHeight = 300;

            cmbkodebarang.DropDownStyle = ComboBoxStyle.DropDown;
            cmbkodebarang.MaxDropDownItems = 10;
            cmbkodebarang.DropDownHeight = 300;

            await combonama();
            formSiap = true;
        }

        private void datebulan_MouseDown(object sender, MouseEventArgs e)
        {
            using (Form pickerForm = new Form())
            {
                pickerForm.StartPosition = FormStartPosition.Manual;
                pickerForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pickerForm.ControlBox = false;
                pickerForm.Size = new Size(250, 200);
                pickerForm.Text = "Pilih Bulan & Tahun";

                var screenPos = datebulan.PointToScreen(DrawingPoint.Empty);
                pickerForm.Location = new DrawingPoint(screenPos.X, screenPos.Y + datebulan.Height);

                var cmbBulan = new Guna2ComboBox
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 10,
                    Width = 200,
                    BorderRadius = 6,
                    ForeColor = Color.Black,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };
                string[] bulan = {
                                    "01 - Januari", "02 - Februari", "03 - Maret", "04 - April", "05 - Mei", "06 - Juni",
                                    "07 - Juli", "08 - Agustus", "09 - September", "10 - Oktober", "11 - November", "12 - Desember"
                                };
                cmbBulan.Items.AddRange(bulan);
                cmbBulan.SelectedIndex = datebulan.Value.Month - 1;

                var numTahun = new Guna2NumericUpDown
                {
                    Font = new Font("Segoe UI", 11F),
                    Left = 10,
                    Top = 55,
                    Width = 200,
                    BorderRadius = 6,
                    Minimum = 1900,
                    Maximum = 2100,
                    ForeColor = Color.Black,
                    Value = datebulan.Value.Year,
                    BorderColor = Color.FromArgb(64, 64, 64),
                    BorderThickness = 2,
                };

                var btnOK = new Guna2Button
                {
                    Text = "OK",
                    Font = new Font("Segoe UI", 10F),
                    Left = 10,
                    Top = 110,
                    Width = 80,
                    Height = 35,
                    BorderRadius = 6,
                    FillColor = Color.FromArgb(53, 53, 58)
                };
                btnOK.Click += (s, ev) =>
                {
                    datebulan.Value = new DateTime((int)numTahun.Value, cmbBulan.SelectedIndex + 1, 1);
                    pickerForm.DialogResult = DialogResult.OK;
                };

                pickerForm.Controls.Add(cmbBulan);
                pickerForm.Controls.Add(numTahun);
                pickerForm.Controls.Add(btnOK);

                pickerForm.ShowDialog();
            }
        }

        private void cmbrentang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbrentang.SelectedIndex == 0)
            {
                containertanggal1.Visible = false;
                containertanggal2.Visible = false;
                containertahun.Visible = false;
                containerbulan.Visible = true;
            }
            else if (cmbrentang.SelectedIndex == 1)
            {
                containertanggal1.Visible = false;
                containertanggal2.Visible = false;
                containerbulan.Visible= false;
                containertahun.Visible = true;
            }
            else if (cmbrentang.SelectedIndex == 2)
            {
                containerbulan.Visible = false;
                containertahun.Visible = false;
                containertanggal1.Visible = true;
                containertanggal2.Visible = true;
            }
        }
    }
}
