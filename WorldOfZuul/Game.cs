using System.Linq.Expressions;
using System.Security;

namespace WorldOfZuul
{
    public class Game
    {

        public Room? currentRoom;
        private Room? previousRoom;

        public Game()
        {
            ChooseWorld();
        }
        public void ChooseWorld()
        {
            Console.Write("Enter the name of your character: ");
            GameManager.playerName = Console.ReadLine();

            // Direct user to desired world
            // The do while loop is to keep asking the user to pick a world until they give an appropriate answer
            bool worldPicked = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Choose a world you'll be starting in");
                Console.WriteLine("1. Grasslands\n2. Forest\n3. Mountains\n4. Jungle\n5. Glacial\n");
                string? userInput = Console.ReadLine();

                if (userInput?.ToLower() == "grasslands")
                {

                    Console.WriteLine("You are located in the grasslands.");
                    worldPicked = true;

                }
                else if (userInput?.ToLower() == "forest")
                {

                    Console.WriteLine("You are located in the forest.");
                    worldPicked = true;

                }
                else if (userInput?.ToLower() == "mountains")
                {
                    Console.WriteLine("You are located in the mountains.");
                    worldPicked = true;

                }
                else if (userInput?.ToLower() == "jungle")
                {
                    Console.WriteLine("You are located in the jungle.");
                    worldPicked = true;
                    CreateJungle();

                }
                else if (userInput?.ToLower() == "glacial")
                {
                    Console.WriteLine("You are located in the glacial biome.");
                    worldPicked = true;

                }
                else
                {
                    Console.WriteLine("Incorrect world name. Please try again.");
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

            Quest stopThePoachers = new Quest("Stop the Poachers", "asdhfalsdkfj", false);
            location5.AddQuest(stopThePoachers);
            

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
        }



        public void Play()
        {
            Parser parser = new();

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentRoom?.ShortDescription);
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
                        Console.WriteLine(currentRoom?.LongDescription);
                        // ShowRoomItems() will display the items in the room. If there are no items in the room 
                        // the method will display an according message as well, so I deleted some of the Console.WriteLines
                        currentRoom?.ShowRoomItems();

                        if (currentRoom?.Items != null && currentRoom.Items.Count > 0)
                        {
                            Console.WriteLine("Do you want to pick up an item? (yes/no)");
                            string yesNo = Console.ReadLine() ?? string.Empty;
                            if (yesNo.ToLower() == "yes")
                            {
                                Console.WriteLine("You can pick up an item by typing its name:");
                                string itemToPick = Console.ReadLine() ?? string.Empty;
                                Item? roomItem = currentRoom.GetItem(itemToPick);

                                if (currentRoom != null && currentRoom.Items != null && roomItem != null)
                                {
                                    // the AddItem function automaticaly takes care of removing the item from the room
                                    // and adds the item to the player's inventory
                                    GameManager.Inventory?.AddItem(roomItem);
                                }
                                else
                                {
                                    Console.WriteLine($"The {itemToPick} is not here.");
                                }
                            }
                        }
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
                        break;

                    case "drop":
                        GameManager.Inventory?.ShowInventory();
                        Console.WriteLine("Type the name of the item which you would like to drop: ");
                        string? dropItemName = Console.ReadLine() ?? string.Empty;
                        if (currentRoom != null && currentRoom.Items != null)
                        {
                            // The DropItem() function takes care of the edge cases like if the item is not in the
                            // players inventory
                            GameManager.Inventory?.DropItem(dropItemName);
                        }
                        else
                        {
                            Console.WriteLine("You can't drop an item here");
                            // This is for a scenario where a currentroom for the player is not set or 
                            // is unknown, so that the player doesn't end up deleting an item from their game
                        }

                        break;
                    case "quest":
                        DisplayAvailableQuests();

                        break;
                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;




                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
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
            Console.WriteLine("Welcome to the World of Zuul!");
            Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        public void PrintHelp()
        {

            Console.WriteLine("You are lost. You are alone. You wander");
            Console.WriteLine("around the university.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
        private void DisplayAvailableQuests()
        {
            if(GameManager.questActive){
                Console.WriteLine("Finish your active quest");
                return;
            }
            if (currentRoom?.Quests != null && currentRoom.Quests.Count > 0)
            {
                bool hasAvailableQuests = false;

                foreach (var quest in currentRoom.Quests)
                {
                    if (quest is Quest availableQuest && !availableQuest.IsCompleted)
                    {
                        Console.WriteLine($"- {availableQuest.Name}: {availableQuest.Description}");
                        hasAvailableQuests = true;
                    }
                }

                if (!hasAvailableQuests)
                {
                    Console.WriteLine("There are no available quests in this room.");
                }
            }
            else
            {
                Console.WriteLine("There are no quests in this room.");
            }

        }
    }
}
