using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public sealed class MountainsBiome : Biome
    {
        public Room startLocation;

        public MountainsBiome() {
            MountainsBiome.BiomeType = Biomes.Mountains;
            MountainsBiome.PointsToWin = 100;
            BiomeName = "Mountains";

            Room mountainsStartRoom = new Room("Foot of the Mountains", "You are at the bottom of the mountains.\nThe clouds cover their very tops " +
            "and you can spot patches of snow here and there.\nBefore you lie paths to east and west on which you can begin your climb.", new List<Item>(), new List<NPC>{});
            Room mountainsRoom1 = new Room("Entrance to a cave", "You arrive at an entrance to a dark cave to the north. There is also a path leading away from it to the east.", new List<Item>() { new Item("Flashlight", "Used to light the way", true) }, new List<NPC>{});
            Room mountainsRoom2 = new Room("Old Lodge", "Cranky Shop", new List<Item>() { new Item("Map", "With this you can navigate the mountains")}, new List<NPC>{});
            Room mountainsRoom3 = new Room("Foothill Woodland", "Unprotected zone", new List<Item>(), new List<NPC>{});
            Room mountainsRoom4 = new Room("Grassy Valley", "Unprotected zone", new List<Item>(), new List<NPC>{});
            Room mountainsRoom5 = new DarkRoom("Cave", "You enter a small cave. On the walls you can see signs telling you about unprotected areas in the mountains.\nYou find out about endangered mountain ecosystems that are experiencing environmental degradation.\nZones in need of protecting have been marked on your map. (not really)", new List<Item>() { new Item("hiddenItem", "Hidden Cave Treasure")}, "south",new List<NPC>{});
            Room mountainsRoom6 = new Room("Mountain Shelter", "Cozy Spot", new List<Item>(), new List<NPC>{});
            Room mountainsRoom7 = new Room("Snowy Summit", "You can barely stand the cold", new List<Item>(), new List<NPC>{});
            Room mountainsRoom8 = new Room("Steep Slope", "You will probably need some climbing gear", new List<Item>(), new List<NPC>{});
            Room mountainsOptionalRoom = new Room("?", "?", new List<Item>(), new List<NPC>{});
            Room mountainsFinalRoom = new Room("Top of the Highest Mountain", "Final Room", new List<Item>(), new List<NPC>{});

            mountainsStartRoom.SetExits(null, mountainsRoom4, null, mountainsRoom1);
            mountainsRoom1.SetExits(mountainsRoom5, mountainsRoom2, mountainsStartRoom, null);
            mountainsRoom2.SetExits(null, mountainsRoom3, mountainsRoom1, null);
            mountainsRoom3.SetExits(null, mountainsRoom4, mountainsRoom2, null);
            mountainsRoom4.SetExits(mountainsRoom6, null, mountainsStartRoom, mountainsRoom3);
            mountainsRoom5.SetExits(mountainsRoom7, mountainsOptionalRoom, mountainsRoom1, null);
            mountainsRoom6.SetExits(mountainsRoom8, null, mountainsRoom4, mountainsOptionalRoom);
            mountainsRoom7.SetExits(mountainsFinalRoom, mountainsRoom8, mountainsRoom5, null);
            mountainsRoom8.SetExits(mountainsFinalRoom, null, mountainsRoom6, mountainsRoom7);
            mountainsOptionalRoom.SetExits(null, mountainsRoom6, null, mountainsRoom5);
            mountainsFinalRoom.SetExits(null, mountainsRoom8, null, mountainsRoom7);

            startLocation = mountainsStartRoom;

            MountainsBiome.rooms = new List<Room>(){ mountainsStartRoom, mountainsRoom1, mountainsRoom2, mountainsRoom3, mountainsRoom4, 
            mountainsRoom5, mountainsRoom6, mountainsRoom7, mountainsRoom8, mountainsOptionalRoom, mountainsFinalRoom};

        }

        public override void displayMap()
        {
            string map = "  \t        _--[Top of the Highest Mountain]--_\r\n\t        |\t\t\t\t   |\r\n\t        |\t\t\t\t   |\r\n                [Snowy Summit]---------[Steep Slope] \r\n                    |\t\t              |\r\n\t            |  \t\t              |\r\n                 [Cave]----------[?]       [Mountain Shelter]\r\n                  |\t\t\t\t          |\r\n                  |\t\t\t                  |\r\n[Entrance to a cave]--[Old Lodge]--[Foothill Woodland]--[Grassy Valley]\r\n         |                                                     | \r\n         |                                                     |\r\n         \\----------------[Foot of the Mountains]--------------/";
            for(int i = 0; i<map.Count(); i++)
            {
                // if it's the beggining of the name of a room in this convention of writing the map
                Console.Write(map[i]);
                if (map[i]=='[')
                {
                    i++;
                    string roomName="";
                    while (map[i]!=']')
                    { 
                        roomName += map[i];
                        i++;
                    }
                    if (roomName == GameManager.currentPlayerRoom?.ShortDescription)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(roomName);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(roomName);
                    }
                    Console.Write("]");
                }
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
}
