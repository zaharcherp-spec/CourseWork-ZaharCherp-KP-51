using FinanceTracker.Library.Models;
using FinanceTracker.App.interfaces;

namespace FinanceTracker.App.CommandProviders;

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
        {
            Dictionary.Add("0", ("Exit", () => Environment.Exit(0)));

            return Dictionary;
        }
    }
}
























