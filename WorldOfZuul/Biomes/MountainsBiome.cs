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
        // TODO Update Map according to google drive
        public MountainsBiome() {
            MountainsBiome.BiomeType = Biomes.Mountains;
            MountainsBiome.PointsToWin = 100;
            BiomeName = "Mountains";

            Room mountainsStartRoom = new Room("Foot of the Mountains", "You are at the bottom of the mountains.\nThe clouds cover their very tops " +
            "and you can spot patches of snow here and there.\nBefore you lie paths to east and west on which you can begin your climb.", new List<Item>(), new List<NPC>{});
            Room mountainsRoom1 = new Room("Grassy Planes", "Next to the Starting Room", new List<Item>(), new List<NPC>());
            Room mountainsRoom2 = new Room("Entrance to a cave", "You arrive at an entrance to a dark cave to the north. There is also a path leading away from it to the east.", new List<Item>() { new Item("Flashlight", "Used to light the way", true), new Item("Map", "With this you can navigate the mountains") }, new List<NPC>{});
            Room mountainsRoom3 = new Room("Old Lodge", "Cranky Shop", new List<Item>(), new List<NPC>{});
            Room mountainsRoom4 = new Room("Foothill Woodland", "Unprotected zone", new List<Item>(), new List<NPC>{});
            Room mountainsRoom5 = new Room("Grassy Valley", "Unprotected zone", new List<Item>(), new List<NPC>{});
            Room mountainsRoom6 = new DarkRoom("Cave", "You enter a small cave. On the walls you can see signs telling you about unprotected areas in the mountains.\nYou find out about endangered mountain ecosystems that are experiencing environmental degradation.\nZones in need of protecting have been marked on your map. (not really)", new List<Item>() { new Item("hiddenItem", "Hidden Cave Treasure")}, "south",new List<NPC>{});
            Room mountainsRoom7 = new Room("Mountain Shelter", "Cozy Spot", new List<Item>(), new List<NPC>{});
            Room mountainsRoom8 = new Room("Snowy Summit", "You can barely stand the cold", new List<Item>(), new List<NPC>{});
            Room mountainsRoom9 = new Room("Steep Slope", "You will probably need some climbing gear", new List<Item>(), new List<NPC>{});
            Room mountainsSecretRoom = new Room("?", "?", new List<Item>(), new List<NPC>{});
            Room mountainsPenultimateRoom = new Room("Mountain's shoulder", "You can actually see the peak from here", new List<Item>(), new List<NPC>());
            Room mountainsFinalRoom = new Room("Top of the Highest Mountain", "Final Room", new List<Item>(), new List<NPC>{});

            // set exits is north east south west
            mountainsStartRoom.SetExits(mountainsRoom2, mountainsRoom1, null, null);
            mountainsRoom1.SetExits(mountainsRoom5, null, null, mountainsStartRoom);
            mountainsRoom2.SetExits(mountainsRoom6, mountainsRoom3, mountainsStartRoom, null);
            mountainsRoom3.SetExits(null, mountainsRoom4, null, mountainsRoom2);
            mountainsRoom4.SetExits(null, mountainsRoom5, null, mountainsRoom3);
            mountainsRoom5.SetExits(mountainsRoom7, null, mountainsRoom1, mountainsRoom4);
            mountainsRoom6.SetExits(mountainsRoom8, mountainsSecretRoom, mountainsRoom2, null);
            mountainsRoom7.SetExits(mountainsRoom9, null, mountainsRoom5, null);
            mountainsRoom8.SetExits(null, mountainsPenultimateRoom, mountainsRoom6, null);
            mountainsRoom9.SetExits(null, null, mountainsRoom7, mountainsPenultimateRoom);
            mountainsSecretRoom.SetExits(null, null, null, mountainsRoom6);
            mountainsPenultimateRoom.SetExits(mountainsFinalRoom, mountainsRoom9, null, mountainsRoom8);
            mountainsFinalRoom.SetExits(null, null, mountainsPenultimateRoom, null);

            startLocation = mountainsStartRoom;
            northmostRoom = mountainsFinalRoom;

            MountainsBiome.rooms = new List<Room>(){ mountainsStartRoom, mountainsRoom1, mountainsRoom2, mountainsRoom3, mountainsRoom4, 
            mountainsRoom5, mountainsRoom6, mountainsRoom7, mountainsRoom8, mountainsRoom9, mountainsSecretRoom, mountainsPenultimateRoom, mountainsFinalRoom};

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
