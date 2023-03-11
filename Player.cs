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
    internal class Player
    {
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
    }
}
