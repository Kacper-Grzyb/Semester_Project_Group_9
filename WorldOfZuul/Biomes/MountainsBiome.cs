using System;
using System.Collections.Generic;
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

            Room mountainsStartRoom = new Room("Foot of the Mountains", "You are at the bottom of the mountains.\nThe clouds cover their very tops " +
            "and you can spot patches of snow here and there.\nBefore you lie paths to east and west on which you can begin your climb.", new List<Item>());
            Room mountainsRoom1 = new Room("Entrance to a cave", "You arrive at an entrance to a dark cave to the north. There is also a path leading away from it to the east.", new List<Item>() { new Item("Flashlight", "Used to light the way", true) });
            Room mountainsRoom2 = new Room("Room 2", "Room 2 Description", new List<Item>());
            Room mountainsRoom3 = new Room("Room 3", "Room 3 Description", new List<Item>());
            Room mountainsRoom4 = new Room("Room 4", "Room 4 Description", new List<Item>());
            Room mountainsRoom5 = new DarkRoom("Inside of a cave", "You enter a small cave. On the walls you can see signs telling you about unprotected areas in the mountains.\nYou find out about endangered mountain ecosystems that are experiencing environmental degradation.\nZones in need of protecting have been marked on your map. (not really)", new List<Item>() { new Item("hiddenItem", "Hidden Cave Treasure")}, "south");
            Room mountainsRoom6 = new Room("Room 6", "Room 6 Description", new List<Item>());
            Room mountainsRoom7 = new Room("Room 7", "Room 7 Description", new List<Item>());
            Room mountainsRoom8 = new Room("Room 8", "Room 8 Description", new List<Item>());
            Room mountainsOptionalRoom = new Room("Optional Room", "Optional Room Description", new List<Item>());
            Room mountainsFinalRoom = new Room("Top of the Highest Mountain", "Final Room Description", new List<Item>());

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

        public DarkRoom(string shortDesc, string longDesc, List<Item> items, string unblockableDirection) : base(shortDesc, longDesc, items)
        {
            CheckForLight();
            Update();
            lightShortDescription = shortDesc;
            lightLongDescription = longDesc;
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
