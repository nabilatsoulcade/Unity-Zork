using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Zork.Common
{
    public class Player
    {
        public EventHandler<int> ScoreChanged;
        public EventHandler<string> LocationChanged;
        public EventHandler<int> MovesChanged;
        private int score;
        private int moves;
        private Room location;

        public World World { get; }

        [JsonIgnore]
        public Room Location
        {
            get => location; private set
            {
                location = value;
                LocationChanged?.Invoke(this, LocationName);
            }
        }

        public int Score
        {
            get => score; set
            {
                score = value;
                ScoreChanged?.Invoke(this, Score);
            }
        }
        public int Moves
        {
            get => moves; set
            {
                moves = value;
                MovesChanged?.Invoke(this, moves);
            }
        }
        [JsonIgnore]
        public string LocationName
        {
            get
            {
                return Location?.Name;
            }
            set
            {
                Location = World?.RoomsByName.GetValueOrDefault(value);
                LocationChanged?.Invoke(this, LocationName);
            }
        }

        public Player(World world, string startingLocation)
        {
            World = world;
            LocationName = startingLocation;
        }

        public bool Move(Directions direction)
        {
            bool isValidMove = Location.Neighbors.TryGetValue(direction, out Room destination);
            if (isValidMove)
            {
                Location = destination;
            }

            return isValidMove;
        }
    }

    public enum Directions
    {
        North,
        South,
        East,
        West
    }

}
