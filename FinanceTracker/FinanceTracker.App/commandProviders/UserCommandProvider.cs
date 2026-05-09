using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;

public class UserCommandProvider : BaseCommandProvider
{
    public UserCommandProvider(Action<BaseCommandProvider> changer, AuthManager authManager, Menager menager)
        : base(changer, authManager, menager) { }

    public override Dictionary<string, (string Name, Func<Task> Action)> GetCommands()
    {
        return new Dictionary<string, (string Name, Func<Task> Action)>
        {
            { "0", ("В головне меню (вихід з акаунту)", () =>
                {
                    _authManager.Logout();
                    _changer(new MenuCommandProvider(_changer, _authManager, _menager));
                    return Task.CompletedTask;
                })
            },

             { "1", ("В меню фінансових операцій",()=>
             {
            _changer(new FinanceCommandProvider(_changer, _authManager, _menager));

            return Task.CompletedTask;
             })
             }
        };
    }
}

































