using Guna.UI2.WinForms;
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
using AForge.Video;
using AForge.Video.DirectShow;

namespace GOS_FxApps
{
    public partial class formkamera : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        private bool isCameraReady = false;

        public Image HasilFoto { get; private set; }

        public formkamera()
        {
            InitializeComponent();
        }

        private void btncapture_Click(object sender, EventArgs e)
        {
            if (pictureBoxPreview.Image != null)
            {
                HasilFoto = (Image)pictureBoxPreview.Image.Clone();

                if (videoSource != null && videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                }

                btnya.Visible = true;
                btnno.Visible = true;
                btncapture.Enabled = false;
            }

        }

        private void formkamera_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                MessageBox.Show("Tidak ada kamera terdeteksi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            int indexBelakang = -1;
            for (int i = 0; i < videoDevices.Count; i++)
            {
                string nama = videoDevices[i].Name.ToLower();
                if (nama.Contains("back") || nama.Contains("rear"))
                {
                    indexBelakang = i;
                    break;
                }
            }

            if (indexBelakang == -1)
                indexBelakang = videoDevices.Count - 1;

            videoSource = new VideoCaptureDevice(videoDevices[indexBelakang].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();

            isCameraReady = true;
            btncapture.Enabled = true;
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);

            if (pictureBoxPreview.Image != null)
                pictureBoxPreview.Image.Dispose();

            pictureBoxPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPreview.Image = bmp;
        }

        private void formkamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            if (pictureBoxPreview.Image != null)
            {
                pictureBoxPreview.Image.Dispose();
                pictureBoxPreview.Image = null;
            }
        }

        private void btnno_Click(object sender, EventArgs e)
        {
            HasilFoto = null;

            if (videoSource != null && !videoSource.IsRunning)
            {
                videoSource.Start();
            }

            btnya.Visible = false;
            btnno.Visible = false;
            btncapture.Enabled = true;

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

        private void btnopenfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih Foto";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);

                            if (pictureBoxPreview.Image != null)
                                pictureBoxPreview.Image.Dispose();

                            pictureBoxPreview.Image = (Image)img.Clone();
                            HasilFoto = (Image)img.Clone();
                        }

                        if (videoSource != null && videoSource.IsRunning)
                        {
                            videoSource.SignalToStop();
                            videoSource.WaitForStop();
                        }

                        btnya.Visible = true;
                        btnno.Visible = true;
                        btncapture.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal membuka file: " + ex.Message);
                    }
                }
            }
        }
    }
}
