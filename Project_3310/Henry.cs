using System.Diagnostics;

namespace Project_3310
{
    internal class Henry : NPC
    {
        private char Skin = '&';
        private Point2D? Position { get; set; }
        private Process? DialogProcess { get; set; }
        private List<string> sentences = new List<string>() { "Hello", "Whats up bro", "How do you do" };
        public Henry()
        {
            for (int i = 0; i < LevelEnvironment.Map.GetLength(0); i++)
            {
                for (int j = 0; j < LevelEnvironment.Map.GetLength(1); j++)
                {
                    if (LevelEnvironment.Map[i, j] == Skin)
                    {
                        Position = new Point2D(i, j);
                    }
                }

            }

        }
        int prev = 0;
        override public void Update()
        {

        }
        override public void Chat()
        {
            Console.SetCursorPosition(Position.posY, Position.posX + 1);
            int rnd = new Random().Next(sentences.Count);
            for (int i = 0; i < sentences[prev].Length; i++)
            {
                Console.Write(LevelEnvironment.Map[Position.posX + 1, Position.posY + i]);
            }
            Console.SetCursorPosition(Position.posY, Position.posX + 1);
            Console.WriteLine(sentences[rnd]);
            prev = rnd;
        }
    }
}
