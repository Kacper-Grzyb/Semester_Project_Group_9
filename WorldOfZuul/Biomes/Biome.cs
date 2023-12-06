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
            mapLines.Add("");
            // getting rows of rooms 'on the same level' to the list mapLines
            UpdateRooms(currentRoom, ref mapLines, 0, ref printedRooms);

            // getting the longest row of rooms
            string longestRoomChain = "";
            int currPos = 0, maxE = -1, minW = 0;
            Dictionary<Room, bool> visitedRooms = new Dictionary<Room, bool>();
            Dictionary<Room, int> roomPositions = new Dictionary<Room, int>(); // this will be useful later
            FindMapWidth(currentRoom, ref longestRoomChain, currPos, ref maxE, ref minW, ref visitedRooms, ref roomPositions);
            width = HowManyRoomsInLine(longestRoomChain);
            // northmost room is currPos 0 and maxE and minW were set in the FindMapWidth function

            // finding the grid position of starting room
            foreach (var room in rooms)
            {
                roomPositions[room] -= minW;
            }

            // adding placeholders to allign all of the rooms
            for (int i = 0; i < mapLines.Count; i++) AddPlaceholders(ref mapLines, width - 1, i, roomPositions);

            //removing placeholders so that they don't appear on the map
            string placeholderSwap = new string(' ', longestRoomName);
            for (int i = 0; i < mapLines.Count; i++)
            {
                mapLines[i] = mapLines[i].Replace("|Placeholder|", placeholderSwap);
            }

            for (int i = 0; i < mapLines.Count; i++)
            {
                int openBracketI, closedBracketI;
                for (int j = 0; j < mapLines[i].Length; j++)
                {
                    if (mapLines[i][j] == '[' || mapLines[i][j] == '|')
                    {
                        j++;
                        openBracketI = j;
                        while (mapLines[i][j] != ']' && mapLines[i][j] != '|') j++;
                        closedBracketI = j;
                        while (closedBracketI - openBracketI < longestRoomName)
                        {
                            mapLines[i] = mapLines[i].Insert(openBracketI, filler);
                            closedBracketI++;
                            mapLines[i] = mapLines[i].Insert(closedBracketI, filler);
                            closedBracketI++;
                            j += 2;
                        }
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

        void UpdateRooms(Room r, ref List<string> list, int index, ref Dictionary<Room, bool> printedRooms)
        {
            if (printedRooms.ContainsKey(r)) return;
            Room currentRoom = r;
            while (CanGoFromRoom(currentRoom, "west")) currentRoom = currentRoom.Exits["west"];
            if (list[index].Length > 0) list[index] += " ";
            while (true)
            {
                if (CanGoFromRoom(currentRoom, "south"))
                {
                    if (list.Count == index + 1) list.Add("");
                    UpdateRooms(currentRoom.Exits["south"], ref list, index + 1, ref printedRooms);
                }

                list[index] += $"[{currentRoom.ShortDescription}]";
                printedRooms[currentRoom] = true;
                if (CanGoFromRoom(currentRoom, "east"))
                {
                    list[index] += "-";
                    currentRoom = currentRoom.Exits["east"];
                }
                else break;
            }
        }

        private void FindMapWidth(Room r, ref string s, int currPos, ref int maxE, ref int minW, ref Dictionary<Room, bool> visited, ref Dictionary<Room, int> roomPos)
        {
            Room currentRoom = r;
            if (visited.ContainsKey(currentRoom)) return;
            visited[currentRoom] = true;
            if (!roomPos.ContainsKey(currentRoom)) roomPos[currentRoom] = currPos;
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

            if (CanGoFromRoom(currentRoom, "south")) FindMapWidth(currentRoom.Exits["south"], ref s, currPos, ref maxE, ref minW, ref visited, ref roomPos);
            int initialPos = currPos;


            while (CanGoFromRoom(currentRoom, "west"))
            {
                currentRoom = currentRoom.Exits["west"];
                if (visited.ContainsKey(currentRoom)) return;
                visited[currentRoom] = true;
                currPos--;
                if (!roomPos.ContainsKey(currentRoom)) roomPos[currentRoom] = currPos;

                if (CanGoFromRoom(currentRoom, "south")) FindMapWidth(currentRoom.Exits["south"], ref s, currPos, ref maxE, ref minW, ref visited, ref roomPos);
                if (currPos < minW)
                {
                    s = $"[{currentRoom.ShortDescription}]" + s;
                    minW = currPos;
                }
            }

            currPos = initialPos;
            currentRoom = r;
            while (CanGoFromRoom(currentRoom, "east"))
            {
                currentRoom = currentRoom.Exits["east"];
                if (visited.ContainsKey(currentRoom)) return;
                visited[currentRoom] = true;
                currPos++;
                if (!roomPos.ContainsKey(currentRoom)) roomPos[currentRoom] = currPos;

                if (CanGoFromRoom(currentRoom, "south")) FindMapWidth(currentRoom.Exits["south"], ref s, currPos, ref maxE, ref minW, ref visited, ref roomPos);
                if (currPos > maxE)
                {
                    s = s + $"[{currentRoom.ShortDescription}]";
                    maxE = currPos;
                }
            }
        }

        private void AddPlaceholders(ref List<string> mapLines, int maxPos, int depth, Dictionary<Room, int> positions)
        {
            string s = mapLines[depth];
            List<Room> rooms = new List<Room>();

            // Get rooms from string
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '[')
                {
                    string temp = "";
                    i++;
                    while (s[i] != ']')
                    {
                        temp += s[i];
                        i++;
                    }
                    var room = FindRoomByName(temp);
                    if (room != null)
                    {
                        rooms.Add(room);
                    }
                }
            }

            int countBrackets = -1, loopLength = s.Length;
            for (int i = 0; i < loopLength; i++)
            {
                if (s[i] == '[')
                {
                    countBrackets++;
                    int border;
                    if (countBrackets == 0) border = -1;
                    else border = positions[rooms[countBrackets - 1]];

                    int k = positions[rooms[countBrackets]] - 1;
                    while (k > border)
                    {
                        s = s.Insert(i, "|Placeholder| ");
                        //s = s.Insert(i, placeholder + " ");
                        i += 14;
                        loopLength += 14;
                        k--;
                    }
                }
            }
            while (positions[rooms[countBrackets]] < maxPos)
            {
                s = s + " |Placeholder|";
                //s = s + " " + placeholder;
                positions[rooms[countBrackets]]++;
            }
            mapLines[depth] = s;
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

        private bool CanGoFromRoom(Room room, string direction)
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