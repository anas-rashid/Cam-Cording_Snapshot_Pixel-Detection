using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;
using AForge.Video.Kinect;
using AForge.Video.VFW;

namespace HCI_Assignment_1
{
    public partial class RecordVideo : Form
    {
        public RecordVideo()
        {
            InitializeComponent();
        }
        VideoCaptureDevice webCam = null;
        AVIWriter saveVideo = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.toggleButton.Text == "Start Rec.")
            {
                this.toggleButton.Text = "STOP Rec.";
                this.toggleButton.BackColor = Color.Black;
                toggleButton.ForeColor = Color.White;
                saveVideo = new AVIWriter("wmv3");
                saveVideo.FrameRate = 15;
                saveVideo.Open("video.avi", 640, 480);
            }
            else
            {
                this.toggleButton.Text = "Start Rec.";
                toggleButton.ForeColor = Color.Black;
                toggleButton.BackColor = Color.White;
                saveVideo.Close();
            }
        }
        private void webCam_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
            if(saveVideo!=null)
                saveVideo.AddFrame((Bitmap)eventArgs.Frame.Clone());
        }

        private void RecordVideo_Load(object sender, EventArgs e)
        {
            FilterInfoCollection allCameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            webCam = new VideoCaptureDevice(allCameras[0].MonikerString);
            webCam.NewFrame += new AForge.Video.NewFrameEventHandler(webCam_NewFrame);
            webCam.Start();
        }
        private void RecordVideo_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(saveVideo!=null)
                saveVideo.Close();

            webCam.SignalToStop();
        }
    }
}
