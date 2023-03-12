using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3310
{
    /// <summary>
    /// Класс представляющий информацию об уровне
    /// </summary>
    internal static class LevelEnvironment
    {
        /// <summary>
        /// Массив содержыщий игровое поле в виде символьных знаков
        /// </summary>
        public static char[,] Map;

        /// <summary>
        /// Массив содержащий все возможные типы объекто
        /// </summary>
        public static char[] objectTypes = { ' ', '#', '*', 'X', '^' };
        /*public LevelEnvironment(char[,] Map)
        {
            this.Map = Map;
        }*/

        /// <summary>
        /// Метод извлечения данных из текстового файла
        /// </summary>
        /// <param name="path">Путь к текстовому файлу</param>
        /// <returns></returns>
        public static void ReadMapFromFile(string path)
        {
            if (File.Exists(path)) { 
            string[] mapRaw = File.ReadAllLines(path);
            char[,] map = new char[mapRaw.Length, GetMxLengthOfLine(mapRaw)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = mapRaw[i][j];
                }
            }
            Map = map;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Метод для нахождение длины максимальной строки
        /// </summary>
        /// <param name="lines">Массив строк в котором нужно найти самую длинную строку</param>
        /// <returns></returns>
        public static int GetMxLengthOfLine(string[] lines)
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
