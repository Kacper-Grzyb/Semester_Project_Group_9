using System.ComponentModel;

public class NPC
{
    public string Name { get; set; }
    public string Description { get; set; }

    public NPC(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public virtual void Interact()
    {
        Console.WriteLine($"You are interacting with {Name}. {Description}");
    }
}
    
public class Riddler : NPC
{
    private Queue<(string answer, string riddle)> riddles;
    public Riddler(string name, string description): base(name, description)
    {
       riddles = new Queue<(string , string)>();
       initializeRiddles();
    }

    public void initializeRiddles()
    {
        riddles.Enqueue(("tree.", "I wear a green coat from head to toe, am older than you but still grow. I clean the air you breathe, you see. Cut me down, and you'll miss me. What am I?"));
        riddles.Enqueue(("poaching","I am a silent war waged in forests and plains. I take what is not given and bring endless pains. I threaten kings of the jungle and monarchs of the sky, with every life I take, a piece of the world says goodbye. What am I?"));
        riddles.Enqueue(("A forest.", "I’m home to creatures great and small, I cover about a quarter of your world in all. I can be tropical or cool, and chopping me down is really not cool. What am I"));
    }

    public override void Interact()
    {
        base.Interact();
        Console.WriteLine("I have a riddle for you!");
        Console.WriteLine("Do you want to proceed?");
        string? ans = Console.ReadLine();
        if(ans == "yes")
        {
            Console.WriteLine(GiveNextRiddle());
        }
    }   

    public (string asnwer, string riddle) GiveNextRiddle()
    {
        if (riddles.Count > 0)
        {
            return riddles.Dequeue();
        }
        else
        {
            return ("", "You have solved all my riddles!");
        }
    }
}