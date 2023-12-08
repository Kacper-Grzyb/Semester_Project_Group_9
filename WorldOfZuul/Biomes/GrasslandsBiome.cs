
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
        internal List<Room> grasslandsRooms = new List<Room>();
        public static int completedRooms = 0;
        public GrasslandsBiome()
        {
            BiomeName = "Grasslands";

            Room GrasslandsStartRoom = new("Welcome to the grasslands biome!", "You find yourself standing in front of a vast grassland, stretching as far as the eye can see. Gentle breezes sway the tall grasses, creating a sea of green. The air is filled with soil scent and the native birds sing through the landscape, but the once refined grassland signals of destructive threats...", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom1 = new("Collaboration Corner", "A central hub for various contributors to discuss and implement conservation strategies.", new List<Item> { new Map("Map", "Useful for navigation."), new Item("Camera", "Simple new photo camera.") }, new List<NPC> { new NPC("Michael", "Member of the Collaboration Corner") });
            Room? GrasslandsRoom2 = new("Eco-Unity Hub:", "A space designated for the people intrested in preserving the grassland biome. \n\n This area is crowded with waste that's dangerous for the environment. Type 'Recycle' to give a helping hand.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom3 = new("Animal Aid", "You find yourself in an huge place and someone is here to help you.", new List<Item> { new Item("TrackingDevice", "Helpful to make your way back") }, new List<NPC> { new NPC("Photograph", "Hello and welcome to Animal Aid: \n\n Our job here will be to take pictures of the engangered species of animas and send said photos back to Colaboration Corner! \n\n Among these species we can count the nubat, the marsupial mouse, praire dogs, black-footed ferrets and so much more. \n\n  We better get going because we don't want to miss any of them. \n\n Let's take the *tracking device* so we'll know our way back and start going West, I believe we might find something there...") });
            grasslandsRooms.Add(GrasslandsRoom1);
            Room? GrasslandsRoom4 = new("Sustainable Basecamp", "An open area filled with discussions and ideas about making a change in the biome. Here is Mark, he will test your knowledge on sustainability. \n\n Answer Mark's questions correctly to move forward. (sustainable-quiz)", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom5 = new("Open Field", "You see some animals walking around peacefully. Take a closer look by typing (close1).", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom6 = new("Migration Camp", "You can notice some trees missing their leaves and a small pond. Some animals are happily drinking some water. The *Photograph* will offer some assistance (close2)", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom7 = new("Rest Place", "You can see some animals resting at the base of a tree, enjoying the shadow in this canicular day. (close3)", new List<Item> { }, new List<NPC> { });
            // North East South West
            GrasslandsStartRoom.SetExits(null, null, GrasslandsRoom1, null);
            GrasslandsRoom1.SetExits(GrasslandsStartRoom, GrasslandsRoom2, GrasslandsRoom3, GrasslandsRoom4);
            GrasslandsRoom2.SetExits(null, null, null, GrasslandsRoom1);
            GrasslandsRoom3.SetExits(GrasslandsRoom1, null, null, GrasslandsRoom5);
            GrasslandsRoom4.SetExits(null, GrasslandsRoom1, null, null);
            GrasslandsRoom5.SetExits(null, GrasslandsRoom3, GrasslandsRoom6, null);
            GrasslandsRoom6.SetExits(GrasslandsRoom5, GrasslandsRoom7, null, null);
            GrasslandsRoom7.SetExits(null, null, null, GrasslandsRoom6);


            startLocation = GrasslandsStartRoom;
            northmostRoom = GrasslandsStartRoom;
            rooms = new List<Room>() { GrasslandsStartRoom, GrasslandsRoom1, GrasslandsRoom2, GrasslandsRoom3, GrasslandsRoom4, GrasslandsRoom5, GrasslandsRoom6, GrasslandsRoom7 };
        }

        public void AnimalRoom1()
        {
            Console.WriteLine(" We made it to the first group of animals! \n");
            Console.WriteLine(" Let's make sure we photograph only the endangered animals so we can help the scientists and researchers back at Collaboration Corner!\n");

            List<string> answers = new List<string> { "Praire Dogs (Left)", "Black-footed ferret (Center)", "Zebra (Right)" };
            int playerScore = 0;
            string playerAnswer;
            string choice;
            bool answersChecked = false;
            foreach (string x in answers)
            {
                Console.Write(" \n " + x);
            }
            Console.WriteLine("\n\n Do you see any endangered species?  [yes/no]");
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
                            break;
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
                            break;
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

        public Room GetRoomByName(string roomName)
        {
            return grasslandsRooms.FirstOrDefault(room => room.ShortDescription.Equals(roomName, StringComparison.OrdinalIgnoreCase));
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
            Console.WriteLine(" We made it to the second group of animals! \n");
            Console.WriteLine(" Let's make sure we photograph only the endangered animals \n so we can help the scientists and researchers back at Collaboration Corner!\n");
            List<string> answers = new List<string> { "Elephant (Left)", "Ceetah (Center)", "Giraffe (Right)" };
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
                Console.WriteLine("That's right! No animals in danger here! Let's keep going east now, I think we're on the right path.");
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
            Console.WriteLine(" We made it to the third group of animals! \n");
            Console.WriteLine(" Let's make sure we photograph only the endangered animals \n so we can help the scientists and researchers back at Collaboration Corner!\n");
            List<string> answers = new List<string> { "Numbat (Left)", "Snake (Center)", "Marsupial mouse (Right)" };
            int playerScore = 0;
            string playerAnswer;
            string choice;
            bool answersChecked = false;
            foreach (string x in answers)
            {
                Console.Write(" \n " + x);
            }
            Console.WriteLine();
            Console.WriteLine("Do you see any endangered species? [yes/no]");
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
                        Console.WriteLine("Do you want to photograph other animals? [yes/no]");
                        choice = Console.ReadLine();
                    }
                    else if (string.Equals(playerAnswer, "Left", StringComparison.OrdinalIgnoreCase))
                    {
                        answers.Remove(answers[0]);
                        if (answers.Count == 1)
                        {
                            answersChecked = true;
                            break;
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
                            break;
                        }
                        Console.WriteLine("That's correct! Do you think we should take pictures of other animals?");

                        choice = "yes";
                    }
                    else
                    {
                        Console.WriteLine(" I don't understand, let's try again. \n");
                    }
                }
                Console.WriteLine(" Great, we've made it out of the third group of animals! \n ");
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
                            Console.WriteLine(" Perfect then, let's use the tracking device and return to the collaboration corner!");
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
                    Console.WriteLine(" Perfect then, let's use the tracking device and return to the collaboration corner!\n");
                    Console.WriteLine(" Now type 'trackingdevice' tp return the collaboration corner!\n");
                    string answer = Console.ReadLine();

                    if (answer == "trackingdevice")
                    {
                        // currentPlayerRoom = GrasslandsBiome.GetRoomByName("Collaboration Corner");
                    }
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
            completedRooms++;
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the Grasslands Biome!");
            Console.WriteLine("You find yourself standing in front of a vast grassland, stretching as far as the eye can see. \nGentle breezes sway the tall grasses, creating a sea of green. The air is filled with soil scent and the native birds sing through the landscape, but the once refined grassland signals of destructive threats...\n");
            Console.WriteLine("Maybe it would be a good idea to explore around.\n");
            PrintHelp();
            Console.WriteLine();
        }



        public override void CheckWinCondition()
        {
            if (GrasslandsBiome.completedRooms < 3)
            {
                Console.WriteLine(" It seems like you haven't completed all the locations, you need to complete all the missions before leaving!");
            }
            else
            {
                Console.WriteLine("You have saved the grasslands! Great job!");
                grasslandsFinished = true;
            }

        }

    }

    public class TrashSort
    {
        List<string> Fertilizer;
        List<string> Glass;
        List<string> Metal;
        List<string> Paper;
        List<string> Plastic;
        List<List<string>> Recyclables;
        public void Refill()
        {
            Plastic = new List<string> { "Plastic lid", "Plastic bag", "Water bottle", "Juice bottle", "Straw", "Plastic plate" };
            Paper = new List<string> { "Mail", "Paper cup", "Milk carton", "Magazine", "Egg carton", "Tissue" };
            Metal = new List<string> { "Laptop", "Soup can", "Metal wiring", "Metal storage box", "Aluminum foil", "Broken sharp metal" };
            Glass = new List<string> { "Wine bottle", "Jar", "Drinking glass", "Vase", "Window" };
            Fertilizer = new List<string> { "Rotten vegetables", "Eggshell", "Banana peel", "Potato peel" };
            Recyclables = new List<List<string>>();
            Recyclables.Clear();
            Recyclables.Add(Fertilizer);
            Recyclables.Add(Glass);
            Recyclables.Add(Metal);
            Recyclables.Add(Paper);
            Recyclables.Add(Plastic);
        }

        public TrashSort()
        {
            Refill();
            Recyclables.Clear();
            Recyclables.Add(Fertilizer);
            Recyclables.Add(Glass);
            Recyclables.Add(Metal);
            Recyclables.Add(Paper);
            Recyclables.Add(Plastic);
        }

        public void Game()
        {
            Console.Clear();
            Console.WriteLine("Game: Sort out the trash:\nYou are going to be handed various wasteful products and the job is to sort them out into their right recycling categories.\n");
            Console.WriteLine("Code list:\n1. Fertilizer\n2. Glass\n3. Metal\n4. Paper\n5. Plastic\n\n");

            int counter = 0;
            Random randomizer = new Random();
            int listNum = 0;
            int itemNum = 0;

            Parser parser = new Parser();

            string choice;
            while (counter < 10)
            {
                listNum = randomizer.Next(0, Recyclables.Count);
                if (Recyclables[listNum].Count == 0)
                {
                    continue;
                }
                else
                {
                    bool choiceMade = false;
                    itemNum = randomizer.Next(0, Recyclables[listNum].Count);
                    Console.WriteLine("Score:" + counter);
                    Console.WriteLine("Item:" + Recyclables[listNum][itemNum] + "(choose category: 1/2/3/4/5)");

                    while (!choiceMade)
                    {
                        choice = Console.ReadLine();
                        if (string.IsNullOrEmpty(choice))
                        {
                            Console.WriteLine("Please enter a command.");
                            continue;
                        }

                        Command? command = parser.GetCommand(choice);

                        if (command == null)
                        {
                            Console.WriteLine("I don't know that command.");
                            continue;
                        }

                        if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5")
                        {
                            if (choice == Convert.ToString(listNum + 1))
                            {
                                Recyclables[listNum].RemoveAt(itemNum);
                                counter++;
                                choiceMade = true;
                            }
                            else
                            {
                                Console.WriteLine("Wrong! The item belongs to category" + listNum + 1 + ".\n\n");
                                Refill();
                                counter = 0;
                                choiceMade = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("I don't know that command.");
                            continue;
                        }
                    }

                }
            }
            Console.WriteLine("Congratulatons! Your help was much needed! Let's move on!");
            GrasslandsBiome.completedRooms++;
        }
    }

    public class SustainabilityGame
    {
        public void SustainableQuiz()
        {
            int correctAnsw = 0;
            Random random = new Random();
            List<SustainabilityQuestions> sQuestions = GenerateQuestions();

            while (correctAnsw < 7)
            {
                int index = random.Next(sQuestions.Count);
                SustainabilityQuestions currentQuestion = sQuestions[index];
                sQuestions.RemoveAt(index);

                if (sQuestions.Count == 0)
                {
                    sQuestions = GenerateQuestions();
                }

                Console.WriteLine(currentQuestion.Text);
                string userAnswer = Console.ReadLine();

                if (userAnswer.ToLower() == currentQuestion.Answer.ToLower())
                {
                    correctAnsw++;
                    Console.WriteLine("Correct! You answered " + correctAnsw + " questions correctly!");
                }
                else
                {
                    Console.WriteLine("Wrong answer. You answered " + correctAnsw + " questions correctly!");
                }

                if (correctAnsw == 7)
                {
                    Console.WriteLine("Congratulations! You've reached the necessary amount of correct answers.");
                    return;
                }
                GrasslandsBiome.completedRooms++;
            }

        }

        static List<SustainabilityQuestions> GenerateQuestions()
        {
            List<SustainabilityQuestions> sQuestions = new List<SustainabilityQuestions>
            {
                new SustainabilityQuestions ("How can communities reduce conflicts between humans and endangered species in grasslands? \na) Encourage habitat destruction and fragmentation\nb) Promote activities that disturb wildlife\nc) Implement measures to protect natural habitats and minimize human impact\nd) Ignore the presence of endangered species in grassland areas", "c"),
                new SustainabilityQuestions ("What is a crucial step in protecting endangered species in the grasslands?\na) Encourage hunting and poaching of rare species\nb) Preserve and restore natural habitats for endangered species\nc) Promote the trade of products made from endangered species\nd) Ignore the role of human activities in endangering species", "b"),
                new SustainabilityQuestions ("How can people contribute to sustainable consumption?\na) Buy unnecessary products\nb) Practice planned obsolescence of goods\nc) Implement recycling and reduce waste generation\nd) Encourage excessive resource consumption", "c"),
                new SustainabilityQuestions ("What is a key effort to protect diverse ecosystems? \na) Support activities leading to deforestation\nb) Promote industrial pollution\nc) Preserve and enhance biodiversity on land\nd) Ignore conservation of natural habitats","c"),
                new SustainabilityQuestions ("How can communities contribute to sustainable water use?\na) Increase water pollution\nb) Ensure unequal access to clean water\nc) Achieve universal access to clean water and sanitation\nd) Ignore water conservation efforts","c"),
                new SustainabilityQuestions ("What can people do to make less trash? \na) Use more single-use plastics\nb) Use lots of packaging\nc) Recycle and use fewer things that get thrown away\nd) Use more disposable products","c"),
                new SustainabilityQuestions ("How can individuals help the environment? \na) Cut down more trees for space\nb) Avoid planting any new trees\nc) Plant more trees to create green spaces\nd) Use paper excessively","c"),
                new SustainabilityQuestions ("How can people enjoy nature without harming it?\na) Leave trash behind when visiting natural areas\nb) Stay on designated trails and respect wildlife\nc) Collect plants and animals as souvenirs\nd) Ignore signs about protecting the environment","b"),
                new SustainabilityQuestions ("What is a positive action to help protect the grasslands biome? \na) Encourage large-scale urbanization of grasslands\nb) Support excessive use of chemical fertilizers on grasslands\nc) Preserve natural grasslands and avoid unnecessary development\nd) Ignore the impact of agriculture on grassland ecosystems","c"),
                new SustainabilityQuestions ("What is a responsible approach to managing fires in grassland ecosystems? \na) Avoid controlled burns for vegetation management\nb) Promote uncontrolled wildfires in grasslands\nc) Use fire as a tool for maintaining a healthy grassland ecosystem\nd) Ignore the role of fire in grassland ecology","c")
            };

            return sQuestions;
        }
    }

    public class SustainabilityQuestions
    {
        public string Text { get; }
        public string Answer { get; }

        public SustainabilityQuestions(string text, string answer)
        {
            Text = text;
            Answer = answer;
        }
    }
}