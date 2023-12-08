
/*
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security;
using static WorldOfZuul.GameManager;

namespace WorldOfZuul
{
    public class ForestBiome : Biome
    {
        private int treesPlanted;
        private int forestHealth;

        public ForestBiome()
        {
            BiomeName = "Forest";
            forestHealth = 50; // Initial forest health

            // Initialization of forest-specific rooms with empty lists for items and NPCs
            Room location1 = new Room("Forest Entrance", "You are at the edge of a dense forest. The sounds of wildlife resonate around you. A path leads north into the woods.", new List<Item>(), new List<NPC>());
            Room location2 = new Room("Forest Clearing", "A sunlit clearing lies before you, surrounded by towering trees. There's a sense of serenity here.", new List<Item>(), new List<NPC>());
            Room location3 = new Room("Ancient Tree", "You stand before an ancient tree, its massive trunk suggesting centuries of growth. There's an aura of wisdom about this place.", new List<Item>(), new List<NPC>());
            Room location4 = new Room("Rushing River", "A river cuts through the forest, its waters rushing over rocks. The sound of the water is both loud and calming.", new List<Item>(), new List<NPC>());
            Room location5 = new Room("Forest Camp", "An abandoned campsite, with a firepit and some scattered supplies. It looks like someone left in a hurry.", new List<Item>(), new List<NPC>());
            InitializeAndAddQuestsToRooms(location1, location2, location3, location4, location5);

            // Connect rooms with appropriate exits
            location1.SetExits(null, location2, null, null);
            location2.SetExits(null, location3, null, location1);
            location3.SetExits(null, location4, null, location2);
            location4.SetExits(null, location5, null, location3);
            location5.SetExits(null, null, null, location4);

            // Set the starting point within the forest biome
            startLocation = location1;

            // Configure the map dimensions and player's starting position
            Player.mapHeight = 5;
            Player.mapWidth = 5;
            Player.X = 1;
            Player.Y = 1;
        }

        private void InitializeAndAddQuestsToRooms(params Room[] rooms)
        {
            // Forest Clean-Up Quest
            Quest quest1 = new Quest("Forest Clean-Up", "Help clean up the forest.", false, false);
            quest1.AddObjective("Clean the Forest Entrance", "Trash Bag");
            rooms[0].AddQuest(quest1); // Adding to Forest Entrance

            // Tree Planting Quest
            Quest quest2 = new Quest("Tree Planting", "Assist in planting trees in the clearing.", false, false);
            quest2.AddObjective("Plant Trees in the Clearing", "Sapling");
            rooms[1].AddQuest(quest2); // Adding to Forest Clearing

            // River Cleanup Quest
            Quest quest3 = new Quest("River Cleanup", "Help clean the river from pollution.", false, false);
            quest3.AddObjective("Clean the Rushing River", "Trash Bag");
            rooms[3].AddQuest(quest3); // Adding to Rushing River

            // Campsite Restoration Quest
            Quest quest4 = new Quest("Campsite Restoration", "Restore and organize the abandoned forest camp.", false, false);
            quest4.AddObjective("Organize the Forest Camp", "Tool Kit");
            rooms[4].AddQuest(quest4); // Adding to Forest Camp

            // Ancient Tree Preservation Quest
            Quest quest5 = new Quest("Ancient Tree Preservation", "Conduct a study and preservation efforts on the ancient tree.", false, false);
            quest5.AddObjective("Study the Ancient Tree", "Research Equipment");
            rooms[2].AddQuest(quest5); // Adding to Ancient Tree
        }

        public void EnterRoom(Room room)
        {
            Console.WriteLine(GetNarrativeForRoom(room));
            PresentChoicesForRoom(room);

            string choice = Console.ReadLine();
            ApplyChoiceConsequences(room.Name, choice);

            DisplayForestState();
        }

        private string GetNarrativeForRoom(Room room)
        {
            switch (room.Name)
            {
                case "Forest Entrance":
                    return "You enter the forest entrance. Signs of illegal logging activity are evident.";
                case "Forest Clearing":
                    return "You arrive at a clearing where a reforestation project is underway.";
                case "Ancient Tree":
                    return "You stand before an ancient tree, a vital part of the ecosystem.";
                case "Rushing River":
                    return "You reach a river, crucial for the forest's health but threatened by pollution.";
                case "Forest Camp":
                    return "You find an educational campsite, teaching about forest conservation.";
                default:
                    return "You find yourself in an unexplored part of the forest.";
            }
        }

        private void PresentChoicesForRoom(Room room)
        {
            switch (room.Name)
            {
                case "Forest Entrance":
                    Console.WriteLine("A. Investigate the logging activity");
                    Console.WriteLine("B. Inform authorities about the logging");
                    Console.WriteLine("C. Move to the next area");
                    break;
                case "Forest Clearing":
                    Console.WriteLine("A. Help plant new trees");
                    Console.WriteLine("B. Study the local flora and fauna");
                    Console.WriteLine("C. Move to the next area");
                    break;
                case "Ancient Tree":
                    Console.WriteLine("A. Conduct a biodiversity survey");
                    Console.WriteLine("B. Set up protective measures around the tree");
                    Console.WriteLine("C. Reflect on the forest's history");
                    break;
                case "Rushing River":
                    Console.WriteLine("A. Clean up river pollution");
                    Console.WriteLine("B. Monitor the river's wildlife");
                    Console.WriteLine("C. Rest and enjoy the river's beauty");
                    break;
                case "Forest Camp":
                    Console.WriteLine("A. Participate in a conservation workshop");
                    Console.WriteLine("B. Prepare educational materials for visitors");
                    Console.WriteLine("C. Share your experiences with other campers");
                    break;
            }
        }

        private void ApplyChoiceConsequences(string roomName, string choice)
        {
            switch (roomName)
            {
                case "Forest Entrance":
                    switch (choice)
                    {
                        case "A":
                            Console.WriteLine("You gather evidence of illegal logging.");
                            forestHealth -= 5;
                            break;
                        case "B":
                            Console.WriteLine("Authorities are informed and take action.");
                            forestHealth += 5;
                            break;
                        case "C":
                            Console.WriteLine("You move on, leaving the area as it is.");
                            break;
                    }
                    break;
                case "Forest Clearing":
                    switch (choice)
                    {
                        case "A":
                            Console.WriteLine("You help plant new trees. The forest begins to recover.");
                            forestHealth += 10;
                            treesPlanted += 5;
                            break;
                        case "B":
                            Console.WriteLine("You learn valuable information about local biodiversity.");
                            break;
                        case "C":
                            Console.WriteLine("You move on to explore more of the forest.");
                            break;
                    }
                    break;
                case "Ancient Tree":
                    switch (choice)
                    {
                        case "A":
                            Console.WriteLine("Your survey contributes to understanding local biodiversity.");
                            forestHealth += 5;
                            break;
                        case "B":
                            Console.WriteLine("Your efforts help protect this crucial part of the ecosystem.");
                            forestHealth += 10;
                            break;
                        case "C":
                            Console.WriteLine("You gain a deeper appreciation for the forest's history.");
                            break;
                    }
                    break;
                case "Rushing River":
                    switch (choice)
                    {
                        case "A":
                            Console.WriteLine("Your cleanup efforts help preserve the river's health.");
                            forestHealth += 10;
                            break;
                        case "B":
                            Console.WriteLine("You gain valuable insights into the river's ecosystem.");
                            break;
                        case "C":
                            Console.WriteLine("You rest and enjoy the tranquil beauty of the river.");
                            break;
                    }
                    break;
                case "Forest Camp":
                    switch (choice)
                    {
                        case "A":
                            Console.WriteLine("You learn and share valuable conservation techniques.");
                            forestHealth += 5;
                            break;
                        case "B":
                            Console.WriteLine("Your materials will educate future visitors about the forest.");
                            break;
                        case "C":
                            Console.WriteLine("Sharing experiences fosters a community of conservationists.");
                            break;
                    }
                    break;
            }
        }

        public void DisplayForestState()
        {
            Console.WriteLine($"Forest Health: {forestHealth}");
            Console.WriteLine($"Trees Planted: {treesPlanted}");
        }
    }
}
/*/