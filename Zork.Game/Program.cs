using System;

namespace Zork.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFileName = "Zork.json";
            string gameFileName = (args.Length > 0 ? args[(int)CommandLineArguments.GameFileName] : defaultGameFileName);

            ConsoleOutputService output = new ConsoleOutputService();
            ConsoleInputService input = new ConsoleInputService();
            Common.Game game = Common.Game.Load(gameFileName, output, input);
            output.WriteLine("Welcome to Zork!");
            
            
            while (game.IsRunning)
            {
                output.Write(">");
                input.ProcessInput();

            }
            output.WriteLine("Thank you for playing!");


        }

        private enum CommandLineArguments
        {
            GameFileName = 0
        }
    }
}
