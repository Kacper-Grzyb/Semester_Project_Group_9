using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Quest
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public bool IsCompleted { get; internal set; }
        public int RoomId {get; internal set;}

        public Quest(string name, string description, bool isCompleted, int roomId)
        {
            Name = name;
            Description = description;
            IsCompleted = isCompleted;
            RoomId = roomId;
        }

        public void CompleteQuest()
        {
            IsCompleted = true;
        }
    }
    public class JungleQuest : Quest
    {
        
        public JungleQuest(string name, string description, bool isCompleted, int roomId) : base(name, description, isCompleted, roomId)
        {
            Name = name;
            Description = description;
            IsCompleted = false;
            RoomId = roomId;
        }
        
    }

}