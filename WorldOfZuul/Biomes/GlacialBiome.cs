﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public sealed class GlacialBiome : Biome
    {
        public Room startLocation;

        public GlacialBiome()
        {
            Room? glacialRoom1 = new("Snow Village", "You’re in a small Snow Village. There’s not many people. Everyone is wearing warm clothes. There’s a lot of snow everywhere. It’s hard to walk. It’s very cold. On the west you can see ice/snow desert. It’s not a good idea to go there, you might get lost. You can see some buildings on the south.", new List<Item> { new Item("x", "...") }, new List<NPC>{});
            Room? glacialRoom2 = new("Room 2", "On the east you can see the Ice Island. But there’s no bridge that you could use to get there.", new List<Item> { new Item("x", "...") }, new List<NPC>{});
            Room? glacialRoom3 = new("Room 3", "On the west there is the Ice Lake. You can’t go there. On the south you can see a huge ice mountain. It’s dangerous to go there without proper equipment.", new List<Item> { new Item("x", "...") }, new List<NPC>{});
            Room? glacialRoom4 = new("Room 4", ".", new List<Item> { new Item("x", "...") }, new List<NPC>{});
            Room? glacialRoom5 = new("Room 5", "On the east you can see ocean. ", new List<Item> { new Item("x", "...") }, new List<NPC>{});
            Room? glacialRoom6 = new("Snowy Hillside", "The top of the ice mountain is on the south. It looks very slippery though.", new List<Item> { new Item("x", "...") }, new List<NPC>{});
            Room? glacialRoom7 = new("The bottom of the Ice Mountain", "You’re at the bottom of the Ice Mountain", new List<Item> { new Item("x", "...") }, new List<NPC>{});
            Room? glacialRoom8 = new("Ice Valley", "You're in Ice Valley. Everything around you is white - you’re surrounded by snow and ice. On the east there is a huge ice mountain, it’s dangerous to go there without a proper equipment. On the south you can see a cave.", new List<Item> { }, new List<NPC>{});
            Room? glacialRoom9 = new("Ice Cave", "You've entered an Ice Cave. It's dark here, you can't really see anything.", new List<Item> { }, new List<NPC>{});
            Room? glacialRoom10 = new("Ice Island", "You're on the Ice Island, surrounded by water.", new List<Item> { }, new List<NPC>{});
            Room? glacialFinalRoom = new("The top of the mountain", "You're at the top of the Ice Mountain. Right in front of you there is an igloo (..?), but the entrance is locked.", new List<Item> { }, new List<NPC>{});


            glacialRoom1.SetExits(null, glacialRoom2, glacialRoom4, null);
            glacialRoom2.SetExits(null, glacialRoom10, glacialRoom5, glacialRoom1);
            glacialRoom3.SetExits(null, glacialRoom4, glacialRoom7, null);
            glacialRoom4.SetExits(glacialRoom1, glacialRoom5, glacialRoom8, glacialRoom3);
            glacialRoom5.SetExits(glacialRoom2, glacialRoom4, null, null);
            glacialRoom6.SetExits(null, glacialRoom7, glacialFinalRoom, null);
            glacialRoom7.SetExits(glacialRoom3, glacialRoom8, null, glacialRoom6);
            glacialRoom8.SetExits(glacialRoom4, null, glacialRoom9, glacialRoom7);
            glacialRoom9.SetExits(glacialRoom8, null, null, null);
            glacialRoom10.SetExits(null, null, null, glacialRoom2);
            glacialFinalRoom.SetExits(glacialRoom10, null, null, null);

            startLocation = glacialRoom1;
            GlacialBiome.rooms = new List<Room>(){ glacialRoom1, glacialRoom2, glacialRoom3, glacialRoom4,
            glacialRoom5, glacialRoom6, glacialRoom7, glacialRoom8, glacialRoom9, glacialRoom10, glacialFinalRoom};

        }
    }

        
}
