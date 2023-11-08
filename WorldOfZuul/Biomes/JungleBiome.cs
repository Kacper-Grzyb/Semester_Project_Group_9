using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;


namespace WorldOfZuul
{
    public sealed class JungleBiome:Biome
    {
        public Room startLocation;
        public JungleBiome()
        {
            JungleBiome.BiomeType = Biomes.Jungle;
            JungleBiome.PointsToWin = 100;

             Room? location1 = new("Sector 1", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });

            Room? location2 = new("Sector 2", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<Item> { });

            Room? location3 = new("Sector 3", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<Item> { });

            Room? location4 = new("sector 4", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.", new List<Item> { });

            Room? location5 = new("Sector 5", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<Item> { new Item("Flashlight", "A way to light your path"),
            new Item("Map","Useful for navigation"), new Item("Trap", "Can be used against enemies") });

            Room? location6 = new("Sector 6 ", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.", new List<Item> { });

            Room? location7 = new("Sector 7", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.", new List<Item> { });

            Room? location8 = new("Sector 8", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.", new List<Item> { });

            Room? location9 = new("Sector 9", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.", new List<Item> { });

            var stopPoachers = new List<QuestObjective>
            {
             new QuestObjective("Destroy 9 traps that poachers setup in Sector 9","Map")

            };
            Quest Poachers = new Quest("Disable traps", "fing all the traps that poachers setup in Sector 9", false, false, stopPoachers);
            location5.AddQuest(Poachers);

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

            Player.mapHeight = 3;
            Player.mapWidth = 3;
            Player.X = 1;
            Player.Y = 1;
        }
        public override void displayMap()
        {
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
        }
    }
}