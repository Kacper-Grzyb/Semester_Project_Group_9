﻿using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;

namespace WorldOfZuul
{
    public class Game
    {
        private bool continuePlaying = true;
        private int wrongCommands = 0;
        private int wrongCommandLimit = 5;
        private MountainsBiome? mountainsBiome = null;
        private GlacialBiome? glacialBiome = null;
        private ForestBiome? forestBiome = null;
        private JungleBiome? jungleBiome = null;
        private GrasslandsBiome? grasslandsBiome = null;
        TrashSort trashgame = new();
        SustainabilityGame sustainabilitybasegame = new();

        public void Setup()
        {
            GameManager.Inventory = new Inventory();
            GameManager.gameInstance = this;
            GameManager.previousPlayerRooms = new Stack<Room>();
            GameManager.score = 0;
        }
        public void ChooseWorld()
        {
            if (grasslandsFinished && mountainsFinished && jungleFinished && glacialFinished)
            {
                Console.WriteLine("Congratulations for beating the game!");
                Console.WriteLine("Thank you for being an active participant in our mission to raise awareness about the Sustainable Development Goals.");
                Console.WriteLine("Your achievements in the game contribute to a broader understanding of the challenges we face and the collective efforts required to overcome them.");
                continuePlaying = false;
                return;
            }

            Console.WriteLine("Use \u001b[33m↑\u001b[0m or \u001b[33m↓\u001b[0m to navigate and press \u001b[33mEnter\u001b[0m to select the biome you want to play");
            ConsoleKeyInfo key;
            int option = 1;
            bool isSelected = false;
            (int left, int top) = Console.GetCursorPosition();
            string selection = " \u001b[33m→\u001b[0m \u001b[7m";

            Console.CursorVisible = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);

