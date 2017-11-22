using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
     public  enum Directions { Up, Down, Left, Right };
    class Settings
    {
        static  public int Height { get; set; }
        static public int Width { get; set; }
        static public int Score {get;set; } 
        static  public int Points { get; set; }
        static public int Speed { get; set; }
        static public bool GameOver { get; set; }
       static public  Directions direction { get; set; }

        public Settings()
        {
            Height = 15;
            Width = 15;
            Score = 0;
            Points = 10;
            Speed = 9;
            GameOver = false;
            direction = Directions.Down;
            
        }


    }
}
