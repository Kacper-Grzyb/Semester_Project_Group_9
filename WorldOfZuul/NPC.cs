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
    private List<string> riddles;

    public Riddler(string name, string description, List<string> riddles): base(name, description)
    {
        this.riddles = riddles;
    }

    public override void Interact()
    {
        base.Interact();
        Console.WriteLine("I have a riddle for you!");
    }

    
}