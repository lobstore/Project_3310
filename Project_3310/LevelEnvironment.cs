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
        public static char[,]? Map;

        public static List<Point2D> treasurePositions = new List<Point2D>();

        /// <summary>
        /// Массив содержащий все возможные типы объекто
        /// </summary>
        public static char[] objectTypes = { ' ', '#', '*', 'X', '^', '~', '@', '&' };
        /*public LevelEnvironment(char[,] Map)
        {
            this.Map = Map;
        }*/

        /// <summary>
        /// Метод извлечения данных из текстового файла
        /// </summary>
        /// <param name="path">Путь к текстовому файлу</param>
        /// <returns></returns>
        public static bool ReadMapFromFileAndGetAllTreasurePositions(string path)
        {
            if (File.Exists(path))
            {
                string[] mapRaw = File.ReadAllLines(path);
                if (mapRaw.Length < 3 && mapRaw[0].Length < 3)
                {
                    Map = new char[0, 0];
                    return false;
                }


                char[,] map = new char[mapRaw.Length, GetMaxLengthOfLine(mapRaw)];
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        map[i, j] = mapRaw[i][j];
                    }
                }
                Map = map;
                treasurePositions = GetAllTreasuresPositions(Map);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод для нахождение длины максимальной строки
        /// </summary>
        /// <param name="lines">Массив строк в котором нужно найти самую длинную строку</param>
        /// <returns></returns>
        private static int GetMaxLengthOfLine(string[] lines)
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

        private static List<Point2D> GetAllTreasuresPositions(char[,] map)
        {
            List<Point2D> treasuresList = new List<Point2D>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == (int)ObjectType.Treasure)
                    {
                        int x = i;
                        int y = j;
                        treasuresList.Add(new Point2D(x, y));
                    }
                }
            }
            return treasuresList;
        }

        public static int CountTreasures()
        {
            int count = 0;
            if (Map != null)
            {
                foreach (var cell in Map)
                {
                    if (cell == objectTypes[(int)ObjectType.Treasure])
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
