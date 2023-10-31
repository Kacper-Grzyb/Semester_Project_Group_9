using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public sealed class Inventory
    {
        private List<Item> items = new List<Item>();
        private int currentInventorySize = 0;
        private int maxInventorySize = 9;

        // Prints all of the items in the player's inventory
        public void ShowInventory()
        {
            //check if player has any items
            if (items.Count == 0)
            {
                Console.WriteLine("There are no items in your inventory.");
            } 
            else
            {
                Console.WriteLine("Items currently in your inventory:");
                foreach(Item item in items)
                {
                     Console.WriteLine(item.ToString());
                }
            }
        }

        public void AddItem(Item newItem)
        {
            if(currentInventorySize < maxInventorySize) 
            {
                items.Add(newItem);
                currentInventorySize++;
                GameManager.currentPlayerRoom?.RemoveItem(newItem);
                Console.WriteLine($"{newItem.name} added to your inventory");
            }
            else
            {
                Console.WriteLine("There is not enough space in your inventory!");
            }
        }

        /// <summary>
        ///  To use this method you must provide the name of the item you want to remove
        /// </summary>
        // For now I implemented this method by using the name of the item to be removed, but later I can remake it
        // so that for example it will not require any arguments. The method will display the whole inventory with an
        // index next to each item and will ask the player to type in the index of the item they would like to remove
        public void DropItem(string itemName) {
            if (currentInventorySize==0)
            {
                Console.WriteLine("You have no items to drop!");
                return;
            }
            foreach (Item item in items)
            {
                if (item.name == itemName) 
                {
                    items.Remove(item);
                    currentInventorySize--;
                    GameManager.currentPlayerRoom?.AddItem(item);
                    Console.WriteLine($"{itemName} was removed from your inventory");
                    return;
                }
            }
            // This Console.WriteLine will only execute if the foreach loop loops through every item without finding a match
            Console.WriteLine($"Could not find {itemName} in your inventory!");

            
        }

        public Item? GetItem(string itemName)
        {
            foreach (Item item in items)
            {
                if (item.name.ToLower() == itemName.ToLower())
                {
                    return item;
                }
            }  
            return null;
        }
    }
}
