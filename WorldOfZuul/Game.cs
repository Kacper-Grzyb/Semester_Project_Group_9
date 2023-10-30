using System.Linq.Expressions;
using System.Security;

namespace WorldOfZuul
{
    public class Game
    {
        Gamemanager player = new Gamemanager();
        public Room? currentRoom;
        private Room? previousRoom;

        public Game()
        {
            ChooseWorld();
        }
        public void ChooseWorld()
        {
            Console.Write("Enter the name of your character: ");
            player.playerName = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Choose a world you'll be starting in");
            Console.WriteLine(" 1.Grassland, 2. Forest,  3. Mountain 4.Jungle 5. Glacial");
            string? userInput = Console.ReadLine();

            // Direct the user to the appropriate room
            if (userInput?.ToLower() == "grassland" && userInput == "grassland")
            {

                Console.WriteLine("You are located in a grasslands.");

            }
            else if (userInput == "forest")
            {

                Console.WriteLine("You are located in a grasslands.");

            }
            else if (userInput == "mountain")
            {
                Console.WriteLine("You are located in a grasslands.");

            }
            else if (userInput == "jungle")
            {
                Console.WriteLine("You are located in a grasslands.");
                CreateJungle();

            }
            else if (userInput == "glacial")
            {
                Console.WriteLine("You are located in a grasslands.");
            }

        }
        public void CreateJungle()
        {

            Room? location1 = new("Sector 1", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<string> {});
            Room? location2 = new("Sector 2", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<string> { });
            Room? location3 = new("Sector 3", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<string> { });
            Room? location4 = new("sector 4", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.", new List<string> { });
            Room? location5 = new("Sector 5", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<string> { "flashlight, map, trap" });
            Room? location6 = new("Sector 6 ", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<string> { });
            Room? location7 = new("Sector 7", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<string> { });
            Room? location8 = new("Sector 8", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<string> { });
            Room? location9 = new("Sector 9", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<string> { });

            JungleQuest stopThePoachers = new JungleQuest("Stop the Poachers","asdhfalsdkfj", false, 5);   

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
        // private void CreateRooms()
        // {

        //     Room? outside = new("Outside", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
        //     Room? theatre = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
        //     Room? pub = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
        //     Room? lab = new("Lab", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");
        //     Room? office = new("Office", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

        //     outside.SetExits(null, theatre, lab, pub); // North, East, South, West

        //     theatre.SetExit("west", outside);

        //     pub.SetExit("east", outside);

        //     lab.SetExits(outside, office, null, null);

        //     office.SetExit("west", lab);

        //     currentRoom = outside;
        // }

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
                        Console.WriteLine(currentRoom?.LongDescription);

                        if (currentRoom?.Items != null && currentRoom.Items.Count > 0)
                        {

                            Console.WriteLine("On the floor, you can see: " + string.Join(", ", currentRoom.Items));
                            Console.WriteLine("do you want to pick up the item? yes/no");
                            string yesNo = Console.ReadLine() ?? string.Empty;
                            if (yesNo.ToLower() == "yes")
                            {
                                Console.WriteLine("You can pick up Item by typing its name:");
                                string itemToPick = Console.ReadLine() ?? string.Empty;

                                if (currentRoom != null && currentRoom.Items != null && currentRoom.Items.Contains(itemToPick))
                                {
                                    currentRoom.Items.Remove(itemToPick);
                                    Console.WriteLine($"You picked up the {itemToPick}.");
                                }
                                else
                                {
                                    Console.WriteLine($"The {itemToPick} is not here.");
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("there are no items left in this room");
                        }
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
                        break;

                    case "drop":
                        Console.WriteLine("which item do you want to drop?");
                        break;
                    case "quest":
                        
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
    }
}
