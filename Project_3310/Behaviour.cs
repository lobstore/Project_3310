using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3310
{
    /// <summary>
    /// Базовый интерфейс для всех объектов, имеющих игровую логику
    /// </summary>
    internal interface Behaviour
    {
        /// <summary>
        /// Цикл обновлений каждого объекта
        /// </summary>
        public void Update()
        {
            return;
        }
    }
}
