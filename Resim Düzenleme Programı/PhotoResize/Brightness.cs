using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoResize
{
    public partial class Brightness : Form
    {
        public Brightness()
        {
            InitializeComponent();
        }

        public Form1 f1;

        Services.PhotoColorService pcs = new Services.PhotoColorService();

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap original;
            if (f1.islenmis == null)
            {
                original = new Bitmap(f1.openFileDialog1.FileName);
            }
            else
            {
                original = new Bitmap(f1.islenmis);
            }

            f1.pictureBox1.Image = pcs.AdjustBrightness(original, trackBar1.Value);
            f1.islenmis = (Bitmap)pcs.AdjustBrightness(original, trackBar1.Value).Clone();
            f1.brght = trackBar1.Value;
            f1.geriAlToolStripMenuItem.Enabled = true;
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Bitmap original;
            if (f1.islenmis == null)
            {
                original = new Bitmap(f1.openFileDialog1.FileName);
            }
            else
            {
                original = new Bitmap(f1.islenmis);
            }
            lblBrightness.Text = (trackBar1.Value).ToString();

            f1.pictureBox1.Image = pcs.AdjustBrightness(original, trackBar1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.pictureBox1.Image = f1.geriallistesi[f1.geriallistesi.Count - 1];
            f1.geriallistesi.Remove(f1.geriallistesi[f1.geriallistesi.Count - 1]);
            this.Close();
        }

        private void Brightness_Load(object sender, EventArgs e)
        {

        }
    }
}
