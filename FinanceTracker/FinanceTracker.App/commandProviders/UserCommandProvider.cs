using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;

public class UserCommandProvider : ICommandProvider
{
    private readonly Action<ICommandProvider> _changer;
    private readonly AuthManager _manager;
    public UserCommandProvider(Action<ICommandProvider> changer, AuthManager manager)
    {
        _changer = changer;
        _manager = manager;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var Dictionary = new Dictionary<string, (string, Action)>();

        Dictionary.Add("1", ("Показати баланс", () => Console.WriteLine()));

        Dictionary.Add("0", ("В головне меню (вихід з акаунту)", () => _changer(new MenuCommandProvider(_changer, _manager))));

        return Dictionary;
    }
}













