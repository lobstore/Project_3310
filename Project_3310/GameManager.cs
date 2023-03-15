using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
    internal static class GameManager
    {
        static public Player? player;
        public static List<Behaviour> behaviours = new List<Behaviour>();
        /// <summary>
        /// Загружает новую игру
        /// </summary>
        public static void LoadNewGame()
        {

            Console.CursorVisible = false;
            //Если карта была успешно загружена из файла то...
            if (LevelEnvironment.ReadMapFromFile("map.txt"))
            {
                // Загрузка карты в консоль
                LoadLevelIntoConsole();
                player = new Player();
                behaviours.Add(player);
            }
            else
            {
                Console.WriteLine("Empty map");
            }

        }
        /// <summary>
        /// Метод загрузки игрового поля в консоль
        /// </summary>
        private static void LoadLevelIntoConsole()
        {
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
    }
   }
