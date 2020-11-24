using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;



namespace ProjekatPRS
{
    public partial class Form1 : Form
    {
        public bool IsUpPressed, IsDownPressed;

        bool shoot = false;
        int speed = 8;
        int score = 0;
        int UFOspeed = 10;
        public static int number;
        int index;

        Thread prepreka;
        Thread prepreka1;
        Thread prepreka2;

        Random rand = new Random();


        Player player;

       

         

        public Form1()
        {
            InitializeComponent();
            player = new Player(this, player1,txtScore,1);
           
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            prepreka = new Thread(prepreke);


            prepreka1 = new Thread(UFO);
       

            prepreka2 = new Thread(kill);
           


           /* Parallel.Invoke(
                () =>
                {
                    prepreka.Start();
                },
                () =>
                {
                    prepreka1.Start();
                },
                () =>
                {
                    prepreka2.Start();
                }
            );*/

            prepreka.Start();
            Thread.Sleep(100);
            prepreka1.Start();
            Thread.Sleep(100);
            prepreka2.Start();
            Thread.Sleep(100);

        }



        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Checkem(e, false);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Checkem(e, true);
            
        }
        
        private void Checkem(KeyEventArgs e, bool ifDown)
        {
            if (e.KeyCode == Keys.Up)
            {
                player.IsUpPressed = ifDown;
            }
            else if (e.KeyCode == Keys.W)
            {
                player.IsUpPressed = ifDown;
            }
            else if (e.KeyCode == Keys.Down)
            {
                player.IsDownPressed = ifDown;
            }
            else if (e.KeyCode == Keys.S)
            {
                player.IsDownPressed = ifDown;
            }
        
            if (shoot == true)
            {
                shoot = false;
            }
            if (e.KeyCode == Keys.Space && shoot == false)
            {
                MakeBullet();
                shoot = true;
            }
        }






      
           
        private void GameOver()
        {
            //number = score;
            try
            {
                this.Invoke(new Action(() => number = score
                              ));
            }
            catch(Exception ex)
            { 
            Console.WriteLine(ex);
            }


            try
            {
                this.Invoke(new Action(() => this.Hide()
                           ));
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

           
            Restart sistem = new Restart();
            sistem.ShowDialog();

            prepreka.Abort();
            prepreka1.Abort();
            prepreka2.Abort();
            this.Close();

        }



        private void ChangeUFO()
        {
            index += 1;
            if (index > 3)
            {
                index = 1;
            }


            switch (index)
            {
                case 1:
                    ufo.Invoke(new Action(() => ufo.Image = Properties.Resources.alien1
                        ));
                    break;
                case 2:
                    ufo.Invoke(new Action(() => ufo.Image = Properties.Resources.alien2
                        ));
                    break;
                case 3:
                    ufo.Invoke(new Action(() => ufo.Image = Properties.Resources.alien3
                        ));
                    break;
            }
            
        }


        private void MakeBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.BackColor = Color.Red;
            bullet.Width = 10;
            bullet.Height = 5;

            bullet.Left = player1.Left + player1.Width;

            bullet.Top = player1.Top + player1.Height / 2;
            
            bullet.Tag = "bullet";
            
            this.Controls.Add(bullet);
        }


        private void RemoveBullet(PictureBox bullet)
        {
            this.Invoke(new Action(() => this.Controls.Remove(bullet)));
         
            this.Invoke(new Action(() => bullet.Dispose()));
        
            Thread.Sleep(100);
        }




    private void prepreke()
    {
        while (true)
        {
            try
            {
                pillar1.Invoke(new Action(() => pillar1.Location = new Point((pillar1.Left -= speed), -6
                              )));
                pillar2.Invoke(new Action(() => pillar2.Location = new Point((pillar2.Left -= speed), 246
                         )));

            }
            catch
            {
                prepreka.Abort();
            }

           


            if (pillar1.Left < -150)
            {
                pillar1.Invoke(new Action(() => pillar1.Location = new Point((800), -6
                        )));
            }

            if (pillar2.Left < -150)
            {
                pillar2.Invoke(new Action(() => pillar2.Location = new Point((1000), 246
                )));
            }
            Thread.Sleep(50);
        }

    }



    private void UFO()
    {

        while (true)
        {
            try
            {
                ufo.Invoke(new Action(() => ufo.Left -= UFOspeed
                        ));
            }
            catch
            {
                prepreka1.Abort();
            }

            if (ufo.Left + ufo.Width < 0)
            {

                ChangeUFO();
                ufo.Invoke(new Action(() => ufo.Left = 1000));
                ufo.Invoke(new Action(() => ufo.Top = rand.Next(40, 330) - ufo.Height));
            }
            

            Thread.Sleep(50);
        }
    }


    private void kill()
    {
        while (true)
        {
            try
            {
                txtScore.Invoke(new Action(() => txtScore.Text = "Score:" + score));
            }
            catch
            {
            }
            if (/*ufo.Left < -5 || */player1.Bounds.IntersectsWith(ufo.Bounds) || player1.Bounds.IntersectsWith(pillar1.Bounds) || player1.Bounds.IntersectsWith(pillar2.Bounds))
            {
                GameOver();
            }

            foreach (Control x in this.Controls)
            {
               /* if (score > 20)
                 {
                     if (x is PictureBox && (string)x.Tag == "pillar")
                     {
                         x.Invoke(new Action(() => x.Left -= speed));
                         //x.Left -= speed;
                         if (x.Left < -150)
                         {
                              x.Invoke(new Action(() => x.Left = 1000));
                            // x.Left = 1000;
                         }
                         if (player1.Bounds.IntersectsWith(x.Bounds))
                         {

                             GameOver();
                         }

                         Thread.Sleep(50);

                     }
                 }*/
                 
                if (x is PictureBox && (string)x.Tag == "bullet")
                {
                    x.Invoke(new Action(() => x.Left += 15));
                
                    if (x.Left > 900)
                    {
                        x.Invoke(new Action(() => this.Controls.Remove(x)));

                        x.Invoke(new Action(() => x.Dispose()));

                    }
                    if (x.Bounds.IntersectsWith(ufo.Bounds))
                    {
                        score += 1;
                        RemoveBullet(((PictureBox)x));

                        x.Dispose();
                        ufo.Invoke(new Action(() => ufo.Left = 1000));

                        ufo.Invoke(new Action(() => ufo.Top = rand.Next(40, 330) - ufo.Height));

                        ChangeUFO();
                    }
                }

            }

            

         


        }


    }
        
      
    }
}
