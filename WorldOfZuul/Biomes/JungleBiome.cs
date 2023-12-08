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
            

            Room? location1 = new("Sector 1", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { new Item("nickle", "Can be used to distract enemies") }, new List<NPC>{});

            Room? location2 = new("Sector 2", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<Item> { new Item("bat", "Can be used to beat up enemies") }, new List<NPC>{});

            Room? location3 = new("Sector 3", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<Item> { }, new List<NPC>{});

            Room? location4 = new("sector 4", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.", new List<Item> { }, new List<NPC>{});

            Room? location5 = new("Sector 5", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<Item> { new Item("Flashlight", "A way to light your path"),
            new Map("map","Useful for navigation"), new Item("trap", "Can be used against enemies") }, new List<NPC>{});

            Room? location6 = new("Sector 6", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<Item> { }, new List<NPC>{});

            Room? location7 = new("Sector 7", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC>{});

            Room? location8 = new("Sector 8", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<Item> { }, new List<NPC>{});

            Room? location9 = new("Sector 9", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<Item> { }, new List<NPC>{});

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
            
            

            //--------------------------------------Quests----------------------------------------------
            
            var stopPoachers = new List<QuestObjective>
            {
             new QuestObjective("Destroy 6 traps that poachers setup in Sector 9","Map")

            };
            Quest Poachers = new Quest("Disable traps", "fing all the traps that poachers setup in Sector 9", false, false, stopPoachers);
            
            var findEvidence = new List<QuestObjective>
            {
             new QuestObjective("Find the evidence that poachers left in Sector 3","Map")

            };
            Quest Evidence = new Quest("Find evidence", "Find the evidence that poachers left in Sector 3", false, false, findEvidence);
            var destroyBaseCamp = new List<QuestObjective>
            {
             new QuestObjective("Destroy the base camp in Sector 3","Map")

            };
            Quest DestroyBaseCamp = new Quest("Destroy base camp", "Destroy the base camp in Sector 3", false, false, destroyBaseCamp);

            //--------------------------------------NPCs------------------------------------------------
            Riddler riddler = new Riddler("Riddler","Master of riddles and puzzles.");
            QuestGiver questGiver = new QuestGiver("Mitch","A local villager.");
            
            location5.AddQuest(Poachers);
            location5.AddNpcToRoom(questGiver);
            location1.AddNpcToRoom(riddler);
            
            questGiver.AddQuestToNPC(Evidence);
            questGiver.AddQuestToNPC(DestroyBaseCamp);


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
            if(GameManager.ActiveQuest?.QuestName == "Find evidence" && GameManager.currentPlayerRoom?.ShortDescription == "Sector 3")
            {  
              Evidence();
            }
            if(GameManager.ActiveQuest?.QuestName == "Destroy base camp" && GameManager.currentPlayerRoom?.ShortDescription == "Sector 3")
            {  
              DestroyBaseCamp();
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

                Console.WriteLine("Question 1: Which of the following is NOT a benefit provided by jungle ecosystems?");
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

                Console.WriteLine("Question 2: Deforestation in jungles is primarily driven by the need for land for agriculture and logging operations.\x1b[0m");
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

                Console.WriteLine("Question 3: What does the term ''poaching'' refer to? ");
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

                Console.WriteLine("Question 4: Which of these jungle animals is often poached for its valuable parts and has become endangered as a result? ");
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
                Console.WriteLine("Question 5: Establishing protected areas is a key strategy to combat poaching in jungles. ");
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
                    FailedQuest();
                }
                else if(trapsDisabled == 1)
                {
                    Console.WriteLine("You disabled 1 trap, not a great job but you did atleast something but you lose points for not doing more.");
                    Console.WriteLine("You've lost 10 points for that.");
                    GameManager.score -= 10;
                }
                else if(trapsDisabled == 2)
                {
                    Console.WriteLine("You disabled 2 traps, you did an alright job but you lose points for not doing more.");
                    Console.WriteLine("You've lost 5 points for that.");
                    GameManager.score -= 5;
                }
                else if(trapsDisabled == 3)
                {
                    Console.WriteLine("You disabled 3 traps, good job, you saved half of the animals that would be other wise killed.");
                    Console.WriteLine("You've gained 5 points for that.");
                    GameManager.score += 5;
                }
                else if(trapsDisabled == 4)
                {
                    Console.WriteLine("You disabled 4 traps, really good job, you could have done better but you still did a good job.");
                    Console.WriteLine("You've gained 10 points for that.");
                    GameManager.score += 10;
                }
                else if(trapsDisabled == 5)
                {
                    Console.WriteLine("You disabled 5 traps, amazing job, you saved almost all the animals that would be other wise killed.");
                    Console.WriteLine("You've gained 15 points for that.");
                    GameManager.score += 15;
                }
                else if(trapsDisabled == 6)
                {
                    Console.WriteLine("You disabled all the traps, you stop the poachers from theirs evil plan, and saved all the animals.");
                    Console.WriteLine("You've gained 20 points that.");
                    GameManager.score += 20;
                }
                CheckWinCondition();

            }
        }

        public void Evidence(){
            if (GameManager.ActiveQuest?.QuestName == "Find evidence")
            {
                Console.WriteLine("You stumbled across a poachers camp, you have to search the camp for some evidance (search), or you can turn around and leave (leave).");
                string? ans  = Console.ReadLine();
                bool decision = false;
                bool decision2 = false;
                if (ans == "search"){
                    Console.WriteLine("You proceeded to search an enemy territory be careful to not get caught.");
                    Console.WriteLine("You see 2 buildings in front of you, which one do you want to search first? (1,2)");
                    int ans2 = Convert.ToInt32(Console.ReadLine());
                    while(!decision)
                    {
                        if(ans2 == 1){
                            decision = true;
                            while(decision2 == false)
                            {
                                Console.WriteLine("you chose to proceed to the first building, you see a guard in front of the building, you can try to sneak past him (sneak), or you can try to fight him (fight) or leave (quit).");
                                string? ans3 = Console.ReadLine();
                                if(ans3 == "sneak")
                                {
                                    if (GameManager.Inventory != null && GameManager.Inventory.items != null && GameManager.Inventory.items.Any(item => item.name.ToLower() == "coin")){
                                        Console.WriteLine("You have used a coin that you got to distract the guard, you can now proceed to search the building.");
                                        Console.WriteLine("You found some incriminating evidence against the pouchers, you can now leave the camp");
                                        decision2 = true;
                                        GameManager.ActiveQuest.IsCompleted = true;
                                    }
                                    else{
                                        Console.WriteLine("You don't have anything to distract the guard, you can't sneak past him, you have to fight him.");
                                        Console.WriteLine("Try to come back when you have something to distract the guard with. Or you can try to fight him");                             
                                        
                                    }
                                }
                                else if(ans3 == "fight"){
                                    if (GameManager.Inventory != null && GameManager.Inventory.items != null && GameManager.Inventory.items.Any(item => item.name.ToLower() == "bat"))
                                    {
                                        Console.WriteLine("You have a bat, you can use it to fight the guard.");
                                        Console.WriteLine("You can try to hit him in the head (head), or you can try to hit him in the legs (legs).");
                                        string? ans4 = Console.ReadLine();
                                        if(ans4 == "head"){
                                            Console.WriteLine("You hit the guard in the head, he fell to the ground, you can now proceed to search the building.");
                                            Console.WriteLine("You found some incriminating evidence against the pouchers, you can now leave the camp");
                                            decision2 = true;
                                            GameManager.ActiveQuest.IsCompleted = true;
                                        }
                                        else if(ans4 == "legs"){
                                            Console.WriteLine("You hit the guard in the legs, he fell to the ground, but he is still conscious, and called reinforcmants, you ran away");
                                            FailedQuest();
                                        }
                                    }
                                    else{
                                        Console.WriteLine("You don't have a bat, you can't fight the guard, you have to sneak past him.");
                                        
                                    }
                                }else{
                                    return;
                                }
                        }   }
                        else if(ans2 == 2){
                            Console.WriteLine("You chose to proceed to the second building");
                            Console.WriteLine("the building is competly empty");
                        }
                    }
                    
                }
                else
                {
                    Console.WriteLine("You turned around and left the camp.");
                    FailedQuest();
                    return;
                }
            }
        }
        public void DestroyBaseCamp()
        {
            if(GameManager.ActiveQuest?.QuestName == "Destroy base camp" && GameManager.Inventory != null && GameManager.Inventory.items != null && GameManager.Inventory.items.Any(item => item.name.ToLower() == "dynamite")){
                Console.WriteLine("You came back for revenge, you saw what those poachers are going to do, and you have to stop them.");
                Console.WriteLine("You sneak to the back of the main building, and place your dynimate");  
                GameManager.Inventory?.DropItem("dynamite");
                int ans = Convert.ToInt32(Console.ReadLine());
                if(GameManager.Inventory != null && GameManager.Inventory.items != null && GameManager.Inventory.items.Any(item => item.name.ToLower() == "matches"))
                {
                    
                    Console.WriteLine("You have matches, you can use them to light the dynimate");
                    Console.WriteLine("You've lit the dynamite and run for cover");
                    Thread.Sleep(3000);
                    Console.WriteLine("3");
                    Thread.Sleep(1000);
                    Console.WriteLine("2");
                    Thread.Sleep(1000);
                    Console.WriteLine("1");
                    Thread.Sleep(1000);
                    Console.WriteLine("BOOM");
                    Console.WriteLine("You've destroyed the base camp, and all the poachers equipment");
                    GameManager.Inventory.DropItem("matches");
                    GameManager.currentPlayerRoom?.RemoveItem(new Item("dynamite", "You can destroy the base camp with this, but firstly you'll need somthing to light it with"));
                    GameManager.ActiveQuest.IsCompleted = true;

                }
                else
                {
                    Console.WriteLine("You don't have anything to light the dynamite with, you can't destroy the base camp");
                    Console.WriteLine("Comeback when you have something to light the dynamite with");
                    return;
                }
                
            }else
            {
                Console.WriteLine("You droped the dynamite when you were here the first time, but meanwhile you were gone the poachers found it and took it.");
                FailedQuest();
            }
        }
        public void FailedQuest(){
            Console.WriteLine("You failed the quest, you lost 10 points for that.");
            GameManager.score -= 10;
            if(GameManager.ActiveQuest != null)
            {
                GameManager.ActiveQuest.IsCompleted = false;
                GameManager.ActiveQuest = null;
                GameManager.IsActiveQuest = false; 
            }
        }
        public override void CheckWinCondition()
        {
            if (GameManager.score >= PointsToWin)
            {
                Console.WriteLine("You have won the game, you saved the jungle from the poachers!");
                Console.WriteLine("Score: " + GameManager.score);
                GameManager.jungleFinished = true;
                Program.Main();
            }
            else if (GameManager.score <= 0)
            {
                Console.WriteLine("You have lost the game, you didn't save the jungle from the poachers!");
                Console.WriteLine("Score: " + GameManager.score);
                
            }
        }
    }
}