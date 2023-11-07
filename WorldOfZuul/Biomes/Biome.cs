using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public enum Biomes
    {
        Grasslands,
        Forest,
        Jungle,
        Mountains,
        Glacial
    }
    public class Biome
    {
        public static int PointsToWin {  get; set; }
        public static List<Room>? rooms { get; set; }
        public static Biomes BiomeType { get; set; }

        public virtual void WelcomeMessage()
        {

        }
    }
}
