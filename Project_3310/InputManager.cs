using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Project_3310
{
    internal static class InputManager
    {
        public delegate ConsoleKey ButtonEvent();
        public static event ButtonEvent Event = GetButtonDown;
        
        private static ConsoleKey GetButtonDown()
        {
           
           return Console.ReadKey().Key;

        }
    }
}
