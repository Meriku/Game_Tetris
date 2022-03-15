using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Figure
    {
        static List<int[]> lowerBox = new List<int[]>();

        static List<int[]> coordinates = new List<int[]>();


        static int[] position = new int[] { 61, 3 };




        public Figure(string figuretype)                // Создание новой фигуры, тип задается вручную или случайным образом
        {
            coordinates.Clear();                        // Стираем координаты старой фигуры
            position = new int[] { 61, 3 };             // Стираем координаты старой фигуры

            figuretype = figuretype.ToLower();

            if (figuretype.Equals("rnd"))
            {
                var rnd = new Random();
      
                switch (rnd.Next(0, 7))
                {
                    case 0:
                        figuretype = "o";
                        break;
                    case 1:
                        figuretype = "i";
                        break;
                    case 2:
                        figuretype = "s";
                        break;
                    case 3:
                        figuretype = "z";
                        break;
                    case 4:
                        figuretype = "l";
                        break;
                    case 5:
                        figuretype = "j";
                        break;
                    case 6:
                        figuretype = "t";
                        break;
                }
            }
            if (figuretype.Equals("o"))
            {
                coordinates.Add(new int[] { 0, 0 });
                coordinates.Add(new int[] { 1, 0 });
                coordinates.Add(new int[] { 0, 1 });
                coordinates.Add(new int[] { 1, 1 });
            }
            if (figuretype.Equals("i"))
            {
                coordinates.Add(new int[] { 0, 0 });
                coordinates.Add(new int[] { 0, 1 });
                coordinates.Add(new int[] { 0, 2 });
                coordinates.Add(new int[] { 0, 3 });
            }
            if (figuretype.Equals("s"))
            {
                coordinates.Add(new int[] { 0, 1 });
                coordinates.Add(new int[] { 1, 1 });
                coordinates.Add(new int[] { 1, 0 });
                coordinates.Add(new int[] { 2, 0 });
            }
            if (figuretype.Equals("z"))
            {
                coordinates.Add(new int[] { 0, 0 });
                coordinates.Add(new int[] { 1, 0 });
                coordinates.Add(new int[] { 1, 1 });
                coordinates.Add(new int[] { 2, 1 });
            }
            if (figuretype.Equals("l"))
            {
                coordinates.Add(new int[] { 0, 0 });
                coordinates.Add(new int[] { 0, 1 });
                coordinates.Add(new int[] { 0, 2 });
                coordinates.Add(new int[] { 1, 2 });
            }
            if (figuretype.Equals("j"))
            {
                coordinates.Add(new int[] { 1, 0 });
                coordinates.Add(new int[] { 1, 1 });
                coordinates.Add(new int[] { 1, 2 });
                coordinates.Add(new int[] { 0, 2 });
            }
            if (figuretype.Equals("t"))
            {
                coordinates.Add(new int[] { 0, 0 });
                coordinates.Add(new int[] { 1, 0 });
                coordinates.Add(new int[] { 2, 0 });
                coordinates.Add(new int[] { 1, 1 });
            }
        }

        public static bool IsNextPointLowerBorder()     // Если следующий пиксель - конец игрового поля, или другие фигуры снизу
        {

            foreach (int[] cordinate in coordinates)    // Перебираем все пиксели в фигуре 
            {
                if (cordinate[1] + position[1] == 37)
                {
                    AddFigureToLowerBox();
                    return true;
                }
                
                foreach (int[] pointbox in lowerBox)    // Перебираем все пиксели фигур снизу
                {
                    if (((cordinate[0] * 2 + position[0] == pointbox[0]) || (cordinate[0] * 2 + 1 + position[0] == pointbox[0])) && (cordinate[1] + position[1] + 1 == pointbox[1]))
                    {
                        AddFigureToLowerBox();
                        return true;
                    }
                }
            }
            return false;
        }
   
        public static void AddFigureToLowerBox()        // Добавляем фигуру которая соприкоснулась с границей или другой фигурой в список фигур в буферной зоне
        {
            foreach (int[] cordinate in coordinates)
            {
                lowerBox.Add(new int[] { cordinate[0] * 2 + position[0], cordinate[1] + position[1]});
                lowerBox.Add(new int[] { cordinate[0] * 2 + 1 + position[0], cordinate[1] + position[1] });
            }
        }


        public static void Move(string direction)
        {
            Clear();

            direction = direction.ToLower();

            if (direction.Equals("up"))
            {
               foreach (int[] cordinate in coordinates)
                {
                    //XY[1] += 1;
                }
            }
            if (direction.Equals("down"))
            {
                position[1] += 1;
            }
            if (direction.Equals("right"))
            {
                position[0] += 2;
            }
            if (direction.Equals("left"))
            {
                position[0] -= 2;
            }

            Draw();

        }

        public static void Rotate()
        {
            Clear();

            foreach (int[] cordinate in coordinates)
            {
                (cordinate[0], cordinate[1]) = (cordinate[1], -cordinate[0]);
            }

            Draw();
        }


        public static void Clear()                  // Стираем предидущее положение фигуры
        {
            foreach (int[] cordinate in coordinates)
            {
                Console.SetCursorPosition(cordinate[0] * 2 + position[0], cordinate[1] + position[1]);
                Console.Write(" ");
                Console.SetCursorPosition(cordinate[0] * 2 + 1 + position[0], cordinate[1] + position[1]);
                Console.Write(" ");
            }
        }

        public static void Draw()                   // Рисуем фигуру
        {
            foreach (int[] cordinate in coordinates)
            {
                Console.SetCursorPosition(cordinate[0] * 2 + position[0], cordinate[1] + position[1]);
                Console.Write("█");
                Console.SetCursorPosition(cordinate[0] * 2 + 1 + position[0], cordinate[1] + position[1]);
                Console.Write("█");
            }
        }

    
        public static void CleareLowerBox()         // TODO: необходимо для отладки, в дальнейшем удалить 
        {
            lowerBox.Clear();
        }


    }
}
