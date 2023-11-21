﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> 
        {
             "north", "east", "south", "west", "look", 
             "back", "quit", "take","drop","quest",
             "inv", "map", "paths", "help", "use",
             "talk"
        };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
