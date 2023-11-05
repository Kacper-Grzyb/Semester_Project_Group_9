using System.Diagnostics;

namespace WorldOfZuul
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public List<Item> Items { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public List<Quest> Quests { get; private set; } = new List<Quest>();


        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);
        }

        public Room(string shortDesc, string longDesc, List<Item> items)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            Items = items ?? new List<Item>();
        }

        public void SetExits(Room? north, Room? east, Room? south, Room? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }

        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }

        public void AddItem(Item newItem)
        {
            Items.Add(newItem);
        }

        public void RemoveItem(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
            else
            {
                Console.WriteLine($"There is no {item.name} in this room!");
            } 
        }

        public void ShowRoomItems()
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("There are no items in this room.");
            }
            else
            {
                Console.WriteLine("On the floor you can see:");
                foreach (Item item in Items)
                {
                    string showItems = string.Join(",", item);
                    Console.WriteLine("\n" + showItems);
                }
            }
        }

        public Item? GetItem(string itemName)
        {
            foreach (Item item in Items)
            {
                if (item.name.ToLower() == itemName.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        public void showPaths()
        {
            if ( Exits.ContainsKey("south") )
            {
                Console.WriteLine("There is a path leading south");
            }
            if ( Exits.ContainsKey("north") )
            {
                Console.WriteLine("There is a path leading north");
            }
            if ( Exits.ContainsKey("east") )
            {
                Console.WriteLine("There is a path leading east");
            }
            if ( Exits.ContainsKey("west") )
            {
                Console.WriteLine("There is a path leading west");
            }
        
        }
    }
}
