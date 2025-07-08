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
using Microsoft.Data.SqlClient;

namespace GOS_FxApps { 
   
    
    public partial class MainForm : Form
    {
        private bool sidebarx = false;
        private bool entryprodx = false;
        private bool editx = false;
        private bool historiy = false;

        public static MainForm Instance;
        
        public bool loginstatus = false;

        public Size defaultentrycontainer;
        public Size defaulteditcontainer;
        public Size defaulhistorycontainer;

        public MainForm()
        {
            InitializeComponent();
            shiftcontrol();
            lbluser.Text = "";
            setvisiblefalse();
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
            SwitchPanel(new Dashboard());
            Instance = this;
            if (!loginstatus)
            {
                setvisiblefalse();
            }
            defaulhistorycontainer = historycontainer.Size;
            defaultentrycontainer = entryContainer.Size;
            defaulteditcontainer = EditContainer.Size;
        }


        //kode untuk panel samping
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
        private void dashboardButton_Click(object sender, EventArgs e)
        {
            SwitchPanel(new Dashboard());
        }
        private void entryButton_Click(object sender, EventArgs e)
        {
            entrytimer.Start();
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

                }
            }
            else
            {
                entryContainer.Height -= 10;
                if (entryContainer.Height <= entryContainer.MinimumSize.Height)
                {
                    entrytimer.Stop();
                    entryprodx = true;
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

                }
            }
            else
            {
                EditContainer.Height -= 10;
                if (EditContainer.Height <= EditContainer.MinimumSize.Height)
                {
                    edittimer.Stop();
                    editx = true;
                }

            }
        }
        private void Editbutton_Click(object sender, EventArgs e)
        {
            edittimer.Start();
        }
        private void penerimaanButton1_Click(object sender, EventArgs e)
        {
            SwitchPanel(new Penerimaan());
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            SwitchPanel(new Perbaikan());
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            SwitchPanel(new Pengiriman());
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            SwitchPanel(new weldingp());
        }
        private void iconButton4_Click(object sender, EventArgs e)
        {
            SwitchPanel(new pemakaianMaterial());
        }
        private void iconButton9_Click(object sender, EventArgs e)
        {
            SwitchPanel(new editpenerimaan());
        }
        private void iconButton8_Click(object sender, EventArgs e)
        {
            SwitchPanel(new editperbaikan());
        }
        private void btnlaporan_Click(object sender, EventArgs e)
        {
            SwitchPanel(new printpenerimaan());
        }
        private void iconButton7_Click(object sender, EventArgs e)
        {
            SwitchPanel(new editpengiriman());
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

                }
            }
            else
            {
                historycontainer.Height -= 10;
                if (historycontainer.Height <= historycontainer.MinimumSize.Height)
                {
                    historitimer.Stop();
                    historiy = true;
                }

            }
        }
        private void btnHistori_Click(object sender, EventArgs e)
        {
            historitimer.Start();
        }


        //kode fitur terbatas
        public void setvisiblefalse()
        {
            entryContainer.Visible = false;
            EditContainer.Visible = false;
            btnHistori.Visible = false; 
            btnlaporan.Visible = false;
            historycontainer.Visible = false;
        }
        public void setvisibletrue()
        {
            entryContainer.Visible = true;
            EditContainer.Visible = true;
            btnHistori.Visible = true;
            btnlaporan.Visible = true;
            historycontainer.Visible = true;
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
                else
                {
                    form.Close();
                }
            }
            else
            {
                Form login = new loginform();
                login.ShowDialog();
            }
        }
    }
}
