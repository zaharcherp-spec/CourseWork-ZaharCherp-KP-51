public class GreeterGiver : ITaskGiver
{
    private readonly Action<ITaskGiver> _changer;

    public GreeterGiver(Action<ITaskGiver> changer)
    {
        _changer = changer;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var Dictionary = new Dictionary<string, (string, Action)>();

        Dictionary.Add("1", ("Enter your user Account", () => _changer(new UserTaskGiver(_changer))));
        Dictionary.Add("2", ("Enter Admin Status", () => _changer(new AdminTaskGiver(_changer))));
        Dictionary.Add("0", ("Exit", () => Environment.Exit(0)));

        return Dictionary;
    }
}













