using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Command
    {
        public string Name { get; }
        public string[]? arguments { get; }

        public Command(string name, string[]? secondWord = null)
        {
            Name = name;
            //for now only used for the 'take' command
            arguments = secondWord;
        }
    }
}
