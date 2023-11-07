using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;


namespace WorldOfZuul
{
    public class Forest : Biome
    {
        // Class-level variables for the Forest Biome

        private int treesPlanted;

        private int forestHealth;

        // Existing constructor and methods

        public Forest()

        {

            // Initialization of forest-specific rooms

            Room location1 = new Room("Forest Entrance", "You are at the edge of a dense forest. The sounds of wildlife resonate around you. A path leads north into the woods.", new List<Item>());

            Room location2 = new Room("Forest Clearing", "A sunlit clearing lies before you, surrounded by towering trees. There's a sense of serenity here.", new List<Item>());

            Room location3 = new Room("Ancient Tree", "You stand before an ancient tree, its massive trunk suggesting centuries of growth. There's an aura of wisdom about this place.", new List<Item>());

            Room location4 = new Room("Rushing River", "A river cuts through the forest, its waters rushing over rocks. The sound of the water is both loud and calming.", new List<Item>());

            Room location5 = new Room("Forest Camp", "An abandoned campsite, with a firepit and some scattered supplies. It looks like someone left in a hurry.", new List<Item> {

new Item("Seedling", "A young tree ready for planting"),

new Item("Watering Can", "Useful for nurturing plants"),

new Item("Field Guide", "Helps with identifying flora and fauna")

});

            // Adding reforestation challenges to the forest biome

            var reforestationObjectives = new List<QuestObjective> {

new QuestObjective("Plant 5 seedlings in the clearing to promote forest growth", "Seedling")

};

            Quest Reforestation = new Quest("Reforestation Effort", "Help replenish the forest by planting new trees", false, false, reforestationObjectives);

            location5.AddQuest(Reforestation);

            // Connect rooms with appropriate exits to form the forest layout

            location1.SetExits(null, location2, null, null);

            location2.SetExits(null, location3, null, location1);

            // Additional exits for other rooms as needed

            // Establish the starting point within the forest biome
            // now currentRoom = GameManager.currentPlayerRoom;

            GameManager.currentPlayerRoom = location1;

            GameManager.currentPlayerRoom = GameManager.currentPlayerRoom;

            // Configure the map dimensions and player's starting position for the forest biome

            Player.mapHeight = 3;

            Player.mapWidth = 3;

            Player.X = 1;

            Player.Y = 1;

        }
    }
}
