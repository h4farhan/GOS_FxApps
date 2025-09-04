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

        public string role = null;

        public DateTime tanggal;

        public void ResetButtonColors()
        {
            dashboardButton.FillColor = Color.White;
            dashboardButton.ForeColor = Color.Black;

            penerimaanButton1.FillColor = Color.Transparent;
            penerimaanButton1.ForeColor = Color.Black;

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
        }

        public void ResetColorContainer()
        {
            entryButton.FillColor = Color.White;
            entryButton.ForeColor = Color.Black;

            Editbutton.FillColor = Color.White;
            Editbutton.ForeColor = Color.Black;

            btnHistori.FillColor = Color.White;
            btnHistori.ForeColor = Color.Black;
        }

        public MainForm()
        {
            InitializeComponent();
            shiftcontrol();
            lbluser.Text = "";
            setvisiblefalse();
        }

        public void LoadNotifikasi()
        {

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
            totalNotifikasi = notifList.Count;
            btnnotif.Text = "🔔 " + totalNotifikasi;
            var sortedNotif = notifList
            .OrderByDescending(n => n.Waktu)
            .ThenByDescending(n => n.Text)
            .ToList();
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
                        this.Invoke(new Action(() =>
                        {
                            LoadNotifikasi();
                            registerstok();
                        }));
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
                        this.Invoke(new Action(() =>
                        {
                            LoadNotifikasi();
                            registerSetminRb();
                        }));
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
                        this.Invoke(new Action(() =>
                        {
                            LoadNotifikasi();
                            registerwelding();
                        }));
                    }
                };
                conn.Open();
                cmd.ExecuteReader();
            }
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(Koneksi.GetConnectionString());
            SwitchPanel(new Dashboard());
            dashboardButton.FillColor = Color.FromArgb(52, 52, 57);
            dashboardButton.ForeColor = Color.White;
            Instance = this;
            if (!loginstatus)
            {
                setvisiblefalse();
            }
            defaulhistorycontainer = historycontainer.Size;
            defaultentrycontainer = entryContainer.Size;
            defaulteditcontainer = EditContainer.Size;

            LoadNotifikasi();
            registerSetminRb();
            registerstok();
            registerwelding();
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
        private void entrytimer_Tick(object sender, EventArgs e)
        {
            if (entryprodx)
            {
                entryContainer.Height += 10;
                if (entryContainer.Height >= entryContainer.MaximumSize.Height)
                {
                    entrytimer.Stop();
                    entryprodx = false;
                    entryButton.FillColor = Color.Gray;
                    entryButton.ForeColor = Color.White;
                }
            }
            else
            {
                entryContainer.Height -= 10;
                if (entryContainer.Height <= entryContainer.MinimumSize.Height)
                {
                    entrytimer.Stop();
                    entryprodx = true;
                    entryButton.FillColor = Color.White;
                    entryButton.ForeColor = Color.Black;
                }

            }
        }
        private void edittimer_Tick(object sender, EventArgs e)
        {
            if (editx)
            {
                EditContainer.Height += 10;
                if (EditContainer.Height >= EditContainer.MaximumSize.Height)
                {
                    edittimer.Stop();
                    editx = false;
                    Editbutton.FillColor = Color.Gray;
                    Editbutton.ForeColor = Color.White;
                }
            }
            else
            {
                EditContainer.Height -= 10;
                if (EditContainer.Height <= EditContainer.MinimumSize.Height)
                {
                    edittimer.Stop();
                    editx = true;
                    Editbutton.FillColor = Color.White;
                    Editbutton.ForeColor = Color.Black;
                }

            }
        }  
        private void historitimer_Tick(object sender, EventArgs e)
        {
            if (historiy)
            {
                historycontainer.Height += 10;
                if (historycontainer.Height >= historycontainer.MaximumSize.Height)
                {
                    historitimer.Stop();
                    historiy = false;
                    btnHistori.FillColor = Color.Gray;
                    btnHistori.ForeColor = Color.White;
                }
            }
            else
            {
                historycontainer.Height -= 10;
                if (historycontainer.Height <= historycontainer.MinimumSize.Height)
                {
                    historitimer.Stop();
                    historiy = true;
                    btnHistori.FillColor = Color.White;
                    btnHistori.ForeColor = Color.Black;
                }

            }
        }
        private void gudangtimer_Tick(object sender, EventArgs e)
        {
            if (gudangx)
            {
                gudangContainer.Height += 10;
                if (gudangContainer.Height >= gudangContainer.MaximumSize.Height)
                {
                    gudangtimer.Stop();
                    gudangx = false;
                    btngudang.FillColor = Color.Gray;
                    btngudang.ForeColor = Color.White;
                }
            }
            else
            {
                gudangContainer.Height -= 10;
                if (gudangContainer.Height <= gudangContainer.MinimumSize.Height)
                {
                    gudangtimer.Stop();
                    gudangx = true;
                    btngudang.FillColor = Color.White;
                    btngudang.ForeColor = Color.Black;
                }

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
            entrytimer.Start();
        }
        private void penerimaanButton1_Click_1(object sender, EventArgs e)
        {
            ResetButtonColors();
            penerimaanButton1.FillColor = Color.FromArgb(64, 64, 64);
            penerimaanButton1.ForeColor = Color.White;

            SwitchPanel(new Penerimaan());
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
            edittimer.Start();
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
        private void iconButton12_Click(object sender, EventArgs e)
        {
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
            historitimer.Start();
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
            ResetButtonColors();
            btnestimasi.FillColor = Color.FromArgb(64, 64, 64);
            btnestimasi.ForeColor = Color.White;
            SwitchPanel(new EstimasiPemakaianMaterial());
        }

        private void btngudang_Click(object sender, EventArgs e)
        {
            gudangtimer.Start();
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

        //kode fitur terbatas
        public void setvisiblefalse()
        {
            entryContainer.Visible = false;
            EditContainer.Visible = false; 
            btnlaporan.Visible = false;
            historycontainer.Visible = false;
            btnestimasi.Visible = false;
            gudangContainer.Visible = false;
        }
        public void truemanajer()
        {
            entryContainer.MaximumSize = new Size(232, 215);
            entryContainer.Visible = true;
            penerimaanButton1.Visible = true;
            iconButton1.Visible = true;
            iconButton2.Visible = true;
            iconButton3.Visible = true;
            btnpemakaian.Visible = true;
            iconButton15.Visible = true;
            EditContainer.Visible = true;
            btnHistori.Visible = true;
            btnlaporan.Visible = true;
            historycontainer.Visible = true;
            btnestimasi.Visible = true;
            gudangContainer.Visible = true;
        }
        public void trueadmin()
        {
            btnlaporan.Visible=true;
            historycontainer.Visible= true;
        }
        public void trueoperatorgudang()
        {
            btnestimasi.Visible = true;
            gudangContainer.Visible = true;
            btnlaporan.Visible = true;            
        }
        //public void trueoperatorperbaikan()
        //{
        //    penerimaanButton1.Visible = false;
        //    iconButton2.Visible=false;
        //    iconButton15.Visible = false;

        //    entryContainer.Visible = true;
        //    entryContainer.MaximumSize = new Size(232, 95);    
        //    iconButton1.Visible=true;
        //    btnlaporan.Visible=true;
        //}
        public void trueoperator()
        {
            iconButton1.Visible = false;
            iconButton15.Visible = false;

            entryContainer.Visible = true;
            entryContainer.MaximumSize = new Size(232, 175);
            penerimaanButton1.Visible = true;
            iconButton1.Visible = true;
            iconButton2.Visible = true;
            btnlaporan.Visible = true;
        }


        //kode untuk panel atas
        private void shiftcontrol()
        {
            TimeSpan sekarang = DateTime.Now.TimeOfDay;

            if (sekarang >= TimeSpan.Parse("00:00:00") && sekarang <= TimeSpan.Parse("07:59:59"))
            {
                lblshift.Text = "1";
            }else if (sekarang >= TimeSpan.Parse("08:00:00") && sekarang <= TimeSpan.Parse("15:59:59"))
            {
                lblshift.Text = "2";
            } else if (sekarang >= TimeSpan.Parse("16:00:00") && sekarang <= TimeSpan.Parse("23:59:59"))
            {
                lblshift.Text = "3";
            }
        }
        private void jam_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy  [HH:mm:ss]");
            tanggal = DateTime.Now;
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
            SqlDependency.Stop(Koneksi.GetConnectionString());
        }
    }
}
