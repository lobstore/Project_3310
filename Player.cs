using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3310
{
    /// <summary>
    /// Класс представляющий информацию об игроке
    /// </summary>
    internal class Player : Behaviour
    {

        /// <summary>
        /// Инвентарь игрока
        /// </summary>
        public Inventory inventory { get; set; } = new Inventory(5);

        /// <summary>
        /// Символьная константа отображающая игрока в консоли
        /// </summary>
        public char PlayerSkin { get; set; }

        /// <summary>
        /// Текущая позиция персонажа на игровом поле
        /// </summary>
        public Point2D Position { get; set; } = new Point2D();

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
        public Player(char PlayerSkin, int posX = 1, int posY = 1) {
            Position.posX = posX;
            Position.posY = posY;
            PrevPosition.posX = posX;
            PrevPosition.posY = posY;
            this.PlayerSkin = PlayerSkin;
        }

        /// <summary>
        /// Метод в котором происходит обновление всех состояний
        /// </summary>
        override public void Update()
        {
                MovePlayer();
                InputManagerAndCollideDetector();
                ClearTrace();
        }

        /// <summary>
        /// Перемещает "модельку" игрока по заданным координатам
        /// </summary>
        public void MovePlayer()
        {
            Console.SetCursorPosition(Position.posY, Position.posX);
            Console.Write(PlayerSkin);
        }

        /// <summary>
        /// Проверяет нажатые клавиши и меняет координаты игрока если не произошло столкновения с границами 
        /// </summary>
        public void InputManagerAndCollideDetector()
        {
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();
            pressedKey = Console.ReadKey();
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
                    ClearInputChar();
                    PickUp();
                    break;
                default:
                    ClearInputChar();
                    break;
            };
        }

        /// <summary>
        /// Очищает ввод символа в следующей от игрока клетке
        /// </summary>
        public void ClearInputChar()
        {
            Console.SetCursorPosition(Position.posY + 1, Position.posX);
            Console.Write(LevelEnvironment.Map[Position.posX, Position.posY + 1]);
        }

        /// <summary>
        /// Очищение клетки с которой игрок ушел
        /// </summary>
        public void ClearTrace()
        {
            if (LevelEnvironment.Map[PrevPosition.posX, PrevPosition.posY] != LevelEnvironment.objectTypes[(int)ObjectType.Treasure] && LevelEnvironment.Map[PrevPosition.posX, PrevPosition.posY] != LevelEnvironment.objectTypes[(int)ObjectType.Opened])
            {
                Console.SetCursorPosition(PrevPosition.posY, PrevPosition.posX);
                Console.Write(LevelEnvironment.objectTypes[(int)ObjectType.NONE]);

            }
            else if (LevelEnvironment.Map[PrevPosition.posX, PrevPosition.posY] == LevelEnvironment.objectTypes[(int)ObjectType.Treasure])
            {
                Console.SetCursorPosition(PrevPosition.posY, PrevPosition.posX);
                Console.Write(LevelEnvironment.objectTypes[(int)ObjectType.Treasure]);
            }
            else if (LevelEnvironment.Map[PrevPosition.posX, PrevPosition.posY] == LevelEnvironment.objectTypes[(int)ObjectType.Opened])
            {
                Console.SetCursorPosition(PrevPosition.posY, PrevPosition.posX);
                Console.Write(LevelEnvironment.objectTypes[(int)ObjectType.Opened]);

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
            inventory.slots.Add(item);
        }
        /// <summary>
        /// Удаляет из <paramref name="inventory"/> изъятый предмет <paramref name="item"/>
        /// </summary>
        /// <param name="item"></param>
        public void RemoveFromInventory(char item)
        {
            inventory.slots.Remove(item);
        }
        public void OpenInventory()
        {
            //TODO выделить новый тред и вывести на вторую консоль инвентарь

        }

    }
}
