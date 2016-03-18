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
using HCI_Assignment_2;

namespace HCI_Assignment_2
{
    public partial class GetFrame : Form
    {
        private string videoFilePath = null;
        AVIReader savedVideo;

        public GetFrame()
        {
            InitializeComponent();
        }

        private void getImage_Click(object sender, EventArgs e)
        {
            string inputText = this.textBox1.Text;
            if (inputText == "")
                inputText = "0";

            savedVideo = new AVIReader();
            if (videoFilePath != null)
            {
                savedVideo.Open(videoFilePath);
                if (Int32.Parse(inputText) * savedVideo.FrameRate <= (savedVideo.Length) )
                {
                    savedVideo.Position = Int32.Parse(inputText) * (int)savedVideo.FrameRate;
                    Bitmap frame = savedVideo.GetNextFrame();

                    //Form picture = new Snapshot(frame);
                    //picture.Show(this);

                    frame.Save("snapshot.jpg");
                    label2.Text = "Image Captured!";

                }
                else
                    label2.Text = "Video is not long enough!!!";
            }
            else
                label3.Text = "Select an .avi File first!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select an .avi Video File!";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.avi|*.avi";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                videoFilePath = openFileDialog1.FileName;
                label3.Text = "File is Selected!";
            }
        }

    }
}



//float frameRate = savedVideo.FrameRate;
//int len = savedVideo.Length;

//for (float i = savedVideo.Start; i != savedVideo.Length; i = i + 1)
//{
//    Bitmap frame = savedVideo.GetNextFrame();
//    if (i == Int32.Parse(inputText) * frameRate)
//    {
//        string position = savedVideo.Position.ToString();
//        frame.Save("snapshot.jpg");
//        label2.Text = "Image Saved!";
//        return;
//    }
//}