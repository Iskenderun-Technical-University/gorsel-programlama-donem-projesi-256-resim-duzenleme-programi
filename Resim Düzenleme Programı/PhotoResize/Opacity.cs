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
    public partial class Opacity : Form
    {
        public Opacity()
        {
            InitializeComponent();
        }

        public Form1 f1;
        
        Services.PhotoColorService pcs = new Services.PhotoColorService();

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

            float opacity = (float)((trackBar1.Value * 0.01));

            lblOpacity.Text = (opacity * 100).ToString();

            f1.pictureBox1.Image = pcs.SetOpacity(original, opacity);
        }

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

            float opacity = (float)((trackBar1.Value * 0.01));
            f1.pictureBox1.Image = pcs.SetOpacity(original, opacity);
            f1.islenmis = (Bitmap)pcs.SetOpacity(original, opacity).Clone();
            f1.opcty = opacity;
            f1.geriAlToolStripMenuItem.Enabled = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.pictureBox1.Image = f1.geriallistesi[f1.geriallistesi.Count - 1];
            f1.geriallistesi.Remove(f1.geriallistesi[f1.geriallistesi.Count - 1]);
            this.Close();
        }

        private void Opacity_Load(object sender, EventArgs e)
        {

        }
    }
}
