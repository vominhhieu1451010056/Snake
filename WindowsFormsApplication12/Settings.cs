using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication12
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };
    enum Direction2
    {
        Up,
        Down,
        Left,
        Right,
    };
    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Point { get; set; }

        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }
        public static Direction2 di { get; set; }

        public Settings()
        {
            Width = 16;
            Height = 16;
            Speed = 16;
            Score = 0;
            Point = 100;
            GameOver = false;
            direction = Direction.Down;
            di = Direction2.Left;

        }
        public static Direction2 MoveCoin()
        {
            Array values = Enum.GetValues(typeof(Direction2));
            
            Random random = new Random();
            Direction2 randomBar = (Direction2)values.GetValue(random.Next(values.Length));
            return randomBar;
            
            
        }

    }
}
