using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{

    public partial class Form1 : Form
    {

        private List<Circle> Snake = new List<Circle>();
        private Circle Food = new Circle();
        
        public Form1()
        {
            InitializeComponent();
            new Settings();
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            info.Visible = false;

            

        }

        private void StartGame()
        {
            newgame.Visible = false;
            info.Visible = false;
            new Settings();
            Snake.Clear();

            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);
            GenerateFood();
            lblScore.Text = Settings.Score.ToString();
        }
        private void GenerateFood(object sender, EventArgs e)
        {
            
            GenerateFood();
        }
        private void GenerateFood()
        {
   
            int maxXpos = Map.Size.Width / Settings.Width;
            int maxYpos = Map.Size.Height / Settings.Width;
            Random random = new Random();
            Food.X = random.Next(0, maxXpos);
            Food.Y = random.Next(0, maxYpos);
            
           
            
        }

        private void Eat()
        {
            Circle circle = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);

            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();
           
            GenerateFood();

        }


        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Settings.GameOver)
            {
                info.Visible = true;
                if (Input.KeyPressed(Keys.Enter))
                    StartGame();
            }
            else { 
            // check for direction
            if (Input.KeyPressed(Keys.Down) && Settings.direction != Directions.Up)
            {
                Settings.direction = Directions.Down;
            }
            
            else if (Input.KeyPressed(Keys.Up) && Settings.direction != Directions.Down)
                {
                    Settings.direction = Directions.Up;
                }

                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Directions.Right)
                {
                    Settings.direction = Directions.Left;
                }

                else if (Input.KeyPressed(Keys.Right) && Settings.direction != Directions.Left)
                {
                    Settings.direction = Directions.Right;
                }
                //end of check
                MovePlayer();
            }
            Map.Invalidate();

        }

        private void MovePlayer()
        {
            //move head
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Directions.Up:
                            {
                                Snake[i].Y--;
                            }
                            break;
                        case Directions.Down:
                            {
                                Snake[i].Y++;
                            }
                            break;
                        case Directions.Right:
                            {
                                Snake[i].X++;
                            }
                            break;
                        case Directions.Left:
                            {
                                Snake[i].X--;
                            }
                            break;

                    } //uroboros
                    for (int j = 1; j < Snake.Count; j++)
                    {

                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                            Die();
                    }
                    //walls
                    int maxXpos = Map.Size.Width / Settings.Width;
                    int maxYpos = Map.Size.Height / Settings.Height;
                    if (Snake[i].X >= maxXpos || Snake[i].Y >= maxYpos || Snake[i].X < 0 || Snake[i].Y < 0)
                    {
                        Die();
                    }
                    //eating food
                    if (Snake[i].X == Food.X && Snake[i].Y == Food.Y)
                    {
                        Eat();
                    }

                }
                //move body
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void Die()
        {
            Settings.GameOver = true;
            MessageBox.Show("GameOver!!!", "LOOSER");
        }
       


        private void Map_Paint(object sender, PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            Brush snakecolor;
            if (!Settings.GameOver)
            {
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        snakecolor = Brushes.DarkGreen;
                    }
                    else snakecolor = Brushes.Green;
                    graphics.FillEllipse(snakecolor, new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height, Settings.Width, Settings.Height));

                }
                graphics.FillEllipse(Brushes.Red, new Rectangle(Food.X * Settings.Width, Food.Y * Settings.Height, Settings.Width, Settings.Height));
            }
            


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

       

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Controle snake with keyboard arrows. Eat apples. Do not collide with yourself and with the walls", "Rules");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
