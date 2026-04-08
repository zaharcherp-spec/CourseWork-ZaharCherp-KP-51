using BankSystem.Library;

public class UserTaskGiver : ITaskGiver
{
    private readonly Action<ITaskGiver> _changer;
    private readonly UserManager Manager;
    public UserTaskGiver(Action<ITaskGiver> changer, UserManager manager)
    {
        _changer = changer;
        Manager = manager;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var Dictionary = new Dictionary<string, (string, Action)>();

        Dictionary.Add("1", ("Show Balance", () => Console.WriteLine("Balance: 0")));
        Dictionary.Add("0", ("Back to Main Menu", () => _changer(new GreeterGiver(_changer))));

        return Dictionary;
    }
}



