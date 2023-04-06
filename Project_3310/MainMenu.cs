namespace Project_3310
{
    /// <summary>
    /// Класс для реализации логики главного меню
    /// </summary>
    internal class MainMenu : Behaviour
    {
        string Title = "\r ______     ______     ______     __     __        ______     ______     ______     ______     ______   ______    \r\n/\\  __ \\   /\\  ___\\   /\\  ___\\   /\\ \\   /\\ \\      /\\  ___\\   /\\  ___\\   /\\  ___\\   /\\  __ \\   /\\  == \\ /\\  ___\\   \r\n\\ \\  __ \\  \\ \\___  \\  \\ \\ \\____  \\ \\ \\  \\ \\ \\     \\ \\  __\\   \\ \\___  \\  \\ \\ \\____  \\ \\  __ \\  \\ \\  _-/ \\ \\  __\\   \r\n \\ \\_\\ \\_\\  \\/\\_____\\  \\ \\_____\\  \\ \\_\\  \\ \\_\\     \\ \\_____\\  \\/\\_____\\  \\ \\_____\\  \\ \\_\\ \\_\\  \\ \\_\\    \\ \\_____\\ \r\n  \\/_/\\/_/   \\/_____/   \\/_____/   \\/_/   \\/_/      \\/_____/   \\/_____/   \\/_____/   \\/_/\\/_/   \\/_/     \\/_____/ \r\n                                                                                                                  \r\n";
        Button NewGame = new Button("New Game", ButtonOption.NewGame);
        Button Exit = new Button("Exit", ButtonOption.Exit);
        Button Continue = new Button("Continue", ButtonOption.Continue);
        List<Button> ButtonList = new List<Button>();
        int shift = 7;
        int iterator = 0;

        public MainMenu()
        {
            ButtonList.Add(NewGame);
            ButtonList.Add(Continue);
            ButtonList.Add(Exit);
            
        }
        /// <summary>
        /// Обновление состояния главного меню
        /// </summary>
        public void Update()
        {

            if (GameManager.isGameStarted)
            {
                return;
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Title);
            Console.SetCursorPosition(0, shift);
            foreach (var button in ButtonList)
            {
                Console.WriteLine(button.Text);
            }
            SetButtonPointer();
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();
            pressedKey = Console.ReadKey(true);
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
                    GameManager.isGameStarted = true;
                    GameManager.getInstance().LoadNewGame();
                    break;
                case ButtonOption.Exit:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }
        /// <summary>
        /// Устанавливает символ-указатель  напротив выбранной кнопки
        /// </summary>
        private void SetButtonPointer()
        {
            Console.SetCursorPosition(ButtonList[iterator].Text.Length + 1, iterator + shift);
            Console.Write("*");
        }

        /// <summary>
        /// Очищает символ-указатель  напротив выбранной кнопки
        /// </summary>
        private void CleanButtonPointer()
        {
            Console.SetCursorPosition(ButtonList[iterator].Text.Length + 1, iterator + shift);
            Console.Write(' ');
        }
    }
}
