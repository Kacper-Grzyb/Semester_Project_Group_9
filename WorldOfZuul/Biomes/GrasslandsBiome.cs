using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security;
using static WorldOfZuul.GameManager;

namespace WorldOfZuul
{
    public class GrasslandsBiome : Biome
    {
        public Room startLocation;
        public Room currentGrasslandsRoom;
        public Room previousGrasslandsRoom;

        public GrasslandsBiome()
        {
            BiomeName = "Grasslands";

            Room GrasslandsStartRoom = new("Welcome to the grasslands biome!", "You find yourself standing in front of a vast grassland, stretching as far as the eye can see. Gentle breezes sway the tall grasses, creating a sea of green. The air is filled with soil scent and the native birds sing through the landscape, but the once refined grassland signals of destructive threats...", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom1 = new("Collaboration Corner:", "A central hub for various contributors to discuss and implement conservation strategies.", new List<Item> { new Item("Map", "Useful for navigation."), new Item("Camera", "Simple new photo camera.") }, new List<NPC> { new NPC("Michael", "Member of the Collaboration Corner") });
            Room? GrasslandsRoom2 = new("Eco-Unity Hub:", "A space designated for the people intrested in preserving the grassland biome.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom3 = new("Animal Aid", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { new Item("Tracking device", "Helpful to make your way back") }, new List<NPC> { });
            Room? GrasslandsRoom4 = new("Sustainable Basecamp", "An open area filled with discussions and ideas about making a change in the biome.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom5 = new(/* animal room 1 */ "Room 5", "message", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom6 = new(/* animal room 2 */ "Room 6", "message", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom7 = new(/*animal room 3 */ "Room 7", "message", new List<Item> { }, new List<NPC> { });

            GrasslandsStartRoom.SetExits(null, null, GrasslandsRoom1, null);
            GrasslandsRoom1.SetExits(GrasslandsStartRoom, GrasslandsRoom4, GrasslandsRoom3, GrasslandsRoom2);
            GrasslandsRoom2.SetExits(null, null, null, GrasslandsRoom1);
            GrasslandsRoom3.SetExits(GrasslandsRoom1, null, null, GrasslandsRoom5);
            GrasslandsRoom4.SetExits(null, null, null, GrasslandsRoom1);
            GrasslandsRoom5.SetExits(null, GrasslandsRoom3, GrasslandsRoom6, null);
            GrasslandsRoom6.SetExits(GrasslandsRoom5, GrasslandsRoom7, null, null);
            GrasslandsRoom7.SetExits(null, null, null, GrasslandsRoom6);


            startLocation = GrasslandsRoom1;
        }

       
        public void PlayGrasslands()
        {
            Parser parser = new();

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentPlayerRoom?.ShortDescription);
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
                        Console.WriteLine(currentPlayerRoom?.LongDescription);
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
        }

        public void AnimalRoom1()
        {
            Console.WriteLine(" We made it to the first [group of animals]! \n");
            Console.WriteLine(" Let's make sure we photograph *only* the endangered animals \n so we can help the scientists and researchers back at Collaboration Corner!\n");

            List<string> answers = new List<string> { "Praire Dogs(Left)", "Black-footed ferret (Center)", "Zebra(Right)" };
            int playerScore = 0;
            string playerAnswer;
            string choice;
            bool answersChecked = false;
            Console.WriteLine("Do you see any endangered species?");
            Console.Write(" -->");
            choice = Console.ReadLine();
            if (choice == "yes")
            {
                while (choice == "yes")
                {
                    Console.WriteLine(" Which animals should we photograph?");
                    //Console.WriteLine(answers[0]);
                    foreach (string x in answers)
                    {
                        Console.Write(" \n " + x);
                    }
                    Console.WriteLine();
                    Console.Write(" -->");
                    playerAnswer = Console.ReadLine();
                    if (string.Equals(playerAnswer, "Center", StringComparison.OrdinalIgnoreCase))
                    {
                        answers.Remove(answers[1]);
                        if (answers.Count == 1)
                        {
                            answersChecked = true;
                        }
                        Console.WriteLine("That's correct! Do you think we should take pictures of other animals?");
                        Console.WriteLine("[yes/no]");
                        Console.Write(" -->");

                        choice = Console.ReadLine();
                    }
                    else if (string.Equals(playerAnswer, "Left", StringComparison.OrdinalIgnoreCase))
                    {
                        answers.Remove(answers[0]);
                        if (answers.Count == 1)
                        {
                            answersChecked = true;
                        }
                        Console.WriteLine("That's correct! Do you think we should take pictures of other animals?");
                        Console.WriteLine("[yes/no]");
                        Console.Write(" -->");
                        choice = Console.ReadLine();
                    }
                    else if (string.Equals(playerAnswer, "Right", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("\n Acording to my information, zebras are not an endangered species.");
                        Console.WriteLine(" Let's give it another thought! \n");
                        /*Console.Write(" -->");
                        playerAnswer = Console.ReadLine();*/
                        choice = "yes";
                    }
                    else
                    {
                        Console.WriteLine(" I don't understand, let's try again. \n");
                    }
                }
                Console.WriteLine(" Great, we've made it out of the first group of animals! \n ");
                if (answersChecked != true)
                {
                    while (choice == "yes" || choice == "no")
                    {
                        Console.WriteLine(" Unfortunately, we might have missed some species, would you like to try again?\n ");
                        Console.Write(" -->");
                        choice = Console.ReadLine();
                        if (choice == "yes")
                        {
                            AnimalRoom1();
                        }
                        else if (choice == "no")
                        {
                            Console.WriteLine(" Alright, let's keep heading south, i believe i see something");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(" I don't understand, let's try again. \n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" Let's head south, I believe i see something.\n");
                }
            }
            else if (choice == "no")
            {
                Console.WriteLine(" That doesn't sound quite right. Let's check the information again.");
            }
            else
            {
                Console.WriteLine(" I don't understand, let's try again. \n");
            }
        }

        private void Move(string direction)
        {
            if (currentGrasslandsRoom?.Exits.ContainsKey(direction) == true)
            {
                previousGrasslandsRoom = currentGrasslandsRoom;
                currentGrasslandsRoom = currentGrasslandsRoom?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }


        private static void PrintHelp()
        {
            Console.WriteLine(" Here are your commands: ");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east' or 'west'");
            Console.WriteLine("Type 'back' to go to the previous room");
            Console.WriteLine("Type 'look' for more details");
            Console.WriteLine("Type 'take [item name]' to pick up an item");
            Console.WriteLine("Type 'use [item name]' to use an item");
            Console.WriteLine("Type 'drop [item name]' to drop an item from your inventory");
            Console.WriteLine("Type 'inv' to display your inventory");
            Console.WriteLine("Type 'quest' to see available quest in the room");
            Console.WriteLine("Type 'map' to display the biome map and your current location");
            Console.WriteLine("Type 'paths' to show directions in which you can go from your current location");
            Console.WriteLine("Type 'quit' to exit the game");
            Console.WriteLine("Type 'help' to print this message again");
        }

        public void AnimalRoom2()
        {
            Console.WriteLine(" We made it to the second [group of animals]! \n");
            Console.WriteLine(" Let's make sure we photograph *only* the endangered animals \n so we can help the scientists and researchers back at Collaboration Corner!\n");
            List<string> answers = new List<string> { "Elephant(Left)", "Ceetah (Center)", "Giraffe(Right)" };
            int playerScore = 0;
            string playerAnswer;

            bool answersChecked = false;

            AnimalRoom2Game();
        }

        public void AnimalRoom2Game()
        {
            Console.WriteLine("Do you see any endangered species?");
            Console.Write(" -->");
            string choice = "yes";
            choice = Console.ReadLine();
            if (choice == "yes")
            {
                Console.WriteLine("Are you sure? I think it's best to check the enangered species again.");
                //send player to animal info
            }
            else if (choice == "no")
            {
                Console.WriteLine(" That's right! No animals in danger here! Let's keep going east now, I think we're on the right path.");
                //move player to animal room 3
            }
            else
            {
                Console.WriteLine(" I don't understand, let's try again. \n");
                AnimalRoom2Game();
            }
        }

        public void AnimalRoom3()
        {
            Console.WriteLine(" We made it to the third [group of animals]! \n");
            Console.WriteLine(" Let's make sure we photograph *only* the endangered animals \n so we can help the scientists and researchers back at Collaboration Corner!\n");
            List<string> answers = new List<string> { "Numbat (Left)", "Snake (Center)", "Marsupial mouse (Right)" };
            int playerScore = 0;
            string playerAnswer;
            string choice;
            bool answersChecked = false;
            Console.WriteLine("Do you see any endangered species?");
            Console.Write(" -->");
            choice = Console.ReadLine();
            if (choice == "yes")
            {
                while (choice == "yes")
                {
                    Console.WriteLine(" Which animals should we photograph?");
                    foreach (string x in answers)
                    {
                        Console.Write(" \n " + x);
                    }
                    Console.WriteLine();
                    Console.Write(" -->");
                    playerAnswer = Console.ReadLine();
                    if (string.Equals(playerAnswer, "Center", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("\n Acording to my information, snakes are not an endangered species.");
                        Console.WriteLine(" Let's give it another thought! \n");
                        Console.WriteLine("[yes/no]");
                        Console.Write(" -->");

                        choice = Console.ReadLine();
                    }
                    else if (string.Equals(playerAnswer, "Left", StringComparison.OrdinalIgnoreCase))
                    {
                        answers.Remove(answers[0]);
                        if (answers.Count == 1)
                        {
                            answersChecked = true;
                        }
                        Console.WriteLine("That's correct! Do you think we should take pictures of other animals?");
                        Console.WriteLine("[yes/no]");
                        Console.Write(" -->");
                        choice = Console.ReadLine();
                    }
                    else if (string.Equals(playerAnswer, "Right", StringComparison.OrdinalIgnoreCase))
                    {
                        answers.Remove(answers[1]);
                        if (answers.Count == 1)
                        {
                            answersChecked = true;
                        }
                        Console.WriteLine("That's correct! Do you think we should take pictures of other animals?");

                        choice = "yes";
                    }
                    else
                    {
                        Console.WriteLine(" I don't understand, let's try again. \n");
                    }
                }
                Console.WriteLine(" Great, we've made it out of the first group of animals! \n ");
                if (answersChecked != true)
                {
                    while (choice == "yes" || choice == "no")
                    {
                        Console.WriteLine(" Unfortunately, we might have missed some species, would you like to try again?\n ");
                        Console.Write(" -->");
                        choice = Console.ReadLine();
                        if (choice == "yes")
                        {
                            AnimalRoom3();
                        }
                        else if (choice == "no")
                        {
                            Console.WriteLine(" Perfect then, let's use the *tracking device* and return to the collaboration corner!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(" I don't understand, let's try again. \n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" Perfect then, let's use the *tracking device* and return to the collaboration corner!\n");
                }
            }
            else if (choice == "no")
            {
                Console.WriteLine(" That doesn't sound quite right. Let's check the information again.");
            }
            else
            {
                Console.WriteLine(" I don't understand, let's try again. \n");
            }
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the Grasslands Biome!");
            Console.WriteLine("You find yourself standing in front of a vast grassland, stretching as far as the eye can see. Gentle breezes sway the tall grasses, creating a sea of green. The air is filled with soil scent and the native birds sing through the landscape, but the once refined grassland signals of destructive threats...\n");
            PrintHelp();
            Console.WriteLine();
        }
    }
}