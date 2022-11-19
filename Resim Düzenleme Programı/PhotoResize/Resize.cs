using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoResize
{
    public partial class Resize : Form
    {
        public Resize()
        {
            InitializeComponent();
        }

        public Form1 f;

        public bool IsNumeric(string strDeger)
        {
            try
            {
                Int32.Parse(strDeger);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtHeight.Text == "" || txtWidth.Text == "")
            {
                MessageBox.Show("Bu alan boş geçilemez.", "Hata!");
            }
            else if (IsNumeric(txtWidth.Text) == false && IsNumeric(txtHeight.Text) == false)
            {
                MessageBox.Show("Lütfen sayısal bir değer giriniz.", "Hata!");
            }
            else
            {
                Bitmap image = new Bitmap(f.pictureBox1.Image);


                f.cH = f.lblH.Text;
                f.cW = f.lblW.Text;
                f.geriallistesi.Add((Bitmap)f.pictureBox1.Image);

                f.lblH.Text = txtHeight.Text;
                f.lblW.Text = txtWidth.Text;

                Bitmap originalBMP = new Bitmap(f.pictureBox1.Image);

                int newWidth = Convert.ToInt32(f.lblW.Text);
                int newHeight = Convert.ToInt32(f.lblH.Text);

                Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);


                Graphics oGraphics = Graphics.FromImage(newBMP);

                oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;


                oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                oGraphics.Dispose();

                f.pictureBox1.Image = newBMP;
                f.geriAlToolStripMenuItem.Enabled = true;
                this.Close();
            }
        }


         

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            double PhotoWidth = Convert.ToInt32(f.lblW.Text);
            double PhotoHeight = Convert.ToInt32(f.lblH.Text);
            
            if (checkBox1.Checked)
            {
                if (txtWidth.Text == "")
                {
                    txtWidth.Text = "0";
                }

                if (PhotoWidth > PhotoHeight)
                {
                    txtHeight.Text = Math.Round(Convert.ToDouble(txtWidth.Text) / (PhotoWidth / PhotoHeight)).ToString();
                }
                else
                {
                    txtHeight.Text = Math.Round(Convert.ToDouble(txtWidth.Text) * (PhotoWidth / PhotoHeight)).ToString();
                }
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            double PhotoWidth = Convert.ToDouble(f.lblW.Text);
            double PhotoHeight = Convert.ToDouble(f.lblH.Text);

            if (checkBox1.Checked)
            {
                if (txtHeight.Text == "")
                {
                    txtHeight.Text = "0";
                }
                if (PhotoWidth > PhotoHeight)
                {
                    txtWidth.Text = Math.Round(Convert.ToDouble(txtHeight.Text) * (PhotoWidth / PhotoHeight)).ToString();
                }
                else
                {
                    txtWidth.Text = Math.Round(Convert.ToDouble(txtHeight.Text) / (PhotoWidth / PhotoHeight)).ToString();
                }
            }
        }
    }
}
