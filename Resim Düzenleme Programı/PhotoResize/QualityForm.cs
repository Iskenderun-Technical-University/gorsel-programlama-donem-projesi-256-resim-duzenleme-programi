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
using System.Drawing.Printing;

namespace PhotoResize
{
    public partial class QualityForm : Form
    {
        public QualityForm()
        {
            InitializeComponent();


            comboBox1.SelectedIndex = 3;
            textBox1.Text = "80";
        }

        public Form1 f;
        public Int64 kalite;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                kalite = Convert.ToInt64(textBox1.Text);               
              
                f.saveFileDialog1.Filter = "Resim Dosyası (*.jpeg)|*.jpeg| Jpg Dosyaları(*.jpg)|*.jpg| Png Dosyaları(*.png)|*.png| Gif DOsyaları(*.gif)|*.gif| Tif DOsyaları(*.tif)|*.tif";
                if (f.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Bitmap originalBMP = new Bitmap(f.pictureBox1.Image);

                    int newWidth = Convert.ToInt32(f.lblW.Text);
                    int newHeight = Convert.ToInt32(f.lblH.Text);


                    EncoderParameters parametre = new EncoderParameters(1);

                    parametre.Param[0] =

                    new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, kalite);

                    ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[1];


                    Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);


                    Graphics oGraphics = Graphics.FromImage(newBMP);

                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;


                    oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                    newBMP.Save(f.saveFileDialog1.FileName, codec, parametre);

                    originalBMP.Dispose();
                    newBMP.Dispose();
                    oGraphics.Dispose();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Bir hata oluştu!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = "10";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = "30";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Text = "50";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                textBox1.Text = "80";
            }
            else
            {
                textBox1.Text = "100";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox1.Text) > 100)
                {
                    textBox1.Text = "100";
                }
                else if (Convert.ToInt32(textBox1.Text) < 0)
                {
                    textBox1.Text = "0";
                }
            }
            catch
            {
                MessageBox.Show("Bir hata oluştu.", "Hata!");
            }
        }

        private void QualityForm_Load(object sender, EventArgs e)
        {

        }
    }
}
