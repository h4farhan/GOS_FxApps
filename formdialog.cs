using AForge.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOS_FxApps
{
    public partial class formdialog : Form
    {
        public Image HasilFoto { get; private set; }

        public formdialog(Image img)
        {
            InitializeComponent();
            HasilFoto = img;
            pictureBoxPreview.Image = (Image)img.Clone();

            btnya.Visible = true;
            btnno.Visible = true;
        }

        public formdialog()
        {
            InitializeComponent();
        }

        private void btnya_Click(object sender, EventArgs e)
        {
            if (pictureBoxPreview.Image != null)
            {
                HasilFoto = (Image)pictureBoxPreview.Image.Clone();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnno_Click(object sender, EventArgs e)
        {
            HasilFoto = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

}
