using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Project_3310
{
    internal class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        static void Main()
        {

            Process p = Process.GetCurrentProcess();
            ///Максимизировать окно
            ShowWindow(p.MainWindowHandle, 3);
            Console.CursorVisible= true;
            GameManager gameManager = GameManager.getInstance();
            /// Цикл обновления состояний всех объектов наследующих поведение
            while (true)
            {
                gameManager.UpdateSender();
            }
        }
    }
}