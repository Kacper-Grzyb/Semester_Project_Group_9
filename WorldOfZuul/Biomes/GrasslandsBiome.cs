using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;


namespace WorldOfZuul
{
    public class Grasslands
    {
        public void CreateGrasslands()
        {
            Room? GrasslandsStartRoom = new("Welcome to the grasslands biome!", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom1 = new("Room 1", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom2 = new("Room 2", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom3 = new("Room 3", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom4 = new("Room 4", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom5 = new("Room 5", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom6 = new("Room 6", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom7 = new("Room 7", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom8 = new("Room 8", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });
            Room? GrasslandsRoom9 = new("Room 9", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });       
            Room? GrasslandsFinalRoom = new("You've reached the final room of the grasslands biome!", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });

            
            // North East South West
            GrasslandsStartRoom.SetExits(null, null, GrasslandsRoom1, null);
            GrasslandsRoom1.SetExits(GrasslandsStartRoom, GrasslandsRoom4, GrasslandsRoom3, GrasslandsRoom2);
            GrasslandsRoom2.SetExits(null, GrasslandsRoom1, GrasslandsRoom5, null);
            GrasslandsRoom3.SetExits(GrasslandsRoom1, GrasslandsRoom6, null, GrasslandsRoom5);
            GrasslandsRoom4.SetExits(null, null, GrasslandsRoom6, GrasslandsRoom1);
            GrasslandsRoom5.SetExits(GrasslandsRoom2, GrasslandsRoom3, GrasslandsRoom8, GrasslandsRoom7);
            GrasslandsRoom6.SetExits(GrasslandsRoom4, null , GrasslandsRoom9, GrasslandsRoom3);
            GrasslandsRoom7.SetExits(null, GrasslandsRoom5, GrasslandsFinalRoom, null);
            GrasslandsRoom8.SetExits(GrasslandsRoom5, null, GrasslandsFinalRoom, null);
            GrasslandsRoom9.SetExits(null, null, GrasslandsFinalRoom, GrasslandsRoom6);
            GrasslandsFinalRoom.SetExits(GrasslandsRoom8, GrasslandsRoom9, null /*this is going to be replaced by the "gate"*/,  GrasslandsRoom7);

            Player.mapHeight = 5;
            Player.mapWidth = 4;
            Player.X = 1;
            Player.Y = 1;
        }
    }
}