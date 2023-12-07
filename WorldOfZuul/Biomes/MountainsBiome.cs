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
        public MountainsBiome() 
        {
            MountainsBiome.BiomeType = Biomes.Mountains;
            MountainsBiome.PointsToWin = 100;
            BiomeName = "Mountains";

            Room mountainsStartRoom = new Room("Foot of the Mountains", "You are at the bottom of the mountains.\nThe clouds cover their very tops " +
                "and you can spot patches of snow here and there.\nNear you stands an energetic young woman, she may have some important information.", 
                new List<Item>(), new List<NPC>{ new Guide() });
            Room mountainsRoom1 = new Room("Grassy Planes", "You enter a meadow and spot a slightly damaged structure.\nUpon inspection you see that it is a corridor that allows safe movement \nfor mountain salamanders. Maybe the hut to the north will have some useful equipment for this problem",
                new List<Item>(), new List<NPC>());
            Room mountainsRoom2 = new Room("Entrance to a cave", "You arrive at an entrance to a dark cave to the north. There is also a path leading away from it to the east.",
                new List<Item>() { new TradeableItem("Gold_Ingot", "Shiny, but not very big") }, new List<NPC>{});
            Room mountainsRoom3 = new Room("Old Lodge", "Cranky, dusty shop. You can see that it doesn't get many visitors\n Across the room you spot an old shopkeeper.", 
                new List<Item>(), new List<NPC>{new Shopkeeper() });
            Room mountainsRoom4 = new Room("Foothill Woodland", "Unprotected zone", new List<Item>(), new List<NPC>{});
            Room mountainsRoom5 = new Room("Grassy Valley", "There seems to be a destroyed wildlife reserve for sheep\nThe sheperds are clumsly trying to keep them from getting out", new List<Item>(), new List<NPC>{});
            Room mountainsRoom6 = new DarkRoom("Cave", "You enter a small cave. On the walls you can see signs telling you about unprotected areas in the mountains.\nYou find out about endangered mountain ecosystems that are experiencing environmental degradation.", 
                new List<Item>() { new TradeableItem("Emerald", "This must be worth a lot")}, "south",new List<NPC>{});
            Room mountainsRoom7 = new Room("Mountain Shelter", "Cozy Spot", new List<Item>() { new TradeableItem("Mountain Cheese", "Smoky, creamy and delicious")}, new List<NPC>{});
            Room mountainsRoom8 = new Room("Snowy Summit", "You can barely stand the cold", new List<Item>(), new List<NPC>{});
            Room mountainsRoom9 = new Room("Steep Slope", "You will probably need some climbing gear", new List<Item>(), new List<NPC>{});
            Room mountainsSecretRoom = new Room("?", "?", new List<Item>(), new List<NPC>{});
            Room mountainsPenultimateRoom = new Room("Mountain's shoulder", "You can actually see the peak from here", new List<Item>(), new List<NPC>());
            Room mountainsFinalRoom = new Room("Top of the Highest Mountain", "Final Room", new List<Item>(), new List<NPC>{});

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
        }

        public void UpdateGrassyValley()
        {
            rooms[5].UpdateDescriptions(null, "The reserve is fully functional again, it keeps the sheep out of harms way and the sheperds can finally rest\nYour heart fills with joy knowing you established a protected zone");
            protectedZones++;
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
                base.UnlockExit("east");
                base.UnlockExit("south");
                base.UnlockExit("west");
            }
        }

        public void SetUnblockableExit(string direction)
        {
            unblockableExit = direction;
        } 

    }

    public class Guide : NPC
    {
        public Guide(string name = "Guide", string description = "He is here to point you in the right direction") : base(name, description) 
        {

        }

        public override void Interact()
        {
            Console.WriteLine("Welcome, Traveller!\r\n\r\nI hear you are trying to take on the highest peak in this land! Brave undertaking! If you want any luck on that trip though, remember that these mountains will only treat you as well as you treat them. The paths are also very dangerous, and some are not even usable! Hence you must first help these mountains before you may reach your goal.\r\n\r\nEmbark on a thrilling journey to safeguard the beauty and vitality of our majestic mountainous landscapes. As a steward of the highlands, you will establish and nurture protected zones that align with the United Nations Sustainable Development Goal: Life on Land.\r\n\r\nForge ahead, explorer! Your decisions will shape the destiny of these rugged terrains. Foster the growth of lush forests, mitigate the impact of climate change, and champion the cause of sustainable living.\r\n\r\nThe mountains call, and the challenge awaits. Are you ready to answer the call and become a true champion of the highest peak?");
        }
    }

    public class Shopkeeper : NPC
    {
        int interactCounter = 0;
        bool hasFlashlight = true;
        bool hasClimbingGear = true;
        bool hasCable = true;
        bool hasPickaxe = true;
        public Shopkeeper(string name = "Shopkeeper", string description = "He can sell you some stuff") : base(name, description)
        {
            
        }

        public void Trade(Item item)
        {
            if(item.name == "Mountain_Cheese")
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
                if (GameManager.Inventory.isFull())
                {
                    GameManager.currentPlayerRoom.AddItem(item);
                }
                else GameManager.Inventory.AddNPCItem(item);
                GameManager.Inventory.RemoveItem(item.name);
            }
            else if(item.name == "Sack_of_Gold")
            {
                Console.WriteLine("You must really want that pickaxe huh.");
                Console.WriteLine("Nevertheless, a promise ios a promise");
                hasPickaxe = false;
                if (GameManager.Inventory.isFull())
                {
                    GameManager.currentPlayerRoom.AddItem(item);
                }
                else GameManager.Inventory.AddNPCItem(item);
                GameManager.Inventory.RemoveItem(item.name);
            }
            else if(item.name == "Gold_Ingot")
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
                if(hasFlashlight) Console.WriteLine("If you need a flashlight I can provide it to you for some mountain cheese");
                if(hasClimbingGear) Console.WriteLine("You can also see the climbing gear hanging on the wall there, an emerald is what it will cost you");
                if(hasPickaxe) Console.WriteLine("That pickaxe over there will cost you a sack of gold");
                if(hasCable) Console.WriteLine("And that fence cable over there will run you a gold ingot");
            }
        }
    }

    public class Tools : Item
    {
        public Tools(string name, string description, bool activable = true) : base(name, description, activable)
        {

        }

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
        public Cable(string name, string description, bool activable = true) : base(name, description, activable)
        {

        }

        public override void Activate()
        {
            if (GameManager.currentPlayerRoom.ShortDescription == "Grassy Valley")
            {
                Console.WriteLine("You used the cable to fix up the reserve!");
                GameManager.mountainsBiomeInstance.UpdateGrassyValley();
            }
        }
    }

    public class TradeableItem : Item
    {
        public TradeableItem(string name, string description, bool activable = true) : base(name, description, activable)
        {

        }

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
