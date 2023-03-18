using System.Diagnostics;

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
        Opened,
        Key,
        Spawner,
        Player,
        NPC
    }
    /// <summary>
    /// Класс для управления состояниями
    /// </summary>
    internal class GameManager
    {
        private static List<Point2D> treasuresPositions = new List<Point2D>();
        private static Player? player;
        public static NPC? npc;
        private static List<Behaviour> behaviours = new List<Behaviour>();
        private static bool isLeveLoaded = false;
        private static MainMenu mainMenu = new MainMenu();
        private static GameManager instance = new GameManager();
        public static bool isGameStarted = false;

        private GameManager()
        {
            AddGameObjectIntoUpdatePool(mainMenu);
        }

        public static GameManager getInstance()
        {
            if (instance == null)
            {
                instance = new GameManager();
                
            }
            return instance;
        }

        private static void AddGameObjectIntoUpdatePool(Behaviour behaviour)
        {
            behaviours.Add(behaviour);
        }
        private static void RemoveGameObjectFromUpdatePool(Behaviour behaviour)
        {
            behaviours.Remove(behaviour);
        }
        /// <summary>
        /// Загружает новую игру
        /// </summary>
        public void LoadNewGame()
        {

            Console.CursorVisible = false;
            //Если карта была успешно загружена из файла то...
            if (LevelEnvironment.ReadMapFromFileAndGetAllTreasurePositions("map.txt"))
            {

                if (LevelEnvironment.treasurePositions.Count != 0)
                {
                    treasuresPositions = LevelEnvironment.treasurePositions;
                }
                // Загрузка карты в консоль
                LoadLevelIntoConsole();
                player = new Player();
                npc = new Henry();
                AddGameObjectIntoUpdatePool(player);
                AddGameObjectIntoUpdatePool((Henry)npc);
                isLeveLoaded = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Empty map");
                Console.ReadKey();
                Process.GetCurrentProcess().Kill();
            }

        }
        /// <summary>
        /// Метод загрузки игрового поля в консоль
        /// </summary>
        private void LoadLevelIntoConsole()
        {
            Console.WriteLine("C");
            Console.SetCursorPosition(0, 0);


                for (int i = 0; i < LevelEnvironment.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < LevelEnvironment.Map.GetLength(1); j++)
                    {
                        Console.Write(LevelEnvironment.Map[i, j]);
                    }
                    Console.WriteLine();
                }
            

        }

        public void UpdateSender()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].Update();
                
            }
            if (isLeveLoaded == true)
            {
                if (LevelEnvironment.CountTreasures() == 0)
                {
                    GameEndWithWin();
                }
            }
        }

        private void GameEndWithWin()
        {
            isGameStarted = false;
            isLeveLoaded = false;
            behaviours.Clear();
            Console.Clear();
            Console.WriteLine("You have won");
            Thread.Sleep(1000);
            Console.WriteLine("\nPress eny key to exit main menu");
            Console.ReadKey();
            Console.Clear();
            AddGameObjectIntoUpdatePool(mainMenu);
        }

        public void GameEndWithEscape()
        {

            Console.Clear();
            Console.WriteLine("Are you sure?");
            Thread.Sleep(500);
            Console.WriteLine("\nY or Enter for exit, another button for No");
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch (pressedKey.Key)
            {
                case ConsoleKey.Y:
                    ExitToMainMenu();
                    break;
                case ConsoleKey.Enter:
                    ExitToMainMenu();
                    break;
                default:
                    LoadLevelIntoConsole();
                    break;
            }
        }

        private void ExitToMainMenu()
        {
            isGameStarted = false;
            isLeveLoaded = false;
            Console.Clear();
            behaviours.Clear();
            AddGameObjectIntoUpdatePool(mainMenu);
        }
    }
}
