using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;

public class UserCommandProvider : ICommandProvider
{
    private readonly Action<ICommandProvider> _changer;
    private readonly AuthManager _authmanager;
    private readonly Menager _menager;

    public UserCommandProvider(Action<ICommandProvider> changer, AuthManager manager, Menager menager)
    {
        _changer = changer;
        _authmanager = manager;
        _menager = menager;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var dictionary = new Dictionary<string, (string, Action)>
        {
            { "0", ("В головне меню (вихід з акаунту)", () =>
                {
                    _authmanager.Logout();
                    _changer(new MenuCommandProvider(_changer, _authmanager,_menager));
                })
            }
           {"1",("Депозит"),()=>}
        };
        return dictionary;
    }
}































