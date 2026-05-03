using FinanceTracker.Library.models;
using FinanceTracker.App.interfaces;

namespace FinanceTracker.App.commandProviders;

public class MenuCommandProvider : ICommandProvider
{
    private readonly Action<ICommandProvider> _changer;

    public MenuCommandProvider() { }

    public MenuCommandProvider(Action<ICommandProvider> changer)
    {
        _changer = changer;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var Dictionary = new Dictionary<string, (string, Action)>();

        Dictionary.Add("1", ("Enter your user Account", (() =>
        {
            User user = new User();
            UserManager userManager = new UserManager(user);
            _changer(new UserCommandProvider(_changer, userManager));
        })));

        Dictionary.Add("0", ("Exit", () => Environment.Exit(0)));

        return Dictionary;
    }
}



















