using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    public partial class BreakoutForm : Form
    {
        bool goLeft;
        bool goRight;
        bool isGameOver;

        int score;
        int ballx;
        int bally;
        int playerSpeed;

        Random random = new Random();

        PictureBox[] blockArray;

        public BreakoutForm()
        {
            InitializeComponent();
        }

        

        private void BreakoutForm_Load(object sender, EventArgs e)
        {
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;
            lblScoreNumber.Text = Convert.ToString(score);

            gameTimer.Start();

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                }
            }
        }

        private void GameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();

            lblScoreNumber.Text = Convert.ToString(0) + " " + message;
        }

        private void PlaceBlocks()
        {

        }

        private void breakoutGameTimer(object sender, EventArgs e)
        {
            lblScoreNumber.Text = Convert.ToString(score);

            if (goLeft == true && playerPaddle.Left > 0)
            {
                playerPaddle.Left -= playerSpeed;
            }

            if (goRight == true && playerPaddle.Left < 670)
            {
                playerPaddle.Left += playerSpeed;
            }

            playerBall.Left += ballx;
            playerBall.Top += bally;

            if (playerBall.Left < 0 || playerBall.Left > 770)
            {
                ballx = -ballx;
            }

            if (playerBall.Top < 0)
            {
                bally = -bally;
            }

            if (playerBall.Bounds.IntersectsWith(playerPaddle.Bounds))
            {
                bally = random.Next(5, 10) * -1;

                if (ballx < 0)
                {
                    ballx = random.Next(5, 10) * -1;
                }
                else
                {
                    ballx = random.Next(5, 10);
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if(playerBall.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;

                        bally = -bally;

                        this.Controls.Remove(x);
                    }
                }
            }

            if (score == 45)
            {
                GameOver("Well Done!");
            }

            if (playerBall.Top > 620)
            {
                GameOver("You Lose!");
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }
    }
}
