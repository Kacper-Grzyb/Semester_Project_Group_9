using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public static class GameManager
    {
        public static int score {get; set;}
        public static string? playerName {get; set;}
        public static float time {get; set;}

        public static Inventory? Inventory { get; set;}
        public static Room? currentPlayerRoom { get; set; }
        public static string? currentPlayerWorld { get; set; }
    }
}