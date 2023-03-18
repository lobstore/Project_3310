using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Project_3310
{
    /// <summary>
    /// Класс представляющий информацию об игроке
    /// </summary>
    internal class Player : Behaviour
    {
        private bool isInventoryOpened = false;
        /// <summary>
        /// Хранение процесса вызванного открытием инвентаря
        /// </summary>
        private Process? invetoryProcess { get; set; }
        /// <summary>
        /// Инвентарь игрока
        /// </summary>
        private Inventory Inventory { get; set; } = new Inventory(5);

        /// <summary>
        /// Символьная константа отображающая игрока в консоли
        /// </summary>
        public char PlayerSkin { get; set; }

        /// <summary>
        /// Текущая позиция персонажа на игровом поле
        /// </summary>
        private Point2D Position { get; set; } = new Point2D();

        /// <summary>
        /// Предудущая позиция персонажа на игровом поле
        /// </summary>
        public Point2D PrevPosition { get; set; } = new Point2D();

        /// <summary>
        /// Конструктор принимает в качестве обязательного параметра символьную переменную <paramref name="PlayerSkin"/>
        /// показывающую графическое оторбражение игрока в консоли и координаты <param name="posX"/> и <param name="posY"/> стартовой позиции игрока.
        /// </summary>
        /// <param name="PlayerSkin">Символьная константа представляющая игрока в консоли</param>
        /// <param name="posX">Смещение от начала координат консоли (первого символа) по вертикали</param>
        /// <param name="posY">Смещение от начала координат консоли (первого символа) по горизонтали</param>

        public Player(char PlayerSkin = '@', int posX = 1, int posY = 1)
        {
            Point2D spawnPos = GetSpawnPoint(posX, posY);
            Position.posX = spawnPos.posX;
            Position.posY = spawnPos.posY;
            PrevPosition.posX = Position.posX;
            PrevPosition.posY = Position.posY;
            this.PlayerSkin = PlayerSkin;
        }

        /// <summary>
        /// Поиск в массиве индекса точки спавна игрока
        /// </summary>
        /// <param name="defaultPosX">Значение X заданное кодом</param>
        /// <param name="defaultPosY">Значение Y заданное кодом</param>
        /// <returns></returns>
        private Point2D GetSpawnPoint(int defaultPosX, int defaultPosY)
        {
            int spawnPosX = defaultPosX;
            int spawnPosY = defaultPosY;
            if (spawnPosX != 1 || spawnPosY != 1)
            {
                return new Point2D(spawnPosX, spawnPosY);
            }
            return SearchIndexOfObject(ObjectType.Spawner);
        }
        /// <summary>
        /// Поиск в массиве <see cref="LevelEnvironment.Map"/> индекс элемента переданного в <paramref name="objectType"/>
        /// </summary>
        /// <param name="objectType"> Тип игрового объекта для поиска </param>
        /// <returns></returns>
        private static Point2D SearchIndexOfObject(ObjectType objectType)
        {
            int spawnPosX = 1;
            int spawnPosY = 1;
            for (int i = 0; i < LevelEnvironment.Map.GetLength(0); i++)
            {
                for (int j = 0; j < LevelEnvironment.Map.GetLength(1); j++)
                {
                    if (LevelEnvironment.Map[i, j] == LevelEnvironment.objectTypes[(int)objectType])
                    {
                        spawnPosX = i;
                        spawnPosY = j;
                    }
                }
            }
            return new Point2D(spawnPosX, spawnPosY);

        }



        /// <summary>
        /// Метод в котором происходит обновление состояния игрока
        /// </summary>
        public void Update()
        {
            MovePlayer();
            InputManagerAndCollideDetector();

        }

        /// <summary>
        /// Перемещает "модельку" игрока по заданным координатам
        /// </summary>
        public void MovePlayer()
        {
            ClearTrace();
            Console.SetCursorPosition(Position.posY, Position.posX);
            Console.Write(PlayerSkin);
        }

        /// <summary>
        /// Проверяет нажатые клавиши и меняет координаты игрока если не произошло столкновения с границами 
        /// </summary>
        public void InputManagerAndCollideDetector()
        {
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();
            pressedKey = Console.ReadKey(true);
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (LevelEnvironment.Map[Position.posX - 1, Position.posY] != LevelEnvironment.objectTypes[(int)ObjectType.Wall])
                    {
                        Position.posX--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (LevelEnvironment.Map[Position.posX + 1, Position.posY] != LevelEnvironment.objectTypes[(int)ObjectType.Wall])
                    {
                        Position.posX++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (LevelEnvironment.Map[Position.posX, Position.posY - 1] != LevelEnvironment.objectTypes[(int)ObjectType.Wall])
                    {
                        Position.posY--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (LevelEnvironment.Map[Position.posX, Position.posY + 1] != LevelEnvironment.objectTypes[(int)ObjectType.Wall])
                    {
                        Position.posY++;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    PickUp();
                    break;
                case ConsoleKey.I:
                    if (isInventoryOpened == false)
                    {
                        OpenInventory();
                        isInventoryOpened = true;
                    }
                    else
                    {
                        CloseInventory();
                        isInventoryOpened = false;
                    }
                    break;
                case ConsoleKey.Escape:
                    GameManager.getInstance().GameEndWithEscape();
                    break;
                case ConsoleKey.E:
                    if (LevelEnvironment.Map[Position.posX, Position.posY + 1] == LevelEnvironment.objectTypes[(int)ObjectType.NPC])
                    {
                        GameManager.npc.Chat();
                    }
                    break;
                default:
                    break;
            };
        }

        /// <summary>
        /// Очищение клетки с которой игрок ушел
        /// </summary>
        public void ClearTrace()
        {

            if (LevelEnvironment.Map[PrevPosition.posX, PrevPosition.posY] == LevelEnvironment.objectTypes[(int)ObjectType.NONE])
            {
                Console.SetCursorPosition(PrevPosition.posY, PrevPosition.posX);
                Console.Write(LevelEnvironment.objectTypes[(int)ObjectType.NONE]);
            }
            else
            {
                Console.SetCursorPosition(PrevPosition.posY, PrevPosition.posX);
                Console.Write(LevelEnvironment.Map[PrevPosition.posX, PrevPosition.posY]);
            }
            //Для продолжения цикла смещения предыдущей позиции относительно текущей
            PrevPosition.posX = Position.posX;
            PrevPosition.posY = Position.posY;
        }

        /// <summary>
        /// Поднимает предмет на котором стоит игрок
        /// </summary>
        public void PickUp()
        {

            if (LevelEnvironment.Map[Position.posX, Position.posY] == LevelEnvironment.objectTypes[(int)ObjectType.Treasure])
            {
                MarkOpened();
            }
            else if (LevelEnvironment.Map[Position.posX, Position.posY] == LevelEnvironment.objectTypes[(int)ObjectType.Key])
            {
                PutInInventory(LevelEnvironment.Map[Position.posX, Position.posY]);
                MarkNone();
            }

        }
        /// <summary>
        /// Помещает символ :раскопки" в массив <paramref name="Map"></paramref>
        /// </summary>
        private void MarkOpened()
        {
            //Запись в карту отметкии о раскопке
            LevelEnvironment.Map[Position.posX, Position.posY] = LevelEnvironment.objectTypes[(int)ObjectType.Opened];
            Console.SetCursorPosition(PrevPosition.posY, PrevPosition.posX);
            //Запись на консоль значения из карты
            Console.Write(LevelEnvironment.Map[Position.posX, Position.posY]);
        }
        /// <summary>
        /// Помещает символ "пустой клетки" в массив <paramref name="Map"/>
        /// </summary>
        private void MarkNone()
        {
            //Запись в карту пустого значения
            LevelEnvironment.Map[Position.posX, Position.posY] = LevelEnvironment.objectTypes[(int)ObjectType.NONE];
            Console.SetCursorPosition(PrevPosition.posY, PrevPosition.posX);
            //Запись на консоль значения из карты
            Console.Write(LevelEnvironment.Map[Position.posX, Position.posY]);
        }
        /// <summary>
        /// Записывает в <paramref name="inventory"/> поднятый предмет <paramref name="item"/>
        /// </summary>
        /// <param name="item"></param>
        public void PutInInventory(char item)
        {
            Inventory.slots.Add(item);
            Task.Run(Send);
        }
        /// <summary>
        /// Удаляет из <paramref name="inventory"/> изъятый предмет <paramref name="item"/>
        /// </summary>
        /// <param name="item"></param>
        public void RemoveFromInventory(char item)
        {
            Inventory.slots.Remove(item);
        }
        /// <summary>
        /// Закрывает инвентарь убивая процесс Project3310_Inventory
        /// </summary>
        public void CloseInventory()
        {
            if (invetoryProcess != null)
            {
                invetoryProcess.Kill();
            }
        }
        /// <summary>
        /// Метод открытия инвентаря и установления UDP соединения для отслеживания изменений в инвентаре
        /// </summary>
        public void OpenInventory()
        {
            //TODO выделить новый тред и вывести на вторую консоль инвентарь
            string path = @"../../../../Project3310_Inventory\bin\Debug\net6.0\Project3310_Inventory.exe";
            ProcessStartInfo openInventoryProcessInfo = new ProcessStartInfo();
            openInventoryProcessInfo.CreateNoWindow = false;
            openInventoryProcessInfo.WindowStyle = ProcessWindowStyle.Normal;
            openInventoryProcessInfo.FileName = path;
            openInventoryProcessInfo.UseShellExecute = true;

            invetoryProcess = Process.Start(openInventoryProcessInfo);

            Thread.Sleep(1000);
            Send();
            Task.Run(
                 async () =>
                 {

                     do
                     {
                         Inventory.slots = await Task.Run(Receive);
                         if (invetoryProcess.HasExited)
                         {
                             isInventoryOpened = false;

                         }
                     } while (isInventoryOpened);


                 });


        }
        /// <summary>
        /// Отправка текущего состояния инвентаря
        /// </summary>
        async void Send()
        {

            using var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            char[] message = Inventory.slots.ToArray();
            byte[] data = Encoding.UTF8.GetBytes(message);
            EndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            int bytes = await udpSocket.SendToAsync(data, SocketFlags.None, remotePoint);
            //Console.WriteLine($"Отправлено {bytes} байт");
        }
        /// <summary>
        /// Получение изменений из инвентаря процесса Project3310_Inventory.exe
        /// </summary>
        /// <returns>Задача выполняющая возврат листа с инвентарем</returns>
        async Task<List<char>> Receive()
        {
            using var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            var localIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666);
            // начинаем прослушивание входящих сообщений
            udpSocket.Bind(localIP);

            //Console.WriteLine("UDP-сервер запущен...");
            while (true)
            {
                byte[] data = new byte[256]; // буфер для получаемых данных
                                             //адрес, с которого пришли данные
                EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
                // получаем данные в массив data
                var result = await udpSocket.ReceiveFromAsync(data, SocketFlags.None, remoteIp);
                var message = Encoding.UTF8.GetChars(data, 0, result.ReceivedBytes);
                Inventory.slots = new List<char>(message);
                return Inventory.slots;

            }
        }
    }
}
