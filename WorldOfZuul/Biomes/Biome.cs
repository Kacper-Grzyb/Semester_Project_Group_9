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
        public static string? BiomeName { get; set; }

        public virtual void WelcomeMessage()
        {

        }
        public virtual void displayMap()
        {
            
        }

        public virtual void checkForAvailableObjectives()
        {

        }

        public string returnBiomeName()
        {
            if (BiomeName == null) return "Null Biome, someone forgot to set their biome name.";
            else return BiomeName;
        }

    }
   
}
