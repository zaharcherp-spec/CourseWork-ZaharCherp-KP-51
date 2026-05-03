using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;
public class UserCommandProvider : ICommandProvider
{
    private readonly Action<ICommandProvider> _changer;
    private readonly Menager Menager;
    public UserCommandProvider(Action<ICommandProvider> changer, Menager manager)
    {
        _changer = changer;
        Menager = manager;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var Dictionary = new Dictionary<string, (string, Action)>();

        Dictionary.Add("1", ("Show Balance", () => Console.WriteLine()));

        Dictionary.Add("0", ("Back to Main Menu", () => _changer(new MenuCommandProvider(_changer))));

        return Dictionary;
    }
}













