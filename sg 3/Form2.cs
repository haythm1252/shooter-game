using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace sg_3
{
    public partial class Form2 : Form
    {
        PictureBox player = new PictureBox();
        PictureBox alien = new PictureBox();
        PictureBox ship = new PictureBox();
        PictureBox star1 = new PictureBox();
        PictureBox star2 = new PictureBox();
        Label lbl_score = new Label();
        Label lbl_over = new Label();
        Button back_to_menu =new Button();
        
        bool right, left, space;

        int score=0;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Size = new Size(600, 600);
            this.BackColor = Color.Black;


            //adding items
            this.Controls.Add(player);
            this.Controls.Add(alien);
            this.Controls.Add(ship);
            this.Controls.Add(star1);
            this.Controls.Add(star2);
            this.Controls.Add(lbl_score);
            this.Controls.Add(lbl_over);
            this.Controls.Add(back_to_menu);


            // player picturebox
            player.Location = new Point(250, 450);
            player.Image = Image.FromFile("player_img.gif");
            player.Size = new Size(70, 70);
            player.SizeMode = PictureBoxSizeMode.Zoom;

            alien.Image = Image.FromFile("alien_gif.gif");
            alien.Size = new Size(70, 70);
            alien.SizeMode = PictureBoxSizeMode.Zoom;
            alien.Tag = "enemy";

            ship.Image = Image.FromFile("ship_gif.gif");
            ship.Size = new Size(70, 70);
            ship.SizeMode = PictureBoxSizeMode.Zoom;
            ship.Tag = "enemy";

            star1.Location = new Point(0, 0);
            star1.Image = Image.FromFile("Stars_img.gif");
            star1.SizeMode = PictureBoxSizeMode.StretchImage;
            star1.Size = new Size(600, 300);
            star1.Tag = "stars";


            star2.Location = new Point(0, 300);
            star2.Image = Image.FromFile("Stars_img.gif");
            star2.SizeMode = PictureBoxSizeMode.StretchImage;
            star2.Size = new Size(600, 300);
            star2.Tag = "stars";

            lbl_score.Location = new Point(0, 0);
            lbl_score.Size = new Size(220, 60);
            lbl_score.Font= new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
            lbl_score.Text = "score: 0";
            lbl_score.ForeColor = Color.Aqua;
            lbl_score.BringToFront();

            lbl_over.Location = new Point(100, 220);
            lbl_over.Size = new Size(400, 100);
            lbl_over.Font = new Font("Imprint MT Shadow", 40, FontStyle.Bold);
            lbl_over.Text = "Game over";
            lbl_over.ForeColor = Color.Red;
            lbl_over.Hide();

            back_to_menu.Location = new Point(210, 380);
            back_to_menu.Size = new Size(150, 60);
            back_to_menu.Text = "back to menu";
            back_to_menu.ForeColor = Color.White;
            back_to_menu.Click += new EventHandler(back);
            back_to_menu.Hide();
            
        }
        private void back (object sender, EventArgs e)
        {
            this.Close();
        }
        void Game_Result()
        {
            foreach (Control j in this.Controls)
            {
                foreach (Control i in this.Controls)
                {
                    if (j is PictureBox && j.Tag == "bullet")
                    {
                        if (i is PictureBox && i.Tag == "enemy")
                        {
                            if (j.Bounds.IntersectsWith(i.Bounds))
                            {
                                ///uncomment for enemy movement if required//
                                int x;
                                Random rnd = new Random();
                                x = rnd.Next(0, 515);
                                i.Location = new Point(x, 0);
                                i.Top = -100;
                                ((PictureBox)j).Image = Image.FromFile("explosion.gif");
                                score++;
                                lbl_score.Text = "Score : " + score;
                            }
                        }
                    }
                }
            }
            if (player.Bounds.IntersectsWith(ship.Bounds) || player.Bounds.IntersectsWith(alien.Bounds))
            
            {
                timer1.Stop();
                lbl_over.Show();
                back_to_menu.Show();
                lbl_over.BringToFront();
                back_to_menu.BringToFront();
            }
        }

        void Star()
        {
            foreach (Control j in this.Controls)
            {
                if (j is PictureBox && j.Tag == "stars")
                {
                    j.Top += 10;
                    if (j.Top > 500)
                    {
                        j.Top = 0;
                    }
                }
            }
        }

        void add_Bullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Image.FromFile("bullet_img.png");
            bullet.SizeMode = PictureBoxSizeMode.AutoSize;
            bullet.BackColor = System.Drawing.Color.Transparent;
            bullet.Tag = "bullet";
            bullet.Left = player.Left + 15;
            bullet.Top = player.Top - 30;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        void Bullet_Movement()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "bullet")
                {
                    x.Top -= 10;
                    if (x.Top < 100)
                    {
                        this.Controls.Remove(x);
                    }
                }
            }
        }

        void Enemy_Movement()
        {
            Random rnd = new Random();
            int x, y;
            if (alien.Top >= 600)
            {
                x = rnd.Next(0, 515);
                alien.Location = new Point(x, 0);
            }
            if (ship.Top >= 600)
            {
                y = rnd.Next(0, 515);
                ship.Location = new Point(y, 0);
            }
            else
            {
                alien.Top += 15;
                ship.Top += 10;
            }
        }
            void Arrow_key_Movement()
        {
            if (right == true)
            {
                if (player.Left < 515)
                {
                    player.Left += 20;
                }
            }
            if (left == true)
            {
                if (player.Left > 10)
                {
                    player.Left -= 20;
                }
            }
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                
                add_Bullet();
            }

        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            } 
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                space = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Arrow_key_Movement();
            Enemy_Movement();
            Star();
            Bullet_Movement();
            Game_Result();
        }
    }
}
