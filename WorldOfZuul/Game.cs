using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;

namespace WorldOfZuul
{
    public class Game
    {
        private int wrongCommands = 0;
        private int wrongCommandLimit = 5;
        private MountainsBiome? mountainsBiome = null;
        private GlacialBiome? glacialBiome = null;
        private ForestBiome? forestBiome = null;
        private JungleBiome? jungleBiome = null;
        private GrasslandsBiome? grasslandsBiome = null;

        public void Setup()
        {
            GameManager.Inventory = new Inventory();
            GameManager.gameInstance = this;
            GameManager.previousPlayerRooms = new Stack<Room>();
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
                Console.WriteLine("\x1b[31mChoose the world you'll be starting in: \x1b[0m");
                Console.WriteLine("1. Grasslands\n2. Forest\n3. Mountains\n4. Jungle\n5. Glacial\n");
                Console.Write("> ");
                string? userInput = Console.ReadLine()?.ToString();

                if (userInput?.ToLower() == "grasslands")
                {
                    worldPicked = true;
                    if (grasslandsBiome == null)
                    {
                        grasslandsBiome = new GrasslandsBiome();
                        GameManager.grasslandsBiomeInstance = grasslandsBiome;
                    }
                    GameManager.currentPlayerRoom = grasslandsBiome.startLocation;
                    GameManager.currentPlayerBiome = grasslandsBiome;
                }
                else if (userInput?.ToLower() == "forest")
                {
                    worldPicked = true;
                    if (forestBiome == null)
                    {
                        forestBiome = new ForestBiome();
                        GameManager.forestBiomeInstance = forestBiome;
                    }
                    GameManager.currentPlayerRoom = forestBiome.startLocation;
                    GameManager.currentPlayerBiome = forestBiome;
                }
                else if (userInput?.ToLower() == "mountains")
                {
                    worldPicked = true;
                    if (mountainsBiome == null)
                    {
                        mountainsBiome = new MountainsBiome();
                        GameManager.mountainsBiomeInstance = mountainsBiome;
                    }
                    GameManager.currentPlayerRoom = mountainsBiome.startLocation;
                    GameManager.currentPlayerBiome = mountainsBiome;

                }
                else if (userInput?.ToLower() == "jungle")
                {
                    worldPicked = true;
                    if (jungleBiome == null)
                    {
                        jungleBiome = new JungleBiome();
                        GameManager.jungleBiomeInstance = jungleBiome;
                    }
                    GameManager.currentPlayerRoom = jungleBiome.startLocation;
                    GameManager.currentPlayerBiome = jungleBiome;
                }
                else if (userInput?.ToLower() == "glacial")
                {
                    worldPicked = true;
                    if (glacialBiome == null)
                    {
                        glacialBiome = new GlacialBiome();
                        GameManager.glacialBiomeInstance = glacialBiome;
                    }
                    GameManager.currentPlayerRoom = glacialBiome.startLocation;
                    GameManager.currentPlayerBiome = glacialBiome;
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please try again. (Type in the name of the biome)");
                }
            }
            while (!worldPicked);
            Console.Clear();
            PrintBiomeWelcome();
        }

