using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        Label lbl_boss_health = new Label();
        Button back_to_menu =new Button();
        PictureBox boss =new PictureBox();

        int lvl;
        int count_bullet = 0;
        int boss_move_right = 35;
        int boss_move_left = 35;
        int boss_health = 40;
        int boss_score = 15;

        bool boss_dead=false;
        bool boss_here = false;
        bool boss_got_dmg = false;
        bool right, left, space;

        int score=0;
        public Form2(int game_lvl)
        {
            InitializeComponent();
            this.lvl = game_lvl;  // easy or norml or hard taking the input from form3
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Size = new Size(600, 600);
            this.BackColor = Color.Black;
            this.Text = "Shooter Game";



            //adding items
            this.Controls.Add(player);
            this.Controls.Add(alien);
            this.Controls.Add(ship);
            this.Controls.Add(star1);
            this.Controls.Add(star2);
            this.Controls.Add(lbl_score);
            this.Controls.Add(lbl_over);
            this.Controls.Add(lbl_boss_health);
            this.Controls.Add(back_to_menu);
            

            // player picturebox
            player.Location = new Point(250, 450);
            player.Image = Image.FromFile("player_img.gif");
            player.Size = new Size(70, 70);
            player.SizeMode = PictureBoxSizeMode.Zoom;


            // enemy
            alien.Image = Image.FromFile("alien_gif.gif");
            alien.Size = new Size(70, 70);
            alien.SizeMode = PictureBoxSizeMode.Zoom;
            alien.Tag = "enemy";

            ship.Image = Image.FromFile("ship_gif.gif");
            ship.Size = new Size(70, 70);
            ship.SizeMode = PictureBoxSizeMode.Zoom;
            ship.Tag = "enemy";

            boss.Image = Image.FromFile("enemy.gif");
            boss.Location = new Point(0, 70);
            boss.Size = new Size(100, 70);
            boss.SizeMode = PictureBoxSizeMode.StretchImage;
            boss.Tag = "boss";
            boss.BringToFront();


            //background
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


            // labels
            lbl_score.Location = new Point(0, 0);
            lbl_score.Size = new Size(220, 40);
            lbl_score.Font= new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
            lbl_score.Text = "score: 0";
            lbl_score.ForeColor = Color.Aqua;
            lbl_score.BringToFront();

            lbl_boss_health.Location = new Point(230, 0);
            lbl_boss_health.Size = new Size(220, 60);
            lbl_boss_health.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
            lbl_boss_health.Text = "boss health: 25";
            lbl_boss_health.ForeColor = Color.Red;
            lbl_boss_health.BringToFront();
            lbl_boss_health.Hide();

            lbl_over.Location = new Point(100, 220);
            lbl_over.Size = new Size(400, 100);
            lbl_over.Font = new Font("Imprint MT Shadow", 40, FontStyle.Bold);
            lbl_over.Text = "Game over";
            lbl_over.ForeColor = Color.Red;
            lbl_over.Hide();

            // back to menu button
            back_to_menu.Location = new Point(210, 380);
            back_to_menu.Size = new Size(150, 60);
            back_to_menu.Text = "back to menu";
            back_to_menu.ForeColor = Color.White;
            back_to_menu.Click += new EventHandler(back);
            back_to_menu.Hide();

            // changing boss health depand on difficulty lvl
            if (lvl >= 2)
            {
                boss_health = 80;
                boss_score = 30;
                lbl_boss_health.Text = "boss health: 80";

            }
        }
        
        void boss_again()
        {
            // reseting boss health and boss health label
            if (!boss_here)
            {
                if (lvl >= 2)
                {
                    boss_health = 80;
                    lbl_boss_health.Text = "boss health: 80";
                }
                else
                {
                    boss_health = 40;
                    lbl_boss_health.Text = "boss health: 40";
                }
            }
        }
        private void back (object sender, EventArgs e)
        {
            Form1 form1 = new Form1 ();
            this.Hide ();
            form1.Show ();
        }
        void Game_Result()
        {
            foreach (Control j in this.Controls)
            {
                foreach (Control i in this.Controls)
                {
                    if (j is PictureBox && j.Tag == "bullet")
                    {
                        if (i is PictureBox && (i.Tag == "enemy" || i.Tag == "boss"))
                        {
                            if (j.Bounds.IntersectsWith(i.Bounds))
                            {
                                
                                if ((i.Tag == "boss"))  // if bullets touch the boss 
                                {
                                    ((PictureBox)j).Image = Image.FromFile("explosion.gif"); // explosion picture
                                    boss_health--;
                                    lbl_boss_health.Text = "boss health: " + boss_health;//label of the health changing
                                    if (boss_health == 0) // boss die
                                    {
                                        lbl_boss_health.Visible = false;//remove boss health label if boss die
                                        boss_dead=true;
                                        boss_here = false;
                                        this.Controls.Remove(i); //remove boss
                                        score += boss_score;
                                    }
                                }
                                else              // if bullets touch ships or aliens
                                {
                                    int x;
                                    Random rnd = new Random();    
                                    x = rnd.Next(0, 515);
                                    i.Location = new Point(x, 0);     
                                    i.Top = -100;                     // changing the place of the new enemy
                                    ((PictureBox)j).Image = Image.FromFile("explosion.gif"); // explosion picture
                                    score++;
                                   
                                } 
                                lbl_score.Text = "Score : " + score;      //show the score
                            }
                        }
                    }
                }
            }
            if (player.Bounds.IntersectsWith(ship.Bounds) || player.Bounds.IntersectsWith(alien.Bounds) )    // if player touch ship or alien 
            
            {
                timer1.Stop();                       // game stop
                lbl_over.Show();                     // show game over
                back_to_menu.Show();                 // show back to menu botton
                lbl_over.BringToFront();
                back_to_menu.BringToFront();
            }
        }

        void Star()                 
        {   
            // making the background of the game
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
        void boss_bullet_movement()  
        {
            // making boss bullet move to end of the screen
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "bossbullet")
                {
                    x.Top += 10;
                   
                }
            }
        }
        void die()
        {   
            // lose from boss bullet
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "bossbullet")
                {
                    if (player.Bounds.IntersectsWith(((PictureBox)x).Bounds))
                    {
                        timer1.Stop();
                        lbl_over.Show();
                        back_to_menu.Show();
                        lbl_over.BringToFront();
                        back_to_menu.BringToFront();
                    }

                }
            }
        }
        void add_boss()
        {
            // adding the boss every time got score 50
            if(score % 10 == 0 && score != 0)
            {
                this.Controls.Add(boss);
                lbl_boss_health.Show();
                boss.BringToFront();
                boss_here = true; // using it in add boss bullet
            }
        }
        void add_boss_bullet()
        {
            PictureBox boss_bullet = new PictureBox();
            boss_bullet.Image = Image.FromFile("red_bullet.png");
            boss_bullet.Size = new Size(20, 50);
            boss_bullet.SizeMode = PictureBoxSizeMode.Zoom;
            boss_bullet.BackColor = System.Drawing.Color.Transparent;
            boss_bullet.Tag = "bossbullet";
            boss_bullet.Left = boss.Left + 15;
            boss_bullet.Top = boss.Top + 30;
            this.Controls.Add(boss_bullet);
            boss_bullet.BringToFront();
            
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
                // speeding the enemy based on the difficulty level

                if(lvl==2)       //lvl two
                {
                    alien.Top += 20;
                    ship.Top += 15;
                }
                else if(lvl==3) //lvl three
                {
                    alien.Top += 25;
                    ship.Top += 20;
                }
                else            // lvl one
                { 
                alien.Top += 15;
                ship.Top += 10;
                }
            }
        }
        void boss_movement()
        {
            
            if (boss_move_right > 0)
            {
                boss_move_right--;
                boss.Left += 15;
                if (boss_move_right == 0)
                {
                    boss_move_left = 35;
                }
            }

            else if (boss_move_left > 0)
            {
                boss_move_left--;
                boss.Left -= 15;
                if(boss_move_left == 0)
                {
                    boss_move_right = 35;
                }
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
                count_bullet++; //counting how many bullet the player shoot
                if(count_bullet % 3 == 0 && boss_dead ==false && boss_here == true) 
                {
                    //every 3 bullet the player shoot the boss shoot 1 bullet
                    add_boss_bullet();  
                }
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
        void reset_enemy()
        {
            if (boss_here == true && lvl <= 2)
            {
                alien.Left = 1000;
                ship.Left= 1000;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Arrow_key_Movement();
            if(!boss_here)Enemy_Movement();
            boss_movement();
            Star();
            Bullet_Movement();
            add_boss();
            boss_bullet_movement();
            die();
            reset_enemy();
            boss_again();
            Game_Result();
            
        }
    }
}
