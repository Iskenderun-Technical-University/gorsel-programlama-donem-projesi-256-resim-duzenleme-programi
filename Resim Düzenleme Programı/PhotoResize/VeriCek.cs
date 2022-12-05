using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace PhotoResize
{
    public partial class VeriCek : Form
    {
        public VeriCek()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1NRL2KD\SQLEXPRESS;Initial Catalog=ResimDüzenleme;Integrated Security=True;Pooling=False");

        private void VeriCek_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Resimler", con);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Resimler where Id='" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                if (dr["images"] != null)
                {
                    byte[] resim = new byte[0];
                    resim = (byte[])dr["images"];
                    MemoryStream memory = new MemoryStream(resim);
                    pictureBox1.Image = Image.FromStream(memory);
                    dr.Close();
                    cmd.Dispose();
                    con.Close();
                }
            }
            con.Close();
        }

        
    }
}
