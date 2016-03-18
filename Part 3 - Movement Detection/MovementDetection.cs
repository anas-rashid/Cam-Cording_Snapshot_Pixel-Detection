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

namespace Part_3___Movemet_Detection
{
    public partial class MovementDetection : Form
    {
        string videoFilePath = null;
        AVIReader sampleVid = null;
        Bitmap sampleImg = null;
        string coordInfo = null;

        public MovementDetection()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            linkLabel1.Enabled = false;
        }
        private void recordVideo()
        {
            AVIWriter writer = new AVIWriter("wmv3");
            writer.Open("test.avi", 320, 240);
            Bitmap image = new Bitmap(320, 240);

            for (int i = 0; i < 240; i = i + 5)
            {
                if (i == 235)
                    break;
                if (i > 4)
                {
                    image.SetPixel(i - 1, i - 1, Color.Black);
                    image.SetPixel(i - 2, i - 2, Color.Black);
                    image.SetPixel(i - 3, i - 3, Color.Black);
                    image.SetPixel(i - 4, i - 4, Color.Black);
                    image.SetPixel(i - 5, i - 5, Color.Black);
                }

                image.SetPixel(i, i, Color.Red);
                image.SetPixel(i + 1, i + 1, Color.Red);
                image.SetPixel(i + 2, i + 2, Color.Red);
                image.SetPixel(i + 3, i + 3, Color.Red);
                image.SetPixel(i + 4, i + 4, Color.Red);

                writer.AddFrame(image);
            }
            writer.Close();

        }

        private void SelectVideoFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select an .avi Video File!";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.avi|*.avi";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                videoFilePath = openFileDialog1.FileName;
                label2.Text = " A file is selected!";
                linkLabel1.Enabled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (videoFilePath != null)
            {
                sampleVid = new AVIReader();
                sampleVid.Open(videoFilePath);
                linkLabel1.Enabled = false;
                for (int k = 0; k < sampleVid.Length; k++)
                {
                    sampleImg = sampleVid.GetNextFrame();
                    for (int i = 0; i < sampleImg.Width; i++)
                    {
                        for (int j = 0; j < sampleImg.Height; j++)
                        {
                            Color pixel = sampleImg.GetPixel(i, j);

                            if (pixel.R >= 130)
                            {
                                this.dataGridView1.Rows.Insert((this.dataGridView1.Rows.Count) - 1, i.ToString(), j.ToString());
                                coordInfo += "(" + i.ToString() + " , " + j.ToString() + ") \n\r ";
                                break;
                               
                            }
                        }
                    }
                }

                System.IO.File.WriteAllText("RedDotCoordinateInfo.txt", coordInfo);
            }
            else
                label2.Text = "Select a .avi file first!";
        }
    }
}
