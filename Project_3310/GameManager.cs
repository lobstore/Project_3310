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
        Spawner
    }
    /// <summary>
    /// Класс который управляет игрой
    /// </summary>
    internal class GameManager : Behaviour
    {
        public static List<Point2D> treasuresPositions = new List<Point2D>();
        public static Player? player;
        public static List<Behaviour> behaviours = new List<Behaviour>();
        static bool isLeveLoaded = false;
        private static GameManager instance;
        private GameManager()
        {

        }

        public static GameManager getInstance()
        {
            if (instance == null)
            {
                instance = new GameManager();
                behaviours.Add(instance);
            }
            return instance;
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
                behaviours.Add(player);
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

        public override void Update()
        {

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
            behaviours.Clear();
            Console.Clear();
            Console.WriteLine("You have won");
            Thread.Sleep(1000);
            Console.WriteLine("\nPress eny key to exit main menu");
            Console.ReadKey();
            Console.Clear();
            behaviours.Add(MainMenu.instance);
        }

        public void GameEndWithEscape()
        {

            Console.Clear();
            Console.WriteLine("C");
            Console.WriteLine("Are you sure?");
            Thread.Sleep(1000);
            Console.WriteLine("\nY for yes, another button for No");
            ConsoleKeyInfo pressedKey = Console.ReadKey();
            switch (pressedKey.Key)
            {
                case ConsoleKey.Y:
                    Console.Clear();
                    behaviours.Clear();
                    behaviours.Add(MainMenu.instance);
                    break;
                default:
                    LoadLevelIntoConsole();
                    break;
            }
        }
    }
}
