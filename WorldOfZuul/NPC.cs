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
    public int correctAnswers { get; private set; } = 0;
    public List<Item> items = new List<Item>(){new Item("coin", "You can distract enemies with this")};
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
        
        
        if(currentRiddleIndex > riddles.Count)
        {
          Console.WriteLine("You have answered all my riddles!");
          return; 
        }
    
        if(currentRiddleIndex == 0)
        {
            base.Interact();
            Console.WriteLine($"I have {riddles.Count} riddles for you!");
            Console.WriteLine("And if you want a prise you have to correctly answer all my riddles!");
            Console.WriteLine("Do you want to proceed? (yes/no)");
        }
        else
        {
            Console.WriteLine("I have another riddle for you!");
        }

        
        string? ans = Console.ReadLine();
        if (ans?.Trim().ToLower() == "yes")
        {
            while (currentRiddleIndex < riddles.Count)
            {
                var riddle = GiveNextRiddle();
                Console.WriteLine(riddle.riddle);
                Console.WriteLine("What's your answer?: ");
                string? playerAnswer = Console.ReadLine();
                CheckAnswer(playerAnswer, riddle.answer);
            }
            if(currentRiddleIndex >= riddles.Count)
            {
                GiveNextRiddle();
            }
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
            if(correctAnswers == riddles.Count){
                
                GameManager.Inventory?.AddNPCItem(items[0]);
                items.RemoveAt(0);

                return ("", "You have correctly solved all my riddles! Here is your prize: A coin");
            }else
            {
                return ("", "You have not solved all my riddles! But didnt get all the answers :( !");
                
            }
            
        }
    }

    private void CheckAnswer(string? playerAnswer, string correctAnswer)
    {
        
        if (playerAnswer?.Trim().ToLower() == correctAnswer.ToLower())
        {
            Console.WriteLine("Correct! Well done.");
            GameManager.score += 5;
            correctAnswers++;
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
    public List<Item> items = new List<Item>(){new Item("dyamite", "You can destroy the base camp with this, but firstly you'll need somthing to light it with")};
    int questIndex = 0;
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
        
            if(GameManager.ActiveQuest?.QuestName == "Find evidence" && GameManager.ActiveQuest.IsCompleted)
            {
                Console.WriteLine("You have borught back evidence for the quest giver! \nYou are one step closer to stop those pesky poachers! ");

                Console.WriteLine("You have completed the quest! And you got 20 points for envailing the poachers plan");
                GameManager.score += 20;
                questIndex++;
                GameManager.ActiveQuest.CompleteQuest();
                return;
            }
            if(GameManager.ActiveQuest?.QuestName == "Destroy base camp" && GameManager.ActiveQuest.IsCompleted){
                Console.WriteLine("You have destroyed the base camp! \n You stopped at least some of them");
                Console.WriteLine("You have completed the quest! And you got 20 points for envailing the poachers plan");
                GameManager.score += 20;
                questIndex++;
                GameManager.ActiveQuest.CompleteQuest();
                return;
            }
        
        if (availableQuests.Count > 0)
        {
            Console.WriteLine("I have a task for you, are you interested?");
            Console.WriteLine("Do you want to accept the quest? (yes/no)");
            string? ans = Console.ReadLine();
            if (ans?.Trim().ToLower() == "yes" && questIndex == 0)    
            {
                GameManager.ActiveQuest = availableQuests[questIndex];
                GameManager.IsActiveQuest = true;
                Console.WriteLine($"You have accepted the quest: {availableQuests[questIndex].QuestName}");
                
            }
            if(ans?.Trim().ToLower() == "yes" && questIndex == 1)
            {
                GameManager.ActiveQuest = availableQuests[questIndex];
                GameManager.IsActiveQuest = true;
                GameManager.Inventory?.AddNPCItem(items[0]);
                Console.WriteLine("Now that you found the evidence, you have to destroy the poachers base camp!");
                Console.WriteLine($"You have accepted the quest: {availableQuests[questIndex].QuestName}");
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
public class Gambler : NPC
{
    private List<(string answer, string riddle)> Gambas;
    private int chosenGambaIndex;


    public Gambler(string name, string description): base(name, description)
    {      
        Gambas = new List<(string, string)>();
    }
    public override void Interact()
    {
        WowRoulette();
    }
    public void WowRoulette()
    {
        Random ran = new Random();
        int num = 100;  
        Console.WriteLine("the rules are simple, you'll roll from 1-100 and what number you get the other has to roll from 1-(number that the previous one got)");
        Console.WriteLine("if you someone gets one they lose");
        
        Console.WriteLine("do you want to play? (yes/no)");
        string? ans = Console.ReadLine();
        if(ans == "yes")
        {
            Console.WriteLine("As my guest you can start");
            while(num != 1)
            {
                Console.WriteLine("Roll the dice (Press Enter)");
                
                Console.ReadLine();
                num = ran.Next(1, num);
                if(num == 1)
                {
                    Console.WriteLine("You rolled: " + num);
                    Console.WriteLine("You lost");
                    break;
                }
                Console.WriteLine("You rolled: " + num);
                Console.WriteLine("My turn");
                num = ran.Next(1, num);
                Console.WriteLine("I rolled: " + num);
            }
            
        }
    }

}

