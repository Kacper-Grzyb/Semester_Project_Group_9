using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;

namespace WorldOfZuul
{
    public class Game
    {

        public Room? currentRoom;
        private Room? previousRoom;
        private int wrongCommands = 0;
        private int wrongCommandLimit = 5;

        public void Setup()
        {
            GameManager.Inventory = new Inventory();
            GameManager.score = 0;
        }
        public void ChooseWorld()
        { 

            // Direct user to desired world
            // The do while loop is to keep asking the user to pick a world until they give an appropriate answer
            bool worldPicked = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("\x1b[31mChoose a world you'll be starting in: \x1b[0m");
                Console.WriteLine("1. Grasslands\n2. Forest\n3. Mountains\n4. Jungle\n5. Glacial\n");
                Console.Write("> ");
                string? userInput = Console.ReadLine();

                if (userInput?.ToLower() == "grasslands")
                {
                    GameManager.currentPlayerBiome = "Grasslands";
                    worldPicked = true;

                }
                else if (userInput?.ToLower() == "forest")
                {
                    GameManager.currentPlayerBiome = "Forest";
                    worldPicked = true;

                }
                else if (userInput?.ToLower() == "mountains")
                {
                    GameManager.currentPlayerBiome = "Mountains";
                    worldPicked = true;

                }
                else if (userInput?.ToLower() == "jungle")
                {
                    GameManager.currentPlayerBiome = "Jungle";
                    worldPicked = true;
                    CreateJungle();

                }
                else if (userInput?.ToLower() == "glacial")
                {
                    GameManager.currentPlayerBiome = "Glacial Biome";
                    worldPicked = true;
                }
                else
                {
                    Console.WriteLine("Incorrect world name. Please try again. (Type in the name of the biome)");
                }
            }
            while (!worldPicked);

        }
        public void CreateJungle()
        {

            Room? location1 = new("Sector 1", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });

            Room? location2 = new("Sector 2", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<Item> { });

            Room? location3 = new("Sector 3", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<Item> { });

            Room? location4 = new("sector 4", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.", new List<Item> { });

            Room? location5 = new("Sector 5", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<Item> { new Item("Flashlight", "A way to light your path"),
            new Item("Map","Useful for navigation"), new Item("Trap", "Can be used against enemies") });

            Room? location6 = new("Sector 6 ", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<Item> { });

            Room? location7 = new("Sector 7", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });

            Room? location8 = new("Sector 8", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<Item> { });

            Room? location9 = new("Sector 9", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<Item> { });

            var stopPoachers = new List<QuestObjective>
            {
             new QuestObjective("Destroy 9 traps that poachers setup in Sector 9","Map")

            };
            Quest Poachers = new Quest("Disable traps", "fing all the traps that poachers setup in Sector 9", false, false, stopPoachers);
            location5.AddQuest(Poachers);

            location1.SetExits(null, location2, location4, null);
            location2.SetExits(null, location3, location5, location1);
            location3.SetExits(null, null, location6, location2);
            location4.SetExits(location1, location5, location7, null);

            location5.SetExits(location2, location6, location8, location4);

            location6.SetExits(location3, null, location9, location5);
            location7.SetExits(location4, location8, null, null);
            location8.SetExits(location5, location9, null, location7);
            location9.SetExits(location6, null, null, location8);

            currentRoom = location5;
            GameManager.currentPlayerRoom = currentRoom;
            GameManager.Inventory = new Inventory();
            
