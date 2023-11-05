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

            Room mountainsStartRoom = new Room("Foot of the Mountains", "You are at the bottom of the mountains. The clouds cover their very tops and you can spot patches of snow here and there. Before you lies a path to begin your climb", new List<Item>());
            Room mountainsRoom1 = new Room("Room 1", "Room 1 Description", new List<Item>() { new Item("Flashlight", "Used to light the way") });
            Room mountainsRoom2 = new Room("Room 2", "Room 2 Description", new List<Item>());
            Room mountainsRoom3 = new Room("Room 3", "Room 3 Description", new List<Item>());
            Room mountainsRoom4 = new Room("Room 4", "Room 4 Description", new List<Item>());
            Room mountainsRoom5 = new Room("Room 5", "Room 5 Description", new List<Item>());
            Room mountainsRoom6 = new Room("Room 6", "Room 6 Description", new List<Item>());
            Room mountainsRoom7 = new Room("Room 7", "Room 7 Description", new List<Item>());
            Room mountainsRoom8 = new Room("Room 8", "Room 8 Description", new List<Item>());
            Room mountainsOptionalRoom = new Room("Optional Room", "Optional Room Description", new List<Item>());
            Room mountainsFinalRoom = new Room("Top of the highest Mountain", "Final Room Description", new List<Item>());

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
}
