public class AdminTaskGiver : ITaskGiver
{
    private readonly Action<ITaskGiver> _changer;

    public AdminTaskGiver(Action<ITaskGiver> changer)
    {
        _changer = changer;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var Dictionary = new Dictionary<string, (string, Action)>();

        Dictionary.Add("1", ("Show all users", () => Console.WriteLine("Users list...")));
        Dictionary.Add("0", ("Back to Main Menu", () => _changer(new GreeterGiver(_changer))));

        return Dictionary;
    }
}



