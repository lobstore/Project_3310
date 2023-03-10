namespace Project_3310
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] borders =
            {
                {'X', 'X', 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','X'  },
                {'X', 'X', 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'  },

            };
            char playerSkin = '@';
            int playerX = 6, playerY = 6;
            ConsoleKeyInfo charKey = new ConsoleKeyInfo();
            Console.CursorVisible = false;
            /*Thread newTask = new Thread(() => {
                while (true)
                {
                    charKey = Console.ReadKey();
                }
            });
            newTask.Start();*/

            while (true)
            {
                BackgroundRepaint(borders);
                Console.SetCursorPosition(playerY, playerX);
                Console.Write(playerSkin);
                charKey = Console.ReadKey();
                switch (charKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (borders[playerX - 1, playerY] != 'X')
                        {
                            playerX--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (borders[playerX + 1, playerY] != 'X')
                        {
                            playerX++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (borders[playerX, playerY - 1] != 'X')
                        {
                            playerY--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (borders[playerX, playerY + 1] != 'X')
                        {
                            playerY++;
                        }
                        break;

                };
                if (borders[playerX,playerY] == '*')
                {
                    borders[playerX, playerY] = ' ';
                    borders[new Random().Next(borders.GetLength(0)-1), new Random().Next(borders.GetLength(1)-1)] = '*';
                }
                Console.Clear();
            }

        }

        private static void BackgroundRepaint(char[,] borders)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < borders.GetLength(0); i++)
            {
                for (int j = 0; j < borders.GetLength(1); j++)
                {
                    Console.Write(borders[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}