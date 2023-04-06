using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3310
{
    internal class Dog:Behaviour
    {

        /// <summary>
        /// Предыдущий индекс списка
        /// </summary>
        int prev = 0;
        private char Skin;
        /// <summary>
        /// Текущая позиция персонажа на игровом поле
        /// </summary>
        public Point2D Position { get; set; } = new Point2D();

        /// <summary>
        /// Предудущая позиция персонажа на игровом поле
        /// </summary>
        public Point2D PrevPosition { get; set; } = new Point2D();

        private List<string> sentences = new List<string>() { "Bark!", "Happy noise", "Bark-Bark!" };
        public Dog()
        {
            for (int i = 0; i < LevelEnvironment.Map.GetLength(0); i++)
            {
                for (int j = 0; j < LevelEnvironment.Map.GetLength(1); j++)
                {
                    if (LevelEnvironment.Map[i, j] == LevelEnvironment.objectTypes[(int)ObjectType.Dog])
                    {
                        Position = new Point2D(i, j);
                        PrevPosition.posX = Position.posX;
                        PrevPosition.posY = Position.posY;
                        Skin = LevelEnvironment.Map[i, j];
                    }
                }

            }

        }

        public void Update()
        {
            Thread.Sleep(7);

            InputManagerAndCollideDetector();
        }

        public void Move()
        {

            LevelEnvironment.Map[PrevPosition.posX, PrevPosition.posY] = LevelEnvironment.objectTypes[(int)ObjectType.NONE];
            LevelEnvironment.Map[Position.posX, Position.posY] = LevelEnvironment.objectTypes[(int)ObjectType.Dog];

            ClearTrace();
            Console.SetCursorPosition(Position.posY, Position.posX);
            Console.Write(Skin);
        }
        public void InputManagerAndCollideDetector()
        {
            switch (new Random().Next(500))
            {
                case 0:
                    if (LevelEnvironment.Map[Position.posX - 1, Position.posY] == LevelEnvironment.objectTypes[(int)ObjectType.NONE])
                    {
                        Position.posX--;
                        Move();
                    }
                    break;
                case 1:
                    if (LevelEnvironment.Map[Position.posX + 1, Position.posY] == LevelEnvironment.objectTypes[(int)ObjectType.NONE])
                    {
                        Position.posX++;
                        Move();
                    }
                    break;
                case 2:
                    if (LevelEnvironment.Map[Position.posX, Position.posY - 1] == LevelEnvironment.objectTypes[(int)ObjectType.NONE])
                    {
                        Position.posY--;
                        Move();
                    }
                    break;
                case 3:
                    if (LevelEnvironment.Map[Position.posX, Position.posY + 1] == LevelEnvironment.objectTypes[(int)ObjectType.NONE])
                    {
                        Position.posY++;
                        Move();
                    }
                    break;
                default:
                    break;
            }
        }
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
        /// Метод для обмена любезностями с нпс
        /// </summary>
        public void Chat()
        {
            Console.SetCursorPosition(Position.posY, Position.posX + 1);
            int rnd = new Random().Next(sentences.Count);
            for (int i = 0; i < sentences[prev].Length; i++)
            {
                Console.Write(LevelEnvironment.Map[Position.posX + 1, Position.posY + i]);
            }
            ClearChat(rnd);
        }

        private void ClearChat(int rnd)
        {
            Console.SetCursorPosition(Position.posY, Position.posX + 1);
            Console.WriteLine(sentences[rnd]);
            prev = rnd;
        }
    }
}
