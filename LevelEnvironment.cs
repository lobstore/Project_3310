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
    internal class LevelEnvironment
    {
        /// <summary>
        /// Массив содержыщий игровое поле в виде символьных знаков
        /// </summary>
       public char[,] Map { get; set; }
        /// <summary>
        /// Массив содержащий все возможные типы объектов
        /// </summary>
        public char[] objectTypes { get; set; } = { ' ', '#', '*', 'X' };
        public LevelEnvironment(char[,] Map)
        {
            this.Map = Map;
        }
    }
}
