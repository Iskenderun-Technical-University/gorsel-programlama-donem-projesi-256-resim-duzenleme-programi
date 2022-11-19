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
    public partial class AddText : Form
    {
        public AddText()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }
        public Form1 f;
        private void button1_Click(object sender, EventArgs e)
        {
            f.geriAlToolStripMenuItem.Enabled = true;
            f.fontsecimi = new Font(fontDialog1.Font.FontFamily, fontDialog1.Font.Size);
            f.renksecimi = colorDialog1.Color;
            f.islenmis = myBitmap;
            this.Close();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        Bitmap myBitmap;

        private void txtText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SolidBrush color = new SolidBrush(colorDialog1.Color);

                if (f.islenmis == null)
                {
                    myBitmap = new Bitmap(f.openFileDialog1.FileName);
                }
                else
                {
                    myBitmap = new Bitmap(f.islenmis);
                }

                f.geriallistesi.Add((Bitmap)myBitmap);

                Graphics g = Graphics.FromImage(myBitmap);
                StringFormat strFormat = new StringFormat();

                if (comboBox1.SelectedIndex == 0)
                {
                    strFormat.Alignment = StringAlignment.Far;
                    strFormat.LineAlignment = StringAlignment.Near;
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    strFormat.Alignment = StringAlignment.Center;
                    strFormat.LineAlignment = StringAlignment.Near;
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    strFormat.LineAlignment = StringAlignment.Near;
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    strFormat.Alignment = StringAlignment.Far;
                    strFormat.LineAlignment = StringAlignment.Center;
                }
                else if (comboBox1.SelectedIndex == 4)
                {
                    strFormat.Alignment = StringAlignment.Center;
                    strFormat.LineAlignment = StringAlignment.Center;
                }
                else if (comboBox1.SelectedIndex == 5)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    strFormat.LineAlignment = StringAlignment.Center;
                }
                else if (comboBox1.SelectedIndex == 6)
                {
                    strFormat.Alignment = StringAlignment.Far;
                    strFormat.LineAlignment = StringAlignment.Far;
                }
                else if (comboBox1.SelectedIndex == 7)
                {
                    strFormat.Alignment = StringAlignment.Center;
                    strFormat.LineAlignment = StringAlignment.Far;
                }
                else if (comboBox1.SelectedIndex == 8)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    strFormat.LineAlignment = StringAlignment.Far;
                }

                g.DrawString(txtText.Text, new Font(fontDialog1.Font.Name, fontDialog1.Font.Size), color,
                    new RectangleF(0, 0, myBitmap.Width, myBitmap.Height), strFormat);
                g.Dispose();
                f.pictureBox1.Image = myBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f.pictureBox1.Image = f.geriallistesi[f.geriallistesi.Count - 1];
            f.geriallistesi.Remove(f.geriallistesi[f.geriallistesi.Count - 1]);
            this.Close();
        }
    }
}
