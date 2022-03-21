using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class PlayGround
    {

        public static int playgroundHeight = 28;
        public static int playgroundWidth = 40;

        public static int upStart = 4;
        public static int leftStart = 18;

        public static int leftBorder = leftStart + 1;
        public static int rightBorder = leftStart + playgroundWidth;
        public static int lowerBorder = upStart + playgroundHeight;

        public static void DrawBorders()        // Рисуем границы игрового поля
        {
            for (int i = 0; i <= playgroundHeight; i++)        // Вертикальные линии
            {
       
                Console.SetCursorPosition(leftStart, i + upStart);
                Console.Write("█");
                Console.SetCursorPosition(leftStart + 1, i + upStart);
                Console.Write("█");
                Console.SetCursorPosition(leftStart + playgroundWidth, i + upStart);
                Console.Write("█");
                Console.SetCursorPosition(leftStart + playgroundWidth + 1, i + upStart);
                Console.Write("█");
            }

            for (int i = 0; i <= playgroundWidth; i++)      // Горизонтальные линии
            {
                Console.SetCursorPosition(i + leftStart, upStart);
                Console.Write("█");
                Console.SetCursorPosition(i + leftStart, upStart + playgroundHeight);
                Console.Write("█");
            }
        }
    }
}
