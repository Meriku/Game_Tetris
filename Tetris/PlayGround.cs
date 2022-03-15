using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class PlayGround
    {

        public static void DrawBorders()        // Рисуем границы игрового поля
        {
            for (int i = 3; i < 39; i++)        // Вертикальные линии
            {
                Console.SetCursorPosition(99, i);
                Console.Write("█");
                Console.SetCursorPosition(98, i);
                Console.Write("█");
                Console.SetCursorPosition(19, i);
                Console.Write("█");
                Console.SetCursorPosition(20, i);
                Console.Write("█");
            }

            for (int i = 19; i < 100; i++)      // Горизонтальные линии
            {
                Console.SetCursorPosition(i, 2);
                Console.Write("█");
                Console.SetCursorPosition(i, 38);
                Console.Write("█");
            }
        }




    }
}
