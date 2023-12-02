using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;


namespace WorldOfZuul
{
    public class GrasslandsBiome : Biome
    {
        public GrasslandsBiome()
        {
            BiomeName = "Grasslands";


            Room? GrasslandsStartRoom = new("Welcome to the grasslands biome!", "You find yourself standing in front of a vast grassland, stretching as far as the eye can see. Gentle breezes sway the tall grasses, creating a sea of green. The air is filled with soil scent and the native birds sing through the landscape, but the once refined grassland signals of destructive threats...", new List<Item> { }, new List<NPC> { });
            new Item("Map of Grasslands", "Your guide around the treasures of the biome");
            Room? GrasslandsRoom1 = new("Collaboration Corner:", "A central hub for various contributors to discuss and implement conservation strategies.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom2 = new("Eco-Unity Hub:", "A space designated for the people intrested in preserving the grassland biome.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom3 = new("Animal Aid", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom4 = new("Sustainable Basecamp", "An open area filled with discussions and ideas about making a change in the biome.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom5 = new("Room 5", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom6 = new("Room 6", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom7 = new("Room 7", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom8 = new("Room 8", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsRoom9 = new("Room 9", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { }, new List<NPC> { });
            Room? GrasslandsFinalRoom = new("Final Room", "You've reached the final room of the grasslands biome!", new List<Item> { }, new List<NPC> { });


            // North East South West
            GrasslandsStartRoom.SetExits(null, null, GrasslandsRoom1, null);
            GrasslandsRoom1.SetExits(GrasslandsStartRoom, GrasslandsRoom4, GrasslandsRoom3, GrasslandsRoom2);
            GrasslandsRoom2.SetExits(null, GrasslandsRoom1, GrasslandsRoom5, null);
            GrasslandsRoom3.SetExits(GrasslandsRoom1, GrasslandsRoom6, null, GrasslandsRoom5);
            GrasslandsRoom4.SetExits(null, null, GrasslandsRoom6, GrasslandsRoom1);
            GrasslandsRoom5.SetExits(GrasslandsRoom2, GrasslandsRoom3, GrasslandsRoom8, GrasslandsRoom7);
            GrasslandsRoom6.SetExits(GrasslandsRoom4, null, GrasslandsRoom9, GrasslandsRoom3);
            GrasslandsRoom7.SetExits(null, GrasslandsRoom5, GrasslandsFinalRoom, null);
            GrasslandsRoom8.SetExits(GrasslandsRoom5, null, null, GrasslandsFinalRoom);
            GrasslandsRoom9.SetExits(GrasslandsRoom6, null, null, null);
            GrasslandsFinalRoom.SetExits(GrasslandsRoom7, GrasslandsRoom8, null /*this is going to be replaced by the "gate"*/, null);


            startLocation = GrasslandsStartRoom;
            northmostRoom = GrasslandsStartRoom;
            rooms = new List<Room>() { GrasslandsStartRoom, GrasslandsRoom1, GrasslandsRoom2, GrasslandsRoom3, GrasslandsRoom4, GrasslandsRoom5, GrasslandsRoom6, GrasslandsRoom7, GrasslandsRoom8, GrasslandsRoom9, GrasslandsFinalRoom };

            Player.mapHeight = 5;
            Player.mapWidth = 4;
            Player.X = 1;
            Player.Y = 1;


        }
    }
}