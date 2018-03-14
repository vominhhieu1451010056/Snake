using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication12
{
    public partial class Snake : Form
    {
        private List<Circle> snake = new List<Circle>();
        private Circle food = new Circle();
        private Circle food2 = new Circle();
        public Snake()
        {
            InitializeComponent();

            new Settings();

            gameTimer.Interval = 800 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            StartGame();



            
        }

        private void GenerateFood()
        {
            Random random = new Random();

            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            food = new Circle() {X=random.Next(0,maxXPos),Y = random.Next(0,maxYPos)};
        }
        /*private void GenerateFood2()
        {
            Random random1 = new Random();

            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            food2 = new Circle() { X = random1.Next(0, maxXPos), Y = random1.Next(0, maxYPos) };
            Coin();
        }*/

        private void StartGame()
        {
            new Settings();
            lblGameOver.Visible = false;
            snake.Clear();

            Circle head = new Circle() {X =5, Y =10};

            snake.Add(head);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();
           // GenerateFood2();
            
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if(Settings.GameOver)
            {
                if(Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if(Input.KeyPressed(Keys.Right)&&Settings.direction!=Direction.Left)
                {
                    Settings.direction = Direction.Right;
                }
                else if(Input.KeyPressed(Keys.Left)&&Settings.direction!=Direction.Right)
                {
                    Settings.direction = Direction.Left;
                }
                else if(Input.KeyPressed(Keys.Down)&&Settings.direction!=Direction.Up)
                {
                    Settings.direction = Direction.Down;
                }
                else if(Input.KeyPressed(Keys.Up)&&Settings.direction!=Direction.Down)
                {
                    Settings.direction = Direction.Up;
                }
                MovePlayer();
                //Coin();
            }
            pbCanvas.Invalidate();
        }

        private void MovePlayer()
        {
            for (int i = snake.Count -1 ; i >= 0; i--)
            {
                if(i == 0)
                {
                    switch(Settings.direction)
                    {
                        case Direction.Down:
                            snake[i].Y++;
                            break;
                        case Direction.Up:
                            snake[i].Y--;
                            break;
                        case Direction.Right:
                            snake[i].X++;
                            break;
                        case Direction.Left:
                            snake[i].X--;
                            break;
                    }
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;

                    if (snake[i].X > maxXPos || snake[i].X < 0 || snake[i].Y > maxYPos || snake[i].Y < 0)
                    {
                        die();
                    }

                    for (int j = 1; j < snake.Count; j++)
			        {
                        if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        {
                            die();
                        }
			        }
                    if (snake[0].X == food.X && snake[0].Y == food.Y)
                    {
                        eat();
                    }
                    
                    //Coin();
                }
                else
                {
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }
                
            }
        }

        private void die()
        {
            Settings.GameOver = true;
        }

        private void eat()
        {
            Circle circle = new Circle { X = snake[snake.Count - 1].X, Y = snake[snake.Count - 1].Y };

            snake.Add(circle);

            Settings.Score += Settings.Point;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
            //GenerateFood2();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if(!Settings.GameOver)
            {
                for (int i = 0; i < snake.Count; i++)
                {
                    Brush snakeColour;
                    if(i==0)
                    {
                        snakeColour = Brushes.Pink;
                    }
                    else
                    {
                        snakeColour = Brushes.Green;
                    }

                    canvas.FillEllipse(snakeColour,
                        new Rectangle(snake[i].X * Settings.Width,
                                      snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));
                    canvas.FillEllipse(Brushes.Black, new Rectangle(food.X * Settings.Width,
                        food.Y * Settings.Height, Settings.Width,Settings.Height));
                    //canvas.FillEllipse(Brushes.Blue, new Rectangle(food2.X * Settings.Width,
                        //food2.Y * Settings.Height, Settings.Width, Settings.Height));
                }
                
                
            }
            else
            {
                string gameover = "GAME OVER, Điểm của bạn là: " + Settings.Score + "\n Nhấn Enter để chơi lại!";
                lblGameOver.Text = gameover;
                lblGameOver.Visible = true;
            }
        }

        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Snake_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void Snake_Load(object sender, EventArgs e)
        {

        }

        //public void Coin()
        //{
        //    int maxXPos = pbCanvas.Size.Width / Settings.Width;
        //    int maxYPos = pbCanvas.Size.Height / Settings.Height;

        //    if(food2.X<maxXPos && food2.Y <maxYPos &&food2.Y >0&&food2.X>0)
        //    {
        //        switch (Settings.MoveCoin())
        //        {
        //            case Direction2.Down:
        //                food2.Y++;
        //                break;
        //            case Direction2.Up:
        //                food2.Y--;
        //                break;
        //            case Direction2.Right:
        //                food2.X++;
        //                break;
        //            case Direction2.Left:
        //                food2.X--;
        //                break;
        //        }
        //    }
        //    if(food2.X==maxXPos-1)
        //    {
        //        food2.X--;
        //    }else if(food2.X==0)
        //    {
        //        food2.X++;
        //    }
        //    else if(food2.X==maxYPos-1)
        //    {
        //        food2.Y--;
        //    }
        //    else if(food2.Y==0)
        //    {
        //        food2.Y++;
        //    }
                
            
        //}


        
    }
}
