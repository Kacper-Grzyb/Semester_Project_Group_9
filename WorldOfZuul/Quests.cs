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
        public bool IsActive { get; private set; }
        public List<QuestObjective> Objectives { get; private set; } = new List<QuestObjective>();

        public Quest(string name, string description, bool isCompleted, bool isActive, List<QuestObjective> objectives = null!)
        {
            Name = name;
            Description = description;
            IsCompleted = isCompleted;
            IsActive = isActive;
            Objectives = objectives;

        }

        public virtual void CompleteQuest()
        {
            IsCompleted = true;
            GameManager.IsActive = false;
        }
        public void AddObjective(string description)
        {
            Objectives.Add(new QuestObjective(description));
        }


        public bool AreAllObjectivesCompleted()
        {
            return Objectives.All(o => o.IsCompleted);
        }

        public void AddObjective(QuestObjective objective)
        {
            Objectives.Add(objective);
        }
        public override string ToString()
        {
            return Name;
        }

        public void CheckQuestCompletion()
        {
            if (AreAllObjectivesCompleted())
            {
                CompleteQuest();
            }
        }
    }
    public class QuestObjective
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public QuestObjective(string description)
        {
            Description = description;
            IsCompleted = false;
        }

        public void CompleteObjective()
        {
            IsCompleted = true;
        }
    }

}