            Player.mapHeight = 3;
            Player.mapWidth = 3;
            Player.X = 1;
            Player.Y = 1;
        }



        public void Play()
        {
            Parser parser = new();
            Setup();
            PrintWelcome();
            ChooseWorld();
            PrintBiomeWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                currentRoom = GameManager.currentPlayerRoom;

                Console.WriteLine("\nCurrent room: " + currentRoom?.ShortDescription);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    wrongCommands++;
                    if(wrongCommands == wrongCommandLimit)
                    {
                        Console.WriteLine("Remember that you can type 'help' to display available commands");
                        wrongCommands = 0;
                    }
                    continue;
                }

                switch (command.Name)
                {
                    case "look":
                        if (currentRoom == null)
                        {
                            //added this here for now since we are still adding the biomes
                            Console.WriteLine("You are in a null room. Probably an error or an unfinished feature");
                            break;
                        }
                        DisplayItems(); //made method so it looks better
                        break;
                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            GameManager.currentPlayerRoom = GameManager.previousPlayerRoom;
                        break;
                    case "drop":
                        // turned this into a method also
                        Drop();
                        break;
                    case "quest":
                        Console.WriteLine("Do you want to see 1.Available quest 2. Active quest 3. Quest objective");
                        int rsp = Convert.ToInt32(Console.ReadLine());
                        if (rsp == 1)
                        {
                            PickAvailableQuests();
                        }
                        else if (rsp == 2)
                        {
                            Console.WriteLine($"Your active quest is: {GameManager.ActiveQuest}");
                        }
                        else if (rsp == 3)
                        {
                            if (GameManager.ActiveQuest?.Objectives != null)
                            {
                                Console.WriteLine("These are your objectives:");
                                foreach (var objective in GameManager.ActiveQuest.Objectives)
                                {
                                    Console.WriteLine($"- {objective.Description}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No active quest or objectives found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Wrong input");
                            break;
                        }
                        break;
                    case "inv":
                        GameManager.Inventory?.ShowInventory();
                        break;
                    case "map":
                        DisplayMap();
                        break;
                    case "north":
                        Player.Y = Math.Max(0, Player.Y - 1);
                        Move(command.Name);
                        DisplayMap();
                        break;
                    case "south":
                        Player.Y = Math.Min(Player.mapHeight - 1, Player.Y + 1);
                        Move(command.Name);
                        DisplayMap();
                        break;
                    case "east":
                        Player.X = Math.Min(Player.mapWidth - 1, Player.X + 1);
                        Move(command.Name);
                        DisplayMap();
                        break;
                    case "west":
                        Player.X = Math.Max(0, Player.X - 1);
                        Move(command.Name);
                        DisplayMap();
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("I don't know that command.");
                        break;




                }
            }

            Console.WriteLine("Thank you for playing Scribescape!");
        }

        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }


        private void PrintWelcome()
        {
            Console.WriteLine("Welcome to"); // should it be maybe the full title? Console.WriteLine("Welcome to Scribescape: Textual Eco-Pursuits!");
            Console.WriteLine(" ___   ___  ____  ____  ____  ____  ___   ___    __    ____  ____ \r\n/ __) / __)(  _ \\(_  _)(  _ \\( ___)/ __) / __)  /__\\  (  _ \\( ___)\r\n\\__ \\( (__  )   / _)(_  ) _ < )__) \\__ \\( (__  /(__)\\  )___/ )__) \r\n(___/ \\___)(_)\\_)(____)(____/(____)(___/ \\___)(__)(__)(__)  (____)\n");
            Console.Write("Enter the name of your character: ");
            GameManager.playerName = Console.ReadLine();
        }

        private void PrintBiomeWelcome()
        {
            Console.WriteLine();
            Console.WriteLine($"Welcome to the {GameManager.currentPlayerBiome}!");
            PrintHelp();
        }

        public void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east' or 'west'");
            Console.WriteLine("Type 'look' for more details");
            Console.WriteLine("Type 'back' to go to the previous room");
            Console.WriteLine("Type 'help' to print this message again");
            Console.WriteLine("Type 'quit' to exit the game");
            Console.WriteLine("Type 'quest' to see available quest in the room");
            Console.WriteLine("Type 'drop' to drop an item from your inventory");
            Console.WriteLine("Type 'inv' to display your inventory");
            Console.WriteLine("Type 'map' to display the biome map and your current location");
        }
        public void PickAvailableQuests()
        {
            if (GameManager.IsActive)
            {
                Console.WriteLine("You are already on one quest");
                return;
            }
            if (currentRoom?.Quests == null || currentRoom.Quests.Count == 0)
            {
                Console.WriteLine("There are no quests available in this room.");
                return;
            }

            Console.WriteLine("Available quests in this room:");
            int index = 1;
            List<Quest> availableQuests = new List<Quest>();
            foreach (var quest in currentRoom.Quests)
            {
                if (quest is Quest jungleQuest && !jungleQuest.IsCompleted)
                {
                    Console.WriteLine($"{index}. {jungleQuest.Name}: {jungleQuest.Description}");
                    availableQuests.Add(jungleQuest);
                    index++;
                }
            }

            if (availableQuests.Count == 0)
            {
                Console.WriteLine("There are no available quests in this room.");
                return;
            }

            Console.WriteLine("Enter the number of the quest you want to pick:");

            if (int.TryParse(Console.ReadLine(), out int questNumber) && questNumber > 0 && questNumber <= availableQuests.Count)
            {
                GameManager.ActiveQuest = availableQuests[questNumber - 1];
                Console.WriteLine($"You have picked the quest: {GameManager.ActiveQuest.Name}");
                foreach (var objective in GameManager.ActiveQuest.Objectives)
                {
                    Console.WriteLine($"- {objective.Description}");
                }
                GameManager.IsActive = true;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please pick a valid quest number.");
            }

        }


        public void DisplayItems()
        {
            bool firstIteration = true;
            Console.WriteLine(currentRoom?.LongDescription);
            // ShowRoomItems() will display the items in the room. If there are no items in the room 
            // the method will display an according message as well, so I deleted some of the Console.WriteLines
            currentRoom?.ShowRoomItems();

            while (true)
            { 
                if (currentRoom?.Items != null && currentRoom.Items.Count > 0)
                {
                    if (firstIteration)
                    {
                        Console.WriteLine("Do you want to pick up an item? (yes/no)");
                    }
                    else
                    {
                        Console.WriteLine("Do you want to pick up another item? (yes/no)");
                    }
                    Console.Write("> ");
                    string rsp = Console.ReadLine() ?? string.Empty;

                    if (rsp.ToLower() == "yes" || rsp.ToLower() == "y")
                    {
                        if (GameManager.Inventory != null && GameManager.Inventory.isFull())
                        {
                            Console.WriteLine("Your inventory is full!");
                            return;
                        }
                        Console.WriteLine("You can pick up an item by typing its name:");
                        Console.Write("> ");
                        string itemName = Console.ReadLine()?.Trim() ?? string.Empty;
                        Item? roomItem = currentRoom.GetItem(itemName);

                        if (currentRoom != null && currentRoom.Items != null && roomItem != null)
                        {
                            // the AddItem function automaticaly takes care of removing the item from the room
                            // and adds the item to the player's inventory
                            GameManager.Inventory?.AddItem(roomItem);

                            if (GameManager.ActiveQuest != null)
                            {
                                foreach (var objective in GameManager.ActiveQuest.Objectives)
                                {
                                    if (!objective.IsCompleted && objective.NeededItems.Contains(itemName))
                                    {
                                        objective.CompleteObjective(); // Mark the objective as completed
                                        Console.WriteLine($"Objective completed: {objective.Description}");
                                        GameManager.ActiveQuest.CheckQuestCompletion(); // Check if all objectives are completed
                                        break; // Assuming one item cannot complete multiple objectives
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"The {itemName} is not here.");
                        }
                    }
                    else if(rsp.ToLower() == "no" || rsp.ToLower() == "n")
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid response.");
                        return;
                    }
                }
                firstIteration = false;
            }
        }

        private void Drop()
        {
            if(GameManager.Inventory?.Size() == 0)
            {
                Console.WriteLine("There are no items in your inventory!");
                return;
            }
            GameManager.Inventory?.ShowInventory();
            Console.WriteLine("Type the name of the item which you would like to drop: ");
            string? dropItemName = Console.ReadLine() ?? string.Empty;
            if (currentRoom != null && currentRoom.Items != null)
            {
                GameManager.Inventory?.DropItem(dropItemName);
                // The DropItem() function takes care of the edge cases like if the item is not in the players inventory
            }
            else
            {
                Console.WriteLine("You can't drop an item here");
                // This is for a scenario where a currentroom for the player is not set or 
                // is unknown, so that the player doesn't end up deleting an item from their game
            }
        }
        private void DisplayMap()
        {
            int step = 1;
            
            for (int y = 0; y < Player.mapHeight; y++)
            {
                for (int x = 0; x < Player.mapWidth; x++)
                {
                    if (x == Player.X && y == Player.Y)
                    {
                        Console.Write("[P]");
                        step++; // players current position
                    }
                    else
                    {                        
                        Console.Write($"[{step}]");
                        step++; 
                    }
                }
                Console.WriteLine(); 
            }
        }

    }
}