                Console.Write($"\n{(option == 1 ? selection : "   ")}");
                if (grasslandsFinished)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Grasslands");
                    Console.ResetColor();
                }
                else Console.Write("Grasslands");
                Console.Write("\u001b[0m");
                /*
                Console.Write($"\n{(option == 2 ? selection : "   ")}");
                if (forestFinished)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Forest");
                    Console.ResetColor();
                }
                else Console.Write("Forest");
                Console.Write("\u001b[0m");
                */
                Console.Write($"\n{(option == 2 ? selection : "   ")}");
                if (mountainsFinished)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Mountains");
                    Console.ResetColor();
                }
                else Console.Write("Mountains");
                Console.Write("\u001b[0m");

                Console.Write($"\n{(option == 3 ? selection : "   ")}");
                if (jungleFinished)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Jungle");
                    Console.ResetColor();
                }
                else Console.Write("Jungle");
                Console.Write("\u001b[0m");

                Console.Write($"\n{(option == 4 ? selection : "   ")}");
                if (glacialFinished)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Glacial");
                    Console.ResetColor();
                }
                else Console.Write("Glacial");
                Console.Write("\u001b[0m");
                Console.Write('\n');


                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 4 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 4 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        (int l, int t) = Console.GetCursorPosition();
                        if (option == 1 && grasslandsFinished)
                        {
                            Console.WriteLine("\nYou have already finished the Grasslands biome.");
                            Thread.Sleep(750);
                            Console.SetCursorPosition(l, t);
                            Console.WriteLine();
                            Console.WriteLine(new string(' ', Console.WindowWidth));
                            break;
                        }
                        /*
                        else if (option == 2 && forestFinished)
                        {
                            Console.WriteLine("\nYou have already finished the Forest biome.");
                            Thread.Sleep(750);
                            Console.SetCursorPosition(l, t);
                            Console.WriteLine();
                            Console.WriteLine(new string(' ', Console.WindowWidth));
                            break;
                        }
                        */
                        else if (option == 2 && mountainsFinished)
                        {
                            Console.WriteLine("\nYou have already finished the Mountains biome.");
                            Thread.Sleep(750);
                            Console.SetCursorPosition(l, t);
                            Console.WriteLine();
                            Console.WriteLine(new string(' ', Console.WindowWidth));
                            break;
                        }
                        else if (option == 3 && jungleFinished)
                        {
                            Console.WriteLine("\nYou have already finished the Jungle biome.");
                            Thread.Sleep(750);
                            Console.SetCursorPosition(l, t);
                            Console.WriteLine();
                            Console.WriteLine(new string(' ', Console.WindowWidth));
                            break;
                        }
                        else if (option == 4 && glacialFinished)
                        {
                            Console.WriteLine("\nYou have already finished the Glacial biome.");
                            Thread.Sleep(750);
                            Console.SetCursorPosition(l, t);
                            Console.WriteLine();
                            Console.WriteLine(new string(' ', Console.WindowWidth));
                            break;
                        }

                        isSelected = true;
                        break;
                }

            }

            Console.CursorVisible = true;
            Console.WriteLine();
            if (option == 1)
            {
                Console.WriteLine("You have selected the Grasslands Biome.");
                Thread.Sleep(500);
                if (grasslandsBiome == null)
                {
                    grasslandsBiome = new GrasslandsBiome();
                    GameManager.grasslandsBiomeInstance = grasslandsBiome;
                }
                GameManager.currentPlayerRoom = grasslandsBiome.startLocation;
                GameManager.currentPlayerBiome = grasslandsBiome;
            }
            /*
            else if (option == 2)
            {
                Console.WriteLine("You have selected the Forest Biome.");
                Thread.Sleep(500);
                if (forestBiome == null)
                {
                    forestBiome = new ForestBiome();
                    GameManager.forestBiomeInstance = forestBiome;
                }
                GameManager.currentPlayerRoom = forestBiome.startLocation;
                GameManager.currentPlayerBiome = forestBiome;
            }
            */
            else if (option == 2)
            {
                Console.WriteLine("You have selected the Mountains Biome.");
                Thread.Sleep(500);
                if (mountainsBiome == null)
                {
                    mountainsBiome = new MountainsBiome();
                    GameManager.mountainsBiomeInstance = mountainsBiome;
                }
                GameManager.currentPlayerRoom = mountainsBiome.startLocation;
                GameManager.currentPlayerBiome = mountainsBiome;
            }
            else if (option == 3)
            {
                Console.WriteLine("You have selected the Jungle Biome.");
                Thread.Sleep(500);
                if (jungleBiome == null)
                {
                    jungleBiome = new JungleBiome();
                    GameManager.jungleBiomeInstance = jungleBiome;
                }
                GameManager.currentPlayerRoom = jungleBiome.startLocation;
                GameManager.currentPlayerBiome = jungleBiome;
            }
            else
            {
                Console.WriteLine("You have selected the Glacial Biome.");
                Thread.Sleep(500);
                if (glacialBiome == null)
                {
                    glacialBiome = new GlacialBiome();
                    GameManager.glacialBiomeInstance = glacialBiome;
                }
                GameManager.currentPlayerRoom = glacialBiome.startLocation;
                GameManager.currentPlayerBiome = glacialBiome;
            }

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

                    case "close1":
                        if (currentPlayerBiome == grasslandsBiome)
                            grasslandsBiome.AnimalRoom1();
                        break;

                    case "close2":
                        if (currentPlayerBiome == grasslandsBiome)
                            grasslandsBiome.AnimalRoom2();
                        break;

                    case "close3":
                        if (currentPlayerBiome == grasslandsBiome)
                            grasslandsBiome.AnimalRoom3();
                        break;
                    case "recycle":

                        if (currentPlayerBiome == grasslandsBiome)
                            trashgame.Game();
                        break;
                    case "trackingdevice":
                        if (currentPlayerBiome == grasslandsBiome)
                            currentPlayerRoom = grasslandsBiome.GetRoomByName("Collaboration Corner");
                        break;
                    case "exit-grasslands":
                        if (currentPlayerBiome == grasslandsBiome && currentPlayerRoom == grasslandsBiome.GetRoomByName("Collaboration Corner"))
                            grasslandsBiome.CheckWinCondition();
                        break;
                    case "sustainable-quiz":
                        if (currentPlayerBiome == grasslandsBiome)
                            sustainabilitybasegame.SustainableQuiz();
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

            Console.WriteLine("\nThank you for playing Scribescape!");
            Console.WriteLine("Created by:\n Jakub Galica\n Kacper Grzyb\n Filip Sima\n Natalia Fudali\n Samuel Osei Adjabeng");
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


        private void WriteBiomeOptions()
        {
            //"1. Grasslands\n2. Forest\n3. Mountains\n4. Jungle\n5. Glacial\n"
            Console.Write("1. ");
            if (grasslandsFinished)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Grasslands");
                Console.ResetColor();
            }
            else Console.Write("Grasslands");

            Console.Write("\n2. ");
            if (forestFinished)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Forest");
                Console.ResetColor();
            }
            else Console.Write("Forest");

            Console.Write("\n3. ");
            if (mountainsFinished)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Mountains");
                Console.ResetColor();
            }
            else Console.Write("Mountains");

            Console.Write("\n4. ");
            if (jungleFinished)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Jungle");
                Console.ResetColor();
            }
            else Console.Write("Jungle");

            Console.Write("\n5. ");
            if (glacialFinished)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Glacial");
                Console.ResetColor();
            }
            else Console.Write("Glacial");

            Console.Write('\n');
        }

    }
}
