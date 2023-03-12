using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3310
{
    /// <summary>
    /// Класс представляющий координаты игрового поля
    /// </summary>
    internal class Point2D
    {
        public Point2D(int x, int y)
        {
            posX= x;
            posY= y;
        }

        public Point2D() { }
        /// <summary>
        /// Позиция по X от начала консоли (по вертикали)
        /// </summary>
        public int posX { get; set; }

        /// <summary>
        /// Позиция по Y от начала консоли (по горизонтали)
        /// </summary>
        public int posY { get; set; }
    }
}
