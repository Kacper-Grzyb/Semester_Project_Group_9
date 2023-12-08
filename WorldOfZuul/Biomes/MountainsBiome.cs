using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public sealed class MountainsBiome : Biome
    {
        private int protectedZones = 0;
        public bool completed = false;
        public int hasSeenLogging = 0;
        public MountainsBiome() 
        {
            MountainsBiome.BiomeType = Biomes.Mountains;
            MountainsBiome.PointsToWin = 100;
            BiomeName = "Mountains";

            Room mountainsStartRoom = new Room("Foot of the Mountains", "You are at the bottom of the mountains.\nThe clouds cover their very tops " +
                "and you can spot patches of snow here and there.\nNear you stands an energetic young woman, she may have some important information.", 
                new List<Item>() { }, new List<NPC>{ new Guide() });
            Room mountainsRoom1 = new Room("Grassy Planes", "You enter a meadow and spot a slightly damaged structure.\nUpon inspection you see that it is a corridor that allows safe movement \nfor mountain salamanders. Maybe the hut to the north will have some useful equipment for this problem",
                new List<Item>(), new List<NPC>());
            Room mountainsRoom2 = new Room("Entrance to a cave", "You arrive at an entrance to a dark cave to the north. There is also a path leading away from it to the east.",
                new List<Item>() { new TradeableItem("Ingot", "Shiny, but not very big gold ingot") }, new List<NPC>{});
            Room mountainsRoom3 = new Room("Old Lodge", "Cranky, dusty shop. You can see that it doesn't get many visitors\nAcross the room you spot an old shopkeeper.", 
                new List<Item>(), new List<NPC>{new Shopkeeper() });
            Room mountainsRoom4 = new MountainsTriggerRoom("Foothill Woodland", "You spot some illegal logging near the breathtaking forest.\nYou have to stop it but talking to them definitely won't change their mind.\nMaybe informing resource protection will help.", 
                new List<Item>(), new List<NPC>{});
            Room mountainsRoom5 = new Room("Grassy Valley", "There seems to be a destroyed wildlife reserve for sheep\nThe sheperds are clumsly trying to keep them from getting out", 
                new List<Item>(), new List<NPC>{new Sheperds()});
            Room mountainsRoom6 = new DarkRoom("Cave", "You enter a small cave. On the walls you can see signs telling you about unprotected areas in the mountains.\nYou find out about endangered mountain ecosystems that are experiencing environmental degradation and where conserving biodiversity is a top priority.\nIt tells you that the Sustainable Development Goal number 15 is concerned with all ife on land.\nIts primary objective is to protect, restore, and promote sustainable use of terrestrial ecosystems", 
                new List<Item>() { new Item("Coin", "Small, rusty coin", true), new Cutters("Cutters", "Branch cutters, can do some heavy duty work") }, "south",new List<NPC>{});
            Room mountainsRoom7 = new Room("Mountain Shelter", "Cozy, small shelter with basic functionality.\nYou can see the owner behind the reception desk next to the door.", 
                new List<Item>() { }, new List<NPC>{new ShelterKeeper()});
            Room mountainsRoom8 = new ClimbingGearRoom("Snowy Summit", "You can barely stand the cold.\nThe only way forward is to the east but you have no chances of making it without climbing gear.\nYou also notice that the path leads straight through a habitat of mountain goats, that needs to be changed.", 
                new List<Item>(), new List<NPC>{});
            Room mountainsRoom9 = new Room("Steep Slope", "Your path is cut off by a steep slope going down.\nThis is not the way towards the top of the mountains.\nTo the side you see a hermit guarding something", 
                new List<Item>() { new Trailmaker("Trailmaker", "Can be used to create new trails")}, new List<NPC>{new MountainRiddler()});
            Room mountainsSecretRoom = new Room("?", "You see an empty desk with a fully functional Commodore64, is such a thing even possible?!", new List<Item>(), new List<NPC>{});
            Room mountainsPenultimateRoom = new Room("Mountain's Shoulder", "You can actually see the peak from here, although the passage is completely blocked by the invasive himalayan blackberry, it needs to be cut down.",
                new List<Item>(), new List<NPC>());
            Room mountainsFinalRoom = new MountainsFinalRoom("Top of the Highest Mountain", "You are rewarded with an astounding view of the mountains, yet you feel an emptiness from leaving behind unprotected zones.", new List<Item>() { new TradeableItem("Sack", "Hefty sack of gold, I wonder who left it here?")}, new List<NPC>{});

            // set exits is north east south west
            mountainsStartRoom.SetExits(mountainsRoom2, mountainsRoom1, null, null);
            mountainsRoom1.SetExits(mountainsRoom3, null, null, mountainsStartRoom);
            mountainsRoom2.SetExits(mountainsRoom6, mountainsRoom3, mountainsStartRoom, null);
            mountainsRoom3.SetExits(null, mountainsRoom4, mountainsRoom1, mountainsRoom2);
            mountainsRoom4.SetExits(null, mountainsRoom5, null, mountainsRoom3);
            mountainsRoom5.SetExits(mountainsRoom7, null, null, mountainsRoom4);
            mountainsRoom6.SetExits(mountainsRoom8, mountainsSecretRoom, mountainsRoom2, null);
            mountainsRoom7.SetExits(mountainsRoom9, null, mountainsRoom5, null);
            mountainsRoom8.SetExits(null, mountainsPenultimateRoom, mountainsRoom6, null);
            mountainsRoom9.SetExits(null, null, mountainsRoom7, null);
            mountainsSecretRoom.SetExits(null, null, null, mountainsRoom6);
            mountainsPenultimateRoom.SetExits(mountainsFinalRoom, null, null, mountainsRoom8);
            mountainsFinalRoom.SetExits(null, null, mountainsPenultimateRoom, null);

            mountainsPenultimateRoom.BlockExit("north");
            mountainsRoom6.BlockExit("east");
            startLocation = mountainsStartRoom;
            northmostRoom = mountainsFinalRoom;

            MountainsBiome.rooms = new List<Room>(){ mountainsStartRoom, mountainsRoom1, mountainsRoom2, mountainsRoom3, mountainsRoom4, 
            mountainsRoom5, mountainsRoom6, mountainsRoom7, mountainsRoom8, mountainsRoom9, mountainsSecretRoom, mountainsPenultimateRoom, mountainsFinalRoom};


        }

        public override void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the Mountains!\nThe peaks are teeming with unique biodiversity, and your role\nis critical in ensuring the survival of countless plant and animal species.\nCreate protected zones across the biome in order to win!");
        }

        public void UpdateGrassyPlanes()
        {
            rooms[1].UpdateDescriptions(null, "You see the freshly fixed salamander passage in the meadow\nYour heart fills with joy knowing you established a protected zone");
            protectedZones++;
            isFinished();
        }

        public void UpdateGrassyValley()
        {
            rooms[5].UpdateDescriptions(null, "The reserve is fully functional again, it keeps the sheep out of harms way and the sheperds can finally rest.\nYour heart fills with joy knowing you established a protected zone");
            protectedZones++;
            isFinished();
            rooms[5].NpcsInRoom.OfType<Sheperds>().FirstOrDefault().zoneProtected = true;
        }

        public void UpdateFoothillWoodland()
        {
            rooms[4].UpdateDescriptions(null, "Calling Resource Protection worked!\nThe beautiful forest has been preserved and the habitats of many animals remain untouched.\nYour heart fills with joy knowing you established a protected zone");
            protectedZones++;
            isFinished();
            rooms[4].Items.Add(new Map("Map", "Map of the mountains. Must've been dropped by one of the loggers", true));
        }

        public void UpdateSnowySummit()
        {
            rooms[8].UpdateDescriptions(null, "You can barely stand the cold.\nThe only way forward is to the east but you have no chances of making it without climbing gear.\nYou have also corrected the path so that it leads away from the habitat of mountain goats.\nThey can finally enjoy some peace. Your heart fills with joy knowing you established a protected zone");
            protectedZones++;
            isFinished();
        }

        public void UpdateMountainsShoulder()
        {
            rooms[11].UpdateDescriptions(null, "You see the peak and a clear path to it with the blackberry removed. Your heart fills with joy knowing you established a protected zone");
            rooms[11].UnlockExit("north");
            protectedZones++;
            isFinished();
        }

        public void UpdateCave()
        {
            rooms[6].UnlockExit("east");
        }

        public void isFinished()
        {
            if(protectedZones==5)
            {
                completed = true;
            }
        }

    }

    public class DarkRoom : Room
    {
        private bool isThereLight = false;
        private string darkShortDescription = "A pitch black room";
        private string darkLongDescription = "You enter a pitch black space. You can't see anything.";
        private string lightShortDescription;
        private string lightLongDescription;
        public string? unblockableExit;

        private void CheckForLight()
        {
            Item? tempItem = GameManager.Inventory?.GetItem("Flashlight");
            if (tempItem == null || !tempItem.isActive)
            {
                isThereLight = false;
            }
            if (tempItem != null && tempItem.isActive)
            {
                isThereLight = true;
            }
        }

        public DarkRoom(string shortDesc, string longDesc, List<Item> items, string unblockableDirection, List<NPC> npcsInRoom) : base(shortDesc, longDesc, items, npcsInRoom)
        {
            CheckForLight();
            Update();
            lightShortDescription = shortDesc;
            lightLongDescription = longDesc;
            npcsInRoom = npcsInRoom ?? new List<NPC>();
        }

        public override void Update()
        {
            base.Update();
            CheckForLight();

            if (!isThereLight)
            {
                base.ShortDescription = darkShortDescription;
                base.LongDescription = darkLongDescription;
                base.HideItems();

                if(unblockableExit!="north")
                {
                    base.BlockExit("north");
                }
                if (unblockableExit != "east")
                {
                    base.BlockExit("east");
                }
                if (unblockableExit != "south")
                {
                    base.BlockExit("south");
                }
                if (unblockableExit != "west")
                {
                    base.BlockExit("west");
                }
            }
            else
            {
                base.ShortDescription = lightShortDescription;
                base.LongDescription = lightLongDescription;
                base.UnhideItems();

                base.UnlockExit("north");
                //base.UnlockExit("east");
                base.UnlockExit("south");
                base.UnlockExit("west");
            }
        }

        public void SetUnblockableExit(string direction)
        {
            unblockableExit = direction;
        } 

    }

    public class MountainsFinalRoom : Room
    {
        public MountainsFinalRoom(string shortDesc, string longDesc, List<Item> items, List<NPC> npcsInRoom) : base(shortDesc, longDesc, items, npcsInRoom) { }

        public override void Update()
        {
            if(GameManager.mountainsBiomeInstance.completed)
            {
                Console.WriteLine("You have completed the mountains biome! Congratulations!");
                GameManager.mountainsFinished = true;
                GameManager.gameInstance.ChooseWorld();
            }
        }
    }

    public class ClimbingGearRoom : Room
    {
        bool isThereGear = false;

        public void CheckForGear()
        {
            Item? tempItem = GameManager.Inventory?.GetItem("Gear");
            if (tempItem == null || !tempItem.isActive)
            {
                isThereGear = false;
            }
            if (tempItem != null && tempItem.isActive)
            {
                isThereGear = true;
            }
        }

        public ClimbingGearRoom(string shortDesc, string longDesc, List<Item> items, List<NPC> npcsInRoom) : base(shortDesc, longDesc, items, npcsInRoom) 
        {
            CheckForGear();
            Update();
        }

        public override void Update()
        {
            base.Update();
            CheckForGear();
            if(isThereGear)
            {
                base.UnlockExit("east");
            }
            else
            {
                base.BlockExit("east");
            }
        }
    }


    public class MountainsTriggerRoom : Room
    {
        public MountainsTriggerRoom(string shortDesc, string longDesc, List<Item> items, List<NPC> npcsInRoom) : base(shortDesc, longDesc, items, npcsInRoom) { }

        public override void Update()
        {
            if (GameManager.mountainsBiomeInstance.hasSeenLogging != -1)
            {
                GameManager.mountainsBiomeInstance.hasSeenLogging = 1;
            }
        }
    }

    public class Guide : NPC
    {
        public Guide(string name = "Guide", string description = "He is here to point you in the right direction") : base(name, description) { }

        public override void Interact()
        {
            Console.WriteLine("Welcome, Traveller!\r\n\r\nI hear you are trying to take on the highest peak in this land! Brave undertaking! If you want any luck on that trip though, remember that these mountains will only treat you as well as you treat them. The paths are also very dangerous, and some are not even usable! Hence you must first help these mountains before you may reach your goal.\r\n\r\nEmbark on a thrilling journey to safeguard the beauty and vitality of our majestic mountainous landscapes. As a steward of the highlands, you will establish and nurture protected zones that align with the United Nations Sustainable Development Goal: Life on Land.\r\n\r\nForge ahead, explorer! Your decisions will shape the destiny of these rugged terrains. Foster the growth of lush forests, mitigate the impact of climate change, and champion the cause of sustainable living.\r\n\r\nThe mountains call, and the challenge awaits. Are you ready to answer the call and become a true champion of the highest peak?");
        }
    }

    public class Sheperds : NPC
    {
        public bool zoneProtected = false;
        bool itemGiven = false;
        public Sheperds(string name = "Sheperds", string description = "Not the best at their job") : base(name, description) { }

        public override void Interact()
        {
            if (zoneProtected)
            {
                Console.WriteLine("Thank you so much for helping us out, we don't know what we would have done without you!");
                if (!itemGiven)
                {
                    Console.WriteLine("As a token of our gratitude, please accept this delicious mountain cheese.");
                    TradeableItem cheese = new TradeableItem("Cheese", "Smoky, creamy and delicious mountain cheese");
                    if (GameManager.Inventory.isFull())
                    {
                        GameManager.currentPlayerRoom.AddItem(cheese);
                    }
                    else GameManager.Inventory.AddNPCItem(cheese);
                    itemGiven = true;
                }
            }
            else
            {
                Console.WriteLine("They seem like quite nice people but they are rather busy right not,\nmaybe try talking to them at a different time."); ;
            }
        }
    }

    public class ShelterKeeper : NPC
    {
        bool firstInteraction = true;
        public ShelterKeeper(string name = "Owner of the shelter", string description = "Has a phone in case you need it") : base(name, description) { }

        public override void Interact()
        {
            if(firstInteraction)
            {
                Console.WriteLine("Why hello there! Welcome to my mountain shelter.\nYou can rest here, and trust me you will need the energy to climb the mountain.\nWe have some food around here, showers,\nsomewhat comfortable beds and a phone with an alright connection in case you need to contact someone");
                firstInteraction = false;
            }
            else
            {
                if(GameManager.mountainsBiomeInstance.hasSeenLogging==1)
                {
                    Console.WriteLine("Oh my god! To know that people are destroying that forest right under our noses!\nYes you may definitely use the phone!");
                    Console.WriteLine("You call Resource Protection of the mountains and they successfully get rid of the loggers, good job!");
                    GameManager.mountainsBiomeInstance.UpdateFoothillWoodland();
                    GameManager.mountainsBiomeInstance.hasSeenLogging = -1;
                }
                else Console.WriteLine("Hello again! Came to get some rest?");
            }
        }
    }

    public class Shopkeeper : NPC
    {
        int interactCounter = 0;
        bool hasFlashlight = true;
        bool hasClimbingGear = true;
        bool hasCable = true;
        bool hasPickaxe = true;
        public Shopkeeper(string name = "Shopkeeper", string description = "He can sell you some stuff") : base(name, description) { }

        public void Trade(Item item)
        {
            if(item.name == "Cheese")
            {
                Console.WriteLine("That is some fine cheese indeed.");
                Console.WriteLine("As promised, here is your flashlight");
                hasFlashlight = false;

                Item flashlight = new Item("Flashlight", "Used to light the way", true);
                if (GameManager.Inventory.isFull())
                {
                    GameManager.currentPlayerRoom.AddItem(flashlight);
                }
                else GameManager.Inventory.AddNPCItem(flashlight);
                GameManager.Inventory.RemoveItem(item.name);
            }
            else if(item.name == "Emerald")
            {
                Console.WriteLine("My god! I haven't seen one in years!");
                Console.WriteLine("But as promised, here is your climbing gear");
                hasClimbingGear = false;

                Item gear = new Item("Gear", "Climbing Gear", true);
                if (GameManager.Inventory.isFull())
                {
                    GameManager.currentPlayerRoom.AddItem(gear);
                }
                else GameManager.Inventory.AddNPCItem(gear);
                GameManager.Inventory.RemoveItem(item.name);
            }
            else if(item.name == "Sack")
            {
                Console.WriteLine("You must really want that pickaxe huh.");
                Console.WriteLine("Nevertheless, a promise is a promise");
                hasPickaxe = false;

                Item pickaxe = new Pickaxe("Pickaxe", "Could be used to break through some rocks", true);
                if (GameManager.Inventory.isFull())
                {
                    GameManager.currentPlayerRoom.AddItem(pickaxe);
                }
                else GameManager.Inventory.AddNPCItem(pickaxe);
                GameManager.Inventory.RemoveItem(item.name);
            }
            else if(item.name == "Ingot")
            {
                Console.WriteLine("That is a precious one for sure!");
                Console.WriteLine("As promised, here is your cable");
                hasCable = false;
                Cable cable = new Cable("Cable", "Can be used to repair fences");
                if (GameManager.Inventory.isFull())
                {
                    GameManager.currentPlayerRoom.AddItem(cable);
                }
                else GameManager.Inventory.AddNPCItem(cable);
                GameManager.Inventory.RemoveItem(item.name);
            }
        }

        public override void Interact()
        {
            Tools item = new Tools("Tools", "Can be used to fix basic damage");

            if(interactCounter == 0)
            {
                Console.WriteLine("Welcome to my humble shop. I hear you are the one who's taking on the mountain");
                Console.WriteLine("Since it's not the easiest task, I can give you some repair equipment as a friendly gesture. Take it, it's on the shop.");
                interactCounter++;
                if (GameManager.Inventory.isFull())
                {
                    GameManager.currentPlayerRoom.AddItem(item);
                }
                else GameManager.Inventory.AddNPCItem(item);
            }
            else
            {
                Console.WriteLine("Welcome again, I have some items you may want, for a price of course.");
                if(hasFlashlight) Console.WriteLine("If you need a flashlight I can provide it to you for some mountain cheese.");
                if(hasClimbingGear) Console.WriteLine("You can also see the climbing gear hanging on the wall there, an emerald is what it will cost you.");
                if(hasPickaxe) Console.WriteLine("That pickaxe over there will cost you a sack of gold.");
                if(hasCable) Console.WriteLine("And that fence cable over there will run you a gold ingot.");
            }
        }
    }

    public class MountainRiddler : NPC
    {
        private bool firstInteraction = true;
        private bool questionsAnswered = false;
        public MountainRiddler(string name = "Hermit", string description = "Seems like he is guarding something") : base(name, description) { }

        public override void Interact()
        {
            if (firstInteraction)
            {
                Console.WriteLine("Hello Traveller, if you seek this mountain's emerald, then you've come to the right place.\nObtaining it won't be that easy though. You must prove that you truly care about this mountain.");
                firstInteraction = false;
            }
            else if (!questionsAnswered)
            {
                Console.Write("\nTo prove yourself you must answer my questions. Are you ready? (yes/no)\n> ");
                string? ans = Console.ReadLine();
                if (ans?.Trim().ToLower() == "yes")
                {
                    Console.Write("\nWhat number is the Sustainable Development Goal concerned with preserving the mountains?\n> ");
                    ans = Console.ReadLine();
                    if (ans == "15")
                    {
                        Console.Write("\nGood. What needs to be protected in mountain regions?\n> ");
                        ans = Console.ReadLine();
                        if (ans?.Trim().ToLower() == "biodiversity")
                        {
                            Console.Write("\nGood. Finally, what invasive species are we dealing with here?\n>");
                            ans = Console.ReadLine();
                            if (ans?.Trim().ToLower() == "himalayan blackberry")
                            {
                                Console.Write("\nYou have proved yourself.\nHere is the emerald as promised\nSafe travels.");
                                questionsAnswered = true;
                                TradeableItem emerald = new TradeableItem("Emerald", "Makes you want to say hmmm");
                                if (GameManager.Inventory.isFull())
                                {
                                    GameManager.currentPlayerRoom.AddItem(emerald);
                                }
                                else GameManager.Inventory.AddNPCItem(emerald);
                            }
                            else Console.WriteLine("I knew you didn't have it in you.");
                        }
                        else Console.WriteLine("I knew you didn't have it in you.");
                    }
                    else Console.WriteLine("I knew you didn't have it in you.");
                }
                else if (ans?.Trim().ToLower() == "no")
                {
                    Console.WriteLine("I knew you didn't have it in you.");
                }
                else
                {
                    Console.WriteLine("I don't know what you mean by that, but I will take it as a no.");
                }
            }
            else Console.WriteLine("I hope you reach the top. Good luck!");
        }
    }

    public class Tools : Item
    {
        public Tools(string name, string description, bool activable = true) : base(name, description, activable) { }

        public override void Activate()
        {
            if(GameManager.currentPlayerRoom.ShortDescription == "Grassy Planes")
            {
                Console.WriteLine("You used the repair equipment to fix the passage!");
                GameManager.mountainsBiomeInstance.UpdateGrassyPlanes();
            }
        }
    }

    public class Cable : Item
    {
        public Cable(string name, string description, bool activable = true) : base(name, description, activable) { }

        public override void Activate()
        {
            if (GameManager.currentPlayerRoom.ShortDescription == "Grassy Valley")
            {
                Console.WriteLine("You used the cable to fix up the reserve!");
                GameManager.mountainsBiomeInstance.UpdateGrassyValley();
            }
        }
    }

    public class Cutters : Item
    {
        public Cutters(string name, string description, bool activable = true) : base(name, description, activable) { }

        public override void Activate()
        {
            if(GameManager.currentPlayerRoom.ShortDescription == "Mountain's Shoulder")
            {
                Console.WriteLine("You used the cutter to cut through the invasive Himalayan Blackberry!");
                GameManager.mountainsBiomeInstance.UpdateMountainsShoulder();
            }
        }
    }

    public class Pickaxe : Item
    {
        public Pickaxe(string name, string description, bool activable = true) : base(name, description, activable) { }

        public override void Activate()
        {
            if (GameManager.currentPlayerRoom.ShortDescription == "Cave")
            {
                Console.WriteLine("You used the pickaxe to reveal a hidden passage! The rocks left from the smashed wall seem to be moving though...");
                GameManager.mountainsBiomeInstance.UpdateCave();
            }
        }
    }

    public class Trailmaker: Item
    {
        public Trailmaker(string name, string description, bool activable = true) : base(name, description, activable) { }

        public override void Activate()
        {
            Console.WriteLine("You used the repair equipment to fix the passage!");
            GameManager.mountainsBiomeInstance.UpdateSnowySummit();
        }
    }

    public class TradeableItem : Item
    {
        public TradeableItem(string name, string description, bool activable = true) : base(name, description, activable) { }

        public override void Activate()
        {
            if (GameManager.currentPlayerRoom.NpcsInRoom.Any(npc => npc is Shopkeeper))
            {
                Shopkeeper shopkeeper = GameManager.currentPlayerRoom.NpcsInRoom.OfType<Shopkeeper>().FirstOrDefault();
                shopkeeper.Trade(this);
            }
        }
    }
}
