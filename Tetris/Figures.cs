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

        public static bool IsMoving = false;

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
                coordinates.Add(new int[] { 0, 0 , 0});         // Первый две цифры - координаты, третья - код цвета
                coordinates.Add(new int[] { 1, 0 , 0});
                coordinates.Add(new int[] { 0, 1 , 0});
                coordinates.Add(new int[] { 1, 1 , 0});
            }
            if (figuretype.Equals("i"))
            {
                coordinates.Add(new int[] { 0, 0 , 1});
                coordinates.Add(new int[] { 0, 1 , 1});
                coordinates.Add(new int[] { 0, 2 , 1});
                coordinates.Add(new int[] { 0, 3 , 1});
            }
            if (figuretype.Equals("s"))
            {
                coordinates.Add(new int[] { 0, 1 , 2});
                coordinates.Add(new int[] { 1, 1 , 2});
                coordinates.Add(new int[] { 1, 0 , 2});
                coordinates.Add(new int[] { 2, 0 , 2});
            }
            if (figuretype.Equals("z"))
            {
                coordinates.Add(new int[] { 0, 0 , 3});
                coordinates.Add(new int[] { 1, 0 , 3});
                coordinates.Add(new int[] { 1, 1 , 3});
                coordinates.Add(new int[] { 2, 1 , 3});
            }
            if (figuretype.Equals("l"))
            {
                coordinates.Add(new int[] { 0, 0 , 4});
                coordinates.Add(new int[] { 0, 1 , 4});
                coordinates.Add(new int[] { 0, 2 , 4});
                coordinates.Add(new int[] { 1, 2 , 4});
            }
            if (figuretype.Equals("j"))
            {
                coordinates.Add(new int[] { 1, 0 , 5});
                coordinates.Add(new int[] { 1, 1 , 5});
                coordinates.Add(new int[] { 1, 2 , 5});
                coordinates.Add(new int[] { 0, 2 , 5});
            }
            if (figuretype.Equals("t"))
            {
                coordinates.Add(new int[] { 0, 0 , 6});
                coordinates.Add(new int[] { 1, 0 , 6});
                coordinates.Add(new int[] { 2, 0 , 6});
                coordinates.Add(new int[] { 1, 1 , 6});
            }
            if (figuretype.Equals("test"))
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                coordinates.Add(new int[] { 0, 0 });
                coordinates.Add(new int[] { 0, 1 });
                coordinates.Add(new int[] { 1, 0 });
                coordinates.Add(new int[] { 1, 1 });
                coordinates.Add(new int[] { 2, 0 });
                coordinates.Add(new int[] { 2, 1 });
                coordinates.Add(new int[] { 3, 0 });
                coordinates.Add(new int[] { 3, 1 });
                coordinates.Add(new int[] { 4, 0 });
                coordinates.Add(new int[] { 4, 1 });
                coordinates.Add(new int[] { 5, 0 });
                coordinates.Add(new int[] { 5, 1 });
                coordinates.Add(new int[] { 6, 0 });
                coordinates.Add(new int[] { 6, 1 });
                coordinates.Add(new int[] { 7, 0 });
                coordinates.Add(new int[] { 7, 1 });
            }
        }


        public static void Move(string direction)
        {

            IsMoving = true;

            direction = direction.ToLower();

            Clear();

            if (direction.Equals("right") && !IsFigureOrBorderIsNearRightSide())
            {
                position[0] += 2;
            }
            if (direction.Equals("left") && !IsFigureOrBorderIsNearLeftSide())
            {
                position[0] -= 2;
            }

            if (direction.Equals("down") && !IsNextPointLowerBorder())
            {
                position[1] += 1;
                IsMoving = false;
                Draw();
                return;
            }
         
            Draw();

            if (IsNextPointLowerBorder())
            {
                AddFigureToLowerBox();
                IsRowFull();
                new Figure("rnd");
            }




            // test
            Console.SetCursorPosition(0, 0);
            Console.Write($"Cord: {position[0]} and {position[1]}");
            //test

            IsMoving = false;
        }

        public static void Rotate() // TODO: проверка что бы при повороте фигура не оказалась в границе или другой фигуре 
        {
            IsMoving = true;

            Clear();

            foreach (int[] cordinate in coordinates)
            {
                (cordinate[0], cordinate[1]) = (cordinate[1], -cordinate[0]);
            }

            Draw();

            IsMoving = false;
        }


        public static bool IsNextPointLowerBorder()     // Если следующий пиксель - конец игрового поля, или другие фигуры снизу
        {

            foreach (int[] cordinate in coordinates)    // Перебираем все пиксели в фигуре 
            {
                if (cordinate[1] + position[1] == 37)
                {  
                    return true;
                }
                
                foreach (int[] pointbox in lowerBox)    // Перебираем все пиксели фигур снизу
                {
                    if (((cordinate[0] * 2 + position[0] == pointbox[0]) || (cordinate[0] * 2 + 1 + position[0] == pointbox[0])) && (cordinate[1] + position[1] + 1 == pointbox[1]))
                    {  
                        return true;
                    }
                }
            }
            return false;
        }
        
   

        public static bool IsFigureOrBorderIsNearLeftSide()
        {
            foreach (int[] cordinate in coordinates)    // Перебираем все пиксели в фигуре 
            {
                foreach (int[] pointbox in lowerBox)    // Перебираем все пиксели фигур снизу
                {
                    if (((cordinate[0] * 2 + position[0] - 1 == pointbox[0]) || (cordinate[0] * 2 + 1 + position[0] - 1 == pointbox[0])) && (cordinate[1] + position[1] == pointbox[1]))
                    {       // Есть ли другая фигура слева
                        return true;
                    }    
                }
                
                if ((cordinate[0] * 2 + position[0] < 22) || (cordinate[0] * 2 + position[0] < 22))
                {       // Есть ли край поля слева
                    return true;
                }
            }

            return false;
        }
        public static bool IsFigureOrBorderIsNearRightSide()
        {
            foreach (int[] cordinate in coordinates)    // Перебираем все пиксели в фигуре 
            {
                foreach (int[] pointbox in lowerBox)    // Перебираем все пиксели фигур снизу
                {       
                    if (((cordinate[0] * 2 + position[0] + 1 == pointbox[0]) || (cordinate[0] * 2 + 1 + position[0] + 1 == pointbox[0])) && (cordinate[1] + position[1] == pointbox[1]))
                    {       // Есть ли другая фигура справа
                        return true;
                    }
                }

                if ((cordinate[0] * 2 + position[0] > 73) || (cordinate[0] * 2 + position[0] > 73))
                {       // Есть ли край поля справа
                    return true;
                }

            }

            return false;
        }

        public static void AddFigureToLowerBox()        // Добавляем фигуру которая соприкоснулась с границей или другой фигурой в список фигур в буферной зоне
        {
            foreach (int[] cordinate in coordinates)
            {
                lowerBox.Add(new int[] { cordinate[0] * 2 + position[0], cordinate[1] + position[1] , cordinate[2] });
                lowerBox.Add(new int[] { cordinate[0] * 2 + 1 + position[0], cordinate[1] + position[1], cordinate[2] });
            }
        }



        public static void Clear()                  // Стираем предыдущее положение фигуры
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
         
            switch (coordinates[0][2])
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
            }
          
            foreach (int[] cordinate in coordinates)
            {
                Console.SetCursorPosition(cordinate[0] * 2 + position[0], cordinate[1] + position[1]);
                Console.Write("█");
                Console.SetCursorPosition(cordinate[0] * 2 + 1 + position[0], cordinate[1] + position[1]);
                Console.Write("█");
            }
        }

   

        public static bool IsRowFull() // TODO: удаление рядов / в данный момент не работает, на этапе разработки 
        {
                 
            for (int r = 37; r > 0; r--) // Перебираем все ряды начиная с нижнего
            {
                var solidrow = 0;

                for (var i = 21; i <= 76; i++)      // Перебираем все пиксели в ряде
                {
                    foreach (int[] pointbox in lowerBox)
                    {
                        if (pointbox[1] == r && pointbox[0] == i)
                        {
                            solidrow++;
                        }
                    }
                }

                if (solidrow >= 56) // Если ряд полностью заполнен 
                {
                    ClearRow(r);
                    return true;
                }


            }


            //test
            Console.SetCursorPosition(60, 0);
            Console.Write($"Figures in lowerbox: {lowerBox.Count}");
            //test

            return false;
        }

        public static void ClearRow(int rownumber)
        {
            ClearLowerBox();

            var i = lowerBox.Count - 1;
            while (i >= 0)
            {
                if (lowerBox[i][1] == rownumber)
                {
                    lowerBox.RemoveAt(i);
                }

                i--;
            }


            MoveAllDown(rownumber);
        }

        public static void MoveAllDown(int rownumber)
        {
            
            foreach (int[] pointbox in lowerBox)
            {
                if (pointbox[1] <= rownumber)
                {
                    pointbox[1] += 1;
                }
            }

            DrawLowerBox();
        }

        public static void ClearLowerBox()
        {
            foreach (int[] pointbox in lowerBox)
            {
                Console.SetCursorPosition(pointbox[0], pointbox[1]);
                Console.Write(" ");
            }
        }

        public static void DrawLowerBox()
        {
            foreach (int[] pointbox in lowerBox)
            {
                switch (pointbox[2])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                }

                Console.SetCursorPosition(pointbox[0], pointbox[1]);
                Console.Write("█");
            }
            IsRowFull();
        }


    }
}
