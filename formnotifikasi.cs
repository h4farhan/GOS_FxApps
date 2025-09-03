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

namespace GOS_FxApps
{
    public partial class formnotifikasi : Form
    {
        SqlConnection conn = Koneksi.GetConnection();

        public static formnotifikasi Instance;

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
        private void LoadNotifikasi()
        {
            int scrollPosition = panelNotif.VerticalScroll.Value;

            panelNotif.SuspendLayout();
            panelNotif.Controls.Clear();

            List<(string Text, DateTime Waktu, Color WarnaText, Color WarnaWaktu)> notifList =
                new List<(string, DateTime, Color, Color)>();

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                conn.Open();

                string query2 = @"SELECT TOP 1 bstok, bpe1, bpe2, bbe1, bbe2, wpe1, wpe2, wbe1, wbe2, updated_at 
                          FROM Rb_Stok ORDER BY id_stok DESC";

                using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                {
                    Dictionary<string, int> stokData = new Dictionary<string, int>();
                    DateTime waktuRb = DateTime.Now;

                    if (reader2.Read())
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
                            using (SqlDataReader rdrMin = cmdMin.ExecuteReader())
                            {
                                if (rdrMin.Read())
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
                using (SqlDataReader reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
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

        private void registerstok()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.stok_material", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        if (!this.IsDisposed && this.IsHandleCreated)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                LoadNotifikasi();
                                registerstok();
                            }));
                        }
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerSetminRb()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.setmin_Rb", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        if (!this.IsDisposed && this.IsHandleCreated)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                LoadNotifikasi();
                                registerSetminRb();
                            }));
                        }
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }

        private void registerwelding()
        {
            using (var conn = new SqlConnection(Koneksi.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT updated_at FROM dbo.Rb_Stok", conn))
            {
                cmd.Notification = null;
                var dep = new SqlDependency(cmd);
                dep.OnChange += (s, e) =>
                {
                    if (e.Type == SqlNotificationType.Change)
                    {
                        if (!this.IsDisposed && this.IsHandleCreated)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                LoadNotifikasi();
                                registerwelding();
                            }));
                        }
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
        }


        private void formnotifikasi_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, panelNotif, new object[] { true });
            Instance = this;
            if (MainForm.Instance != null && MainForm.Instance.role != null)
            {
                btntiga.Visible = (MainForm.Instance.role == "Operator Gudang" || MainForm.Instance.role == "Manajer");
            }
            else
            {
                btntiga.Visible = false;
            }
            LoadNotifikasi();
            registerSetminRb();
            registerstok();
            registerwelding();
        }

        private void formnotifikasi_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }

        private void btntiga_Click(object sender, EventArgs e)
        {
            Form setmin = new setmin_rb();
            setmin.Show();
        }
    }
}