        public void Play()
        {
            Parser parser = new();
            Setup();
            PrintWelcome();
            Console.Clear();
            ChooseWorld();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                GameManager.currentPlayerRoom?.Update();
                Console.WriteLine("\nCurrent room: " + GameManager.currentPlayerRoom?.ShortDescription);
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
                    if (wrongCommands == wrongCommandLimit)
                    {
                        Console.WriteLine("Remember that you can type 'help' to display available commands");
                        wrongCommands = 0;
                    }
                    continue;
                }
                switch (command.Name)
                {

                    case "look":
                        if (GameManager.currentPlayerRoom == null)
                        {
                            Console.WriteLine("Error: players room is not set to anything.");
                            break;
                        }
                        Look();
                        break;
                    case "back":
                        if (GameManager.previousPlayerRooms?.Count() == 0)
                        {
                            Console.WriteLine("You can't go back from here!");
                        }
                        else
                        {
                            GameManager.currentPlayerRoom = GameManager.previousPlayerRooms?.Peek();
                            GameManager.previousPlayerRooms?.Pop();
                            Console.Clear();
                        }
                        break;
                    case "drop":
                        // turned this into a method also
                        Drop(command);
                        break;
                    case "take":
                        Take(command);
                        break;
                    case "use":
                        UseItem(command);
                        break;
                    case "quest":
                        Console.WriteLine("Do you want to see 1.Available quest 2. Active quest 3. Quest objective (Pick a number)");
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
                            if (GameManager.ActiveQuest?.QuestObjectives != null)
                            {
                                Console.WriteLine("These are your objectives:");
                                foreach (var objective in GameManager.ActiveQuest.QuestObjectives)
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
                    case "paths":
                        GameManager.currentPlayerRoom?.showPaths();
                        break;
                    case "north":
                        Player.Y = Math.Max(0, Player.Y - 1);
                        Move(command.Name);

                        break;
                    case "south":
                        Player.Y = Math.Min(Player.mapHeight - 1, Player.Y + 1);
                        Move(command.Name);

                        break;
                    case "east":
                        Player.X = Math.Min(Player.mapWidth - 1, Player.X + 1);
                        Move(command.Name);

                        break;
                    case "west":
                        Player.X = Math.Max(0, Player.X - 1);
                        Move(command.Name);

                        break;
                    case "talk":
                        if (GameManager.currentPlayerRoom != null && GameManager.currentPlayerRoom.NpcsInRoom.Count < 1)
                        {
                            Console.WriteLine("There is no one to talk to!");
                            break;
                        }
                        else
                        {
                            if (GameManager.currentPlayerRoom != null && GameManager.currentPlayerRoom.NpcsInRoom.Count == 1)
                            {
                                GameManager.currentPlayerRoom?.InteractWithNpc(GameManager.currentPlayerRoom, GameManager.currentPlayerRoom.NpcsInRoom[0].Name);
                            }
                            else
                            {
                                GameManager.currentPlayerRoom?.ShowNpcsInRoom(GameManager.currentPlayerRoom);
                                Console.WriteLine("Which npc would you like to talk to?");

                                string? chosenNpc = Console.ReadLine();
                                if (chosenNpc != null)
                                {
                                    GameManager.currentPlayerRoom?.InteractWithNpc(GameManager.currentPlayerRoom, chosenNpc);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please try again.");
                                }
                            }

                        }
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
            if (GameManager.currentPlayerRoom?.Exits.ContainsKey(direction) == true && GameManager.currentPlayerRoom != null)
            {
                if (GameManager.currentPlayerRoom?.blockedExits[direction] == false)
                {
                    GameManager.previousPlayerRooms?.Push(GameManager.currentPlayerRoom);
                    GameManager.currentPlayerRoom = GameManager.currentPlayerRoom?.Exits[direction];
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("This path is currently blocked!");
                }
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
            // Deleted this from the ifs in the north south east west switch statements and moved it here
            currentPlayerBiome?.checkForAvailableObjectives();
        }


        private void PrintWelcome()
        {
            Console.WriteLine("Welcome to"); // should it be maybe the full title? so Scribescape: Textual Eco-Pursuits
            Console.WriteLine(" ___   ___  ____  ____  ____  ____  ___   ___    __    ____  ____ \r\n/ __) / __)(  _ \\(_  _)(  _ \\( ___)/ __) / __)  /__\\  (  _ \\( ___)\r\n\\__ \\( (__  )   / _)(_  ) _ < )__) \\__ \\( (__  /(__)\\  )___/ )__) \r\n(___/ \\___)(_)\\_)(____)(____/(____)(___/ \\___)(__)(__)(__)  (____)\n");
            Console.Write("Enter the name of your character: ");
            GameManager.playerName = Console.ReadLine();
        }

        private void PrintBiomeWelcome()
        {
            Console.WriteLine();
            GameManager.currentPlayerBiome?.WelcomeMessage();
            PrintHelp();
        }

        public void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east' or 'west'");
            Console.WriteLine("Type 'back' to go to the previous room");
            Console.WriteLine("Type 'look' for more details");
            Console.WriteLine("Type 'take [item name]' to pick up an item");
            Console.WriteLine("Type 'use [item name]' to use an item");
            Console.WriteLine("Type 'drop [item name]' to drop an item from your inventory");
            Console.WriteLine("Type 'inv' to display your inventory");
            Console.WriteLine("Type 'quest' to see available quest in the room");
            Console.WriteLine("Type 'paths' to show directions in which you can go from your current location");
            Console.WriteLine("Type 'talk' to talk to the NPC or NPCs in your current location");
            Console.WriteLine("Type 'quit' to exit the game");
            Console.WriteLine("Type 'help' to print this message again");
        }
        public void PickAvailableQuests()
        {
            if (GameManager.IsActiveQuest)
            {
                Console.WriteLine("You are already on one quest");
                return;
            }
            if (GameManager.currentPlayerRoom?.Quests == null || GameManager.currentPlayerRoom.Quests.Count == 0)
            {
                Console.WriteLine("There are no quests available in this room.");
                return;
            }

            Console.WriteLine("Available quests in this room:");
            int index = 1;
            List<Quest> availableQuests = new List<Quest>();
            foreach (var quest in GameManager.currentPlayerRoom.Quests)
            {
                if (quest is Quest jungleQuest && !jungleQuest.IsCompleted)
                {
                    Console.WriteLine($"{index}. {jungleQuest.QuestName}: {jungleQuest.QuestDescription}");
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
                Console.WriteLine($"You have picked the quest: {GameManager.ActiveQuest.QuestName}");
                foreach (var objective in GameManager.ActiveQuest.QuestObjectives)
                {
                    Console.WriteLine($"- {objective.Description}");
                }
                GameManager.IsActiveQuest = true;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please pick a valid quest number.");
            }

        }


        public void Look()
        {
            Console.WriteLine(GameManager.currentPlayerRoom?.LongDescription);
            // ShowRoomItems() will display the items in the room. If there are no items in the room 
            // the method will display an according message as well, so I deleted some of the Console.WriteLines
            GameManager.currentPlayerRoom?.ShowRoomItems();
        }


        private void Drop(Command command)
        {
            if (GameManager.Inventory?.Size() == 0)
            {
                Console.WriteLine("There are no items in your inventory!");
                return;
            }
            if (command.arguments == null)
            {
                Console.WriteLine("Please specify which items you want to drop.");
                return;
            }
            else
            {
                if (GameManager.currentPlayerRoom != null && GameManager.currentPlayerRoom.Items != null)
                {
                    foreach (string itemName in command.arguments)
                    {
                        // This function will remove an item from the player's inventory if it exists
                        // and add it into the inventory of currentPlayerRoom
                        GameManager.Inventory?.DropItem(itemName);
                    }
                }
                else
                {
                    Console.WriteLine("You can't drop items here!");
                    // This check is implemented just in case there is something wrong with currentPlayerRoom
                    // so that the player doesn't end up deleting an item from his game
                }

            }
        }


        private void Take(Command command)
        {
            if (command.arguments == null)
            {
                Console.WriteLine("Please specify which items you want to pick up.");
                return;
            }
            else
            {
                foreach (string itemName in command.arguments)
                {
                    Item? takeItem = GameManager.currentPlayerRoom?.GetItem(itemName);
                    if (GameManager.currentPlayerRoom != null && GameManager.currentPlayerRoom.Items != null && takeItem != null)
                    {
                        GameManager.Inventory?.AddItem(takeItem);

                        if (GameManager.ActiveQuest != null)
                        {
                            foreach (var objective in GameManager.ActiveQuest.QuestObjectives)
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
            }
        }

        //for now this function checks if an item can be activated and activates it, to be expanded in the future
        private void UseItem(Command command)
        {
            if (command.arguments == null)
            {
                Console.WriteLine("Please specify which items you want to use");
                return;
            }
            else
            {
                foreach (string itemName in command.arguments)
                {
                    Item? useItem = GameManager.Inventory?.GetItem(itemName);
                    if (useItem != null)
                    {
                        useItem.Activate();
                    }
                    else
                    {
                        Console.WriteLine($"There is no {itemName} in your inventory!");
                    }
                }
            }
        }


    }
}
