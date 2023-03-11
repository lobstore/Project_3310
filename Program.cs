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
        Treasure
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            char[,] map =
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


            #region Settings
            LevelEnvironment levelEnv = new LevelEnvironment(map);
            Console.CursorVisible = false;
            Player player = new Player('@');
            #endregion



            LoadLevelIntoConsole(levelEnv);
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
                PickUp(levelEnv, player);
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
            Console.SetCursorPosition(player.PrevPosition.posY, player.PrevPosition.posX);
            Console.Write(levelEnv.objectTypes[(int)ObjectType.NONE]);
            player.PrevPosition.posX = player.Position.posX;
            player.PrevPosition.posY = player.Position.posY;
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
                levelEnv.Map[player.Position.posX, player.Position.posY] = levelEnv.objectTypes[(int)ObjectType.NONE];
                levelEnv.Map[new Random().Next(levelEnv.Map.GetLength(0) - 1), new Random().Next(levelEnv.Map.GetLength(1) - 1)] = '*';
            }
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
            };
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
    }
}