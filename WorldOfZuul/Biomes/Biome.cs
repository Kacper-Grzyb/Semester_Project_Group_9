using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public static int PointsToWin { get; set; }
        public static List<Room>? rooms { get; set; }
        public static Biomes BiomeType { get; set; }
        public static string? BiomeName { get; set; }
        public Room? northmostRoom { get; set; }
        public Room? startLocation { get; set; }

        private string filler = "_";

        public virtual void WelcomeMessage()
        {
            Console.WriteLine($"Welcome to the {GameManager.currentPlayerBiome?.returnBiomeName()}!");
        }
        public virtual void CheckWinCondition()
        {
            
        }
        public void WriteMapWithHighlight(string map)
        {
            for (int i = 0; i < map.Count(); i++)
            {
                // if it's the beggining of the name of a room in this convention of writing the map
                Console.Write(map[i]);
                if (map[i] == '[')
                {
                    i++;
                    string roomName = "";
                    while (map[i] != ']')
                    {
                        roomName += map[i];
                        i++;
                    }
                    if (roomName.Replace(filler, "") == GameManager.currentPlayerRoom?.ShortDescription)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(roomName);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(roomName);
                    }
                    Console.Write("]");
                }
            }
        }

        public virtual void DisplayMap()
        {
            if (rooms == null)
            {
                Console.WriteLine("This biome doesn't have a rooms list!");
                return;

            }
            if (northmostRoom == null)
            {
                Console.WriteLine("This biome has not set the northmostRoom variable to anything!");
                return;
            }

            string map = "";
            int longestRoomName = 0, width = 0;

            // Find the longest room name so the other room names can be filled with placeholders to match it so the map is uniform
            foreach (Room room in rooms)
            {
                if (room.ShortDescription.Length > longestRoomName)
                {
                    longestRoomName = room.ShortDescription.Length;
                }
            }

            // currentRoom needs to be set to the room that's located most north in the map in order for the function to work
            Room currentRoom = northmostRoom;
            Dictionary<Room, bool> printedRooms = new Dictionary<Room, bool>();
            List<string> mapLines = new List<string>();

            // getting the longest row of rooms
            string longestRoomChain = "";
            int currPos = 0, maxE = -1, minW = 0, currDepth=0, maxDepth=0;
            Dictionary<Room, bool> visitedRooms = new Dictionary<Room, bool>();
            Dictionary<Room, int> roomPositions = new Dictionary<Room, int>(); // this will be useful later
            Dictionary<Room, int> roomDepths = new Dictionary<Room, int>();
            FindGridDimensions(currentRoom, ref longestRoomChain, currPos, ref maxE, ref minW, currDepth, ref maxDepth, ref visitedRooms, ref roomPositions, ref roomDepths);
            width = HowManyRoomsInLine(longestRoomChain);
            string[,] helperArray = new string[maxDepth + 1, width];
            Dictionary<Room, int>.KeyCollection keys = roomPositions.Keys;

            // Shifting values so that the room most on the west starts with index 0
            foreach (var room in rooms)
            {
                roomPositions[room] -= minW;
                helperArray[roomDepths[room], roomPositions[room]] = room.ShortDescription;
                Console.WriteLine($"{room.ShortDescription} has a position of {roomPositions[room]} and a depth of {roomDepths[room]}");
            }

            for(int i= 0; i < maxDepth+1; i++)
            {
                mapLines.Add("");
                for (int j= 0; j < width; j++)
                {
                    if (helperArray[i, j] != null)
                    {
                        //helperArray[i, j]
                        mapLines[i] += AddFillers(helperArray[i, j], filler, longestRoomName);
                        if (CanGoFromRoom(FindRoomByName(helperArray[i,j]), "east"))
                        {
                            mapLines[i] += "-";
                        }
                        else
                        {
                            mapLines[i] += " ";
                        }
                    }
                    else
                    {
                        mapLines[i] += new string(' ', longestRoomName + 3);
                    }

                }
            }

            // Place the vertical connections and add to the mapToPrint string
            int openBracketPos = 0, closedBracketPos = 0, longestLineLength = (longestRoomName * width) + width + 10;
            string roomName = "";
            for (int i = 0; i < mapLines.Count; i++)
            {
                string spaceLine = "";
                map += mapLines[i] + "\n";
                for (int j = 0; j < mapLines[i].Length; j++)
                {
                    while (spaceLine.Length < longestLineLength) spaceLine += " ";

                    if (mapLines[i][j] == '[')
                    {
                        openBracketPos = j;
                        j++;
                        while (mapLines[i][j] != ']')
                        {
                            roomName += mapLines[i][j];
                            j++;
                        }
                        closedBracketPos = j;
                        Room? foundRoom = FindRoomByName(roomName);
                        if (foundRoom == null) throw new Exception($"Found room was null! |{roomName}|");
                        if (CanGoFromRoom(foundRoom, "south"))
                        {
                            StringBuilder sb = new StringBuilder(spaceLine);
                            sb[(openBracketPos + closedBracketPos) / 2] = '|';
                            spaceLine = sb.ToString();
                        }
                        roomName = "";
                    }
                }
                map += spaceLine + "\n";
            }

            Console.Clear();
            WriteMapWithHighlight(map);
            Console.WriteLine("\n\nPress Any Key to exit Map View");
            Console.ReadKey();
            Console.Clear();
        }

        private void FindGridDimensions(Room r, ref string s, int currPos, ref int maxE, ref int minW, int currDepth, ref int maxDepth, ref Dictionary<Room, bool> visited, ref Dictionary<Room, int> roomPos, ref Dictionary<Room, int> roomDepths)
        {
            Room currentRoom = r;
            if (!roomPos.ContainsKey(currentRoom)) roomPos[currentRoom] = currPos;
            if (!roomDepths.ContainsKey(currentRoom)) roomDepths[currentRoom] = currDepth;

            if (visited.ContainsKey(currentRoom)) return;
            else visited[currentRoom] = true;

            if (currPos > maxE)
            {
                s = s + $"[{currentRoom.ShortDescription}]";
                maxE = currPos;
            }
            if (currPos < minW)
            {
                s = $"[{currentRoom.ShortDescription}]" + s;
                minW = currPos;
            }

            if(currDepth > maxDepth) maxDepth = currDepth;

            if (CanGoFromRoom(currentRoom, "south"))
            {
                Room southRoom = currentRoom.Exits["south"];
                if (!visited.ContainsKey(southRoom))
                {
                    FindGridDimensions(currentRoom.Exits["south"], ref s, currPos, ref maxE, ref minW, currDepth+1, ref maxDepth, ref visited, ref roomPos, ref roomDepths);
                }
            }
            if(CanGoFromRoom(currentRoom, "north"))
            {
                Room northRoom = currentRoom.Exits["north"];
                if (!visited.ContainsKey(northRoom))
                {
                    FindGridDimensions(currentRoom.Exits["north"], ref s, currPos, ref maxE, ref minW, currDepth-1, ref maxDepth, ref visited, ref roomPos, ref roomDepths);
                }
            }
            if(CanGoFromRoom(currentRoom, "west"))
            {
                Room westRoom = currentRoom.Exits["west"];
                if (!visited.ContainsKey(westRoom))
                {
                    FindGridDimensions(r.Exits["west"], ref s, currPos - 1, ref maxE, ref minW, currDepth, ref maxDepth, ref visited, ref roomPos, ref roomDepths);
                }
            }
            if(CanGoFromRoom(currentRoom, "east"))
            {
                Room eastRoom = currentRoom.Exits["east"];
                if (!visited.ContainsKey(eastRoom))
                {
                    FindGridDimensions(r.Exits["east"], ref s, currPos + 1, ref maxE, ref minW, currDepth, ref maxDepth, ref visited, ref roomPos, ref roomDepths);
                }
            }
        }

        private string AddFillers(string roomName, string filler, int requiredWidth)
        {
            string newRoomName = roomName;
            bool alternate = true;
            while(newRoomName.Length < requiredWidth)
            {
                if(alternate)
                {
                    newRoomName = filler + newRoomName;
                    alternate = false;
                }
                else
                {
                    newRoomName = newRoomName + filler;
                    alternate = true;
                }
            }
            return "[" + newRoomName + "]";
        }

        private int HowManyRoomsInLine(string s)
        {
            int count = 0;
            foreach (char c in s)
            {
                if (c == '[') count++;
            }
            return count;
        }

        private bool CanGoFromRoom(Room? room, string direction)
        {
            if (room != null && room.Exits.ContainsKey(direction))
            {
                if (room.Exits[direction] != null) return true;
                else return false;
            }
            return false;
        }

        private Room? FindRoomByName(string roomName)
        {
            roomName = roomName.Replace(filler, "");
            if (rooms == null) return null;
            foreach (Room room in rooms)
            {
                if (room.ShortDescription == roomName) return room;
            }
            return null;
        }

        public virtual void checkForAvailableObjectives()
        {

        }

        public virtual void addQuests()
        {

        }
        public virtual void addNPCs()
        {

        }
        public string returnBiomeName()
        {
            if (BiomeName == null) return "Null Biome, someone forgot to set their biome name.";
            else return BiomeName;
        }

    }

}