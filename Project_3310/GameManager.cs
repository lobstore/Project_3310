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
        NPC,
        Dog
    }
    /// <summary>
    /// Класс для управления состояниями
    /// </summary>
    internal class GameManager
    {
        private static List<Point2D> treasuresPositions = new List<Point2D>();
        private static Player? player = null;
        public static NPC? npc = null;
        public static Dog? dog = null;
        private static List<Behaviour> behaviours = new List<Behaviour>();
        private static bool isLeveLoaded = false;
        private static MainMenu mainMenu = new MainMenu();
        private static GameManager instance = new GameManager();
        public static bool isGameStarted = false;
        public static bool isGamePaused = false;
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
        /// <summary>
        /// Добавление объекта в пул обновлений
        /// </summary>
        /// <param name="behaviour"></param>
        private static void AddGameObjectIntoUpdatePool(Behaviour behaviour)
        {
            if (behaviour != null)
                behaviours.Add(behaviour);
        }
        /// <summary>
        /// Удаление объекта из пула обновлений
        /// </summary>
        /// <param name="behaviour"></param>
        private static void RemoveGameObjectFromUpdatePool(Behaviour behaviour)
        {
            behaviours.Remove(behaviour);
        }
        /// <summary>
        /// Загружает новую игру
        /// </summary>
        public void LoadNewGame()
        {

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
                dog = new Dog();
                RemoveGameObjectFromUpdatePool(mainMenu);
                //AddGameObjectIntoUpdatePool(player);
                AddGameObjectIntoUpdatePool((Henry)npc);
                AddGameObjectIntoUpdatePool(dog);
                isLeveLoaded = true;
                Task.Run(() =>
                {
                    while (isGameStarted && isGamePaused == false && isLeveLoaded)
                    {
                        player.InputManagerAndCollideDetector();
                    }
                });
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
            Console.SetCursorPosition(0, 0);
            Console.SetBufferSize(LevelEnvironment.Map.GetLength(1) * 2, LevelEnvironment.Map.GetLength(0) * 2);
            for (int i = 0; i < LevelEnvironment.Map.GetLength(0); i++)
            {
                for (int j = 0; j < LevelEnvironment.Map.GetLength(1); j++)
                {
                    Console.Write(LevelEnvironment.Map[i, j]);
                }
                Console.WriteLine();
            }


        }
        /// <summary>
        /// Цикл обновлений
        /// </summary>
        public void UpdateSender()
        {
            if (isGamePaused == false)
            {


                for (int i = 0; i < behaviours.Count; i++)
                {
                    behaviours[i].Update();
                }
                if (isLeveLoaded == true)
                {
                    CameraMover(player.pressedKey);
                    if (LevelEnvironment.CountTreasures() == 0)
                    {
                        GameEndWithWin();
                    }
                }
            }
        }

        private void CameraMover(ConsoleKeyInfo cki)
        {

            switch (cki.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (Console.WindowLeft > 0 && Console.WindowLeft > player.Position.posY - Console.WindowWidth / 2)
                        Console.SetWindowPosition(Console.WindowLeft - 1, Console.WindowTop);
                    break;
                case ConsoleKey.UpArrow:
                    if (Console.WindowTop > 0 && Console.WindowTop > player.Position.posX - Console.WindowHeight / 2)
                        Console.SetWindowPosition(Console.WindowLeft, Console.WindowTop - 1);
                    break;
                case ConsoleKey.RightArrow:
                    if (Console.WindowLeft < player.Position.posY - Console.WindowWidth / 2 && Console.WindowLeft < LevelEnvironment.Map.GetLength(1) - Console.WindowWidth)
                        Console.SetWindowPosition(Console.WindowLeft + 1, Console.WindowTop);
                    break;
                case ConsoleKey.DownArrow:
                    if (Console.WindowTop < player.Position.posX - Console.WindowHeight / 2 && Console.WindowTop < LevelEnvironment.Map.GetLength(0) - Console.WindowHeight)
                        Console.SetWindowPosition(Console.WindowLeft, Console.WindowTop + 1);
                    break;
            }
        }
        /// <summary>
        /// Выход в главное меню при победе
        /// </summary>
        private void GameEndWithWin()
        {
            ExitToMainMenu();
            Console.WriteLine("You have won");
            Thread.Sleep(1000);
            Console.WriteLine("\nPress eny key to exit main menu");
            Console.ReadKey();
            Console.Clear();
        }
        /// <summary>
        /// Выход в клавное меню при загруженном уровне кнопкой Escape
        /// </summary>
        public void GameEndWithEscape()
        {
            isGamePaused = true;
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
                    isGamePaused = false;
                    break;
            }
        }
        /// <summary>
        /// Выход в главное меню
        /// </summary>
        private void ExitToMainMenu()
        {
            isGamePaused = false;
            isGameStarted = false;
            isLeveLoaded = false;
            Console.Clear();
            behaviours.Clear();
            AddGameObjectIntoUpdatePool(mainMenu);
        }

    }
}
