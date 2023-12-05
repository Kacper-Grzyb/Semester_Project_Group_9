using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;


namespace WorldOfZuul
{
    public sealed class JungleBiome : Biome
    {
        public JungleBiome()
        {
            JungleBiome.BiomeType = Biomes.Jungle;
            JungleBiome.PointsToWin = 100;
            BiomeName = "Jungle";
            

            Room? location1 = new("Sector 1", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC>{});

            Room? location2 = new("Sector 2", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<Item> { }, new List<NPC>{});

            Room? location3 = new("Sector 3", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<Item> { }, new List<NPC>{});

            Room? location4 = new("sector 4", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.", new List<Item> { }, new List<NPC>{});

            Room? location5 = new("Sector 5", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<Item> { new Item("Flashlight", "A way to light your path"),
            new Item("map","Useful for navigation"), new Item("trap", "Can be used against enemies") }, new List<NPC>{});

            Room? location6 = new("Sector 6 ", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<Item> { }, new List<NPC>{});

            Room? location7 = new("Sector 7", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC>{});

            Room? location8 = new("Sector 8", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<Item> { }, new List<NPC>{});

            Room? location9 = new("Sector 9", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<Item> { }, new List<NPC>{});

            var stopPoachers = new List<QuestObjective>
            {
             new QuestObjective("Destroy 6 traps that poachers setup in Sector 9","Map")

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

            startLocation = location5;

            GameManager.Inventory = new Inventory();

            rooms = new List<Room> { location1, location2, location3, location4, location5, location6, location7, location8, location9 };
            northmostRoom = location1;
            
            Riddler riddler = new Riddler("riddler","Master of riddles and puzzles.");
            location1.AddNpcToRoom(riddler);
            


            Player.mapHeight = 3;
            Player.mapWidth = 3;
            Player.X = 1;
            Player.Y = 1;
        }
        public override void DisplayMap()
        {
            // this is a new display map function, if you prefer the old one you can uncomment it
            base.DisplayMap();
            /*
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
            */
        }
        public override void checkForAvailableObjectives()
        {
            if(GameManager.IsActiveQuest == false)
            {   
                return;
            }
            if(GameManager.ActiveQuest?.QuestName == "Disable traps" && GameManager.currentPlayerRoom?.ShortDescription == "Sector 9")
            {  
              quizTrap();
            }
        }
        public void quizTrap()
        {
            if (GameManager.ActiveQuest?.QuestName == "Disable traps")
            {
                int trapsDisabled = 0;

                string? inputAnswer;
                Console.WriteLine("You have to disable the traps set by the poachers, you can disable them by answering 5 questions.");
                Console.WriteLine("Each question will disable one of the traps, try to disable as many as possible.");
                Console.WriteLine("Good luck! \n");

                Console.WriteLine("\x1b[1mQuestion 1:\x1b: Which of the following is NOT a benefit provided by jungle ecosystems?");
                Console.WriteLine("A)They are habitats for diverse species.");
                Console.WriteLine("B)They help stabilize the world's climate.");
                Console.WriteLine("C) They are the primary source of freshwater.");
                Console.WriteLine("D) They provide the main ingredients for processed foods.\n");

                inputAnswer = Console.ReadLine();
                if (inputAnswer?.ToLower() != "d")
                {
                    Console.WriteLine("Wrong!");
                    Console.WriteLine($"Correct asnwer: D ");
                    
                    
                }
                else
                {
                    Console.WriteLine($"Correct asnwer!");
                    
                    trapsDisabled++;
                }

                Console.WriteLine("\x1b1mQuestion 2: Deforestation in jungles is primarily driven by the need for land for agriculture and logging operations.\x1b[0m");
                Console.WriteLine("True");
                Console.WriteLine("False");

                inputAnswer = Console.ReadLine();
                if (inputAnswer?.ToLower() != "true")
                {
                    Console.WriteLine("Wrong!");
                    Console.WriteLine($"Correct asnwer: true ");
                    
                    
                }
                else
                {
                    Console.WriteLine($"Correct asnwer!");
                    
                    trapsDisabled++;
                }

                Console.WriteLine("\x1b[1mQuestion 3:\x1b:What does the term ''poaching'' refer to? ");
                Console.WriteLine("A) The legal hunting of wild animals.");
                Console.WriteLine("B) The illegal hunting, capturing, and killing of wild animals.");
                Console.WriteLine("C) A method of cooking eggs.");
                Console.WriteLine("D) A technique used in wildlife photography.");

                inputAnswer = Console.ReadLine();
                if (inputAnswer?.ToLower() != "b")
                {
                    Console.WriteLine("Wrong!");
                    Console.WriteLine($"Correct asnwer: B ");
                    
                }
                else
                {
                    Console.WriteLine($"Correct asnwer!");
                    
                    trapsDisabled++;
                }

                Console.WriteLine("\x1b[1mQuestion 4:\x1b Which of these jungle animals is often poached for its valuable parts and has become endangered as a result? ");
                Console.WriteLine("A) Sloth");
                Console.WriteLine("B) Toucan");
                Console.WriteLine("C) Elephant");
                Console.WriteLine("D) Iguana");

                inputAnswer = Console.ReadLine();
                if (inputAnswer?.ToLower() != "C")
                {
                    Console.WriteLine("Wrong!");
                    Console.WriteLine($"Correct asnwer: C ");
                    
                }
                else
                {
                    Console.WriteLine($"Correct asnwer!");
                    
                    trapsDisabled++;
                }
                Console.WriteLine("\x1b[1mQuestion 5:\x1b Establishing protected areas is a key strategy to combat poaching in jungles. ");
                Console.WriteLine("True");
                Console.WriteLine("False");
                inputAnswer = Console.ReadLine();

                if(inputAnswer?.ToLower() != "true")
                {
                    Console.WriteLine("Wrong!");
                    Console.WriteLine($"Correct asnwer: true ");
                    
                }else{
                    Console.WriteLine($"Correct asnwer!");
                    
                    trapsDisabled++;
                }
                GameManager.ActiveQuest.IsCompleted = true;
                GameManager.ActiveQuest = null;
                GameManager.IsActiveQuest = false; 

                if(trapsDisabled == 0)
                {
                    Console.WriteLine("You didn't disable any traps, because of that the poachers caught you and endangered species.");
                    Console.WriteLine("You failed to save the jungle");
                    Console.WriteLine("Game over, Score: " + GameManager.score);
                    Environment.Exit(0);
                }
                else if(trapsDisabled == 1)
                {
                    Console.WriteLine("You disabled 1 trap, not a great job but you did atleast something but you lose points for not doing more.");
                    Console.WriteLine("You've lost 10 points for that.");
                }
                else if(trapsDisabled == 2)
                {
                    Console.WriteLine("You disabled 2 traps, you did an alright job but you lose points for not doing more.");
                    Console.WriteLine("You've lost 5 points for that.");
                }
                else if(trapsDisabled == 3)
                {
                    Console.WriteLine("You disabled 3 traps, good job, you saved half of the animals that would be other wise killed.");
                    Console.WriteLine("You've gained 5 points for that.");
                }
                else if(trapsDisabled == 4)
                {
                    Console.WriteLine("You disabled 4 traps, really good job, you could have done better but you still did a good job.");
                    Console.WriteLine("You've gained 10 points for that.");
                }
                else if(trapsDisabled == 5)
                {
                    Console.WriteLine("You disabled 5 traps, good job!");
                    Console.WriteLine("You've gained 15 points for that.");
                }
                else if(trapsDisabled == 6)
                {
                    Console.WriteLine("You disabled all the traps, good job!");
                    Console.WriteLine("You've gained 20 points that.");
                }

            }
        }
    }
}