public class UserCommandProvider : ICommandProvider
{
    private readonly Action<ICommandProvider> _changer;
    private readonly UserManager Manager;
    public UserCommandProvider(Action<ICommandProvider> changer, UserManager manager)
    {
        _changer = changer;
        Manager = manager;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var Dictionary = new Dictionary<string, (string, Action)>();

        Dictionary.Add("1", ("Show Balance", () => Console.WriteLine(Manager.GetBalance())));

        Dictionary.Add("0", ("Back to Main Menu", () => _changer(new MenuCommandProvider(_changer))));

        

        return Dictionary;
    }
}









