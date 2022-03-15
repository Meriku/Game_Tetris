using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Program
    {
        static int speed = 200;

        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 40);
            Console.SetBufferSize(100, 40);
            Console.CursorVisible = false;

            PlayGround.DrawBorders();                                                   // Границы игрового поля

            Figure f1 = new Figure("rnd");                                              // Новая фигура случайного типа

            Thread Falling = new Thread(new ThreadStart(FigureFalling));                // Отдельный поток для падения фигур вниз
            Falling.Start();

            while (true)
            {
                
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key.Equals(ConsoleKey.UpArrow))                                 // Вращаем фигуру
                {
                    Figure.Rotate();
                }
                if (key.Key.Equals(ConsoleKey.DownArrow))
                {          
                    speed = 50;                   
                }
                if (key.Key.Equals(ConsoleKey.LeftArrow))
                {
                    Figure.Move("left");
                }
                if (key.Key.Equals(ConsoleKey.RightArrow))
                {
                    Figure.Move("right");
                }
                if (key.Key.Equals(ConsoleKey.R))                                      
                {
                    Figure.CleareLowerBox();
                }

            }

            Console.ReadLine();
        }


        public static void FigureFalling()
        {
            while (true)
            {
                if (speed < 200)
                {
                    speed += 50;
                }

                if (Figure.IsNextPointLowerBorder())
                {
                    new Figure("rnd");
                }
             
                Figure.Move("down");
                                        
                Thread.Sleep(speed);
            }
          
        }



    }
}
