using System.Globalization;

namespace sg_3
{
    public partial class Form1 : Form
    {
        Button b1 = new Button();
        Button b2 = new Button();
        PictureBox p1 = new PictureBox();
        PictureBox p2 = new PictureBox();
        PictureBox p3 = new PictureBox();


        int lvl;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(600, 600);
            this.BackColor = Color.Black;
            this.Text = "Shooter Game";


            this.Controls.Add(b1);
            this.Controls.Add(b2);
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


            b1.Location = new Point(210, 300);
            b1.Size = new Size(150, 60);
            b1.Text = "Start play";
            b1.ForeColor = Color.White;
            b1.Click += new EventHandler(startgame);
            b1.MouseHover += hover1;
            b1.MouseLeave += leave1;

            b2.Location = new Point(210, 380);
            b2.Size = new Size(150, 60);
            b2.Text = "Exit";
            b2.ForeColor = Color.White;
            b2.Click += new EventHandler(ex);
            b2.MouseHover += hover2;
            b2.MouseLeave += leave2;

            p1.Location = new Point(130, 100);
            p1.Image = Image.FromFile("download.png");
            p1.Size = new Size(310, 130);
            p1.SizeMode = PictureBoxSizeMode.Zoom;




        }
        private void startgame(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.ShowDialog();
            //Form2 form2 = new Form2();
            //form2.ShowDialog();

        }

        private void ex(object sender, EventArgs e)
        {
            this.Close();
        }
        private void hover1(object sender, EventArgs e)
        {
            b1.BackColor = Color.DarkBlue;

        }
        private void leave1(object sender, EventArgs e)
        {
            b1.BackColor = Color.Black;
        }
        private void hover2(object sender, EventArgs e)
        {
            b2.BackColor = Color.DarkBlue;
        }
        private void leave2(object sender, EventArgs e)
        {
            b2.BackColor = Color.Black;
        }

        
    }







}