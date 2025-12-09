using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Management.Instrumentation;
using System.Threading;

namespace GOS_FxApps
{
    public partial class formnotifikasi : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public static formnotifikasi Instance;
        private bool allowDeactivate = false;

        public formnotifikasi()
        {
            InitializeComponent();
        }

        public class RoundedPanel : Panel
        {
            public int BorderRadius { get; set; } = 15;

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                using (GraphicsPath path = new GraphicsPath())
                {
                    Rectangle rect = this.ClientRectangle;
                    int radius = BorderRadius;

                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    this.Region = new Region(path);
                }
            }
        }

        private async Task LoadNotifikasi()
        {
            int scrollPosition = panelNotif.VerticalScroll.Value;

            panelNotif.SuspendLayout();
            panelNotif.Controls.Clear();

            List<(string Text, DateTime Waktu, Color WarnaText, Color WarnaWaktu)> notifList =
                new List<(string, DateTime, Color, Color)>();

            try
            {
                using (var conn = await Koneksi.GetConnectionAsync())
                {
                    string query2 = @"SELECT TOP 1 bstok, bpe1, bpe2, bbe1, bbe2, wpe1, wpe2, wbe1, wbe2, updated_at 
                              FROM Rb_Stok ORDER BY id_stok DESC";

                    using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                    using (SqlDataReader reader2 =await cmd2.ExecuteReaderAsync())
                    {
                        Dictionary<string, int> stokData = new Dictionary<string, int>();
                        DateTime waktuRb = DateTime.Now;

                        if (await reader2.ReadAsync())
                        {
                            string[] kolomList = { "bstok", "bpe1", "bpe2", "bbe1", "bbe2", "wpe1", "wpe2", "wbe1", "wbe2" };

                            foreach (string kolom in kolomList)
                            {
                                stokData[kolom] = Convert.ToInt32(reader2[kolom]);
                            }

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
                              FROM stok_material WHERE jumlahStok < min_stok";

                    using (SqlCommand cmd1 = new SqlCommand(query1, conn))
                    using (SqlDataReader reader1 = await cmd1.ExecuteReaderAsync())
                    {
                        while (await reader1.ReadAsync())
                        {
                            string nama = reader1["namaBarang"].ToString();
                            int stok = Convert.ToInt32(reader1["jumlahStok"]);
                            int minStok = Convert.ToInt32(reader1["min_stok"]);
                            DateTime waktu = Convert.ToDateTime(reader1["updated_at"]);

                            notifList.Add((
                                $"Material {nama} Stok Rendah ({stok}/{minStok})",
                                waktu,
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

            var sortedNotif = notifList
            .OrderByDescending(n => n.Waktu)
            .ThenByDescending(n => n.Text)
            .ToList();

            foreach (var notif in sortedNotif)
            {
                AddNotifPanel(
                    notif.Text,
                    notif.Waktu.ToString("dd MMM yyyy HH:mm"),
                    notif.WarnaText,
                    notif.WarnaWaktu
                );
            }

            panelNotif.AutoScroll = true;
            panelNotif.ResumeLayout();
            panelNotif.VerticalScroll.Value = Math.Min(scrollPosition, panelNotif.VerticalScroll.Maximum);
            panelNotif.PerformLayout();
        }

        private void AddNotifPanel(string text, string waktu, Color warnaText, Color warnaWaktu)
        {
            RoundedPanel itemPanel = new RoundedPanel();
            itemPanel.Width = panelNotif.Width - 25;
            itemPanel.Height = text.Contains("Material") ? 130 : 110;
            itemPanel.BackColor = Color.WhiteSmoke;
            itemPanel.Margin = new Padding(3);
            itemPanel.Padding = new Padding(10);
            itemPanel.BorderRadius = 10;
            itemPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Panel teksPanel = new Panel();
            teksPanel.Dock = DockStyle.Fill;
            teksPanel.BackColor = Color.WhiteSmoke;
            teksPanel.Padding = new Padding(5);

            Label icon = new Label();
            icon.Text = "🔔";
            icon.Font = new Font("Segoe UI Emoji", 16);
            icon.Dock = DockStyle.Left;
            icon.Width = 40;
            icon.TextAlign = ContentAlignment.MiddleCenter;

            Label lblText = new Label();
            lblText.Text = text;
            lblText.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            lblText.ForeColor = warnaText;
            lblText.AutoSize = true;
            lblText.MaximumSize = new Size(teksPanel.Width - 10, 0);
            lblText.Dock = DockStyle.Top;

            Label lblWaktu = new Label();
            lblWaktu.Text = waktu;
            lblWaktu.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblWaktu.ForeColor = warnaWaktu;
            lblWaktu.AutoSize = true;
            lblWaktu.Dock = DockStyle.Bottom;

            teksPanel.Controls.Add(lblWaktu);
            teksPanel.Controls.Add(lblText);
            teksPanel.Controls.Add(icon);

            itemPanel.Controls.Add(teksPanel);

            panelNotif.Controls.Add(itemPanel);
        }

        private CancellationTokenSource reloadToken;

        private async Task OnDatabaseChanged(string table)
        {
            string[] affected =
            {
                "stok_material", "setmin_Rb",
                "Rb_Stok"
            };

            if (!affected.Contains(table))
                return;

            reloadToken?.Cancel();
            reloadToken = new CancellationTokenSource();

            try
            {
                await Task.Delay(300, reloadToken.Token);
                await LoadNotifikasi();
            }
            catch (TaskCanceledException)
            {
            }
        }

        private async void formnotifikasi_Load(object sender, EventArgs e)
        {
            MainForm.DataChanged += OnDatabaseChanged;
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, panelNotif, new object[] { true });
            Instance = this;
            if (MainForm.Instance != null && MainForm.Instance.role != null)
            {
                btntiga.Visible = (MainForm.Instance.role == "Operator Gudang" || MainForm.Instance.role == "Manajer" || MainForm.Instance.role == "Developer");
            }
            else
            {
                btntiga.Visible = false;
            }
            await LoadNotifikasi();
        }

        private void formnotifikasi_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.DataChanged -= OnDatabaseChanged;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 200; 
            t.Tick += (s, ev) =>
            {
                allowDeactivate = true;
                t.Stop();
                t.Dispose();
            };
            t.Start();

            this.Activate();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            if (allowDeactivate)
            {
                this.Close();
            }
        }

        private void btntiga_Click(object sender, EventArgs e)
        {
            Form setmin = new setmin_rb();
            setmin.Show();
        }
    }
}
