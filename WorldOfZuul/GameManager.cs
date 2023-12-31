using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public static class GameManager
    {

        public static int score { get; set; }
        public static string? playerName { get; set; }
        public static float time { get; set; }
        public static Quest? ActiveQuest { get; set; }
        public static bool IsActiveQuest { get; set; }

        public static Inventory? Inventory { get; set; }
        public static Room? currentPlayerRoom { get; set; }
        public static Stack<Room>? previousPlayerRooms { get; set; }
        public static Biome? currentPlayerBiome { get; set; }
        public static MountainsBiome? mountainsBiomeInstance { get; set; }
        public static JungleBiome? jungleBiomeInstance { get; set; }
        public static GrasslandsBiome? grasslandsBiomeInstance { get; set; }
        public static GlacialBiome? glacialBiomeInstance { get; set; }
        public static ForestBiome? forestBiomeInstance { get; set; }
        public static Game? gameInstance { get; set; }

        public static bool mountainsFinished = false;
        public static bool jungleFinished = false;
        public static bool grasslandsFinished = false;
        public static bool glacialFinished = false;
        public static bool forestFinished = false;


        public static class Player
        {
            public static int X { get; set; }
            public static int Y { get; set; }
            public static int mapHeight { get; set; }
            public static int mapWidth { get; set; }
        }

        public static void TimeQuests()
        {
            if(GameManager.IsActiveQuest)
            {
                
            }
        }
    }
    public static class Player
    {
        public static int X { get; set; }
        public static int Y { get; set; }
        public static int mapHeight { get; set; }
        public static int mapWidth { get; set; }
    }

}