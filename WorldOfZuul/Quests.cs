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
        public string QuestName { get; internal set; }
        public string QuestDescription { get; internal set; }
        public bool IsCompleted { get; internal set; }
        public bool IsActive { get; private set; }
        public List<QuestObjective> QuestObjectives { get; private set; } = new List<QuestObjective>();
        

        public Quest(string questName, string questDescription, bool isCompleted, bool isActive, List<QuestObjective> questObjectives = null!)
        {
            QuestName = questName;
            QuestDescription = questDescription;
            IsCompleted = isCompleted;
            IsActive = isActive;
            QuestObjectives = questObjectives;
            
        }

        public virtual void CompleteQuest()
        {
            IsCompleted = true;
            GameManager.IsActiveQuest = false;
        }
        public void AddObjective(string description, string neededItems)
        {
            QuestObjectives.Add(new QuestObjective(description, neededItems));
        }


        public bool AreAllObjectivesCompleted()
        {
            return QuestObjectives.All(o => o.IsCompleted);
        }

        public void AddObjective(QuestObjective objective)
        {
            QuestObjectives.Add(objective);
        }
        public override string ToString()
        {
            return QuestName;
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
        public string NeededItems { get; set; }

        public QuestObjective(string description, string neededItems)
        {
            Description = description;
            IsCompleted = false;
            NeededItems = neededItems;
        }

        public void CompleteObjective()
        {
            IsCompleted = true;
        }
    }

}