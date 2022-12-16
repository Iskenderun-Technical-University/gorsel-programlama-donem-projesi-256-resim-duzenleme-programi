using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Net;

namespace PhotoResize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            if (pictureBox1.Image == null)
            {
                farklıKaydetToolStripMenuItem.Enabled = false;
                resimToolStripMenuItem.Enabled = false;
                düzenleToolStripMenuItem.Enabled = false;
                button2.Enabled = false;
                geriAlToolStripMenuItem.Enabled = false;
                ileriAlToolStripMenuItem.Enabled = false;
                sayfaÖnizlemeToolStripMenuItem.Enabled = false;
                sayfaYazdırToolStripMenuItem.Enabled = false;
                sayfaYapısıToolStripMenuItem.Enabled = false;
            }
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NRL2KD\SQLEXPRESS;Initial Catalog=ResimDüzenleme;Integrated Security=True;Pooling=False");
        

        public Bitmap islenmis;
        public string cH;
        public string cW;
        public float opcty;
        public int brght;
        public bool ResmiOrtala = false;
        public bool ResmiSigdir = false;
        public Font fontsecimi;
        public Color renksecimi;
        public List<Bitmap> geriallistesi = new List<Bitmap>();
        public List<Bitmap> ileriallistesi = new List<Bitmap>();

        Services.PhotoColorService pcs = new Services.PhotoColorService();
        private Bitmap RotatePhoto(RotateFlipType value)
        {
            Bitmap Bmp = new Bitmap(pictureBox1.Image);

                Bmp.RotateFlip(value);
              
                lblW.Text = Bmp.Width.ToString();
                lblH.Text = Bmp.Height.ToString();

                return Bmp;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
 
            AddText addtxt = new AddText();
            addtxt.f = this;
            if (fontsecimi == null)
            {
                addtxt.fontDialog1.Font = new Font("Cambria", 26);
                addtxt.colorDialog1.Color = Color.Black;
            }
            else
            {
                addtxt.fontDialog1.Font = new Font(fontsecimi.FontFamily, fontsecimi.Size);
                addtxt.colorDialog1.Color = renksecimi;
            }
            addtxt.ShowDialog();
        }

        
        public void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Resim Seç";
            openFileDialog1.Filter = "Jpeg Dosyaları(*.jpeg)|*.jpeg| Jpg Dosyaları(*.jpg)|*.jpg| Png Dosyaları(*.png)|*.png| Gif DOsyaları(*.gif)|*.gif| Tif DOsyaları(*.tif)|*.tif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                cH = bmp.Height.ToString();
                cW = lblW.Text = bmp.Width.ToString();
                lblW.Text = bmp.Width.ToString();
                lblH.Text = bmp.Height.ToString();
                farklıKaydetToolStripMenuItem.Enabled = true;
                düzenleToolStripMenuItem.Enabled = true;
                resimToolStripMenuItem.Enabled = true;
                sayfaÖnizlemeToolStripMenuItem.Enabled = true;
                sayfaYazdırToolStripMenuItem.Enabled = true;
                sayfaYapısıToolStripMenuItem.Enabled = true;
                button2.Enabled = true;
                lblDosya.Text = openFileDialog1.SafeFileName;
                opcty = 1;
                brght = 0;
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void farklıKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QualityForm q = new QualityForm();
            q.f = this;
            q.ShowDialog();
        }



        private void sayfaÖnizlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPhoto pp = new PrintPhoto();
            pp.f = this;
            pp.printPreviewControl1.Document = printDocument1;
            if (ResmiSigdir == true)
            {
                pp.radioButton1.Checked = true;
            }
            else if (ResmiOrtala == true)
            {
                pp.radioButton2.Checked = true;
            }
            else
            {
                pp.radioButton3.Checked = true;
            }
            pp.ShowDialog();
        }

        private void sayfaYapısıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.PageSettings = printDocument1.DefaultPageSettings;
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.DefaultPageSettings = pageSetupDialog1.PageSettings;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x, y;
            int genişlik, yükseklik;
            System.Drawing.Printing.PageSettings p;
            p = printDocument1.DefaultPageSettings;

            x = p.Margins.Left;
            y = p.Margins.Top;
            genişlik = pictureBox1.Image.Width;
            yükseklik = pictureBox1.Image.Height;

            if (ResmiSigdir == true)
            {
                genişlik = p.PaperSize.Width - p.Margins.Left - p.Margins.Right;
                yükseklik = p.PaperSize.Height - p.Margins.Top - p.Margins.Bottom;
            }
            if (ResmiOrtala == true)
            {
                x = p.Margins.Left +
                 ((p.PaperSize.Width - p.Margins.Left - p.Margins.Right) -
                 pictureBox1.Image.Width) / 2;
                y = p.Margins.Top +
                 ((p.PaperSize.Height - p.Margins.Top - p.Margins.Bottom) -
                 pictureBox1.Image.Height) / 2;
            }

            e.Graphics.DrawImage(pictureBox1.Image, x, y, genişlik, yükseklik);
        }

        private void geriAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ileriallistesi.Add((Bitmap)pictureBox1.Image);
            pictureBox1.Image = geriallistesi[geriallistesi.Count - 1];


            lblW.Text = geriallistesi[geriallistesi.Count - 1].Width.ToString();
            lblH.Text = geriallistesi[geriallistesi.Count - 1].Height.ToString();

             geriallistesi.Remove(geriallistesi[geriallistesi.Count - 1]);
            
            if (geriallistesi.Count == 0)
            {
                geriAlToolStripMenuItem.Enabled = false;
            }

            ileriAlToolStripMenuItem.Enabled = true;
        }

        private void ileriAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            pictureBox1.Image = ileriallistesi[ileriallistesi.Count - 1];

           

            lblW.Text = ileriallistesi[ileriallistesi.Count - 1].Width.ToString();
            lblH.Text = ileriallistesi[ileriallistesi.Count - 1].Height.ToString();

            ileriallistesi.Remove(ileriallistesi[ileriallistesi.Count - 1]);
            if (ileriallistesi.Count == 0)
            {
                ileriAlToolStripMenuItem.Enabled = false;
            }
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            Bitmap original = new Bitmap(openFileDialog1.FileName);

            pictureBox1.Image = pcs.MakeGrayscale(original);
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            Bitmap original = new Bitmap(openFileDialog1.FileName);

            pictureBox1.Image = pcs.MakeSepia(original);
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void invertColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            Bitmap original = new Bitmap(openFileDialog1.FileName);

            pictureBox1.Image = pcs.InvertColors(original);
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void shearingColorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            Bitmap original = new Bitmap(openFileDialog1.FileName);

            pictureBox1.Image = pcs.ShearingColors(original);
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void scalingColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            Bitmap original = new Bitmap(openFileDialog1.FileName);

            pictureBox1.Image = pcs.ScalingColors(original);
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void temizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            lblW.Text = null;
            lblH.Text = null;
            lblDosya.Text = null;
            farklıKaydetToolStripMenuItem.Enabled = false;
            düzenleToolStripMenuItem.Enabled = false;
            resimToolStripMenuItem.Enabled = false;
            sayfaÖnizlemeToolStripMenuItem.Enabled = false;
            sayfaYazdırToolStripMenuItem.Enabled = false;
            sayfaYapısıToolStripMenuItem.Enabled = false;
            button2.Enabled = false;
            geriallistesi.Clear();
            ileriallistesi.Clear();
            islenmis = null;
            cH = null;
            cW = null;
            geriAlToolStripMenuItem.Enabled = false;
            ileriAlToolStripMenuItem.Enabled = false;
        }

        private void boyutunuDegistirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resize rsz = new Resize();
            rsz.f = this;
            rsz.ShowDialog();
        }

        private void opaklikAyarlaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            Opacity opc = new Opacity();
            opc.lblOpacity.Text = (opcty * 100).ToString();
            opc.trackBar1.Value = Convert.ToInt32(opcty * 100);
            opc.f1 = this;
            opc.ShowDialog();  
        }

        private void sagaDondurToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            pictureBox1.Image = RotatePhoto(RotateFlipType.Rotate90FlipNone);
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void solaDondurToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            pictureBox1.Image = RotatePhoto(RotateFlipType.Rotate270FlipNone);
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void tersCevirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            pictureBox1.Image = RotatePhoto(RotateFlipType.Rotate180FlipNone);
            islenmis = (Bitmap)pictureBox1.Image;
            geriAlToolStripMenuItem.Enabled = true;
        }

        private void parlaklıkAyarlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geriallistesi.Add((Bitmap)pictureBox1.Image);
            Brightness br = new Brightness();
            br.lblBrightness.Text = brght.ToString();
            br.trackBar1.Value = Convert.ToInt32(brght);
            br.f1 = this;
            br.ShowDialog();  
        }

        private void sayfaYazdırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void resimKalitesiAyarlaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void veriTabanıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VeriCek frm = new VeriCek();
            frm.Show();
        }
        string imagepath;
        private void buttonVeritabanınaEkle_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.ImageLocation = saveFileDialog1.FileName;
                imagepath = pictureBox1.ImageLocation;
                FileStream filestream = new FileStream(imagepath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(filestream);
                byte[] resim = reader.ReadBytes((int)filestream.Length);
                reader.Close();
                filestream.Close();
                con.Open();
                SqlCommand komut = new SqlCommand("insert into Resimler(images) values(@images)", con);
                komut.Parameters.Add("@images", SqlDbType.Image, resim.Length).Value = resim;
                komut.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Resim Veritabanına Eklendi", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Önce Resmi Kaydedin");
            }
        }

        private void buttonBirlestirmekIcinGonder_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (pictureBox2.Image == null)
                {
                    pictureBox2.Image = pictureBox1.Image;
                }
                else if (pictureBox3.Image == null)
                {
                    pictureBox3.Image = pictureBox1.Image;
                }
                else if (pictureBox4.Image == null)
                {
                    pictureBox4.Image = pictureBox1.Image;
                }
                else if (pictureBox5.Image == null)
                {
                    pictureBox5.Image = pictureBox1.Image;
                }
                else
                {
                    MessageBox.Show("Resim Kutuları Dolu");
                }
            }
            else
            {
                MessageBox.Show("Önce Resim Seçiniz.");
            }
        }

        private void buttonBirlestir_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null || pictureBox3.Image != null || pictureBox4.Image != null || pictureBox5.Image != null)
            {
                pictureBox1.Image = null;
                pictureBox1.Image = new Bitmap(608, 458);
                using (var g = Graphics.FromImage(pictureBox1.Image))
                {
                    if (pictureBox5.Image != null)
                    {
                        g.Clear(Color.White);
                        g.DrawImage(pictureBox2.Image, 0, 0, 304, 229);
                        g.DrawImage(pictureBox3.Image, 304, 0, 304, 229);
                        g.DrawImage(pictureBox4.Image, 0, 229, 304, 229);
                        g.DrawImage(pictureBox5.Image, 304, 229, 304, 229);
                    }
                    else if (pictureBox4.Image != null)
                    {
                        g.Clear(Color.White);
                        g.DrawImage(pictureBox2.Image, 0, 0, 304, 229);
                        g.DrawImage(pictureBox3.Image, 304, 0, 304, 229);
                        g.DrawImage(pictureBox4.Image, 0, 229, 304, 229);
                    }
                    else if (pictureBox3.Image != null)
                    {
                        g.Clear(Color.White);
                        g.DrawImage(pictureBox2.Image, 0, 0, 304, 229);
                        g.DrawImage(pictureBox3.Image, 304, 0, 304, 229);
                    }
                    else
                    {
                        MessageBox.Show("Zaten Tek Resim Var.");
                    }

                }
            }
            else
            {
                MessageBox.Show("Birleştirilecek Resim Yok");
            }
        }

        private void buttonSonEklenenResmiSil_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null || pictureBox3.Image != null || pictureBox4.Image != null || pictureBox5.Image != null)
            {
                if (pictureBox5.Image != null)
                {
                    pictureBox5.Image = null;
                }
                else if (pictureBox4.Image != null)
                {
                    pictureBox4.Image = null;
                }
                else if (pictureBox3.Image != null)
                {
                    pictureBox3.Image = null;
                }
                else if (pictureBox2.Image != null)
                {
                    pictureBox2.Image = null;
                }
            }
            else
            {
                MessageBox.Show("Silinecek Resim Yok");
            }
        }

        private void buttonSunucuyaGonder_Click(object sender, EventArgs e)
        {
            try
            {

                FileInfo DosyaBilgisi = new FileInfo(saveFileDialog1.FileName);
                string FtpAdresi = textBoxServer.Text + "/" + DosyaBilgisi.Name;
                FtpWebRequest FtpIstek = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpAdresi));
                FtpIstek.Credentials = new NetworkCredential(textBoxKullaniciAdi.Text, textBoxSifre.Text);
                FtpIstek.KeepAlive = false;
                FtpIstek.Method = WebRequestMethods.Ftp.UploadFile;
                FtpIstek.UseBinary = true;
                FtpIstek.ContentLength = DosyaBilgisi.Length;
                int BufferUzunlugu = 2048;
                byte[] buff = new byte[10000000];
                int sayi;
                FileStream stream = DosyaBilgisi.OpenRead();
                Stream str = FtpIstek.GetRequestStream();
                sayi = stream.Read(buff, 0, BufferUzunlugu);
                while (sayi != 0)
                {
                    str.Write(buff, 0, sayi);
                    sayi = stream.Read(buff, 0, BufferUzunlugu);
                }
                str.Close();
                stream.Close();
            }
            catch
            {
                MessageBox.Show("Önce resmi kaydedin ve girdiğiniz bilgilerin doğru olduğunundan emin olun.");
            }
        }
    }
}
