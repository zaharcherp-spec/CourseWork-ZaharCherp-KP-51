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
        var dictionary = new Dictionary<string, (string, Action)>
        {
            { "0", ("В головне меню (вихід з акаунту)", () =>
                {
                    _manager.Logout();
                    _changer(new MenuCommandProvider(_changer, _manager));
                })
            }
        };
        return dictionary;
    }
}






























