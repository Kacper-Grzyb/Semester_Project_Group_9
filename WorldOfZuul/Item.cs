﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Item : ICloneable
    {
        public readonly string name;
        public readonly string description;
        public bool isActive = false;
        private bool activable;

        public Item(string name, string description, bool activable = false)
        {
            this.name = name;
            this.description = description;
            this.activable = activable;
        }

        public void Activate()
        {
            if (activable)
            {
                if(!this.isActive)
                {
                    this.isActive = true;
                    Console.WriteLine($"Item {this.name} activated!");
                }
                else
                {
                    this.isActive = false;
                    Console.WriteLine($"Item {this.name} deactivated!");
                }
                
            }
            else
            {
                Console.WriteLine($"Cannot activate {this.name}!");
            }
        }

        // This method will return a string with the item's name and description
        public override string ToString()
        {
            return $"{name}: {description}";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
