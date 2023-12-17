using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sg_3
{
    public partial class Form3 : Form
    {
        
        Button easy = new Button();
        Button normal = new Button();
        Button hard = new Button();
        PictureBox p1 = new PictureBox();
        PictureBox p2 = new PictureBox();
        PictureBox p3 = new PictureBox();
        
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Size = new Size(600, 600);
            this.BackColor = Color.Black;
            this.Text = "Shooter Game";
            this.Controls.Add(easy);
            this.Controls.Add(normal);
            this.Controls.Add(hard);
            this.Controls.Add(p1);
            this.Controls.Add(p2);
            this.Controls.Add(p3);
            
            

            p2.Location = new Point(0, 0);
            p2.Image = Image.FromFile("360_F_394951673_cd1x3b0YwOJRmdjJLHLSFuPQmIohQYOs.jpg");
            p2.SizeMode = PictureBoxSizeMode.StretchImage;
            p2.Size = new Size(600, 300);

            p3.Location = new Point(0, 300);
            p3.Image = Image.FromFile("360_F_394951673_cd1x3b0YwOJRmdjJLHLSFuPQmIohQYOs.jpg");
            p3.SizeMode = PictureBoxSizeMode.StretchImage;
            p3.Size = new Size(600, 300);

            p1.Location = new Point(130, 100);
            p1.Image = Image.FromFile("download.png");
            p1.Size = new Size(310, 130);
            p1.SizeMode = PictureBoxSizeMode.Zoom;




            easy.Location = new Point(210, 300);
            easy.Size = new Size(150, 60);
            easy.Text = "Easy";
            easy.ForeColor = Color.White;
            easy.Click += new EventHandler(easy_lvl);
            easy.MouseHover += hover3;
            easy.MouseLeave += leave3;


            normal.Location = new Point(210, 380);
            normal.Size = new Size(150, 60);
            normal.Text = "Normal";
            normal.ForeColor = Color.White;
            normal.Click += new EventHandler(normal_lvl);
            normal.MouseHover += hover4;
            normal.MouseLeave += leave4;


            hard.Location = new Point(210, 460);
            hard.Size = new Size(150, 60);
            hard.Text = "hard";
            hard.ForeColor = Color.White;
            hard.Click += new EventHandler(hard_lvl);
            hard.MouseHover += hover5;
            hard.MouseLeave += leave5;

        }
        private void easy_lvl(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(1);  // 1 ---> easy
            this.Hide();
            form2.ShowDialog();
        }
        private void normal_lvl(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(2);  // 2 ---> normal
            this.Hide();
            form2.ShowDialog();
        }
        private void hard_lvl(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(3);  // 3 ---> hard
            this.Hide();
            form2.ShowDialog();
        }
        private void hover3(object sender, EventArgs e)
        {
            easy.BackColor = Color.Green;

        }
        private void leave3(object sender, EventArgs e)
        {
            easy.BackColor = Color.Black;
        }
        private void hover4(object sender, EventArgs e)
        {
            normal.BackColor = Color.Blue;

        }
        private void leave4(object sender, EventArgs e)
        {
            normal.BackColor = Color.Black;
        }
        private void hover5(object sender, EventArgs e)
        {
            hard.BackColor = Color.Red;

        }
        private void leave5(object sender, EventArgs e)
        {
            hard.BackColor = Color.Black;
        }
    }
}
