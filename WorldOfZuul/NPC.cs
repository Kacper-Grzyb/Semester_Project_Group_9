using System.ComponentModel;
using System.Security.Policy;
using WorldOfZuul;

public class NPC
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Quest> Quests { get; private set; } = new List<Quest>();
    public NPC(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public virtual void AddQuestToNPC(Quest quest)
    {
    }


    public virtual void Interact()
    {
        Console.WriteLine($"You are interacting with {Name}. {Description}");
    }
}
    
public class Riddler : NPC
{
    private List<(string answer, string riddle)> riddles;
    private int currentRiddleIndex;

    public Riddler(string name, string description): base(name, description)
    {
        riddles = new List<(string, string)>();
        currentRiddleIndex = 0;
        InitializeRiddles();
    }

    public void InitializeRiddles()
    {
        riddles.Add(("tree", "I wear a green coat from head to toe, am older than you but still grow. I clean the air you breathe, you see. Cut me down, and you'll miss me. What am I?"));
        riddles.Add(("poaching", "I am a silent war waged in forests and plains. I take what is not given and bring endless pains. I threaten kings of the jungle and monarchs of the sky, with every life I take, a piece of the world says goodbye. What am I?"));
        riddles.Add(("forest", "I’m home to creatures great and small, I cover about a quarter of your world in all. I can be tropical or cool, and chopping me down is really not cool. What am I"));
    }
        
    public override void Interact()
    {
        base.Interact();
        if(currentRiddleIndex > riddles.Count)
        {
           GiveNextRiddle();
        }
    
        if(currentRiddleIndex == 0)
        {
        Console.WriteLine("I have a riddle for you!");
        }
        else
        {
            Console.WriteLine("I have another riddle for you!");
        }

        Console.WriteLine("Do you want to proceed? (yes/no)");
        string? ans = Console.ReadLine();
        if (ans?.Trim().ToLower() == "yes")
        {
            var riddle = GiveNextRiddle();
            Console.WriteLine(riddle.riddle);
            Console.WriteLine("What's your answer?: ");
            string? playerAnswer = Console.ReadLine();
            CheckAnswer(playerAnswer, riddle.answer);
        }
    }   

    private (string answer, string riddle) GiveNextRiddle()
    {
        if (currentRiddleIndex < riddles.Count)
        {
            return riddles[currentRiddleIndex++];
        }
        else
        {
            return ("", "You have solved all my riddles!");
        }
    }

    private void CheckAnswer(string? playerAnswer, string correctAnswer)
    {
        if (playerAnswer?.Trim().ToLower() == correctAnswer.ToLower())
        {
            Console.WriteLine("Correct! Well done.");
            GameManager.score += 5;
        }
        else
        {
            Console.WriteLine($"Incorrect. The correct answer was: {correctAnswer}");
            GameManager.score -= 7;
        }
    }
}

public class QuestGiver : NPC
{
    List<Quest> availableQuests = new List<Quest>();

    public QuestGiver(string name, string description) : base(name, description)
    {
    }

    public override void AddQuestToNPC(Quest quest)
    {
        availableQuests.Add(quest);
    }

    public override void Interact()
    {
        base.Interact();
        if (availableQuests.Count > 0)
        {
            Console.WriteLine("I have a task for you, are you interested?");
            Console.WriteLine("Do you want to accept the quest? (yes/no)");
            string? ans = Console.ReadLine();
            if (ans?.Trim().ToLower() == "yes")
            {
                GameManager.ActiveQuest = availableQuests[0];
                Console.WriteLine($"You have accepted the quest: {availableQuests[0].QuestName}");
                // Optionally, remove the quest from availableQuests if it's a one-time quest
                // availableQuests.RemoveAt(0);
            }
            else
            {
                Console.WriteLine("Maybe another time then.");
            }
        }
        else
        {
            Console.WriteLine("I don't have any tasks for you at the moment.");
        }
    }
}