using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using ProjekatPRS.Properties;


namespace ProjekatPRS
{
    class Player
    {
        public bool IsUpPressed, IsDownPressed;
        public PictureBox helicopter;
        Label scoreLabel;
        Thread PlayerThread;
        Form1 form;




        public Player(Form1 form, PictureBox helicopter_, Label score_, int num)
        {
            PlayerThread = new Thread(Move);

            this.form = form;
            this.helicopter = helicopter_;
            this.scoreLabel = score_;
            form.Controls.Add(helicopter);
            PlayerThread.Start();
        }

        internal void Move()
        {
            while (true)
            {
                bool? goingUp = null;

                if (IsUpPressed)
                {
                    goingUp = true;
                }

                if (IsDownPressed)
                {
                    if (goingUp.HasValue)
                    {
                        goingUp = null;
                    }
                    else
                    {
                        goingUp = false;
                    }
                }
                DoMove(goingUp);
            };
        }

        private void DoMove(bool? goingUp)
        {
            if (goingUp.HasValue)
            {
                var playerSpeed = 7;
                if (goingUp.Value)
                {
                    playerSpeed *= -1;
                }
                try
                {
                    form.Invoke(new Action(() => helicopter.Location = new Point(helicopter.Location.X,
                         helicopter.Top + playerSpeed
                         ))


                       );
                }
                catch
                {
                    PlayerThread.Abort();
                }
                Thread.Sleep(50);
            }
        }
    }
}
