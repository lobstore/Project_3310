using System.Diagnostics;

namespace Project_3310
{
    /// <summary>
    /// Класс для реализации логики главного меню
    /// </summary>
    internal class MainMenu : Behaviour
    {
        Button NewGame = new Button("New Game", ButtonOption.NewGame);
        Button Exit = new Button("Exit", ButtonOption.Exit);
        List<Button> ButtonList = new List<Button>();
        public static MainMenu instance;

        int iterator = 0;

        public MainMenu()
        {
            instance = this;
            ButtonList.Add(NewGame);
            ButtonList.Add(Exit);
        }
        /// <summary>
        /// Обновление состояния главного меню
        /// </summary>
        public override void Update()
        {
            Console.SetCursorPosition(0, 0);
            foreach (var button in ButtonList)
            {
                Console.WriteLine(button.Text);
            }
            SetButtonPointer();
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();
            pressedKey = Console.ReadKey();
            switch (pressedKey.Key)
            {
                case ConsoleKey.DownArrow:
                    if (iterator + 1 < ButtonList.Count)
                    {
                        CleanButtonPointer();
                        iterator++;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (iterator > 0)
                    {
                        CleanButtonPointer();
                        iterator--;
                    }
                    break;
                case ConsoleKey.Enter:
                    SelectedOptionActivation();
                    break;
                case ConsoleKey.Spacebar:
                    SelectedOptionActivation();
                    break;
            };

        }
        /// <summary>
        /// Метод для активации выбранной опции из главного меню
        /// </summary>
        private void SelectedOptionActivation()
        {
            switch (ButtonList[iterator].OptionId)
            {
                case ButtonOption.NewGame:
                    GameManager.behaviours.Remove(this);
                    GameManager.getInstance().LoadNewGame();
                    break;
                case ButtonOption.Exit:
                    Console.Clear();
                    Process.GetCurrentProcess().Kill();
                    break;
            }
        }
        /// <summary>
        /// Устанавливает символ-указатель  напротив выбранной кнопки
        /// </summary>
        private void SetButtonPointer()
        {
            Console.SetCursorPosition(ButtonList[iterator].Text.Length + 1, iterator);
            Console.Write("*");
        }

        /// <summary>
        /// Очищает курсор возле кнопки
        /// </summary>
        private void CleanButtonPointer()
        {
            Console.SetCursorPosition(ButtonList[iterator].Text.Length + 1, iterator);
            Console.Write(' ');
        }
    }
}
