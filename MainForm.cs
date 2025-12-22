using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SqlClient;

namespace GOS_FxApps { 
   
    
    public partial class MainForm : Form
    {
        private bool sidebarx = false;
        private bool entryprodx = false;
        private bool editx = false;
        private bool historiy = false;
        private bool gudangx = false;

        public static MainForm Instance;

        private int totalNotifikasi;

        public bool loginstatus = false;

        public Size defaultentrycontainer;
        public Size defaulteditcontainer;
        public Size defaulhistorycontainer;
        public Size defaulgudangcontainer;

        public string role = null;

        public bool isManual = false;
        public DateTime tanggal;

        public event Action ShiftChanged;
        private string _shift;

        private Timer alertTimer;

        public string shift
        {
            get => _shift;
            private set
            {
                if (_shift != value)
                {
                    _shift = value;
                    ShiftChanged?.Invoke();
                }
            }
        }

        public void ResetButtonColors()
        {
            dashboardButton.FillColor = Color.White;
            dashboardButton.ForeColor = Color.Black;

            penerimaanButton1.FillColor = Color.Transparent;
            penerimaanButton1.ForeColor = Color.Black;

            btnbuktiperubahan.FillColor = Color.Transparent;
            btnbuktiperubahan.ForeColor = Color.Black;

            iconButton1.FillColor = Color.Transparent;
            iconButton1.ForeColor = Color.Black;

            iconButton2.FillColor = Color.Transparent;
            iconButton2.ForeColor = Color.Black;

            iconButton3.FillColor = Color.Transparent;
            iconButton3.ForeColor = Color.Black;

            btnpemakaian.FillColor = Color.Transparent;
            btnpemakaian.ForeColor = Color.Black;

            iconButton15.FillColor = Color.Transparent;
            iconButton15.ForeColor = Color.Black;

            iconButton9.FillColor = Color.Transparent;
            iconButton9.ForeColor = Color.Black;

            iconButton8.FillColor = Color.Transparent;
            iconButton8.ForeColor = Color.Black;

            btneditbuktiperubahan.FillColor = Color.Transparent;
            btneditbuktiperubahan.ForeColor = Color.Black;

            iconButton12.FillColor = Color.Transparent;
            iconButton12.ForeColor = Color.Black;

            btnlaporan.FillColor = Color.White;
            btnlaporan.ForeColor = Color.Black;

            iconButton24.FillColor = Color.Transparent;
            iconButton24.ForeColor = Color.Black;

            iconButton11.FillColor = Color.Transparent;
            iconButton11.ForeColor = Color.Black;

            iconButton10.FillColor = Color.Transparent;
            iconButton10.ForeColor = Color.Black;

            iconButton6.FillColor = Color.Transparent;
            iconButton6.ForeColor = Color.Black;

            iconButton5.FillColor = Color.Transparent;
            iconButton5.ForeColor = Color.Black;

            iconButton13.FillColor = Color.Transparent;
            iconButton13.ForeColor = Color.Black;

            btnestimasi.FillColor = Color.Transparent;
            btnestimasi.ForeColor = Color.Black;

            btnpemakaian.FillColor = Color.Transparent;
            btnpemakaian.ForeColor = Color.Black;

            btnmaterialmasuk.FillColor = Color.Transparent;
            btnmaterialmasuk.ForeColor = Color.Black;

            btntambahmaterial.FillColor = Color.Transparent;
            btntambahmaterial.ForeColor = Color.Black;

            btnlaporanpersediaan.FillColor = Color.Transparent;
            btnlaporanpersediaan.ForeColor = Color.Black;

            btndata.FillColor = Color.Transparent;
            btndata.ForeColor = Color.Black;

            guna2Button1.FillColor = Color.Transparent;
            guna2Button1.ForeColor = Color.Black;

            btnchartpermaterial.FillColor = Color.Transparent;
            btnchartpermaterial.ForeColor = Color.Black;
        }

        public void ResetColorContainer()
        {
            entryButton.FillColor = Color.White;
            entryButton.ForeColor = Color.Black;

            Editbutton.FillColor = Color.White;
            Editbutton.ForeColor = Color.Black;

            btngudang.FillColor = Color.White;
            btngudang.ForeColor = Color.Black;

            btnHistori.FillColor = Color.White;
            btnHistori.ForeColor = Color.Black;
        }

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
        }

