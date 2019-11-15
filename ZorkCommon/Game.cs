using System;
using System.IO;
using Newtonsoft.Json;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        [JsonIgnore]
        public bool IsRunning
        {
            get => isRunning; set
            {
                isRunning = value;
                GameRunningChanged?.Invoke(this, isRunning);
            }
        }

        [JsonIgnore]
        public CommandManager CommandManager { get; }

        public IOutputService Output { get; private set; }
        public IInputService Input { get; private set; }
        public EventHandler<bool> GameRunningChanged;
        private bool isRunning;

        public Game(World world, Player player)
        {
            World = world;
            Player = player;
        }

        public Game()
        {
            Command[] commands =
            {
                new Command("LOOK", new string[] { "LOOK", "L" },
                    (game, commandContext) => Output.WriteLine(game.Player.Location.Description)),

                new Command("QUIT", new string[] { "QUIT", "Q" },
                    (game, commandContext) => game.IsRunning = false),

                new Command("NORTH", new string[] { "NORTH", "N" }, MovementCommands.North),

                new Command("SOUTH", new string[] { "SOUTH", "S" }, MovementCommands.South),

                new Command("EAST", new string[] { "EAST", "E" }, MovementCommands.East),

                new Command("WEST", new string[] { "WEST", "W" }, MovementCommands.West),

                new Command("REWARD", new string[] { "REWARD", "R" }, Reward)
            };

            CommandManager = new CommandManager(commands);
        }

        public static void Reward(Game game, CommandContext commandContext)
        {
            game.Player.Score++;
            game.Output.WriteLine("Your score would be " + game.Player.Score.ToString() + ", in " + game.Player.Moves.ToString() + " move(s)");
        }

        public static void Score(Game game, CommandContext commandContext)
        {
            game.Output.WriteLine("Score:" + game.Player.Score.ToString());
        }


        public static Game Load(string filename, IOutputService output, IInputService input)
        {
            return LoadFromFile(File.ReadAllText(filename), output, input);
        }

        public static Game LoadFromFile(string jsonString, IOutputService output, IInputService input)
        {
            Game game = JsonConvert.DeserializeObject<Game>(jsonString);
            game.Output = output;
            game.Input = input;
            game.Player = game.World.SpawnPlayer();
            game.IsRunning = true;
            game.Input.InputRecieved += game.InputRecieved;
            game.CommandManager.PerformCommand(game, "LOOK");
            return game;
        }

        private void InputRecieved(object sender, string e)
        {
            Room previousRoom = Player.Location;
            if (CommandManager.PerformCommand(this, e))
            {
                Player.Moves++;

                if (previousRoom != Player.Location)
                {
                    CommandManager.PerformCommand(this, "LOOK");
                }
            }
            else
            {
                Output.WriteLine("That's not a verb I recognize.");
            }
        }
    }
}

