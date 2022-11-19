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
    public partial class PrintPhoto : Form
    {
        public PrintPhoto()
        {
            InitializeComponent();
        }

        public Form1 f;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            f.ResmiSigdir = true;
            f.ResmiOrtala = false;
            printPreviewControl1.Document = f.printDocument1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            f.ResmiSigdir = false;
            f.ResmiOrtala = true;
            printPreviewControl1.Document = f.printDocument1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            f.ResmiSigdir = false;
            f.ResmiOrtala = false;
            printPreviewControl1.Document = f.printDocument1;
        }

        private void PrintPhoto_Load(object sender, EventArgs e)
        {

        }
    }
}
