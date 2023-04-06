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
        public static char[] objectTypes = { ' ', '#', '*', 'X', '^', '~', 'i', '&', '@' };
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
                treasurePositions = GetAllTreasuresPositions();
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
        /// <summary>
        /// </summary>
        /// <param name="map"></param>
        /// <returns>Список координат всех сокровищ</returns>
        private static List<Point2D> GetAllTreasuresPositions()
        {
            //List<Point2D> treasuresList = new List<Point2D>();
            //for (int i = 0; i < map.GetLength(0); i++)
            //{
            //    for (int j = 0; j < map.GetLength(1); j++)
            //    {
            //        if (map[i, j] == (int)ObjectType.Treasure)
            //        {
            //            int x = i;
            //            int y = j;
            //            treasuresList.Add(new Point2D(x, y));
            //        }
            //    }
            //}
            return SearchIndexesOfObjects(ObjectType.Treasure);
        }
        /// <summary>
        /// Поиск в массиве <see cref="LevelEnvironment.Map"/> индекс элемента переданного в <paramref name="objectType"/>
        /// </summary>
        /// <param name="objectType"> Тип игрового объекта для поиска </param>
        /// <returns>Позиция игрового объекта в массиве</returns>
        public static Point2D SearchIndexOfObject(ObjectType objectType)
        {
            int posX = 1;
            int posY = 1;
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == objectTypes[(int)objectType])
                    {
                        posX = i;
                        posY = j;
                    }
                }
            }
            return new Point2D(posX, posY);
        }

        public static List<Point2D> SearchIndexesOfObjects(ObjectType objectType)
        {
            List<Point2D> list = new List<Point2D>();
            foreach (var item in Map)
            {
                list.Add (SearchIndexOfObject(objectType));
            }
            return list;
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
