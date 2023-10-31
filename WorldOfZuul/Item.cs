using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Item
    {
        public readonly string name;
        public readonly string description;

        public Item(string name, string description) {
            this.name = name;
            this.description = description;
        }

        // This method will return a string with the item's name and description
        public override string ToString()
        {
            return $"{name}: {description}";
        }
    }
}
