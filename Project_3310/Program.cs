namespace Project_3310
{
    internal class Program
    {

        static void Main(string[] args)
        {
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