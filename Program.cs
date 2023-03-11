#define MYTEST
namespace Project_3310
{
    /// <summary>
    /// Перечисление всех существующих типов объектов
    /// </summary>
    enum ObjectType
    {
        NONE = 0,
        Wall,
        Treasure,
        Opened
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            /* Пример карты
             * char[,] map =
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
             };*/

            char[,] map = ReadMapFromFile("map.txt");
            #region Settings
            LevelEnvironment levelEnv = new LevelEnvironment(map);
            Console.CursorVisible = true;
            Player player = new Player('@', 7, 10);
            #endregion


            // Загрузка карты в консоль
            LoadLevelIntoConsole(levelEnv);
            // Обновление состояний игры (получение управления, перемещение игрока и т.д)
            Update(levelEnv, player);

        }
        /// <summary>
        /// Метод в котором происходит обновление всех состояний
        /// </summary>
        /// <param name="levelEnv"></param>
        /// <param name="player"></param>
        private static void Update(LevelEnvironment levelEnv, Player player)
        {
            while (true)
            {
                MovePlayer(player);
                InputManagerAndCollideDetector(levelEnv, player);
                // PickUp(levelEnv, player);
                ClearPlayerTrace(levelEnv, player);
            }
        }

        /// <summary>
        /// Очищение клетки с которой игрок ушел
        /// </summary>
        /// <param name="levelEnv"></param>
        /// <param name="player"></param>
        private static void ClearPlayerTrace(LevelEnvironment levelEnv, Player player)
        {
            if (levelEnv.Map[player.PrevPosition.posX, player.PrevPosition.posY] != levelEnv.objectTypes[(int)ObjectType.Treasure] && levelEnv.Map[player.PrevPosition.posX, player.PrevPosition.posY] != levelEnv.objectTypes[(int)ObjectType.Opened])
            {
                Console.SetCursorPosition(player.PrevPosition.posY, player.PrevPosition.posX);
                Console.Write(levelEnv.objectTypes[(int)ObjectType.NONE]);

            }
            else if (levelEnv.Map[player.PrevPosition.posX, player.PrevPosition.posY] == levelEnv.objectTypes[(int)ObjectType.Treasure])
            {
                Console.SetCursorPosition(player.PrevPosition.posY, player.PrevPosition.posX);
                Console.Write(levelEnv.objectTypes[(int)ObjectType.Treasure]);
            }
            else if (levelEnv.Map[player.PrevPosition.posX, player.PrevPosition.posY] == levelEnv.objectTypes[(int)ObjectType.Opened])
            {
                Console.SetCursorPosition(player.PrevPosition.posY, player.PrevPosition.posX);
                Console.Write(levelEnv.objectTypes[(int)ObjectType.Opened]);

            }

            player.PrevPosition.posX = player.Position.posX;
            player.PrevPosition.posY = player.Position.posY;
        }


        /// <summary>
        /// Проверяет нажатые клавиши и меняет координаты игрока если не произошло столкновения с границами 
        /// </summary>
        /// <param name="levelEnv"></param>
        /// <param name="player"></param>
        private static void InputManagerAndCollideDetector(LevelEnvironment levelEnv, Player player)
        {
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();
            pressedKey = Console.ReadKey();
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (levelEnv.Map[player.Position.posX - 1, player.Position.posY] != levelEnv.objectTypes[(int)ObjectType.Wall])
                    {
                        player.Position.posX--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (levelEnv.Map[player.Position.posX + 1, player.Position.posY] != levelEnv.objectTypes[(int)ObjectType.Wall])
                    {
                        player.Position.posX++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (levelEnv.Map[player.Position.posX, player.Position.posY - 1] != levelEnv.objectTypes[(int)ObjectType.Wall])
                    {
                        player.Position.posY--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (levelEnv.Map[player.Position.posX, player.Position.posY + 1] != levelEnv.objectTypes[(int)ObjectType.Wall])
                    {
                        player.Position.posY++;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    PickUp(levelEnv, player);
                    break;
            };
        }

        /// <summary>
        /// Поднимает предмет на который наступил игрок
        /// </summary>
        /// <param name="levelEnv"></param>
        /// <param name="player"></param>
        private static void PickUp(LevelEnvironment levelEnv, Player player)
        {
            if (levelEnv.Map[player.Position.posX, player.Position.posY] == levelEnv.objectTypes[(int)ObjectType.Treasure])
            {
                levelEnv.Map[player.Position.posX, player.Position.posY] = levelEnv.objectTypes[(int)ObjectType.Opened];
                Console.SetCursorPosition(player.PrevPosition.posY, player.PrevPosition.posX);
                Console.Write(levelEnv.Map[player.Position.posX, player.Position.posY]);
                Console.SetCursorPosition(player.Position.posY, player.Position.posX);
            }
        }

        /// <summary>
        /// Перемещает "модельку" игрока по заданным координатам
        /// </summary>
        /// <param name="player"></param>
        private static void MovePlayer(Player player)
        {
            Console.SetCursorPosition(player.Position.posY, player.Position.posX);
            Console.Write(player.PlayerSkin);
        }

        /// <summary>
        /// Метод загрузки игрового поля в консоль
        /// </summary>
        /// <param name="levelEnv"></param>
        private static void LoadLevelIntoConsole(LevelEnvironment levelEnv)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < levelEnv.Map.GetLength(0); i++)
            {
                for (int j = 0; j < levelEnv.Map.GetLength(1); j++)
                {
                    Console.Write(levelEnv.Map[i, j]);
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Метод извлечения данных из текстового файла
        /// </summary>
        /// <param name="path">Путь к текстовому файлу</param>
        /// <returns></returns>
        private static char[,] ReadMapFromFile(string path)
        {
            string[] mapRaw = File.ReadAllLines(path);
            char[,] map = new char[mapRaw.Length, GetMxLengthOfLine(mapRaw)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = mapRaw[i][j];
                }
            }
            return map;
        }
        
        /// <summary>
        /// Метод для нахождение длины максимальной строки
        /// </summary>
        /// <param name="lines">Массив строк в котором нужно найти самую длинную строку</param>
        /// <returns></returns>
        private static int GetMxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
            {
                if (line.Length > maxLength)
                {
                    maxLength = line.Length;
                }
            }
            return maxLength;
        }
    }
}