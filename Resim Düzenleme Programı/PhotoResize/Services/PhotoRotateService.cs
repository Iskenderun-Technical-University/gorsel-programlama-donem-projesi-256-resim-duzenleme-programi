using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoResize.Services
{
    class PhotoRotateService
    {
        public Bitmap RotateLeft(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            g.TranslateTransform((float)newBitmap.Width / 2, (float)newBitmap.Height / 2);

            g.RotateTransform(-90);

            g.TranslateTransform(-(float)newBitmap.Width / 2, -(float)newBitmap.Height / 2);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(original, new Point(0, 0));
            g.Dispose();

            return newBitmap;
        }

        public Bitmap RotateRight(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            g.TranslateTransform((float)newBitmap.Width / 2, (float)newBitmap.Height / 2);

            g.RotateTransform(90);

            g.TranslateTransform(-(float)newBitmap.Width / 2, -(float)newBitmap.Height / 2);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(original, new Point(0, 0));
            g.Dispose();

            return newBitmap;
        }

        public Bitmap RotateReverse(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            g.TranslateTransform((float)newBitmap.Width / 2, (float)newBitmap.Height / 2);

            g.RotateTransform(180);

            g.TranslateTransform(-(float)newBitmap.Width / 2, -(float)newBitmap.Height / 2);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(original, new Point(0, 0));
            g.Dispose();

            return newBitmap;
        }
    }
}
