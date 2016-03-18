using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCI_Assignment_2
{
    public partial class Snapshot : Form
    {

        public Snapshot(Bitmap picture)
        {
            pictureBox1 = new PictureBox();
            pictureBox1.Height = 480;
            pictureBox1.Width = 640;
            pictureBox1.BackgroundImage = (Bitmap)picture.Clone();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
                pictureBox1.Image.Save("snapshot.jpg");
        }
    }
}
