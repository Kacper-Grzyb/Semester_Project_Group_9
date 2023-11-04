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
        public static bool IsActive { get; set; }

        public static Inventory? Inventory { get; set; }
        public static Room? currentPlayerRoom { get; set; }
        public static Room? previousPlayerRoom { get; set; }
        public static string? currentPlayerBiomeName { get; set; }
        public static Biomes currentPlayerBiomeType { get; set; }

        public static class Player
        {
            public static int X { get; set; }
            public static int Y { get; set; }
            public static int mapHeight { get; set; }
            public static int mapWidth { get; set; }
        }
    }
    public static class Player
    {
        public static int X {get; set; }
        public static int Y {get; set; }
        public static int mapHeight { get; set; }
        public static int mapWidth { get; set; } 
    }
}