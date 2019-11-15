using System;
using System.Collections.Generic;
using System.Text;
using Zork.Common;

namespace Zork.Game
{
    internal class ConsoleInputService : IInputService
    {
        public event EventHandler<string> InputRecieved;

        public void ProcessInput()
        {
            string inputString = Console.ReadLine().Trim();
            InputRecieved?.Invoke(this, inputString);
        }
    }
}