        private NotifyIcon trayNotifier;
        private toastform toast;
        private bool isDisconnected = false;
        private bool isDependencyActive = false;
        public static event Func<string, Task> DataChanged;
        private readonly Dictionary<string, SqlDependency> depMap =
    new Dictionary<string, SqlDependency>();

        private async Task<bool> CheckSqlConnection()
        {
            try
            {
                using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private async void StartConnectionWatcher()
        {
            while (true)
            {
                bool connected = await CheckSqlConnection();

                if (!connected && !isDisconnected)
                {
                    isDisconnected = true;
                    ShowToast();
                    StopSqlDependency();
                    Console.WriteLine("DB disconnected, stopped dependency.");
                }
                else if (connected && isDisconnected)
                {
                    isDisconnected = false;
                    HideToast();
                    RestartSqlDependency();
                    Console.WriteLine("DB reconnected, restarted dependency.");
                }

                await Task.Delay(2000);
            }
        }

        private void InitializeTrayNotifier()
        {
            trayNotifier = new NotifyIcon
            {
                Visible = true,
                Icon = SystemIcons.Warning
            };
        }

        private void ShowToast()
        {
            if (toast != null && toast.Visible)
                return;

            if (toast == null || toast.IsDisposed)
                toast = new toastform();

            toast.Show();
        }

        private void HideToast()
        {
            if (toast != null && !toast.IsDisposed)
                toast.Hide();
        }

        private void StartSqlDependency()
        {
            if (isDependencyActive) return;

            try
            {
                SqlDependency.Start(Koneksi.GetConnectionString());
                RegisterAllDependencies();
                isDependencyActive = true;
                Console.WriteLine("SqlDependency started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StartSqlDependency Error: {ex.Message}");
            }
        }

        private void StopSqlDependency()
        {
            if (!isDependencyActive) return;

            try
            {
                lock (depMap)
                {
                    foreach (var dep in depMap.Values)
                    {
                        try { dep.OnChange -= null; } catch { }
                    }
                    depMap.Clear();
                }

                SqlDependency.Stop(Koneksi.GetConnectionString());
                isDependencyActive = false;
                Console.WriteLine("SqlDependency stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StopSqlDependency Error: {ex.Message}");
            }
        }

        private void RestartSqlDependency()
        {
            StopSqlDependency();
            StartSqlDependency();
        }

        private void RegisterAllDependencies()
        {
            string[] tables = new string[]
            {
        "stok_material",
        "setmin_Rb",
        "Rb_Stok",
        "penerimaan_s",
        "penerimaan_p",
        "perbaikan_s",
        "perbaikan_p",
        "pengiriman",
        "perputaran_rod",
        "kondisiROD",
        "material_masuk",
        "pemakaian_material",
        "koefisiensi_material",
        "users"

            };

            foreach (var t in tables)
                RegisterDependency(t);
        }

        private void RegisterDependency(string tableName)
        {
            Task.Run(async () =>
            {
                if (isDisconnected) return;

                try
                {
                    using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
                    using (var cmd = new SqlCommand($"SELECT updated_at FROM dbo.{tableName}", conn))
                    {
                        cmd.Notification = null;
                        var dep = new SqlDependency(cmd);

                        dep.OnChange += async (s, e) =>
                        {
                            try
                            {
                                Console.WriteLine($"Dependency fired for {tableName}, Type={e.Type}");
                                if (e.Type == SqlNotificationType.Change && DataChanged != null)
                                {
                                    if (InvokeRequired)
                                        BeginInvoke((Action)(async () => await DataChanged.Invoke(tableName)));
                                    else
                                        await DataChanged.Invoke(tableName);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"OnChange handler failed for {tableName}: {ex.Message}");
                            }

                            await Task.Delay(100); 
                            RegisterDependency(tableName);
                        };

                        lock (depMap)
                        {
                            if (depMap.ContainsKey(tableName))
                                depMap[tableName] = dep;
                            else
                                depMap.Add(tableName, dep);
                        }

                        conn.Open();
                        await cmd.ExecuteReaderAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"RegisterDependency failed for {tableName}: {ex.Message}");
                    await Task.Delay(1500);
                    if (!isDisconnected)
                        RegisterDependency(tableName);
                }
            });
        }

        private async Task OnDatabaseChanged(string table)
        {
            try
            {
                switch (table)
                {
                    case "stok_material":
                        await LoadNotifikasi();
                        break;

                    case "setmin_Rb":
                        await LoadNotifikasi();
                        break;

                    case "Rb_Stok":
                        await LoadNotifikasi();
                        break;

                    default:
                        break;
                }
            }
            catch { }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            DataChanged += OnDatabaseChanged;

            InitializeTrayNotifier();
            StartConnectionWatcher();
            StartSqlDependency();

            jam.Start();
            shiftcontrol();

            tanggal = DateTime.Now;
            lbluser.Text = "";

            SwitchPanel(new Dashboard());
            dashboardButton.FillColor = Color.FromArgb(52, 52, 57);
            dashboardButton.ForeColor = Color.White;

            alertTimer = new Timer();
            alertTimer.Interval = 150;
            alertTimer.Tick += AlertTimer_Tick;

            if (!loginstatus) setvisiblefalse();

            defaulhistorycontainer = historycontainer.Size;
            defaultentrycontainer = entryContainer.Size;
            defaulteditcontainer = EditContainer.Size;
            defaulgudangcontainer = gudangContainer.Size;

            await LoadNotifikasi();
        }

        public async Task LoadNotifikasi()
        {
            List<(string Text, DateTime Waktu, Color WarnaText, Color WarnaWaktu)> notifList =
                new List<(string, DateTime, Color, Color)>();

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    string query2 = @"SELECT TOP 1 bstok, bpe1, bpe2, bbe1, bbe2, 
                                 wpe1, wpe2, wbe1, wbe2, updated_at
                          FROM Rb_Stok ORDER BY id_stok DESC";

                    using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                    using (SqlDataReader reader2 = await cmd2.ExecuteReaderAsync())
                    {
                        Dictionary<string, int> stokData = new Dictionary<string, int>();
                        DateTime waktuRb = DateTime.Now;

                        if (await reader2.ReadAsync())
                        {
                            string[] kolomList =
                            {
                    "bstok","bpe1","bpe2","bbe1","bbe2",
                    "wpe1","wpe2","wbe1","wbe2"
                    };

                            foreach (string kolom in kolomList)
                                stokData[kolom] = Convert.ToInt32(reader2[kolom]);

                            waktuRb = Convert.ToDateTime(reader2["updated_at"]);
                        }

                        reader2.Close();

                        foreach (var item in stokData)
                        {
                            using (SqlCommand cmdMin = new SqlCommand(
                                "SELECT namaTampilan, min_stok FROM setmin_Rb WHERE kode = @kode", conn))
                            {
                                cmdMin.Parameters.AddWithValue("@kode", item.Key);
                                using (SqlDataReader rdrMin = await cmdMin.ExecuteReaderAsync())
                                {
                                    if (await rdrMin.ReadAsync())
                                    {
                                        string nama = rdrMin["namaTampilan"].ToString();
                                        int minStok = Convert.ToInt32(rdrMin["min_stok"]);

                                        if (item.Value < minStok)
                                        {
                                            notifList.Add((
                                                $"{nama} Stok Rendah ({item.Value}/{minStok})",
                                                waktuRb,
                                                Color.FromArgb(255, 0, 0),
                                                Color.Gray
                                            ));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    string query1 = @"SELECT namaBarang, jumlahStok, min_stok, updated_at
                          FROM stok_material 
                          WHERE jumlahStok < min_stok";

                    using (SqlCommand cmd1 = new SqlCommand(query1, conn))
                    using (SqlDataReader reader1 = await cmd1.ExecuteReaderAsync())
                    {
                        while (await reader1.ReadAsync())
                        {
                            notifList.Add((
                                $"Material {reader1["namaBarang"]} Stok Rendah ({reader1["jumlahStok"]}/{reader1["min_stok"]})",
                                Convert.ToDateTime(reader1["updated_at"]),
                                Color.FromArgb(255, 0, 0),
                                Color.Gray
                            ));
                        }
                    }
                }
            }
            catch
            {

            }

            totalNotifikasi = notifList.Count;
            btnnotif.Text = "🔔 " + totalNotifikasi;

            var sortedNotif = notifList
                .OrderByDescending(n => n.Waktu)
                .ThenByDescending(n => n.Text)
                .ToList();
        }

        //kode utama
        public void SwitchPanel(Form panel)
        {
            panel4.Controls.Clear();
            panel.TopLevel = false;
            panel4.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
            panel.Show();
        }

        private void AlertTimer_Tick(object sender, EventArgs e)
        {
            lblalert.Visible = !lblalert.Visible;
        }

        public void StartAlert()
        {
            lblalert.Visible = true;
            alertTimer.Start();
        }

        public void StopAlert()
        {
            alertTimer.Stop();
            lblalert.Visible = false;
        }

        private void HamburgerButton_Click_1(object sender, EventArgs e)
        {
            if (sidebarx)
            {
                sidebarPanel.Width = sidebarPanel.MaximumSize.Width;
                sidebarx = false;
            }
            else
            {
                sidebarPanel.Width = sidebarPanel.MinimumSize.Width;
                sidebarx = true;
            }
        }

        private void dashboardButton_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();

            dashboardButton.FillColor = Color.FromArgb(64, 64, 64);
            dashboardButton.ForeColor = Color.White;

            SwitchPanel(new Dashboard());
        }

        private void entryButton_Click_1(object sender, EventArgs e)
        {
            if (entryprodx)
            {
                entryContainer.Height = entryContainer.MinimumSize.Height;
                entryButton.FillColor = Color.White;
                entryButton.ForeColor = Color.Black;
                entryprodx = false;
            }
            else
            {
                entryContainer.Height = entryContainer.MaximumSize.Height;
                entryButton.FillColor = Color.Gray;
                entryButton.ForeColor = Color.White;
                entryprodx = true;
            }
        }
        private void penerimaanButton1_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            penerimaanButton1.FillColor = Color.FromArgb(64, 64, 64);
            penerimaanButton1.ForeColor = Color.White;

            SwitchPanel(new Penerimaan());
        }
        private void btnbuktiperubahan_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnbuktiperubahan.FillColor = Color.FromArgb(64, 64, 64);
            btnbuktiperubahan.ForeColor = Color.White;

            SwitchPanel(new formperubahanperbaikan());
        }
        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton1.FillColor = Color.FromArgb(64, 64, 64);
            iconButton1.ForeColor = Color.White;

            SwitchPanel(new Perbaikan());
        }
        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton2.FillColor = Color.FromArgb(64, 64, 64);
            iconButton2.ForeColor = Color.White;

            SwitchPanel(new Pengiriman());
        }
        private void iconButton3_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton3.FillColor = Color.FromArgb(64, 64, 64);
            iconButton3.ForeColor = Color.White;

            SwitchPanel(new weldingp());
        }
        private void iconButton15_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton15.FillColor = Color.FromArgb(64, 64, 64);
            iconButton15.ForeColor = Color.White;
            SwitchPanel(new formbuttman());
        }

        private void Editbutton_Click_1(object sender, EventArgs e)
        {
            if (editx)
            {
                EditContainer.Height = EditContainer.MinimumSize.Height;
                Editbutton.FillColor = Color.White;
                Editbutton.ForeColor = Color.Black;
                editx = false;
            }
            else
            {
                EditContainer.Height = EditContainer.MaximumSize.Height;
                Editbutton.FillColor = Color.Gray;
                Editbutton.ForeColor = Color.White;
                editx = true;
            }
        }
        private void iconButton9_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton9.FillColor = Color.FromArgb(64, 64, 64);
            iconButton9.ForeColor = Color.White;

            SwitchPanel(new editpenerimaan());
        }
        private void iconButton8_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton8.FillColor = Color.FromArgb(64, 64, 64);
            iconButton8.ForeColor = Color.White;

            SwitchPanel(new editperbaikan());
        }
        private void btneditbuktiperubahan_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btneditbuktiperubahan.FillColor = Color.FromArgb(64, 64, 64);
            btneditbuktiperubahan.ForeColor = Color.White;

            SwitchPanel(new formeditperubahanperbaikan());
        }
        private void iconButton12_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Form ini sedang Maintenance.",
            //                   "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ResetButtonColors();
            iconButton12.FillColor = Color.FromArgb(64, 64, 64);
            iconButton12.ForeColor = Color.White;
            SwitchPanel(new koefisiensi());
        }

        private void btnlaporan_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnlaporan.FillColor = Color.FromArgb(64, 64, 64);
            btnlaporan.ForeColor = Color.White;

            SwitchPanel(new printpenerimaan());
        }

        private void btnHistori_Click(object sender, EventArgs e)
        {
            if (historiy)
            {
                historycontainer.Height = historycontainer.MinimumSize.Height;
                btnHistori.FillColor = Color.White;
                btnHistori.ForeColor = Color.Black;
                historiy = false;
            }
            else
            {
                historycontainer.Height = historycontainer.MaximumSize.Height;
                btnHistori.FillColor = Color.Gray;
                btnHistori.ForeColor = Color.White;
                historiy = true;
            }
        }
        private void iconButton24_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton24.FillColor = Color.FromArgb(64, 64, 64);
            iconButton24.ForeColor = Color.White;

            SwitchPanel(new historyPenerimaan());
        }
        private void iconButton11_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton11.FillColor = Color.FromArgb(64, 64, 64);
            iconButton11.ForeColor = Color.White;

            SwitchPanel(new historyPerbaikan());
        }
        private void iconButton10_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton10.FillColor = Color.FromArgb(64, 64, 64);
            iconButton10.ForeColor = Color.White;

            SwitchPanel(new historyPengiriman());
        }
        private void iconButton6_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton6.FillColor = Color.FromArgb(64, 64, 64);
            iconButton6.ForeColor = Color.White;

            SwitchPanel(new historyWelding());
        }
        private void iconButton5_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton5.FillColor = Color.FromArgb(64, 64, 64);
            iconButton5.ForeColor = Color.White;

            SwitchPanel(new historyPemakaianMaterial());
        }
        private void iconButton13_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            iconButton13.FillColor = Color.FromArgb(64, 64, 64);
            iconButton13.ForeColor = Color.White;

            SwitchPanel(new historyStokMaterial());
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            guna2Button1.FillColor = Color.FromArgb(64, 64, 64);
            guna2Button1.ForeColor = Color.White;

            SwitchPanel(new historymaterialmasuk());
        }
        private void btnestimasi_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Form ini sedang Maintenance.",
            //                   "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ResetButtonColors();
            btnestimasi.FillColor = Color.FromArgb(64, 64, 64);
            btnestimasi.ForeColor = Color.White;
            SwitchPanel(new EstimasiPemakaianMaterial());
        }

        private void btngudang_Click(object sender, EventArgs e)
        {
            if (gudangx)
            {
                gudangContainer.Height = gudangContainer.MinimumSize.Height;
                btngudang.FillColor = Color.White;
                btngudang.ForeColor = Color.Black;
                gudangx = false;
            }
            else
            {
                gudangContainer.Height = gudangContainer.MaximumSize.Height;
                btngudang.FillColor = Color.Gray;
                btngudang.ForeColor = Color.White;
                gudangx = true;
            }
        }
        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnpemakaian.FillColor = Color.FromArgb(64, 64, 64);
            btnpemakaian.ForeColor = Color.White;

            SwitchPanel(new pemakaianMaterial());
        }
        private void btnmaterialmasuk_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnmaterialmasuk.FillColor = Color.FromArgb(64, 64, 64);
            btnmaterialmasuk.ForeColor = Color.White;

            SwitchPanel(new materialmasuk());
        }
        private void btntambahmaterial_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btntambahmaterial.FillColor = Color.FromArgb(64, 64, 64);
            btntambahmaterial.ForeColor = Color.White;

            SwitchPanel(new formstok());
        }
        private void btnlaporanpersediaan_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnlaporanpersediaan.FillColor = Color.FromArgb(64, 64, 64);
            btnlaporanpersediaan.ForeColor = Color.White;

            SwitchPanel(new laporanpersediaan());
        }
        private void btndata_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btndata.FillColor = Color.FromArgb(64, 64, 64);
            btndata.ForeColor = Color.White;

            SwitchPanel(new datamaterial());
        }
        private void btnchartpermaterial_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnchartpermaterial.FillColor = Color.FromArgb(64, 64, 64);
            btnchartpermaterial.ForeColor = Color.White;

            SwitchPanel(new chartpermaterial());
        }

        //kode fitur terbatas
        public void setvisiblefalse()
        {
            entryContainer.Visible = false;
            EditContainer.Visible = false; 
            btnlaporan.Visible = false;
            historycontainer.Visible = false;
            gudangContainer.Visible = false;
            btnaturjam.Visible = false;
        }
        public void truemanajer()
        {
            entryContainer.MaximumSize = new Size(232, 255);
            entryContainer.Visible = true;
            penerimaanButton1.Visible = true;
            btnbuktiperubahan.Visible = true;
            iconButton1.Visible = true;
            iconButton2.Visible = true;
            iconButton3.Visible = true;
            btnpemakaian.Visible = true;
            iconButton15.Visible = true;
            EditContainer.MaximumSize = new Size(232, 178);
            EditContainer.Visible = true;
            iconButton12.Visible = true;
            btnHistori.Visible = true;
            btnlaporan.Visible = true;

            historycontainer.Visible = true;
            historycontainer.MaximumSize = new Size(232, 348);
            iconButton24.Visible = true;
            iconButton11.Visible = true;
            iconButton10.Visible = true;
            iconButton6.Visible = true;
            iconButton5.Visible = true;
            guna2Button1.Visible = true;
            iconButton13.Visible = true;

            gudangContainer.Visible = true;
            gudangContainer.MaximumSize = new Size(231, 415);
            iconButton12.Visible = true;
            iconButton3.Visible=true;
            btnmaterialmasuk.Visible = true;
            btnpemakaian.Visible=true;
            btntambahmaterial.Visible = true;
            btnestimasi.Visible=true;
            btnlaporanpersediaan.Visible=true;
            btndata.Visible=true;

            btnaturjam.Visible=true;
        }
        public void trueadmin()
        {
            gudangContainer.Visible = true;
            gudangContainer.MaximumSize = new Size(231,215);
            iconButton12.Visible = false;
            iconButton3.Visible = false;
            btnmaterialmasuk.Visible = false;
            btnpemakaian.Visible = false;
            btntambahmaterial.Visible = false;
            btnestimasi.Visible = true;
            btnlaporanpersediaan.Visible = true;
            btndata.Visible = true;

            btnlaporan.Visible=true;
            historycontainer.Visible= true;
            historycontainer.Visible = true;
            historycontainer.MaximumSize = new Size(232, 348);
            iconButton24.Visible = true;
            iconButton11.Visible = true;
            iconButton10.Visible = true;
            iconButton6.Visible = true;
            iconButton5.Visible = true;
            guna2Button1.Visible = true;
            iconButton13.Visible = true;

            btnaturjam.Visible = false;
        }
        public void trueoperatorgudang()
        {
            gudangContainer.Visible = true;
            gudangContainer.MaximumSize = new Size(231, 415);
            iconButton3.Visible = true;
            btnmaterialmasuk.Visible = true;
            btnpemakaian.Visible = true;
            btntambahmaterial.Visible = true;
            btnestimasi.Visible = true;
            btnlaporanpersediaan.Visible = true;
            btndata.Visible = true;
            btnlaporan.Visible = true;

            iconButton24.Visible = false;
            iconButton11.Visible = false;
            iconButton10.Visible = false;

            historycontainer.Visible = true;
            historycontainer.MaximumSize = new Size(232, 225);
            iconButton6.Visible = true;
            iconButton5.Visible = true;
            guna2Button1.Visible = true;
            iconButton13.Visible = true;
            iconButton12.Visible = true;

            btnaturjam.Visible = false;
        }
        public void trueoperator()
        {
            iconButton1.Visible = false;
            iconButton15.Visible = false;
            iconButton12.Visible = false;

            iconButton6.Visible = false;
            iconButton5.Visible = false;
            guna2Button1.Visible = false;
            iconButton13.Visible = false;

            entryContainer.Visible = true;
            entryContainer.MaximumSize = new Size(232, 215);
            EditContainer.Visible = true;
            EditContainer.MaximumSize = new Size(232, 178);
            penerimaanButton1.Visible = true;
            btnbuktiperubahan.Visible = true;
            iconButton1.Visible = true;
            iconButton2.Visible = true;
            iconButton15.Visible = false;
            btnlaporan.Visible = true;
            historycontainer.Visible = true;
            historycontainer.MaximumSize = new Size(232, 177);
            iconButton24.Visible = true;
            iconButton11.Visible = true;
            iconButton10.Visible = true;

            btnaturjam.Visible = true;
        }
        public void trueforeman()
        {
            iconButton1.Visible = false;
            iconButton15.Visible = false;
            iconButton12.Visible = false;

            iconButton6.Visible = false;
            iconButton5.Visible = false;
            guna2Button1.Visible = false;
            iconButton13.Visible = false;

            entryContainer.Visible = true;
            entryContainer.MaximumSize = new Size(232, 255);
            EditContainer.Visible = true;
            EditContainer.MaximumSize = new Size(232, 178);
            penerimaanButton1.Visible = true;
            btnbuktiperubahan.Visible = true;
            iconButton1.Visible = true;
            iconButton2.Visible = true;
            iconButton15.Visible = true;
            btnlaporan.Visible = true;
            historycontainer.Visible = true;
            historycontainer.MaximumSize = new Size(232, 177);
            iconButton24.Visible = true;
            iconButton11.Visible = true;
            iconButton10.Visible = true;

            btnaturjam.Visible = true;
        }


        //kode untuk panel atas
        public void shiftcontrol()
        {
            TimeSpan sekarang = tanggal.TimeOfDay;

            if (sekarang >= TimeSpan.Parse("00:00:00") && sekarang <= TimeSpan.Parse("07:59:59"))
                shift = "1";
            else if (sekarang >= TimeSpan.Parse("08:00:00") && sekarang <= TimeSpan.Parse("15:59:59"))
                shift = "2";
            else if (sekarang >= TimeSpan.Parse("16:00:00") && sekarang <= TimeSpan.Parse("23:59:59"))
                shift = "3";

            lblshift.Text = shift;
        }

        public void jam_Tick(object sender, EventArgs e)
        {
            if (isManual)
            {
                tanggal = tanggal.AddSeconds(1);
            }
            else
            {
                tanggal = DateTime.Now;
            }

            lbldate.Text = tanggal.ToString("dddd, dd MMMM yyyy  [HH:mm:ss]");
            shiftcontrol();
        }

        private userinfo form = null;
        private void iconButton14_Click(object sender, EventArgs e)
        {
            if (loginstatus == true)
            {
                if (form == null || form.IsDisposed)
                {
                    form = new userinfo();

                    Point lokasi = iconButton14.PointToScreen(Point.Empty);

                    int formwidth = form.Width;
                    int formheight = form.Height;

                    int x = lokasi.X + iconButton14.Width - formwidth;
                    int y = lokasi.Y + iconButton14.Height;

                    form.StartPosition = FormStartPosition.Manual;
                    form.Location = new Point(x, y);

                    form.FormClosed += (s, args) =>
                    {
                        form = null;
                    };

                    form.Show();
                }
            }
            else
            {
                Form login = new loginform();
                login.ShowDialog();
            }
        }

        private formnotifikasi formnotif = null;
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (formnotif == null || formnotif.IsDisposed)
            {
                formnotif = new formnotifikasi();

                Point lokasi = btnnotif.PointToScreen(Point.Empty);

                int formwidth = formnotif.Width;
                int formheight = formnotif.Height;

                int x = lokasi.X + btnnotif.Width - formwidth;
                int y = lokasi.Y + btnnotif.Height;

                Rectangle workingArea = Screen.GetWorkingArea(btnnotif);

                if (x + formwidth > workingArea.Right)
                {
                    x = workingArea.Right - formwidth;
                }

                if (y + formheight > workingArea.Bottom)
                {
                    y = lokasi.Y - formheight;
                }

                formnotif.StartPosition = FormStartPosition.Manual;
                formnotif.Location = new Point(x, y);

                formnotif.FormClosed += (s, args) =>
                {
                    formnotif = null;
                };

                formnotif.Show();
                formnotif.BringToFront();
            }
            else if (!formnotif.Visible)
            {
                formnotif.Show();
                formnotif.BringToFront();
                formnotif.Activate();
            }
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataChanged -= OnDatabaseChanged;
        }

        private void btnaturjam_Click(object sender, EventArgs e)
        {
            aturjam f = new aturjam();
            f.Owner = this;   
            f.ShowDialog();
        }
    }
}
