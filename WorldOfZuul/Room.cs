using System.Diagnostics;

namespace WorldOfZuul
{
    public class Room
    {
        public string ShortDescription { get; protected set; }
        public string LongDescription { get; protected set; }
        public List<Item> Items { get; private set; }
        private List<Item> hiddenItems;
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public Dictionary<string, bool> blockedExits { get; protected set; } = new(); // true if an exit is blocked and false if not
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
            hiddenItems = new List<Item>();
        }

        public virtual void Update()
        {
            // Here you can add state changes to the room to make it more dynamic
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
            {
                Exits[direction] = neighbor;
                blockedExits[direction] = false; // by default every exit added is open
            }
        }

        public void BlockExit(string direction)
        {
            if(blockedExits.ContainsKey(direction))
            {
                blockedExits[direction] = true;
            }
        }

        public void UnlockExit(string direction)
        {
            if (blockedExits.ContainsKey(direction))
            {
                blockedExits[direction] = false;
            }
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

        // I don't know if this is the best way of doing this but the reason I like this implementation is for example if
        // the items in a room are hidden and player walks into that room and drops an item he will still be able to pick it back up
        // because the dropped item will be added to the Items array and not the hiddenItems one
        // This will not be the case though if the items are visible and dropping an item will make them hide them, so that would need a fix,
        // the best solution i see right now is maybe also check if a room that unhides the items is also in the room
        public void HideItems()
        {
            foreach (Item item in Items)
            {
                hiddenItems.Add((Item)item.Clone());
            }
            Items.Clear();
        }

        public void UnhideItems()
        {
            foreach (Item item in hiddenItems)
            {
                Items.Add((Item)item.Clone());
            }
            hiddenItems.Clear();
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
